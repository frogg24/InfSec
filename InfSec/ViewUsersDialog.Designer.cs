namespace InfSec
{
    partial class ViewUsersDialog
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
            lblUsername = new Label();
            lblBlocked = new Label();
            lblRestrictions = new Label();
            lblMinLength = new Label();
            lblExpiry = new Label();
            btnPrev = new Button();
            btnNext = new Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblUsername.Location = new Point(16, 38);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(207, 25);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Имя пользователя";
            // 
            // lblBlocked
            // 
            lblBlocked.AutoSize = true;
            lblBlocked.Location = new Point(16, 85);
            lblBlocked.Margin = new Padding(4, 0, 4, 0);
            lblBlocked.Name = "lblBlocked";
            lblBlocked.Size = new Size(140, 20);
            lblBlocked.TabIndex = 1;
            lblBlocked.Text = "Статус блокировки";
            // 
            // lblRestrictions
            // 
            lblRestrictions.AutoSize = true;
            lblRestrictions.Location = new Point(16, 120);
            lblRestrictions.Margin = new Padding(4, 0, 4, 0);
            lblRestrictions.Name = "lblRestrictions";
            lblRestrictions.Size = new Size(179, 20);
            lblRestrictions.TabIndex = 2;
            lblRestrictions.Text = "Ограничения на пароль";
            // 
            // lblMinLength
            // 
            lblMinLength.AutoSize = true;
            lblMinLength.Location = new Point(16, 155);
            lblMinLength.Margin = new Padding(4, 0, 4, 0);
            lblMinLength.Name = "lblMinLength";
            lblMinLength.Size = new Size(210, 20);
            lblMinLength.TabIndex = 3;
            lblMinLength.Text = "Минимальная длина пароля";
            // 
            // lblExpiry
            // 
            lblExpiry.AutoSize = true;
            lblExpiry.Location = new Point(16, 191);
            lblExpiry.Margin = new Padding(4, 0, 4, 0);
            lblExpiry.Name = "lblExpiry";
            lblExpiry.Size = new Size(165, 20);
            lblExpiry.TabIndex = 4;
            lblExpiry.Text = "Срок действия пароля";
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(16, 231);
            btnPrev.Margin = new Padding(4, 5, 4, 5);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(153, 35);
            btnPrev.TabIndex = 5;
            btnPrev.Text = "Предыдущий";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(177, 231);
            btnNext.Margin = new Padding(4, 5, 4, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(150, 35);
            btnNext.TabIndex = 6;
            btnNext.Text = "Следующий";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // ViewUsersDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(340, 285);
            Controls.Add(btnNext);
            Controls.Add(btnPrev);
            Controls.Add(lblExpiry);
            Controls.Add(lblMinLength);
            Controls.Add(lblRestrictions);
            Controls.Add(lblBlocked);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ViewUsersDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Список пользователей";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblBlocked;
        private System.Windows.Forms.Label lblRestrictions;
        private System.Windows.Forms.Label lblMinLength;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
    }
}