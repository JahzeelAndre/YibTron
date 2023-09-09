//Librerías internas.
using System.Diagnostics.CodeAnalysis;

namespace YibTronBackend.Atributos.Campos
{
    public class ColumnaAtributte : Attribute
    {
        #region Enums...
        /// <summary>
        /// Enumerable con los tipos de columnas para la base de datos.
        /// </summary>
        public enum ETipoCampo
        {
            ClaveForanea,
            Texto,
            NumeroFloat,
            NumeroNumerico,
            SiNo,
            FechaDate,
            FechaDateTime,
            XML
        }
        #endregion

        #region Propiedades...
        /// <summary>
        /// Nombre de la columna en la base de datos.
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Descripción de la columna en la base de datos.
        /// </summary>
        public required string Descripcion { get; set; }

        /// <summary>
        /// Tipo de la columna en la base de datos.
        /// </summary>
        public required ETipoCampo Tipo { get; set; }

        /// <summary>
        /// Tamaño de la columna en la base de datos.
        /// </summary>
        public required int Tamaño { get; set; }

        /// <summary>
        /// Verificar si la columna en la base de datos permite valores nulos.
        /// </summary>
        public required bool PermiteNulos { get; set; }

        /// <summary>
        /// Verificar si la columna en la base de datos puede aceptar valores repetidos.
        /// </summary>
        public bool ValorUnico { get; set; }

        /// <summary>
        /// Decimales que tendrá la columna en caso de ser tipo numerico.
        /// </summary>
        public int Decimales { get; set; }

        /// <summary>
        /// Datos de la columna en caso de ser tipo llave foranea.
        /// </summary>
        public string? ReferenciaTabla { get; set; }
        #endregion

        #region Contructor...
        [SetsRequiredMembers]
        public ColumnaAtributte(string nombre, string descripcion, ETipoCampo tipo, int tamaño = 50, bool permiteNulos = true, int decimales = 0, bool valorUnico = false)
        {
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Tipo = tipo;
            this.Tamaño = tamaño;
            this.PermiteNulos = permiteNulos;
            this.ValorUnico = valorUnico;
            this.Decimales = decimales;
        }
        #endregion
    }
}
