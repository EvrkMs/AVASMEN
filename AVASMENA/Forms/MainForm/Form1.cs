using MaterialSkin;
using MaterialSkin.Controls;
using PasswordFormExample;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel;
using FormsSetting;
using TelegramCode;

namespace AVASMENA
{
    public partial class MainForm : MaterialForm
    {
        //листы
        private readonly Dictionary<string, long> users = new Dictionary<string, long>
        {
            {"Ярый", 1972629490 },
            {"Серый", 986504267 },
            {"Вова", 5784613858},
            {"Егор",  917263855},
            {"Дима По", 1497063301 },
            {"Али",  5540567292},
            {"Илья",  5107083008}
        };
        private readonly Dictionary<string, int> names = new Dictionary<string, int>
        {
            { "Вова", 6 },
            { "Ярый", 178 },
            { "Серый", 448 },
            { "Егор", 12 },
            { "Дима По", 913 },
            { "Али", 11 },
            { "Илья", 10 }
        };
        //по екселю
        private readonly string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Documents", "excel", "itog.xlsx");
        //по форме
        private static string min = "";
        private readonly Timer timer = new Timer();
        //по боту
        private readonly Telegram.Bot.TelegramBotClient bot = new Telegram.Bot.TelegramBotClient("6375453330:AAFIKOIwAztwY4__CF2c_vZvzcNuUf4l3KM");
        private readonly long forwardChatId = -1002066018588;
        private readonly long chatID = -1001990911245;
        // Установите ваш пароль здесь
        private const string CorrectPassword = "238384";
        public MainForm()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue800, Primary.Blue900, Primary.Purple400, Accent.Purple700, TextShade.WHITE);


            InitializeComponent();
            Forms.LoadItemsToListBox(listBox5);
            Forms.InitializeListBox(listBoxNameInv);

            Forms.SetupListBox(listBox1);
            Forms.SetupListBox(listBox2);
            Forms.SetupListBox(listBox3);
            Forms.SetupListBox(listBoxRas);
            Forms.SetupListBox(listBoxNameInv);

            Forms.SetupButton1(button1, button2);

            Forms.SetupTabPage(tabPage1, tabPage2, tabPage3, tabPage4, tabPage5, tabPage6, tabPage7);

            Forms.Setup1(label1, label2, label3, label4, label5, label6, label7, label8, label9, label10, label11, label12, label13, label14, label15, label16, label17, label18, label21);
            Forms.Setup2(label19, label20);


            Forms.SetupMaterialSwitch(materialSwitch1);
            Forms.SetupComboBoxes(comboBox1, materialComboBox1, materialComboBox2, materialComboBox3, materialComboBox4, comboBoxNameRas);
            materialComboBox3.Items.Add("нет");
            materialComboBox4.Items.Add("нет");

            this.ClientSize = new System.Drawing.Size(1680, 802);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = this.MaximumSize = this.Size;

