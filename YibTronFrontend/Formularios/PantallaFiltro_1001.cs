namespace YibTronFronted.Formularios
{
    partial class PantallaFiltro_1001 : Form
    {
        #region Propiedades...
        public TreeView treeView { get; set; }
        #endregion

        public PantallaFiltro_1001(TreeView treeView)
        {
            InitializeComponent();
            this.treeView = treeView;
            Size = new Size(200, 200);
            Point localizacionButtonFiltro = Cursor.Position;
            StartPosition = FormStartPosition.Manual;
            Location = localizacionButtonFiltro;
            //----------------------------------------- Button Aceptar -----------------------------------------
            Button btnAceptar = new Button();
            btnAceptar.Text = "Aceptar";
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Font = YibTronFronted.Utilerias.Letras.letraPrimaria(9);
            btnAceptar.TabIndex = 0;
            btnAceptar.DialogResult = DialogResult.OK;
            btnAceptar.AutoSize = true;
            btnAceptar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnAceptar.Margin = new Padding(0);
            btnAceptar.Padding = new Padding(0);
            btnAceptar.Anchor = AnchorStyles.None;

            //----------------------------------------- Button Limpiar -----------------------------------------
            Button btnLimpiar = new Button();
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Font = YibTronFronted.Utilerias.Letras.letraPrimaria(9);
            btnLimpiar.TabIndex = 1;
            //btnCancelar.DialogResult = DialogResult.Retry;
            btnLimpiar.AutoSize = true;
            btnLimpiar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnLimpiar.Margin = new Padding(0);
            btnLimpiar.Padding = new Padding(0);
            btnLimpiar.Anchor = AnchorStyles.None;

            btnLimpiar.Click += BtnLimpiar_Click;

            //----------------------------------------- TableLayoutPanel Principial -----------------------------------------
            TableLayoutPanel tableLayoutPrincipal = new TableLayoutPanel();
            tableLayoutPrincipal.Name = "tableLayoutPrincipal";
            tableLayoutPrincipal.Margin = new Padding(0);
            tableLayoutPrincipal.ColumnCount = 1;
            tableLayoutPrincipal.RowCount = 2;

            tableLayoutPrincipal.ColumnStyles.Clear();
            tableLayoutPrincipal.RowStyles.Clear();
            tableLayoutPrincipal.ColumnStyles.Add(new ColumnStyle { SizeType = SizeType.Percent, Width = 100 });
            tableLayoutPrincipal.RowStyles.Add(new RowStyle { SizeType = SizeType.Percent, Height = 80 });
            tableLayoutPrincipal.RowStyles.Add(new RowStyle { SizeType = SizeType.Percent, Height = 20 });

            //----------------------------------------- TableLayoutPanel Botones Aceptar & Cancelar -----------------------------------------
            TableLayoutPanel tableLayoutBotones = new TableLayoutPanel();
            tableLayoutBotones.Name = "tableLayoutBotones";
            tableLayoutBotones.Margin = new Padding(0);
            tableLayoutBotones.ColumnCount = 2;
            tableLayoutBotones.RowCount = 1;

            tableLayoutBotones.ColumnStyles.Clear();
            tableLayoutBotones.RowStyles.Clear();
            tableLayoutBotones.RowStyles.Add(new RowStyle { SizeType = SizeType.Percent, Height = 100 });
            tableLayoutBotones.ColumnStyles.Add(new ColumnStyle { SizeType = SizeType.Percent, Width = 50 });
            tableLayoutBotones.ColumnStyles.Add(new ColumnStyle { SizeType = SizeType.Percent, Width = 50 });

            tableLayoutBotones.Controls.Add(btnAceptar);
            tableLayoutBotones.Controls.Add(btnLimpiar);

            tableLayoutBotones.Dock = DockStyle.Fill;

            //----------------------------------------- TreeView Filtro -----------------------------------------
            this.treeView.CheckBoxes = true;
            this.treeView.Dock = DockStyle.Fill;
            asignarFuncionesNodosPadres();
            //----------------------------------------- Agregar a TableLayoutPanel Principal -----------------------------------------
            tableLayoutPrincipal.Controls.Add(this.treeView);
            tableLayoutPrincipal.Controls.Add(tableLayoutBotones);

            tableLayoutPrincipal.Dock = DockStyle.Fill;
            Controls.Add(tableLayoutPrincipal);
        }

        #region Métodos...
        public void asignarFuncionesNodosPadres()
        {
            treeView.AfterCheck += TreeView_AfterCheck;
        }

        public void limpiarTree(TreeNode node, bool btnLimpiar, bool valorPadre = false)
        {
            if(btnLimpiar)
                node.Checked = false;
            else
                node.Checked = valorPadre;
            foreach (TreeNode nodeItem in node.Nodes)
            {
                if(btnLimpiar)
                    nodeItem.Checked = false;
                else
                    nodeItem.Checked = !nodeItem.Checked;

                limpiarTree(nodeItem, btnLimpiar);
            }
        }
        #endregion

        #region Eventos...
        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            foreach (TreeNode nodeItem in treeView.Nodes)
            {
                limpiarTree(nodeItem, true);
            }
        }

        private void TreeView_AfterCheck(object? sender, TreeViewEventArgs e)
        {
            treeView.Enabled = false;
            foreach(TreeNode nodeItem in e.Node!.Nodes)
            {
                limpiarTree(nodeItem, false, e.Node.Checked);
            }
            treeView.Enabled = true;
        }
        #endregion
    }
}
