using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Practica1_Compi2_2016
{
    public partial class Form1 : Form
    {
        int cont = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.groupBox1.Visible = true;
            this.groupBox2.Visible = false;
            this.groupBox3.Visible = false;
        }

        private void analizarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.groupBox2.Visible = true;
            this.groupBox1.Visible = false;
            this.groupBox3.Visible = false;
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.groupBox3.Visible = true;
            this.groupBox2.Visible = false;
            this.groupBox1.Visible = false;
        }

        private void bAgregar_Click(object sender, EventArgs e)
        {
            TabPage Nueva = new TabPage("Archivo"+cont+".dx");
            TextBox texto = new TextBox();
            texto.Name = "texto";
            texto.Text = "";
            texto.Multiline = true;
            texto.SetBounds(0, 0, tabDx.Width, tabDx.Height);
            Nueva.Controls.Add(texto);
            tabDx.TabPages.Add(Nueva);
            cont++;
            tabDx.SelectedTab = Nueva;
            tabDx.ResetText();
        }

        private void bCargar_Click(object sender, EventArgs e)
        {
            Compilar();
        }

        private void Compilar()
        {
            Analizador a = new Analizador(new Gramatica_ER());

            string text = "";
            if(tabDx.SelectedTab.Controls.ContainsKey("texto"))
                text = tabDx.SelectedTab.Controls["texto"].Text;
            a.parse(text, new AccionesGramatica_ER());
        }

    }
}
