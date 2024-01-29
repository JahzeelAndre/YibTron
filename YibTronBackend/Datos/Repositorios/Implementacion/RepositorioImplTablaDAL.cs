//Librerías internas.
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using YibTronBackend.Atributos.Campos;
using YibTronBackend.Datos.Repositorios.Base;
using YibTronBackend.Entidades.CamposValores;
using YibTronBackend.Entidades.CondicionesSql;
using YibTronBackend.Entidades.Tablas;
using YibTronBackend.Interfaces.Entidades;
using YibTronBackend.Utilerias.SqlServer;

namespace YibTronBackend.Datos.Repositorios.Implementacion
{
    public class RepositorioImplTablaDAL<Entidad, Campos> : RepositorioBaseTablaDAL<Entidad> where Entidad : IEntidad
    {
        #region Propiedades...
        //Propiedad de la cadena de conexión a la base de datos.
        public override string? CadenaConexion { get; set; }

        //Nombre de la tabla en la base de datos.
        public override string? NombreTabla { get; set; }
        #endregion

        #region Métodos CRUD...

        //Método para eliminar un objeto en la base de datos.
        public override async Task<int> eliminar(int Id)
        {
            string cadenaComando = $"delete from {NombreTabla} where Id = {Id}";
            ComprobarCadenas(CadenaConexion, cadenaComando);
            return await SqlMetodos.ejecutarComando(CadenaConexion!, cadenaComando);
        }

        //Método para insertar objeto en la base de datos.
        public override async Task<int> insertar(Entidad entidad)
        {
            return await guardar(entidad);
        }

        //Método para modificar objeto en la base de datos.
        public override async Task<int> modificar(Entidad entidad)
        {
            return await guardar(entidad);
        }

        public async Task<int> guardar(Entidad entidad)
        {
            //Verificamos que el nombre de la tabla no este vacio.
            if (string.IsNullOrEmpty(NombreTabla)) throw new Exception("Favor de mandar el nombre de la tabla.");
            //Obtener la lista con el nombre de las columnas, valores y tipo.
            List<InformacionColumnasInfo> listaInformacionColumnas = obtenerListaInformacionColumnas(entidad);
            //Creamos StringBuilder donde se guardará el comando a mandar.
            StringBuilder cadenaComando = new StringBuilder();
            //Creamos StringBuilder para guardar el nombre de las columnas.
            List<string> nombresColumnas = new List<string>();
            //Creamos StringBuilder para guardar los valores de las columnas.
            List<object?> valoresColumnas = new List<object?>();
            //Recorre la lista con los datos de las columnas.
            listaInformacionColumnas.ForEach(informacionColumnaItem =>
            {
                //Agregamos a la lista el nombre de la columna en la posición actual
                nombresColumnas.Add(informacionColumnaItem.NombreColumna);
                //Comparamos el tipo del valor y lo guardamos en la lista de valores.
                switch (informacionColumnaItem.TipoCampo)
                {
                    case ColumnaAtributte.ETipoCampo.ClaveForanea:
                        valoresColumnas.Add(informacionColumnaItem.ValorColumna);
                        break;
                    case ColumnaAtributte.ETipoCampo.Texto:
                        valoresColumnas.Add($"'{informacionColumnaItem.ValorColumna}'");
                        break;
                    case ColumnaAtributte.ETipoCampo.NumeroFloat:
                        valoresColumnas.Add(informacionColumnaItem.ValorColumna);
                        break;
                    case ColumnaAtributte.ETipoCampo.NumeroNumerico:
                        valoresColumnas.Add(informacionColumnaItem.ValorColumna);
                        break;
                    case ColumnaAtributte.ETipoCampo.SiNo:
                        valoresColumnas.Add(informacionColumnaItem.ValorColumna);
                        break;
                    case ColumnaAtributte.ETipoCampo.FechaDate:
                        valoresColumnas.Add(informacionColumnaItem.ValorColumna);
                        break;
                    case ColumnaAtributte.ETipoCampo.FechaDateTime:
                        DateTime dateTimeAux = Convert.ToDateTime(informacionColumnaItem.ValorColumna);
                        valoresColumnas.Add($"CONVERT (datetime, '{dateTimeAux.ToString("s")}')");
                        break;
                }
            });
            //Separamos los nombres de las columnas y los valores con una coma ",".
            string nombresColumnasSeparados = string.Join(", ", nombresColumnas);
            string valoresColumnasSeparados = string.Join(", ", valoresColumnas);
            if (entidad.Id == 0)
            {
                cadenaComando.AppendLine($"insert into [dbo].[{NombreTabla}] ({nombresColumnasSeparados}) values ({valoresColumnasSeparados}); ");
            }
            else
            {
                string[] arregloNombresConValores = new string[listaInformacionColumnas.Count];
                cadenaComando.AppendLine($"update [dbo].[{NombreTabla}] set ");
                for (int i = 0; i < arregloNombresConValores.Length; i++)
                {
                    arregloNombresConValores[i] = $"{nombresColumnas[i]} = {valoresColumnas[i]}";
                }
                cadenaComando.AppendLine($"{string.Join(", ", arregloNombresConValores)} where Id = {entidad.Id}; ");
            }

            cadenaComando.AppendLine("SELECT SCOPE_IDENTITY();");
            //Armamos el comando a ejecutar.
            string cadenaComandoString = cadenaComando.ToString();
            //Comprobamos cadenas para la ejecución del sql.
            ComprobarCadenas(CadenaConexion, cadenaComandoString);
            //Regresamos el resultado del método.
            object? respuesta = await SqlMetodos.ejecutarEscalar(CadenaConexion!, cadenaComandoString);

            if (entidad.Id > 0)
                respuesta = 0;

            return Convert.ToInt32(respuesta);
        }

