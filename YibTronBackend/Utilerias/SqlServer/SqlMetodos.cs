//Librerías internas
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
//Librerías externas
using Microsoft.Data.SqlClient;

namespace YibTronBackend.Utilerias.SqlServer
{
    internal class SqlMetodos
    {
        #region Métodos...
        /// <summary>
        /// Este método sirve para traer la información de una tabla en sql server mediante un comando.
        /// </summary>
        /// <param name="cadenaConexion"></param>
        /// <param name="cadenaComando"></param>
        /// <returns>Valor DataTable, resultante del comando ejecutado.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<DataTable> obtenerTablaDatos(string cadenaConexion, string cadenaComando)
        {
            //Se comprueba de que la cadena de conexión y el comando sean recibidos.
            if (string.IsNullOrEmpty(cadenaConexion)) throw new Exception("Favor de mandar la cadena de conexión.");
            if (string.IsNullOrEmpty(cadenaComando)) throw new Exception("Favor de mandar la cadena del comando.");
            try
            {
                //Se crea objeto DataTable a devolver.
                DataTable tablaDatos = new DataTable();
                //Se instancia la clase SqlConnection, y se le manda la cadena de conexión como parámetro.
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    //Se comprueba el estado de la conexión con la base de datos, si está cerrada, la abrimos.
                    if (conexion.State == ConnectionState.Closed) await conexion.OpenAsync();
                    //Se instancia la clase SqlCommand, la cual ayudará a ejecutar el comando recibido.
                    using (SqlCommand comando = new SqlCommand())
                    {
                        //Se configura el comando.
                        comando.CommandType = CommandType.Text;
                        comando.Connection = conexion;
                        comando.CommandText = cadenaComando;
                        comando.Notification = null;
                        //Se instancia la clase SQLDataReader, el cual leerá y traerá la información de la tabla.
                        using (SqlDataReader lector = await comando.ExecuteReaderAsync())
                        {
                            //Carga la tabla con la información de la tabla.
                            tablaDatos.Load(lector);
                        }
                    }
                    //Se cierra la conexión.
                    if (conexion.State == ConnectionState.Open) await conexion.CloseAsync();
                    //Devuelve la tabla.
                    return tablaDatos;
                }
            }
            catch (Exception ex)
            {
                //Compara si el error fue por un nombre de columna invalido.
                if (ex.Message.Contains("Invalid column name"))
                {
                    //Obtiene el nombre de la columna recibido.
                    string[] cadenaSeparada = ex.Message.Split("'");
                    //Provoca una excepción con el nombre de la columna invalido.
                    throw new Exception($"El nombre de la columna \"{cadenaSeparada[1]}\" es invalido.");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Este método sirve para ejecutar un comando que no sea destinado a realizar una consulta, si no, a ejecutar una operación.
        /// </summary>
        /// <param name="cadenaConexion"></param>
        /// <param name="cadenaComando"></param>
        /// <returns>Valor int, resultante de las filas que fueron afectadas por dicho comando.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<int> ejecutarComando(string cadenaConexion, string cadenaComando)
        {
            //Se comprueba de que la cadena de conexión y el comando sean recibidos.
            if (string.IsNullOrEmpty(cadenaConexion)) throw new Exception("Favor de mandar la cadena de conexión.");
            if (string.IsNullOrEmpty(cadenaComando)) throw new Exception("Favor de mandar la cadena del comando.");
            //Variable para guardar la cantidad de filas afectadas por el comando.
            int filasAfectadas;
            //Se instancia la clase SqlConnection, y se le manda la cadena de conexión como parámetro.
            try
            {
                //Se instancia la clase SqlConnection, y se le manda la cadena de conexión como parámetro.
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    //Se comprueba el estado de la conexión con la base de datos, si está cerrada, la abrimos.
                    if (conexion.State == ConnectionState.Closed) await conexion.OpenAsync();
                    //Se instancia la clase SqlCommand, la cual ayudará a ejecutar el comando recibido.
                    using (SqlCommand comando = new SqlCommand())
                    {
                        //Se configura el comando.
                        comando.CommandType = CommandType.Text;
                        comando.Connection = conexion;
                        comando.CommandText = cadenaComando;
                        comando.Notification = null;
                        //Se asigna la cantidad de filas que fueron afectadas
                        filasAfectadas = await comando.ExecuteNonQueryAsync();
                    }
                    //Se cierra la conexión.
                    if (conexion.State == ConnectionState.Open) await conexion.CloseAsync();
                    await conexion.DisposeAsync();
                }
                //Devuelve filas afectadas.
                return filasAfectadas;
            }
            catch (Exception ex)
            {
                //Compara si el error fue por un nombre de columna invalido.
                if (ex.Message.Contains("Invalid column name"))
                {
                    //Obtiene el nombre de la columna recibido.
                    string[] cadenaSeparada = ex.Message.Split("'");
                    //Provoca una excepción con el nombre de la columna invalido.
                    throw new Exception($"El nombre de la columna \"{cadenaSeparada[1]}\" es invalido.");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        /// <summary>
        /// Este método sirve para ejecutar un comando y devolver la primer columna del resultado.
        /// </summary>
        /// <param name="cadenaConexion"></param>
        /// <param name="cadenaComando"></param>
        /// <returns>Valor object, resultado del comando sql ejecutado.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<object?> ejecutarEscalar(string cadenaConexion, string cadenaComando)
        {
            //Se comprueba de que la cadena de conexión y el comando sean recibidos.
            if (string.IsNullOrEmpty(cadenaConexion)) throw new Exception("Favor de mandar la cadena de conexión.");
            if (string.IsNullOrEmpty(cadenaComando)) throw new Exception("Favor de mandar la cadena del comando.");
            object? objetoResultado;
            try
            {
                //Se instancia la clase SqlConnection, y se le manda la cadena de conexión como parámetro.
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    //Se comprueba el estado de la conexión con la base de datos, si está cerrada, la abrimos.
                    if (conexion.State == ConnectionState.Closed) await conexion.OpenAsync();
                    //Se instancia la clase SqlCommand, la cual ayudará a ejecutar el comando recibido.
                    using (SqlCommand comando = new SqlCommand())
                    {
                        //Se configura el comando.
                        comando.CommandType = CommandType.Text;
                        comando.Connection = conexion;
                        comando.CommandText = cadenaComando;
                        comando.Notification = null;

                        //Se asigna la cantidad de filas que fueron afectadas
                        objetoResultado = await comando.ExecuteScalarAsync();
                    }
                    //Se cierra la conexión.
                    if (conexion.State == ConnectionState.Open) await conexion.CloseAsync();
                }
                //Devuelve filas afectadas.
                return objetoResultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Este método sirve para traer las columnas de una tabla según el comando recibido.
        /// </summary>
        /// <param name="cadenaConexion"></param>
        /// <param name="cadenaComando"></param>
        /// <returns>Valor ReadOnlyCollection, resultado de las columnas de la tabla.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<ReadOnlyCollection<DbColumn>> obtenerEstructuras(string cadenaConexion, string cadenaComando)
        {
            //Se comprueba de que la cadena de conexión y el comando sean recibidos.
            if (string.IsNullOrEmpty(cadenaConexion)) throw new Exception("Favor de mandar la cadena de conexión.");
            if (string.IsNullOrEmpty(cadenaComando)) throw new Exception("Favor de mandar la cadena del comando.");
            try
            {
                //Se crea objeto DataTable a devolver.
                ReadOnlyCollection<DbColumn> columnas;
                //Se instancia la clase SqlConnection, y se le manda la cadena de conexión como parámetro.
                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    //Se comprueba el estado de la conexión con la base de datos, si está cerrada, la abrimos.
                    if (conexion.State == ConnectionState.Closed) await conexion.OpenAsync();
                    //Se instancia la clase SqlCommand, la cual ayudará a ejecutar el comando recibido.
                    using (SqlCommand comando = new SqlCommand())
                    {
                        //Se configura el comando.
                        comando.CommandType = CommandType.Text;
                        comando.Connection = conexion;
                        comando.CommandText = cadenaComando;
                        comando.Notification = null;
                        //Se instancia la clase SQLDataReader, el cual leerá y traerá la información de la tabla.
                        using (SqlDataReader lector = await comando.ExecuteReaderAsync())
                        {
                            //Lee las columnas que se encuentran en la tabla.
                            columnas = lector.GetColumnSchema();
                        }
                    }
                    //Se cierra la conexión.
                    if (conexion.State == ConnectionState.Open) await conexion.CloseAsync();
                    //Devuelve la tabla.
                    return columnas;
                }
            }
            catch (Exception ex)
            {
                //Compara si el error fue por un nombre de columna invalido.
                if (ex.Message.Contains("Invalid column name"))
                {
                    //Obtiene el nombre de la columna recibido.
                    string[] cadenaSeparada = ex.Message.Split("'");
                    //Provoca una excepción con el nombre de la columna invalido.
                    throw new Exception($"El nombre de la columna \"{cadenaSeparada[1]}\" es invalido.");
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        #endregion
    }
}
