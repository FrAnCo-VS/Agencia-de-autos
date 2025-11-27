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
    public partial class cliente_s : Form
    {
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";
        public cliente_s()
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

        private void CargarDatos() //* Carga datos de servicios desde MySQL *
        {
            try
            {
                // Asegúrate de que 'conexionString' esté definido y sea válido para tu base de datos
                using (MySqlConnection conexion = new MySqlConnection(conexionString)) //* Crea conexión a MySQL *
                {
                    conexion.Open(); //* Abre la conexión *

                    // **CORRECCIÓN: Consulta SQL para la tabla Servicios**
                    // Selecciona las columnas de la tabla Servicios
                    string consulta = "SELECT id_servicio, id_vehiculo, fecha_servicio, tipo_servicio, costo FROM Servicios";

                    MySqlDataAdapter da = new MySqlDataAdapter(consulta, conexion); //* Consulta SQL *
                    DataTable dt = new DataTable(); //* Crea tabla en memoria *
                    da.Fill(dt); //* Llena la tabla con resultados de la consulta *
                    dataGridView1.DataSource = dt; //* Asigna la tabla al DataGridView *

                    // **CORRECCIÓN: Configurar formato de columnas para Servicios**
                    if (dataGridView1.Columns.Count > 0)
                    {
                        // Configuración de encabezados para las columnas de la tabla Servicios
                        dataGridView1.Columns["id_servicio"].HeaderText = "ID Servicio"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["id_vehiculo"].HeaderText = "ID Vehículo"; //* Cambia texto de encabezado (llave foránea) *
                        dataGridView1.Columns["fecha_servicio"].HeaderText = "Fecha"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["tipo_servicio"].HeaderText = "Tipo de Servicio"; //* Cambia texto de encabezado *

                        // Formato para el campo monetario 'costo'
                        dataGridView1.Columns["costo"].HeaderText = "Costo"; //* Cambia texto de encabezado *
                        dataGridView1.Columns["costo"].DefaultCellStyle.Format = "N2"; //* Formato numérico con 2 decimales *
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos de servicios: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error); //* Muestra error si falla *
            }
        }

        private void BTNI_Click(object sender, EventArgs e)
        {
            cotizar formcotizar = new cotizar();
            formcotizar.Show();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Formcliente formcliente = new Formcliente();
            formcliente.Show();
            this.Hide();
        }
    }
    
}
