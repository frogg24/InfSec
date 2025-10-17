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
            string username = txtUsername.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatabaseManager.UserExists(username))
            {
                MessageBox.Show("Пользователь с таким именем уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Username = username;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}