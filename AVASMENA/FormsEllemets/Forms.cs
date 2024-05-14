using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using jsonData;
using MaterialSkin.Controls;

namespace FormsSetting
{
    public static class Forms
    {
        private static readonly List<string> namesList = UserDataLoader.LoadFromFile().NameList;
        private static readonly List<string> Login = new List<string> { "Admin", "Root" };

        public static Task Setup1(params Label[] labels)
        {
            foreach (var label in labels)
            {
                label.BackColor = System.Drawing.Color.Transparent;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
                label.Size = new System.Drawing.Size(230, 40);
                label.ForeColor = System.Drawing.Color.LimeGreen;
            }
            return Task.CompletedTask;
        }
        public static Task Setup2(params Label[] labels)
        {
            foreach (var label in labels)
            {
                label.BackColor = System.Drawing.Color.Transparent;
                label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
                label.Size = new System.Drawing.Size(230, 40);
                label.ForeColor = System.Drawing.Color.LimeGreen;
            }
            return Task.CompletedTask;
        }
        public static Task SetupTabPage(params TabPage[] tabPage)
        {
            foreach (var tab in tabPage)
                tab.BackColor = System.Drawing.Color.FromArgb(64, 0, 64);

            return Task.CompletedTask;
        }
        public static Task<Tuple<System.Drawing.Point, System.Drawing.Point>> SetupButton1(MaterialButton button1, MaterialButton button2)
        {
            button1.Location = new System.Drawing.Point(647, 452);
            button2.Location = new System.Drawing.Point(647, 687);
            return Task.FromResult(new Tuple<System.Drawing.Point, System.Drawing.Point>(button1.Location, button2.Location));
        }
        public static Task SetupListBox(params ListBox[] listBox)
        {
            foreach(var box in listBox)
            {
                box.ForeColor = System.Drawing.Color.LawnGreen;
                box.BackColor = System.Drawing.Color.FromArgb(25, 0, 64);
                box.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            }
            return Task.FromResult(listBox);
        }
        public static Task SetupMaterialSwitch(MaterialSwitch materialSwitch)
        {
            materialSwitch.BackColor = System.Drawing.Color.Transparent;
            materialSwitch.Size = new System.Drawing.Size(49, 32);

            return Task.FromResult(materialSwitch);
        }
        public static Task SetupComboBoxes(params MaterialComboBox[] Box)
        {
            foreach (var box in Box)
            {
                foreach (var name in namesList)
                {
                    box.Items.Add(name);
                }
            }

            return Task.CompletedTask;
        }
        public static Task InitializeListBox(ListBox box)
        {
            foreach (var name in namesList)
            {
                box.Items.Add(name);
            }
            box.SelectionMode = SelectionMode.MultiExtended; // Разрешить множественный выбор

            return Task.FromResult(box);
        }
        public static Task LoadItemsToListBox(ListBox list)
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
            return Task.FromResult(list);
        }
        public static Task RemoveTabPage(MaterialTabControl TabC, List<TabPage> tabs)
        {
                foreach (var tab in tabs)
                {
                    if (TabC.TabPages.Contains(tab))
                        TabC.TabPages.Remove(tab);
                }
            return Task.CompletedTask;
        }
        public static Task SetupComBox(MaterialComboBox ComBox)
        {
            ComBox.Items.Clear();
            foreach (var name in Login)
            {
                ComBox.Items.Add(name);
            }
            return Task.CompletedTask;
        }
        public static Task PasswordBoxText(MaterialTextBox2 PasswordBox, bool i)
        {
            PasswordBox.Visible = i;
            return Task.CompletedTask;
        }
        public static Task HideShowSelector(MaterialTabSelector TabSel, bool i)
        {
            TabSel.Visible = i;
            return Task.CompletedTask;
        }
        public static Task InitializedataGrid(DataGridView dataGridViewJson)
        {
            dataGridViewJson.Columns.Clear();
            // Настройка DataGridView для работы с данными
            dataGridViewJson.Columns.Add("Name", "Name");
            dataGridViewJson.Columns.Add("Users", "Users");
            dataGridViewJson.Columns.Add("Names", "Names");
            dataGridViewJson.Columns.Add("NamesZP", "NamesZP");

            dataGridViewJson.AllowUserToAddRows = true;
            dataGridViewJson.AllowUserToDeleteRows = true;
            return Task.CompletedTask;
        }
    }
}