        //Método para obtener objeto de la base de datos.
        public override async Task<Entidad> obtenerEntidad(int id)
        {
            //Obtiene la lista de constantes que tienen los atributos con los nombres de las columnas de la base de datos.
            FieldInfo[] listaConstantes = obtenerArregloConstantes();
            //Crea un arreglo donde se pondrá los nombres de las columnas.
            List<string> listaNombresColumnas = new List<string>();
            //Recorre la lista de constantes.
            foreach (FieldInfo constanteItem in listaConstantes)
            {
                //Obtenemos el atributo campo para obtener el nombre de columna.
                ColumnaAtributte? columnaAtributo = constanteItem.GetCustomAttribute<ColumnaAtributte>();
                //Compara que el atributo sea diferente de nulo.
                if (columnaAtributo != null)
                {
                    //Agregar el nombre de la columna al arreglo de nombres.
                    listaNombresColumnas.Add(columnaAtributo.Nombre); ;
                }
                else
                {
                    if (!constanteItem.Name.Equals("Id"))
                    {
                        throw new Exception("Las constantes con los nombres de las columnas deben de tener el atributo \"ColumnaAttribute\".");
                    }
                }
            }
            //Construye la cadena del comando a ejecutar.
            string cadenaComando = $"select Id, {string.Join(",", listaNombresColumnas)} from {NombreTabla} where Id = {id}";
            ComprobarCadenas(CadenaConexion, cadenaComando);
            //Trae la tabla de datos con el objeto requerido.
            DataTable tablaDatos = await SqlMetodos.obtenerTablaDatos(CadenaConexion!, cadenaComando);
            //Transforme la fila de la tabla a un objeto de tipo entidad.
            return convertirFilaAEntidad(tablaDatos.Rows[0], listaConstantes);
        }

        //Método para obtener una lista de objetos de la base de datos.
        public override async Task<List<Entidad>?> obtenerListaEntidades(List<CondicionSQLInfo>? listaCondiciones = null)
        {
            //Obtiene la tabla de datos llena.
            DataTable tablaDatos = await obtenerTablaDatos(listaCondiciones);
            //Obtiene la lista de las constantes con los nombres de las columnas.
            FieldInfo[] listaConstantes = obtenerArregloConstantes();
            //Creamos la lista que contendrá los objetos a devolver.
            List<Entidad> listaEntidades = new List<Entidad>();
            //Recorre las filas que tiene la tabla de datos.
            foreach (DataRow Fila in tablaDatos.Rows)
            {
                //Agregamos el objeto con ayuda de un método que recibe la fila y la lista de constantes para asignar valores.
                listaEntidades.Add(convertirFilaAEntidad(Fila, listaConstantes));
            }
            //Devuelve la lista llena.
            return listaEntidades;
        }

