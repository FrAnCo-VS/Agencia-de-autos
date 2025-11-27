using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Agencia
{
    public partial class vehiculos : Form
    {
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";

        public vehiculos()
        {
            InitializeComponent();
            CargarDatos();//* Carga los datos de utensilios al iniciar *
            ConfigurarDataGridView();//* Configura el aspecto del DataGridView *
        }
        private void ConfigurarDataGridView()//* Configura las propiedades visuales del DataGridView *
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;//* Selección por fila completa *
            dataGridView1.MultiSelect = false;//* No permite selección múltiple *
            dataGridView1.ReadOnly = true;//* Solo lectura *
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Ajustar tamaño automáticamente
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;//* Centra encabezados *
        }
        private void CargarDatos() //* Carga datos de vehículos desde MySQL *
        {
            try
            {
                // Asegúrate de que 'conexionString' esté definido y sea válido para tu base de datos
                using (MySqlConnection conexion = new MySqlConnection(conexionString)) //* Crea conexión a MySQL *
                {
                    conexion.Open(); //* Abre la conexión *

                    // **CORRECCIÓN 1: Consulta SQL para la tabla Vehiculos**
                    // Selecciona las columnas de la tabla Vehiculos
                    string consulta = "SELECT id_vehiculo, modelo, año, color, precio, estado FROM Vehiculos";

                    MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion); //* Consulta SQL *
                    DataTable dt = new DataTable(); //* Crea tabla en memoria *
                    da.Fill(dt); //* Llena la tabla con resultados de la consulta *
                    dataGridView1.DataSource = dt; //* Asigna la tabla al DataGridView *

                    // **CORRECCIÓN 2: Configurar formato de columnas para Vehiculos**
                    if (dataGridView1.Columns.Count > 0)
                    {
                        // Configuración de encabezados para las columnas de la tabla Vehiculos
                        dataGridView1.Columns["id_vehiculo"].HeaderText = "ID Vehículo"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["modelo"].HeaderText = "Modelo"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["año"].HeaderText = "Año"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["color"].HeaderText = "Color"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["precio"].HeaderText = "Precio"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["precio"].DefaultCellStyle.Format = "N2";
                        dataGridView1.Columns["estado"].HeaderText = "Estado"; //* Cambia texto de encabezado *
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de vehículos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error); //* Muestra error si falla *
            }
        }

        private void BTA_Click(object sender, EventArgs e)
        {

        }

        private void BTNE_Click(object sender, EventArgs e)
        {

        }

        private void BTNEL_Click(object sender, EventArgs e)
        {

        }

        private void BTNI_Click(object sender, EventArgs e)
        {

        }
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            menu formmenu = new menu();
            formmenu.Show();
            this.Close();
        }
    }
}
