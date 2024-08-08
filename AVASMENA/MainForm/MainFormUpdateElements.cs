using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AVASMENA
{
    partial class MainForm
    {
        //запуск логирования при нажатие на Энтр
        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Logining();
        }

        //один чекбокс среди своих
        private void CheckBoxUpdateInPagesAvans(object sender, List<CheckBox> list)
        {
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
        //ввод только цыфр
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

    }
}
