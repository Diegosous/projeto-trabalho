using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
        }

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        SqlConnection sqlcon = null;
        private string strCon = @"Data Source=.\sqlexpress;Initial Catalog=trabalho;User ID=diego;Password=diego123; MultipleActiveResultSets=true";
        private string strsql = string.Empty;

        #region variaveis

        public string strcon = @"Data Source=.\sqlexpress;Initial Catalog=trabalho;User ID=diego;Password=diego123; MultipleActiveResultSets=true";
        public SqlConnection sqlcon;
        float TotalVenda = 0;
        int i;
        int totalanterior = 1;
        #endregion
        #region metodo fechar,sair,limpar,datagrid,gerarcodigo

        private void NomearDataGrid()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Código";
            dataGridView1.Columns[1].Name = "Código";
            dataGridView1.Columns[1].Width = 280;
            dataGridView1.Columns[2].Name = "Valor Unitario";
            dataGridView1.Columns[3].Name = "Quantidade";
            dataGridView1.Columns[4].Name = "Total";

        }

        private void GerarCodigo()
        {
            sqlcon = new SqlConnection(strcon);
            string sintaxe = "select max (Idvenda) from caixa";

            try
            {
                sqlCon.Open();
                SqlCommand cmdvodvenda = new SqlCommand(sintaxe, sqlCon);

                if (cmdvodvenda.ExecuteScalar() == DBNull.Value)
                {
                    label2.Text = "1";
                }
                else
                {
                    Int32 ra = Convert.ToInt32(cmdvodvenda.ExecuteScalar()) + 1;
                    label2.Text = "1";
                }


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

        private void FecharFormulario(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void Limpar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            pictureBox1.Image = null;
        }
        #endregion
        #region Método de Consulta,Gravar venda e itens de venda 

        private void ConsultarPruduto()
        {
            string strsql = "select * from produtos where idprod=@idprod";
            sqlcon = new SqlConnection(strCon);

            SqlCommand cmdprod = new SqlCommand(strsql, sqlcon);
            cmdprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox1.Text);

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.open();
                }
                SqlDataReader drprod = cmdprod.ExecuteReader();

                if (idprod.masRows)
                {
                    MessageBox.Show("produto não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox2.Text = drprod["nome"].ToString();
                    textBox3.Text = drprod["preco"].ToString();
                    byte[] imagem = (byte[])(drprod["fotos"]);


                    if











                }


            }
            
            
            
        }


             

        private void Caixa_Load(object sender, EventArgs e)
            {

            }
        }
    }
}