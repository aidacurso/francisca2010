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
            }
            else
            {
                MessageBox.Show("você precisa selecionar um status ou inserir um evento");
            }
            
        }
    }
   
}
