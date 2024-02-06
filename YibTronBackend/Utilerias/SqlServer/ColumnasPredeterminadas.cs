//Librerías internas.
using YibTronBackend.Atributos.Campos;
using YibTronBackend.Entidades.CamposValores;

namespace YibTronBackend.Utilerias.SqlServer
{
    public class ColumnasPredeterminadas
    {
        #region Propiedades...
        public static List<InformacionColumnasInfo> listaColumnasPredeterminadas => new List<InformacionColumnasInfo>
        {
            new InformacionColumnasInfo
            {
                NombreColumna = ConstantesColumnasPredeterminadas.IdUsuarioAlta,
                TipoCampo = Atributos.Campos.ColumnaAtributte.ETipoCampo.ClaveForanea,
                ValorColumna = 1
            },
            new InformacionColumnasInfo
            {
                NombreColumna = ConstantesColumnasPredeterminadas.IdUsuarioActualizado,
                TipoCampo = Atributos.Campos.ColumnaAtributte.ETipoCampo.ClaveForanea,
                ValorColumna = 1,
            },
            new InformacionColumnasInfo
            {
                NombreColumna = ConstantesColumnasPredeterminadas.FechaAlta,
                TipoCampo = Atributos.Campos.ColumnaAtributte.ETipoCampo.FechaDateTime,
                ValorColumna = DateTime.Now
            },
            new InformacionColumnasInfo
            {
                NombreColumna = ConstantesColumnasPredeterminadas.FechaActualizado,
                TipoCampo = Atributos.Campos.ColumnaAtributte.ETipoCampo.FechaDateTime,
                ValorColumna = DateTime.Now,
            }
        };
        #endregion

        #region Constantes...
        public class ConstantesColumnasPredeterminadas
        {
            [ColumnaAtributte(IdUsuarioAlta, "Id del usuario que dio de alta.", ColumnaAtributte.ETipoCampo.ClaveForanea, ReferenciaTabla = "Usuarios")]
            public const string IdUsuarioAlta = "IdUsuarioAlta";
            [ColumnaAtributte(IdUsuarioActualizado, "Id del usuario que realizó la última modificación.", ColumnaAtributte.ETipoCampo.ClaveForanea, ReferenciaTabla = "Usuarios")]
            public const string IdUsuarioActualizado = "IdUsuarioActualizado";
            [ColumnaAtributte(FechaAlta, "Fecha de cuando se dio de alta", ColumnaAtributte.ETipoCampo.FechaDateTime, 0, true)]
            public const string FechaAlta = "FechaAlta";
            [ColumnaAtributte(FechaActualizado, "Fecha de cuando se realizó la última modificación.", ColumnaAtributte.ETipoCampo.FechaDateTime, 0, true)]
            public const string FechaActualizado = "FechaActualizado";
        }
        #endregion
    }
}
