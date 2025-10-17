using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class ChangeExpiryDialog : Form
    {
        public string Username { get; private set; }
        public int ExpiryMonths { get; private set; }

        public ChangeExpiryDialog()
        {
            InitializeComponent();
            this.Text = "Изменение срока действия пароля";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            int expiryMonths;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Введите имя пользователя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseManager.UserExists(username))
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtExpiryMonths.Text, out expiryMonths))
            {
                MessageBox.Show("Введите корректное значение срока действия", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (expiryMonths < 0)
            {
                MessageBox.Show("Срок действия не может быть отрицательным", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Username = username;
            ExpiryMonths = expiryMonths;
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