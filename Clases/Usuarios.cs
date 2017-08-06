using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConexionMySQL.Clases
{
    public class Usuarios
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }

        public Usuarios()
        {

        }

        public Usuarios(string usuario, string pwd, string nombre, string apellidos)
        {
            this.usuario = usuario;
            this.password = pwd;
            this.nombre = nombre;
            this.apellidos = apellidos;
        }

        public static int guardarUsuario(MySqlConnection conexion, Usuarios pUsuario)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("INSERT INTO usuarios VALUES(null,'{0}','{1}','{2}','{3}')",pUsuario.usuario,pUsuario.password,pUsuario.nombre,pUsuario.apellidos),conexion);
            retorno = comando.ExecuteNonQuery();

            return retorno;
        }

         public static int editarUsuario(MySqlConnection conexion,Usuarios pUsuario)
         {
             int retorno = 0;

             MySqlCommand comando = new MySqlCommand(String.Format("UPDATE usuarios SET usuario='{1}',password='{2}',nombre='{3}',apellidos='{4}' WHERE id={0}", pUsuario.id,pUsuario.usuario, pUsuario.password, pUsuario.nombre, pUsuario.apellidos), conexion);
             retorno = comando.ExecuteNonQuery();

             return retorno;
         }

        public static int eliminarUsuario(MySqlConnection conexion, int idUsuario)
        {
            int retorno = 0;

            MySqlCommand comando = new MySqlCommand(String.Format("DELETE FROM usuarios WHERE id={0}", idUsuario), conexion);
            retorno = comando.ExecuteNonQuery();

            return retorno;
        }

        public static IList<Usuarios> buscar(MySqlConnection conexion, string pnombre, string usuario)
        {
            List<Usuarios> lista = new List<Usuarios>();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id,usuario,password,nombre,apellidos FROM usuarios WHERE usuario like ('%{0}%') AND nombre like ('%{1}%')",usuario,pnombre),conexion);
            MySqlDataReader reader = comando.ExecuteReader();

            while(reader.Read()){
                Usuarios pUsuario = new Usuarios();
                pUsuario.id = reader.GetInt32(0);
                pUsuario.usuario = reader.GetString(1);
                pUsuario.password = reader.GetString(2);
                pUsuario.nombre = reader.GetString(3);
                pUsuario.apellidos = reader.GetString(4);

                lista.Add(pUsuario);
            }

            return lista;
        }

        public static Usuarios obtenerUsuario(MySqlConnection conexion,int idUser)
        {
            Usuarios pUsuario = new Usuarios();
            MySqlCommand comando = new MySqlCommand(String.Format("SELECT id,usuario,password,nombre,apellidos FROM usuarios WHERE id = '{0}'", idUser), conexion);
            MySqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                pUsuario.id = reader.GetInt32(0);
                pUsuario.usuario = reader.GetString(1);
                pUsuario.password = reader.GetString(2);
                pUsuario.nombre = reader.GetString(3);
                pUsuario.apellidos = reader.GetString(4);
            }
            return pUsuario;
        }
    }
}
