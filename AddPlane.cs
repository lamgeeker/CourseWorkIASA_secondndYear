using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Planes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace laba_6_oop
{
    public partial class AddPlane : Form
    {
        private MainForm mainForm;
        public AddPlane(MainForm form)
        {
            InitializeComponent();
            mainForm = form;
            this.Load += AddPlane_Load; // додай цей рядок

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string userInput = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string userInput1 = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string userInput2 = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string userInput3 = textBox4.Text;
        }

        private void AddPlane_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "Boeing", "Mriya", "AirBusA330", "Boeing757" });
            comboBox1.SelectedIndex = 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int capacity = int.Parse(textBox1.Text);
                int loadCapacity = int.Parse(textBox3.Text);
                int fuelConsumption = int.Parse(textBox2.Text);
                int flightRange = int.Parse(textBox4.Text);

                string selectedType = comboBox1.SelectedItem.ToString();

                PlaneFactory factory = selectedType switch
                {
                    "Boeing" => new BoeingFactory(),
                    "Mriya" => new MriyaFactory(),
                    "AirBusA330" => new AirbusA330Factory(),
                    "Boeing757" => new Boeing757Factory(),
                    _ => throw new Exception("Невідомий тип літака")
                };

                Plane newPlane = factory.CreatePlane(flightRange, capacity, loadCapacity, fuelConsumption);
                mainForm.AddPlane(newPlane);
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть правильні числові значення.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
