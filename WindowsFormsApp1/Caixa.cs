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
        public SqlConnection sqlCon;
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
                    sqlcon.Open();
                }
                SqlDataReader drprod = cmdprod.ExecuteReader();

                if (! drprod.HasRows)
                {
                    MessageBox.Show("produto não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    textBox2.Text = drprod["nome"].ToString();
                    textBox3.Text = drprod["preco"].ToString();
                    byte[] imagem = (byte[])(drprod["fotos"]);


                    if (imagem ==null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream mstream = new MemoryStream(imagem);
                        pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
                    }

                  }








                


            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
            
            
        }
        private void GravarVenda()
        {
            string sqlvenda = "insert into caixa(Idvenda,ValorTotal) values(@idvenda,@valortotal)";
            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand cmdvenda = new SqlCommand(sqlvenda, conn);

            cmdvenda.Parameters.AddWithValue("@idvenda", Convert.ToInt32(label2.Text));
            cmdvenda.Parameters.AddWithValue("@valortotal", float.Parse(label6.Text));


            try
            {
                conn.Open();
                cmdvenda.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dataGridView1.Rows.Clear();
                TotalVenda = 0;
                conn.Close();
                textBox1.Focus();
            }
        }

        private void inserir()
        {
            string sqlItens = "insert Intermediaria(IdVenda,idprod,qt,ValorT) values(@codvenda,@codprod,@quantidade,@total)";
            SqlConnection conn = new SqlConnection(strCon);

            try
            {
                SqlCommand cmdinserir = new SqlCommand(sqlItens, conn);
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    cmdinserir.Parameters.Clear();
                    cmdinserir.Parameters.AddWithValue("@codvenda", label2.Text);
                    cmdinserir.Parameters.AddWithValue("@codprod", dataGridView1.Rows[i].Cells[0].Value);
                    cmdinserir.Parameters.AddWithValue("@quantidade", Convert.ToInt32(dataGridView1.Rows[1].Cells[3].Value));
                    cmdinserir.Parameters.AddWithValue("@total", Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value));

                    conn.Open();
                    cmdinserir.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               conn.Close();
            }
        }



        #endregion
        private void Caixa_Load(object sender, EventArgs e)
        {
            NomearDataGrid();
            GerarCodigo();

        }
        

    }
}
