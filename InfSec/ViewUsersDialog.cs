using System;
using System.Data;
using System.Windows.Forms;

namespace InfSec
{
    public partial class ViewUsersDialog : Form
    {
        private DataTable usersTable;
        private int currentIndex = 0;

        public ViewUsersDialog()
        {
            InitializeComponent();
            this.Text = "Список пользователей";
            LoadUsers();
        }

        private void LoadUsers()
        {
            usersTable = DatabaseManager.GetAllUsers();
            if (usersTable.Rows.Count > 0)
            {
                DisplayUser(currentIndex);
            }
            else
            {
                lblUsername.Text = "Нет пользователей";
                lblBlocked.Text = "";
                lblRestrictions.Text = "";
                lblMinLength.Text = "";
                lblExpiry.Text = "";
            }
        }

        private void DisplayUser(int index)
        {
            if (usersTable.Rows.Count <= 0) return;

            DataRow row = usersTable.Rows[index];
            lblUsername.Text = row["username"].ToString();
            lblBlocked.Text = Convert.ToBoolean(row["blocked"]) ? "Заблокирован" : "Активен";
            lblRestrictions.Text = Convert.ToBoolean(row["restrictions_enabled"]) ? "Ограничения включены" : "Ограничения отключены";
            lblMinLength.Text = $"Минимальная длина: {row["min_length"]}";
            lblExpiry.Text = $"Срок действия: {row["expiry_months"]} месяцев";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (usersTable.Rows.Count > 0)
            {
                currentIndex++;
                if (currentIndex >= usersTable.Rows.Count)
                {
                    currentIndex = 0;
                }
                DisplayUser(currentIndex);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (usersTable.Rows.Count > 0)
            {
                currentIndex--;
                if (currentIndex < 0)
                {
                    currentIndex = usersTable.Rows.Count - 1;
                }
                DisplayUser(currentIndex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}