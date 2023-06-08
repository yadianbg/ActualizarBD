namespace ActualizarBD
{
    partial class frmPrincipal
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.wzdActualizarBD = new Sicema.WizardControls.Wizard();
            this.pageBienvenida = new Sicema.WizardControls.WizardPage();
            this.pageFinal = new Sicema.WizardControls.WizardPage();
            this.pageProgreso = new Sicema.WizardControls.WizardPage();
            this.lvwEstado = new System.Windows.Forms.ListView();
            this.Proceso = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Estado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlVerificar = new System.Windows.Forms.ImageList(this.components);
            this.pageVerificacion = new Sicema.WizardControls.WizardPage();
            this.lblEstado = new System.Windows.Forms.Label();
            this.lvwConfig = new System.Windows.Forms.ListView();
            this.Id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Parametro = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Valor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pageUbicacion = new Sicema.WizardControls.WizardPage();
            this.frParametros = new System.Windows.Forms.GroupBox();
            this.txtCaminoDatos = new System.Windows.Forms.TextBox();
            this.btnDatos = new System.Windows.Forms.Button();
            this.pageConfiguracion = new Sicema.WizardControls.WizardPage();
            this.frConectarse = new System.Windows.Forms.GroupBox();
            this.optSeguridad1 = new System.Windows.Forms.RadioButton();
            this.optSeguridad0 = new System.Windows.Forms.RadioButton();
            this.btnProbar = new System.Windows.Forms.Button();
            this.txtContrasenna = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.cbxCatalogo = new System.Windows.Forms.ComboBox();
            this.cbxServidor = new System.Windows.Forms.ComboBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.lblContrasenna = new System.Windows.Forms.Label();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.lblSeguridad = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblServidor = new System.Windows.Forms.Label();
            this.pageLicencia = new Sicema.WizardControls.WizardPage();
            this.chkAcuerdo = new System.Windows.Forms.CheckBox();
            this.txtLicencia = new System.Windows.Forms.TextBox();
            this.ttMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.errIconoError = new System.Windows.Forms.ErrorProvider(this.components);
            this.errIconoInfo = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.wzdActualizarBD.SuspendLayout();
            this.pageProgreso.SuspendLayout();
            this.pageVerificacion.SuspendLayout();
            this.pageUbicacion.SuspendLayout();
            this.frParametros.SuspendLayout();
            this.pageConfiguracion.SuspendLayout();
            this.frConectarse.SuspendLayout();
            this.pageLicencia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errIconoError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errIconoInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // wzdActualizarBD
            // 
            this.wzdActualizarBD.Controls.Add(this.pageUbicacion);
            this.wzdActualizarBD.Controls.Add(this.pageConfiguracion);
            this.wzdActualizarBD.Controls.Add(this.pageLicencia);
            this.wzdActualizarBD.Controls.Add(this.pageBienvenida);
            this.wzdActualizarBD.Controls.Add(this.pageFinal);
            this.wzdActualizarBD.Controls.Add(this.pageProgreso);
            this.wzdActualizarBD.Controls.Add(this.pageVerificacion);
            this.wzdActualizarBD.HeaderImage = ((System.Drawing.Image)(resources.GetObject("wzdActualizarBD.HeaderImage")));
            this.wzdActualizarBD.HelpVisible = true;
            this.wzdActualizarBD.Location = new System.Drawing.Point(0, 0);
            this.wzdActualizarBD.Name = "wzdActualizarBD";
            this.wzdActualizarBD.Pages.AddRange(new Sicema.WizardControls.WizardPage[] {
            this.pageBienvenida,
            this.pageLicencia,
            this.pageConfiguracion,
            this.pageUbicacion,
            this.pageVerificacion,
            this.pageProgreso,
            this.pageFinal});
            this.wzdActualizarBD.Size = new System.Drawing.Size(500, 354);
            this.wzdActualizarBD.TabIndex = 1;
            this.wzdActualizarBD.WelcomeImage = global::ActualizarBD.Properties.Resources.wizard;
            this.wzdActualizarBD.BeforeSwitchPages += new Sicema.WizardControls.Wizard.BeforeSwitchPagesEventHandler(this.wzdActualizarBD_BeforeSwitchPages);
            this.wzdActualizarBD.AfterSwitchPages += new Sicema.WizardControls.Wizard.AfterSwitchPagesEventHandler(this.wzdActualizarBD_AfterSwitchPages);
            this.wzdActualizarBD.Cancel += new System.ComponentModel.CancelEventHandler(this.wzdActualizarBD_Cancel);
            this.wzdActualizarBD.Help += new System.EventHandler(this.wzdActualizarBD_Help);
            // 
            // pageBienvenida
            // 
            this.pageBienvenida.Description = resources.GetString("pageBienvenida.Description");
            this.pageBienvenida.Location = new System.Drawing.Point(0, 0);
            this.pageBienvenida.Name = "pageBienvenida";
            this.pageBienvenida.Size = new System.Drawing.Size(500, 306);
            this.pageBienvenida.Style = Sicema.WizardControls.WizardPageStyle.Welcome;
            this.pageBienvenida.TabIndex = 1;
            this.pageBienvenida.Title = "Bienvenido";
            // 
            // pageFinal
            // 
            this.pageFinal.Description = "La actualización de la base de datos en el servidor a finalizado satisfactoriamen" +
    "te.\n\nPresione [Aceptar] para salir.";
            this.pageFinal.Location = new System.Drawing.Point(0, 0);
            this.pageFinal.Name = "pageFinal";
            this.pageFinal.Size = new System.Drawing.Size(500, 306);
            this.pageFinal.Style = Sicema.WizardControls.WizardPageStyle.Finish;
            this.pageFinal.TabIndex = 27;
            this.pageFinal.Title = "Proceso Finalizado";
            // 
            // pageProgreso
            // 
            this.pageProgreso.Controls.Add(this.lvwEstado);
            this.pageProgreso.Description = "Por favor, espere mientras se realiza el proceso de actualización de la base de d" +
    "atos en el Servidor SQL con los parámetros especificados.";
            this.pageProgreso.Location = new System.Drawing.Point(0, 0);
            this.pageProgreso.Name = "pageProgreso";
            this.pageProgreso.Size = new System.Drawing.Size(428, 208);
            this.pageProgreso.TabIndex = 25;
            this.pageProgreso.Title = "Actualización de la base de datos";
            // 
            // lvwEstado
            // 
            this.lvwEstado.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvwEstado.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Proceso,
            this.Estado});
            this.lvwEstado.FullRowSelect = true;
            this.lvwEstado.GridLines = true;
            this.lvwEstado.HideSelection = false;
            this.lvwEstado.Location = new System.Drawing.Point(10, 75);
            this.lvwEstado.Name = "lvwEstado";
            this.lvwEstado.Size = new System.Drawing.Size(480, 222);
            this.lvwEstado.SmallImageList = this.imlVerificar;
            this.lvwEstado.TabIndex = 26;
            this.lvwEstado.UseCompatibleStateImageBehavior = false;
            this.lvwEstado.View = System.Windows.Forms.View.Details;
            // 
            // Proceso
            // 
            this.Proceso.Text = "Proceso";
            this.Proceso.Width = 350;
            // 
            // Estado
            // 
            this.Estado.Text = "Estado";
            this.Estado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Estado.Width = 105;
            // 
            // imlVerificar
            // 
            this.imlVerificar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlVerificar.ImageStream")));
            this.imlVerificar.TransparentColor = System.Drawing.Color.Transparent;
            this.imlVerificar.Images.SetKeyName(0, "arrow_right.ico");
            this.imlVerificar.Images.SetKeyName(1, "button_ok.ico");
            this.imlVerificar.Images.SetKeyName(2, "button_cancel.ico");
            // 
            // pageVerificacion
            // 
            this.pageVerificacion.Controls.Add(this.lblEstado);
            this.pageVerificacion.Controls.Add(this.lvwConfig);
            this.pageVerificacion.Description = "Verifique que la configuración seleccionada es correcta antes de iniciar el proce" +
    "so.";
            this.pageVerificacion.Location = new System.Drawing.Point(0, 0);
            this.pageVerificacion.Name = "pageVerificacion";
            this.pageVerificacion.Size = new System.Drawing.Size(428, 208);
            this.pageVerificacion.TabIndex = 23;
            this.pageVerificacion.Title = "Verificación de la configuración";
            // 
            // lblEstado
            // 
            this.lblEstado.AllowDrop = true;
            this.lblEstado.AutoSize = true;
            this.lblEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblEstado.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEstado.Location = new System.Drawing.Point(10, 280);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblEstado.Size = new System.Drawing.Size(40, 13);
            this.lblEstado.TabIndex = 24;
            this.lblEstado.Text = "Estado";
            // 
            // lvwConfig
            // 
            this.lvwConfig.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvwConfig.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Id,
            this.Parametro,
            this.Valor});
            this.lvwConfig.FullRowSelect = true;
            this.lvwConfig.GridLines = true;
            this.lvwConfig.HideSelection = false;
            this.lvwConfig.Location = new System.Drawing.Point(10, 75);
            this.lvwConfig.Name = "lvwConfig";
            this.lvwConfig.Size = new System.Drawing.Size(480, 200);
            this.lvwConfig.SmallImageList = this.imlVerificar;
            this.lvwConfig.TabIndex = 24;
            this.lvwConfig.UseCompatibleStateImageBehavior = false;
            this.lvwConfig.View = System.Windows.Forms.View.Details;
            // 
            // Id
            // 
            this.Id.Text = "No.";
            this.Id.Width = 50;
            // 
            // Parametro
            // 
            this.Parametro.Text = "Parámetro";
            this.Parametro.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Parametro.Width = 100;
            // 
            // Valor
            // 
            this.Valor.Text = "Valor";
            this.Valor.Width = 325;
            // 
            // pageUbicacion
            // 
            this.pageUbicacion.Controls.Add(this.frParametros);
            this.pageUbicacion.Description = "Introduzca la ubicación de los archivos de datos para la actualización.";
            this.pageUbicacion.Location = new System.Drawing.Point(0, 0);
            this.pageUbicacion.Name = "pageUbicacion";
            this.pageUbicacion.Size = new System.Drawing.Size(500, 306);
            this.pageUbicacion.TabIndex = 14;
            this.pageUbicacion.Title = "Ubicación de los archivos de datos";
            // 
            // frParametros
            // 
            this.frParametros.AllowDrop = true;
            this.frParametros.BackColor = System.Drawing.SystemColors.Control;
            this.frParametros.Controls.Add(this.txtCaminoDatos);
            this.frParametros.Controls.Add(this.btnDatos);
            this.frParametros.ForeColor = System.Drawing.SystemColors.ControlText;
            this.frParametros.Location = new System.Drawing.Point(10, 75);
            this.frParametros.Name = "frParametros";
            this.frParametros.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.frParametros.Size = new System.Drawing.Size(480, 66);
            this.frParametros.TabIndex = 15;
            this.frParametros.TabStop = false;
            this.frParametros.Text = "   Ubicación del archivo de actualización";
            // 
            // txtCaminoDatos
            // 
            this.txtCaminoDatos.AcceptsReturn = true;
            this.txtCaminoDatos.AllowDrop = true;
            this.txtCaminoDatos.BackColor = System.Drawing.SystemColors.Window;
            this.txtCaminoDatos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCaminoDatos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCaminoDatos.Location = new System.Drawing.Point(22, 28);
            this.txtCaminoDatos.MaxLength = 0;
            this.txtCaminoDatos.Name = "txtCaminoDatos";
            this.txtCaminoDatos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCaminoDatos.Size = new System.Drawing.Size(356, 20);
            this.txtCaminoDatos.TabIndex = 17;
            this.txtCaminoDatos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // btnDatos
            // 
            this.btnDatos.AllowDrop = true;
            this.btnDatos.BackColor = System.Drawing.SystemColors.Control;
            this.btnDatos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnDatos.Image")));
            this.btnDatos.Location = new System.Drawing.Point(397, 19);
            this.btnDatos.Name = "btnDatos";
            this.btnDatos.Size = new System.Drawing.Size(70, 35);
            this.btnDatos.TabIndex = 20;
            this.btnDatos.Text = " &Datos";
            this.btnDatos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDatos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDatos.UseVisualStyleBackColor = false;
            this.btnDatos.Click += new System.EventHandler(this.btnDatos_Click);
            // 
            // pageConfiguracion
            // 
            this.pageConfiguracion.Controls.Add(this.frConectarse);
            this.pageConfiguracion.Description = "Introduzca los datos necesarios para conectarse al Servidor SQL.";
            this.pageConfiguracion.Location = new System.Drawing.Point(0, 0);
            this.pageConfiguracion.Name = "pageConfiguracion";
            this.pageConfiguracion.Size = new System.Drawing.Size(500, 306);
            this.pageConfiguracion.TabIndex = 5;
            this.pageConfiguracion.Title = "Configuración del Servidor SQL";
            // 
            // frConectarse
            // 
            this.frConectarse.AllowDrop = true;
            this.frConectarse.BackColor = System.Drawing.SystemColors.Control;
            this.frConectarse.Controls.Add(this.optSeguridad1);
            this.frConectarse.Controls.Add(this.optSeguridad0);
            this.frConectarse.Controls.Add(this.btnProbar);
            this.frConectarse.Controls.Add(this.txtContrasenna);
            this.frConectarse.Controls.Add(this.txtUsuario);
            this.frConectarse.Controls.Add(this.cbxCatalogo);
            this.frConectarse.Controls.Add(this.cbxServidor);
            this.frConectarse.Controls.Add(this.btnActualizar);
            this.frConectarse.Controls.Add(this.lblContrasenna);
            this.frConectarse.Controls.Add(this.lblDatabase);
            this.frConectarse.Controls.Add(this.lblSeguridad);
            this.frConectarse.Controls.Add(this.lblUsuario);
            this.frConectarse.Controls.Add(this.lblServidor);
            this.frConectarse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.frConectarse.Location = new System.Drawing.Point(10, 75);
            this.frConectarse.Name = "frConectarse";
            this.frConectarse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.frConectarse.Size = new System.Drawing.Size(480, 217);
            this.frConectarse.TabIndex = 1;
            this.frConectarse.TabStop = false;
            this.frConectarse.Text = "   Información para conectarse al Servidor SQL";
            // 
            // optSeguridad1
            // 
            this.optSeguridad1.AllowDrop = true;
            this.optSeguridad1.BackColor = System.Drawing.SystemColors.Control;
            this.optSeguridad1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optSeguridad1.Location = new System.Drawing.Point(32, 128);
            this.optSeguridad1.Name = "optSeguridad1";
            this.optSeguridad1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optSeguridad1.Size = new System.Drawing.Size(247, 17);
            this.optSeguridad1.TabIndex = 8;
            this.optSeguridad1.Text = "Usar un nombre de usuario y contraseña";
            this.optSeguridad1.UseVisualStyleBackColor = false;
            // 
            // optSeguridad0
            // 
            this.optSeguridad0.AllowDrop = true;
            this.optSeguridad0.BackColor = System.Drawing.SystemColors.Control;
            this.optSeguridad0.Checked = true;
            this.optSeguridad0.ForeColor = System.Drawing.SystemColors.ControlText;
            this.optSeguridad0.Location = new System.Drawing.Point(32, 104);
            this.optSeguridad0.Name = "optSeguridad0";
            this.optSeguridad0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.optSeguridad0.Size = new System.Drawing.Size(247, 17);
            this.optSeguridad0.TabIndex = 7;
            this.optSeguridad0.TabStop = true;
            this.optSeguridad0.Text = "Usar la seguridad integrada de Windows NT";
            this.optSeguridad0.UseVisualStyleBackColor = false;
            this.optSeguridad0.CheckedChanged += new System.EventHandler(this.optSeguridad0_CheckedChanged);
            // 
            // btnProbar
            // 
            this.btnProbar.AllowDrop = true;
            this.btnProbar.BackColor = System.Drawing.SystemColors.Control;
            this.btnProbar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnProbar.Image = ((System.Drawing.Image)(resources.GetObject("btnProbar.Image")));
            this.btnProbar.Location = new System.Drawing.Point(264, 170);
            this.btnProbar.Name = "btnProbar";
            this.btnProbar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnProbar.Size = new System.Drawing.Size(100, 35);
            this.btnProbar.TabIndex = 13;
            this.btnProbar.Text = "&Probar";
            this.btnProbar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnProbar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnProbar.UseVisualStyleBackColor = false;
            this.btnProbar.Click += new System.EventHandler(this.btnProbar_Click);
            // 
            // txtContrasenna
            // 
            this.txtContrasenna.AllowDrop = true;
            this.txtContrasenna.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtContrasenna.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtContrasenna.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtContrasenna.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtContrasenna.Location = new System.Drawing.Point(365, 128);
            this.txtContrasenna.MaxLength = 0;
            this.txtContrasenna.Name = "txtContrasenna";
            this.txtContrasenna.PasswordChar = '*';
            this.txtContrasenna.ReadOnly = true;
            this.txtContrasenna.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtContrasenna.Size = new System.Drawing.Size(97, 20);
            this.txtContrasenna.TabIndex = 10;
            this.txtContrasenna.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // txtUsuario
            // 
            this.txtUsuario.AllowDrop = true;
            this.txtUsuario.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtUsuario.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsuario.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUsuario.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtUsuario.Location = new System.Drawing.Point(365, 104);
            this.txtUsuario.MaxLength = 0;
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.ReadOnly = true;
            this.txtUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUsuario.Size = new System.Drawing.Size(97, 20);
            this.txtUsuario.TabIndex = 9;
            this.txtUsuario.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbxCatalogo
            // 
            this.cbxCatalogo.AllowDrop = true;
            this.cbxCatalogo.BackColor = System.Drawing.SystemColors.Window;
            this.cbxCatalogo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbxCatalogo.Location = new System.Drawing.Point(32, 184);
            this.cbxCatalogo.Name = "cbxCatalogo";
            this.cbxCatalogo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxCatalogo.Size = new System.Drawing.Size(185, 21);
            this.cbxCatalogo.TabIndex = 11;
            this.cbxCatalogo.DropDown += new System.EventHandler(this.cbxCatalogo_DropDown);
            this.cbxCatalogo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // cbxServidor
            // 
            this.cbxServidor.AllowDrop = true;
            this.cbxServidor.BackColor = System.Drawing.SystemColors.Window;
            this.cbxServidor.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbxServidor.Items.AddRange(new object[] {
            "(local)"});
            this.cbxServidor.Location = new System.Drawing.Point(32, 43);
            this.cbxServidor.Name = "cbxServidor";
            this.cbxServidor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxServidor.Size = new System.Drawing.Size(185, 21);
            this.cbxServidor.TabIndex = 6;
            this.cbxServidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // btnActualizar
            // 
            this.btnActualizar.AllowDrop = true;
            this.btnActualizar.BackColor = System.Drawing.SystemColors.Control;
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.Location = new System.Drawing.Point(264, 35);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnActualizar.Size = new System.Drawing.Size(100, 35);
            this.btnActualizar.TabIndex = 12;
            this.btnActualizar.Text = "&Actualizar";
            this.btnActualizar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnActualizar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // lblContrasenna
            // 
            this.lblContrasenna.AllowDrop = true;
            this.lblContrasenna.AutoSize = true;
            this.lblContrasenna.BackColor = System.Drawing.Color.Transparent;
            this.lblContrasenna.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblContrasenna.Location = new System.Drawing.Point(291, 131);
            this.lblContrasenna.Name = "lblContrasenna";
            this.lblContrasenna.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblContrasenna.Size = new System.Drawing.Size(64, 13);
            this.lblContrasenna.TabIndex = 8;
            this.lblContrasenna.Text = "Contraseña:";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AllowDrop = true;
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.BackColor = System.Drawing.Color.Transparent;
            this.lblDatabase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDatabase.Location = new System.Drawing.Point(16, 160);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDatabase.Size = new System.Drawing.Size(210, 13);
            this.lblDatabase.TabIndex = 10;
            this.lblDatabase.Text = "3. Seleccione la base de datos del servidor";
            // 
            // lblSeguridad
            // 
            this.lblSeguridad.AllowDrop = true;
            this.lblSeguridad.AutoSize = true;
            this.lblSeguridad.BackColor = System.Drawing.Color.Transparent;
            this.lblSeguridad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSeguridad.Location = new System.Drawing.Point(16, 80);
            this.lblSeguridad.Name = "lblSeguridad";
            this.lblSeguridad.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSeguridad.Size = new System.Drawing.Size(293, 13);
            this.lblSeguridad.TabIndex = 3;
            this.lblSeguridad.Text = "2. Seleccione el tipo de seguridad de su conexión al servidor";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AllowDrop = true;
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.BackColor = System.Drawing.Color.Transparent;
            this.lblUsuario.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUsuario.Location = new System.Drawing.Point(309, 106);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblUsuario.Size = new System.Drawing.Size(46, 13);
            this.lblUsuario.TabIndex = 6;
            this.lblUsuario.Text = "Usuario:";
            // 
            // lblServidor
            // 
            this.lblServidor.AllowDrop = true;
            this.lblServidor.AutoSize = true;
            this.lblServidor.BackColor = System.Drawing.Color.Transparent;
            this.lblServidor.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblServidor.Location = new System.Drawing.Point(16, 24);
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblServidor.Size = new System.Drawing.Size(226, 13);
            this.lblServidor.TabIndex = 1;
            this.lblServidor.Text = "1. Seleccione o escriba un nombre de servidor";
            // 
            // pageLicencia
            // 
            this.pageLicencia.Controls.Add(this.chkAcuerdo);
            this.pageLicencia.Controls.Add(this.txtLicencia);
            this.pageLicencia.Description = "Por favor lea cuidadosamente este acuerdo de responsabilidad y confirme si está d" +
    "e acuerdo con todos los términos y condiciones.";
            this.pageLicencia.Location = new System.Drawing.Point(0, 0);
            this.pageLicencia.Name = "pageLicencia";
            this.pageLicencia.Size = new System.Drawing.Size(500, 306);
            this.pageLicencia.TabIndex = 2;
            this.pageLicencia.Title = "Acuerdo de Responsabilidad";
            // 
            // chkAcuerdo
            // 
            this.chkAcuerdo.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chkAcuerdo.Location = new System.Drawing.Point(12, 277);
            this.chkAcuerdo.Name = "chkAcuerdo";
            this.chkAcuerdo.Size = new System.Drawing.Size(288, 16);
            this.chkAcuerdo.TabIndex = 3;
            this.chkAcuerdo.Text = "Estoy de acuerdo con todos los términos y condiciones.";
            this.chkAcuerdo.CheckedChanged += new System.EventHandler(this.chkAcuerdo_CheckedChanged);
            // 
            // txtLicencia
            // 
            this.txtLicencia.AcceptsReturn = true;
            this.txtLicencia.BackColor = System.Drawing.SystemColors.Window;
            this.txtLicencia.Location = new System.Drawing.Point(12, 74);
            this.txtLicencia.Multiline = true;
            this.txtLicencia.Name = "txtLicencia";
            this.txtLicencia.ReadOnly = true;
            this.txtLicencia.Size = new System.Drawing.Size(477, 188);
            this.txtLicencia.TabIndex = 4;
            this.txtLicencia.Text = resources.GetString("txtLicencia.Text");
            // 
            // ttMensaje
            // 
            this.ttMensaje.IsBalloon = true;
            // 
            // errIconoError
            // 
            this.errIconoError.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errIconoError.ContainerControl = this;
            // 
            // errIconoInfo
            // 
            this.errIconoInfo.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errIconoInfo.ContainerControl = this;
            this.errIconoInfo.Icon = ((System.Drawing.Icon)(resources.GetObject("errIconoInfo.Icon")));
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 354);
            this.Controls.Add(this.wzdActualizarBD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizar Base de Datos Sicema SQL";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.wzdActualizarBD.ResumeLayout(false);
            this.pageProgreso.ResumeLayout(false);
            this.pageVerificacion.ResumeLayout(false);
            this.pageVerificacion.PerformLayout();
            this.pageUbicacion.ResumeLayout(false);
            this.frParametros.ResumeLayout(false);
            this.frParametros.PerformLayout();
            this.pageConfiguracion.ResumeLayout(false);
            this.frConectarse.ResumeLayout(false);
            this.frConectarse.PerformLayout();
            this.pageLicencia.ResumeLayout(false);
            this.pageLicencia.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errIconoError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errIconoInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Sicema.WizardControls.Wizard wzdActualizarBD;
        private Sicema.WizardControls.WizardPage pageBienvenida;
        private Sicema.WizardControls.WizardPage pageFinal;
        private Sicema.WizardControls.WizardPage pageProgreso;
        private System.Windows.Forms.ListView lvwEstado;
        private System.Windows.Forms.ColumnHeader Proceso;
        private System.Windows.Forms.ColumnHeader Estado;
        private Sicema.WizardControls.WizardPage pageVerificacion;
        public System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.ListView lvwConfig;
        private System.Windows.Forms.ColumnHeader Id;
        private System.Windows.Forms.ColumnHeader Parametro;
        private System.Windows.Forms.ColumnHeader Valor;
        private Sicema.WizardControls.WizardPage pageUbicacion;
        public System.Windows.Forms.GroupBox frParametros;
        public System.Windows.Forms.TextBox txtCaminoDatos;
        private System.Windows.Forms.Button btnDatos;
        private Sicema.WizardControls.WizardPage pageConfiguracion;
        public System.Windows.Forms.GroupBox frConectarse;
        private System.Windows.Forms.RadioButton optSeguridad1;
        private System.Windows.Forms.RadioButton optSeguridad0;
        public System.Windows.Forms.Button btnProbar;
        public System.Windows.Forms.TextBox txtContrasenna;
        public System.Windows.Forms.TextBox txtUsuario;
        public System.Windows.Forms.ComboBox cbxCatalogo;
        public System.Windows.Forms.ComboBox cbxServidor;
        public System.Windows.Forms.Button btnActualizar;
        public System.Windows.Forms.Label lblContrasenna;
        public System.Windows.Forms.Label lblDatabase;
        public System.Windows.Forms.Label lblSeguridad;
        public System.Windows.Forms.Label lblUsuario;
        public System.Windows.Forms.Label lblServidor;
        private Sicema.WizardControls.WizardPage pageLicencia;
        private System.Windows.Forms.CheckBox chkAcuerdo;
        private System.Windows.Forms.TextBox txtLicencia;
        private System.Windows.Forms.ImageList imlVerificar;
        private System.Windows.Forms.ToolTip ttMensaje;
        private System.Windows.Forms.ErrorProvider errIconoError;
        private System.Windows.Forms.ErrorProvider errIconoInfo;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

