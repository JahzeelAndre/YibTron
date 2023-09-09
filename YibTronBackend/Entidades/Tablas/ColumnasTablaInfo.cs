//Librerías internas.
using static YibTronBackend.Atributos.Campos.ColumnaAtributte;

namespace YibTronBackend.Entidades.Tablas
{
    public class ColumnasTablaInfo
    {
        #region Propiedades...
        public required string Nombre { get; set; }
        public required string Descripcion { get; set; }
        public required ETipoCampo Tipo { get; set; }
        public required decimal Tamaño { get; set; }
        public required bool PermiteNulos { get; set; }
        public required bool ValorUnico { get; set; }
        public int Decimales { get; set; }
        public string? ReferenciaTabla { get; set; }
        #endregion
    }
}
