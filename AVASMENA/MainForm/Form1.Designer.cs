using MaterialSkin;
using MaterialSkin.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AVASMENA
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.materialButton2 = new MaterialSkin.Controls.MaterialButton();
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.AutherPage = new System.Windows.Forms.TabPage();
            this.PasswordTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            this.AuthBtn = new MaterialSkin.Controls.MaterialButton();
            this.LoginBox = new MaterialSkin.Controls.MaterialComboBox();
            this.OtchetPage = new System.Windows.Forms.TabPage();
            this.label22 = new System.Windows.Forms.Label();
            this.Minus3 = new MaterialSkin.Controls.MaterialTextBox2();
            this.ThertyComboBox = new MaterialSkin.Controls.MaterialComboBox();
            this.Hours3 = new MaterialSkin.Controls.MaterialTextBox2();
            this.Расчитать = new MaterialSkin.Controls.MaterialButton();
            this.Отправить = new MaterialSkin.Controls.MaterialButton();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.Minus2 = new MaterialSkin.Controls.MaterialTextBox2();
            this.Minus1 = new MaterialSkin.Controls.MaterialTextBox2();
            this.label18 = new System.Windows.Forms.Label();
            this.materialTextBox1 = new MaterialSkin.Controls.MaterialTextBox2();
            this.SecondComboBoxNameOtchet = new MaterialSkin.Controls.MaterialComboBox();
            this.materialTextBox24 = new MaterialSkin.Controls.MaterialTextBox2();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.NameComboBoxOtchet = new MaterialSkin.Controls.MaterialComboBox();
            this.TextBox8 = new MaterialSkin.Controls.MaterialTextBox2();
            this.textBox7 = new MaterialSkin.Controls.MaterialTextBox2();
            this.textBox5 = new MaterialSkin.Controls.MaterialTextBox2();
            this.textBox4 = new MaterialSkin.Controls.MaterialTextBox2();
            this.textBox3 = new MaterialSkin.Controls.MaterialTextBox2();
            this.textBox2 = new MaterialSkin.Controls.MaterialTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AvansPage = new System.Windows.Forms.TabPage();
            this.Otpusknie = new MaterialSkin.Controls.MaterialCheckbox();
            this.BnAvansCheck = new MaterialSkin.Controls.MaterialCheckbox();
            this.ZPcheak = new MaterialSkin.Controls.MaterialCheckbox();
            this.Premia = new MaterialSkin.Controls.MaterialCheckbox();
            this.AvansCheack = new MaterialSkin.Controls.MaterialCheckbox();
            this.MinusPoSeyf = new MaterialSkin.Controls.MaterialCheckbox();
            this.Аванс = new MaterialSkin.Controls.MaterialButton();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.materialTextBox23 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialComboBox2 = new MaterialSkin.Controls.MaterialComboBox();
            this.SeyfPlusPage = new System.Windows.Forms.TabPage();
            this.materialCheckbox1 = new MaterialSkin.Controls.MaterialCheckbox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.PlusSeyf = new MaterialSkin.Controls.MaterialButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.materialTextBox22 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox21 = new MaterialSkin.Controls.MaterialTextBox2();
            this.RashodPage = new System.Windows.Forms.TabPage();
            this.PhotoMessageRashod = new MaterialSkin.Controls.MaterialCheckbox();
            this.SeyfRasHod = new MaterialSkin.Controls.MaterialCheckbox();
            this.PostavkaRashod = new MaterialSkin.Controls.MaterialCheckbox();
            this.listBoxRas = new System.Windows.Forms.ListBox();
            this.comboBoxNameRas = new MaterialSkin.Controls.MaterialComboBox();
            this.Расход = new MaterialSkin.Controls.MaterialButton();
            this.label21 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.materialTextBox26 = new MaterialSkin.Controls.MaterialTextBox2();
            this.materialTextBox25 = new MaterialSkin.Controls.MaterialTextBox2();
            this.ShtraphPage = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.WhoVipisl = new MaterialSkin.Controls.MaterialComboBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.materialTextBox2 = new MaterialSkin.Controls.MaterialTextBox();
            this.Штраф = new MaterialSkin.Controls.MaterialButton();
            this.listBox5 = new System.Windows.Forms.ListBox();
            this.KomuVipisal = new MaterialSkin.Controls.MaterialComboBox();
            this.InventPage = new System.Windows.Forms.TabPage();
            this.listBoxNameInv = new System.Windows.Forms.ListBox();
            this.InventBTN = new MaterialSkin.Controls.MaterialButton();
            this.InventSum = new MaterialSkin.Controls.MaterialTextBox2();
            this.ExitBtn = new MaterialSkin.Controls.MaterialButton();
            this.materialTabControl1.SuspendLayout();
            this.AutherPage.SuspendLayout();
            this.OtchetPage.SuspendLayout();
            this.AvansPage.SuspendLayout();
            this.SeyfPlusPage.SuspendLayout();
            this.RashodPage.SuspendLayout();
            this.ShtraphPage.SuspendLayout();
            this.InventPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialButton2
            // 
            resources.ApplyResources(this.materialButton2, "materialButton2");
            this.materialButton2.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.materialButton2.Depth = 0;
            this.materialButton2.HighEmphasis = true;
            this.materialButton2.Icon = null;
            this.materialButton2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialButton2.Name = "materialButton2";
            this.materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            this.materialButton2.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.materialButton2.UseAccentColor = false;
            this.materialButton2.UseVisualStyleBackColor = true;
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.CharacterCasing = MaterialSkin.Controls.MaterialTabSelector.CustomCharacterCasing.Normal;
            this.materialTabSelector1.Depth = 0;
            resources.ApplyResources(this.materialTabSelector1, "materialTabSelector1");
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.AutherPage);
            this.materialTabControl1.Controls.Add(this.OtchetPage);
            this.materialTabControl1.Controls.Add(this.AvansPage);
            this.materialTabControl1.Controls.Add(this.SeyfPlusPage);
            this.materialTabControl1.Controls.Add(this.RashodPage);
            this.materialTabControl1.Controls.Add(this.ShtraphPage);
            this.materialTabControl1.Controls.Add(this.InventPage);
            this.materialTabControl1.Depth = 0;
            resources.ApplyResources(this.materialTabControl1, "materialTabControl1");
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Multiline = true;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            // 
            // AutherPage
            // 
            this.AutherPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.AutherPage.Controls.Add(this.PasswordTextBox);
            this.AutherPage.Controls.Add(this.AuthBtn);
            this.AutherPage.Controls.Add(this.LoginBox);
            resources.ApplyResources(this.AutherPage, "AutherPage");
            this.AutherPage.Name = "AutherPage";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.AnimateReadOnly = false;
            resources.ApplyResources(this.PasswordTextBox, "PasswordTextBox");
            this.PasswordTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.PasswordTextBox.Depth = 0;
            this.PasswordTextBox.HideSelection = true;
            this.PasswordTextBox.LeadingIcon = null;
            this.PasswordTextBox.MaxLength = 32767;
            this.PasswordTextBox.MouseState = MaterialSkin.MouseState.OUT;
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '●';
            this.PasswordTextBox.ReadOnly = false;
            this.PasswordTextBox.SelectedText = "";
            this.PasswordTextBox.SelectionLength = 0;
            this.PasswordTextBox.SelectionStart = 0;
            this.PasswordTextBox.ShortcutsEnabled = true;
            this.PasswordTextBox.TabStop = false;
            this.PasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.PasswordTextBox.TrailingIcon = null;
            this.PasswordTextBox.UseSystemPasswordChar = true;
            this.PasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PasswordTextBox_KeyDown);
            // 
            // AuthBtn
            // 
            resources.ApplyResources(this.AuthBtn, "AuthBtn");
            this.AuthBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.AuthBtn.Depth = 0;
            this.AuthBtn.HighEmphasis = true;
            this.AuthBtn.Icon = null;
            this.AuthBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.AuthBtn.Name = "AuthBtn";
            this.AuthBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.AuthBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AuthBtn.UseAccentColor = false;
            this.AuthBtn.UseVisualStyleBackColor = true;
            this.AuthBtn.Click += new System.EventHandler(this.AuthBtn_Click);
            // 
            // LoginBox
            // 
            this.LoginBox.AutoResize = false;
            this.LoginBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.LoginBox.Depth = 0;
            this.LoginBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.LoginBox.DropDownHeight = 174;
            this.LoginBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LoginBox.DropDownWidth = 121;
            resources.ApplyResources(this.LoginBox, "LoginBox");
            this.LoginBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LoginBox.FormattingEnabled = true;
            this.LoginBox.MouseState = MaterialSkin.MouseState.OUT;
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.StartIndex = 0;
            this.LoginBox.SelectedIndexChanged += new System.EventHandler(this.LoginBox_SelectedIndex);
            // 
            // OtchetPage
            // 
            this.OtchetPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.OtchetPage, "OtchetPage");
            this.OtchetPage.Controls.Add(this.label22);
            this.OtchetPage.Controls.Add(this.Minus3);
            this.OtchetPage.Controls.Add(this.ThertyComboBox);
            this.OtchetPage.Controls.Add(this.Hours3);
            this.OtchetPage.Controls.Add(this.Расчитать);
            this.OtchetPage.Controls.Add(this.Отправить);
            this.OtchetPage.Controls.Add(this.label20);
            this.OtchetPage.Controls.Add(this.label19);
            this.OtchetPage.Controls.Add(this.Minus2);
            this.OtchetPage.Controls.Add(this.Minus1);
            this.OtchetPage.Controls.Add(this.label18);
            this.OtchetPage.Controls.Add(this.materialTextBox1);
            this.OtchetPage.Controls.Add(this.SecondComboBoxNameOtchet);
            this.OtchetPage.Controls.Add(this.materialTextBox24);
            this.OtchetPage.Controls.Add(this.listBox2);
            this.OtchetPage.Controls.Add(this.listBox1);
            this.OtchetPage.Controls.Add(this.NameComboBoxOtchet);
            this.OtchetPage.Controls.Add(this.TextBox8);
            this.OtchetPage.Controls.Add(this.textBox7);
            this.OtchetPage.Controls.Add(this.textBox5);
            this.OtchetPage.Controls.Add(this.textBox4);
            this.OtchetPage.Controls.Add(this.textBox3);
            this.OtchetPage.Controls.Add(this.textBox2);
            this.OtchetPage.Controls.Add(this.label8);
            this.OtchetPage.Controls.Add(this.label7);
            this.OtchetPage.Controls.Add(this.label5);
            this.OtchetPage.Controls.Add(this.label4);
            this.OtchetPage.Controls.Add(this.label3);
            this.OtchetPage.Controls.Add(this.label2);
            this.OtchetPage.Controls.Add(this.label1);
            this.OtchetPage.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.OtchetPage.Name = "OtchetPage";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.Color.LimeGreen;
            this.label22.Name = "label22";
            // 
            // Minus3
            // 
            this.Minus3.AnimateReadOnly = false;
            resources.ApplyResources(this.Minus3, "Minus3");
            this.Minus3.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.Minus3.Depth = 0;
            this.Minus3.HideSelection = true;
            this.Minus3.LeadingIcon = null;
            this.Minus3.MaxLength = 32767;
            this.Minus3.MouseState = MaterialSkin.MouseState.OUT;
            this.Minus3.Name = "Minus3";
            this.Minus3.PasswordChar = '\0';
            this.Minus3.ReadOnly = false;
            this.Minus3.SelectedText = "";
            this.Minus3.SelectionLength = 0;
            this.Minus3.SelectionStart = 0;
            this.Minus3.ShortcutsEnabled = true;
            this.Minus3.TabStop = false;
            this.Minus3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Minus3.TrailingIcon = null;
            this.Minus3.UseSystemPasswordChar = false;
            // 
            // ThertyComboBox
            // 
            this.ThertyComboBox.AutoResize = false;
            this.ThertyComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ThertyComboBox.Depth = 0;
            this.ThertyComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ThertyComboBox.DropDownHeight = 174;
            this.ThertyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ThertyComboBox.DropDownWidth = 121;
            resources.ApplyResources(this.ThertyComboBox, "ThertyComboBox");
            this.ThertyComboBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ThertyComboBox.FormattingEnabled = true;
            this.ThertyComboBox.MouseState = MaterialSkin.MouseState.OUT;
            this.ThertyComboBox.Name = "ThertyComboBox";
            this.ThertyComboBox.StartIndex = 0;
            this.ThertyComboBox.SelectedIndexChanged += new System.EventHandler(this.MaterialComboBox1_SelectedIndexChanged);
            // 
            // Hours3
            // 
            this.Hours3.AnimateReadOnly = false;
            resources.ApplyResources(this.Hours3, "Hours3");
            this.Hours3.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.Hours3.Depth = 0;
            this.Hours3.HideSelection = true;
            this.Hours3.LeadingIcon = null;
            this.Hours3.MaxLength = 32767;
            this.Hours3.MouseState = MaterialSkin.MouseState.OUT;
            this.Hours3.Name = "Hours3";
            this.Hours3.PasswordChar = '\0';
            this.Hours3.ReadOnly = false;
            this.Hours3.SelectedText = "";
            this.Hours3.SelectionLength = 0;
            this.Hours3.SelectionStart = 0;
            this.Hours3.ShortcutsEnabled = true;
            this.Hours3.TabStop = false;
            this.Hours3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Hours3.TrailingIcon = null;
            this.Hours3.UseSystemPasswordChar = false;
            this.Hours3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.Hours3.TextChanged += new System.EventHandler(this.ChengedHaurs);
            // 
            // Расчитать
            // 
            resources.ApplyResources(this.Расчитать, "Расчитать");
            this.Расчитать.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Расчитать.Depth = 0;
            this.Расчитать.HighEmphasis = true;
            this.Расчитать.Icon = null;
            this.Расчитать.MouseState = MaterialSkin.MouseState.HOVER;
            this.Расчитать.Name = "Расчитать";
            this.Расчитать.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Расчитать.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Расчитать.UseAccentColor = false;
            this.Расчитать.UseVisualStyleBackColor = true;
            this.Расчитать.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Отправить
            // 
            resources.ApplyResources(this.Отправить, "Отправить");
            this.Отправить.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Отправить.Depth = 0;
            this.Отправить.HighEmphasis = true;
            this.Отправить.Icon = null;
            this.Отправить.MouseState = MaterialSkin.MouseState.HOVER;
            this.Отправить.Name = "Отправить";
            this.Отправить.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Отправить.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Отправить.UseAccentColor = false;
            this.Отправить.UseVisualStyleBackColor = true;
            this.Отправить.Click += new System.EventHandler(this.Button2_Click);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.LimeGreen;
            this.label20.Name = "label20";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.LimeGreen;
            this.label19.Name = "label19";
            // 
            // Minus2
            // 
            this.Minus2.AnimateReadOnly = false;
            resources.ApplyResources(this.Minus2, "Minus2");
            this.Minus2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.Minus2.Depth = 0;
            this.Minus2.HideSelection = true;
            this.Minus2.LeadingIcon = null;
            this.Minus2.MaxLength = 32767;
            this.Minus2.MouseState = MaterialSkin.MouseState.OUT;
            this.Minus2.Name = "Minus2";
            this.Minus2.PasswordChar = '\0';
            this.Minus2.ReadOnly = false;
            this.Minus2.SelectedText = "";
            this.Minus2.SelectionLength = 0;
            this.Minus2.SelectionStart = 0;
            this.Minus2.ShortcutsEnabled = true;
            this.Minus2.TabStop = false;
            this.Minus2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Minus2.TrailingIcon = null;
            this.Minus2.UseSystemPasswordChar = false;
            // 
            // Minus1
            // 
            this.Minus1.AnimateReadOnly = false;
            resources.ApplyResources(this.Minus1, "Minus1");
            this.Minus1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.Minus1.Depth = 0;
            this.Minus1.HideSelection = true;
            this.Minus1.LeadingIcon = null;
            this.Minus1.MaxLength = 32767;
            this.Minus1.MouseState = MaterialSkin.MouseState.OUT;
            this.Minus1.Name = "Minus1";
            this.Minus1.PasswordChar = '\0';
            this.Minus1.ReadOnly = false;
            this.Minus1.SelectedText = "";
            this.Minus1.SelectionLength = 0;
            this.Minus1.SelectionStart = 0;
            this.Minus1.ShortcutsEnabled = true;
            this.Minus1.TabStop = false;
            this.Minus1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.Minus1.TrailingIcon = null;
            this.Minus1.UseSystemPasswordChar = false;
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.Color.LimeGreen;
            this.label18.Name = "label18";
            // 
            // materialTextBox1
            // 
            this.materialTextBox1.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox1, "materialTextBox1");
            this.materialTextBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox1.Depth = 0;
            this.materialTextBox1.HideSelection = true;
            this.materialTextBox1.LeadingIcon = null;
            this.materialTextBox1.MaxLength = 32767;
            this.materialTextBox1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox1.Name = "materialTextBox1";
            this.materialTextBox1.PasswordChar = '\0';
            this.materialTextBox1.ReadOnly = false;
            this.materialTextBox1.SelectedText = "";
            this.materialTextBox1.SelectionLength = 0;
            this.materialTextBox1.SelectionStart = 0;
            this.materialTextBox1.ShortcutsEnabled = true;
            this.materialTextBox1.TabStop = false;
            this.materialTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox1.TrailingIcon = null;
            this.materialTextBox1.UseSystemPasswordChar = false;
            this.materialTextBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.materialTextBox1.TextChanged += new System.EventHandler(this.ChengedHaurs);
            // 
            // SecondComboBoxNameOtchet
            // 
            this.SecondComboBoxNameOtchet.AutoResize = false;
            this.SecondComboBoxNameOtchet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.SecondComboBoxNameOtchet.Depth = 0;
            this.SecondComboBoxNameOtchet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.SecondComboBoxNameOtchet.DropDownHeight = 174;
            this.SecondComboBoxNameOtchet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SecondComboBoxNameOtchet.DropDownWidth = 121;
            resources.ApplyResources(this.SecondComboBoxNameOtchet, "SecondComboBoxNameOtchet");
            this.SecondComboBoxNameOtchet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.SecondComboBoxNameOtchet.FormattingEnabled = true;
            this.SecondComboBoxNameOtchet.MouseState = MaterialSkin.MouseState.OUT;
            this.SecondComboBoxNameOtchet.Name = "SecondComboBoxNameOtchet";
            this.SecondComboBoxNameOtchet.StartIndex = 0;
            this.SecondComboBoxNameOtchet.SelectedIndexChanged += new System.EventHandler(this.MaterialComboBox3_SelectedIndexChanged);
            // 
            // materialTextBox24
            // 
            this.materialTextBox24.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox24, "materialTextBox24");
            this.materialTextBox24.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox24.Depth = 0;
            this.materialTextBox24.HideSelection = true;
            this.materialTextBox24.LeadingIcon = null;
            this.materialTextBox24.MaxLength = 32767;
            this.materialTextBox24.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox24.Name = "materialTextBox24";
            this.materialTextBox24.PasswordChar = '\0';
            this.materialTextBox24.ReadOnly = false;
            this.materialTextBox24.SelectedText = "";
            this.materialTextBox24.SelectionLength = 0;
            this.materialTextBox24.SelectionStart = 0;
            this.materialTextBox24.ShortcutsEnabled = true;
            this.materialTextBox24.TabStop = false;
            this.materialTextBox24.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox24.TrailingIcon = null;
            this.materialTextBox24.UseSystemPasswordChar = false;
            this.materialTextBox24.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.materialTextBox24.TextChanged += new System.EventHandler(this.ChengedHaurs);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listBox2, "listBox2");
            this.listBox2.ForeColor = System.Drawing.Color.YellowGreen;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Name = "listBox2";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.ForeColor = System.Drawing.Color.LawnGreen;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Name = "listBox1";
            // 
            // NameComboBoxOtchet
            // 
            this.NameComboBoxOtchet.AutoResize = false;
            this.NameComboBoxOtchet.BackColor = System.Drawing.Color.DarkGray;
            this.NameComboBoxOtchet.Depth = 0;
            this.NameComboBoxOtchet.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.NameComboBoxOtchet.DropDownHeight = 174;
            this.NameComboBoxOtchet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NameComboBoxOtchet.DropDownWidth = 121;
            resources.ApplyResources(this.NameComboBoxOtchet, "NameComboBoxOtchet");
            this.NameComboBoxOtchet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.NameComboBoxOtchet.FormattingEnabled = true;
            this.NameComboBoxOtchet.MouseState = MaterialSkin.MouseState.OUT;
            this.NameComboBoxOtchet.Name = "NameComboBoxOtchet";
            this.NameComboBoxOtchet.StartIndex = 0;
            // 
            // TextBox8
            // 
            this.TextBox8.AnimateReadOnly = true;
            resources.ApplyResources(this.TextBox8, "TextBox8");
            this.TextBox8.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.TextBox8.Depth = 0;
            this.TextBox8.HideSelection = true;
            this.TextBox8.LeadingIcon = null;
            this.TextBox8.LeaveOnEnterKey = true;
            this.TextBox8.MaxLength = 32767;
            this.TextBox8.MouseState = MaterialSkin.MouseState.OUT;
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.PasswordChar = '\0';
            this.TextBox8.ReadOnly = false;
            this.TextBox8.SelectedText = "";
            this.TextBox8.SelectionLength = 0;
            this.TextBox8.SelectionStart = 0;
            this.TextBox8.ShortcutsEnabled = true;
            this.TextBox8.TabStop = false;
            this.TextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextBox8.TrailingIcon = null;
            this.TextBox8.UseSystemPasswordChar = false;
            this.TextBox8.TextChanged += new System.EventHandler(this.UpdateListBox5);
            // 
            // textBox7
            // 
            this.textBox7.AnimateReadOnly = true;
            resources.ApplyResources(this.textBox7, "textBox7");
            this.textBox7.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.textBox7.Depth = 0;
            this.textBox7.HideSelection = true;
            this.textBox7.LeadingIcon = null;
            this.textBox7.LeaveOnEnterKey = true;
            this.textBox7.MaxLength = 32767;
            this.textBox7.MouseState = MaterialSkin.MouseState.OUT;
            this.textBox7.Name = "textBox7";
            this.textBox7.PasswordChar = '\0';
            this.textBox7.ReadOnly = false;
            this.textBox7.SelectedText = "";
            this.textBox7.SelectionLength = 0;
            this.textBox7.SelectionStart = 0;
            this.textBox7.ShortcutsEnabled = true;
            this.textBox7.TabStop = false;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox7.TrailingIcon = null;
            this.textBox7.UseSystemPasswordChar = false;
            this.textBox7.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox7.TextChanged += new System.EventHandler(this.UpdateListBox5);
            // 
            // textBox5
            // 
            this.textBox5.AnimateReadOnly = true;
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.textBox5.Depth = 0;
            this.textBox5.HideSelection = true;
            this.textBox5.LeadingIcon = null;
            this.textBox5.LeaveOnEnterKey = true;
            this.textBox5.MaxLength = 32767;
            this.textBox5.MouseState = MaterialSkin.MouseState.OUT;
            this.textBox5.Name = "textBox5";
            this.textBox5.PasswordChar = '\0';
            this.textBox5.ReadOnly = false;
            this.textBox5.SelectedText = "";
            this.textBox5.SelectionLength = 0;
            this.textBox5.SelectionStart = 0;
            this.textBox5.ShortcutsEnabled = true;
            this.textBox5.TabStop = false;
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox5.TrailingIcon = null;
            this.textBox5.UseSystemPasswordChar = false;
            this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox5.TextChanged += new System.EventHandler(this.UpdateListBox5);
            // 
            // textBox4
            // 
            this.textBox4.AnimateReadOnly = true;
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.textBox4.Depth = 0;
            this.textBox4.HideSelection = true;
            this.textBox4.LeadingIcon = null;
            this.textBox4.LeaveOnEnterKey = true;
            this.textBox4.MaxLength = 32767;
            this.textBox4.MouseState = MaterialSkin.MouseState.OUT;
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '\0';
            this.textBox4.ReadOnly = false;
            this.textBox4.SelectedText = "";
            this.textBox4.SelectionLength = 0;
            this.textBox4.SelectionStart = 0;
            this.textBox4.ShortcutsEnabled = true;
            this.textBox4.TabStop = false;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox4.TrailingIcon = null;
            this.textBox4.UseSystemPasswordChar = false;
            this.textBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox4.TextChanged += new System.EventHandler(this.UpdateListBox5);
            // 
            // textBox3
            // 
            this.textBox3.AnimateReadOnly = true;
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.textBox3.Depth = 0;
            this.textBox3.HideSelection = true;
            this.textBox3.LeadingIcon = null;
            this.textBox3.LeaveOnEnterKey = true;
            this.textBox3.MaxLength = 32767;
            this.textBox3.MouseState = MaterialSkin.MouseState.OUT;
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '\0';
            this.textBox3.ReadOnly = false;
            this.textBox3.SelectedText = "";
            this.textBox3.SelectionLength = 0;
            this.textBox3.SelectionStart = 0;
            this.textBox3.ShortcutsEnabled = true;
            this.textBox3.TabStop = false;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox3.TrailingIcon = null;
            this.textBox3.UseSystemPasswordChar = false;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox3.TextChanged += new System.EventHandler(this.UpdateListBox5);
            // 
            // textBox2
            // 
            this.textBox2.AnimateReadOnly = true;
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.textBox2.Depth = 0;
            this.textBox2.HideSelection = true;
            this.textBox2.LeadingIcon = null;
            this.textBox2.LeaveOnEnterKey = true;
            this.textBox2.MaxLength = 32767;
            this.textBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '\0';
            this.textBox2.ReadOnly = false;
            this.textBox2.SelectedText = "";
            this.textBox2.SelectionLength = 0;
            this.textBox2.SelectionStart = 0;
            this.textBox2.ShortcutsEnabled = true;
            this.textBox2.TabStop = false;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.textBox2.TrailingIcon = null;
            this.textBox2.UseSystemPasswordChar = false;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.textBox2.TextChanged += new System.EventHandler(this.UpdateListBox5);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.ForeColor = System.Drawing.Color.LimeGreen;
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.LimeGreen;
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.LimeGreen;
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.LimeGreen;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.LimeGreen;
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.LimeGreen;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.LimeGreen;
            this.label1.Name = "label1";
            // 
            // AvansPage
            // 
            this.AvansPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.AvansPage.Controls.Add(this.Otpusknie);
            this.AvansPage.Controls.Add(this.BnAvansCheck);
            this.AvansPage.Controls.Add(this.ZPcheak);
            this.AvansPage.Controls.Add(this.Premia);
            this.AvansPage.Controls.Add(this.AvansCheack);
            this.AvansPage.Controls.Add(this.MinusPoSeyf);
            this.AvansPage.Controls.Add(this.Аванс);
            this.AvansPage.Controls.Add(this.label17);
            this.AvansPage.Controls.Add(this.label16);
            this.AvansPage.Controls.Add(this.materialTextBox23);
            this.AvansPage.Controls.Add(this.materialComboBox2);
            resources.ApplyResources(this.AvansPage, "AvansPage");
            this.AvansPage.Name = "AvansPage";
            // 
            // Otpusknie
            // 
            resources.ApplyResources(this.Otpusknie, "Otpusknie");
            this.Otpusknie.Depth = 0;
            this.Otpusknie.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Otpusknie.MouseState = MaterialSkin.MouseState.HOVER;
            this.Otpusknie.Name = "Otpusknie";
            this.Otpusknie.ReadOnly = false;
            this.Otpusknie.Ripple = true;
            this.Otpusknie.UseVisualStyleBackColor = true;
            // 
            // BnAvansCheck
            // 
            resources.ApplyResources(this.BnAvansCheck, "BnAvansCheck");
            this.BnAvansCheck.Depth = 0;
            this.BnAvansCheck.MouseLocation = new System.Drawing.Point(-1, -1);
            this.BnAvansCheck.MouseState = MaterialSkin.MouseState.HOVER;
            this.BnAvansCheck.Name = "BnAvansCheck";
            this.BnAvansCheck.ReadOnly = false;
            this.BnAvansCheck.Ripple = true;
            this.BnAvansCheck.UseVisualStyleBackColor = true;
            // 
            // ZPcheak
            // 
            resources.ApplyResources(this.ZPcheak, "ZPcheak");
            this.ZPcheak.Depth = 0;
            this.ZPcheak.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ZPcheak.MouseState = MaterialSkin.MouseState.HOVER;
            this.ZPcheak.Name = "ZPcheak";
            this.ZPcheak.ReadOnly = false;
            this.ZPcheak.Ripple = true;
            this.ZPcheak.UseVisualStyleBackColor = true;
            this.ZPcheak.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // Premia
            // 
            resources.ApplyResources(this.Premia, "Premia");
            this.Premia.Depth = 0;
            this.Premia.MouseLocation = new System.Drawing.Point(-1, -1);
            this.Premia.MouseState = MaterialSkin.MouseState.HOVER;
            this.Premia.Name = "Premia";
            this.Premia.ReadOnly = false;
            this.Premia.Ripple = true;
            this.Premia.UseVisualStyleBackColor = true;
            this.Premia.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // AvansCheack
            // 
            resources.ApplyResources(this.AvansCheack, "AvansCheack");
            this.AvansCheack.Depth = 0;
            this.AvansCheack.MouseLocation = new System.Drawing.Point(-1, -1);
            this.AvansCheack.MouseState = MaterialSkin.MouseState.HOVER;
            this.AvansCheack.Name = "AvansCheack";
            this.AvansCheack.ReadOnly = false;
            this.AvansCheack.Ripple = true;
            this.AvansCheack.UseVisualStyleBackColor = true;
            this.AvansCheack.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // MinusPoSeyf
            // 
            resources.ApplyResources(this.MinusPoSeyf, "MinusPoSeyf");
            this.MinusPoSeyf.Depth = 0;
            this.MinusPoSeyf.MouseLocation = new System.Drawing.Point(-1, -1);
            this.MinusPoSeyf.MouseState = MaterialSkin.MouseState.HOVER;
            this.MinusPoSeyf.Name = "MinusPoSeyf";
            this.MinusPoSeyf.ReadOnly = false;
            this.MinusPoSeyf.Ripple = true;
            this.MinusPoSeyf.UseVisualStyleBackColor = true;
            this.MinusPoSeyf.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // Аванс
            // 
            resources.ApplyResources(this.Аванс, "Аванс");
            this.Аванс.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Аванс.Depth = 0;
            this.Аванс.HighEmphasis = true;
            this.Аванс.Icon = null;
            this.Аванс.MouseState = MaterialSkin.MouseState.HOVER;
            this.Аванс.Name = "Аванс";
            this.Аванс.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Аванс.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Аванс.UseAccentColor = false;
            this.Аванс.UseVisualStyleBackColor = true;
            this.Аванс.Click += new System.EventHandler(this.Аванс_Click);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.Color.LimeGreen;
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ForeColor = System.Drawing.Color.LimeGreen;
            this.label16.Name = "label16";
            // 
            // materialTextBox23
            // 
            this.materialTextBox23.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox23, "materialTextBox23");
            this.materialTextBox23.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox23.Depth = 0;
            this.materialTextBox23.HideSelection = true;
            this.materialTextBox23.LeadingIcon = null;
            this.materialTextBox23.MaxLength = 32767;
            this.materialTextBox23.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox23.Name = "materialTextBox23";
            this.materialTextBox23.PasswordChar = '\0';
            this.materialTextBox23.ReadOnly = false;
            this.materialTextBox23.SelectedText = "";
            this.materialTextBox23.SelectionLength = 0;
            this.materialTextBox23.SelectionStart = 0;
            this.materialTextBox23.ShortcutsEnabled = true;
            this.materialTextBox23.TabStop = false;
            this.materialTextBox23.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox23.TrailingIcon = null;
            this.materialTextBox23.UseSystemPasswordChar = false;
            // 
            // materialComboBox2
            // 
            this.materialComboBox2.AutoResize = false;
            this.materialComboBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.materialComboBox2.Depth = 0;
            this.materialComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.materialComboBox2.DropDownHeight = 174;
            this.materialComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialComboBox2.DropDownWidth = 121;
            resources.ApplyResources(this.materialComboBox2, "materialComboBox2");
            this.materialComboBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialComboBox2.FormattingEnabled = true;
            this.materialComboBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialComboBox2.Name = "materialComboBox2";
            this.materialComboBox2.StartIndex = 0;
            // 
            // SeyfPlusPage
            // 
            this.SeyfPlusPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.SeyfPlusPage.Controls.Add(this.materialCheckbox1);
            this.SeyfPlusPage.Controls.Add(this.listBox3);
            this.SeyfPlusPage.Controls.Add(this.PlusSeyf);
            this.SeyfPlusPage.Controls.Add(this.label11);
            this.SeyfPlusPage.Controls.Add(this.label10);
            this.SeyfPlusPage.Controls.Add(this.materialTextBox22);
            this.SeyfPlusPage.Controls.Add(this.materialTextBox21);
            resources.ApplyResources(this.SeyfPlusPage, "SeyfPlusPage");
            this.SeyfPlusPage.Name = "SeyfPlusPage";
            // 
            // materialCheckbox1
            // 
            resources.ApplyResources(this.materialCheckbox1, "materialCheckbox1");
            this.materialCheckbox1.Depth = 0;
            this.materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialCheckbox1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialCheckbox1.Name = "materialCheckbox1";
            this.materialCheckbox1.ReadOnly = false;
            this.materialCheckbox1.Ripple = true;
            this.materialCheckbox1.UseVisualStyleBackColor = true;
            this.materialCheckbox1.CheckedChanged += new System.EventHandler(this.Update3);
            // 
            // listBox3
            // 
            this.listBox3.BackColor = System.Drawing.Color.DimGray;
            this.listBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.listBox3, "listBox3");
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Name = "listBox3";
            // 
            // PlusSeyf
            // 
            resources.ApplyResources(this.PlusSeyf, "PlusSeyf");
            this.PlusSeyf.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.PlusSeyf.Depth = 0;
            this.PlusSeyf.HighEmphasis = true;
            this.PlusSeyf.Icon = null;
            this.PlusSeyf.MouseState = MaterialSkin.MouseState.HOVER;
            this.PlusSeyf.Name = "PlusSeyf";
            this.PlusSeyf.NoAccentTextColor = System.Drawing.Color.Empty;
            this.PlusSeyf.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.PlusSeyf.UseAccentColor = false;
            this.PlusSeyf.UseVisualStyleBackColor = true;
            this.PlusSeyf.Click += new System.EventHandler(this.PlusSeyf_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.LimeGreen;
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.LimeGreen;
            this.label10.Name = "label10";
            // 
            // materialTextBox22
            // 
            this.materialTextBox22.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox22, "materialTextBox22");
            this.materialTextBox22.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox22.Depth = 0;
            this.materialTextBox22.HideSelection = true;
            this.materialTextBox22.LeadingIcon = null;
            this.materialTextBox22.MaxLength = 32767;
            this.materialTextBox22.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox22.Name = "materialTextBox22";
            this.materialTextBox22.PasswordChar = '\0';
            this.materialTextBox22.ReadOnly = false;
            this.materialTextBox22.SelectedText = "";
            this.materialTextBox22.SelectionLength = 0;
            this.materialTextBox22.SelectionStart = 0;
            this.materialTextBox22.ShortcutsEnabled = true;
            this.materialTextBox22.TabStop = false;
            this.materialTextBox22.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox22.TrailingIcon = null;
            this.materialTextBox22.UseSystemPasswordChar = false;
            this.materialTextBox22.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            this.materialTextBox22.TextChanged += new System.EventHandler(this.Update3);
            // 
            // materialTextBox21
            // 
            this.materialTextBox21.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox21, "materialTextBox21");
            this.materialTextBox21.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox21.Depth = 0;
            this.materialTextBox21.HideSelection = true;
            this.materialTextBox21.LeadingIcon = null;
            this.materialTextBox21.MaxLength = 32767;
            this.materialTextBox21.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox21.Name = "materialTextBox21";
            this.materialTextBox21.PasswordChar = '\0';
            this.materialTextBox21.ReadOnly = false;
            this.materialTextBox21.SelectedText = "";
            this.materialTextBox21.SelectionLength = 0;
            this.materialTextBox21.SelectionStart = 0;
            this.materialTextBox21.ShortcutsEnabled = true;
            this.materialTextBox21.TabStop = false;
            this.materialTextBox21.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox21.TrailingIcon = null;
            this.materialTextBox21.UseSystemPasswordChar = false;
            this.materialTextBox21.TextChanged += new System.EventHandler(this.Update3);
            // 
            // RashodPage
            // 
            this.RashodPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.RashodPage.Controls.Add(this.PhotoMessageRashod);
            this.RashodPage.Controls.Add(this.SeyfRasHod);
            this.RashodPage.Controls.Add(this.PostavkaRashod);
            this.RashodPage.Controls.Add(this.listBoxRas);
            this.RashodPage.Controls.Add(this.comboBoxNameRas);
            this.RashodPage.Controls.Add(this.Расход);
            this.RashodPage.Controls.Add(this.label21);
            this.RashodPage.Controls.Add(this.label9);
            this.RashodPage.Controls.Add(this.materialTextBox26);
            this.RashodPage.Controls.Add(this.materialTextBox25);
            resources.ApplyResources(this.RashodPage, "RashodPage");
            this.RashodPage.Name = "RashodPage";
            // 
            // PhotoMessageRashod
            // 
            resources.ApplyResources(this.PhotoMessageRashod, "PhotoMessageRashod");
            this.PhotoMessageRashod.Depth = 0;
            this.PhotoMessageRashod.MouseLocation = new System.Drawing.Point(-1, -1);
            this.PhotoMessageRashod.MouseState = MaterialSkin.MouseState.HOVER;
            this.PhotoMessageRashod.Name = "PhotoMessageRashod";
            this.PhotoMessageRashod.ReadOnly = false;
            this.PhotoMessageRashod.Ripple = true;
            this.PhotoMessageRashod.UseVisualStyleBackColor = true;
            // 
            // SeyfRasHod
            // 
            resources.ApplyResources(this.SeyfRasHod, "SeyfRasHod");
            this.SeyfRasHod.Depth = 0;
            this.SeyfRasHod.MouseLocation = new System.Drawing.Point(-1, -1);
            this.SeyfRasHod.MouseState = MaterialSkin.MouseState.HOVER;
            this.SeyfRasHod.Name = "SeyfRasHod";
            this.SeyfRasHod.ReadOnly = false;
            this.SeyfRasHod.Ripple = true;
            this.SeyfRasHod.UseVisualStyleBackColor = true;
            // 
            // PostavkaRashod
            // 
            resources.ApplyResources(this.PostavkaRashod, "PostavkaRashod");
            this.PostavkaRashod.Depth = 0;
            this.PostavkaRashod.MouseLocation = new System.Drawing.Point(-1, -1);
            this.PostavkaRashod.MouseState = MaterialSkin.MouseState.HOVER;
            this.PostavkaRashod.Name = "PostavkaRashod";
            this.PostavkaRashod.ReadOnly = false;
            this.PostavkaRashod.Ripple = true;
            this.PostavkaRashod.UseVisualStyleBackColor = true;
            // 
            // listBoxRas
            // 
            resources.ApplyResources(this.listBoxRas, "listBoxRas");
            this.listBoxRas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.listBoxRas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxRas.ForeColor = System.Drawing.Color.YellowGreen;
            this.listBoxRas.FormattingEnabled = true;
            this.listBoxRas.Name = "listBoxRas";
            // 
            // comboBoxNameRas
            // 
            this.comboBoxNameRas.AutoResize = false;
            this.comboBoxNameRas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.comboBoxNameRas.Depth = 0;
            this.comboBoxNameRas.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.comboBoxNameRas.DropDownHeight = 174;
            this.comboBoxNameRas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNameRas.DropDownWidth = 121;
            resources.ApplyResources(this.comboBoxNameRas, "comboBoxNameRas");
            this.comboBoxNameRas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.comboBoxNameRas.FormattingEnabled = true;
            this.comboBoxNameRas.MouseState = MaterialSkin.MouseState.OUT;
            this.comboBoxNameRas.Name = "comboBoxNameRas";
            this.comboBoxNameRas.StartIndex = 0;
            // 
            // Расход
            // 
            resources.ApplyResources(this.Расход, "Расход");
            this.Расход.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Расход.Depth = 0;
            this.Расход.HighEmphasis = true;
            this.Расход.Icon = null;
            this.Расход.MouseState = MaterialSkin.MouseState.HOVER;
            this.Расход.Name = "Расход";
            this.Расход.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Расход.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Расход.UseAccentColor = false;
            this.Расход.UseVisualStyleBackColor = true;
            this.Расход.Click += new System.EventHandler(this.Расход_Click);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.ForeColor = System.Drawing.Color.LimeGreen;
            this.label21.Name = "label21";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.LimeGreen;
            this.label9.Name = "label9";
            // 
            // materialTextBox26
            // 
            this.materialTextBox26.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox26, "materialTextBox26");
            this.materialTextBox26.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox26.Depth = 0;
            this.materialTextBox26.HideSelection = true;
            this.materialTextBox26.LeadingIcon = null;
            this.materialTextBox26.MaxLength = 32767;
            this.materialTextBox26.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox26.Name = "materialTextBox26";
            this.materialTextBox26.PasswordChar = '\0';
            this.materialTextBox26.ReadOnly = false;
            this.materialTextBox26.SelectedText = "";
            this.materialTextBox26.SelectionLength = 0;
            this.materialTextBox26.SelectionStart = 0;
            this.materialTextBox26.ShortcutsEnabled = true;
            this.materialTextBox26.TabStop = false;
            this.materialTextBox26.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox26.TrailingIcon = null;
            this.materialTextBox26.UseSystemPasswordChar = false;
            // 
            // materialTextBox25
            // 
            this.materialTextBox25.AnimateReadOnly = false;
            resources.ApplyResources(this.materialTextBox25, "materialTextBox25");
            this.materialTextBox25.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.materialTextBox25.Depth = 0;
            this.materialTextBox25.HideSelection = true;
            this.materialTextBox25.LeadingIcon = null;
            this.materialTextBox25.MaxLength = 32767;
            this.materialTextBox25.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox25.Name = "materialTextBox25";
            this.materialTextBox25.PasswordChar = '\0';
            this.materialTextBox25.ReadOnly = false;
            this.materialTextBox25.SelectedText = "";
            this.materialTextBox25.SelectionLength = 0;
            this.materialTextBox25.SelectionStart = 0;
            this.materialTextBox25.ShortcutsEnabled = true;
            this.materialTextBox25.TabStop = false;
            this.materialTextBox25.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.materialTextBox25.TrailingIcon = null;
            this.materialTextBox25.UseSystemPasswordChar = false;
            // 
            // ShtraphPage
            // 
            this.ShtraphPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ShtraphPage.Controls.Add(this.label6);
            this.ShtraphPage.Controls.Add(this.WhoVipisl);
            this.ShtraphPage.Controls.Add(this.listBox4);
            this.ShtraphPage.Controls.Add(this.label15);
            this.ShtraphPage.Controls.Add(this.label14);
            this.ShtraphPage.Controls.Add(this.materialTextBox2);
            this.ShtraphPage.Controls.Add(this.Штраф);
            this.ShtraphPage.Controls.Add(this.listBox5);
            this.ShtraphPage.Controls.Add(this.KomuVipisal);
            this.ShtraphPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.ShtraphPage, "ShtraphPage");
            this.ShtraphPage.Name = "ShtraphPage";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label6.ForeColor = System.Drawing.Color.LimeGreen;
            this.label6.Name = "label6";
            // 
            // WhoVipisl
            // 
            this.WhoVipisl.AutoResize = false;
            this.WhoVipisl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.WhoVipisl.Depth = 0;
            this.WhoVipisl.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.WhoVipisl.DropDownHeight = 174;
            this.WhoVipisl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WhoVipisl.DropDownWidth = 121;
            resources.ApplyResources(this.WhoVipisl, "WhoVipisl");
            this.WhoVipisl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.WhoVipisl.FormattingEnabled = true;
            this.WhoVipisl.MouseState = MaterialSkin.MouseState.OUT;
            this.WhoVipisl.Name = "WhoVipisl";
            this.WhoVipisl.StartIndex = 0;
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            resources.ApplyResources(this.listBox4, "listBox4");
            this.listBox4.Name = "listBox4";
            this.listBox4.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox4.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.Color.LimeGreen;
            this.label15.Name = "label15";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.LimeGreen;
            this.label14.Name = "label14";
            // 
            // materialTextBox2
            // 
            this.materialTextBox2.AnimateReadOnly = false;
            this.materialTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialTextBox2.Depth = 0;
            resources.ApplyResources(this.materialTextBox2, "materialTextBox2");
            this.materialTextBox2.LeadingIcon = null;
            this.materialTextBox2.MouseState = MaterialSkin.MouseState.OUT;
            this.materialTextBox2.Name = "materialTextBox2";
            this.materialTextBox2.TrailingIcon = null;
            // 
            // Штраф
            // 
            resources.ApplyResources(this.Штраф, "Штраф");
            this.Штраф.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.Штраф.Depth = 0;
            this.Штраф.HighEmphasis = true;
            this.Штраф.Icon = null;
            this.Штраф.MouseState = MaterialSkin.MouseState.HOVER;
            this.Штраф.Name = "Штраф";
            this.Штраф.NoAccentTextColor = System.Drawing.Color.Empty;
            this.Штраф.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.Штраф.UseAccentColor = false;
            this.Штраф.UseVisualStyleBackColor = true;
            this.Штраф.Click += new System.EventHandler(this.Штраф_Click);
            // 
            // listBox5
            // 
            this.listBox5.FormattingEnabled = true;
            resources.ApplyResources(this.listBox5, "listBox5");
            this.listBox5.Name = "listBox5";
            this.listBox5.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox5.TabStop = false;
            this.listBox5.SelectedIndexChanged += new System.EventHandler(this.ListBox5_SelectedIndexChanged);
            // 
            // KomuVipisal
            // 
            this.KomuVipisal.AutoResize = false;
            this.KomuVipisal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.KomuVipisal.Depth = 0;
            this.KomuVipisal.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.KomuVipisal.DropDownHeight = 174;
            this.KomuVipisal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.KomuVipisal.DropDownWidth = 121;
            resources.ApplyResources(this.KomuVipisal, "KomuVipisal");
            this.KomuVipisal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.KomuVipisal.FormattingEnabled = true;
            this.KomuVipisal.MouseState = MaterialSkin.MouseState.OUT;
            this.KomuVipisal.Name = "KomuVipisal";
            this.KomuVipisal.StartIndex = 0;
            // 
            // InventPage
            // 
            this.InventPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.InventPage.Controls.Add(this.listBoxNameInv);
            this.InventPage.Controls.Add(this.InventBTN);
            this.InventPage.Controls.Add(this.InventSum);
            this.InventPage.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.InventPage, "InventPage");
            this.InventPage.Name = "InventPage";
            // 
            // listBoxNameInv
            // 
            resources.ApplyResources(this.listBoxNameInv, "listBoxNameInv");
            this.listBoxNameInv.FormattingEnabled = true;
            this.listBoxNameInv.Name = "listBoxNameInv";
            // 
            // InventBTN
            // 
            resources.ApplyResources(this.InventBTN, "InventBTN");
            this.InventBTN.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.InventBTN.Depth = 0;
            this.InventBTN.HighEmphasis = true;
            this.InventBTN.Icon = null;
            this.InventBTN.MouseState = MaterialSkin.MouseState.HOVER;
            this.InventBTN.Name = "InventBTN";
            this.InventBTN.NoAccentTextColor = System.Drawing.Color.Empty;
            this.InventBTN.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.InventBTN.UseAccentColor = false;
            this.InventBTN.UseVisualStyleBackColor = true;
            this.InventBTN.Click += new System.EventHandler(this.InventBTN_Click);
            // 
            // InventSum
            // 
            this.InventSum.AnimateReadOnly = false;
            resources.ApplyResources(this.InventSum, "InventSum");
            this.InventSum.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            this.InventSum.Depth = 0;
            this.InventSum.HideSelection = true;
            this.InventSum.LeadingIcon = null;
            this.InventSum.MaxLength = 32767;
            this.InventSum.MouseState = MaterialSkin.MouseState.OUT;
            this.InventSum.Name = "InventSum";
            this.InventSum.PasswordChar = '\0';
            this.InventSum.ReadOnly = false;
            this.InventSum.SelectedText = "";
            this.InventSum.SelectionLength = 0;
            this.InventSum.SelectionStart = 0;
            this.InventSum.ShortcutsEnabled = true;
            this.InventSum.TabStop = false;
            this.InventSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.InventSum.TrailingIcon = null;
            this.InventSum.UseSystemPasswordChar = false;
            // 
            // ExitBtn
            // 
            this.ExitBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ExitBtn.AllowDrop = true;
            resources.ApplyResources(this.ExitBtn, "ExitBtn");
            this.ExitBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ExitBtn.CausesValidation = false;
            this.ExitBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.ExitBtn.Depth = 0;
            this.ExitBtn.DrawShadows = false;
            this.ExitBtn.ForeColor = System.Drawing.Color.White;
            this.ExitBtn.HighEmphasis = true;
            this.ExitBtn.Icon = null;
            this.ExitBtn.MouseState = MaterialSkin.MouseState.HOVER;
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.NoAccentTextColor = System.Drawing.Color.Empty;
            this.ExitBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Text;
            this.ExitBtn.UseAccentColor = true;
            this.ExitBtn.UseVisualStyleBackColor = false;
            this.ExitBtn.Click += new System.EventHandler(this.ExitBtn_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.AuthBtn;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.materialTabSelector1);
            this.Controls.Add(this.materialTabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.TransparencyKey = System.Drawing.Color.LightGoldenrodYellow;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.AutherPage.ResumeLayout(false);
            this.AutherPage.PerformLayout();
            this.OtchetPage.ResumeLayout(false);
            this.OtchetPage.PerformLayout();
            this.AvansPage.ResumeLayout(false);
            this.AvansPage.PerformLayout();
            this.SeyfPlusPage.ResumeLayout(false);
            this.SeyfPlusPage.PerformLayout();
            this.RashodPage.ResumeLayout(false);
            this.RashodPage.PerformLayout();
            this.ShtraphPage.ResumeLayout(false);
            this.ShtraphPage.PerformLayout();
            this.InventPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialButton materialButton2;
        private MaterialButton ExitBtn;
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialTabControl materialTabControl1;
        private TabPage AutherPage;
        private MaterialTextBox2 PasswordTextBox;
        private MaterialButton AuthBtn;
        private MaterialComboBox LoginBox;
        private TabPage OtchetPage;
        private Label label22;
        private MaterialTextBox2 Minus3;
        private MaterialComboBox ThertyComboBox;
        private MaterialTextBox2 Hours3;
        private MaterialButton Расчитать;
        private MaterialButton Отправить;
        private Label label20;
        private Label label19;
        private MaterialTextBox2 Minus2;
        private MaterialTextBox2 Minus1;
        private Label label18;
        private MaterialTextBox2 materialTextBox1;
        private MaterialComboBox SecondComboBoxNameOtchet;
        private MaterialTextBox2 materialTextBox24;
        private ListBox listBox2;
        private ListBox listBox1;
        private MaterialComboBox NameComboBoxOtchet;
        private MaterialTextBox2 TextBox8;
        private MaterialTextBox2 textBox7;
        private MaterialTextBox2 textBox5;
        private MaterialTextBox2 textBox4;
        private MaterialTextBox2 textBox3;
        private MaterialTextBox2 textBox2;
        private Label label8;
        private Label label7;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TabPage AvansPage;
        private MaterialCheckbox BnAvansCheck;
        private MaterialCheckbox ZPcheak;
        private MaterialCheckbox Premia;
        private MaterialCheckbox AvansCheack;
        private MaterialCheckbox MinusPoSeyf;
        private MaterialButton Аванс;
        private Label label17;
        private Label label16;
        private MaterialTextBox2 materialTextBox23;
        private MaterialComboBox materialComboBox2;
        private TabPage SeyfPlusPage;
        private MaterialCheckbox materialCheckbox1;
        private ListBox listBox3;
        private MaterialButton PlusSeyf;
        private Label label11;
        private Label label10;
        private MaterialTextBox2 materialTextBox22;
        private MaterialTextBox2 materialTextBox21;
        private TabPage RashodPage;
        private MaterialCheckbox PhotoMessageRashod;
        private MaterialCheckbox SeyfRasHod;
        private MaterialCheckbox PostavkaRashod;
        private ListBox listBoxRas;
        private MaterialComboBox comboBoxNameRas;
        private MaterialButton Расход;
        private Label label21;
        private Label label9;
        private MaterialTextBox2 materialTextBox26;
        private MaterialTextBox2 materialTextBox25;
        private TabPage ShtraphPage;
        private Label label6;
        private MaterialComboBox WhoVipisl;
        private ListBox listBox4;
        private Label label15;
        private Label label14;
        private MaterialTextBox materialTextBox2;
        private MaterialButton Штраф;
        private ListBox listBox5;
        private MaterialComboBox KomuVipisal;
        private TabPage InventPage;
        private ListBox listBoxNameInv;
        private MaterialButton InventBTN;
        private MaterialTextBox2 InventSum;
        private MaterialCheckbox Otpusknie;
    }
}