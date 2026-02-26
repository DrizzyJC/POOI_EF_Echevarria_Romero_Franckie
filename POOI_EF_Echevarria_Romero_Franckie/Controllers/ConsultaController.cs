using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace POOI_EF_Echevarria_Romero_Franckie.Controllers
{
    public class ConsultaController : Controller
    {
        // GET: Consulta
        public ActionResult Consulta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Consulta(string dni)
        {
            // Validación: solo números y 8 dígitos
            if (string.IsNullOrEmpty(dni) || dni.Length != 8 || !long.TryParse(dni, out _))
            {
                ViewBag.Mensaje = "Ingrese un DNI válido de 8 dígitos numéricos";
                return View();
            }

            string pais = "";
            string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("usp_pais_x_dni_supervisor", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dni", dni);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            pais = dr["nompais"].ToString();
                        }
                    }
                }
            }

            if (pais == "")
                ViewBag.Mensaje = "No existe supervisor con el DNI ingresado";
            else
                ViewBag.Mensaje = "El supervisor pertenece al país: " + pais;

            return View();
        }


    }
}