using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class ChangeMinLengthDialog : Form
    {
        public string Username { get; private set; }
        public int MinLength { get; private set; }

        public ChangeMinLengthDialog()
        {
            InitializeComponent();
            this.Text = "Изменение минимальной длины пароля";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            int minLength;

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

            if (!int.TryParse(txtMinLength.Text, out minLength))
            {
                MessageBox.Show("Введите корректное значение минимальной длины", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (minLength < 0)
            {
                MessageBox.Show("Минимальная длина не может быть отрицательной", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Username = username;
            MinLength = minLength;
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