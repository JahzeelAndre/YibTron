//Librerías internas.
using System.Data;
using YibTronBackend.Entidades.CondicionesSql;
using YibTronBackend.Entidades.Tablas;
using YibTronBackend.Interfaces.Entidades;

namespace YibTronBackend.Negocio.Repositorios.Base
{
    public abstract class IRepositorioBaseTablaBAL<Entidad> where Entidad : IEntidad
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
        /// <summary>
        /// Propiedad para establecer u obtener la versión del programa.
        /// </summary>
        public abstract string? Version { get; set; }
        #endregion

        #region Métodos CRUD...
        /// <summary>
        /// Método para obtener la tabla de datos de la base de datos.
        /// </summary>
        /// <param name="listaCondiciones"></param>
        /// <returns>Valor DataTable, resultante de la consulta a la base de datos.</returns>
        public abstract Task<DataTable> obtenerTablaDatos(List<CondicionSQLInfo>? listaCondiciones = null);

        /// <summary>
        /// Método para obtener un objeto de tipo entidad de la base de datos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Valor entidad, resultante de la consulta a la base de datos.</returns>
        public abstract Task<Entidad> obtenerEntidad(int id);

        /// <summary>
        /// Método para obtener una lista de entidades de la base de datos.
        /// </summary>
        /// <param name="listaCondiciones"></param>
        /// <returns>Valor List, resultante de la consulta a la base de datos.</returns>
        public abstract Task<List<Entidad>?> obtenerListaEntidades(List<CondicionSQLInfo>? listaCondiciones = null);

        /// <summary>
        /// Método para insertar o modificar una entidad de la base de datos.
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Valor int, resultante de las filas afectadas en la base de datos.</returns>
        public abstract Task<int?> guardar(Entidad entidad);

        /// <summary>
        /// Método para eliminar una entidad de la base de datos.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Valor int, resultante de las filas afectadas en la base de datos.</returns>
        public abstract Task<int?> eliminar(int Id);
        #endregion

        #region Métodos auxiliares...
        /// <summary>
        /// Método para obtener el tamaño de una columna de la base de datos.
        /// </summary>
        /// <param name="nombreColumna"></param>
        /// <returns>Valor int, resultante del tamaño de la columna.</returns>
        public abstract int obtenerTamañoColumna(string nombreColumna);
        #endregion

        #region Métodos estructuras...
        /// <summary>
        /// Método que sirve para obtener la estructura de una entidad.
        /// </summary>
        /// <returns>Valor EstructuraInfo, resultado de los atributos de la entidad.</returns>
        public abstract EstructuraTablaInfo obtenerEstructuraTabla();
        #endregion Métodos estructuras...
    }
}
