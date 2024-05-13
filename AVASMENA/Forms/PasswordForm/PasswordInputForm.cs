using MaterialSkin.Controls;
using System.Windows.Forms;

namespace PasswordFormExample
{
    public partial class PasswordInputForm : MaterialForm
    {
        public string EnteredPassword { get; private set; }

        public PasswordInputForm()
        {
            InitializeComponent();
            // Добавляем обработчик события KeyDown для текстового поля passwordTextBox
            passwordTextBox.KeyDown += PasswordTextBox_KeyDown;
            passwordTextBox.UseSystemPasswordChar = true;
        }

        // Обработчик события KeyDown для текстового поля passwordTextBox
        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Если нажата клавиша Enter, сохраняем введенный пароль и закрываем форму
                EnteredPassword = passwordTextBox.Text;
                DialogResult = DialogResult.OK;
            }
        }
    }
}