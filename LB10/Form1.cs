using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LB10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = textBox1.Text;
            if (date == "")
            {
                DB db = new DB();
                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command1 = new MySqlCommand("SELECT * FROM `back1`", db.getConnection());
                adapter.SelectCommand = command1;
                adapter.Fill(table1);

                MySqlCommand command2 = new MySqlCommand("SELECT * FROM `back2`", db.getConnection());
                adapter.SelectCommand = command2;
                adapter.Fill(table2);

                List<string> key = new List<string>();
                for (int i = 0; i < 4; i++)
                {
                    key.Add(table1.Rows[i][1].ToString());
                }

                foreach (DataRow row in table2.Rows)
                {
                    object v = row[1];
                    object c = row[2];
                    object p = row[3];
                    object d = row[4];
                    if (key.Contains(v.ToString()))
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (table1.Rows[i][1].ToString() == v.ToString())
                            {
                                table1.Rows[i][2] = Int32.Parse(table1.Rows[i][2].ToString()) + Int32.Parse(c.ToString());
                                table1.Rows[i][3] = Int32.Parse(table1.Rows[i][3].ToString()) + Int32.Parse(p.ToString());
                            }
                        }
                    }
                    else
                    {
                        int t = table1.Rows.Count;
                        DataRow n = table1.NewRow();
                        n[0] = t + 1;
                        n[1] = v.ToString();
                        n[2] = c.ToString();
                        n[3] = p.ToString();
                        n[4] = d.ToString();
                        table1.Rows.Add(n);
                    }
                }

                table1.Columns.Add("Общая стоимость", typeof(Int32));
                foreach (DataRow row in table1.Rows)
                {
                    object c = row[2];
                    object p = row[3];
                    row[5] = (Int32.Parse(c.ToString()) * Int32.Parse(p.ToString()));
                }

                dataGridView1.DataSource = table1;
            }
            else
            {
                DB db = new DB();
                DataTable table1 = new DataTable();
                DataTable table2 = new DataTable();
                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command1 = new MySqlCommand("SELECT * FROM `back1` WHERE `Дата` = @1", db.getConnection());
                command1.Parameters.Add("@1", MySqlDbType.VarChar).Value = date;
                adapter.SelectCommand = command1;
                adapter.Fill(table1);

                MySqlCommand command2 = new MySqlCommand("SELECT * FROM `back2` WHERE `Дата` = @2", db.getConnection());
                command2.Parameters.Add("@2", MySqlDbType.VarChar).Value = date;
                adapter.SelectCommand = command2;
                adapter.Fill(table2);

                List<string> key = new List<string>();
                for (int i = 0; i < table1.Rows.Count; i++)
                {
                    key.Add(table1.Rows[i][1].ToString());
                }

                foreach (DataRow row in table2.Rows)
                {
                    object v = row[1];
                    object c = row[2];
                    object p = row[3];
                    object d = row[4];
                    if (key.Contains(v.ToString()))
                    {
                        for (int i = 0; i < table1.Rows.Count; i++)
                        {
                            if (table1.Rows[i][1].ToString() == v.ToString())
                            {
                                table1.Rows[i][2] = Int32.Parse(table1.Rows[i][2].ToString()) + Int32.Parse(c.ToString());
                                table1.Rows[i][3] = Int32.Parse(table1.Rows[i][3].ToString()) + Int32.Parse(p.ToString());
                            }
                        }
                    }
                    else
                    {
                        int t = table1.Rows.Count;
                        DataRow n = table1.NewRow();
                        n[0] = t + 1;
                        n[1] = v.ToString();
                        n[2] = c.ToString();
                        n[3] = p.ToString();
                        n[4] = d.ToString();
                        table1.Rows.Add(n);
                    }
                }

                table1.Columns.Add("Общая стоимость", typeof(Int32));
                foreach (DataRow row in table1.Rows)
                {
                    object c = row[2];
                    object p = row[3];
                    row[5] = (Int32.Parse(c.ToString()) * Int32.Parse(p.ToString()));
                }

                dataGridView1.DataSource = table1;
            }
        }
    }
}