        //Método para obtener la tabla de datos de la base de datos.¿
        public override async Task<DataTable> obtenerTablaDatos(List<CondicionSQLInfo>? listaCondiciones = null)
        {
            //Obtiene la lista de constantes con los nombres de las columnas.
            FieldInfo[] listaConstantes = obtenerArregloConstantes();
            //Crea una lista que contendrá los nombres de las columnas.
            List<string> listaNombresColumnas = new List<string>();
            foreach (FieldInfo constanteItem in listaConstantes)
            {
                //Obtiene el atributo de la constante.
                ColumnaAtributte? columnaAtributo = constanteItem.GetCustomAttribute<ColumnaAtributte>();
                //Comprueba que el atributo sea diferente de nulo.
                if (columnaAtributo != null)
                {
                    //Se agrega a la lista el nombre de la columna.
                    listaNombresColumnas.Add(columnaAtributo.Nombre);
                }
                else
                {
                    if (!constanteItem.Name.Equals("Id"))
                    {
                        //Lanza un error si no se le puso el atributo a la constante.
                        throw new Exception("Se necesita poner el atributo \"ColumnaAttribute\" en los nombres de las columnas.");
                    }
                }
            }
            //Junta los nombres de las columnas con una separación por coma (,).
            string nombresColumnasUnidos = string.Join(", ", listaNombresColumnas);
            //Crea un StringBuilder para guardar las condiciones de la consulta.
            StringBuilder condiciones = new StringBuilder();
            //Comprueba que la lista de condiciones sea diferente de nulo.
            if (listaCondiciones != null && listaCondiciones.Count > 0)
            {
                //Recorre la lista de condiciones.
                foreach (CondicionSQLInfo condicionitem in listaCondiciones)
                {
                    //Guarda las condiciones en el stringbuilder.
                    condiciones.AppendLine($"{condicionitem.NombreColumna} {condicionitem.Condicional} {condicionitem.ValorComparar} ");
                    if (condicionitem.CondicionExtra != null) condiciones.AppendLine(condicionitem.CondicionExtra.ToString());
                }
            }
            //Contruye el comando.
            string cadenaComando = $"select Id, {nombresColumnasUnidos} from {NombreTabla} where Id > 0";
            //Si condiciones es diferente de nulo, las agregar a la cadena del comando.
            if (condiciones.Length > 0) cadenaComando += $"AND {condiciones.ToString()}";
            //Comprueba que ni la cadena de conexión ni el comando estén vacias.
            ComprobarCadenas(CadenaConexion, cadenaComando);
            //Ejecuta el comando en la base de datos, el cual obtiene la tabla de datos.
            DataTable tablaDatos = await SqlMetodos.obtenerTablaDatos(CadenaConexion!, cadenaComando);
            //Regresa la tabla de datos.
            return tablaDatos;
        }
        #endregion

