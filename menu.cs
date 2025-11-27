using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;


namespace Agencia
{
    public partial class menu : Form
    {
        public menu()
        {
            InitializeComponent();
        }

        private void BTE_Click(object sender, EventArgs e)
        {
            empleados form1 = new empleados();  //* Crea instancia del formulario *
            this.Controls.Clear();      //* Limpia controles actuales *
            form1.TopLevel = false;     //* Indica que no es formulario de nivel superior *
            form1.FormBorderStyle = FormBorderStyle.None;  //* Sin bordes *
            form1.Dock = DockStyle.Fill;  //* Ocupa todo el espacio disponible *
            this.Controls.Add(form1);    //* Agrega el formulario como control *
            form1.Show();
        }

        private void BTV_Click(object sender, EventArgs e)
        {
            ventas form2 = new ventas();  //* Crea instancia del formulario *
            this.Controls.Clear();      //* Limpia controles actuales *
            form2.TopLevel = false;     //* Indica que no es formulario de nivel superior *
            form2.FormBorderStyle = FormBorderStyle.None;  //* Sin bordes *
            form2.Dock = DockStyle.Fill;  //* Ocupa todo el espacio disponible *
            this.Controls.Add(form2);    //* Agrega el formulario como control *
            form2.Show();
        }

        private void BTS_Click(object sender, EventArgs e)
        {
            servicios form3 = new servicios();  //* Crea instancia del formulario *
            this.Controls.Clear();      //* Limpia controles actuales *
            form3.TopLevel = false;     //* Indica que no es formulario de nivel superior *
            form3.FormBorderStyle = FormBorderStyle.None;  //* Sin bordes *
            form3.Dock = DockStyle.Fill;  //* Ocupa todo el espacio disponible *
            this.Controls.Add(form3);    //* Agrega el formulario como control *
            form3.Show();
        }

        private void BTVH_Click(object sender, EventArgs e)
        {
            vehiculos form4 = new vehiculos();  //* Crea instancia del formulario *
            this.Controls.Clear();      //* Limpia controles actuales *
            form4.TopLevel = false;     //* Indica que no es formulario de nivel superior *
            form4.FormBorderStyle = FormBorderStyle.None;  //* Sin bordes *
            form4.Dock = DockStyle.Fill;  //* Ocupa todo el espacio disponible *
            this.Controls.Add(form4);    //* Agrega el formulario como control *
            form4.Show();
        }

        private void BTC_Click(object sender, EventArgs e)
        {
            clientes form5 = new clientes();  //* Crea instancia del formulario *
            this.Controls.Clear();      //* Limpia controles actuales *
            form5.TopLevel = false;     //* Indica que no es formulario de nivel superior *
            form5.FormBorderStyle = FormBorderStyle.None;  //* Sin bordes *
            form5.Dock = DockStyle.Fill;  //* Ocupa todo el espacio disponible *
            this.Controls.Add(form5);    //* Agrega el formulario como control *
            form5.Show();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            login formlogin = new login();
            formlogin.Show();
        }
    }
}
