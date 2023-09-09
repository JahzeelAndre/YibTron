namespace YibTronFronted.Controles
{
    partial class PantallaInicio_1001
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainerContenido = new SplitContainer();
            panelBarraNavegacionBotones = new Panel();
            splitContainerBarraNavegacionEncabezado = new SplitContainer();
            iconButtonBurguer = new FontAwesome.Sharp.IconButton();
            iconButtonDatosEmpresa = new FontAwesome.Sharp.IconButton();
            iconMenuItem1 = new FontAwesome.Sharp.IconMenuItem();
            ((System.ComponentModel.ISupportInitialize)splitContainerContenido).BeginInit();
            splitContainerContenido.Panel1.SuspendLayout();
            splitContainerContenido.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerBarraNavegacionEncabezado).BeginInit();
            splitContainerBarraNavegacionEncabezado.Panel1.SuspendLayout();
            splitContainerBarraNavegacionEncabezado.Panel2.SuspendLayout();
            splitContainerBarraNavegacionEncabezado.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerContenido
            // 
            splitContainerContenido.Dock = DockStyle.Fill;
            splitContainerContenido.Location = new Point(0, 0);
            splitContainerContenido.Name = "splitContainerContenido";
            // 
            // splitContainerContenido.Panel1
            // 
            splitContainerContenido.Panel1.Controls.Add(panelBarraNavegacionBotones);
            splitContainerContenido.Panel1.Controls.Add(splitContainerBarraNavegacionEncabezado);
            splitContainerContenido.Size = new Size(800, 450);
            splitContainerContenido.SplitterDistance = 266;
            splitContainerContenido.TabIndex = 0;
            // 
            // panelBarraNavegacionBotones
            // 
            panelBarraNavegacionBotones.AutoScroll = true;
            panelBarraNavegacionBotones.Dock = DockStyle.Fill;
            panelBarraNavegacionBotones.Location = new Point(0, 41);
            panelBarraNavegacionBotones.Name = "panelBarraNavegacionBotones";
            panelBarraNavegacionBotones.Size = new Size(266, 409);
            panelBarraNavegacionBotones.TabIndex = 2;
            // 
            // splitContainerBarraNavegacionEncabezado
            // 
            splitContainerBarraNavegacionEncabezado.Dock = DockStyle.Top;
            splitContainerBarraNavegacionEncabezado.FixedPanel = FixedPanel.Panel1;
            splitContainerBarraNavegacionEncabezado.IsSplitterFixed = true;
            splitContainerBarraNavegacionEncabezado.Location = new Point(0, 0);
            splitContainerBarraNavegacionEncabezado.Name = "splitContainerBarraNavegacionEncabezado";
            // 
            // splitContainerBarraNavegacionEncabezado.Panel1
            // 
            splitContainerBarraNavegacionEncabezado.Panel1.Controls.Add(iconButtonBurguer);
            splitContainerBarraNavegacionEncabezado.Panel1MinSize = 0;
            // 
            // splitContainerBarraNavegacionEncabezado.Panel2
            // 
            splitContainerBarraNavegacionEncabezado.Panel2.Controls.Add(iconButtonDatosEmpresa);
            splitContainerBarraNavegacionEncabezado.Size = new Size(266, 41);
            splitContainerBarraNavegacionEncabezado.SplitterDistance = 88;
            splitContainerBarraNavegacionEncabezado.TabIndex = 1;
            // 
            // iconButtonBurguer
            // 
            iconButtonBurguer.AutoSize = true;
            iconButtonBurguer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            iconButtonBurguer.Dock = DockStyle.Left;
            iconButtonBurguer.FlatAppearance.BorderSize = 0;
            iconButtonBurguer.FlatStyle = FlatStyle.Flat;
            iconButtonBurguer.IconChar = FontAwesome.Sharp.IconChar.None;
            iconButtonBurguer.IconColor = Color.Black;
            iconButtonBurguer.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonBurguer.IconSize = 35;
            iconButtonBurguer.Location = new Point(0, 0);
            iconButtonBurguer.Margin = new Padding(0);
            iconButtonBurguer.Name = "iconButtonBurguer";
            iconButtonBurguer.Size = new Size(41, 41);
            iconButtonBurguer.TabIndex = 0;
            iconButtonBurguer.TextAlign = ContentAlignment.MiddleRight;
            iconButtonBurguer.UseVisualStyleBackColor = true;
            iconButtonBurguer.Click += iconButtonBurguer_Click;
            // 
            // iconButtonDatosEmpresa
            // 
            iconButtonDatosEmpresa.AutoSize = true;
            iconButtonDatosEmpresa.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            iconButtonDatosEmpresa.Dock = DockStyle.Top;
            iconButtonDatosEmpresa.FlatAppearance.BorderSize = 0;
            iconButtonDatosEmpresa.FlatStyle = FlatStyle.Flat;
            iconButtonDatosEmpresa.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            iconButtonDatosEmpresa.IconChar = FontAwesome.Sharp.IconChar.Image;
            iconButtonDatosEmpresa.IconColor = Color.Black;
            iconButtonDatosEmpresa.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonDatosEmpresa.IconSize = 35;
            iconButtonDatosEmpresa.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonDatosEmpresa.Location = new Point(0, 0);
            iconButtonDatosEmpresa.Margin = new Padding(0);
            iconButtonDatosEmpresa.Name = "iconButtonDatosEmpresa";
            iconButtonDatosEmpresa.Size = new Size(174, 41);
            iconButtonDatosEmpresa.TabIndex = 0;
            iconButtonDatosEmpresa.Text = "YibTron";
            iconButtonDatosEmpresa.UseVisualStyleBackColor = true;
            // 
            // iconMenuItem1
            // 
            iconMenuItem1.IconChar = FontAwesome.Sharp.IconChar.None;
            iconMenuItem1.IconColor = Color.Black;
            iconMenuItem1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconMenuItem1.Name = "iconMenuItem1";
            iconMenuItem1.Size = new Size(32, 19);
            iconMenuItem1.Text = "iconMenuItem1";
            // 
            // PantallaInicio_1001
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainerContenido);
            Name = "PantallaInicio_1001";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PantallaInicio_1001";
            Resize += PantallaInicio_1001_Resize;
            splitContainerContenido.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerContenido).EndInit();
            splitContainerContenido.ResumeLayout(false);
            splitContainerBarraNavegacionEncabezado.Panel1.ResumeLayout(false);
            splitContainerBarraNavegacionEncabezado.Panel1.PerformLayout();
            splitContainerBarraNavegacionEncabezado.Panel2.ResumeLayout(false);
            splitContainerBarraNavegacionEncabezado.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerBarraNavegacionEncabezado).EndInit();
            splitContainerBarraNavegacionEncabezado.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        protected SplitContainer splitContainerContenido;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem1;
        protected SplitContainer splitContainerBarraNavegacionEncabezado;
        protected FontAwesome.Sharp.IconButton iconButtonBurguer;
        protected FontAwesome.Sharp.IconButton iconButtonDatosEmpresa;
        protected Panel panelBarraNavegacionBotones;
        private FontAwesome.Sharp.IconButton iconButton1;
    }
}