            SetupEvents();
            SetupTimer();
        }
        private void SetupEvents()
        {
            textBox2.TextChanged += UpdateListBox5;
            textBox3.TextChanged += UpdateListBox5;
            textBox4.TextChanged += UpdateListBox5;
            textBox5.TextChanged += UpdateListBox5;
            textBox7.TextChanged += UpdateListBox5;
            TextBox8.TextChanged += UpdateListBox5;
            materialTextBox21.TextChanged += UpdateListBox;
            materialTextBox22.TextChanged += UpdateListBox;
            materialSwitch1.CheckedChanged += UpdateListBox1;
            comboBoxExcel.SelectedIndexChanged += ComboBoxExcel_SelectedIndexChanged;
        }
        private void SetupTimer()
        {
            timer.Interval = 30000;
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            button2.Enabled = true;
            timer.Stop();
        }
        private void ListBox5_SelectedIndexChanged(object sender, EventArgs e)
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
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // проверяем, откуда пришло событие KeyPress
            // если это не textbox8 и не textbox9 - запрещаем ввод букв
            if (sender != TextBox8)
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }
        private void HandleEnterKeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Ваш код для переключения свитча
                materialSwitch1.Checked = !materialSwitch1.Checked;
            }
        }
        private void MaterialComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, что выбранное значение не равно null
            if (materialComboBox3.SelectedItem != null && materialComboBox3.SelectedItem.ToString() != "нет")
            {
                // Показываем текстовые поля
                Minus1.Visible = true;
                Minus2.Visible = true;
                // Показываем лейблы
                label19.Visible = true;
                label20.Visible = true;
                // Показываем MaterialTextBox24
                materialTextBox24.Visible = true;
            }
            else
            {
                // Если выбрано null, скрываем элементы
                Minus1.Visible = false;
                Minus2.Visible = false;
                label19.Visible = false;
                label20.Visible = false;
                materialTextBox24.Visible = false;
            }
        }
        private void BoxesMinus()
        {
            int.TryParse(textBox2.Text, out int nalf);
            int.TryParse(textBox3.Text, out int bnf);
            int.TryParse(textBox4.Text, out int nalp);
            int.TryParse(textBox5.Text, out int bnp);
            int.TryParse(Minus1.Text, out int minus1);
            int.TryParse(Minus2.Text, out int minus2);
            int minus = CalculateDifference(nalf, nalp, bnf, bnp);

            if (minus > 0)
            {
                minus = 0;
            }
            if (minus1 + minus2 != minus)
            {
                MessageBox.Show($"ваша сумма минуса не равна общему минусу, вами указанно {minus1 + minus2}, а надо {minus}");
                return;
            }

        }
        private void Button1_Click(object sender, EventArgs e)
        {
            UpdateAndButton1(1);
        }
        private void UpdateListBox5(object sender, EventArgs e)
        {
            UpdateAndButton1(0);
        }
        private void UpdateAndButton1(int prov)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("Вы забыли имя");
                return;
            }

            SetDefaultValuesAndTextBoxes(materialTextBox1, materialTextBox24, textBox2, textBox3, textBox4, textBox5, textBox7);

            int nalf = int.Parse(textBox2.Text);
            int bnf = int.Parse(textBox3.Text);
            int nalp = int.Parse(textBox4.Text);
            int bnp = int.Parse(textBox5.Text);
            int seyf = int.Parse(textBox7.Text);

            int minus = CalculateDifference(nalf, nalp, bnf, bnp);
            if (minus > 0)
            {
                minus = 0;
            }

            int zp = 167;
            int haurs = CalculateHaurs(materialTextBox1, materialTextBox24);
            int zp4 = zp * haurs + (minus > 0 ? 0 : minus);

            if (string.IsNullOrWhiteSpace(materialComboBox3.Text))
            {
                haurs += int.Parse(materialTextBox24.Text);
                zp4 = zp * haurs + (minus > 0 ? 0 : minus);
            }

            int seyfEnd = seyf + nalf - 1000;
            int viruchka = nalf + bnf - 1000;
            int itog = viruchka - zp4;

            if (prov == 1 && Minus1.Visible == true)
            {
                BoxesMinus();
            }

            UpdateTextBoxes2(textBox2, textBox3, textBox4, textBox5, textBox7);
            listBox1.Items.Clear();
            PopulateListBox(nalf, bnf, viruchka, minus, zp4, itog, seyfEnd, nalp, bnp);
        }
        private void SetDefaultValuesAndTextBoxes(MaterialTextBox2 textBox1, MaterialTextBox2 textBox2, params MaterialTextBox2[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    textBox.Text = "0";
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
                textBox1.Text = "";
            if (string.IsNullOrWhiteSpace(textBox2.Text))
                textBox2.Text = "";
        }
        private int CalculateDifference(int nalf, int nalp, int bnf, int bnp)
        {
            return (nalf - nalp) + (bnf - bnp);
        }
        private int CalculateHaurs(params MaterialTextBox2[] textBoxes)
        {
            int total = 0;
            foreach (var textBox in textBoxes)
            {
                total += int.Parse(textBox.Text);
            }
            return total;
        }
        private void UpdateTextBoxes2(params MaterialTextBox2[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    textBox.Text = "0";
            }
        }
        private void PopulateListBox(int nalf, int bnf, int viruchka, int minus, int zp4, int itog, int seyfEnd, int nalp, int bnp)
        {
            string name = comboBox1.Text;

            if (!string.IsNullOrWhiteSpace(materialComboBox3.Text) && materialComboBox3.Text != "нет")
                name = $"{name}/{materialComboBox3.Text}";

            listBox1.Items.Add($"ДАТА: {DateTime.Now:yyyy.MM.dd HH:mm:ss}");
            listBox1.Items.Add($"{name}\n");
            listBox1.Items.Add($"Нал: {nalf}р");
            listBox1.Items.Add($"Б/Н: {bnf}р");
            listBox1.Items.Add($"-1000р размен.");
            listBox1.Items.Add($"Выручка: {viruchka}р");

            if (string.IsNullOrWhiteSpace(TextBox8.Text))
                listBox1.Items.Add($"Минус: {minus}р");
            else
                listBox1.Items.Add($"Минус: {minus}р ({TextBox8.Text})");

            listBox1.Items.Add($"ЗП: {zp4}р");
            listBox1.Items.Add($"Итог: {itog}р\n");
            listBox1.Items.Add($"Сейф: {seyfEnd}р\n");
            listBox1.Items.Add($"Нала в программе: {nalp}р");
            listBox1.Items.Add($"Б/Н в программе: {bnp}р");
        }
        private async void Button2_Click(object sender, EventArgs e)
        {
            string name = comboBox1.Text;
            string name2 = materialComboBox3.Text;

            int.TryParse(textBox2.Text, out int nalf);
            int.TryParse(textBox3.Text, out int bnf);
            int.TryParse(textBox4.Text, out int nalp);
            int.TryParse(textBox5.Text, out int bnp);
            int.TryParse(Minus1.Text, out int minus1);
            int.TryParse(Minus2.Text, out int minus2);
            int.TryParse(materialTextBox1.Text, out int hours1);
            int.TryParse(materialTextBox24.Text, out int hours2);
            // Calculate the difference
            int minus = CalculateDifference(nalf, nalp, bnf, bnp);

            if (minus > 0)
            {
                minus = 0;
            }
            // Calculate zp
            int zp = 167;
            int haurs = CalculateHaurs(materialTextBox1, materialTextBox24);
            int zp4 = zp * haurs + (minus > 0 ? 0 : minus);

            // Check if materialComboBox3 is filled
            if (string.IsNullOrWhiteSpace(materialComboBox3.Text))
            {
                haurs += int.Parse(materialTextBox24.Text);
                zp4 = zp * haurs + (minus > 0 ? 0 : minus);
            }

            // Calculate other variables
            int viruchka = nalf + bnf - 1000;
            int itog = viruchka - zp4;
            int zarp1 = 0;
            int zarp2 = 0;

            if (minus > 0)
            {
                minus = 0;
            }
            if (!string.IsNullOrWhiteSpace(materialComboBox3.Text) && materialComboBox3.Text != "нет")
            {
                BoxesMinus();
            }

            if (!string.IsNullOrWhiteSpace(materialComboBox3.Text) && materialComboBox3.Text != "нет")
            {
                zarp1 = zp * hours1 + (-1 * minus1);
                zarp2 = zp * hours2 + (-1 * minus2);
            }
            else
            {
                zarp1 = zp * hours1 + minus;
            }

            string selectedName = comboBox1.SelectedItem?.ToString();
            long userId = users[selectedName];
            int TredID = 2;
            // Call the method
            await Telegrame.ProcessUpdates(userId, TredID, selectedName, listBox2, button2);

            var zp1 = $"{DateTime.Now:yyyy.MM.dd}\n+{zarp1}p";
            var zp2 = $"{DateTime.Now:yyyy.MM.dd}\n+{zarp2}p";

            await Telegrame.SendMessageAsync(zp1, zp2, name, name2, button2);
            StringBuilder listBox1StringBuilder = new StringBuilder();
            listBox1.Invoke((MethodInvoker)delegate
            {
                foreach (var item in listBox1.Items)
                    listBox1StringBuilder.AppendLine(item.ToString());
            });
            await bot.SendTextMessageAsync(forwardChatId, listBox1StringBuilder.ToString(), replyToMessageId: TredID);

            await ExcelHelper.UpdateExcel(viruchka, itog);
            await ExcelHelper.ScreenExcel(filePath);
            await ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel);
            await ExcelHelper.ZPexcelОтчет(zarp1, zarp2, comboBox1, materialComboBox3, Minus2);
            PopravkaDa.Checked = false;
            return;
        }
        private void MaterialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            min = (materialSwitch1.Checked) ? "+" : "-";
            materialSwitch1.Tag = $"Optial1,{min}";
        }
        private async void MaterialButton1_Click_1(object sender, EventArgs e)
        {
            if (!materialCheckbox1.Checked || string.IsNullOrWhiteSpace(materialTextBox21.Text) || string.IsNullOrWhiteSpace(materialTextBox22.Text))
                return;

            string Qwerty = materialTextBox21.Text;
            int Qwerty2 = int.Parse(materialTextBox22.Text);

            MaterialSwitch1_CheckedChanged(materialSwitch1, EventArgs.Empty);

            string message = $"{min}{Qwerty2} {Qwerty}";
            await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: 2);
            materialButton1.Enabled = false;
            await Task.Delay(5000);
            materialButton1.Enabled = true;
            return;
        }
        private void UpdateListBox(object sender, EventArgs e)
        {
            MaterialSwitch1_CheckedChanged(materialSwitch1, EventArgs.Empty);

            string chart = materialTextBox21.Text;
            string chart2 = materialTextBox22.Text;
            string all = chart2 + " " + chart;
            listBox3.Items.Clear();
            listBox3.Items.Add($"{min}{all}");
        }
        private void UpdateListBox1(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add($"MaterialSwitch is {(materialSwitch1.Checked ? "On" : "Off")}");
            MaterialSwitch1_CheckedChanged(materialSwitch1, EventArgs.Empty);

            string chart = materialTextBox21.Text;
            string chart2 = materialTextBox22.Text;
            string all = chart2 + " " + chart;
            listBox3.Items.Clear();
            listBox3.Items.Add($"{min}{all}");
        }
        private void MaterialCheckbox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(materialTextBox21.Text)) || (string.IsNullOrWhiteSpace(materialTextBox22.Text)))
            {
                MessageBox.Show("Поля не заполнены");
                materialCheckbox1.Checked = false; // Снимаем галочку
                return; // Прекращаем выполнение метода, если поле не заполнено
            }
            MaterialSwitch1_CheckedChanged(materialSwitch1, EventArgs.Empty);
            string Qwerty = materialTextBox21.Text;
            int Qwerty2 = int.Parse(materialTextBox22.Text);
            string message = $"{min}{Qwerty2} {Qwerty}";

            listBox3.Items.Clear();
            listBox3.Items.Add($"{message}");
        }
        private async void MainForm_Load(object sender, EventArgs e)
        {
            await ExcelHelper.ExcelCreated();
            await ExcelHelper.LoadSheet(comboBoxExcel);
            Minus1.Visible = false;
            Minus2.Visible = false;
            label19.Visible = false;
            label20.Visible = false;
            materialTextBox24.Visible = false;
            materialTextBox24.Text = "0";
            materialTextBox1.Text = "0";
            try
            {
                await bot.DeleteWebhookAsync();
                listBox2.Invoke((MethodInvoker)delegate
                {
                    listBox2.Items.Add("Webhook removed.");
                });
            }
            catch (Exception ex)
            {
                listBox2.Invoke((MethodInvoker)delegate
                {
                    listBox2.Items.Add($"Error removing webhook: {ex.Message}");
                });
            }
            return;
        }
        private void ComboBoxExcel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем выбранное имя листа
            string selectedSheetName = comboBoxExcel.SelectedItem.ToString();
            // Загружаем данные из выбранного листа Excel в DataGridView
            ExcelHelper.LoadGrindSheet(dataGridViewExcel, selectedSheetName);
        }
        private async void MaterialButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(materialComboBox4.Text) || materialComboBox4.Text == "нет")
            {
                MessageBox.Show("Укажите кто выписывает");
                return;
            }
            var messag = string.Join(Environment.NewLine, listBox4.Items.Cast<string>());
            var name = materialComboBox1.Text;
            var syu = materialTextBox2.Text;
            var nameVipi = materialComboBox4.Text;
            var message = $"Выписал: {nameVipi}\n\n{name}\n\n{messag}\n\n-{syu}";

            await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: 3);

            if (names.ContainsKey(name))
                await bot.SendTextMessageAsync(chatID, message, replyToMessageId: names[name]);
            return;
        }
        private async void MaterialButton4_Click(object sender, EventArgs e)
        {
            string name = materialComboBox2.Text;
            int.TryParse(materialTextBox23.Text, out int summa);
            summa *= -1;
            var message = $"{summa} аванс {name}";

            await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: 2);
            if (names.ContainsKey(name))
                await bot.SendTextMessageAsync(chatID, message, replyToMessageId: names[name]);
            await ExcelHelper.AvansExcel(name, summa);
            return;
        }
        private async void MaterialButton5_Click(object sender, EventArgs e)
        {
            PopravkaDa.Checked = false;
            if (string.IsNullOrWhiteSpace(materialTextBox25.Text) || string.IsNullOrWhiteSpace(materialTextBox26.Text) || string.IsNullOrWhiteSpace(comboBoxNameRas.Text))
            {
                MessageBox.Show("Вы заполнели не все поля");

                return;
            }

            string selectedName = comboBoxNameRas.SelectedItem?.ToString();
            long userId = users[selectedName];
            int TredID = 22513;

            if (materialCheckbox6.Checked)
            {
                TredID = 21;
            }

            int.TryParse(materialTextBox25.Text, out int summ);
            int sum = summ * -1;
            string message = $"{sum} {materialTextBox26.Text}";

            // Call the method
            if (materialCheckbox2.Checked)
            {
                await Telegrame.ProcessUpdates(userId, TredID, selectedName, listBoxRas, materialButton5);
            }
            await ExcelHelper.UpdateExlel2(summ);
            await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: TredID);
            await ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel);
            if (materialCheckbox4.Checked)
            {
                var messag = $"-{summ} {materialTextBox26.Text}";
                await bot.SendTextMessageAsync(forwardChatId, messag, replyToMessageId: 2);
            }
            return;
        }
        private void MaterialButton6_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel);
            PopravkaDa.Checked = false;
        }
        private void MaterialCheckbox5_CheckedChanged(object sender, EventArgs e)
        {
            if (PopravkaDa.Checked == true)
            {
                dataGridViewExcel.ReadOnly = false;
            }
            else
            {
                dataGridViewExcel.ReadOnly = true;
            }

        }
        private void BtnSaveExcel_Click(object sender, EventArgs e)
        {
            string selectedSheetName = comboBoxExcel.SelectedItem.ToString();
            ExcelHelper.SaveDataToExcel(selectedSheetName, dataGridViewExcel);
            ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel);
            PopravkaDa.Checked = false;
        }
        private async void SendMessageToChat(string message, int replyToMessageId)
        {
            await bot.SendTextMessageAsync(chatID, message, replyToMessageId: replyToMessageId);
        }
        private void SendMessageToSelectedNames(List<string> selectedNames, string message)
        {
            foreach (var name in selectedNames)
            {
                if (names.TryGetValue(name, out int id))
                {
                    SendMessageToChat(message, id);
                }
                else
                {
                    MessageBox.Show("Вы не выбрали имена");
                }
            }
        }
        private async void InventBTN_Click(object sender, EventArgs e)
        {
            using (var passwordInputForm = new PasswordInputForm())
            {
                if (passwordInputForm.ShowDialog() == DialogResult.OK)
                {
                    string enteredPassword = passwordInputForm.EnteredPassword;

                    if (enteredPassword != CorrectPassword)
                    {
                        MessageBox.Show("Пароль не верный.");
                        return; // Метод завершается полностью здесь, если пароль неверен
                    }
                }
            }

            // Парсим значение из InventSum
            if (!int.TryParse(InventSum.Text, out int inventSum))
            {
                MessageBox.Show("Введите корректное число.");
                return;
            }

            // Получаем количество выбранных элементов в listBoxNameInv
            int selectedCount = listBoxNameInv.SelectedItems.Count;

            // Проверяем, чтобы выбрано было хотя бы одно имя
            if (selectedCount == 0)
            {
                MessageBox.Show("Выберите хотя бы одно имя.");
                return;
            }

            // Расчет результата деления
            int result = inventSum / selectedCount;
            result *= -1;

            // Формирование сообщения
            string message = $"{result}р инвентаризация";

            List<string> selectedNames = new List<string>();
            foreach (var item in listBoxNameInv.SelectedItems)
            {
                selectedNames.Add(item.ToString());
            }
            SendMessageToSelectedNames(selectedNames, message);
            await ExcelHelper.ЗаполнениеExcelInvent(result, listBoxNameInv, SendMessageToChat);
        }
    }
}