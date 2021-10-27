using francisca2010.ConfigDB;
using francisca2010.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace francisca2010
{
    public partial class Form1 : Form
    {
        private MyEventosDbContext contex;
        public Form1()
        {

            InitializeComponent();
            //abrir conexão com o banco
            contex= new MyEventosDbContext();
            //selecionar nome de status
            var status = contex.Statuses.ToList();
            foreach(Status s in status)
            {
                comboBox1.Items.Add(s);
            }
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            BindingSource bi = new BindingSource();
            var query = from e in contex.Eventos
                        orderby e.Data descending
                        select new { e.Id, e.Nome, e.Data, e.status };
            bi.DataSource = query.ToList();
            dataGridView1.DataSource = bi;
            dataGridView1.Refresh();

        }

        private void buttonCriar_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && textNome.Text != string.Empty)
            {
                var evento = new Evento()
                {
                    Nome = textNome.Text,
                    Data = dateTimePicker1.Value,
                    StatusId = (comboBox1.SelectedItem as Status).Id
                };
                contex.Eventos.Add(evento);//inserir em
                contex.SaveChanges();
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("você precisa selecionar um status ou inserir um evento");
            }
            
        }

        private void buttonExcluir_Click(object sender, EventArgs e)
        {
            //pesquisando valor no banco de chave primaria que seja igual o da celula 0
            var t = contex.Eventos.Find((int)dataGridView1.SelectedCells[0].Value);
            contex.Eventos.Remove(t);
            contex.SaveChanges();
            RefreshGrid();

        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            textNome.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            comboBox1.Text = "Selecionar...";
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if(buttonEditar.Text== "Editar")
            {
                textNome.Text = dataGridView1.SelectedCells[1].Value.ToString();
                dateTimePicker1.Value = (DateTime)dataGridView1.SelectedCells[2].Value;


                foreach(Status s in comboBox1.Items)
                {
                    if (s.Nome == dataGridView1.SelectedCells[1].Value.ToString())
                    {
                        comboBox1.SelectedItem = s;
                    }
                    
                }
                buttonEditar.Text = "Salvar";
            }
            else if(buttonEditar.Text == "Salvar")
            {
                var editarEventos = contex.Eventos.Find((int)dataGridView1.SelectedCells[0].Value);

                editarEventos.Nome = textNome.Text;
                editarEventos.Data = dateTimePicker1.Value;
                editarEventos.StatusId = (comboBox1.SelectedItem as Status).Id;

                contex.SaveChanges();
                RefreshGrid();

                buttonEditar.Text = "Editar";
                textNome.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                comboBox1.Text = "Selecionar...";

            }
        }
    }
   
}
