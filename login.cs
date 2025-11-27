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
    public partial class login : Form
    {
        // Usaremos una clase simple para almacenar el nombre de usuario, la contraseña y el rol.
        private class UserCredentials
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; } // "Admin" o "Cliente"
        }

        // Usamos un List<UserCredentials> para almacenar los datos
        private List<UserCredentials> users = new List<UserCredentials>
    {
        // Administradores (Irán al formulario 'menu')
        new UserCredentials { Username = "david", Password = "robles", Role = "Admin" },
        new UserCredentials { Username = "fabrizio", Password = "stevenson", Role = "Admin" },
        new UserCredentials { Username = "Farid", Password = "castro", Role = "Admin" },
        new UserCredentials { Username = "jhan", Password = "villarroel", Role = "Admin" },

        // Cliente (Irá a otro formulario, por ejemplo, 'FormCliente')
        new UserCredentials { Username = "jose", Password = "mamani", Role = "Cliente" }
    };

        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Lógica de carga del formulario si es necesaria
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {

            // Obtener el usuario y la contraseña ingresados
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Buscar el usuario en la lista
            UserCredentials authenticatedUser = users.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password.Equals(password)
            );

            if (authenticatedUser != null)
            {
                this.Hide(); // Oculta el formulario de login

                if (authenticatedUser.Role == "Admin")
                {
                    // Si es administrador, abre el formulario 'menu'
                    menu formAdmin = new menu();
                    formAdmin.Show();
                }
                else if (authenticatedUser.Role == "Cliente")
                {
                    Formcliente formCliente = new Formcliente();
                     formCliente.Show(); 
                    MessageBox.Show("¡Bienvenido cliente! Serás dirigido a tu formulario.");
                }
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Evento no utilizado
        }

        private void BtnCerrar_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
