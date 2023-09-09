//Librerías internas.
using System.Diagnostics;
using YibTronFronted.Utilerias;
//Librerías externas.
using FontAwesome.Sharp;

namespace YibTronFronted.Controles
{
    public partial class PantallaInicio_1001 : Form
    {
        #region Variables...
        /// <summary>
        /// Variable para guardar el estado del panel de navegación, si está colapsado o no.
        /// </summary>
        protected bool _estaColapsado = false;

        /// <summary>
        /// Variable para saber el botón de navegación actual.
        /// </summary>
        protected IconButton? _botonActual;

        /// <summary>
        /// Lista con los botones de la barra de navegación.
        /// </summary>
        protected IconButton[]? _arregloBotonesBarraNavegacion;

        /// <summary>
        /// Lista con el texto de los botones de la barra de navegación.
        /// </summary>
        protected List<string>? _listaTextoBotonesBarraNavegacion;

        #endregion Variables

        #region Constructor...
        public PantallaInicio_1001()
        {
            InitializeComponent();
            IniciarVariables();
            IniciarComponentes();
        }
        #endregion Constructor

        #region Métodos...

        /// <summary>
        /// Método para inicializar las variables.
        /// </summary>
        protected void IniciarVariables()
        {
            //Se inicializa la lista con los textos de los botones que hay en el panel de navegación.
            _listaTextoBotonesBarraNavegacion = new List<string>();
            //Se inicializa y rellena el arreglo con los botones que hay en el panel de navegación.
            _arregloBotonesBarraNavegacion = obtenerArregloControlesPanelNavegacion();
        }

        /// <summary>
        /// Método para inicializar los componentes o controles.
        /// </summary>
        protected void IniciarComponentes()
        {
            BackColor = ColoresBase.PRIMER_TEMA_CONTROL_FONDO_CLARO;
            splitContainerContenido.BackColor = BackColor;
            //Se cambia el color del panel de navegación.
            splitContainerContenido.Panel1.BackColor = ColoresBase.PRIMER_TEMA_PANEL_NAVEGACION;
            //Se configura el botón utilizado para minimizar el panel de navegación.
            iconButtonBurguer.IconChar = IconChar.Bars;
            iconButtonBurguer.ImageAlign = ContentAlignment.MiddleCenter;
            iconButtonBurguer.IconColor = ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
            iconButtonBurguer.MouseEnter += BotonItem_MouseEnter;
            iconButtonBurguer.MouseLeave += BotonItem_MouseLeave;
            //Se configura el botón con los datos de la empresa.
            iconButtonDatosEmpresa.ForeColor = ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
            iconButtonDatosEmpresa.IconColor = ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
            iconButtonDatosEmpresa.MouseEnter += BotonItem_MouseEnter;
            iconButtonDatosEmpresa.MouseLeave += BotonItem_MouseLeave;
            //Se comprueba que el arreglo con los botones del panel de navegación no sea nulo.
            if (_arregloBotonesBarraNavegacion != null && _arregloBotonesBarraNavegacion.Length > 0)
            {
                //Se recorre el arreglo con los botones del panel de navegación.
                foreach (IconButton botonItem in _arregloBotonesBarraNavegacion)
                {
                    //Se les da diseño a los botones del panel de navegación.
                    botonItem.Font = Letras.letraPrimaria();
                    botonItem.ForeColor = ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
                    botonItem.IconColor = ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
                    botonItem.FlatAppearance.BorderSize = 0;
                    botonItem.FlatStyle = FlatStyle.Flat;
                    botonItem.AutoSize = true;
                    botonItem.TextImageRelation = TextImageRelation.Overlay;
                    botonItem.ImageAlign = ContentAlignment.MiddleLeft;
                    botonItem.TextAlign = ContentAlignment.MiddleCenter;
                    botonItem.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    botonItem.MouseEnter += BotonItem_MouseEnter;
                    botonItem.MouseLeave += BotonItem_MouseLeave;
                }
            }
            //Se inicializa el tamaño del panel de navegación.
            CambiarTamañoPanelNavegacion();
        }

