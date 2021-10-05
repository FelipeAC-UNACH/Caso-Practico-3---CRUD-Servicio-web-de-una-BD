using PeliculaWS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace PeliculaWS
{
    /// <summary>
    /// Descripción breve de PelisWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PelisWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloMovies()
        {
            return "Bienvenido al servicio de listado de películas";
        }
        #region CRUD PELICULAS
        [WebMethod(Description ="Obtener toda la lista de películas")]
        public List<Pelicula> GetPeliculas()
        {
            using (PelisEntities1 conexion = new PelisEntities1())
            {
                var consulta = (from p in conexion.Peliculas select p);
                return consulta.ToList();
            }
        }

        [WebMethod(Description = "Insertar una película")]
        public bool CreatePeli(string Titulo, string Pais, int Anio, int Duracion, string Genero)
        {
            try
            {
                using (PelisEntities1 conexion = new PelisEntities1())
                {
                    Pelicula nuevo = new Pelicula();
                    nuevo.idPelicula = Guid.NewGuid();
                    nuevo.Titulo = Titulo;
                    nuevo.Pais = Pais;
                    nuevo.Anio = Anio;
                    nuevo.Duracion = Duracion;
                    nuevo.Genero = Genero;
                    conexion.Peliculas.Add(nuevo);
                    conexion.SaveChanges(); //Se guardan los datos
                    return true;
                }
            }catch(Exception)
            {
                return false;
            }
        }

        [WebMethod(Description = "Modificar una película")]
        public bool UpdatePeli(Guid id, string Titulo, string Pais, int Anio, int Duracion, string Genero)
        {
            try
            {
                using (PelisEntities1 conexion = new PelisEntities1())
                {
                    var consulta = (from p in conexion.Peliculas where p.idPelicula == id select p).FirstOrDefault();
                    if (consulta != null)
                    {
                        consulta.Titulo = Titulo;
                        consulta.Pais = Pais;
                        consulta.Anio = Anio;
                        consulta.Duracion = Duracion;
                        consulta.Genero = Genero;
                        conexion.SaveChanges(); //Se guardan los datos
                        return true;
                    }
                    else
                    {
                        return false;
                    }                    
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod(Description = "Eliminar una película")]
        public bool DeletePeli(Guid id)
        {
            try
            {
                using (PelisEntities1 conexion = new PelisEntities1())
                {
                    var consulta = (from p in conexion.Peliculas where p.idPelicula == id select p).FirstOrDefault();
                    if (consulta != null)
                    {
                        conexion.Peliculas.Remove(consulta);
                        conexion.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
    }
}
