using System;
using System.Collections.Generic;
using System.Windows.Forms;
using jsonData;
using MaterialSkin.Controls;

namespace FormsSetting
{
    public static class Forms   
    {
        private static readonly List<string> namesList = UserDataLoader.LoadFromFile().NameList;
        private static readonly List<string> Login = new List<string> { "Admin", "Root" };

        public static void Setup1(List<Label> labels)
        {
            foreach (var label in labels)
            {
                label.BackColor = System.Drawing.Color.Transparent;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
                label.Size = new System.Drawing.Size(230, 40);
                label.ForeColor = System.Drawing.Color.LimeGreen;
            }
        }
        public static void Setup2(params Label[] labels)
        {
            foreach (var label in labels)
            {
                label.BackColor = System.Drawing.Color.Transparent;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
                label.Size = new System.Drawing.Size(230, 40);
                label.ForeColor = System.Drawing.Color.LimeGreen;
            }
        }
        public static void SetupTabPage(params TabPage[] tabPage)
        {
            foreach (var tab in tabPage)
                tab.BackColor = System.Drawing.Color.FromArgb(64, 0, 64);
        }
        public static void SetupButton1(MaterialButton button1, MaterialButton button2)
        {
            button1.Location = new System.Drawing.Point(647, 452);
            button2.Location = new System.Drawing.Point(647, 687);
        }
        public static void SetupListBox(params ListBox[] listBox)
        {
            foreach(var box in listBox)
            {
                box.ForeColor = System.Drawing.Color.LawnGreen;
                box.BackColor = System.Drawing.Color.FromArgb(25, 0, 64);
                box.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            }
        }
        public static void SetupComboBoxes(params MaterialComboBox[] Box)
        {
            foreach (var box in Box)
            {
                foreach (var name in namesList)
                {
                    box.Items.Add(name);
                }
            }
        }
        public static void InitializeListBox(ListBox box)
        {
            foreach (var name in namesList)
            {
                box.Items.Add(name);
            }
            box.SelectionMode = SelectionMode.MultiExtended; // Разрешить множественный выбор
        }
        public static void LoadItemsToListBox(ListBox list)
        {
            // Путь к текстовому файлу с пунктами
            string filePath = "forms\\files\\shtraph.txt";

            try
            {
                // Читаем содержимое файла
                string[] lines = System.IO.File.ReadAllLines(filePath);

                // Очищаем ListBox5 перед загрузкой новых данных
                list.Items.Clear();

                // Добавляем каждую строку из файла в ListBox5
                foreach (string line in lines)
                {
                    list.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                // Если возникла ошибка при чтении файла, выводим сообщение об ошибке
                MessageBox.Show("Ошибка при загрузке пунктов из файла: " + ex.Message);
            }
        }
        public static void RemoveTabPage(MaterialTabControl TabC, List<TabPage> tabs)
        {
                foreach (var tab in tabs)
                {
                    if (TabC.TabPages.Contains(tab))
                        TabC.TabPages.Remove(tab);
                }
        }
        public static void SetupComBox(MaterialComboBox ComBox)
        {
            ComBox.Items.Clear();
            foreach (var name in Login)
            {
                ComBox.Items.Add(name);
            }
        }
        public static void PasswordBoxText(MaterialTextBox2 PasswordBox, bool i)
        {
            PasswordBox.Visible = i;
        }
        public static void HideShowSelector(MaterialTabSelector TabSel, bool i)
        {
            TabSel.Visible = i;
        }
        public static void InitializedataGrid(DataGridView dataGridViewJson)
        {
            dataGridViewJson.Columns.Clear();
            // Настройка DataGridView для работы с данными
            dataGridViewJson.Columns.Add("Name", "Имя");
            dataGridViewJson.Columns.Add("Users", "Телеграм Id");
            dataGridViewJson.Columns.Add("Names", "Топик в чате ЗП");
            dataGridViewJson.Columns.Add("NamesZP", "Столбец в эксель зп");

            dataGridViewJson.AllowUserToAddRows = true;
            dataGridViewJson.AllowUserToDeleteRows = true;
        }
        public static void ShtraphBox(ListBox listBox4, ListBox listBox5)
        {
            listBox4.BeginUpdate(); // Начать обновление listBox4

            listBox4.Items.Clear(); // Очистить все элементы listBox4

            string opisanie = ""; // Переменная для хранения описания
            foreach (var item in listBox5.SelectedItems)
            {
                opisanie += item.ToString() + "\n"; // Добавляем текущий элемент с символом новой строки
                listBox4.Items.Add(item); // Добавляем выбранный элемент в listBox4
            }

            listBox4.EndUpdate(); // Завершить обновление listBox4
        }
    }
}