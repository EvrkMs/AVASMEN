using Excel;
using jsonData;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using TelegramCode;

namespace AVASMENA
{
    public partial class MainForm : MaterialForm
    {
        //листыы
        private readonly Dictionary<string, long> users = UserDataLoader.LoadFromFile().Users;
        private static readonly Dictionary<string, int> names = UserDataLoader.LoadFromFile().Names;
        //по екселю
        private static readonly string folderPath = "\\\\192.168.88.254\\AVASMENAUpdate\\Needed\\excel";
        private static readonly string filePath = $"{folderPath}\\itog.xlsx";
        //по форме
        private readonly Timer timer = new Timer();
        private static readonly string token = UserDataLoader.LoadFromFile().TokenBot;
        private readonly Telegram.Bot.TelegramBotClient bot = new Telegram.Bot.TelegramBotClient(token);
        private readonly long forwardChatId = UserDataLoader.LoadFromFile().ForwardChat;
        private readonly long chatID = UserDataLoader.LoadFromFile().ChatId;
        private static readonly List<string> stopsAvans = UserDataLoader.LoadFromFile().StopsAvans;
        // Установите ваш пароль здесь
        private const string CorrectPassword = "238384";
        private bool RemoveDa = false;
        public MainForm()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue900, Primary.Blue900, Primary.Purple100, Accent.Purple100, TextShade.WHITE);


            InitializeComponent();
            //лист вкладок доступных только админу
            _RootList = new List<TabPage> { ShtraphPage, InventPage, PravkaItogPage, ManPage, ZpPage, SeyfExcel };
            //вкладка аунтификации
            _Auth = new List<TabPage> { AutherPage };
            //лист текстов
            _labelList = new List<Label> { label1, label2, label3, label4, label5, label6, label7, label8,
                label9, label10, label11, label12, label13, label14, label15, label16, label17, label18, label21};
            //лис чекбоксов для правки эксель
            _labelPopravka = new List<MaterialCheckbox> { PopravkaDa, ZpPopravka, PopravkaSeyf };

            ExitBtn.Visible = false;
            InitializedataGrid(dataGridViewJson);
            HideShowSelector(materialTabSelector1, false);
            PasswordBoxText(PasswordTextBox, false);
            LoadItemsToListBox(listBox5);
            InitializeListBox(listBoxNameInv);

