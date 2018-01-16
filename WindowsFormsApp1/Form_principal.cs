using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form_principal : Form
    {
        public Form_principal()
        {
            InitializeComponent();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cadastrar_Produto Cadastroproduto = new Cadastrar_Produto();
            Cadastroproduto.Show();
        }

        private void alterarExcluirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Alt_Exc alterarexcluir = new Alt_Exc();
            alterarexcluir.Show();
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caixa Caixa1 = new Caixa();
            Caixa1.Show();
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form_principal_Load(object sender, EventArgs e)
        {

        }
    }
}
