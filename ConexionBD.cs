using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Connection;

namespace Agencia
{
    internal class ConexionBD
    {
        private MySqlConnection conexion;
        private string cadenaConexion = "server=localhost; database=TOYOTA; user=root; password=huevos1;";

        public ConexionBD()
        {
            conexion = new MySqlConnection(cadenaConexion);
        }

        public MySqlConnection AbrirConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Closed)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
            return conexion;
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }
        public MySqlConnection ObtenerConexion()
        {
            return conexion;
        }
    }
}