        #region Métodos estructura...
        public override EstructuraTablaInfo obtenerEstructuraTabla()
        {
            //Se declara la lista con la información de las columnas.
            List<ColumnasTablaInfo> listaColumnasTablas = new List<ColumnasTablaInfo>();
            //Arreglo que contiene todas las constantes con los nombres de las columnas.
            FieldInfo[] arregloConstantes = obtenerArregloConstantes();
            //Recorre el arreglo con las constantes.
            foreach (FieldInfo constanteItem in arregloConstantes)
            {
                //Guardamos el atributo de la constante.
                ColumnaAtributte? atributoConstante = constanteItem.GetCustomAttribute<ColumnaAtributte>();
                //Comprobamos que si le hayan puesto un atributo a la constante.
                if (atributoConstante != null)
                {
                    //Añadimos a la lista de las columnas, la información de las columnas.
                    listaColumnasTablas.Add(
                            new ColumnasTablaInfo
                            {
                                Nombre = atributoConstante.Nombre,
                                Descripcion = atributoConstante.Descripcion,
                                Tipo = atributoConstante.Tipo,
                                Tamaño = atributoConstante.Tamaño,
                                PermiteNulos = atributoConstante.PermiteNulos,
                                ValorUnico = atributoConstante.ValorUnico,
                                Decimales = atributoConstante.Decimales,
                                ReferenciaTabla = atributoConstante.ReferenciaTabla
                            }
                        );
                }
                else
                {
                    if (!constanteItem.Name.Equals("Id")) throw new Exception("Debe de asignar el atributo campo a las constantes con los nombres de las columnas de la base de datos.");
                }
            }
            //Se crea el objeto con la estructura de la tabla.
            EstructuraTablaInfo estructuraTabla = new EstructuraTablaInfo
            {
                Nombre = NombreTabla ?? "",
                Columnas = listaColumnasTablas
            };
            //Regresa el objeto.
            return estructuraTabla;
        }
        #endregion Métodos estructura

        #region Métodos Auxiliares...

        public override int obtenerTamañoColumna(string nombreColumna)
        {
            FieldInfo[] arregloConstantes = obtenerArregloConstantes();
            int tamaño = 0;
            foreach (FieldInfo constanteItem in arregloConstantes)
            {
                if (constanteItem.Name.Equals(nombreColumna))
                {
                    ColumnaAtributte? atributoConstante = constanteItem.GetCustomAttribute<ColumnaAtributte>();
                    if (atributoConstante != null)
                    {
                        tamaño = atributoConstante.Tamaño;
                    }
                    else
                    {
                        throw new Exception("Debe de poner un atributo a la constante.");
                    }
                }
            }
            return tamaño;
        }

        //Método para obtener los valores una entidad: nombre de la columna, valor y tipo.
        private List<InformacionColumnasInfo> obtenerListaInformacionColumnas(Entidad entidad)
        {
            //Crea la lista a devolver.
            List<InformacionColumnasInfo> listaCamposValores = new List<InformacionColumnasInfo>();
            //Obtiene el tipo de la entidad recibida.
            Type tipoEntidadObjeto = entidad.GetType();
            //Crea un array con la propiedades de la entidad.
            List<PropertyInfo> listaPropiedadesObjeto = tipoEntidadObjeto.GetProperties().ToList();
            //Obtiene la lista con las constantes de los nombres de las columnas.
            FieldInfo[] listaConstantes = obtenerArregloConstantes();
            //Recorre la lista de las constantes.
            foreach (FieldInfo constanteItem in listaConstantes)
            {
                //Obtiene la propiedad que tenga el mismo nombre de el nombre de la constante.
                PropertyInfo? propiedad = listaPropiedadesObjeto.Find(propiedadItem => propiedadItem.Name.Equals(constanteItem.Name));
                //Si la propiedad se encuentra.
                if (propiedad != null)
                {
                    //Obtiene el atributo de la constante.
                    ColumnaAtributte? columnaAtributo = constanteItem.GetCustomAttribute<ColumnaAtributte>();
                    //Si el atributo es diferente de nulo.
                    if (columnaAtributo != null)
                    {
                        //Obtiene el valor en la propiedad de la entidad.
                        object? valor = propiedad.GetValue(entidad);
                        //Agregamos un objeto con los datos de la entidad a la lista.
                        listaCamposValores.Add(new InformacionColumnasInfo
                        {
                            NombreColumna = columnaAtributo.Nombre,
                            TipoCampo = columnaAtributo.Tipo,
                            ValorColumna = valor
                        });
                    }
                    //Si no, lanzamos un error que diga al usuario que debe de poner un atributo.
                    else
                    {
                        //switch (constanteItem.Name)
                        //{
                        //    case "Id":

                        //        break;
                        //    case "IdUsuario":

                        //        break;
                        //    case "FechaAlta":
                        //        listaCamposValores.Add
                        //        break;
                        //    case "FechaUltimaModificacion":

                        //        break;
                        //    case "FechaBaja":
                        //        listaCamposValores.Add(new InformacionColumnasInfo
                        //        {
                        //            NombreColumna = constanteItem.Name,
                        //            TipoCampo = ColumnaAtributte.ETipoCampo.FechaDateTime,
                        //            ValorColumna = null
                        //        });
                        //        break;

                        //}
                        if (!constanteItem.Name.Equals("Id") ) throw new Exception("Las constantes con los nombres de las columnas deben de tener el atributo \"ColumnaAttribute\".");

                    }
                }
            }
            //Regresamos la lista con la información de la entidad.
            return listaCamposValores;
        }

