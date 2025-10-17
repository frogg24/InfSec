using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class ChangePasswordDialog : Form
    {
        private string username;

        public ChangePasswordDialog(string username)
        {
            InitializeComponent();
            this.username = username;
            this.Text = $"Смена пароля для пользователя {username}";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string newPassword1 = txtNewPassword1.Text;
            string newPassword2 = txtNewPassword2.Text;

            // Проверяем старый пароль
            if (!DatabaseManager.ValidateUser(username, oldPassword))
            {
                MessageBox.Show("Неверный старый пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(newPassword1))
            {
                MessageBox.Show("Введите новый пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword1 != newPassword2)
            {
                MessageBox.Show("Новые пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем требования к паролю
            if (!ValidatePasswordRequirements(newPassword1))
            {
                MessageBox.Show("Пароль не соответствует требованиям:\n" +
                                "- Наличие цифр\n" +
                                "- Наличие знаков препинания\n" +
                                "- Наличие знаков арифметических операций",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DatabaseManager.SetUserPassword(username, newPassword1);
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidatePasswordRequirements(string password)
        {
            // Проверяем наличие цифр
            bool hasDigits = false;
            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasDigits = true;
                    break;
                }
            }

            // Проверяем наличие знаков препинания
            bool hasPunctuation = false;
            foreach (char c in password)
            {
                if (char.IsPunctuation(c))
                {
                    hasPunctuation = true;
                    break;
                }
            }

            // Проверяем наличие знаков арифметических операций
            bool hasArithmetic = false;
            foreach (char c in password)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    hasArithmetic = true;
                    break;
                }
            }

            return hasDigits && hasPunctuation && hasArithmetic;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}