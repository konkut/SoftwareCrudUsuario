using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Login
{
    public partial class Usuarios : Form
    {
        public Usuarios()
        {
            InitializeComponent();
        }
        SqlConnection conexion = new SqlConnection("server=DESKTOP-SMN0IF8\\SERVER;database=Registro_Usuario;integrated security=true");
        public void actualizar_tabla()
        {
            if(txtNivelActual.Text == "1")
            {
                string consulta = "select * from usuarios";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            else
            {
                string consulta = "select cedula_identidad, nombre, apellido, celular from usuarios";
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            
        }
        public void limpiar_campos()
        {
            txtId.Clear();
            txtCi.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtCelular.Clear();
            txtNivel.Clear();
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtId.Focus();
        }
        

        private void btnVolver_Click(object sender, EventArgs e)
        {
            MenuPrincipal menu1 = new Login.MenuPrincipal();
            this.Hide();
            menu1.Show();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            if ((txtCi.Text != "") &&
                (txtNombre.Text != "") && (txtApellidos.Text != "") &&
                (txtCelular.Text != "") && (txtNivel.Text != "") &&
                (txtUsuario.Text != "") && (txtContraseña.Text != ""))
            {
                //Para Agregar registro en el boton nuevo
                conexion.Open();
                string consulta = "insert into usuarios values('" + txtCi.Text + "','" + txtNombre.Text + "','" + txtApellidos.Text +
                 "','" + txtCelular.Text + "'," + txtNivel.Text + ",'" + txtUsuario.Text + "','" + txtContraseña.Text + "');";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro de usuario agregado a la Base de Datos");
                actualizar_tabla();
                limpiar_campos();
                conexion.Close();
            }
            else
            {
                //mensaje de error al no llenar datos correctamente
                MessageBox.Show("FALTAN CAMPOS POR LLENAR ", "Mensaje de ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if (txtId.Text != "")
            {
                string consulta = "delete from usuarios where id_usuario=" + txtId.Text;
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.ExecuteNonQuery();
                MessageBox.Show("usuario " + txtNombre.Text + "elminado con exito");

                actualizar_tabla();
                limpiar_campos();
                conexion.Close();
            }
            else
            {
                MessageBox.Show("DAR EL CODIGO DEL USUARIO QUE DESEA ELIMINAR ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                conexion.Close();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            conexion.Open();
            if ((txtCi.Text != "") &&
                 (txtNombre.Text != "") && (txtApellidos.Text != "") &&
                 (txtCelular.Text != "") && (txtNivel.Text != "") &&
                 (txtUsuario.Text != "") && (txtContraseña.Text != ""))
            {
                string consulta = "UPDATE usuarios SET cedula_identidad ='" + txtCi.Text + "',nombre ='" + txtNombre.Text +
                "',apellido='" + txtApellidos.Text + "',celular='" + txtCelular.Text + "',nivel=" + txtNivel.Text +
                ",usuario='" + txtUsuario.Text + "',contraseña='" + txtContraseña + "' where id_usuario=" + txtId.Text + ";";

                SqlCommand comando = new SqlCommand(consulta, conexion);
                int contador;
                contador = comando.ExecuteNonQuery();
                if (contador > 0)
                {
                    MessageBox.Show("Registro del usuario : " + txtNombre.Text + " modificado en la Base de  Datos");
                }
                else
                {
                    MessageBox.Show("NO PODEMOS MODIFICAR PORQUE EL DATO NO EXISTE");
                }
                actualizar_tabla();
                limpiar_campos();
            }
            else
            {
                MessageBox.Show("FALTAN CAMPOS PARA MODIFICAR ", "Mensaje de ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtBuscar.Text != "")
            {
                conexion.Open();
                string consulta1 = "select  * from usuarios where nombre='" + txtBuscar.Text + "';";

                SqlCommand comando1 = new SqlCommand(consulta1, conexion);
                SqlDataReader lector1;
                lector1 = comando1.ExecuteReader();
                if (lector1.Read())
                {
                    txtUsuario.Text = lector1["id_usuario"].ToString();
                    txtCi.Text = lector1["cedula_identidad"].ToString();
                    txtNombre.Text = lector1["nombre"].ToString();
                    txtApellidos.Text = lector1["apellido"].ToString();
                    txtCelular.Text = lector1["celular"].ToString();
                    txtNivel.Text = lector1["nivel"].ToString();
                    txtUsuario.Text = lector1["usuario"].ToString();
                    txtContraseña.Text = lector1["contraseña"].ToString();
                }
                else
                {
                    MessageBox.Show(" NO EXISTE DATO ", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                conexion.Close();
                actualizar_tabla();
            } 
        }

        private void Usuarios_BackgroundImageLayoutChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar_campos();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void DGV2(object sender, DataGridViewCellEventArgs e)
        {
            if (txtNivelActual.Text == "1")
            {
                txtId.Text = dataGridView2.SelectedCells[0].Value.ToString();
                txtCi.Text = dataGridView2.SelectedCells[1].Value.ToString();
                txtNombre.Text = dataGridView2.SelectedCells[2].Value.ToString();
                txtApellidos.Text = dataGridView2.SelectedCells[3].Value.ToString();
                txtCelular.Text = dataGridView2.SelectedCells[4].Value.ToString();
                txtNivel.Text = dataGridView2.SelectedCells[5].Value.ToString();
                txtUsuario.Text = dataGridView2.SelectedCells[6].Value.ToString();
                txtContraseña.Text = dataGridView2.SelectedCells[7].Value.ToString();
            }
            else {
                txtId.Text = dataGridView2.SelectedCells[0].Value.ToString();
                txtCi.Text = dataGridView2.SelectedCells[1].Value.ToString();
                txtNombre.Text = dataGridView2.SelectedCells[2].Value.ToString();
                txtApellidos.Text = dataGridView2.SelectedCells[3].Value.ToString();
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("DAR DATOS NUMERICOS PORFAVOR", "DATO NO VALIDO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("DATOS: 1.- Admistrador 2.- Usuario", "AYUDA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNivel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("DAR DATOS NUMERICOS PORFAVOR", "DATO NO VALIDO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("DATOS: 1.- Admistrador 2.- Usuario", "AYUDA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            if (txtNivelActual.Text == "1")
            {
                actualizar_tabla();
                btnNuevo.Enabled = true;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnLimpiar.Enabled = true;
            }
            if (txtNivelActual.Text == "2")
            {
                actualizar_tabla();
                btnNuevo.Enabled = false;
                btnModificar.Enabled = false;
                btnEliminar.Enabled = false;
                btnLimpiar.Enabled = false;
            }
            
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
      
        }
    }
}
