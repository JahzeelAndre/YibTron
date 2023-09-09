//Librerías internas.
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using YibTronBackend.Atributos.Campos;
using YibTronBackend.Entidades.Tablas;
using YibTronBackend.Utilerias.SqlServer;

namespace YibTronBackend.Negocio.Repositorios.Base
{
    public abstract class RepositorioBaseCrearBaseDatosBAL
    {
        #region Propiedades...
        public abstract string? CadenaConexion { get; set; }
        #endregion Propiedades...

        #region Métodos...
        /// <summary>
        /// Método que sirve para obtener el nombre del proyecto (es el nombre que le pondrá a la base de datos).
        /// </summary>
        /// <returns>Valor string, resultante del nombre asignado en la implementación del método.</returns>
        public abstract string obtenerNombreProyecto();

        /// <summary>
        /// Método que sirve para obtener la descripción del proyecto.
        /// </summary>
        /// <returns>Valor string, resultante de la descripción asignada en la implementación del método.</returns>
        public abstract string obtenerDescripcionProyecto();

        /// <summary>
        /// Método que sirve para obtener la estructura de las entidades.
        /// </summary>
        /// <returns>Valor List, resultante de las estructuras asignadas en la implementación del método. </returns>
        public abstract List<EstructuraTablaInfo>? obtenerEstructurasTablas();

        /// <summary>
        /// Método que sirve para eliminar la base de datos.
        /// </summary>
        /// <returns>Valor bool, resultante de la eliminación de la base de datos.</returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<bool> borrarBaseDatos()
        {
            //Verifica que la cadena de conexión no venga vacía.
            if (CadenaConexion == null) throw new Exception("Favor de enviar la cadena de conexión.");
            //Verifica si la base de datos está creada.
            object? resultadoVerificarBD = await SqlMetodos.ejecutarEscalar(CadenaConexion, $"select NAME from dbo.sysdatabases where NAME = '{obtenerNombreProyecto()}'");

            //Si no está creada, procede a crearla.
            if (!string.IsNullOrEmpty(resultadoVerificarBD?.ToString()))
            {
                //Ejecuta comando para borrar base de datos y recibe el resultado.
                string cadenaComando = @"
                ALTER DATABASE " + obtenerNombreProyecto() + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [" + obtenerNombreProyecto() + "]";
                await SqlMetodos.ejecutarComando(CadenaConexion, cadenaComando);
            }
            return true;
        }

