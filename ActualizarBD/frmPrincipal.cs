using CapaDatos;
using SevenZip;
using Sicema.WizardControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ActualizarBD
{
    public enum EUpdateType
    {
        IS_VERSION = 0,
        IS_EXPORT = 1,
        IS_SCRIPT = 2,
        IS_UPDATE = 3
    }

    public partial class frmPrincipal : Form
    {
        // Llave del algoritmo de encriptacion
        private const string GEN_KEY = "ATKVV2WMJM4R35499X5BZHP53F00HOHG1PKT9CX1XLNM1RWZ1UNU88X3IHZRDMWBXDI6MWDEPO6SBM96NGGSFE59I1QC1FZFEL1ZV0F8AMCAUYDCKH58H9J5ITZREDXB";
        // Archivo de datos sql
        private const string DATA_FILE = "updatebd.sql";
        // Mensajes de notificacion
        private const string ErrAddListElement = "Error al adicionar elementos a la lista.";
        private const string MsgMissingData = "Faltan algunos datos por ingresar, serán remarcados";
        private const string MsgNeedData = "Ingrese un valor";

        // Directorio temporal
        private string TEMP_DIR = Path.GetTempPath();
        // Version del archivo de actualizacion
        private string fileVer = string.Empty;
        private string fileRev = string.Empty;
        private string fileUpd = string.Empty;

        public frmPrincipal()
        {
            InitializeComponent();

            // Establecer los controles requeridos
            SetIconInfo(cbxServidor);
            SetIconInfo(cbxCatalogo);
            SetIconInfo(txtCaminoDatos);
        }

        /// <summary>
        /// Establecer parametros para un control de entrada obligatoria.
        /// </summary>
        /// <param name="sender">
        /// Control que se establece como requerido.
        /// </param>
        private void SetIconInfo(object sender)
        {
            Control ctrl = (Control)sender;
            errIconoInfo.SetIconAlignment(ctrl, ErrorIconAlignment.MiddleLeft);
            errIconoInfo.SetIconPadding(ctrl, 3);
            errIconoInfo.SetError(ctrl, "Campo requerido");
        }

        /// <summary>
        /// Realiza el evento OnKeyDown en el control especificado 
        /// para moverse al siguiente control con la tecla ENTER.
        /// </summary>
        /// <param name="sender">
        /// Control que recibe el evento
        /// </param>
        /// <param name="e">
        /// Evento que se produce
        /// </param>
        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            Control ctrl = (Control)sender;
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                ctrl.Parent.SelectNextControl(ctrl, true, true, true, true);
                e.Handled = true;
            }
        }

        /// <summary>
        /// Realiza el evento Validating en el control especificado
        /// que realiza la validacion de sus datos.
        /// </summary>
        /// <param name="sender">
        /// Control que recibe el evento
        /// </param>
        /// <param name="e">
        /// Evento que se produce
        /// </param>
        private void Control_Validating(object sender, CancelEventArgs e)
        {

        }

        /// <summary>
        /// Habilitar / Inhabilitar controles de usuario y contraseña.
        /// </summary>
        /// <param name="Activar">
        /// true  -> Se activan los controles
        /// false -> Se inactgivan los controles
        /// </param>
        private void HabilitarControles(bool Activar)
        {
            if (Activar)
            {
                // Habilitar controles de usuario y contraseña
                txtUsuario.ReadOnly = false;
                txtUsuario.BackColor = SystemColors.Window;
                txtContrasenna.ReadOnly = false;
                txtContrasenna.BackColor = SystemColors.Window;
            }
            else
            {
                // Inhabilitar controles de usuario y contraseña
                txtUsuario.ReadOnly = true;
                txtUsuario.BackColor = SystemColors.InactiveBorder;
                txtContrasenna.ReadOnly = true;
                txtContrasenna.BackColor = SystemColors.InactiveBorder;
            }
        }

        /// <summary>
        /// Cargar los datos de configuracion desde un archivo ini.
        /// </summary>
        /// <exception cref="SicemaException">
        /// No se ha podido cargar la configuración.
        /// </exception>
        private void LoadConfigData()
        {
            // Establecer archivo ini de configuracion 
            string file = Application.StartupPath + "\\" + Globals.INI_FILE;

            if (File.Exists(file))
            {
                try
                {
                    // Establecer el algoritmo de encriptacion
                    SymmetricAlgorithm symmetricAlgorithm = Crypto.CreateSymmetricAlgorithm(GEN_KEY);
                    // Llenar los datos de la configuracion a partir del archivo ini
                    ConfigParams config = new ConfigParams(file, symmetricAlgorithm);
                    // Llenar los controles con la configuracion cargada
                    cbxServidor.Text = config.Server;
                    cbxCatalogo.Text = config.Catalog;
                    if (config.Security)
                    {
                        optSeguridad1.Checked = true;
                        txtUsuario.Text = config.User;
                        txtContrasenna.Text = config.Password;
                        // Habilitar controles de usuario y contraseña
                        HabilitarControles(true);
                    }
                    else
                    {
                        optSeguridad0.Checked = true;
                        txtUsuario.Text = "";
                        txtContrasenna.Text = "";
                        // Inhabilitar controles de usuario y contraseña
                        HabilitarControles(false);
                    }
                }
                catch (SicemaException) { throw; }
                catch (Exception ex)
                {
                    throw new SicemaException("No se ha podido cargar la configuración.", ex);
                }
            }
            else
            {
                // No existe el archivo ini, inicializar los controles en blanco
                cbxServidor.Items.Clear();
                cbxServidor.Items.Add("(local)");
                cbxCatalogo.Items.Clear();
                optSeguridad0.Checked = true;
                txtUsuario.Text = "";
                txtContrasenna.Text = "";
                // Inhabilitar controles de usuario y contraseña
                HabilitarControles(false);
            }
        }

        /// <summary>
        /// Llenar el control cbxServidor con los servidores SQL disponibles.
        /// </summary>
        /// <exception cref="SicemaException">
        /// No se ha podido llenar los servidores SQL.
        /// </exception>
        private void FillServers()
        {
            // Establecer el tipo de cursor
            Cursor = Cursors.WaitCursor;
            // Limpiar el control
            cbxServidor.DataSource = null;
            // Establecer la instancia para buscar los servidores
            SqlLocator sqlLoc = new SqlLocator();
            try
            {
                // Llenar lista con los servidores encontrados
                List<string> listServers = sqlLoc.GetSqlServers();
                // Llenar el control con los resultados
                cbxServidor.DataSource = listServers;
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException("No se ha podido llenar los servidores SQL.", ex);
            }
            finally { Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Llenar el control cbxCatalogo con las bases de datos del servidor.
        /// </summary>
        /// <exception cref="SicemaException">
        /// No se ha podido llenar las bases de datos.
        /// </exception>
        private void FillDatabases()
        {
            // Establecer el tipo de cursor
            Cursor = Cursors.WaitCursor;
            // Limpiar el control
            cbxCatalogo.DataSource = null;
            // Establecer la instancia para buscar las bases de datos
            SqlLocator sqlLoc = new SqlLocator();
            // Inicializar lista para las bases de datos encontradas
            List<string> listDBs = new List<string>();
            try
            {
                // Llenar lista con las bases de datos encontradas
                if (optSeguridad0.Checked)  // Seguridad integrada
                    listDBs = sqlLoc.GetDatabases(cbxServidor.Text);
                else  // Seguridad SQL
                    listDBs = sqlLoc.GetDatabases(cbxServidor.Text, true, txtUsuario.Text, txtContrasenna.Text);
                // Llenar el control con los resultados
                cbxCatalogo.DataSource = listDBs;
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException("No se ha podido llenar las bases de datos.", ex);
            }
            finally { Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Obtener los datos de la version del archivo de actualizacion.
        /// </summary>
        /// <param name="Line">
        /// Linea del archivo con los datos de la version.
        /// </param>
        /// <param name="Version">
        /// Cadena que devuelve la version del archivo.
        /// </param>
        /// <param name="Revision">
        /// Cadena que devuelve la revision del archivo.
        /// </param>
        /// <param name="Update">
        /// Cadena que devuelve la actualizacion del archivo.
        /// </param>
        /// <exception cref="SicemaException">
        /// Parametros de entrada incorrectos.
        /// </exception>
        private void FileVersion(string Line, ref string Version, ref string Revision, ref string Update)
        {
            try
            {
                int posVer = Line.IndexOf("Version:");
                int posRev = Line.IndexOf("Revision:");
                int posUpd = Line.IndexOf("Actualizacion:");

                Version = Line.Substring(posVer + 8, posRev - (posVer + 8)).Trim();
                Revision = Line.Substring(posRev + 9, posUpd - (posRev + 9)).Trim();
                Update = Line.Substring(posUpd + 14, Line.Length - (posUpd + 14)).Trim();
            }
            catch (Exception ex)
            {
                throw new SicemaException("La linea de versión en el archivo es incorrecta.", ex);
            }
        }

        /// <summary>
        /// Obtener los datos de la fecha del archivo de actualizacion.
        /// </summary>
        /// <param name="Line">
        /// Linea del archivo con los datos de la fecha.
        /// </param>
        /// <param name="Date">
        /// Cadena que devuelve la fecha de creado el archivo.
        /// </param>
        /// <param name="Days">
        /// Entero que devuelve la cantidad de dias que el archivo es valido.
        /// </param>
        /// <exception cref="SicemaException">
        /// Parametros de entrada incorrectos.
        /// </exception>
        private void ScriptDate(string Line, ref string Date, ref int Days)
        {
            try
            {
                int posDate = Line.IndexOf("Fecha:");
                if (posDate > 0)
                {
                    int posDays = Line.IndexOf("Dias:");
                    if (posDays > 0)
                    {
                        Date = Line.Substring(posDate + 6, posDays - (posDate + 7)).Trim();
                        string tmp = Line.Substring(posDays + 5, Line.Length - (posDays + 5)).Trim();
                        Days = Convert.ToInt32(tmp);
                    }
                    else
                    {
                        Date = Line.Substring(posDate + 6, Line.Length - (posDate + 6)).Trim(); ;
                        Days = 7;
                    }
                }
                else
                {
                    Date = "";
                    Days = 0;
                }
            }
            catch (Exception ex)
            {
                throw new SicemaException("La linea de la fecha en el archivo es incorrecta.", ex);
            }
        }

        /// <summary>
        /// Validar los controles de la pagina Configuracion.
        /// </summary>
        private string CheckConfigData()
        {
            string msgError = String.Empty;
            errIconoError.Clear();

            // Servidor requerido
            if (cbxServidor.Text.Length == 0)
            {
                errIconoError.SetError(cbxServidor, MsgNeedData);
                msgError = MsgMissingData;
            }
            // Catalogo requerido
            if (cbxCatalogo.Text.Length == 0)
            {
                errIconoError.SetError(cbxCatalogo, MsgNeedData);
                msgError = MsgMissingData;
            }
            // Usuario requerido para seguridad SQL
            if ((optSeguridad1.Checked) && (txtUsuario.Text.Length == 0))
            {
                errIconoError.SetError(txtUsuario, MsgNeedData);
                msgError = MsgMissingData;
            }
            // Contraseña requerida para seguridad SQL
            if ((optSeguridad1.Checked) && (txtContrasenna.Text.Length == 0))
            {
                errIconoError.SetError(txtContrasenna, MsgNeedData);
                msgError = MsgMissingData;
            }

            return msgError;
        }

        /// <summary>
        /// Validar los controles de la pagina Ubicacion.
        /// </summary>
        private string CheckLocationsData()
        {
            string msgError = String.Empty;
            errIconoError.Clear();

            // Camino de los datos requerido
            if (txtCaminoDatos.Text.Length == 0)
            {
                errIconoError.SetError(txtCaminoDatos, MsgNeedData);
                msgError = MsgMissingData;
            }

            return msgError;
        }

        /// <summary>
        /// Llenar el ListView de verificacion con la informacion del servidor.
        /// </summary>
        /// <param name="Index">
        /// Indice que le corresponde en el listado de elementos.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al adicionar elementos a la lista.
        /// </exception>
        private bool FillServerInfo(int Index)
        {
            try
            {
                // Adicionar nuevo elemento al listado
                ListViewItem item = lvwConfig.Items.Add(Index.ToString());
                // Nombre del elemento
                item.SubItems.Add("Servidor");
                // Valor del elemento
                item.SubItems.Add(cbxServidor.Text);
                // Chequear restricciones
                if (cbxServidor.Text.Trim().Length > 0)
                {
                    item.ImageIndex = 1;
                    return true;
                }
                else
                {
                    item.ImageIndex = 2;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new SicemaException(ErrAddListElement, ex);
            }
        }

        /// <summary>
        /// Llenar el ListView de verificacion con la informacion de seguridad.
        /// </summary>
        /// <param name="Index">
        /// Indice que le corresponde en el listado de elementos.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al adicionar elementos a la lista.
        /// </exception>
        private bool FillSecurityInfo(int Index)
        {
            try
            {
                // Adicionar nuevo elemento al listado
                ListViewItem item = lvwConfig.Items.Add(Index.ToString());
                // Nombre del elemento
                item.SubItems.Add("Seguridad");
                // Valor del elemento
                if (optSeguridad0.Checked)
                    item.SubItems.Add("Seguridad Integrada de Windows NT");
                else
                    item.SubItems.Add("Seguridad de SQL Server (usuario: " + txtUsuario.Text.Trim() + ")");
                item.ImageIndex = 1;
                return true;
            }
            catch (Exception ex)
            {
                throw new SicemaException(ErrAddListElement, ex);
            }
        }

        /// <summary>
        /// Llenar el ListView de verificacion con la informacion de la base de datos.
        /// </summary>
        /// <param name="Index">
        /// Indice que le corresponde en el listado de elementos.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al adicionar elementos a la lista.
        /// </exception>
        private bool FillDBInfo(int Index)
        {
            try
            {
                // Adicionar nuevo elemento al listado
                ListViewItem item = lvwConfig.Items.Add(Index.ToString());
                // Nombre del elemento
                item.SubItems.Add("Base de Datos");
                // Valor del elemento
                item.SubItems.Add(cbxCatalogo.Text);
                // Chequear restricciones
                if (cbxCatalogo.Text.Trim().Length > 0)
                {
                    if (!cbxCatalogo.Text.Trim().Contains(" "))
                    {
                        item.ImageIndex = 1;
                        return true;
                    }
                    else
                    {
                        item.ImageIndex = 2;
                        return false;
                    }
                }
                else
                {
                    item.ImageIndex = 2;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new SicemaException(ErrAddListElement, ex);
            }
        }

        /// <summary>
        /// Llenar el ListView de verificacion con la informacion del archivo de datos.
        /// </summary>
        /// <param name="Index">
        /// Indice que le corresponde en el listado de elementos.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al adicionar elementos a la lista.
        /// </exception>
        private bool FillDataFileInfo(int Index)
        {
            try
            {
                // Adicionar nuevo elemento al listado
                ListViewItem item = lvwConfig.Items.Add(Index.ToString());
                // Nombre del elemento
                item.SubItems.Add("Archivo");
                // Valor del elemento
                item.SubItems.Add(txtCaminoDatos.Text);
                // Chequear restricciones
                if ((txtCaminoDatos.Text.Trim().Length > 0) && (File.Exists(txtCaminoDatos.Text.Trim())))
                {
                    item.ImageIndex = 1;
                    return true;
                }
                else
                {
                    item.ImageIndex = 2;
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new SicemaException(ErrAddListElement, ex);
            }
        }

        /// <summary>
        /// Llenar el ListView de verificacion con la informacion de los parametros.
        /// </summary>
        private void FillVerificationList()
        {
            bool errConfig = false;
            int i = 0;

            // Limpiar lista de elementos
            lvwConfig.Items.Clear();
            try
            {
                // Informacion del servidor
                errConfig = !FillServerInfo(++i) || errConfig;
                // Informacion de seguridad
                errConfig = !FillSecurityInfo(++i) || errConfig;
                // Informacion de base de datos
                errConfig = !FillDBInfo(++i) || errConfig;
                // Informacion del archivo de datos
                errConfig = !FillDataFileInfo(++i) || errConfig;

                // Chequear errores
                if (errConfig)
                {
                    wzdActualizarBD.NextEnabled = false;
                    lblEstado.Text = "Existen errores en la configuración seleccionada. Revise los parámetros incorrectos.";
                }
                else
                {
                    wzdActualizarBD.NextEnabled = true;
                    lblEstado.Text = "Configuración correcta, presione el botón [Siguiente] para crear la base de datos.";
                }

            }
            catch (SicemaException) { throw; }
        }

        /// <summary>
        /// Version a utilizar para 7z.dll dependiendo 
        /// de la version del sistema operativo.
        /// </summary>
        /// <returns>
        /// Ubicacion de la libreria 7z.dll a utilizar.
        /// </returns>
        private string SevenZipPath()
        {
            if (Environment.Is64BitOperatingSystem)
                // Para 64 bits
                return Application.StartupPath + "\\7z64.dll";
            else
                // Para 32 bits
                return Application.StartupPath + "\\7z32.dll";
        }

        /// <summary>
        /// Extraer la informacion del archivo de datos encriptado.
        /// </summary>
        /// <returns>
        /// Ubicacion del archivo extraido.
        /// </returns>
        /// <exception cref="SicemaException">
        /// Error al extraer el archivo encriptado.
        /// </exception>
        private string ExtractFile(string CryptFile)
        {
            // Contraseña para desencriptar archivo
            const string PASSWORD = "SicemaSQL2020";
            // Mensajes de notificacion
            const string ErrFileDecrypt = "Error al desencriptar el archivo";

            // Establecer archivo de datos sql
            string dataFile = TEMP_DIR + DATA_FILE;
            // Libreria SevenZip a utilizar
            string zipLibrary = SevenZipPath();

            // Verificar que existe el archivo de datos
            if (!File.Exists(CryptFile))
            {
                throw new SicemaException($"No se ha encontrado el archivo de datos\n'{CryptFile}'");
            }
            else
            {
                try
                {
                    // Eliminar si existe un archivo anterior
                    if (File.Exists(dataFile))
                        File.Delete(dataFile);
                    // Inicializar la libreria para extraer el archivo
                    if (File.Exists(zipLibrary))
                    {
                        SevenZipExtractor.SetLibraryPath(zipLibrary);
                        // Extraer archivo encriptado
                        using (SevenZipExtractor sevenZip = new SevenZipExtractor(CryptFile, PASSWORD))
                        {
                            sevenZip.ExtractArchive(TEMP_DIR);
                        }
                    }
                    else
                    {
                        throw new SicemaException(ErrFileDecrypt + $"\n'{CryptFile}'");
                    }
                }
                catch (SevenZipLibraryException ex)
                {
                    throw new SicemaException(ErrFileDecrypt + $"\n'{CryptFile}'", ex);
                }
                catch (Exception ex)
                {
                    throw new SicemaException(ErrFileDecrypt + $"\n'{CryptFile}'", ex);
                }

                // Devolver ubicacion del archivo desencriptado
                return dataFile;
            }
        }

        /// <summary>
        /// Chequear la version del archivo de actualizacion y 
        /// la version de la base de datos.
        /// </summary>
        /// <param name="DB">
        /// Base de datos a actualizar.
        /// </param>
        /// <param name="Line">
        /// Cadena que contiene la version del archivo.
        /// </param>
        /// <returns>
        /// Devuelve una cadena que especifica el resultado de la verificación.
        /// </returns>
        /// <exception cref="SicemaException">
        /// Error al chequear la version del archivo o 
        /// la version de la base de datos.
        /// </exception>
        private string CheckVersion(Database DB, string Line)
        {
            const string errMessage = "La actualización que intenta realizar es anterior a la BD.";
            string retval = string.Empty;
            string dbVer = string.Empty;
            string dbRev = string.Empty;
            string dbUpd = string.Empty;
            ListViewItem item = new ListViewItem();

            // Adicionar nueva tarea
            item = lvwEstado.Items.Add("CheckVersion", "Chequeando versión de la actualización", 0);
            // Estado de la tarea
            item.SubItems.Add("Procesando...");
            // Mostrar la tarea
            item.EnsureVisible();
            try
            {
                // Version de la base de datos
                DB.Version(ref dbVer, ref dbRev, ref dbUpd);
                // Version del archivo de actualizacion
                FileVersion(Line, ref fileVer, ref fileRev, ref fileUpd);
                // Comparar versiones
                if (Convert.ToDecimal(dbVer) > Convert.ToDecimal(fileVer))
                {
                    // La version de BD es superior
                    retval = errMessage;
                }
                else
                {
                    if ((Convert.ToDecimal(dbVer) == Convert.ToDecimal(fileVer)) && 
                        (Convert.ToDecimal(dbRev) > Convert.ToDecimal(fileRev)))
                    {
                        // La version de BD es igual, pero la revision es superior
                        retval = errMessage;
                    }
                    else
                    {
                        if ((Convert.ToDecimal(dbVer) == Convert.ToDecimal(fileVer)) &&
                            (Convert.ToDecimal(dbRev) == Convert.ToDecimal(fileRev)) &&
                            (Convert.ToDecimal(dbUpd) > Convert.ToDecimal(fileUpd)))
                        {
                            // La version de BD y la revision es igual, pero la actualizacion es superior
                            retval = errMessage;
                        }
                        else
                        {
                            if ((Convert.ToDecimal(dbVer) == Convert.ToDecimal(fileVer)) &&
                                (Convert.ToDecimal(dbRev) == Convert.ToDecimal(fileRev)) &&
                                (Convert.ToDecimal(dbUpd) == Convert.ToDecimal(fileUpd)))
                            {
                                // La version de BD, la revision y la actualizacion es igual
                                retval = "Ya se ha realizado esta actualización a la BD.";
                            }
                            else
                            {
                                // Preguntar si se desea realizar la actualizacion
                                string msg = $"Versión de la base de datos: {dbVer} Rev: {dbRev} Update: {dbUpd}\n";
                                msg += $"Actualización a realizar: {fileVer} Rev: {fileRev} Update: {fileUpd}\n";
                                msg += "¿Seguro que desea realizar la actualización?";
                                DialogResult res = MessageBox.Show(msg, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (res == DialogResult.Yes)
                                {
                                    retval = "Version de actualización correcta.";
                                }
                                else
                                {
                                    retval = "Ha decidido cancelar la actualización de la base de datos.";
                                }
                            }
                        }
                    }
                }
            }
            catch (SicemaException)
            {
                if (item != null)
                {
                    // Actualizar estado de la tarea
                    item.SubItems[1].Text = "Detenido";
                    // Actualizar imagen de la tarea
                    item.ImageIndex = 2;
                }
                throw;
            }

            if (retval == "Version de actualización correcta.")
            {
                // Actualizar estado de la tarea
                item.SubItems[1].Text = "Realizado";
                // Actualizar imagen de la tarea
                item.ImageIndex = 1;
            }
            else
            {
                // Actualizar estado de la tarea
                item.SubItems[1].Text = "Detenido";
                // Actualizar imagen de la tarea
                item.ImageIndex = 2;
            }
            lvwEstado.Refresh();
            return retval;
        }

        /// <summary>
        /// Chequear la fecha del archivo de actualizacion.
        /// </summary>
        /// <param name="Line">
        /// Cadena que contiene la fecha del archivo.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error al chequear la fecha del archivo de actualizacion.
        /// </exception>
        private string CheckScriptDate(string Line)
        {
            string retval = string.Empty;
            string date = string.Empty;
            int days = 0;
            ListViewItem item = new ListViewItem();

            // Adicionar nueva tarea
            item = lvwEstado.Items.Add("CheckScriptDate", "Chequeando fecha de la actualización", 0);
            // Estado de la tarea
            item.SubItems.Add("Procesando...");
            // Mostrar la tarea
            item.EnsureVisible();
            try
            {
                // Fecha del archivo
                ScriptDate(Line, ref date, ref days);
                if (date == string.Empty)
                {
                    // Encabezado del archivo incorrecto
                    retval = "El encabezado del archivo no es válido.";
                }
                else
                {
                    TimeSpan duration = new TimeSpan(days, 0, 0, 0);
                    DateTime tmp = DateTime.Parse(date).Add(duration);
                    if (DateTime.Compare(tmp, DateTime.Today) < 0)
                    {
                        // Fecha del archivo incorrecta
                        retval = "La fecha del archivo de actualización es incorrecta.";
                    }
                    else
                    {
                        retval = "La fecha del archivo de actualización es correcta.";
                    }
                }
            }
            catch (SicemaException)
            {
                if (item != null)
                {
                    // Actualizar estado de la tarea
                    item.SubItems[1].Text = "Detenido";
                    // Actualizar imagen de la tarea
                    item.ImageIndex = 2;
                }
                throw;
            }

            if (retval == "La fecha del archivo de actualización es correcta.")
            {
                // Actualizar estado de la tarea
                item.SubItems[1].Text = "Realizado";
                // Actualizar imagen de la tarea
                item.ImageIndex = 1;
            }
            else
            {
                // Actualizar estado de la tarea
                item.SubItems[1].Text = "Detenido";
                // Actualizar imagen de la tarea
                item.ImageIndex = 2;
            }
            lvwEstado.Refresh();
            return retval;
        }

        /// <summary>
        /// Ejecutar la actualizacion de la base de datos.
        /// </summary>
        /// <param name="DB">
        /// Base de datos a actualizar.
        /// </param>
        /// <param name="UpdFile">
        /// Archivo de actualizacion de la base de datos.
        /// </param>
        /// <exception cref="SicemaException">
        /// Error actualizando la base de datos en el servidor.
        /// </exception>
        private void UpdateDB(Database DB, string UpdFile)
        {
            ListViewItem item = new ListViewItem();
            string creatingItem = string.Empty;
            string errorLine = string.Empty;
            string sameLine = string.Empty;
            int intItem = 0;
            string query = string.Empty;
            string line = string.Empty;
            bool started = false;
            bool inTrans = false;
            EUpdateType updType;
            List<Parameter> lstParam = null;

            try
            {
                using (StreamReader sr = new StreamReader(UpdFile, Encoding.Default))
                {
                    // Leer primera linea con encabezado del archivo
                    // Indica que operacion se va a ejecutar
                    if (sr.Peek() >= 0)
                    {
                        line = sr.ReadLine();
                        if (line.Trim().Contains("@@ Version"))  // Actualizar version de BD
                        {
                            updType = EUpdateType.IS_VERSION;
                            string checkVer = CheckVersion(DB, line);
                            if (checkVer != "Version de actualización correcta.")
                            {
                                throw new SicemaException(checkVer);
                            }
                        }
                        else if (line.Trim().Contains("@@ Export_DB") || line.Trim().Contains("@ BD Exportada"))  // Importar datos de una BD exportada
                        {
                            updType = EUpdateType.IS_EXPORT;
                        }
                        else if (line.Trim().Contains("@@ Script"))  // Script de actualizacion por fecha
                        {
                            updType = EUpdateType.IS_SCRIPT;
                            string checkScript = CheckScriptDate(line);
                            if (checkScript != "La fecha del archivo de actualización es correcta.")
                            {
                                throw new SicemaException(checkScript);
                            }
                        }
                        else if (line.Trim().Contains("@@ Update_DB"))  // Script de actualizacion libre
                        {
                            updType = EUpdateType.IS_UPDATE;
                        }
                        else  // No es un archivo valido de actualizacion
                        {
                            throw new SicemaException("El archivo de actualización no es válido.");
                        }
                    }
                    else  // Archivo en blanco
                    {
                        throw new SicemaException("El archivo de actualización está vacio.");
                    }

                    // Crear conexion con el servidor
                    SqlConn sqlConn = new SqlConn(DB.Server, DB.Catalog, DB.Security, DB.User, DB.Password);
                    if (sqlConn.Connect())
                    {
                        // Iniciar una transaction.
                        SqlTransaction sqlTrans = sqlConn.Connection.BeginTransaction();
                        inTrans = true;
                        try
                        {
                            // Leer el archivo de actualizacion
                            // Recorrer el archivo mientras existan datos
                            while (sr.Peek() >= 0)
                            {
                                // Leer una linea
                                line = sr.ReadLine();
                                if ((line.Trim().Length == 0) || (line.Trim().StartsWith("--")) || (line.Trim().StartsWith("/*")))  // Comentario o linea en blanco
                                {
                                    if (started)  // Sentencia sql iniciada
                                    {
                                        // Adicionar linae a la sentencia sql
                                        query += Environment.NewLine + line;
                                    }
                                }
                                else
                                {
                                    if (line.Trim().ToLower() == "go")  // Fin de la sentencia sql
                                    {
                                        if (started)
                                        {
                                            started = false;
                                            if (query.Trim().Length > 0)
                                            {
                                                if ((updType == EUpdateType.IS_EXPORT) &&
                                                    (query.ToUpper().Contains("SET DATEFORMAT")))
                                                {
                                                    // Deshabilitar trigger en sicBitacora
                                                    string disTrigger = "ALTER TABLE sicBitacora DISABLE TRIGGER sicBitacora_triud";
                                                    sqlConn.ExecuteQueryCon(disTrigger, sqlTrans);
                                                    // Deshabilitar trigger en conHistoriD
                                                    disTrigger = "ALTER TABLE conHistoriD DISABLE TRIGGER conHistoriD_triud";
                                                    sqlConn.ExecuteQueryCon(disTrigger, sqlTrans);
                                                    // Deshabilitar trigger en conSubmayor
                                                    disTrigger = "ALTER TABLE conSubmayor DISABLE TRIGGER conSubmayor_triud";
                                                    sqlConn.ExecuteQueryCon(disTrigger, sqlTrans);
                                                    // Deshabilitar trigger en conSubmayEspHist
                                                    disTrigger = "ALTER TABLE conSubmayEspHist DISABLE TRIGGER conSubmayEspHist_triud";
                                                    sqlConn.ExecuteQueryCon(disTrigger, sqlTrans);
                                                    // Ejecutar SQL primera sentencia
                                                    sqlConn.ExecuteQueryCon(query, sqlTrans);
                                                }
                                                else
                                                {
                                                    // Ejecutar SQL
                                                    sqlConn.ExecuteQueryCon(query, sqlTrans, null, 120);
                                                }
                                            }
                                        }
                                        query = string.Empty;
                                    }
                                    else  // Cuerpo de la sentencia sql
                                    {
                                        if (!started)
                                        {
                                            if (updType == EUpdateType.IS_EXPORT)
                                            {
                                                if ((line.ToUpper().Contains("SET DATEFORMAT")) ||
                                                    (line.ToUpper().Contains("SET IDENTITY_INSERT")) ||
                                                    (line.ToUpper().Contains("INSERT INTO")))
                                                {
                                                    if (!sameLine.Equals(line)) intItem++;
                                                }
                                            }
                                            else
                                            {
                                                intItem++;
                                            }
                                            errorLine = line;
                                            if (updType == EUpdateType.IS_EXPORT)
                                            {
                                                if (!sameLine.Equals(line))
                                                {
                                                    if (intItem > 1)
                                                    {
                                                        // Actualizar estado de la tarea
                                                        item.SubItems[1].Text = "Realizado";
                                                        // Actualizar imagen de la tarea
                                                        item.ImageIndex = 1;
                                                        lvwEstado.Refresh();
                                                    }
                                                    creatingItem = "Elemento" + intItem.ToString();
                                                    // Adicionar nueva tarea
                                                    if (line.Length > 100)
                                                    {
                                                        item = lvwEstado.Items.Add(creatingItem, line.Substring(0, 100).Trim(), 0);
                                                    }
                                                    else
                                                    {
                                                        item = lvwEstado.Items.Add(creatingItem, line.Trim(), 0);
                                                    }
                                                    // Estado de la tarea
                                                    item.SubItems.Add("Procesando...");
                                                    // Mostrar la tarea
                                                    item.EnsureVisible();
                                                    lvwEstado.Refresh();
                                                    sameLine = line;
                                                }
                                            }
                                            else
                                            {
                                                if (intItem > 1)
                                                {
                                                    // Actualizar estado de la tarea
                                                    item.SubItems[1].Text = "Realizado";
                                                    // Actualizar imagen de la tarea
                                                    item.ImageIndex = 1;
                                                    lvwEstado.Refresh();
                                                }
                                                creatingItem = "Elemento" + intItem.ToString();
                                                // Adicionar nueva tarea
                                                if (line.Length > 100)
                                                {
                                                    item = lvwEstado.Items.Add(creatingItem, line.Substring(0, 100).Trim(), 0);
                                                }
                                                else
                                                {
                                                    item = lvwEstado.Items.Add(creatingItem, line.Trim(), 0);
                                                }
                                                // Estado de la tarea
                                                item.SubItems.Add("Procesando...");
                                                // Mostrar la tarea
                                                item.EnsureVisible();
                                                lvwEstado.Refresh();
                                            }
                                            started = true;
                                            query = line;
                                        }
                                        else
                                        {
                                            if (query == string.Empty)
                                                query = line;
                                            else
                                                query += Environment.NewLine + line;
                                        }
                                    }
                                }
                            }
                            // Actualizar estado de la tarea
                            item.SubItems[1].Text = "Realizado";
                            // Actualizar imagen de la tarea
                            item.ImageIndex = 1;
                            lvwEstado.Refresh();

                            creatingItem = "ElementoFin";
                            // Adicionar nueva tarea
                            item = lvwEstado.Items.Add(creatingItem, "Finalizando Actualización", 0);
                            // Estado de la tarea
                            item.SubItems.Add("Procesando...");
                            // Mostrar la tarea
                            item.EnsureVisible();
                            lvwEstado.Refresh();
                            if (updType == EUpdateType.IS_EXPORT)
                            {
                                // Habilitar trigger en sicBitacora
                                query = "ALTER TABLE sicBitacora ENABLE TRIGGER sicBitacora_triud";
                                sqlConn.ExecuteQueryCon(query, sqlTrans);
                                // Habilitar trigger en conHistoriD
                                query = "ALTER TABLE conHistoriD ENABLE TRIGGER conHistoriD_triud";
                                sqlConn.ExecuteQueryCon(query, sqlTrans);
                                // Habilitar trigger en conSubmayor
                                query = "ALTER TABLE conSubmayor ENABLE TRIGGER conSubmayor_triud";
                                sqlConn.ExecuteQueryCon(query, sqlTrans);
                                // Habilitar trigger en conSubmayEspHist
                                query = "ALTER TABLE conSubmayEspHist ENABLE TRIGGER conSubmayEspHist_triud";
                                sqlConn.ExecuteQueryCon(query, sqlTrans);
                                // Actualizar tabla sicControl
                                query = "UPDATE sicControl SET CtaResultado1 = '0-0-0-0'";
                                sqlConn.ExecuteQueryCon(query, sqlTrans);
                                // Actualizar estado de la tarea
                                item.SubItems[1].Text = "Realizado";
                                // Actualizar imagen de la tarea
                                item.ImageIndex = 1;
                                lvwEstado.Refresh();
                                // Actualizar TodasCuentas
                                creatingItem = "TodasCuentas";
                                // Adicionar nueva tarea
                                item = lvwEstado.Items.Add(creatingItem, "Actualizando catálogo de cuentas", 0);
                                // Estado de la tarea
                                item.SubItems.Add("Procesando...");
                                // Mostrar la tarea
                                item.EnsureVisible();
                                lvwEstado.Refresh();
                                // Crear la tabla TodasCuentas (catTodasCuentas)
                                lstParam = new List<Parameter>();
                                sqlConn.ExecuteSPCon("cat_CreaTodaCuentas", sqlTrans, ref lstParam, 120);
                                // Marcar las cuentas invalidas
                                lstParam = new List<Parameter>
                                {
                                    new Parameter("@iCuentas", 0),
                                    new Parameter("@Error", "", SqlDbType.Int, ParameterDirection.Output)
                                };
                                sqlConn.ExecuteSPCon("sic_spValidaCuentas", sqlTrans, ref lstParam, 60);
                            }
                            // Si es actualizacion de version escribir nueva
                            // version en la tabla sicActualizar
                            if (updType == EUpdateType.IS_VERSION)
                            {
                                // Definir la consulta de actualizacion
                                query = "INSERT INTO sicActualizar(Version, Revision, Actualizacion)\n";
                                query += "VALUES(@Version, @Revision, @Actualizacion)";
                                // Parametros de la consulta
                                lstParam = new List<Parameter>
                                {
                                    new Parameter("@Version", fileVer),
                                    new Parameter("@Revision", fileRev),
                                    new Parameter("@Actualizacion", fileUpd)
                                };
                                // Realizar la actualizacion
                                sqlConn.ExecuteQueryCon(query, sqlTrans, lstParam);
                            }
                            // Actualizar estado de la tarea
                            item.SubItems[1].Text = "Realizado";
                            // Actualizar imagen de la tarea
                            item.ImageIndex = 1;
                            lvwEstado.Refresh();
                            // Hacer efectiva la transaccion
                            if (inTrans) sqlTrans.Commit();
                            // Desconectar el servidor
                            sqlConn.Disconnect();
                        }
                        catch (SicemaException)
                        {
                            if (item != null)
                            {
                                // Actualizar estado de la tarea
                                item.SubItems[1].Text = "Detenido";
                                // Actualizar imagen de la tarea
                                item.ImageIndex = 2;
                                lvwEstado.Refresh();
                            }
                            if (inTrans) sqlTrans.Rollback();
                            throw;
                        }
                        catch (Exception ex)
                        {
                            if (item != null)
                            {
                                // Actualizar estado de la tarea
                                item.SubItems[1].Text = "Detenido";
                                // Actualizar imagen de la tarea
                                item.ImageIndex = 2;
                                lvwEstado.Refresh();
                            }
                            if (inTrans) sqlTrans.Rollback();
                            throw new SicemaException($"Ha ocurrido un error en la creación del objeto\n{errorLine}", ex);
                        }
                    }
                    else
                    {
                        throw new SicemaException("No se ha podido realizar la conexión al servidor.");
                    }
                }
            }
            catch (SicemaException) { throw; }
            catch (Exception ex)
            {
                throw new SicemaException("Error el acceder al archivo de actualización.", ex);
            }
        }

        /// <summary>
        /// Ejecutar la tarea de creacion de la base de datos.
        /// </summary>
        /// <exception cref="SicemaException">
        /// Error creando la base de datos en el servidor.
        /// </exception>
        private void StartTask()
        {
            // Inhabilitar navegacion entre paginas
            wzdActualizarBD.BackEnabled = false;
            wzdActualizarBD.NextEnabled = false;
            this.Refresh();
            lvwEstado.Items.Clear();

            ListViewItem item = new ListViewItem();
            try
            {
                // Adicionar nueva tarea
                item = lvwEstado.Items.Add("Preparando", "Preparando los datos", 0);
                // Estado de la tarea
                item.SubItems.Add("Procesando...");
                // Mostrar la tarea
                item.EnsureVisible();
                lvwEstado.Refresh();
                // Crear directorio C:\SicemaTemp si no existe
                if (!Directory.Exists(TEMP_DIR))
                    Directory.CreateDirectory(TEMP_DIR);
                // Extraer la informacion del archivo de datos encriptado
                string extFile = ExtractFile(txtCaminoDatos.Text);
                // Actualizar estado de la tarea
                item.SubItems[1].Text = "Realizado";
                // Actualizar imagen de la tarea
                item.ImageIndex = 1;
                lvwEstado.Refresh();

                // Adicionar nueva tarea
                item = lvwEstado.Items.Add("Chequeando", "Chequeando la base de datos", 0);
                // Estado de la tarea
                item.SubItems.Add("Procesando...");
                // Mostrar la tarea
                item.EnsureVisible();
                lvwEstado.Refresh();
                // Chequear si existe la base de datos
                Database db = new Database(cbxServidor.Text, cbxCatalogo.Text, optSeguridad1.Checked,
                                           txtUsuario.Text, txtContrasenna.Text);
                if (db.Exists())
                {
                    // Actualizar estado de la tarea
                    item.SubItems[1].Text = "Realizado";
                    // Actualizar imagen de la tarea
                    item.ImageIndex = 1;
                    lvwEstado.Refresh();
                }
                else
                {
                    // Actualizar estado de la tarea
                    item.SubItems[1].Text = "Detenido";
                    // Actualizar imagen de la tarea
                    item.ImageIndex = 2;
                    lvwEstado.Refresh();
                    // Permitir navegar hacia atras
                    wzdActualizarBD.BackEnabled = true;
                    // Emitir error de base de datos no encontrada
                    throw new SicemaException("No se ha encontrado la base de datos a actualizar.");
                }

                // Actualizar la base de datos
                item = null;
                if (File.Exists(extFile))
                {
                    UpdateDB(db, extFile);
                }
                // Actualizacion realizada correctamente
                MessageBox.Show("Se ha actualizado la base de datos satisfactoriamente.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Navegar a la siguiente pagina
                wzdActualizarBD.Next();
            }
            catch (SicemaException)
            {
                if (item != null)
                {
                    // Actualizar estado de la tarea
                    item.SubItems[1].Text = "Detenido";
                    // Actualizar imagen de la tarea
                    item.ImageIndex = 2;
                }
                wzdActualizarBD.BackEnabled = true;

                throw;
            }
            catch (Exception ex)
            {
                if (item != null)
                {
                    // Actualizar estado de la tarea
                    item.SubItems[1].Text = "Detenido";
                    // Actualizar imagen de la tarea
                    item.ImageIndex = 2;
                }
                wzdActualizarBD.BackEnabled = true;

                throw new SicemaException("No se ha podido realizar la tarea.", ex);
            }
            finally
            {
                // Eliminar archivo de datos si se creo
                string dataFile = TEMP_DIR + DATA_FILE;
                if (File.Exists(dataFile))
                    File.Delete(dataFile);
            }
        }

        private void wzdActualizarBD_Cancel(object sender, CancelEventArgs e)
        {
            // Chequear si la tarea se esta ejecutando
            //bool isTaskRunning = this.timerTask.Enabled;
            // Detener la tarea
            //this.timerTask.Enabled = false;

            if (MessageBox.Show("¿Seguro que desea salir sin actualizar la base de datos?",
                                this.Text,
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                // Reiniciar la tarea
                //this.timerTask.Enabled = isTaskRunning;
            }
        }

        private void wzdActualizarBD_Help(object sender, EventArgs e) =>
            MessageBox.Show("Este asistente permite actualizar una base de datos para Sicema SQL.",
                            this.Text,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

        private void chkAcuerdo_CheckedChanged(object sender, EventArgs e) =>
            // Sincronizar el estado del boton Siguiente con el checkbox
            wzdActualizarBD.NextEnabled = chkAcuerdo.Checked;

        private void wzdActualizarBD_AfterSwitchPages(object sender, Wizard.AfterSwitchPagesEventArgs e)
        {
            // Establecer la nueva pagina a mostrar
            WizardPage newPage = wzdActualizarBD.Pages[e.NewIndex];
            // Chequear si es la pagina Licencia
            if (newPage == pageLicencia)
            {
                // Sincronizar el estado del boton Siguiente con el checkbox
                wzdActualizarBD.NextEnabled = chkAcuerdo.Checked;
            }
            // Chequear si es la pagina Configuracion
            else if (newPage == this.pageConfiguracion)
            {
                // Hacer algo
            }
            // Chequear si es la pagina Ubicacion
            else if (newPage == this.pageUbicacion)
            {
                // Hacer algo
            }
            // Chequear si es la pagina Verificacion
            else if (newPage == this.pageVerificacion)
            {
                // Llenar la lista de verificacion
                try
                {
                    FillVerificationList();
                }
                catch (SicemaException ex)
                {
                    if (ex.InnerException == null)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message,
                                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            // Chequear si es la pagina Progreso
            else if (newPage == this.pageProgreso)
            {
                // Ejecutar la tarea
                try
                {
                    StartTask();
                }
                catch (SicemaException ex)
                {
                    if (ex.InnerException == null)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message,
                                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void wzdActualizarBD_BeforeSwitchPages(object sender, Wizard.BeforeSwitchPagesEventArgs e)
        {
            // Establecer la pagina activa
            WizardPage oldPage = this.wzdActualizarBD.Pages[e.OldIndex];

            // Chequear si nos movemos a la pagina posterior a Configuracion
            if (oldPage == this.pageConfiguracion && e.NewIndex > e.OldIndex)
            {
                // Chequear que se han llenado los datos requeridos
                string msgError = CheckConfigData();
                if (msgError.Length != 0)
                {
                    MessageBox.Show(msgError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    e.Cancel = true;
                }
            }
            // Chequear si nos movemos a la pagina posterior a Ubicacion
            else if (oldPage == this.pageUbicacion && e.NewIndex > e.OldIndex)
            {
                // Chequear que se han llenado los datos requeridos
                string msgError = CheckLocationsData();
                if (msgError.Length != 0)
                {
                    MessageBox.Show(msgError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    e.Cancel = true;
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Llenar el control con el listado de servidores
                FillServers();
                // Pasar el foco al control
                cbxServidor.Focus();
                // Mostrar el listado de servidores
                SendKeys.Send("{F4}");
            }
            catch (SicemaException ex)
            {
                if (ex.InnerException == null)
                {
                    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message,
                                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void cbxCatalogo_DropDown(object sender, EventArgs e)
        {
            if (cbxServidor.Text.Length > 0)
            {
                // Llenar el control con el listado de bases de datos
                try
                {
                    FillDatabases();
                }
                catch (SicemaException ex)
                {
                    if (ex.InnerException == null)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message,
                                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            else
            {
                // Limpiar el control
                cbxCatalogo.Items.Clear();
            }
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            // Chequear los datos en los controles
            string msgError = CheckConfigData();

            if (msgError.Length != 0)  // Emitir mensaje si hay error
            {
                MessageBox.Show(msgError, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else  // Probar conexion con la base de datos
            {
                // Establecer el tipo de cursor
                Cursor = Cursors.WaitCursor;
                // Tratar de realizar la conexion con la base de datos 
                // en el servidor SQL
                try
                {
                    // Establecer los parametros de la conexion
                    SqlConn sqlConn = new SqlConn(cbxServidor.Text, cbxCatalogo.Text, optSeguridad1.Checked, txtUsuario.Text, txtContrasenna.Text);
                    if (sqlConn.CanConnect())  //Conexion satisfactoria
                    {
                        // Restablecer el tipo de cursor
                        Cursor = Cursors.Default;
                        // Emitir mensaje satisfactorio
                        MessageBox.Show("La conexión con la base de datos ha sido satisfactoria.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Restablecer el tipo de cursor
                        Cursor = Cursors.Default;
                        // Emitir mensaje con error
                        MessageBox.Show("No se ha podido realizar la conexión con la base de datos.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (SicemaException ex)
                {
                    Cursor = Cursors.Default;
                    if (ex.InnerException == null)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message,
                                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("No se ha podido realizar la conexión con la de bases de datos.\n" +
                                    "Compruebe los parámetros de la conexión.",
                                    this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            // Establecer el proceso actual
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            // Chequear que sea la misma hora de inicio
            if (processes.Length > 1 && Process.GetCurrentProcess().StartTime != processes[0].StartTime)
            {
                // Solo se permite la aplicacion una vez
                MessageBox.Show("Esta aplicacion solo puede estar cargada una vez.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Salir
                Application.Exit();
            }
            else
            {
                // Adicionar la version al titulo de la ventana
                this.Text = String.Format(this.Text + "{0}v{1}.{2} Rev. {3}", new String(' ', 3), FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMajorPart.ToString(),
                    FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMinorPart.ToString(), FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileBuildPart.ToString().PadLeft(3, '0'));
                // Especificar mensajes de ayuda para los controles
                ttMensaje.SetToolTip(btnActualizar, "Actualizar lista de servidores SQL");
                ttMensaje.SetToolTip(txtUsuario, "Login del usuario con acceso a la base de datos");
                ttMensaje.SetToolTip(txtContrasenna, "Contraseña del usuario con acceso a la base de datos");
                ttMensaje.SetToolTip(btnProbar, "Probar conexión a la base de datos");
                ttMensaje.SetToolTip(btnDatos, "Seleccionar el archivo de actualización");
                // Cargar los datos de la configuracion
                try
                {
                    LoadConfigData();
                }
                catch (SicemaException ex)
                {
                    if (ex.InnerException == null)
                    {
                        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message + Environment.NewLine + ex.InnerException.Message,
                                        this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void optSeguridad0_CheckedChanged(object sender, EventArgs e)
        {
            if (optSeguridad0.Checked)  //Seguridad integrada
            {
                // Limpiar controles de usuario y contraseña
                txtUsuario.Text = "";
                txtContrasenna.Text = "";
                // Inhabilitar controles de usuario y contraseña
                HabilitarControles(false);
                errIconoError.SetError(txtUsuario, "");
                errIconoError.SetError(txtContrasenna, "");
            }
            else  // Seguridad SQL
            {
                // Habilitar controles de usuario y contraseña
                HabilitarControles(true);
                // Pasar el foco a control usuario
                txtUsuario.Focus();
            }
        }

        private void btnDatos_Click(object sender, EventArgs e)
        {
            // Titulo del dialogo
            openFileDialog1.Title = "Seleccione el archivo de actualización";
            // Nombre del archivo en blanco por defecto
            openFileDialog1.FileName = "";
            // Tipos de archivo permitidos
            openFileDialog1.Filter = "Archivo de actualización (*.upd)|*.upd|Todos los archivos(*.*)|*.*";
            // Establecer directorio inicial
            openFileDialog1.InitialDirectory = Application.StartupPath;
            // Establecer el control con el directorio seleccionado
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtCaminoDatos.Text = openFileDialog1.FileName;
        }
    }
}
