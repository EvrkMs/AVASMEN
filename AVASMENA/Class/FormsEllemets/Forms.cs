using DocumentFormat.OpenXml.Bibliography;
using jsonData;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AVASMENA
{
    public partial class MainForm
    {
        private const string ShtraphFilePath = "Class\\forms\\files\\shtraph.txt";
        private const string FontFamily = "Microsoft Sans Serif";
        private const int FontSizeLarge = 17;
        private const int FontSizeSmall = 10;
        private const string TransparentColor = "Transparent";
        private const string ForeColor = "LimeGreen";
        private static readonly List<string> namesList = UserDataLoader.LoadFromFile().NameList;
        private static readonly List<string> Login = new List<string> { "Admin", "Root" };

        public static void SetupComBox(ComboBox box)
        {
            foreach (var name in Login)
                box.Items.Add(name);
        }
        public static void SetupLabels(List<Label> labels, float fontSize, Color foreColor)
        {
            foreach (var label in labels)
            {
                label.BackColor = Color.Transparent;
                label.Font = new Font(FontFamily, fontSize);
                label.AutoSize = true;
                label.ForeColor = foreColor;
            }
        }
        public static void Setup1(List<Label> labels)
        {
            SetupLabels(labels, FontSizeLarge, Color.LimeGreen);
        }
        public static void Setup2(params Label[] labels)
        {
            SetupLabels(labels.ToList(), FontSizeSmall, Color.LimeGreen);
        }

        public static void SetupTabPage(params TabPage[] tabPage)
        {
            foreach (var tab in tabPage)
                tab.BackColor = System.Drawing.Color.FromArgb(64, 0, 64);
        }
        public static void SetupListBox(params ListBox[] listBox)
        {
            foreach (var box in listBox)
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
            try
            {
                string[] lines = System.IO.File.ReadAllLines(ShtraphFilePath);
                list.Items.Clear();
                foreach (string line in lines)
                {
                    list.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пунктов из файла {ShtraphFilePath}: {ex.Message}", "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public static void SetupComboBoxes(MaterialComboBox comboBox, List<string> names)
        {
            comboBox.Items.Clear();
            foreach (var name in names)
            {
                comboBox.Items.Add(name);
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
            dataGridViewJson.Columns.Add("ForwardChat", "Основной час");
            dataGridViewJson.Columns.Add("ChatId", "Чат с зп");
            dataGridViewJson.Columns.Add("TokenBot", "Токен бота");
            dataGridViewJson.Columns.Add("StopsAvans", "кто на стопе");


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