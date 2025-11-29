namespace GostMagma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            labelStudentInfo = new Label();
            textBoxInfo = new TextBox();
            buttonSelectFileEncrypt = new Button();
            textBoxFilePath = new TextBox();
            buttonCalculate = new Button();
            labelFilePath = new Label();
            richTextBoxAlgorithm = new RichTextBox();
            buttonSelectPasswordFile = new Button();
            label1 = new Label();
            textBoxPassword = new TextBox();
            buttonSelectFileDecrypt = new Button();
            SuspendLayout();
            // 
            // labelStudentInfo
            // 
            labelStudentInfo.AutoSize = true;
            labelStudentInfo.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelStudentInfo.Location = new Point(9, 9);
            labelStudentInfo.Margin = new Padding(4, 0, 4, 0);
            labelStudentInfo.Name = "labelStudentInfo";
            labelStudentInfo.Size = new Size(338, 18);
            labelStudentInfo.TabIndex = 0;
            labelStudentInfo.Text = "Студент: Барсуков П.О., Группа: ПИбд-41";
            // 
            // textBoxInfo
            // 
            textBoxInfo.BackColor = SystemColors.Control;
            textBoxInfo.BorderStyle = BorderStyle.None;
            textBoxInfo.Enabled = false;
            textBoxInfo.Location = new Point(9, 32);
            textBoxInfo.Margin = new Padding(4, 5, 4, 5);
            textBoxInfo.Multiline = true;
            textBoxInfo.Name = "textBoxInfo";
            textBoxInfo.ReadOnly = true;
            textBoxInfo.Size = new Size(598, 29);
            textBoxInfo.TabIndex = 1;
            textBoxInfo.Text = "Вариант 5: алгоритм ГОСТ Р 34.12-2018 Магма (ГОСТ 28147). Режим гаммирования.";
            // 
            // buttonSelectFileEncrypt
            // 
            buttonSelectFileEncrypt.Location = new Point(560, 256);
            buttonSelectFileEncrypt.Margin = new Padding(4, 5, 4, 5);
            buttonSelectFileEncrypt.Name = "buttonSelectFileEncrypt";
            buttonSelectFileEncrypt.Size = new Size(133, 35);
            buttonSelectFileEncrypt.TabIndex = 5;
            buttonSelectFileEncrypt.Text = "Выбрать файл";
            buttonSelectFileEncrypt.UseVisualStyleBackColor = true;
            buttonSelectFileEncrypt.Click += buttonSelectFileEncrypt_Click;
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.Location = new Point(13, 260);
            textBoxFilePath.Margin = new Padding(4, 5, 4, 5);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.ReadOnly = true;
            textBoxFilePath.Size = new Size(532, 27);
            textBoxFilePath.TabIndex = 4;
            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(701, 256);
            buttonCalculate.Margin = new Padding(4, 5, 4, 5);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(133, 35);
            buttonCalculate.TabIndex = 6;
            buttonCalculate.Text = "Зашифровать";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += buttonCalculate_Click;
            // 
            // labelFilePath
            // 
            labelFilePath.AutoSize = true;
            labelFilePath.Location = new Point(9, 235);
            labelFilePath.Margin = new Padding(4, 0, 4, 0);
            labelFilePath.Name = "labelFilePath";
            labelFilePath.Size = new Size(135, 20);
            labelFilePath.TabIndex = 3;
            labelFilePath.Text = "Выбранный файл:";
            // 
            // richTextBoxAlgorithm
            // 
            richTextBoxAlgorithm.Enabled = false;
            richTextBoxAlgorithm.Location = new Point(13, 55);
            richTextBoxAlgorithm.Margin = new Padding(4, 5, 4, 5);
            richTextBoxAlgorithm.Name = "richTextBoxAlgorithm";
            richTextBoxAlgorithm.Size = new Size(806, 102);
            richTextBoxAlgorithm.TabIndex = 2;
            richTextBoxAlgorithm.Text = resources.GetString("richTextBoxAlgorithm.Text");
            // 
            // buttonSelectPasswordFile
            // 
            buttonSelectPasswordFile.Location = new Point(560, 187);
            buttonSelectPasswordFile.Margin = new Padding(4, 5, 4, 5);
            buttonSelectPasswordFile.Name = "buttonSelectPasswordFile";
            buttonSelectPasswordFile.Size = new Size(274, 35);
            buttonSelectPasswordFile.TabIndex = 12;
            buttonSelectPasswordFile.Text = "Выбрать файл с паролем";
            buttonSelectPasswordFile.UseVisualStyleBackColor = true;
            buttonSelectPasswordFile.Click += buttonSelectPasswordFile_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 162);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(188, 20);
            label1.TabIndex = 10;
            label1.Text = "Пароль для шифрования:";
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(13, 191);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(532, 27);
            textBoxPassword.TabIndex = 14;
            // 
            // buttonSelectFileDecrypt
            // 
            buttonSelectFileDecrypt.Location = new Point(13, 312);
            buttonSelectFileDecrypt.Margin = new Padding(4, 5, 4, 5);
            buttonSelectFileDecrypt.Name = "buttonSelectFileDecrypt";
            buttonSelectFileDecrypt.Size = new Size(274, 35);
            buttonSelectFileDecrypt.TabIndex = 15;
            buttonSelectFileDecrypt.Text = "Выбрать файл для расшифровки";
            buttonSelectFileDecrypt.UseVisualStyleBackColor = true;
            buttonSelectFileDecrypt.Click += buttonSelectFileDecrypt_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 364);
            Controls.Add(buttonSelectFileDecrypt);
            Controls.Add(textBoxPassword);
            Controls.Add(buttonSelectPasswordFile);
            Controls.Add(label1);
            Controls.Add(buttonCalculate);
            Controls.Add(buttonSelectFileEncrypt);
            Controls.Add(textBoxFilePath);
            Controls.Add(labelFilePath);
            Controls.Add(richTextBoxAlgorithm);
            Controls.Add(textBoxInfo);
            Controls.Add(labelStudentInfo);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MainForm";
            Text = "Gost Magma";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStudentInfo;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Button buttonSelectFileEncrypt;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.RichTextBox richTextBoxAlgorithm;
        private Button buttonSelectPasswordFile;
        private Label label1;
        private TextBox textBoxPassword;
        private Button buttonSelectFileDecrypt;
    }
    }

