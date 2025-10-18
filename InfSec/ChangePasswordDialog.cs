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
            // Запись паролей
            string oldPassword = txtOldPassword.Text;
            string newPassword1 = txtNewPassword1.Text;
            string newPassword2 = txtNewPassword2.Text;

            // Если поля заполнены не верно
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

            // Проверяем, если минимальная длина больше нуля, то проверяем длину нового пароля
            int minLength = DatabaseManager.GetUserMinLength(username);
            if (minLength > 0 && newPassword1.Length < minLength)
            {
                MessageBox.Show($"Пароль должен быть не короче {minLength} символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Если ограничения на пароль включены для пользователя
            if (DatabaseManager.GetUserRestrictions(username))
            {
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
            }
            
            // Запись нового пароля
            DatabaseManager.SetUserPassword(username, newPassword1);

            // Закрытие формы
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Метод валидации пароля по индивидуальному заданию
        /// </summary>
        /// <param name="password">Пароль для валидации</param>
        /// <returns>True, есди пароль прошел валидацию, иначе - False</returns>
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

        // Закрытие формы по клику
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}