using FontAwesome.Sharp;
using YibTronFronted.Utilerias;

namespace YibTronFronted.Controles
{
    public partial class PantallaConsulta_1001 : UserControl
    {
        #region Constructor...
        public PantallaConsulta_1001()
        {
            InitializeComponent();
            IniciarComponentes();
        }
        #endregion Constructor

        #region Métodos...
        public void IniciarComponentes()
        {
            flowLayoutPanelHerramientas.BackColor = Utilerias.ColoresBase.PRIMER_TEMA_PANEL_HERRAMIENTAS;
            recorrerBotonesHerramientas(flowLayoutPanelHerramientas);
        }

        public void recorrerBotonesHerramientas(Control control)
        {
            foreach (Control controlItem in control.Controls)
            {
                if (controlItem.HasChildren) recorrerBotonesHerramientas(controlItem);

                foreach (IconButton controlPadreItem in control.Controls.OfType<Button>())
                {
                    controlPadreItem.Font = Letras.letraPrimaria();
                    controlPadreItem.FlatAppearance.BorderSize = 0;
                    controlPadreItem.FlatStyle = FlatStyle.Flat;
                    controlPadreItem.AutoSize = true;
                    controlPadreItem.TextImageRelation = TextImageRelation.ImageBeforeText;
                    controlPadreItem.ImageAlign = ContentAlignment.MiddleLeft;
                    controlPadreItem.TextAlign = ContentAlignment.MiddleCenter;
                    controlPadreItem.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    controlPadreItem.MouseEnter += ControlPadreItem_MouseEnter;
                    controlPadreItem.MouseLeave += ControlPadreItem_MouseLeave;
                }
            }
        }
        #endregion Métodos

        #region Eventos...
        private void ControlPadreItem_MouseLeave(object? sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void ControlPadreItem_MouseEnter(object? sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        #endregion Eventos
    }
}
