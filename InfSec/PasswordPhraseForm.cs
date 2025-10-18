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
            // Считывание парольной фразы
            string phrase = txtPasswordPhrase.Text.Trim();

            // Проверка парольной фразы на пустоту
            if (string.IsNullOrEmpty(phrase))
            {
                MessageBox.Show("Введите парольную фразу", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Запись введенной парольной фразы
            PasswordPhrase = phrase;

            //Звкрытие формы
            DialogResult = DialogResult.OK;
            Close();
        }

        // Закрытик формы по клику
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