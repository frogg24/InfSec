namespace InfSec
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            menuChangePassword = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            пользователиToolStripMenuItem = new ToolStripMenuItem();
            menuAddUser = new ToolStripMenuItem();
            menuViewUsers = new ToolStripMenuItem();
            настройкиToolStripMenuItem = new ToolStripMenuItem();
            menuChangeMinLength = new ToolStripMenuItem();
            menuChangeExpiry = new ToolStripMenuItem();
            справкаToolStripMenuItem = new ToolStripMenuItem();
            menuAbout = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lstUsers = new DataGridView();
            menuBlockUser = new ToolStripMenuItem();
            menuSetRestrictions = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lstUsers).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem, пользователиToolStripMenuItem, настройкиToolStripMenuItem, справкаToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(8, 3, 0, 3);
            menuStrip1.Size = new Size(917, 30);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuChangePassword, menuExit });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // menuChangePassword
            // 
            menuChangePassword.Name = "menuChangePassword";
            menuChangePassword.Size = new Size(207, 26);
            menuChangePassword.Text = "Сменить пароль";
            menuChangePassword.Click += menuChangePassword_Click;
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(207, 26);
            menuExit.Text = "Выход";
            menuExit.Click += menuExit_Click;
            // 
            // пользователиToolStripMenuItem
            // 
            пользователиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuAddUser, menuViewUsers });
            пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
            пользователиToolStripMenuItem.Size = new Size(122, 24);
            пользователиToolStripMenuItem.Text = "Пользователи";
            // 
            // menuAddUser
            // 
            menuAddUser.Name = "menuAddUser";
            menuAddUser.Size = new Size(346, 26);
            menuAddUser.Text = "Добавить пользователя";
            menuAddUser.Click += menuAddUser_Click;
            // 
            // menuViewUsers
            // 
            menuViewUsers.Name = "menuViewUsers";
            menuViewUsers.Size = new Size(346, 26);
            menuViewUsers.Text = "Просмотреть список пользователей";
            menuViewUsers.Click += menuViewUsers_Click;
            // 
            // настройкиToolStripMenuItem
            // 
            настройкиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuChangeMinLength, menuChangeExpiry, menuBlockUser, menuSetRestrictions });
            настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            настройкиToolStripMenuItem.Size = new Size(98, 24);
            настройкиToolStripMenuItem.Text = "Настройки";
            // 
            // menuChangeMinLength
            // 
            menuChangeMinLength.Name = "menuChangeMinLength";
            menuChangeMinLength.Size = new Size(402, 26);
            menuChangeMinLength.Text = "Изменить минимальную длину пароля";
            menuChangeMinLength.Click += menuChangeMinLength_Click;
            // 
            // menuChangeExpiry
            // 
            menuChangeExpiry.Name = "menuChangeExpiry";
            menuChangeExpiry.Size = new Size(402, 26);
            menuChangeExpiry.Text = "Изменить срок действия пароля";
            menuChangeExpiry.Click += menuChangeExpiry_Click;
            // 
            // справкаToolStripMenuItem
            // 
            справкаToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuAbout });
            справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            справкаToolStripMenuItem.Size = new Size(81, 24);
            справкаToolStripMenuItem.Text = "Справка";
            // 
            // menuAbout
            // 
            menuAbout.Name = "menuAbout";
            menuAbout.Size = new Size(187, 26);
            menuAbout.Text = "О программе";
            menuAbout.Click += menuAbout_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 683);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 19, 0);
            statusStrip1.Size = new Size(917, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(151, 20);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // lstUsers
            // 
            lstUsers.AllowUserToAddRows = false;
            lstUsers.AllowUserToDeleteRows = false;
            lstUsers.BackgroundColor = SystemColors.ButtonHighlight;
            lstUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            lstUsers.Location = new Point(12, 33);
            lstUsers.MultiSelect = false;
            lstUsers.Name = "lstUsers";
            lstUsers.ReadOnly = true;
            lstUsers.RowHeadersVisible = false;
            lstUsers.RowHeadersWidth = 51;
            lstUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lstUsers.Size = new Size(893, 647);
            lstUsers.TabIndex = 2;
            // 
            // menuBlockUser
            // 
            menuBlockUser.Name = "menuBlockUser";
            menuBlockUser.Size = new Size(402, 26);
            menuBlockUser.Text = "Блокировать/разблокировать пользователя";
            menuBlockUser.Click += this.menuBlockUser_Click;
            // 
            // menuSetRestrictions
            // 
            menuSetRestrictions.Name = "menuSetRestrictions";
            menuSetRestrictions.Size = new Size(402, 26);
            menuSetRestrictions.Text = "Установить ограничения";
            menuSetRestrictions.Click += this.menuSetRestrictions_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(917, 709);
            Controls.Add(lstUsers);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Система управления учетными записями";
            FormClosing += MainForm_FormClosing;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)lstUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuChangePassword;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAddUser;
        private System.Windows.Forms.ToolStripMenuItem menuViewUsers;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuChangeMinLength;
        private System.Windows.Forms.ToolStripMenuItem menuChangeExpiry;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridView lstUsers;
        private ToolStripMenuItem menuBlockUser;
        private ToolStripMenuItem menuSetRestrictions;
    }
}