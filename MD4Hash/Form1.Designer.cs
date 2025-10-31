namespace MD4Hash
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            labelStudentInfo = new Label();
            textBoxInfo = new TextBox();
            buttonSelectFile = new Button();
            textBoxFilePath = new TextBox();
            buttonCalculate = new Button();
            textBoxHashResult = new TextBox();
            buttonSaveHash = new Button();
            labelFilePath = new Label();
            labelHashResult = new Label();
            richTextBoxAlgorithm = new RichTextBox();
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
            textBoxInfo.Location = new Point(9, 32);
            textBoxInfo.Margin = new Padding(4, 5, 4, 5);
            textBoxInfo.Multiline = true;
            textBoxInfo.Name = "textBoxInfo";
            textBoxInfo.ReadOnly = true;
            textBoxInfo.Size = new Size(209, 29);
            textBoxInfo.TabIndex = 1;
            textBoxInfo.Text = "Вариант: Хэш-функция MD4";
            // 
            // buttonSelectFile
            // 
            buttonSelectFile.Location = new Point(560, 173);
            buttonSelectFile.Margin = new Padding(4, 5, 4, 5);
            buttonSelectFile.Name = "buttonSelectFile";
            buttonSelectFile.Size = new Size(133, 35);
            buttonSelectFile.TabIndex = 5;
            buttonSelectFile.Text = "Выбрать файл";
            buttonSelectFile.UseVisualStyleBackColor = true;
            buttonSelectFile.Click += buttonSelectFile_Click;
            // 
            // textBoxFilePath
            // 
            textBoxFilePath.Location = new Point(13, 177);
            textBoxFilePath.Margin = new Padding(4, 5, 4, 5);
            textBoxFilePath.Name = "textBoxFilePath";
            textBoxFilePath.ReadOnly = true;
            textBoxFilePath.Size = new Size(532, 27);
            textBoxFilePath.TabIndex = 4;
            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(701, 173);
            buttonCalculate.Margin = new Padding(4, 5, 4, 5);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(133, 35);
            buttonCalculate.TabIndex = 6;
            buttonCalculate.Text = "Вычислить MD4";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += buttonCalculate_Click;
            // 
            // textBoxHashResult
            // 
            textBoxHashResult.Location = new Point(17, 245);
            textBoxHashResult.Margin = new Padding(4, 5, 4, 5);
            textBoxHashResult.Name = "textBoxHashResult";
            textBoxHashResult.ReadOnly = true;
            textBoxHashResult.Size = new Size(676, 27);
            textBoxHashResult.TabIndex = 8;
            // 
            // buttonSaveHash
            // 
            buttonSaveHash.Location = new Point(701, 242);
            buttonSaveHash.Margin = new Padding(4, 5, 4, 5);
            buttonSaveHash.Name = "buttonSaveHash";
            buttonSaveHash.Size = new Size(133, 35);
            buttonSaveHash.TabIndex = 9;
            buttonSaveHash.Text = "Сохранить";
            buttonSaveHash.UseVisualStyleBackColor = true;
            buttonSaveHash.Click += buttonSaveHash_Click;
            // 
            // labelFilePath
            // 
            labelFilePath.AutoSize = true;
            labelFilePath.Location = new Point(9, 152);
            labelFilePath.Margin = new Padding(4, 0, 4, 0);
            labelFilePath.Name = "labelFilePath";
            labelFilePath.Size = new Size(135, 20);
            labelFilePath.TabIndex = 3;
            labelFilePath.Text = "Выбранный файл:";
            // 
            // labelHashResult
            // 
            labelHashResult.AutoSize = true;
            labelHashResult.Location = new Point(13, 220);
            labelHashResult.Margin = new Padding(4, 0, 4, 0);
            labelHashResult.Name = "labelHashResult";
            labelHashResult.Size = new Size(76, 20);
            labelHashResult.TabIndex = 7;
            labelHashResult.Text = "MD4-хэш:";
            // 
            // richTextBoxAlgorithm
            // 
            richTextBoxAlgorithm.Enabled = false;
            richTextBoxAlgorithm.Location = new Point(13, 55);
            richTextBoxAlgorithm.Margin = new Padding(4, 5, 4, 5);
            richTextBoxAlgorithm.Name = "richTextBoxAlgorithm";
            richTextBoxAlgorithm.Size = new Size(806, 87);
            richTextBoxAlgorithm.TabIndex = 2;
            richTextBoxAlgorithm.Text = resources.GetString("richTextBoxAlgorithm.Text");
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(840, 302);
            Controls.Add(buttonSaveHash);
            Controls.Add(textBoxHashResult);
            Controls.Add(labelHashResult);
            Controls.Add(buttonCalculate);
            Controls.Add(buttonSelectFile);
            Controls.Add(textBoxFilePath);
            Controls.Add(labelFilePath);
            Controls.Add(richTextBoxAlgorithm);
            Controls.Add(textBoxInfo);
            Controls.Add(labelStudentInfo);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "MD4 Hash Calculator";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStudentInfo;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.TextBox textBoxHashResult;
        private System.Windows.Forms.Button buttonSaveHash;
        private System.Windows.Forms.Label labelFilePath;
        private System.Windows.Forms.Label labelHashResult;
        private System.Windows.Forms.RichTextBox richTextBoxAlgorithm;
    }
}
