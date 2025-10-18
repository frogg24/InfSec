using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class AddUserDialog : Form
    {
        public string Username { get; private set; }

        public AddUserDialog()
        {
            InitializeComponent();
            this.Text = "Добавление нового пользователя";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Считывание имени 
            string username = txtUsername.Text.Trim();

            // Если поле пустое
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверка на повтор имени пользователя
            if (DatabaseManager.UserExists(username))
            {
                MessageBox.Show("Пользователь с таким именем уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Закрытие формы
            Username = username;
            DialogResult = DialogResult.OK;
            Close();
        }

        // Закрытие формы по клику
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}