//Librerías internas.
using static YibTronBackend.Atributos.Campos.ColumnaAtributte;

namespace YibTronBackend.Entidades.CamposValores
{
    public class InformacionColumnasInfo
    {
        #region Propiedades...
        /// <summary>
        /// Nombre de la columna en la base de datos.
        /// </summary>
        public required string NombreColumna { get; set; }

        /// <summary>
        /// Tipo de la columna en la base de datos.
        /// </summary>
        public required ETipoCampo TipoCampo { get; set; }

        /// <summary>
        /// Valor que se le asignará a la columna en la base de datos.
        /// </summary>
        public object? ValorColumna { get; set; }
        #endregion
    }
}
