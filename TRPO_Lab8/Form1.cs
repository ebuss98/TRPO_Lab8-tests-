using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Threading;
namespace TRPO_Lab8
{

    public partial class Form1 : Form
    {
        string subjId = "";
        string groupId = "";
        string subjName = "";
        string teacherLastname = "";
        string controlType = "";
        string groupCount = "";
        string lectHours = "";
        string pracHours = "";
        string isCoursework = "";
        string filename = "";
        XDocument doc;
        public Form1()
        {
            InitializeComponent();
            
        }

       

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox1.Enabled = true;
            }
            else textBox1.Enabled = false;
           
            
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                textBox2.Enabled = true;
            }
            else textBox2.Enabled = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                textBox3.Enabled = true;
            }
            else textBox3.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox4.Enabled = true;
            }
            else textBox4.Enabled = false;
        }
        //file opening
        public void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = openFileDialog1.FileName;
            string showfile = openFileDialog1.SafeFileName;
            // читаем файл в строку
            string fileText = System.IO.File.ReadAllText(filename);
            textBox14.Text = fileText;
            label10.Text = "File: ";
            label10.Text += showfile;
            doc = XDocument.Load(filename);
            MessageBox.Show("Файл открыт");
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }


        //add
        private void button2_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(filename))
            {
                MessageBox.Show("FILE NOT FOUND", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrEmpty(textBox7.Text) || String.IsNullOrEmpty(textBox8.Text) 
                || String.IsNullOrEmpty(textBox9.Text) || String.IsNullOrEmpty(textBox10.Text) || String.IsNullOrEmpty(textBox11.Text) || String.IsNullOrEmpty(textBox12.Text) 
                || String.IsNullOrEmpty(textBox13.Text))
            {
                MessageBox.Show("Empty fields are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XElement subject = new XElement("subject",
                        new XElement("subject_id", textBox5.Text),
                        new XElement("subject_name", textBox6.Text),
                        new XElement("teacher_lastname", textBox7.Text),
                        new XElement("group_id", textBox8.Text),
                        new XElement("group_quantity", textBox9.Text),
                        new XElement("lection_hours", textBox10.Text),
                        new XElement("practice_hours", textBox11.Text),
                        new XElement("has_coursework", textBox12.Text),
                        new XElement("final_control", textBox13.Text));
                doc.Root.Add(subject);
                doc.Save(filename);
                MessageBox.Show("Запись добавлена");

            }
        }
        //select
        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filename))
            {
                MessageBox.Show("FILE NOT FOUND", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrEmpty(textBox7.Text) || String.IsNullOrEmpty(textBox8.Text)
            //   || String.IsNullOrEmpty(textBox9.Text) || String.IsNullOrEmpty(textBox10.Text) || String.IsNullOrEmpty(textBox11.Text) || String.IsNullOrEmpty(textBox12.Text)
            //   || String.IsNullOrEmpty(textBox13.Text))
            //{
            //    MessageBox.Show("Empty fields are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            else
            {
                int counter = 0;
                IEnumerable<XElement> elements = null;
                if (radioButton1.Checked && textBox1.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("teacher_lastname") == textBox1.Text).ToList();
                }
                else if (radioButton2.Checked && textBox2.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("group_id") == textBox2.Text).ToList();
                }
                else if (radioButton3.Checked && textBox3.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("has_coursework") == textBox3.Text).ToList();
                }
                else if (radioButton4.Checked && textBox4.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("final_control") == textBox4.Text).ToList();
                }
                else
                {
                    MessageBox.Show("Empty fields are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                foreach (XElement s in elements)
                {
                    counter++;
                    textBox15.Text += (string)s.Element("subject_id") + Environment.NewLine + (string)s.Element("group_id") + Environment.NewLine + (string)s.Element("subject_name")
                        + Environment.NewLine + (string)s.Element("teacher_lastname") + Environment.NewLine + (string)s.Element("group_quantity") + Environment.NewLine + (string)s.Element("lection_hours")
                        + Environment.NewLine + (string)s.Element("practice_hours") + Environment.NewLine + (string)s.Element("has_coursework") + Environment.NewLine + (string)s.Element("final_control") + Environment.NewLine;
                }
                textBox15.Text += "Найдено " + Convert.ToString(counter) + " записей";
                doc.Save(filename);
                MessageBox.Show("Найдено " + Convert.ToString(counter) + " записей");
            }
        }
        //delete
        private void button3_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filename))
            {
                MessageBox.Show("FILE NOT FOUND", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrEmpty(textBox7.Text) || String.IsNullOrEmpty(textBox8.Text)
               || String.IsNullOrEmpty(textBox9.Text) || String.IsNullOrEmpty(textBox10.Text) || String.IsNullOrEmpty(textBox11.Text) || String.IsNullOrEmpty(textBox12.Text)
               || String.IsNullOrEmpty(textBox13.Text))
            {
                MessageBox.Show("Empty fields are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int counter = 0;
                IEnumerable<XElement> elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("subject_id") == textBox5.Text
                && (string)s.Element("group_id").Value == textBox8.Text
                && (string)s.Element("subject_name").Value == textBox6.Text
                && (string)s.Element("teacher_lastname").Value == textBox7.Text
                && (string)s.Element("group_quantity").Value == textBox9.Text
                && (string)s.Element("lection_hours").Value == textBox10.Text
                && (string)s.Element("practice_hours").Value == textBox11.Text
                && (string)s.Element("has_coursework").Value == textBox12.Text
                && (string)s.Element("final_control").Value == textBox13.Text
                ).ToList();
                foreach (XElement s in elements)
                {
                    counter++;
                    s.Remove();
                }
                doc.Save(filename);
                MessageBox.Show("Удалено " + Convert.ToString(counter) +  " записей");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filename))
            {
                MessageBox.Show("FILE NOT FOUND", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //else if (String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrEmpty(textBox7.Text) || String.IsNullOrEmpty(textBox8.Text)
            //   || String.IsNullOrEmpty(textBox9.Text) || String.IsNullOrEmpty(textBox10.Text) || String.IsNullOrEmpty(textBox11.Text) || String.IsNullOrEmpty(textBox12.Text)
            //   || String.IsNullOrEmpty(textBox13.Text))
            //{
            //    MessageBox.Show("Empty fields are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            else
            {
                int counter = 0;
                IEnumerable<XElement> elements = null;
                if (radioButton1.Checked && textBox1.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("teacher_lastname") == textBox1.Text).ToList();
                }
                else if (radioButton2.Checked && textBox2.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("group_id") == textBox2.Text).ToList();
                }
                else if (radioButton3.Checked && textBox3.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("has_coursework") == textBox3.Text).ToList();
                }
                else if (radioButton4.Checked && textBox4.Text != "")
                {
                    elements = doc.Root.Descendants("subject").Where(s => (string)s.Element("final_control") == textBox4.Text).ToList();
                }
                else
                {
                    MessageBox.Show("Empty fields are not allowed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                foreach (XElement s in elements)
                {
                    counter++;
                    s.Remove();
                }
                textBox15.Text += "Удалено " + Convert.ToString(counter) + " записей";
                doc.Save(filename);
                MessageBox.Show("Удалено " + Convert.ToString(counter) + " записей");
            }
        }
    }
}
