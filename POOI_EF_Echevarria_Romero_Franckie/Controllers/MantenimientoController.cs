using POOI_EF_Echevarria_Romero_Franckie.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace POOI_EF_Echevarria_Romero_Franckie.Controllers
{
    public class MantenimientoController : Controller
    {
        public ActionResult Index(string mensaje = "")
        {
            List<Supervisor> lista = new List<Supervisor>();
            string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("usp_listar_supervisor", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Supervisor
                            {
                                idsup = (int)dr["idsup"],
                                nomsup = dr["nomsup"].ToString(),
                                dirsup = dr["dirsup"].ToString(),
                                emailsup = dr["emailsup"].ToString(),
                                dnisup = dr["dnisup"].ToString()
                            });
                        }
                    }
                }
            }

            ViewBag.Mensaje = mensaje;
            return View(lista);
        }
        List<Pais> ListarPaises()
        {
            List<Pais> lista = new List<Pais>();
            string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("usp_listar_paises", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Pais
                            {
                                idpais = (int)dr["idpais"],
                                nompais = dr["nompais"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }




        public ActionResult Create()
        {
            ViewBag.Paises = ListarPaises();
            return View(new Supervisor());
        }

            [HttpPost]
            public ActionResult Create(Supervisor s)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.Paises = ListarPaises();
                        return View(s);
                    }

                    string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

                    using (SqlConnection con = new SqlConnection(cn))
                    {
                        using (SqlCommand cmd = new SqlCommand("usp_agrega_supervisor", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@nomsup", s.nomsup);
                            cmd.Parameters.AddWithValue("@dirsup", s.dirsup);
                            cmd.Parameters.AddWithValue("@emailsup", s.emailsup);
                            cmd.Parameters.AddWithValue("@idpais", s.idpais);
                            cmd.Parameters.AddWithValue("@dnisup", s.dnisup);

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    ViewBag.Mensaje = "Supervisor registrado correctamente";
                    ViewBag.Paises = ListarPaises();
                    return View(new Supervisor());
                }
                catch (Exception ex)
                {
                    ViewBag.Mensaje = ex.Message;
                    ViewBag.Paises = ListarPaises(); 
                    return View(s);
                }
            }
        
        public ActionResult Delete(int id)
        {
            string cn = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("usp_eliminar_supervisor", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idsup", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index", new { mensaje = "Registro eliminado correctamente" });
        }

    
    
    }
}