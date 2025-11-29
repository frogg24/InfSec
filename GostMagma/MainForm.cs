using System;
using System.IO;
using System.Windows.Forms;

namespace GostMagma
{
    public partial class MainForm : Form
    {
        private string password = null;
        private string selectedFilePath = null;
        private string outputFilePath = null;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод обратоки кнопки "Выбрать файл с паролем"
        /// Метод для выбора пользователем txt файла с паролем для шифрования
        /// </summary>
        private void buttonSelectPasswordFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл с паролем";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    password = File.ReadAllText(openFileDialog.FileName);
                    textBoxPassword.Text = password; 
                }
            }
        }

        /// <summary>
        /// Метод обратоки кнопки "Выбрать файл"
        /// Метод для выбора пользователем  файла для шифрования
        /// </summary>
        private void buttonSelectFileEncrypt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл для шифрования";

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
        /// Метод обратоки кнопки "Зашифровать"
        /// Метод для шифрования и сохранения зашифрованного файла
        /// </summary>
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedFilePath))
            {
                MessageBox.Show("Пожалуйста, выберите файл.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                password = textBoxPassword.Text;
                if (string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Пожалуйста, введите пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Зашифрованные файлы (*.enc)|*.enc|Все файлы (*.*)|*.*";
                saveFileDialog.Title = "Сохранить зашифрованный файл";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFilePath = saveFileDialog.FileName;

                    try
                    {
                        GostMagmaCipher.EncryptFile(selectedFilePath, outputFilePath, password);
                        MessageBox.Show("Файл успешно зашифрован!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при шифровании: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Метод обратоки кнопки "Выбрать файл для расшифровки"
        /// Метод для выбора пользователем файла для дешифрования
        /// </summary>
        private void buttonSelectFileDecrypt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Зашифрованные файлы (*.enc)|*.enc|Все файлы (*.*)|*.*";
                openFileDialog.Title = "Выберите файл для расшифровки";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFilePath = openFileDialog.FileName;
                    textBoxFilePath.Text = selectedFilePath;

                    password = textBoxPassword.Text;

                    if (string.IsNullOrEmpty(password))
                    {
                        password = textBoxPassword.Text;
                        if (string.IsNullOrEmpty(password))
                        {
                            MessageBox.Show("Пожалуйста, введите пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                    {
                        saveFileDialog.Filter = "Все файлы (*.*)|*.*";
                        saveFileDialog.Title = "Сохранить расшифрованный файл";

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            try
                            {
                                GostMagmaCipher.DecryptFile(selectedFilePath, saveFileDialog.FileName, password);
                                MessageBox.Show("Файл успешно расшифрован!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при расшифровке: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
    }
}