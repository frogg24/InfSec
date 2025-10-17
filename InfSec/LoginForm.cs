using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text = "Вход в систему";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, является ли пользователь администратором
            if (username.ToUpper() == "ADMIN")
            {
                // Для администратора проверяем пароль
                if (string.IsNullOrEmpty(password))
                {
                    // Если пароль пустой, проверяем существование записи администратора
                    if (DatabaseManager.UserExists("ADMIN"))
                    {
                        // Создаем запись администратора и предлагаем установить пароль
                        DatabaseManager.CreateAdminUser();
                        SetPasswordForAdmin();
                        return;
                    }
                    else
                    {
                        // Создаем новую запись администратора
                        DatabaseManager.CreateAdminUser();
                        SetPasswordForAdmin();
                        return;
                    }
                }
                else
                {
                    // Проверяем существующий пароль администратора
                    if (DatabaseManager.ValidateUser("ADMIN", password))
                    {
                        // Вход успешен
                        MainForm mainForm = new MainForm("ADMIN");
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль администратора", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                // Проверяем, существует ли пользователь
                if (!DatabaseManager.UserExists(username))
                {
                    MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверяем блокировку учетной записи
                if (DatabaseManager.IsUserBlocked(username))
                {
                    MessageBox.Show("Учетная запись заблокирована", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверяем пароль
                if (DatabaseManager.ValidateUser(username, password))
                {
                    // Вход успешен
                    MainForm mainForm = new MainForm(username);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void SetPasswordForAdmin()
        {
            PasswordDialog pwdDialog = new PasswordDialog();
            if (pwdDialog.ShowDialog() == DialogResult.OK)
            {
                string newPassword = pwdDialog.NewPassword;
                DatabaseManager.SetUserPassword("ADMIN", newPassword);
                MessageBox.Show("Пароль администратора установлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Пытаемся войти с новым паролем
                if (DatabaseManager.ValidateUser("ADMIN", newPassword))
                {
                    MainForm mainForm = new MainForm("ADMIN");
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Ошибка при установке пароля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Работа программы завершена", "Выход", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // При закрытии формы сохраняем изменения
            try
            {
                DatabaseManager.SaveChangesAndEncrypt();
            }
            catch (Exception ex)
            {
                // Игнорируем ошибки сохранения при закрытии, так как это нормально
                Console.WriteLine("Ошибка при сохранении данных: " + ex.Message);
            }
        }
    }
}