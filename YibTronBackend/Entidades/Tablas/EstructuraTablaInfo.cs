namespace YibTronBackend.Entidades.Tablas
{
    public class EstructuraTablaInfo
    {
        #region Propiedades...
        public required string Nombre { get; set; }
        public required List<ColumnasTablaInfo> Columnas { get; set; }
        #endregion
    }
}
