using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace InfSec
{
    public partial class PasswordDialog : Form
    {
        public string NewPassword { get; private set; }

        public PasswordDialog()
        {
            InitializeComponent();
            this.Text = "Установка пароля";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Запись паролей
            string password1 = txtPassword1.Text;
            string password2 = txtPassword2.Text;

            // Если поля заполнены не верно
            if (string.IsNullOrEmpty(password1))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (password1 != password2)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, если минимальная длина больше нуля, то проверяем длину нового пароля
            int minLength = DatabaseManager.GetUserMinLength("ADMIN");
            if (minLength > 0 && password1.Length < minLength)
            {
                MessageBox.Show($"Пароль должен быть не короче {minLength} символов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Если ограничения для пользователя включены
            if (DatabaseManager.GetUserRestrictions("ADMIN"))
            {
                // Проверяем требования к паролю
                if (!ValidatePasswordRequirements(password1))
                {
                    MessageBox.Show("Пароль не соответствует требованиям:\n" +
                                    "- Наличие цифр\n" +
                                    "- Наличие знаков препинания\n" +
                                    "- Наличие знаков арифметических операций",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //Закрытие формы
            NewPassword = password1;
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