using System;
using System.Windows.Forms;

namespace InfSec
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                // Окно для ввода парольной фразы
                using (PasswordPhraseForm phraseForm = new PasswordPhraseForm())
                {
                    if (phraseForm.ShowDialog() == DialogResult.OK)
                    {
                        string passwordPhrase = phraseForm.PasswordPhrase;

                        // Инициализация базы данных с введенной парольной фразой
                        DatabaseManager.InitializeDatabase(passwordPhrase);

                        // Запускаем основную форму входа
                        Application.Run(new LoginForm());
                    }
                    else
                    {
                        // Если отмена — выходим
                        Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при запуске программы: {ex.Message}\n\nПопробуйте запустить программу заново.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}