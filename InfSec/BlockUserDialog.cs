using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class BlockUserDialog : Form
    {
        public string Username { get; private set; }
        public bool Block { get; private set; }

        public BlockUserDialog()
        {
            InitializeComponent();
            this.Text = "Блокировка пользователя";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();

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

            Username = username;
            Block = chkBlock.Checked;
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