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
    public partial class clientes : Form
    {
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";
        public clientes()
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

        private void CargarDatos() //* Carga datos de clientes desde MySQL *
        {
            try
            {
                // Asegúrate de que 'conexionString' esté definido y sea válido para tu base de datos
                using (MySqlConnection conexion = new MySqlConnection(conexionString)) //* Crea conexión a MySQL *
                {
                    conexion.Open(); //* Abre la conexión *

                    // **CORRECCIÓN: Consulta SQL para la tabla Clientes**
                    // Selecciona todas las columnas de la tabla Clientes
                    string consulta = "SELECT id_cliente, nombre, apellido, telefono, correo_electronico, direccion FROM Clientes";

                    MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion); //* Consulta SQL *
                    DataTable dt = new DataTable(); //* Crea tabla en memoria *
                    da.Fill(dt); //* Llena la tabla con resultados de la consulta *
                    dataGridView1.DataSource = dt; //* Asigna la tabla al DataGridView *

                    // **CORRECCIÓN: Configurar formato de columnas para Clientes**
                    if (dataGridView1.Columns.Count > 0)
                    {
                        // Configuración de encabezados para las columnas de la tabla Clientes
                        dataGridView1.Columns["id_cliente"].HeaderText = "ID Cliente"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["nombre"].HeaderText = "Nombre"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["apellido"].HeaderText = "Apellido"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["telefono"].HeaderText = "Teléfono"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["correo_electronico"].HeaderText = "Email"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["direccion"].HeaderText = "Dirección"; //* Cambia texto de encabezado *
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de clientes: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error); //* Muestra error si falla *
            }
        }


        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            menu formmenu = new menu();
            formmenu.Show();
            this.Close();
        }
    }
}
