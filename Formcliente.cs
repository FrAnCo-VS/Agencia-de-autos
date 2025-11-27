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
    public partial class Formcliente : Form
    {
        public Formcliente()
        {
            InitializeComponent();
        }

        private void BTS_Click(object sender, EventArgs e)
        {

            cliente_s formcotizar = new cliente_s();
            formcotizar.Show();
        }

        private void BTVH_Click(object sender, EventArgs e)
        {
            ag_v formagv = new ag_v();
            formagv.Show();
            this.Hide();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            login formlogin = new login();
            formlogin.Show();
            this.Hide();    
        }
    }
}
