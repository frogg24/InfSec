using System;
using System.Data.Common;
using System.Windows.Forms;

namespace InfSec
{
    public partial class ChangeQuantityDialog : Form
    {
        public string Username { get; private set; }
        public string ParameterType { get;  set; } // "expiry" или "minlength"
        public int Value { get; private set; }

        public ChangeQuantityDialog(string username, string parameterType)
        {
            InitializeComponent();
            this.Username = username;
            this.ParameterType = parameterType;
            this.Text = parameterType == "expiry" ? "Изменение срока действия пароля" : "Изменение минимальной длины пароля";

            // Настройка заголовка и метки в зависимости от типа параметра
            if (parameterType == "expiry")
            {
                label2.Text = "Срок действия пароля (месяцы):";
                this.Text = "Изменение срока действия пароля";
            }
            else
            {
                label2.Text = "Минимальная длина пароля:";
                this.Text = "Изменение минимальной длины пароля";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // Считывание и проверка параметра
            int value;
            if (!int.TryParse(newQuantity.Text, out value))
            {
                MessageBox.Show("Введите корректное значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (value < 0)
            {
                MessageBox.Show("Значение не может быть отрицательным", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Передача параметра и закрытие формы
            Value = value;
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