        //Método para convertir una DataRow a un objeto de tipo Entidad.
        public Entidad convertirFilaAEntidad(DataRow Fila, FieldInfo[] listaConstantes)
        {
            //Se crea una instancia de la entidad.
            Entidad instanciaEntidad = Activator.CreateInstance<Entidad>();
            //Iniciamos la variable Id.

            //Obtener el tipo de la entidad.
            Type tipoEntidad = instanciaEntidad.GetType();
            //Obtener las propiedades que tiene la entidad.
            PropertyInfo[] listaPropiedades = instanciaEntidad.GetType().GetProperties();
            //Recorre las constantes.
            foreach (FieldInfo constanteItem in listaConstantes)
            {
                //Recorre las propiedades.
                foreach (PropertyInfo propiedadItem in listaPropiedades)
                {

                    //Compara que el nombre de la constante coincida con el nombre de la propiedad de la entidad.
                    if (constanteItem.Name.Equals(propiedadItem.Name))
                    {
                        //Obtiene el atributo de la constante.
                        ColumnaAtributte? columnaAtributo = constanteItem.GetCustomAttribute<ColumnaAtributte>();
                        //Obtiene el tipo de la propiedad de la entidad.
                        Type tipoPropiedad = propiedadItem.PropertyType;

                        if (columnaAtributo != null)
                        {
                            //Asigna el valor obtenido de la fila de la base de datos a la propiedad.
                            propiedadItem.SetValue(instanciaEntidad, Convert.ChangeType(Fila[columnaAtributo.Nombre].ToString(), tipoPropiedad));
                        }
                        else
                        {
                            //Ya que el id no tiene atributo, se comprueba acá su existencia y se asigna al objeto a devolver.
                            if (constanteItem.Name.Equals("Id"))
                            {
                                //Asigna el valor obtenido de la fila de la base de datos a la propiedad.
                                propiedadItem.SetValue(instanciaEntidad, Convert.ChangeType(Fila["Id"].ToString(), tipoPropiedad));
                            }
                            else
                            {
                                throw new Exception("Las constantes con los nombres de las columnas deben de tener el atributo \"ColumnaAttribute\".");
                            }
                        }

                    }
                }
            }
            return instanciaEntidad;
        }

        //Método para obtener las constantes de nuestra clase que contiene el nombre de las columnas de la base de datos.
        private FieldInfo[] obtenerArregloConstantes()
        {
            //Obtiene el tipo de la clase que contiene nuestra constantes.
            Type tipoNombresColumnas = typeof(Campos);
            //Obtiene un array con las constantes de la clase.
            FieldInfo[] listaNombresConstantes = tipoNombresColumnas.GetFields();
            //Comparamos que la lista de constantes sea mayor a 0 o diferente de nulo.
            if (listaNombresConstantes.Length <= 0 || listaNombresConstantes == null) throw new Exception("Debe de implementar la clase con los nombres de los campos en constantes.");
            //Regresa la lista con las constantes.
            return listaNombresConstantes;
        }

        //Método que comprueba que las cadenas de conexión y de comando sean diferentes de nulo o vacio.
        public void ComprobarCadenas(string? cadenaConexion, string? cadenaComando)
        {
            //Se comprueba de que la cadena de conexión y el comando sean recibidos.
            if (string.IsNullOrEmpty(cadenaConexion)) throw new Exception("Favor de mandar la cadena de conexión.");
            if (string.IsNullOrEmpty(cadenaComando)) throw new Exception("Favor de mandar la cadena del comando.");
        }

        #endregion
    }
}
