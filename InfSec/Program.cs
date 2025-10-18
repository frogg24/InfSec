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
                // ���� ��� ����� ��������� �����
                using (PasswordPhraseForm phraseForm = new PasswordPhraseForm())
                {
                    if (phraseForm.ShowDialog() == DialogResult.OK)
                    {
                        string passwordPhrase = phraseForm.PasswordPhrase;

                        // ������������� ���� ������ � ��������� ��������� ������
                        DatabaseManager.InitializeDatabase(passwordPhrase);

                        // ��������� �������� ����� �����
                        Application.Run(new LoginForm());
                    }
                    else
                    {
                        // ���� ������ � �������
                        Application.Exit();
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"������ ��� ������� ���������: {ex.Message}\n\n���������� ��������� ��������� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}