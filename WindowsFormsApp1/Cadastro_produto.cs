using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Cadastrar_Produto : Form
    {
        public Cadastrar_Produto()
        {
            InitializeComponent();
        }

        string imagem;

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection sqlcon = null;

        private string strcon = @"Data Source=.\sqlexpress;Initial Catalog=trabalho;User ID=diego;Password=diego123; MultipleActiveResultSets=true";

        private string strsql = string.Empty;



        private void button2_Click(object sender, EventArgs e)
        {
            byte[] imagem_byte = null;

            FileStream fs = new FileStream(imagem, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imagem_byte = br.ReadBytes((int)fs.Length);

            strsql = ("INSERT INTO produtos(idprod,nome,preco,quant,fotos) VALUES (@idprod,@nome,@preco,@quant,@fotos)") ;

            sqlcon = new SqlConnection(strcon);

            SqlCommand comando = new SqlCommand(strsql, sqlcon);






            comando.Parameters.Add("@idprod", SqlDbType.VarChar).Value = textBox1.Text;
            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = textBox2.Text;
            comando.Parameters.Add("@preco", SqlDbType.VarChar).Value = textBox4.Text;
            comando.Parameters.Add("@quant", SqlDbType.VarChar).Value = textBox3.Text;

            comando.Parameters.Add(new SqlParameter("@fotos", imagem_byte));





            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Dados Cadastrados com Sucesso");
                Limpar();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }

        }

        private void Limpar()
        {
            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";

            pictureBox1.Image = null;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                imagem = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imagem;


            }
        }

        private void Cadastrar_Produto_Load(object sender, EventArgs e)
        {

        }
    }
}