        /// <summary>
        /// Método para obtener el arreglo de botones que se encuentran en la barra de navegación.
        /// </summary>
        /// <returns>Valor List<IconButton></returns>
        protected IconButton[] obtenerArregloControlesPanelNavegacion()
        {
            //Se crea el arreglo a regresar, con los botones del panel de navegación.
            IconButton[] ArregloControles = panelBarraNavegacionBotones.Controls.OfType<IconButton>().ToArray();
            //Se recorre el arreglo de botones del panel de navegación.
            foreach (IconButton botonItem in panelBarraNavegacionBotones.Controls.OfType<IconButton>())
            {
                //Se agregar el texto del botón a la lista con los textos de los botones del panel de navegación.
                _listaTextoBotonesBarraNavegacion!.Add(botonItem.Text);
            }
            //Se regresa el arreglo.
            return ArregloControles;
        }

        /// <summary>
        /// Método para cambiar de tamaño el panel de navegación.
        /// </summary>
        protected void CambiarTamañoPanelNavegacion()
        {
            //Se crea una variable con la cantidad de tamaño que tendrá el primer panel del panel principal, cuando dicho panel esté colapsado.
            int margen = 7;
            //Se cambia el tamaño solo cuando la ventana cambie su tamaño en estado visible, no se ejecuta si se minimiza la ventana.
            if (WindowState != FormWindowState.Minimized)
            {
                //Comprueba si el tamaño de la ventana es mayor a 1200.
                if (Width > 1200)
                {
                    //Hace más grandes lo íconos y la letra de los botones dentro del panel de navegación.
                    iconButtonBurguer.IconSize = 40;
                    iconButtonDatosEmpresa.IconSize = 40;
                    iconButtonDatosEmpresa.Font = Letras.letraPrimaria(13);
                    //Comprueba que el arreglo con los botones del panel de navegación no sea nulo.
                    if (_arregloBotonesBarraNavegacion != null && _arregloBotonesBarraNavegacion.Length > 0)
                    {
                        //Recorre el arreglo con los botones del panel de navegación.
                        foreach (IconButton botonItem in _arregloBotonesBarraNavegacion)
                        {
                            //Les cambia el texto y tamaño de letra a los botones.
                            botonItem.IconSize = 40;
                            botonItem.Font = Letras.letraPrimaria(13);
                        }
                    }
                }
                else
                {
                    iconButtonBurguer.IconSize = 35;
                    iconButtonDatosEmpresa.IconSize = 35;
                    iconButtonDatosEmpresa.Font = Letras.letraPrimaria();
                    //Comprueba que el arreglo con los botones del panel de navegación no sea nulo.
                    if (_arregloBotonesBarraNavegacion != null && _arregloBotonesBarraNavegacion.Length > 0)
                    {
                        //Recorre el arreglo con los botones del panel de navegación.
                        foreach (IconButton botonItem in _arregloBotonesBarraNavegacion)
                        {
                            //Les cambia el texto y tamaño de letra a los botones.
                            botonItem.IconSize = 35;
                            botonItem.Font = Letras.letraPrimaria();
                        }
                    }
                }
                //Revisa si el estado del panel de navegación es colapsado.
                if (_estaColapsado)
                {
                    //Hace chico el tamaño del panel 1.
                    splitContainerContenido.SplitterDistance = iconButtonBurguer.IconSize + margen;
                    splitContainerBarraNavegacionEncabezado.SplitterDistance = iconButtonBurguer.IconSize + margen;
                }
                else
                {
                    //Alarga el tamaño del panel del contenido 1.
                    splitContainerContenido.SplitterDistance = Size.Width / 5;
                    splitContainerBarraNavegacionEncabezado.SplitterDistance = iconButtonBurguer.IconSize + margen;
                }
            }
        }

