using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ConexionMySQL
{
    public partial class Form1 : Form
    {
        Clases.Conexion conexion = new Clases.Conexion();
        public Clases.Usuarios usuarioSelected { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (conexion.abrirConexion() == true)
                {
                    listarUsuarios(conexion.conexion, txtuser.Text, txtnombre.Text);
                    conexion.cerrarConexion();
                }
                else
                {
                    MessageBox.Show("No se ha podido conectar.");
                }
                
            }catch(MySqlException ex){
                MessageBox.Show(ex.Message);
            }
            
        }

        public void listarUsuarios(MySqlConnection conexion,string usuario,string nombre)
        {
            dgvusuarios.DataSource = Clases.Usuarios.buscar(conexion,nombre,usuario);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if(conexion.abrirConexion()==true){
                    Clases.Usuarios pUsuario = new Clases.Usuarios();
                    pUsuario.usuario = txtuser.Text;
                    pUsuario.password = txtpassword.Text;
                    pUsuario.nombre = txtnombre.Text;
                    pUsuario.apellidos = txtapellidos.Text;

                    int resultado;

                    if (string.IsNullOrEmpty(txtid.Text))
                    {
                        resultado = Clases.Usuarios.guardarUsuario(conexion.conexion, pUsuario);
                    }
                    else
                    {
                        pUsuario.id = Convert.ToInt32(txtid.Text);
                        resultado = Clases.Usuarios.editarUsuario(conexion.conexion,pUsuario);
                    }
                    

                    if(resultado > 0){
                        txtuser.Clear();
                        txtpassword.Clear();
                        txtnombre.Clear();
                        txtapellidos.Clear();
                        txtid.Clear();
                        listarUsuarios(conexion.conexion, txtuser.Text, txtnombre.Text);
                    }

                    conexion.cerrarConexion();
                }
            }
            catch(MySqlException ed)
            {
                MessageBox.Show(ed.Message);
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (conexion.abrirConexion() == true)
                {
                    listarUsuarios(conexion.conexion, txtuser.Text, txtnombre.Text);
                    conexion.cerrarConexion();
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvusuarios.SelectedRows.Count == 1)
                {
                    int idUsuario = Convert.ToInt32(dgvusuarios.CurrentRow.Cells[0].Value);
                    if (conexion.abrirConexion() == true)
                    {
                        usuarioSelected = Clases.Usuarios.obtenerUsuario(conexion.conexion,idUsuario);
                        txtid.Text = usuarioSelected.id.ToString();
                        txtuser.Text = usuarioSelected.usuario;
                        txtpassword.Text = usuarioSelected.password;
                        txtnombre.Text = usuarioSelected.nombre;
                        txtapellidos.Text = usuarioSelected.apellidos;
                    }
                    conexion.cerrarConexion();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro");
                }
            }
            catch(MySqlException es)
            {
                MessageBox.Show(es.Message);
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvusuarios.SelectedRows.Count == 1)
                {
                    int idUsuario = Convert.ToInt32(dgvusuarios.CurrentRow.Cells[0].Value);

                    DialogResult confirmDelete = MessageBox.Show("Se eliminará el registro " + idUsuario + ", desea continuar?", "Alerta de eliminación",MessageBoxButtons.YesNo);

                    if (confirmDelete == DialogResult.Yes)
                    {
                        if (conexion.abrirConexion() == true)
                        {
                            int resultado;
                            resultado = Clases.Usuarios.eliminarUsuario(conexion.conexion, idUsuario);

                            if (resultado > 0)
                            {
                                txtuser.Clear();
                                txtpassword.Clear();
                                txtnombre.Clear();
                                txtapellidos.Clear();
                                txtid.Clear();
                                listarUsuarios(conexion.conexion, txtuser.Text, txtnombre.Text);
                                conexion.cerrarConexion();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Eliminación cancelada");
                        txtuser.Clear();
                        txtpassword.Clear();
                        txtnombre.Clear();
                        txtapellidos.Clear();
                        txtid.Clear();
                        listarUsuarios(conexion.conexion, txtuser.Text, txtnombre.Text);
                        conexion.cerrarConexion();
                    }
                    
                    
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro");
                }
            }
            catch (MySqlException es)
            {
                MessageBox.Show(es.Message);
            }
        }
    }
}
