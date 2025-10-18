using System;
using System.Data;
using System.Windows.Forms;

namespace InfSec
{
    public partial class MainForm : Form
    {
        private string currentUser;
        private bool isAdmin;

        public MainForm(string username)
        {
            InitializeComponent();
            this.currentUser = username;
            this.isAdmin = (username.ToUpper() == "ADMIN");
            this.Text = $"Система управления учетными записями - {username}";

            UpdateStatus();
            LoadUsersList();

            // Скрываем недоступные пункты меню для обычных пользователей
            if (!isAdmin)
            {
                menuChangePassword.Enabled = true;
                menuAddUser.Visible = false;
                menuViewUsers.Visible = false;
                menuBlockUser.Visible = false;
                menuSetRestrictions.Visible = false;
                menuChangeMinLength.Visible = false;
                menuChangeExpiry.Visible = false;
            }
            else
            {
                menuChangePassword.Enabled = true;
                menuAddUser.Enabled = true;
                menuViewUsers.Enabled = true;
                menuBlockUser.Enabled = true;
                menuSetRestrictions.Enabled = true;
                menuChangeMinLength.Enabled = true;
                menuChangeExpiry.Enabled = true;
            }
        }

        private void UpdateStatus()
        {
            toolStripStatusLabel1.Text = $"Текущий пользователь: {currentUser}";
        }

        private void LoadUsersList()
        {
            DataTable users = DatabaseManager.GetAllUsers();
            lstUsers.DataSource = users;
            lstUsers.Columns["username"].HeaderText = "Имя пользователя";
            lstUsers.Columns["blocked"].HeaderText = "Блокировка";
            lstUsers.Columns["restrictions_enabled"].HeaderText = "Условие пароля";
            lstUsers.Columns["min_length"].HeaderText = "Минимальная длина пароля";
            lstUsers.Columns["expiry_months"].HeaderText = "Срок действия пароля ";
            lstUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lstUsers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lstUsers.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Автор: Барсуков Павел ПИбд-41\n" +
                "Индивидуальное задание: Наличие цифр, знаков препинания и знаков арифметических операций\n" +
                "Используемый режим шифрования алгоритма DES для шифрования файла: ECB\n" +
                "Добавление к ключу случайного значения: Да\n" +
                "Используемый алгоритм хеширования пароля: MD5\n",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void menuChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordDialog changePwdDialog = new ChangePasswordDialog(currentUser);
            if (changePwdDialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Пароль успешно изменен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menuAddUser_Click(object sender, EventArgs e)
        {
            if (!isAdmin) return;

            AddUserDialog addUserDialog = new AddUserDialog();
            if (addUserDialog.ShowDialog() == DialogResult.OK)
            {
                string username = addUserDialog.Username;
                DatabaseManager.AddUser(username);
                LoadUsersList();
                MessageBox.Show($"Пользователь {username} добавлен", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menuViewUsers_Click(object sender, EventArgs e)
        {
            if (!isAdmin) return;

            ViewUsersDialog viewUsersDialog = new ViewUsersDialog();
            viewUsersDialog.ShowDialog();
        }

        private void menuBlockUser_Click(object sender, EventArgs e)
        {
            if (!isAdmin) return;
            // Проверяем, есть ли выбранная строка
            if (lstUsers.SelectedRows.Count == 0 || lstUsers.SelectedRows[0].Cells["username"].Value == null)
            {
                MessageBox.Show("Выберите пользователя из списка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем значение из ячейки "username" выбранной строки
            string usernameToBlock = lstUsers.SelectedRows[0].Cells["username"].Value.ToString();

            if (!DatabaseManager.UserExists(usernameToBlock))
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем текущее состояние blocked
            bool isBlocked = DatabaseManager.IsUserBlocked(usernameToBlock);

            string actionText = isBlocked ? "разблокировать" : "заблокировать";

            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите {actionText} пользователя {usernameToBlock}?",
                "Управление блокировкой",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                DatabaseManager.SetUserBlocked(usernameToBlock, !isBlocked);
                LoadUsersList();

                string status = isBlocked ? "разблокирован" : "заблокирован";
                MessageBox.Show($"Пользователь {usernameToBlock} {status}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void menuSetRestrictions_Click(object sender, EventArgs e)
        {
            if (!isAdmin) return;

            // Проверяем, что есть выделенная строка в списке пользователей
            if (lstUsers.SelectedRows.Count == 0 || lstUsers.SelectedRows[0].Cells["username"].Value == null)
            {
                MessageBox.Show("Выберите пользователя из списка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем имя пользователя из выделенной строки
            string username = lstUsers.SelectedRows[0].Cells["username"].Value.ToString();

            if (!DatabaseManager.UserExists(username))
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем текущее состояние blocked
            bool isRestrictionsOn = DatabaseManager.GetUserRestrictions(username);

            string actionText = isRestrictionsOn ? "выключить ограничения" : "включить ограничения";

            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите {actionText} пользователю {username}?", "Управление блокировкой",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                DatabaseManager.SetUserRestrictions(username, !isRestrictionsOn);
                LoadUsersList();

                string status = isRestrictionsOn ? "ограничения для пароля сняты" : "ограничения для пароля включены";
                MessageBox.Show($"Для пользователя {username} {status}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menuChangeMinLength_Click(object sender, EventArgs e)
        {
            if (!isAdmin) return;

            // Проверяем, что есть выделенная строка в списке пользователей
            if (lstUsers.SelectedRows.Count == 0 || lstUsers.SelectedRows[0].Cells["username"].Value == null)
            {
                MessageBox.Show("Выберите пользователя из списка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем имя пользователя из выделенной строки
            string username = lstUsers.SelectedRows[0].Cells["username"].Value.ToString();

            if (!DatabaseManager.UserExists(username))
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Показываем диалог для ввода новой минимальной длины
            ChangeQuantityDialog paramDialog = new ChangeQuantityDialog(username, "minlength");
            if (paramDialog.ShowDialog() == DialogResult.OK)
            {
                int minLength = paramDialog.Value;
                DatabaseManager.SetUserMinLength(username, minLength);
                LoadUsersList();
                MessageBox.Show($"Минимальная длина пароля для пользователя {username} установлена в {minLength}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void menuChangeExpiry_Click(object sender, EventArgs e)
        {
            if (!isAdmin) return;

            // Проверяем, что есть выделенная строка в списке пользователей
            if (lstUsers.SelectedRows.Count == 0 || lstUsers.SelectedRows[0].Cells["username"].Value == null)
            {
                MessageBox.Show("Выберите пользователя из списка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем имя пользователя из выделенной строки
            string username = lstUsers.SelectedRows[0].Cells["username"].Value.ToString();

            if (!DatabaseManager.UserExists(username))
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Показываем диалог для ввода нового срока действия
            ChangeQuantityDialog paramDialog = new ChangeQuantityDialog(username, "expiry");
            if (paramDialog.ShowDialog() == DialogResult.OK)
            {
                int expiryMonths = paramDialog.Value;
                DatabaseManager.SetUserExpiry(username, expiryMonths);
                LoadUsersList();
                MessageBox.Show($"Срок действия пароля для пользователя {username} установлен в {expiryMonths} месяцев", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // При закрытии основной формы сохраняем изменения и шифруем базу данных
            try
            {
                DatabaseManager.SaveChangesAndEncrypt();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении базы данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}