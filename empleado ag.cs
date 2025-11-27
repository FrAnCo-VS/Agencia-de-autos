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
    public partial class empleado_ag : Form
    {
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";
        private ConexionBD conexion;
        private int? id_Empleado;

        public empleado_ag()
        {
            InitializeComponent();
            conexion = new ConexionBD();
            this.Text = "Agregar Empleado"; // Opcional: Establecer el título de la ventana
        }

        // Constructor para Editar (Empleado Existente)
        public empleado_ag(int id, string nombre, string apellido, string puesto, decimal salario)
        {
            InitializeComponent();
            conexion = new ConexionBD();

            // Asignación de valores
            id_Empleado = id;
            this.Text = "Editar Empleado"; // Opcional: Establecer el título de la ventana

            // Asumiendo nombres de TextBox según la imagen:
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtPuesto.Text = puesto;
            txtSalario.Text = salario.ToString();
        }

        // Métodos TextChanged (se dejan vacíos, pero se actualizan los nombres de los controles)
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtApellido_TextChanged(object sender, EventArgs e) { }
        private void txtPuesto_TextChanged(object sender, EventArgs e) { }
        private void txtSalario_TextChanged(object sender, EventArgs e) { }

        private void guardar_Click(object sender, EventArgs e)
        {
            // 1. Obtener y validar todos los campos de entrada
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string puesto = txtPuesto.Text.Trim();

            // Validación de Salario
            if (!decimal.TryParse(txtSalario.Text, out decimal salario))
            {
                MessageBox.Show("Ingrese un salario válido (valor numérico).", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación de campos de texto
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(puesto))
            {
                MessageBox.Show("Todos los campos (Nombre, Apellido, Puesto) deben ser llenados.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexion.AbrirConexion();
                MySqlCommand cmd;

                if (id_Empleado == null)
                {
                    // 2. Consulta SQL para INSERTAR un nuevo Empleado
                    string query = "INSERT INTO Empleados (nombre, apellido, puesto, salario) VALUES (@nombre, @apellido, @puesto, @salario)";
                    cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                }
                else
                {
                    // 3. Consulta SQL para ACTUALIZAR un Empleado existente
                    string query = "UPDATE Empleados SET nombre = @nombre, apellido = @apellido, puesto = @puesto, salario = @salario WHERE id_empleado = @id";
                    cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                    cmd.Parameters.AddWithValue("@id", id_Empleado);
                }

                // 4. Añadir parámetros para todos los campos
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@puesto", puesto);
                cmd.Parameters.AddWithValue("@salario", salario);

                cmd.ExecuteNonQuery();
                conexion.CerrarConexion();

                MessageBox.Show("Datos del empleado guardados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                conexion.CerrarConexion(); // Asegura cerrar la conexión si hay error
                MessageBox.Show("Error al guardar los datos del empleado: " + ex.Message, "Error de MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

        
    
}
