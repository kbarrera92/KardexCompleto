using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Farmacia1.DbHelpers
{
    public static class Utils
    {
        public static string ConsultaParametro(string parametro)
        {
            var retValue = string.Empty;
            try
            {
                var appSetting = ConfigurationManager.AppSettings;
                retValue = appSetting[parametro] ?? string.Empty;
            }
            catch (ConfigurationErrorsException e)
            {
                retValue = string.Empty;
                Log.Error($"Ocurrio un error obtener el valor. Error: {e.Message}");
            }

            return retValue;
        }

        public static string AjustaParametro(string strParametro, object objCampo)
        {
            var strReturn = "";
            try
            {
                strReturn = (objCampo == null ? "null" : (Convert.ToString(objCampo) == string.Empty ? null : Convert.ToString(objCampo)));
                return string.Format("{0}|{1}", strParametro, strReturn);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                strReturn = null;
            }
        }

        public static void Minimize(Form frm)
        {
            frm.WindowState = FormWindowState.Minimized;
        }
                                                
        public static string LogDataGridViewContent(DataGridView dataGridView)
        {
            try
            {
                StringBuilder logContent = new StringBuilder();

                // Escribir encabezados de las columnas
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    logContent.Append(column.HeaderText + "\t");
                }
                logContent.AppendLine();

                // Escribir filas
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    if (!row.IsNewRow) // Ignorar la fila vacía al final del DataGridView
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            logContent.Append(cell.Value?.ToString() + "\t");
                        }
                        logContent.AppendLine();
                    }
                }

                return logContent.ToString();

            }
            catch (Exception ex)
            {
                Log.Error($"Ocurrio un error al escribir los detalles. Error: {ex.Message}");
                return string.Empty;
            }
        }

        public static string ConvertDataTableToXmlString(DataTable dataTable)
        {
            try
            {
                if (dataTable == null || dataTable.Rows.Count == 0)
                {
                    throw new ArgumentException("El DataTable está vacío o es nulo.");
                }

                using (StringWriter stringWriter = new StringWriter())
                {
                    // Escribir el DataTable como XML en el StringWriter
                    dataTable.WriteXml(stringWriter, XmlWriteMode.WriteSchema);

                    // Devolver el contenido del StringWriter como un string
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al convertir el DataTable a XML: " + ex.Message);
                return null;
            }
        }
    }
}
