using System.Data.SqlClient;

namespace Login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        SqlConnection conexion = new SqlConnection("server=DESKTOP-SMN0IF8\\SERVER;database=Registro_Usuario;integrated security=true");
        private int contador = 0;
        public void limpiar_campos()
        {
            txtUsuario.Clear();
            txtContraseña.Clear();
            txtUsuario.Focus();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "select * from usuarios where usuario = '"+txtUsuario.Text+"' and contraseña ='"+txtContraseña.Text+"'";
            SqlCommand comando = new SqlCommand(consulta,conexion);
            SqlDataReader lector;
            lector = comando.ExecuteReader();

            if (lector.HasRows == true)
            {
                MessageBox.Show("Bienvenido al sistema...");
                MenuPrincipal menu1 = new Login.MenuPrincipal();
                if (lector.Read())
                {
                    menu1.txtnivel.Text = lector["nivel"].ToString();
                    
                }
                this.Hide();
                menu1.Show();
                    
            }
            else
            {
                contador++;
                limpiar_campos();
                MessageBox.Show("Usuario y contraseña incorrecto..."+ contador);
                
            }

            if (contador == 3)
            {
                MessageBox.Show("Salir del sistema");
                this.Close();
            }
            conexion.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();    
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsuario1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 123 && e.KeyChar <= 255)) {
                MessageBox.Show("Solo letras");
                e.Handled = true;
                return;
            }
        }
    }
}