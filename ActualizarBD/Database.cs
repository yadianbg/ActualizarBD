using CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace ActualizarBD
{
    public class Database
    {
        // Base de datos temporal
        private const string DB_TEMP = "master";

        public string Server { get; }
        public string Catalog { get; }
        public bool Security { get; }
        public string User { get; }
        public string Password { get; }

        /// <summary>
        /// Constructor de la clase que se inicializa mediante sus parametros.
        /// </summary>
        /// <param name="Server">
        /// Nombre del servidor SQL.
        /// </param>
        /// <param name="Catalog">
        /// Nombre del catalogo o base de datos.
        /// </param>
        /// <param name="Security">
        /// Tipo de seguridad del servidor.
        ///  (false -> seguridad integrada)
        ///  (true  -> seguridad de sql server)
        /// </param>
        /// <param name="User">
        /// Usuario para conectarse al servidor si se utiliza seguridad de sql.
        /// </param>
        /// <param name="Password">
        /// Contraseña para conectarse al servidor si se utiliza seguridad de sql.
        /// </param>
        public Database(string Server, string Catalog, bool Security, string User, string Password)
        {
            this.Server = Server;
            this.Catalog = Catalog;
            this.Security = Security;
            this.User = User;
            this.Password = Password;
        }

        /// <summary>
        /// Determina si existe la base de datos en el servidor.
        /// </summary>
        /// <returns>
        /// true  -> Existe la BD
        /// false -> No existe la BD
        /// </returns>
        /// <exception cref="SicemaException">
        /// Error al acceder al servidor o al ejecutar la consulta.
        /// </exception>
        public bool Exists()
        {
            bool retval = false;

            try
            {
                // Consulta de datos
                string query = "SELECT name FROM sysdatabases\n";
                query += "WHERE name = @Nombre";
                // Parametros de la consulta
                List<Parameter> lstParam = new List<Parameter>
                {
                    new Parameter("@Nombre", Catalog)
                };
                // Crear conexion con el servidor
                SqlConn con = new SqlConn(Server, DB_TEMP, Security, User, Password);
                // Ejecutar la consulta de seleccion
                DataTable dt = con.ListDataQuery(query, lstParam);
                // Recuperar los resultados
                retval = (dt.Rows.Count != 0);
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException("No se ha podido ejecutar la consulta.", ex);
            }

            // Devolver el estado
            return retval;
        }

        /// <summary>
        /// Crea una base de datos en el servidor.
        /// </summary>
        /// <param name="Path">
        /// Ubicacion de los archivos de datos
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al acceder al servidor SQL o al crear la base de datos.
        /// </exception>
        public void Create(string Path)
        {
            const string errMessage = "No se ha podido crear la base de datos.";

            // Asegurar que el camino termina con el caracter \
            if (!Path.EndsWith(@"\"))
            {
                Path += @"\";
            }
            try
            {
                // Consulta de datos
                string query = $"CREATE DATABASE {Catalog} ON PRIMARY\n";
                query += $"(NAME = Sicema_Data, FILENAME = '{Path}{Catalog}_Data.mdf',\n";
                query += "SIZE = 5 MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10 %)\n";
                query += $"LOG ON ( NAME = Sicema_Log, FILENAME = '{Path}{Catalog}_Log.ldf',\n";
                query += "SIZE = 5 MB, MAXSIZE = UNLIMITED, FILEGROWTH = 10 %)";
                // Crear conexion con el servidor
                SqlConn con = new SqlConn(Server, DB_TEMP, Security, User, Password);
                // Ejecutar la consulta de actualizacion
                con.ExecuteQuery(query);
                // Chequear que se creo la BD correctamente
                if (!Exists())
                {
                    throw new SicemaException(errMessage);
                }
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException(errMessage, ex);
            }
        }

        /// <summary>
        /// Elimina una base de datos del servidor.
        /// </summary>
        /// <exception cref="SicemaException">
        /// Error al acceder al servidor SQL o al eliminar la base de datos.
        /// </exception>
        public void Delete()
        {
            const string errMessage = "No se ha podido eliminar la base de datos.";

            try
            {
                // Consulta de datos
                string query = $"DROP DATABASE {Catalog}";
                // Crear conexion con el servidor
                SqlConn con = new SqlConn(Server, DB_TEMP, Security, User, Password);
                // Ejecutar la consulta de actualizacion
                con.ExecuteQuery(query);
                // Chequear que se elimino la BD correctamente
                if (Exists())
                {
                    throw new SicemaException(errMessage);
                }
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException(errMessage, ex);
            }
        }

        /// <summary>
        /// Determina la version de la base de datos.
        /// </summary>
        /// <param name="Version">
        /// Cadena que devuelve la version de la base de datos.
        /// </param>
        /// <param name="Revision">
        /// Cadena que devuelve la revision de la base de datos.
        /// </param>
        /// <param name="Update">
        /// Cadena que devuelve la actualizacion de la base de datos.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al acceder a la informacion de la version.
        /// </exception>
        public void Version(ref string Version, ref string Revision, ref string Update)
        {
            const string errMessage = "No se ha podido recuperar la versión de la base de datos.";

            try
            {
                // Consulta de datos
                string query = "SELECT TOP 1 * FROM sicActualizar\n";
                query += "ORDER BY Id DESC";
                // Crear conexion con el servidor
                SqlConn con = new SqlConn(Server, Catalog, Security, User, Password);
                // Ejecutar la consulta
                DataTable dt = con.ListDataQuery(query);
                // Recuperar los resultados
                if (dt.Rows.Count != 0)
                {
                    DataRow row = dt.Rows[0];
                    Version = row.IsNull("Version") ? "4.0" : row["Version"].ToString();
                    Revision = row.IsNull("Revision") ? "1" : row["Revision"].ToString();
                    Update = row.IsNull("Actualizacion") ? "1" : row["Actualizacion"].ToString();
                }
                else
                {
                    throw new SicemaException(errMessage);
                }
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException(errMessage, ex);
            }
        }
    }
}
