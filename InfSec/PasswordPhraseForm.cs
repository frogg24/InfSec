using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class PasswordPhraseForm : Form
    {
        public string PasswordPhrase { get; private set; }
        public bool Cancelled { get; private set; }

        public PasswordPhraseForm()
        {
            InitializeComponent();
            this.Text = "Ввод парольной фразы";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string phrase = txtPasswordPhrase.Text.Trim();

            if (string.IsNullOrEmpty(phrase))
            {
                MessageBox.Show("Введите парольную фразу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PasswordPhrase = phrase;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PasswordPhraseForm_Load(object sender, EventArgs e)
        {
            txtPasswordPhrase.Focus();
        }
    }
}