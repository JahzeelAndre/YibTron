![LogoYibTron](https://github.com/JahzeelAndre/YibTronLibrary/assets/95154848/87c170fa-173e-4f34-8855-75d4e1b6ef82)
<h1> Bienvenidos al repositorio de la librería YibTron </h1>
<p> <b> Este repositorio contendrá dos proyectos de Net (biblioteca de clases y biblioteca de controles windows forms), uno será para el uso en el backend, y el otro para el uso en el frontend. </b> </p>

<h2>Proyectos: </h2>

<h3> YibTronBackend </h3>
<p> Este proyecto facilita la creación de la base de datos y tablas que se ocuparán dentro del proyecto creado con el framework Net 7 y el lenguaje de programación C#. </p>
<p> También contiene clases que son heredables y ya tienen métodos implementados para desarrollar un crud (obtener, agregar, modificar y eliminar). </p>

<H4>Instrucciones de uso:</H4>
<p>Se necesita crear una clase entidad, la cual contiene todas las propiedades de un objeto.</p>
<p><b>Ejemplo:</b></p>

<pre> 
  <code >
//Clase de la entidad, que hereda de una interface, lo que la obliga a tener la propiedad Id.
public class EjemploEntidad : IEntidad {
    
  //Propiedad Id.
  public int Id { get; set; }
    
  //Propiedad del objeto, esta es de ejemplo, pueden ser muchas más.
  public string PropiedadRandom { get; set; }

  //Clase que tiene constantes con el nombres de las columnas que tendrán o tienen en las tablas de la base de datos.
  public class NombresColumnas {

    //Nombre de la columna que contiene el Id, es obligatorio ponerle este nombre y valor a la constante.
    public const string Id = "Id";

    /*A continuación, se coloca un atributo a todas las constantes de la entidad, excepto a la constante Id. Este atributo tiene para implementar más cosas que son opcionales.
    Nombre de la columna que contiene la PropiedadRandom.*/
    [ColumnaAtributte(nombre: PropiedadRandom, descripcion: "Descripción de la columna en la tabla...", tipo: ETipoCampo.Texto, tamaño: 50, permiteNulos: false, valorUnico: true)]
    public const string PropiedadRandom = "PropiedadRandom";
  
  }
    
}
  </code> 
</pre>

<p>Ya que se tiene creada la entidad, lo siguiente es crear la clase de datos, la cual, es la encargada de comunicarse directamente con la base de datos.</p>
<p><b>Ejemplo:</b></p>

<pre>
  <code>
    //Se crea la clase de datos, esta debe heredar de la clase RepositorioImplTablaDAL y recibir la clase de la entidad y la clase con los nombres de las columnas.
    public class EjemploDatos : RepositorioImplTablaDAL &lt; EjemploEntidad, EjemploEntidad.NombresColumnas &gt; {
      //Únicamente se crea el contructor y asigna el nombre que tendrá la tabla para esa entidad.
      public EjemploDatos() : super() {
        NombreTabla = "TablaEjemplo";
      }
    }
  </code>
</pre>

<p>Al finalizar la clase de datos, proseguimos a crear la clase de negocios, la cual será la intermediaria entre la capa de presentación y la capa de datos.</p>
<p><b>Ejemplo:</b></p>

<pre>
  <code>
    //Se crea la clase de datos, esta debe heredar de la clase RepositorioImplTablaBAL y recibir la clase de la entidad, la clase con los nombres de las columnas y la clase de datos creada.
    //Después no se tiene que poner nada más, porque los métodos ya están implementados en la clase heredada.
    public class EjemploNegocio : RepositorioImplTablaBAL &lt; EjemploEntidad, EjemploEntidad.NombresColumnas, EjemploDatos &gt; {
      
    }
  </code>
</pre>

<h4>Ya para finalizar, solo debes de instanciar la clase de negocio en donde la vayas a ocupar, suele instanciarse en la capa de presentación.</h4>

<h3>YibTron Frontend</h3>
<h4>Primeramente, debemos instalar el nuget "FontAwesome.Sharp", en dado caso de que no se haya instalado anteriormente.</h4>

<p>Dicha librería, consta de tres controles: PantallaBase_1001, PantallaConsulta_1001 y PantallaInicio_1001</p>

<p>Tanto PantallaBase_1001, como PantallaConsulta1001, son controles de usuario, para hacer uso de estos controles, basta con crear un nuevo control de usuario en su proyecto y en el código c# le heredan a su clase uno de los dos controles mencionados anteriormente.</p>

<p>Para el formulario PantallaInicio_1001, se debe crear un formulario, y heredarle el control de PantallaInicio_1001</p>

<p>Nota. Casi todos los controles y formularios tienen los métodos IniciarVariables e IniciarComponentes, en dado caso de no tener el método "IniciarVariables", solo deben mandar a traer el método "IniciarComponentes", en dado caso de tener ambos métodos, entonces estos se deben mandar a traer en el constructor de las clases que heredaron de ellos, en el siguiente orden: </p>
<ol>
  <li>IniciarVariables</li>
  <li>IniciarComponentes</li>
</ol>
<p>A continuación, se muestra un ejemplo utilizando como referencia el formulario "PantallaInicio_1001: </p>
<pre>
  <code>
    public partial class Form1 : YibTronFronted.Controles.PantallaInicio_1001
    {
        public Form1()
        {
            InitializeComponent();
            IniciarVariables();
            IniciarComponentes();
        }
    }
  </code>
</pre>

<p>Al heredarle el formulario PantallaInicio_1001, aparecerá una barra lateral, el cual tendrá los botones para navegar entre sus distintas vistas. Para poder agregar opciones de navegación, deberán arrastrar de la barra de herramientas los "IconButton" del nuget FontAwesome.Sharp y colocarlos encima del panel "panelBarraNavegacionBotones" y de preferencia poner su propiedad "Dock" en "Top", el demás diseño se lo darán los métodos mostrados anteriomente (IniciarVariables e IniciarComponentes).</p>

<p>Para los IconButton que se pongan en el panel de navegación , se les deberá poner en su acción click lo siguiente: </p>
<pre>
  <code>
    private void iconButton1_Click(object sender, EventArgs e)
        {
            activarBotonNavegacion(sender);
        }
  </code>
</pre>

<h3>Agradecemos su confianza en nuestro proyecto.</h3>