        /// <summary>
        /// Método que sirve para generar la base de datos junto con sus tablas.
        /// </summary>
        /// <returns>Valor int, resultante del comando sql ejecutado en la base de datos.</returns>
        /// <exception cref="Exception"></exception>
        public virtual async Task<int> generarBaseDeDatos()
        {
            //Obtiene las estructuras de las tablas.
            List<EstructuraTablaInfo>? estructurasTablas = obtenerEstructurasTablas();
            //Comprueba que hayan puesto  las estructuras.
            if (estructurasTablas == null || estructurasTablas.Count == 0) throw new Exception("Favor de mandar las estructuras de las tablas.");
            //Verifica que la cadena de conexión no venga vacía.
            if (CadenaConexion == null) throw new Exception("Favor de enviar la cadena de conexión.");
            //Verifica si la base de datos está creada.
            object? resultadoVerificarBD = await SqlMetodos.ejecutarEscalar(CadenaConexion, $"select NAME from dbo.sysdatabases where NAME = '{obtenerNombreProyecto()}'");

            //Si no está creada, procede a crearla.
            if (string.IsNullOrEmpty(resultadoVerificarBD?.ToString()))
            {
                await SqlMetodos.ejecutarComando(CadenaConexion, $"create database {obtenerNombreProyecto()};");
            }

            //Crea la cadena que contendrá el comando a ejecutar.
            StringBuilder cadenaComando = new StringBuilder();
            //Crea la cadena que contendrá los procedimientos a ejecutar para añadir las descripciones a las columnas.
            StringBuilder comandoAñadirDescripcion = new StringBuilder();

            //Comando sql para usar la base de datos creada.
            cadenaComando.AppendLine($"use {obtenerNombreProyecto()};");

            //Añade a la conexión la base de datos creada.
            CadenaConexion = $"{CadenaConexion} Initial Catalog={obtenerNombreProyecto()};";

            //Recorre las estructuras de las entidades o tablas.
            foreach (EstructuraTablaInfo estructuraTablaItem in estructurasTablas)
            {
                //Verifica si la tabla en la bd está creada.
                object? resultadoVerificarTabla = await SqlMetodos.ejecutarEscalar(CadenaConexion, $"select TABLE_NAME from information_schema.tables where TABLE_SCHEMA = 'dbo' AND TABLE_NAME = '{estructuraTablaItem.Nombre}'");
                //Si no está creada, procede a crearla y si no trae las columnas que se encuentran en la tabla.
                if (string.IsNullOrEmpty(resultadoVerificarTabla?.ToString()))
                {
                    //Comando sql para crear tabla.
                    cadenaComando.AppendLine($"create table {estructuraTablaItem.Nombre} (");
                    //Se añade la columna Id.
                    cadenaComando.AppendLine($"Id numeric(11,0) primary key identity(1,1) not null,");
                    //Se recorren las columnas de la tabla o entidad.
                    foreach (ColumnasTablaInfo columnaTablaItem in estructuraTablaItem.Columnas)
                    {
                        //Switch para comprobar el tipo de columna, y dependiendo de que tipo sea, se inserta un comando sql que añade la columna con la que se está interactuando a nuestra cadena de comando.
                        switch (columnaTablaItem.Tipo)
                        {
                            case ColumnaAtributte.ETipoCampo.ClaveForanea:
                                if (columnaTablaItem.ReferenciaTabla == null) throw new Exception("Para ocupar la creación de la llave foránea, necesitas poner un objeto LlaveForaneaInfo como atributo.");
                                string opcionNuloLlaveForanea = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoLlaveForanea = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{estructuraTablaItem.Nombre} numeric(11,0) {opcionNuloLlaveForanea} {opcionValorUnicoLlaveForanea},");
                                cadenaComando.AppendLine($"CONSTRAINT fk_{estructuraTablaItem.Nombre} FOREIGN KEY ({columnaTablaItem.Nombre}) REFERENCES {columnaTablaItem.ReferenciaTabla}  (Id),");
                                break;

                            case ColumnaAtributte.ETipoCampo.Texto:
                                if (columnaTablaItem.Tamaño == 0) throw new Exception($"El tamaño de la columna \"{columnaTablaItem.Nombre}\" debe de ser mayor a 0.");
                                if (columnaTablaItem.Tamaño > 1000) throw new Exception($"Los campos de tipo \"Texto\" deben ser menor a {1000} (\"{columnaTablaItem.Nombre}\").");
                                string opcionNuloTexto = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoTexto = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} nvarchar({columnaTablaItem.Tamaño}) {opcionNuloTexto} {opcionValorUnicoTexto},");
                                break;

                            case ColumnaAtributte.ETipoCampo.NumeroFloat:
                                string opcionNuloNumeroFloat = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoNumeroFloat = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} float {opcionNuloNumeroFloat} {opcionValorUnicoNumeroFloat},");
                                break;

                            case ColumnaAtributte.ETipoCampo.NumeroNumerico:
                                if (columnaTablaItem.Tamaño == 0) throw new Exception($"El tamaño de la columna \"{columnaTablaItem.Nombre}\" debe de ser mayor a 0.");
                                if (columnaTablaItem.Tamaño > 35) throw new Exception($"Los campos de tipo \"NumeroNumerico\" deben ser menor a {35} (\"{columnaTablaItem.Nombre}\").");
                                if (columnaTablaItem.Decimales > 3) throw new Exception($"Los campos de tipo \"NumeroNumerico\" deben tener menos de {3} decimales (\"{columnaTablaItem.Nombre}\").");
                                string opcionNuloNumeroNumerico = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoNumeroNumerico = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} numeric({columnaTablaItem.Tamaño},{columnaTablaItem.Decimales}) {opcionNuloNumeroNumerico} {opcionValorUnicoNumeroNumerico},");
                                break;

                            case ColumnaAtributte.ETipoCampo.FechaDate:
                                string opcionNuloDate = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoDate = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} date {opcionNuloDate} {opcionValorUnicoDate},");
                                break;
                            case ColumnaAtributte.ETipoCampo.FechaDateTime:
                                string opcionNuloDateTime = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoDateTime = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} datetime {opcionNuloDateTime} {opcionValorUnicoDateTime},");
                                break;
                            case ColumnaAtributte.ETipoCampo.SiNo:
                                string opcionNuloBit = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoBit = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} bit {opcionNuloBit} {opcionValorUnicoBit},");
                                break;
                            case ColumnaAtributte.ETipoCampo.XML:
                                string opcionNuloXML = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                string opcionValorUnicoXML = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                cadenaComando.AppendLine($"{columnaTablaItem.Nombre} XML {opcionNuloXML} {opcionValorUnicoXML},");
                                break;
                            default:
                                throw new Exception("Tipo de columna inválido.");
                        }
                        //Añade a la cadena para añadir los comentarios, el comando sql que ejecuta un procedimiento para añadir una descripción a la columna con la que se está interactuando en el foreach.
                        comandoAñadirDescripcion.AppendLine("EXEC sp_addextendedproperty ");
                        comandoAñadirDescripcion.AppendLine("@name = N'MS_Description',");
                        comandoAñadirDescripcion.AppendLine($"@value = '{columnaTablaItem.Descripcion}',");
                        comandoAñadirDescripcion.AppendLine("@level0type = N'Schema',");
                        comandoAñadirDescripcion.AppendLine("@level0name = 'dbo',");
                        comandoAñadirDescripcion.AppendLine("@level1type = N'Table',");
                        comandoAñadirDescripcion.AppendLine($"@level1name = '{estructuraTablaItem.Nombre}',");
                        comandoAñadirDescripcion.AppendLine("@level2type = N'Column',");
                        comandoAñadirDescripcion.AppendLine($"@level2name = '{columnaTablaItem.Nombre}';");
                    }
                    cadenaComando.AppendLine(");");
                }
                else
                {
                    //Obtiene las columnas que se encuentran creadas en la tabla de la BD.
                    ReadOnlyCollection<DbColumn> columnas = await SqlMetodos.obtenerEstructuras(CadenaConexion, $"select * from {estructuraTablaItem.Nombre}");
                    //Crea una lista que contendrá los nombres de las columnas en la tabla de la BD.
                    List<string> listaNombres = new List<string>();
                    //Rellena la lista de nombres con los nombres de las columnas de la tabla.
                    foreach (DbColumn columnaItem in columnas)
                    {
                        listaNombres.Add(columnaItem.ColumnName);
                    }
                    //Interactura con las columnas que vienen de la estructura de la entidad.
                    foreach (ColumnasTablaInfo columnaTablaItem in estructuraTablaItem.Columnas)
                    {
                        //Busca la columna que tiene la entidad en las que ya están creadas, para verificar si existen o no y así ver si es una nueva columna para agregarla.
                        bool seEncontro = listaNombres.Contains(columnaTablaItem.Nombre);
                        //Si la encuentra procede a modificar la tabla con la que se encuentra interactuando el foreach.
                        if (seEncontro == false)
                        {
                            switch (columnaTablaItem.Tipo)
                            {
                                case ColumnaAtributte.ETipoCampo.ClaveForanea:
                                    if (columnaTablaItem.ReferenciaTabla == null) throw new Exception("Para ocupar la creación de la llave foránea, necesitas poner un objeto LlaveForaneaInfo como atributo.");
                                    string opcionNuloLlaveForanea = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoLlaveForanea = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} numeric(11,0) {opcionNuloLlaveForanea} {opcionValorUnicoLlaveForanea};");
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add CONSTRAINT fk_{columnaTablaItem.Nombre} FOREIGN KEY ({columnaTablaItem.Nombre}) REFERENCES {columnaTablaItem.ReferenciaTabla}  (Id);");
                                    break;

                                case ColumnaAtributte.ETipoCampo.Texto:
                                    if (columnaTablaItem.Tamaño == 0) throw new Exception($"El tamaño de la columna \"{columnaTablaItem.Nombre}\" debe de ser mayor a 0.");
                                    if (columnaTablaItem.Tamaño > 1000) throw new Exception($"Los campos de tipo \"Texto\" deben ser menor a {1000} (\"{columnaTablaItem.Nombre}\").");
                                    string opcionNuloTexto = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoTexto = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} nvarchar({columnaTablaItem.Tamaño}) {opcionNuloTexto} {opcionValorUnicoTexto} CONSTRAINT NVarchar_Minimo DEFAULT '' WITH VALUES;");
                                    break;

                                case ColumnaAtributte.ETipoCampo.NumeroFloat:
                                    string opcionNuloNumeroFloat = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoNumeroFloat = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} float {opcionNuloNumeroFloat} {opcionValorUnicoNumeroFloat} CONSTRAINT Float_Minimo DEFAULT 1 WITH VALUES;");
                                    break;

                                case ColumnaAtributte.ETipoCampo.NumeroNumerico:
                                    if (columnaTablaItem.Tamaño == 0) throw new Exception($"El tamaño de la columna \"{columnaTablaItem.Nombre}\" debe de ser mayor a 0.");
                                    if (columnaTablaItem.Tamaño > 35) throw new Exception($"Los campos de tipo \"NumeroNumerico\" deben ser menor a {35} (\"{columnaTablaItem.Nombre}\").");
                                    if (columnaTablaItem.Decimales > 3) throw new Exception($"Los campos de tipo \"NumeroNumerico\" deben tener menos de {3} decimales (\"{columnaTablaItem.Nombre}\").");
                                    string opcionNuloNumeroNumerico = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoNumeroNumerico = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} numeric({columnaTablaItem.Tamaño},{columnaTablaItem.Decimales}) {opcionNuloNumeroNumerico} {opcionValorUnicoNumeroNumerico} CONSTRAINT Numeric_Minimo DEFAULT 1 WITH VALUES;");
                                    break;

                                case ColumnaAtributte.ETipoCampo.FechaDate:
                                    string opcionNuloDate = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoDate = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} date {opcionNuloDate} {opcionValorUnicoDate} CONSTRAINT Date_Minimo DEFAULT {DateTime.Now} WITH VALUES;");
                                    break;
                                case ColumnaAtributte.ETipoCampo.FechaDateTime:
                                    string opcionNuloDateTime = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoDateTime = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} datetime {opcionNuloDateTime} {opcionValorUnicoDateTime} CONSTRAINT DateTime_Minimo DEFAULT {DateTime.Now} WITH VALUES;");
                                    break;
                                case ColumnaAtributte.ETipoCampo.SiNo:
                                    string opcionNuloBit = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoBit = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} bit {opcionNuloBit} {opcionValorUnicoBit} CONSTRAINT Bit_Minimo DEFAULT 1 WITH VALUES;");
                                    break;
                                case ColumnaAtributte.ETipoCampo.XML:
                                    string opcionNuloXML = columnaTablaItem.PermiteNulos == true ? "" : "not null";
                                    string opcionValorUnicoXML = columnaTablaItem.ValorUnico == true ? "unique" : "";
                                    cadenaComando.AppendLine($"alter table {estructuraTablaItem.Nombre} add {columnaTablaItem.Nombre} XML {opcionNuloXML} {opcionValorUnicoXML};");
                                    break;
                                default:
                                    throw new Exception("Tipo de columna inválido.");
                            }
                            //Añade a la cadena para añadir los comentarios, el comando sql que ejecuta un procedimiento para añadir una descripción a la columna con la que se está interactuando en el foreach.
                            comandoAñadirDescripcion.AppendLine("EXEC sp_addextendedproperty ");
                            comandoAñadirDescripcion.AppendLine("@name = N'MS_Description',");
                            comandoAñadirDescripcion.AppendLine($"@value = '{columnaTablaItem.Descripcion}',");
                            comandoAñadirDescripcion.AppendLine("@level0type = N'Schema',");
                            comandoAñadirDescripcion.AppendLine("@level0name = 'dbo',");
                            comandoAñadirDescripcion.AppendLine("@level1type = N'Table',");
                            comandoAñadirDescripcion.AppendLine($"@level1name = '{estructuraTablaItem.Nombre}',");
                            comandoAñadirDescripcion.AppendLine("@level2type = N'Column',");
                            comandoAñadirDescripcion.AppendLine($"@level2name = '{columnaTablaItem.Nombre}';");
                        }
                    }
                }
            }
            //Verifica que la cadena con los procedimientos para agregar la descripción a las columnas no venga vacio y lo añada a la cadena del comando a ejecutar.
            if (!string.IsNullOrEmpty(comandoAñadirDescripcion.ToString())) cadenaComando.AppendLine(comandoAñadirDescripcion.ToString());
            //Regresa resultado del comando ejecutado en la BD.
            return await SqlMetodos.ejecutarComando(CadenaConexion, cadenaComando.ToString());
        }
        #endregion Métodos...
    }
}
