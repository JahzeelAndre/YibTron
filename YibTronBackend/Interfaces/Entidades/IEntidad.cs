namespace YibTronBackend.Interfaces.Entidades
{
    public interface IEntidad
    {
        #region Propiedades...
        /// <summary>
        /// Propiedad para obtener o asignar el Id de la entidad.
        /// </summary>
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public DateTime FechaBaja { get; set; }
        #endregion
    }
}
