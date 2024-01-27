//Librerías internas.
using System.Data;
using YibTronBackend.Entidades.CondicionesSql;
using YibTronBackend.Entidades.Tablas;
using YibTronBackend.Interfaces.Entidades;

namespace YibTronBackend.Datos.Repositorios.Base
{
    public abstract class RepositorioBaseTablaDAL<Entidad> where Entidad : IEntidad
    {
        #region Propiedades...
        /// <summary>
        /// Propiedad para establecer u obtener la cadena de conexión a la base de datos.
        /// </summary>
        public abstract string? CadenaConexion { get; set; }

        /// <summary>
        /// Propiedad para establecer u obtener el nombre de la tabla en la base de datos.
        /// </summary>
        public abstract string? NombreTabla { get; set; }
        #endregion

        #region Métodos CRUD...

        /// <summary>
        /// Método para obtener la tabla de datos de la base de datos.
        /// </summary>
        /// <returns>Valor DataTable, resultante de la consulta a la base de datos.</returns>
        /// <exception cref="Exception"></exception>
        public abstract Task<DataTable> obtenerTablaDatos(List<CondicionSQLInfo>? listaCondiciones = null);

        /// <summary>
        /// Método para obtener objeto de la base de datos.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Valor objeto, resultante de la consulta a la base de datos.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract Task<Entidad> obtenerEntidad(int id);

        /// <summary>
        /// Método para obtener una lista de objetos de la base de datos.
        /// </summary>
        /// <param name="listaCondiciones"></param>
        /// <returns>Valor List, resultante de la consulta a la base de datos.</returns>
        public abstract Task<List<Entidad>?> obtenerListaEntidades(List<CondicionSQLInfo>? listaCondiciones = null);

        /// <summary>
        /// Método para insertar objeto en la base de datos.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Valor int, resultante del id del objeto insertado en la base de datos.</returns>
        /// <exception cref="Exception"></exception>
        public abstract Task<int> insertar(Entidad entidad);

        /// <summary>
        /// Método para modificar objeto en la base de datos.
        /// </summary>
        /// <param name="Entidad"></param>
        /// <returns>Valor int, resultante del id del objeto modificado en la base de datos.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract Task<int> modificar(Entidad entidad);

        /// <summary>
        /// Método para eliminar un objeto en la base de datos.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Valor int, resultante de las filas afectadas en la base de datos.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public abstract Task<int> eliminar(int Id);
        #endregion

        #region Métodos estructura...
        /// <summary>
        /// Método para obtener la estructura de una entidad.
        /// </summary>
        /// <returns>Valor EstructuraInfo, resultado de los atributos de la entidad.</returns>
        public abstract EstructuraTablaInfo obtenerEstructuraTabla();
        #endregion

        #region Métodos auxiliares...
        public abstract int obtenerTamañoColumna(string nombreColumna);
        #endregion
    }
}
