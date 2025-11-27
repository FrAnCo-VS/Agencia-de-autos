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
    public partial class ventas : Form
    {
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";
        public ventas()
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

        private void CargarDatos() //* Carga datos de ventas desde MySQL *
        {
            try
            {
                // Asegúrate de que 'conexionString' esté definido y sea válido para tu base de datos
                using (MySqlConnection conexion = new MySqlConnection(conexionString)) //* Crea conexión a MySQL *
                {
                    conexion.Open(); //* Abre la conexión *

                    // **CORRECCIÓN: Consulta SQL para la tabla Ventas**
                    // Selecciona las columnas de la tabla Ventas
                    string consulta = "SELECT id_venta, id_cliente, id_vehiculo, fecha_venta, precio_final FROM Ventas";

                    MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion); //* Consulta SQL *
                    DataTable dt = new DataTable(); //* Crea tabla en memoria *
                    da.Fill(dt); //* Llena la tabla con resultados de la consulta *
                    dataGridView1.DataSource = dt; //* Asigna la tabla al DataGridView *

                    // **CORRECCIÓN: Configurar formato de columnas para Ventas**
                    if (dataGridView1.Columns.Count > 0)
                    {
                        // Configuración de encabezados para las columnas de la tabla Ventas
                        dataGridView1.Columns["id_venta"].HeaderText = "ID Venta"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["id_cliente"].HeaderText = "ID Cliente"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["id_vehiculo"].HeaderText = "ID Vehículo"; //* Cambia texto de encabezado *

                        // Configuración de formato para Fecha/Hora
                        dataGridView1.Columns["fecha_venta"].HeaderText = "Fecha de Venta"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["fecha_venta"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"; //* Formato de fecha y hora *

                        // Formato para el campo monetario 'precio_final'
                        dataGridView1.Columns["precio_final"].HeaderText = "Precio Final"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["precio_final"].DefaultCellStyle.Format = "N2"; //* Formato numérico con 2 decimales *
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de ventas: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error); //* Muestra error si falla *
            }
        }


        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            menu formlogin = new menu();
            formlogin.Show();
            this.Close();
        }

        private void BTA_Click(object sender, EventArgs e)
        {

        }
    }
}
