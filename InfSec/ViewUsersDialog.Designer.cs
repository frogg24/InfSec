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
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblBlocked = new System.Windows.Forms.Label();
            this.lblRestrictions = new System.Windows.Forms.Label();
            this.lblMinLength = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUsername.Location = new System.Drawing.Point(12, 25);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(153, 20);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Имя пользователя";
            // 
            // lblBlocked
            // 
            this.lblBlocked.AutoSize = true;
            this.lblBlocked.Location = new System.Drawing.Point(12, 55);
            this.lblBlocked.Name = "lblBlocked";
            this.lblBlocked.Size = new System.Drawing.Size(100, 13);
            this.lblBlocked.TabIndex = 1;
            this.lblBlocked.Text = "Статус блокировки";
            // 
            // lblRestrictions
            // 
            this.lblRestrictions.AutoSize = true;
            this.lblRestrictions.Location = new System.Drawing.Point(12, 78);
            this.lblRestrictions.Name = "lblRestrictions";
            this.lblRestrictions.Size = new System.Drawing.Size(121, 13);
            this.lblRestrictions.TabIndex = 2;
            this.lblRestrictions.Text = "Ограничения на пароль";
            // 
            // lblMinLength
            // 
            this.lblMinLength.AutoSize = true;
            this.lblMinLength.Location = new System.Drawing.Point(12, 101);
            this.lblMinLength.Name = "lblMinLength";
            this.lblMinLength.Size = new System.Drawing.Size(122, 13);
            this.lblMinLength.TabIndex = 3;
            this.lblMinLength.Text = "Минимальная длина пароля";
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Location = new System.Drawing.Point(12, 124);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(113, 13);
            this.lblExpiry.TabIndex = 4;
            this.lblExpiry.Text = "Срок действия пароля";
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(15, 150);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 5;
            this.btnPrev.Text = "Предыдущий";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(96, 150);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 6;
            this.btnNext.Text = "Следующий";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(177, 150);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewUsersDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 185);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.lblExpiry);
            this.Controls.Add(this.lblMinLength);
            this.Controls.Add(this.lblRestrictions);
            this.Controls.Add(this.lblBlocked);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ViewUsersDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список пользователей";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblBlocked;
        private System.Windows.Forms.Label lblRestrictions;
        private System.Windows.Forms.Label lblMinLength;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnClose;
    }
}