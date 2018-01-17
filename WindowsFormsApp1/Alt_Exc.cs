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
    public partial class Alt_Exc : Form
    {
        public Alt_Exc()
        {
            InitializeComponent();
        }
        string imagem;

        private void Alt_Exc_Load(object sender, EventArgs e)
        {

        }

        private void voltarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        SqlConnection sqlcon = null;

        private string strcon = @"Data Source=.\sqlexpress;Initial Catalog=trabalho;User ID=diego;Password=diego123; MultipleActiveResultSets=true";

        private string strsql = string.Empty;


        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                imagem = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imagem;


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String strsql = "update produtos set nome@nome, preco@preco, quant@quant,fotos@fotos where idprod=@idprod";
            sqlcon = new SqlConnection(strcon);

            SqlCommand comandoprod = new SqlCommand(strsql, sqlcon);
            comandoprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox1.Text);
            comandoprod.Parameters.Add("@nome", SqlDbType.Int).Value = textBox2.Text;
            comandoprod.Parameters.Add("@preco", SqlDbType.Int).Value = textBox4.Text;
            comandoprod.Parameters.Add("@quant", SqlDbType.Int).Value = textBox3.Text;

            byte[] imagem_byte = null;
            FileStream fs = new FileStream(imagem, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imagem_byte = br.ReadBytes((int)fs.Length);

            comandoprod.Parameters.Add(new SqlParameter("@fotos", imagem_byte));

            if (MessageBox.Show("Deseja Alterar os Dados?") == DialogResult.No) 
            { }else
            {

                try
                {
                    sqlcon.Open();
                    comandoprod.ExecuteNonQuery();
                    MessageBox.Show("Dados Cadastrados com Sucesso");
                   

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
                

        }

        private void button4_Click(object sender, EventArgs e)
        {
            String strsql = "delete from produtos where idprod=@idprod";
            sqlcon = new SqlConnection(strcon);

            SqlCommand comandoprod = new SqlCommand(strsql, sqlcon);
            comandoprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox1.Text);
            if (MessageBox.Show("Deseja Excluir ?") == DialogResult.No) 
            { }else
            {

                try
                {
                    sqlcon.Open();
                    comandoprod.ExecuteNonQuery();
                    MessageBox.Show("Dados excluidos");


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
        }
    }
}
