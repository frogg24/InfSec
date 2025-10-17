using System;
using System.Windows.Forms;

namespace InfSec
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            this.Text = "���� � �������";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("������� ��� ������������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ���������, �������� �� ������������ ���������������
            if (username.ToUpper() == "ADMIN")
            {
                // ��� �������������� ��������� ������
                if (string.IsNullOrEmpty(password))
                {
                    // ���� ������ ������, ��������� ������������� ������ ��������������
                    if (DatabaseManager.UserExists("ADMIN"))
                    {
                        // ������� ������ �������������� � ���������� ���������� ������
                        DatabaseManager.CreateAdminUser();
                        SetPasswordForAdmin();
                        return;
                    }
                    else
                    {
                        // ������� ����� ������ ��������������
                        DatabaseManager.CreateAdminUser();
                        SetPasswordForAdmin();
                        return;
                    }
                }
                else
                {
                    // ��������� ������������ ������ ��������������
                    if (DatabaseManager.ValidateUser("ADMIN", password))
                    {
                        // ���� �������
                        MainForm mainForm = new MainForm("ADMIN");
                        mainForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("�������� ������ ��������������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                // ���������, ���������� �� ������������
                if (!DatabaseManager.UserExists(username))
                {
                    MessageBox.Show("������������ �� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ��������� ���������� ������� ������
                if (DatabaseManager.IsUserBlocked(username))
                {
                    MessageBox.Show("������� ������ �������������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ��������� ������
                if (DatabaseManager.ValidateUser(username, password))
                {
                    // ���� �������
                    MainForm mainForm = new MainForm(username);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("�������� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void SetPasswordForAdmin()
        {
            PasswordDialog pwdDialog = new PasswordDialog();
            if (pwdDialog.ShowDialog() == DialogResult.OK)
            {
                string newPassword = pwdDialog.NewPassword;
                DatabaseManager.SetUserPassword("ADMIN", newPassword);
                MessageBox.Show("������ �������������� ����������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // �������� ����� � ����� �������
                if (DatabaseManager.ValidateUser("ADMIN", newPassword))
                {
                    MainForm mainForm = new MainForm("ADMIN");
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("������ ��� ��������� ������", "������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("������ ��������� ���������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ��� �������� ����� ��������� ���������
            try
            {
                DatabaseManager.SaveChangesAndEncrypt();
            }
            catch (Exception ex)
            {
                // ���������� ������ ���������� ��� ��������, ��� ��� ��� ���������
                Console.WriteLine("������ ��� ���������� ������: " + ex.Message);
            }
        }
    }
}