            SetupListBox(listBox1, listBox2, listBox3, listBoxRas, listBoxNameInv);
            SetupTabPage(AutherPage, OtchetPage, AvansPage, SeyfPlusPage, RashodPage, ShtraphPage, InventPage, PravkaItogPage, ManPage, ZpPage, SeyfExcel);
            Setup1(_labelList);
            Setup2(label19, label20, label22);
            SetupComBox(LoginBox);
            LoginBox.Text = "Admin";
            SetupComboBoxes(NameComboBoxOtchet, KtoVipisal, materialComboBox2, SecondComboBoxNameOtchet, WhoVipisl, comboBoxNameRas, ThertyComboBox);
            SecondComboBoxNameOtchet.Items.Add("нет");
            WhoVipisl.Items.Add("нет");
            ThertyComboBox.Items.Add("нет");
            SetupTimer();
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            UserDataLoader.LoadBttn(dataGridViewJson);
        }
        private void SaveButton_Click(object sender, EventArgs e)
        {
            UserDataLoader.SaveButton_Click(dataGridViewJson);
            ExcelHelper.ExcelCreated();
        }

        public void LoginBox_SelectedIndex(object sender, EventArgs e)
        {
            if (LoginBox.Text == "Root")
            {
                PasswordBoxText(PasswordTextBox, true);
            }
            else
            {
                PasswordBoxText(PasswordTextBox, false);
            }
        }
        private void AuthBtn_Click(object sender, EventArgs e)
        {
            Logining();
        }
        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Logining();
            }
        }
        private void Logining()
        {
            if (PasswordTextBox.Visible)
            {
                if (PasswordTextBox.Text != CorrectPassword)
                {
                    MessageBox.Show("Incorrect password");
                    return;
                }
                RemoveDa = false;
            }
            materialTabControl1.SelectedTab = OtchetPage;
            if (LoginBox.Text == "Admin")
            {
                RemoveDa = true;
                MinusPoSeyf.Visible = false;
                Premia.Visible = false;
                RemoveTabPage(materialTabControl1, _RootList);
            }
            else
            {
                Premia.Visible = true;
                MinusPoSeyf.Visible = true;
            }
            RemoveTabPage(materialTabControl1, _Auth);
            HideShowSelector(materialTabSelector1, true);
            ExitBtn.Visible = true;
        }
        private void RestoreTabs()
        {
            if (RemoveDa == true)
            {
                foreach (var tab in _RootList)
                {
                    materialTabControl1.TabPages.Add(tab);
                }
            }
        }
        private void ExitBtn_Click(object sender, EventArgs e)
        {
            RestoreTabs();
            materialTabControl1.TabPages.Insert(0, AutherPage);
            materialTabControl1.SelectedTab = AutherPage;
            materialTabSelector1.Visible = false;
            ExitBtn.Visible = false;
        }


        private void SetupTimer()
        {
            timer.Interval = 30000;
            timer.Tick += Timer_Tick;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Отправить.Enabled = true;
            timer.Stop();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            List<CheckBox> list = new List<CheckBox> { AvansCheack, ZPcheak, MinusPoSeyf, Premia };
            CheckBox changedCheckBox = sender as CheckBox;

            if (changedCheckBox.Checked)
            {
                foreach (var checkBox in list)
                {
                    if (checkBox != changedCheckBox)
                    {
                        checkBox.Checked = false;
                    }
                }
            }
            else
            {
                // Проверка, если все CheckBox установлены в false, то AvansCheack устанавливается в true
                if (list.All(cb => !cb.Checked))
                {
                    AvansCheack.Checked = true;
                }
            }
        }
        private void ListBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShtraphBox(listBox4, listBox5);
        }

        private void CheakPlsHaurs()
        {
            if (materialTextBox1 == null || materialTextBox24 == null || Hours3 == null)
            {
                MessageBox.Show("Укажи сначало отработанное время, пожадуйста(а то будет ошибка)");
                return;
            }
        }
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheakPlsHaurs();
            // Проверяем, откуда пришло событие KeyPress
            // Если это не TextBox8 - запрещаем ввод букв
            if (sender != TextBox8)
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void ChengedHaurs(object sender, EventArgs e)
        {
            CheackHaurs();
        }

        private void MessageMaxHaurs()
        {
            ResetHours();
            MessageBox.Show("Борщанул с часами, указываешь меньше часов чем дозволено (12 часов)");
        }
        private void CheackHaurs()
        {
            // Преобразуем текстовые значения в числа
            int.TryParse(materialTextBox1.Text, out int hours1);
            int.TryParse(materialTextBox24.Text, out int hours2);
            int.TryParse(Hours3.Text, out int hours3);

            // Проверка видимости Hours3 и суммы часов
            if (Hours3.Visible)
            {
                if (hours1 + hours2 + hours3 > 12)
                {
                    MessageMaxHaurs();
                    return;
                }
            }
            // Проверка видимости materialTextBox24 и суммы часов
            if (materialTextBox24.Visible)
            {
                if (hours1 + hours2 > 12)
                {
                    MessageMaxHaurs();
                    return;
                }
            }
            // Проверка только первого текстбокса
            if (!materialTextBox24.Visible && !Hours3.Visible)
            {
                if (hours1 > 12)
                {
                    MessageMaxHaurs();
                    return;
                }
            }
        }

        private void ResetHours()
        {
            // Сбрасываем значения текстбоксов в 0
            materialTextBox1.Text = "0";
            materialTextBox24.Text = "0";
            Hours3.Text = "0";
        }

        private void MaterialComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем, что выбранное значение не равно null
            if (SecondComboBoxNameOtchet.SelectedItem != null && SecondComboBoxNameOtchet.SelectedItem.ToString() != "нет")
            {
                VisibleBox(true);
            }
            else
            {
                VisibleBox(false);
            }
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ThertyComboBox.SelectedItem != null && ThertyComboBox.SelectedItem.ToString() != "нет")
            {
                VisibleBox3(true);
            }
            else
            {
                VisibleBox3(false);
            }
        }
        private void VisibleBox(bool i)
        {
            Minus1.Visible = i;
            Minus2.Visible = i;
            label19.Visible = i;
            label20.Visible = i;
            materialTextBox24.Visible = i;
            ThertyComboBox.Visible = i;
            if (i == false)
                SecondComboBoxNameOtchet.Text = "нет";
        }

        private void VisibleBox3(bool i)
        {
            Minus3.Visible = i;
            label22.Visible = i;
            Hours3.Visible = i;
            if (i == false)
                ThertyComboBox.Text = "нет";
        }

        private
            (int nalf, int bnf, int nalp, int bnp, int seyf, int minus, int zp,
             int haurs, int zp4, int seyfEnd, int viruchka, int itog,
             int zarp1, int zarp2, int zarp3, string name, string name2, string name3, int minus1, int minus2, int minus3)
            GetValues()
        {
            string name = NameComboBoxOtchet.Text;
            string name2 = SecondComboBoxNameOtchet.Text;
            string name3 = ThertyComboBox.Text;

            int.TryParse(textBox2.Text, out int nalf);
            int.TryParse(textBox3.Text, out int bnf);
            int.TryParse(textBox4.Text, out int nalp);
            int.TryParse(textBox5.Text, out int bnp);
            int.TryParse(textBox7.Text, out int seyf);
            int.TryParse(Minus1.Text, out int minus1);
            int.TryParse(Minus2.Text, out int minus2);
            int.TryParse(Minus3.Text, out int minus3);
            int.TryParse(materialTextBox1.Text, out int hours1);
            int.TryParse(materialTextBox24.Text, out int hours2);
            int.TryParse(Hours3.Text, out int hours3);

            int minus = (nalf - nalp) + (bnf - bnp);
            if (minus > 0)
            {
                minus = 0;
            }

            int zp = 167;
            int haurs = CalculateHaurs(materialTextBox1, materialTextBox24);
            int zp4 = zp * haurs + (minus > 0 ? 0 : minus);

            int seyfEnd = seyf + nalf - 1000;
            int viruchka = nalf + bnf - 1000;
            int itog = viruchka - zp4;
            //для ДляButton2 далее
            int zarp1 = 0;
            int zarp2 = 0;
            int zarp3 = 0;

            if (!string.IsNullOrWhiteSpace(SecondComboBoxNameOtchet.Text) && SecondComboBoxNameOtchet.Text != "нет")
            {
                zarp1 = zp * hours1 - minus1;
                zarp2 = zp * hours2 - minus2;
            }
            else if (!string.IsNullOrWhiteSpace(ThertyComboBox.Text) && ThertyComboBox.Text != "нет")
            {
                zarp1 = zp * hours1 - minus1;
                zarp2 = zp * hours2 - minus2;
                zarp3 = zp * hours2 - minus3;
            }
            else
            {
                zarp1 = zp * hours1 - minus;
            }

            return (nalf, bnf, nalp, bnp, seyf, minus, zp, haurs, zp4, seyfEnd, viruchka, itog, zarp1, zarp2, zarp3, name, name2, name3, minus1, minus2, minus3);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            UpdateAndButton1(1, sender);
        }
        private void UpdateListBox5(object sender, EventArgs e)
        {
            UpdateAndButton1(0, sender);
        }
        private void VisableReset(List<MaterialTextBox2> Boxs)
        {
            foreach (var box in Boxs)
            {
                if (!box.Visible)
                    box.Text = "0";
            }
        }
        private void UpdateAndButton1(int prov, object sender)
        {
            if (string.IsNullOrWhiteSpace(NameComboBoxOtchet.Text))
            {
                MessageBox.Show("Вы забыли имя");
                return;
            }
            CheakPlsHaurs();
            List<MaterialTextBox2> _listHaursBox = new List<MaterialTextBox2> { materialTextBox1, materialTextBox24, Hours3 };
            VisableReset(_listHaursBox);

            SetDefaultValuesAndTextBoxes(materialTextBox1, materialTextBox24, textBox2, textBox3, textBox4, textBox5, textBox7);

            var values = GetValues();

            if (prov == 1 && Minus1.Visible == true)
            {
                if (-1 * (values.minus1 + values.minus2) != values.minus)
                {
                    MessageBox.Show($"ваша сумма минуса не равна общему минусу, вами указанно {-1 * (values.minus1 + values.minus2)}, а надо {values.minus}");
                    return;
                }
            }
            UpdateTextBoxes2(textBox2, textBox3, textBox4, textBox5, textBox7);
            listBox1.Items.Clear();
            PopulateListBox(values.nalf, values.bnf, values.viruchka, values.minus, values.zp4, values.itog, values.seyfEnd, values.nalp, values.bnp);
        }
        private void PopulateListBox(int nalf, int bnf, int viruchka, int minus, int zp4, int itog, int seyfEnd, int nalp, int bnp)
        {
            string name = NameComboBoxOtchet.Text;

            if (!string.IsNullOrWhiteSpace(SecondComboBoxNameOtchet.Text) && SecondComboBoxNameOtchet.Text != "нет")
                name = $"{name}/{SecondComboBoxNameOtchet.Text}";
            if (!string.IsNullOrWhiteSpace(ThertyComboBox.Text) && ThertyComboBox.Text != "нет")
                name = $"{name}/{ThertyComboBox.Text}";

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
            var values = GetValues();
            if (Minus1.Visible)
            {
                if (-1 * (values.minus1 + values.minus2) != values.minus)
                {
                    MessageBox.Show($"ваша сумма минуса не равна общему минусу, вами указанно {-1 * (values.minus1 + values.minus2)}, а надо {values.minus}");
                    return;
                }
            }
            string selectedName = NameComboBoxOtchet.SelectedItem?.ToString();
            long userId = users[selectedName];
            int TredID = 2;

            var zp1 = $"{DateTime.Now:yyyy.MM.dd}\n+{values.zarp1}p";
            var zp2 = $"{DateTime.Now:yyyy.MM.dd}\n+{values.zarp2}p";

            StringBuilder listBox1StringBuilder = new StringBuilder();
            listBox1.Invoke((MethodInvoker)delegate
            {
                foreach (var item in listBox1.Items)
                    listBox1StringBuilder.AppendLine(item.ToString());
            });
            await Telegrame.ProcessUpdates(userId, TredID, selectedName, listBox2, Отправить);
            await Telegrame.SendMessageAsync(zp1, zp2, values.name, values.name2, values.name3, Отправить);

            await bot.SendTextMessageAsync(forwardChatId, listBox1StringBuilder.ToString(), replyToMessageId: TredID);

            await ExcelHelper.UpdateExcel(values.viruchka);
            await ExcelHelper.ScreenExcel(filePath);
            await ExcelHelper.ZPexcelОтчет(values.zarp1, values.zarp2, values.zarp3, NameComboBoxOtchet, SecondComboBoxNameOtchet, ThertyComboBox, Minus2, Minus3);
            int Seyf = values.nalf - 1000;
            await ExcelHelper.SeyfMinus(Seyf);
            LoudAuto();
            foreach (var i in _labelPopravka)
            {
                i.Checked = false;
            }
            return;
        }

        private async void LoudAuto()
        {
            await ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel, 0);
            await ExcelHelper.ExcelViewer(dataGridViewZp, ZpExcelSheet, 1);
            await ExcelHelper.ExcelViewer(dataGridViewSeyfExcel, SeyfExcelBox, 3);
        }

        private void SetDefaultValuesAndTextBoxes(MaterialTextBox2 textBox1, MaterialTextBox2 textBox2, params MaterialTextBox2[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                    textBox.Text = "";
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
                textBox1.Text = "";
            if (string.IsNullOrWhiteSpace(textBox2.Text))
                textBox2.Text = "";
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
                    textBox.Text = "";
            }
        }

        private async void PlusSeyf_Click(object sender, EventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(materialTextBox21.Text)) || (string.IsNullOrWhiteSpace(materialTextBox22.Text)))
            {
                MessageBox.Show("Поля не заполнены");
                materialCheckbox1.Checked = false; // Снимаем галочку
                return; // Прекращаем выполнение метода, если поле не заполнено
            }
            var value = CalculetMessagePageSeyfPlus();

            await bot.SendTextMessageAsync(forwardChatId, value.message, replyToMessageId: 2);
            await ExcelHelper.SeyfMinus(value.Qwerty2);
            PlusSeyf.Enabled = false;
            await Task.Delay(5000);
            PlusSeyf.Enabled = true;
            return;
        }
        private void Update3(object sender, EventArgs e)
        {
            UpdateListBox2();
        }
        private void UpdateListBox2()
        {
            var value = CalculetMessagePageSeyfPlus();

            listBox3.Items.Clear();
            listBox3.Items.Add($"{value.message}");
        }
        private (string message, int Qwerty2) CalculetMessagePageSeyfPlus()
        {
            string Qwerty = materialTextBox21.Text;
            int Qwerty2 = int.Parse(materialTextBox22.Text);
            string message = $"+{Qwerty2} {Qwerty}";
            return (message, Qwerty2);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            materialTabControl1.SelectedTab = AutherPage;
            await ExcelHelper.ExcelCreated();
            await ExcelHelper.LoadSheet(comboBoxExcel, 0);
            await ExcelHelper.LoadSheet(ZpExcelSheet, 1);
            await ExcelHelper.LoadSheet(SeyfExcelBox, 3);
            VisibleBox(false);
            VisibleBox3(false);
            ResetHours();
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
            UserDataLoader.LoadBttn(dataGridViewJson);
            AvansCheack.Checked = true;
            return;
        }

        private void ComboBoxExcel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Проверяем выбранное имя листа
            string selectedSheetName = comboBoxExcel.SelectedItem.ToString();
            // Загружаем данные из выбранного листа Excel в DataGridView
            ExcelHelper.LoadGrindSheet(dataGridViewExcel, selectedSheetName, 0);
        }
        private void ОткрытьИтог_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel, 0);
            foreach (var i in _labelPopravka)
            {
                i.Checked = false;
            };
        }
        private void PravkaCheackBox_CheckedChanged(object sender, EventArgs e)
        {
            ChekedPopravka(dataGridViewExcel, PopravkaDa);
        }
        private void BtnSaveExcel_Click(object sender, EventArgs e)
        {
            string selectedSheetName = comboBoxExcel.SelectedItem.ToString();
            ExcelHelper.SaveDataToExcel(selectedSheetName, dataGridViewExcel, 0);
            ExcelHelper.ExcelViewer(dataGridViewExcel, comboBoxExcel, 0);
            foreach (var i in _labelPopravka)
            {
                i.Checked = false;
            }
        }


        private async void Штраф_Click(object sender, EventArgs e)
        {
            Аванс.Enabled = false;
            if (string.IsNullOrEmpty(WhoVipisl.Text) || WhoVipisl.Text == "нет")
            {
                MessageBox.Show("Укажите кто выписывает");
                return;
            }
            var messag = string.Join(Environment.NewLine, listBox4.Items.Cast<string>());
            var name = KtoVipisal.Text;
            var syu = materialTextBox2.Text;
            var nameVipi = WhoVipisl.Text;
            var message = $"Выписал: {nameVipi}\n\n{name}\n\n{messag}\n\n-{syu}";

            await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: 3);

            if (names.ContainsKey(name))
                await bot.SendTextMessageAsync(chatID, message, replyToMessageId: names[name]);
            Аванс.Enabled = true;
        }

        private async void Аванс_Click(object sender, EventArgs e)
        {
            string name = materialComboBox2.Text; // Инициализирует имя
            if (stopsAvans.Contains(name)) // Проверяет имя в стопе
            {
                MessageBox.Show("Пока что вы в стопе.");
                return;
            }
            Аванс.Enabled = false; // Блокирует кнопку, чтобы повторно не нажали

            // Переводит CheckBox'ы в переменные
            bool premia = Premia.Checked;
            bool avans = AvansCheack.Checked;
            bool zp = ZPcheak.Checked;
            bool minusPoSeyf = MinusPoSeyf.Checked;
            bool isAvans = false;

            // Определяет тип выписывания по CheckBox'ам
            string type = avans ? "Аванс" : zp ? "ЗП" : minusPoSeyf ? "Был минус по сейфу у" : premia ? "Премия" : "";

            if (!int.TryParse(materialTextBox23.Text, out int summ))
            {
                MessageBox.Show("Неверная сумма.");
                Аванс.Enabled = true;
                return;
            }

            if (!premia) // Если не премия, то будет минус
                summ *= -1;

            var message = $"{summ} {type} {name}"; // Составление сообщения для Telegram
            if (premia)
                message = $"+{summ} {type} {name}";

            string comm = $"{type} {name}"; // Составление сообщения для Excel

            try
            {
                if (!BnAvansCheck.Checked && !premia) // Выписывает с сейфа, если не Б/Н и не премия
                {
                    await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: 2);
                    await ExcelHelper.SeyfMinus(summ);
                }
                if (names.ContainsKey(name))
                {
                    await bot.SendTextMessageAsync(chatID, message, replyToMessageId: names[name]);
                }

                if (avans || zp)
                    isAvans = true;
                await ExcelHelper.AddRecordToExcel(summ, comm, isAvans, name); // Добавили параметр name
                await ExcelHelper.AvansMinus(summ, name, premia);

                LoudAuto();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                Аванс.Enabled = true;
            }
        }

        private async void Расход_Click(object sender, EventArgs e)
        {
            foreach (var i in _labelPopravka)
            {
                i.Checked = false;
            }
            if (string.IsNullOrWhiteSpace(materialTextBox25.Text) || string.IsNullOrWhiteSpace(materialTextBox26.Text) || string.IsNullOrWhiteSpace(comboBoxNameRas.Text))
            {
                MessageBox.Show("Вы заполнели не все поля");

                return;
            }
            Аванс.Enabled = false;
            string selectedName = comboBoxNameRas.SelectedItem?.ToString();
            long userId = users[selectedName];
            int TredID = 22513;

            if (PostavkaRashod.Checked)
            {
                TredID = 21;
            }

            int.TryParse(materialTextBox25.Text, out int summ);

            string message = $"{summ} {materialTextBox26.Text}";
            string comm = $"{materialTextBox26.Text}";
            // Call the method
            if (PhotoMessageRashod.Checked)
            {
                await Telegrame.ProcessUpdates(userId, TredID, selectedName, listBoxRas, Расход);
            }

            await ExcelHelper.AddRecordToExcel(summ, comm, false, "false");
            await bot.SendTextMessageAsync(forwardChatId, message, replyToMessageId: TredID);
            if (SeyfRasHod.Checked)
            {
                if (summ > 0)
                    summ *= -1;
                var messag = $"{summ} {materialTextBox26.Text}";
                await bot.SendTextMessageAsync(forwardChatId, messag, replyToMessageId: 2);
                await ExcelHelper.SeyfMinus(summ);
            }
            LoudAuto();
            Аванс.Enabled = true;
        }

        private async void InventBTN_Click(object sender, EventArgs e)
        {
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
            Аванс.Enabled = true;
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
            Telegrame.SendMessageToSelectedNames(selectedNames, message);
            await ExcelHelper.ЗаполнениеExcelInvent(result, listBoxNameInv);
            Аванс.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Проверяем, действительно ли пользователь хочет закрыть программу
            DialogResult result = MessageBox.Show("Вы уверены, что хотите закрыть программу?", "Подтверждение закрытия", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Если пользователь выбрал "Нет", отменяем закрытие программы
            if (result == DialogResult.No)
            {
                e.Cancel = true; // Отменяем закрытие формы
            }
        }

        private void ZpVeiw_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExcelViewer(dataGridViewZp, ZpExcelSheet, 1);
        }
        private void ZpSave_Click(object sender, EventArgs e)
        {
            string selectedSheetName = ZpExcelSheet.SelectedItem.ToString();
            ExcelHelper.SaveDataToExcel(selectedSheetName, dataGridViewZp, 1);
            ExcelHelper.ExcelViewer(dataGridViewZp, ZpExcelSheet, 1);
            ZpPopravka.Checked = false;
        }
        private void ZpExcelSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSheetName = ZpExcelSheet.SelectedItem.ToString();
            // Загружаем данные из выбранного листа Excel в DataGridView
            ExcelHelper.LoadGrindSheet(dataGridViewZp, selectedSheetName, 1);
        }
        private void ZpPopravka_CheckedChanged(object sender, EventArgs e)
        {
            ChekedPopravka(dataGridViewZp, ZpPopravka);
        }

        private void LoadSeyfExcel_Click(object sender, EventArgs e)
        {
            ExcelHelper.ExcelViewer(dataGridViewSeyfExcel, SeyfExcelBox, 3);
        }
        private void SaveSeyfExcel_Click(object sender, EventArgs e)
        {
            string selectedSheetName = SeyfExcelBox.SelectedItem.ToString();
            ExcelHelper.SaveDataToExcel(selectedSheetName, dataGridViewSeyfExcel, 3);
            ExcelHelper.ExcelViewer(dataGridViewSeyfExcel, SeyfExcelBox, 3);
            PopravkaSeyf.Checked = false;
        }
        private void SeyfExcelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSheetName = SeyfExcelBox.SelectedItem.ToString();
            // Загружаем данные из выбранного листа Excel в DataGridView
            ExcelHelper.LoadGrindSheet(dataGridViewSeyfExcel, selectedSheetName, 3);
        }
        private void PopravkaSeyf_CheckedChanged(object sender, EventArgs e)
        {
            ChekedPopravka(dataGridViewSeyfExcel, PopravkaSeyf);
        }

        private void ChekedPopravka(DataGridView dataGrid, MaterialCheckbox checkbox)
        {
            if (checkbox.Checked == true)
            {
                dataGrid.ReadOnly = false;
            }
            else
            {
                dataGrid.ReadOnly = true;
            }
        }
    }
}