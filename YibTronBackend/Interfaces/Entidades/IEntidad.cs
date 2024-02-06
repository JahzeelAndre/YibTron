namespace YibTronBackend.Interfaces.Entidades
{
    public interface IEntidad
    {
        #region Propiedades...
        /// <summary>
        /// Propiedad para obtener o asignar el Id de la entidad.
        /// </summary>
        public int Id { get; set; }
        public int IdUsuarioAlta { get; set; }
        public int IdUsuarioActualizado { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaActualizado { get; set; }
        #endregion
    }
}
