using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Agencia
{
    public partial class empleados : Form
    {
        private readonly ConexionBD conexionDB = new ConexionBD();
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";

        // Variables de impresión
        private PrintDocument printDocument1 = new PrintDocument();
        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        private int currentPrintRow;

        public empleados()
        {
            InitializeComponent();
            ConfigurarDataGridView();
            CargarDatos();
        }

        // --- Configuración y Carga ---
        private void ConfigurarDataGridView()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void CargarDatos()
        {
            try
            {
                using (MySqlConnection conexion = new MySqlConnection(conexionString))
                {
                    conexion.Open();
                    string consulta = "SELECT id_empleado, nombre, apellido, puesto, salario FROM Empleados";
                    MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns.Count > 0)
                    {
                        dataGridView1.Columns["id_empleado"].HeaderText = "ID Empleado";
                        dataGridView1.Columns["nombre"].HeaderText = "Nombre";
                        dataGridView1.Columns["apellido"].HeaderText = "Apellido";
                        dataGridView1.Columns["puesto"].HeaderText = "Puesto";
                        dataGridView1.Columns["salario"].HeaderText = "Salario";
                        dataGridView1.Columns["salario"].DefaultCellStyle.Format = "N2";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de empleados: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- CRUD ---

        private void BTA_Click(object sender, EventArgs e) // Botón Agregar (Nuevo)
        {
            // Usamos el constructor vacío
            empleado_ag formulario = new empleado_ag();
            formulario.ShowDialog();
            CargarDatos();
        }

        private void BTNE_Click(object sender, EventArgs e) // Botón Editar
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dataGridView1.SelectedRows[0];

                    int id_empleado = Convert.ToInt32(row.Cells["id_empleado"].Value);
                    string nombre = row.Cells["nombre"].Value.ToString();
                    string apellido = row.Cells["apellido"].Value.ToString();
                    string puesto = row.Cells["puesto"].Value.ToString();
                    decimal salario = Convert.ToDecimal(row.Cells["salario"].Value);

                    // Usamos el constructor con parámetros para la edición
                    empleado_ag formulario = new empleado_ag(
                        id_empleado, nombre, apellido, puesto, salario);

                    formulario.ShowDialog();
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al editar: {ex.Message}", "Error de Edición", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BTNEL_Click(object sender, EventArgs e) // Botón Eliminar
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id_empleado = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_empleado"].Value);

                DialogResult result = MessageBox.Show("¿Está seguro de eliminar el empleado seleccionado?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        conexionDB.AbrirConexion();
                        string query = "DELETE FROM Empleados WHERE id_empleado = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conexionDB.ObtenerConexion());
                        cmd.Parameters.AddWithValue("@id", id_empleado);
                        cmd.ExecuteNonQuery();
                        conexionDB.CerrarConexion();

                        CargarDatos();
                        MessageBox.Show("Empleado eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        conexionDB.CerrarConexion();
                        MessageBox.Show("Error al eliminar el empleado: " + ex.Message, "Error de MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un empleado para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // --- IMPRIMIR (BTNI_Click) ---

        private void BTNI_Click(object sender, EventArgs e) // Botón Imprimir
        {
            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay datos para imprimir.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            printDocument1.PrintPage -= printDocument1_PrintPage;
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.DefaultPageSettings.Landscape = true;

            printPreviewDialog1.Document = printDocument1;
            ((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;

            currentPrintRow = 0;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 10, FontStyle.Regular);
            Font headerFont = new Font("Arial", 12, FontStyle.Bold);
            int lineHeight = 20;
            int x = e.MarginBounds.Left;
            int y = e.MarginBounds.Top;
            int width = e.MarginBounds.Width;

            string title = "Reporte de Empleados - Agencia TOYOTA";
            e.Graphics.DrawString(title, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, x, y);
            y += 40;

            int numColumns = dataGridView1.Columns.Count;
            int columnWidth = width / numColumns;

            int currentX = x;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, currentX, y);
                currentX += columnWidth;
            }
            y += lineHeight;

            e.Graphics.DrawLine(Pens.Black, x, y, x + width, y);
            y += 5;

            bool morePages = false;
            while (currentPrintRow < dataGridView1.Rows.Count)
            {
                if (y + lineHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    morePages = true;
                    break;
                }

                DataGridViewRow row = dataGridView1.Rows[currentPrintRow];

                if (!row.IsNewRow)
                {
                    currentX = x;
                    for (int i = 0; i < numColumns; i++)
                    {
                        string cellValue = row.Cells[i].Value != null ? row.Cells[i].Value.ToString() : "";
                        e.Graphics.DrawString(cellValue, font, Brushes.Black, currentX, y);
                        currentX += columnWidth;
                    }
                    y += lineHeight;
                }
                currentPrintRow++;
            }

            if (!morePages)
            {
                e.HasMorePages = false;
            }
        }

        // --- CERRAR ---

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
          
            menu formmenu = new menu();
            formmenu.Show();
            this.Close();
        }
    }
}