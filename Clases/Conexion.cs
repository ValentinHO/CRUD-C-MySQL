using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConexionMySQL.Clases
{
    public class Conexion
    {
        public MySqlConnection conexion;

        public Conexion()
        {
            //conexion = new MySqlConnection("server=localhost; port=3306; database=csharp; Uid=valentin; pwd=Vho140295_;");
            conexion = new MySqlConnection("server="+Properties.Settings.Default.DBServidor+"; port="+Properties.Settings.Default.DBPuerto+"; database="+Properties.Settings.Default.DBNombre+"; Uid="+Properties.Settings.Default.DBUsuario+"; pwd="+Properties.Settings.Default.DBPassword+";");

        }

        public bool abrirConexion()
        {
            try
            {
                conexion.Open();
                return true;
            }catch(MySqlException e){
                return false;
                throw e;
            }
            
        }

        public bool cerrarConexion()
        {
            try
            {
                conexion.Close();
                return true;
            }
            catch (MySqlException e)
            {
                return false;
                throw e;
            }
        }
    }
}
