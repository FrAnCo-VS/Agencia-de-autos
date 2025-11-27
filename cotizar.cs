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
 
    public partial class cotizar : Form
    {
        string conexionString = "server=localhost; database=TOYOTA; user=root; password=huevos1;";
        private ConexionBD conexion;
        private int? id_Cliente; // Usamos la clave primaria de la tabla Clientes

        // Constructor para Agregar (Nuevo Cliente)
        public cotizar()
        {
            InitializeComponent();
            conexion = new ConexionBD();
            this.Text = "COTIZA - Nuevo Cliente";
        }

        // Constructor para Editar (Cliente Existente)
        // Acepta los 5 campos de la tabla Clientes
        public cotizar(int id, string nombre, string apellido, string telefono, string correo, string direccion)
        {
            InitializeComponent();
            conexion = new ConexionBD();

            id_Cliente = id;
            this.Text = "COTIZA - Editar Cliente";

            // Asignación a los controles de texto (Asegúrate de que los nombres coincidan)
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtTelefono.Text = telefono;
            txtCorreo.Text = correo;
            txtDireccion.Text = direccion;
        }

        // Métodos TextChanged (se dejan vacíos o con la lógica que necesites)
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtApellido_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_TextChanged(object sender, EventArgs e) { }
        private void txtCorreo_TextChanged(object sender, EventArgs e) { }
        private void txtDireccion_TextChanged(object sender, EventArgs e) { }

        private void guardar_Click_1(object sender, EventArgs e)
        {
          // 1. Obtener y sanear todos los campos
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string direccion = txtDireccion.Text.Trim();

            // 2. Validación de campos obligatorios
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(telefono))
            {
                MessageBox.Show("Los campos Nombre, Apellido y Teléfono son obligatorios.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Opcional: Validación básica de formato de correo (puedes expandirla)
            if (!string.IsNullOrEmpty(correo) && !correo.Contains("@"))
            {
                MessageBox.Show("El formato del correo electrónico no es válido.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conexion.AbrirConexion();
                MySqlCommand cmd;

                if (id_Cliente == null)
                {
                    // 3. Consulta SQL para INSERTAR un nuevo Cliente
                    string query = "INSERT INTO Clientes (nombre, apellido, telefono, correo_electronico, direccion) " +
                                   "VALUES (@nombre, @apellido, @telefono, @correo, @direccion)";
                    cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                }
                else
                {
                    // 4. Consulta SQL para ACTUALIZAR un Cliente existente
                    string query = "UPDATE Clientes SET nombre = @nombre, apellido = @apellido, telefono = @telefono, " +
                                   "correo_electronico = @correo, direccion = @direccion WHERE id_cliente = @id";
                    cmd = new MySqlCommand(query, conexion.ObtenerConexion());
                    cmd.Parameters.AddWithValue("@id", id_Cliente);
                }

                // 5. Añadir parámetros para todos los campos
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@direccion", direccion);

                cmd.ExecuteNonQuery();
                conexion.CerrarConexion();
                MessageBox.Show("Datos del cliente guardados correctamente.", "Éxito");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los datos del cliente: " + ex.Message, "Error de Guardado");
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
