//Librerías internas
using System.Data;
using YibTronBackend.Datos.Repositorios.Base;
using YibTronBackend.Entidades.CondicionesSql;
using YibTronBackend.Entidades.Tablas;
using YibTronBackend.Interfaces.Entidades;
using YibTronBackend.Negocio.Repositorios.Base;

namespace YibTronBackend.Negocio.Repositorios.Implementacion
{
    public class RepositorioImplTablaBAL<Entidad, Campos, RepositorioImplTablaDAL> : IRepositorioBaseTablaBAL<Entidad> where Entidad : IEntidad where RepositorioImplTablaDAL : RepositorioBaseTablaDAL<Entidad>
    {
        #region Propiedades...
        //Acceso a la clase de datos.
        public RepositorioImplTablaDAL AccesoDatosDAL { get; }

        //Cadena de conexión a la base de datos.
        public override string? CadenaConexion { get => AccesoDatosDAL.CadenaConexion; set => AccesoDatosDAL.CadenaConexion = value; }

        //Nombre de la tabla en la base de datos.
        public override string? NombreTabla { get; set; }

        //Versión del programa.
        public override string? Version { get; set; }
        #endregion

        #region Constructor...
        public RepositorioImplTablaBAL()
        {
            //Inicializa e instancia la clase de datos.
            AccesoDatosDAL = (RepositorioImplTablaDAL)Activator.CreateInstance(typeof(RepositorioImplTablaDAL))!;
        }
        #endregion

        #region Métodos...

        //Método para eliminar una entidad en la base de datos.
        public override async Task<int> eliminar(int Id)
        {
            return await AccesoDatosDAL.eliminar(Id);
        }

        //Método para insertar o modificar una entidad en la base de datos.
        public override async Task<int> guardar(Entidad entidad)
        {
            if (entidad.Id == 0)
            {
                return await AccesoDatosDAL.insertar(entidad);
            }
            else
            {
                return await AccesoDatosDAL.modificar(entidad);
            }
        }

        //Método para obtener la tabla de datos de la base de datos
        public override async Task<DataTable> obtenerTablaDatos(List<CondicionSQLInfo>? listaCondiciones = null)
        {
            return await AccesoDatosDAL.obtenerTablaDatos(listaCondiciones);
        }

        //Método para obtener una entidad de la base de datos.
        public override async Task<Entidad> obtenerEntidad(int id)
        {
            return await AccesoDatosDAL.obtenerEntidad(id);
        }

        //Método para obtener una lista de entidad de la base de datos
        public override async Task<List<Entidad>?> obtenerListaEntidades(List<CondicionSQLInfo>? listaCondiciones = null)
        {
            return await AccesoDatosDAL.obtenerListaEntidades(listaCondiciones);
        }
        #endregion

        #region Métodos estructuras...
        //Método para traer la estructura de una entidad.
        public override EstructuraTablaInfo obtenerEstructuraTabla()
        {
            return AccesoDatosDAL.obtenerEstructuraTabla();
        }
        #endregion Métodos estructuras

        #region Métodos auxiliares...
        public override int obtenerTamañoColumna(string nombreColumna)
        {
            return AccesoDatosDAL.obtenerTamañoColumna(nombreColumna);
        }
        #endregion
    }
}
