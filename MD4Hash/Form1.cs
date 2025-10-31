using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MD4Hash
{
    public partial class Form1 : Form
    {
        private string selectedFilePath = null;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработка нажатия на кнопку "Выбрать файл".
        /// Выбор ркализован через FileDialog и не обрабатывает файлы размером менее 1кб
        /// </summary>
        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл для вычисления MD4-хэша";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    textBoxFilePath.Text = selectedFilePath;

                    FileInfo fileInfo = new FileInfo(selectedFilePath);
                    if (fileInfo.Length < 1024)
                    {
                        MessageBox.Show("Размер файла должен быть не менее 1 КБ.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        selectedFilePath = null;
                        textBoxFilePath.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// Обратока кнопки "Вычислить MD4".
        /// Из выбранного ранее файла считывает все байты и вызывает метод ComputeMD4Hash с
        /// реализованным алгоритмом хэширования, после записывает полученный хэш на форму
        /// </summary>
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Пожалуйста, выберите файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                byte[] fileBytes = File.ReadAllBytes(selectedFilePath);
                string hash = ComputeMD4Hash(fileBytes);
                textBoxHashResult.Text = hash;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при вычислении хэша: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Метод обратоки кнопки "Сохранить"
        /// Сохраняет полкченный хэш в txt файл
        /// </summary>
        private void buttonSaveHash_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxHashResult.Text))
            {
                MessageBox.Show("Сначала вычислите MD4-хэш.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить MD4-хэш";
                saveFileDialog.DefaultExt = "txt";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog.FileName, textBoxHashResult.Text);
                        MessageBox.Show("Хэш успешно сохранён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Метод который передает байты в статический класс и после посчета соединяет полученные назад
        /// байты в строку для отображения и сохранения
        /// </summary>
        /// <param name="data">Байты выбрианного файла</param>
        private string ComputeMD4Hash(byte[] data)
        {
            // Вычисление MD4 хэша
            byte[] hashBytes = MD4.ComputeHash(data);

            // Преобразование байтов в шестнадцатеричную строку
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}