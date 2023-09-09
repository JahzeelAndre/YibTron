//Librerías internas
using System.Diagnostics.CodeAnalysis;

namespace YibTronBackend.Entidades.CondicionesSql
{
    public class CondicionSQLInfo
    {
        #region Enums...
        /// <summary>
        /// Condiciones SQL para consultas.
        /// </summary>
        public enum CondicionesExtra
        {
            AND,
            OR,
            NOT,
        }
        #endregion

        #region Propiedades...
        /// <summary>
        /// Nombre de la columna en la base de datos.
        /// </summary>
        public required string NombreColumna { get; set; }

        /// <summary>
        /// Condicional para comparar en la consulta.
        /// </summary>
        public required string Condicional { get; set; }

        /// <summary>
        /// Valor que se compara después del condicional.
        /// </summary>
        public required string ValorComparar { get; set; }

        /// <summary>
        /// Condición extra, en caso de querer poner más condiciones a la consulta.
        /// </summary>
        public CondicionesExtra? CondicionExtra { get; set; }
        #endregion

        #region Constructor...
        [SetsRequiredMembers]
        public CondicionSQLInfo(string nombreColumna, string condicional, string valorComparar, CondicionesExtra? condicionExtra = null)
        {
            NombreColumna = nombreColumna;
            Condicional = condicional;
            ValorComparar = valorComparar;
            CondicionExtra = condicionExtra;
        }
        #endregion
    }
}
