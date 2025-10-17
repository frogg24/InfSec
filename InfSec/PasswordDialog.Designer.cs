namespace InfSec
{
    partial class PasswordDialog
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
            label1 = new Label();
            label2 = new Label();
            txtPassword1 = new TextBox();
            txtPassword2 = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 41);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(115, 20);
            label1.TabIndex = 0;
            label1.Text = "Новый пароль:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 85);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(179, 20);
            label2.TabIndex = 1;
            label2.Text = "Подтверждение пароля:";
            // 
            // txtPassword1
            // 
            txtPassword1.Location = new Point(203, 34);
            txtPassword1.Margin = new Padding(4, 5, 4, 5);
            txtPassword1.Name = "txtPassword1";
            txtPassword1.PasswordChar = '*';
            txtPassword1.Size = new Size(235, 27);
            txtPassword1.TabIndex = 2;
            // 
            // txtPassword2
            // 
            txtPassword2.Location = new Point(203, 80);
            txtPassword2.Margin = new Padding(4, 5, 4, 5);
            txtPassword2.Name = "txtPassword2";
            txtPassword2.PasswordChar = '*';
            txtPassword2.Size = new Size(235, 27);
            txtPassword2.TabIndex = 3;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(203, 134);
            btnOK.Margin = new Padding(4, 5, 4, 5);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(100, 35);
            btnOK.TabIndex = 4;
            btnOK.Text = "ОК";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(340, 134);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // PasswordDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 188);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtPassword2);
            Controls.Add(txtPassword1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PasswordDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Установка пароля";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword1;
        private System.Windows.Forms.TextBox txtPassword2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}