using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using airCompany;
using CustomExceptions;
using FileManager;
using Planes;
namespace laba_6_oop
{
    public partial class MainForm : Form
    {

        public MainForm()
        {

            InitializeComponent();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = planes;

        }

        private BindingList<Plane> planes = new BindingList<Plane>();
        private void button1_Click(object sender, EventArgs e)
        {
            AddPlane addForm = new AddPlane(this);
            addForm.ShowDialog(); // показуємо форму модально
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PlaneSerializer.Save(new HashSet<Plane>(planes));
                MessageBox.Show("Літаки успішно збережено у файл.", "Збереження");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при збереженні: " + ex.Message, "Помилка");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AirCompany company = new AirCompany();
            Plane[] planeArray = planes.ToArray(); // `planes` — твій BindingList<Plane>
            company.CountCapAndLoad(planeArray);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PlaneContext context = new PlaneContext();
            context.SetStrategy(new SortByRangeStrategy());

            Plane[] planeArray = planes.ToArray();
            context.ExecuteStrategy(planeArray);

            // Оновлюємо BindingList
            planes.Clear();
            foreach (var plane in planeArray)
            {
                planes.Add(plane);
            }

            // Показуємо повідомлення
            string result = string.Join("\n", planeArray.Select(p =>
                $"{p.TypeName} - Дальність: {p.FlightRange} км"));

            MessageBox.Show($"Відсортовано за дальністю польоту:\n\n{result}", "Сортування");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Перевіряємо, чи є літаки
                if (planes.Count == 0)
                    throw new EmptyPlaneSetException();

                // Парсимо введення
                int min = int.Parse(textBox1.Text);
                int max = int.Parse(textBox2.Text);

                // Перевірка коректності діапазону
                if (min >= max)
                    throw new InvalidFuelRangeException();

                // Використання стратегії
                PlaneContext context = new PlaneContext();
                context.SetStrategy(new FilterByFuelStrategy(min, max));

                Plane[] planeArray = planes.ToArray();
                context.ExecuteStrategy(planeArray);

                // Фільтрація
                var filtered = planeArray
                    .Where(p => p.FuelConsuption > min && p.FuelConsuption < max)
                    .ToList();

                // Вивід результату
                if (filtered.Any())
                {
                    string result = string.Join("\n", filtered.Select(p =>
                        $"{p.TypeName} — {p.FuelConsuption} л/100км"));

                    MessageBox.Show($"Знайдено літаки з витратою пального від {min} до {max}:\n\n{result}", "Результати фільтрації");
                }
                else
                {
                    MessageBox.Show("Жодного літака не знайдено у вказаному діапазоні.", "Результати фільтрації");
                }
            }
            catch (EmptyPlaneSetException ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
            }
            catch (InvalidFuelRangeException ex)
            {
                MessageBox.Show(ex.Message, "Помилка діапазону");
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть правильні числові значення.", "Помилка вводу");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Невідома помилка: {ex.Message}", "Помилка");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                var loadedPlanes = PlaneSerializer.Load();

                if (loadedPlanes.Count == 0)
                {
                    MessageBox.Show("Файл порожній або не існує. Завантаження скасовано.", "Інформація");
                    return;
                }

                planes.Clear(); // очистити старі
                foreach (var plane in loadedPlanes)
                    planes.Add(plane);

                MessageBox.Show("Літаки успішно завантажено з файлу.", "Завантаження");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при завантаженні: " + ex.Message, "Помилка");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
        public void AddPlane(Plane plane)
        {
            if (!planes.Any(p => p.Equals(plane)))
            {
                planes.Add(plane);
                MessageBox.Show("Літак успішно додано!");
            }
            else
            {
                MessageBox.Show("Такий літак вже існує.");
            }
        }



        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
