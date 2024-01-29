using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YibTronBackend.Atributos.Campos;

namespace YibTronBackend.Clases_Abstractas
{
    public abstract class NombresColumnas
    {
        public const string Id = "Id";

        [ColumnaAtributte(IdUsuario, "Columna con el id del usuario que subió el registro.", ColumnaAtributte.ETipoCampo.ClaveForanea, ReferenciaTabla = "Usuarios")]
        public const string IdUsuario = "IdUsuario";

        [ColumnaAtributte(FechaAlta, "Fecha en la que se subió el registro.", ColumnaAtributte.ETipoCampo.FechaDateTime)]
        public const string FechaAlta = "FechaAlta";
        public const string FechaUltimaModificacion = "FechaUltimaModificacion";
        public const string FechaBaja = "FechaBaja";
    }
}
