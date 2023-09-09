namespace YibTronFronted.Controles
{
    partial class PantallaConsulta_1001
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            panelContenido = new Panel();
            flowLayoutPanelHerramientas = new FlowLayoutPanel();
            iconButtonAgregar = new FontAwesome.Sharp.IconButton();
            iconButtonEditar = new FontAwesome.Sharp.IconButton();
            iconButtonBorrar = new FontAwesome.Sharp.IconButton();
            iconButtonFiltro = new FontAwesome.Sharp.IconButton();
            textBoxBuscar = new TextBox();
            panelContenido.SuspendLayout();
            flowLayoutPanelHerramientas.SuspendLayout();
            SuspendLayout();
            // 
            // panelContenido
            // 
            panelContenido.Controls.Add(flowLayoutPanelHerramientas);
            panelContenido.Dock = DockStyle.Fill;
            panelContenido.Location = new Point(0, 0);
            panelContenido.Name = "panelContenido";
            panelContenido.Size = new Size(814, 489);
            panelContenido.TabIndex = 0;
            // 
            // flowLayoutPanelHerramientas
            // 
            flowLayoutPanelHerramientas.AutoScroll = true;
            flowLayoutPanelHerramientas.AutoScrollMargin = new Size(0, 5);
            flowLayoutPanelHerramientas.AutoSize = true;
            flowLayoutPanelHerramientas.Controls.Add(iconButtonAgregar);
            flowLayoutPanelHerramientas.Controls.Add(iconButtonEditar);
            flowLayoutPanelHerramientas.Controls.Add(iconButtonBorrar);
            flowLayoutPanelHerramientas.Controls.Add(iconButtonFiltro);
            flowLayoutPanelHerramientas.Controls.Add(textBoxBuscar);
            flowLayoutPanelHerramientas.Dock = DockStyle.Top;
            flowLayoutPanelHerramientas.Location = new Point(0, 0);
            flowLayoutPanelHerramientas.Name = "flowLayoutPanelHerramientas";
            flowLayoutPanelHerramientas.Size = new Size(814, 47);
            flowLayoutPanelHerramientas.TabIndex = 0;
            flowLayoutPanelHerramientas.WrapContents = false;
            // 
            // iconButtonAgregar
            // 
            iconButtonAgregar.AutoSize = true;
            iconButtonAgregar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            iconButtonAgregar.FlatAppearance.BorderSize = 0;
            iconButtonAgregar.FlatStyle = FlatStyle.Flat;
            iconButtonAgregar.IconChar = FontAwesome.Sharp.IconChar.Add;
            iconButtonAgregar.IconColor = Color.SteelBlue;
            iconButtonAgregar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonAgregar.IconSize = 35;
            iconButtonAgregar.Location = new Point(3, 3);
            iconButtonAgregar.Name = "iconButtonAgregar";
            iconButtonAgregar.Size = new Size(41, 41);
            iconButtonAgregar.TabIndex = 0;
            iconButtonAgregar.UseVisualStyleBackColor = true;
            // 
            // iconButtonEditar
            // 
            iconButtonEditar.AutoSize = true;
            iconButtonEditar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            iconButtonEditar.FlatAppearance.BorderSize = 0;
            iconButtonEditar.FlatStyle = FlatStyle.Flat;
            iconButtonEditar.IconChar = FontAwesome.Sharp.IconChar.PencilAlt;
            iconButtonEditar.IconColor = Color.Goldenrod;
            iconButtonEditar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonEditar.IconSize = 35;
            iconButtonEditar.Location = new Point(50, 3);
            iconButtonEditar.Name = "iconButtonEditar";
            iconButtonEditar.Size = new Size(41, 41);
            iconButtonEditar.TabIndex = 1;
            iconButtonEditar.UseVisualStyleBackColor = true;
            // 
            // iconButtonBorrar
            // 
            iconButtonBorrar.AutoSize = true;
            iconButtonBorrar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            iconButtonBorrar.FlatAppearance.BorderSize = 0;
            iconButtonBorrar.FlatStyle = FlatStyle.Flat;
            iconButtonBorrar.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            iconButtonBorrar.IconColor = Color.SlateGray;
            iconButtonBorrar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonBorrar.IconSize = 35;
            iconButtonBorrar.Location = new Point(97, 3);
            iconButtonBorrar.Name = "iconButtonBorrar";
            iconButtonBorrar.Size = new Size(41, 41);
            iconButtonBorrar.TabIndex = 2;
            iconButtonBorrar.UseVisualStyleBackColor = true;
            // 
            // iconButtonFiltro
            // 
            iconButtonFiltro.Anchor = AnchorStyles.Left;
            iconButtonFiltro.AutoSize = true;
            iconButtonFiltro.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            iconButtonFiltro.FlatAppearance.BorderSize = 0;
            iconButtonFiltro.FlatStyle = FlatStyle.Flat;
            iconButtonFiltro.IconChar = FontAwesome.Sharp.IconChar.Filter;
            iconButtonFiltro.IconColor = Color.Black;
            iconButtonFiltro.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonFiltro.IconSize = 25;
            iconButtonFiltro.Location = new Point(144, 8);
            iconButtonFiltro.Name = "iconButtonFiltro";
            iconButtonFiltro.Size = new Size(31, 31);
            iconButtonFiltro.TabIndex = 3;
            iconButtonFiltro.UseVisualStyleBackColor = true;
            // 
            // textBoxBuscar
            // 
            textBoxBuscar.Anchor = AnchorStyles.Left;
            textBoxBuscar.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxBuscar.Location = new Point(181, 10);
            textBoxBuscar.Name = "textBoxBuscar";
            textBoxBuscar.PlaceholderText = "Buscar...";
            textBoxBuscar.Size = new Size(308, 27);
            textBoxBuscar.TabIndex = 4;
            // 
            // PantallaConsulta_1001
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelContenido);
            Name = "PantallaConsulta_1001";
            Size = new Size(814, 489);
            panelContenido.ResumeLayout(false);
            panelContenido.PerformLayout();
            flowLayoutPanelHerramientas.ResumeLayout(false);
            flowLayoutPanelHerramientas.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        protected Panel panelContenido;
        protected FlowLayoutPanel flowLayoutPanelHerramientas;
        protected FontAwesome.Sharp.IconButton iconButtonAgregar;
        protected FontAwesome.Sharp.IconButton iconButtonEditar;
        protected FontAwesome.Sharp.IconButton iconButtonBorrar;
        protected FontAwesome.Sharp.IconButton iconButtonFiltro;
        protected TextBox textBoxBuscar;
    }
}