        /// <summary>
        /// Método para activar el botón de navegación actual.
        /// </summary>
        /// <param name="botonNavegacion"></param>
        protected void activarBotonNavegacion(object botonNavegacion)
        {
            //Comprueba de que haya un botón actual.
            if (botonNavegacion != null)
            {
                //Método para desactivar todos los botones del panel de navegación que no se seleccionaron.
                desactivarBotonesNavegacion();
                //Activa y modifica el botón que se seleccionó.
                _botonActual = (IconButton)botonNavegacion;
                _botonActual.BackColor = Utilerias.ColoresBase.PRIMER_TEMA_PANEL_NAVEGACION_MENU_SELECCIONADO;
                _botonActual.ForeColor = Utilerias.ColoresBase.PRIMER_TEMA_CONTROL_SELECCIONADO_CLARO;
                _botonActual.IconColor = Utilerias.ColoresBase.PRIMER_TEMA_CONTROL_SELECCIONADO_CLARO;
            }
        }

        /// <summary>
        /// Método para desactivar los botones de navegación no utilizados.
        /// </summary>
        protected void desactivarBotonesNavegacion()
        {
            //Si el botón actual de diferente de nulo, desmarca todos los botones del panel de navegación que no hayan sido seleccionados.
            if (_botonActual != null)
            {
                _botonActual.ForeColor = Utilerias.ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
                _botonActual.IconColor = Utilerias.ColoresBase.PRIMER_TEMA_TEXTO_CLARO;
                _botonActual.BackColor = Utilerias.ColoresBase.PRIMER_TEMA_PANEL_NAVEGACION;
            }
        }

        /// <summary>
        /// Desmarcar todos los botones del panel de navegación.
        /// </summary>
        protected void ReiniciarBotonesNavegacion()
        {
            //Desactiva todos los botones del panel de navegación.
            desactivarBotonesNavegacion();
        }

        #endregion Métodos

        #region Eventos...

        private void PantallaInicio_1001_Resize(object sender, EventArgs e)
        {
            //Ejecuta el método para cambiar el tamaño del panel de navegación.
            Debug.WriteLine("Resize: " + Width);
            CambiarTamañoPanelNavegacion();
        }

        private void iconButtonBurguer_Click(object sender, EventArgs e)
        {
            //Cambia el estado del panel de navegación.
            _estaColapsado = !_estaColapsado;
            //Comprueba si el estado de navegación es colapsado.
            if (_estaColapsado)
            {
                //Colapsa el panel del menú, donde se encuentra el botón con los datos de la empresa.
                splitContainerBarraNavegacionEncabezado.Panel2Collapsed = true;
                //Comprueba de que el arreglo con los botones el panel de navegación no sea nulo.
                if (_arregloBotonesBarraNavegacion != null && _arregloBotonesBarraNavegacion.Length > 0)
                {
                    //Recorre los botones del panel de navegación.
                    foreach (IconButton botonItem in _arregloBotonesBarraNavegacion)
                    {
                        //Les quita el texto a los botones.
                        botonItem.Text = "";
                    }
                }
            }
            else
            {
                //Comprueba de que el arreglo con los botones el panel de navegación no sea nulo.
                if (_arregloBotonesBarraNavegacion != null && _arregloBotonesBarraNavegacion.Length > 0)
                {
                    //Recorre los botones del panel de navegación.
                    for (int i = 0; i < _arregloBotonesBarraNavegacion.Length; i++)
                    {
                        //Les asgina su respectivo texto a los botones.
                        _arregloBotonesBarraNavegacion[i].Text = _listaTextoBotonesBarraNavegacion![i];
                    }
                }
                //Vuelve a mostrar el panel que contiene el botón con los datos de la empresa.
                splitContainerBarraNavegacionEncabezado.Panel2Collapsed = false;
            }
            //Ejecuta el método para cambiar de tamaño el panel de navegación.
            CambiarTamañoPanelNavegacion();
        }

        //Evento de los botones dentro del panel de navegación, para cuando el cursor esté encima de ellos, cambie el tipo de cursor al default.
        private void BotonItem_MouseLeave(object? sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        //Evento de los botones dentro del panel de navegación, para cuando el cursor esté encima de ellos, cambie el tipo de cursor a la manita.
        private void BotonItem_MouseEnter(object? sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        #endregion Eventos
    }
}
