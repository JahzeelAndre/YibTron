//Librerías internas.
using YibTronFronted.Utilerias;

namespace YibTronFronted.Controles
{
    public partial class PantallaBase_1001 : UserControl
    {
        public PantallaBase_1001()
        {
            InitializeComponent();
        }

        public void temaClaro()
        {
            BackColor = ColoresBase.PRIMER_TEMA_CONTROL_FONDO_CLARO;
        }
        
        public void temaOscuro()
        {
            BackColor = ColoresBase.PRIMER_TEMA_CONTROL_FONDO_OSCURO;
        }
    }
}
