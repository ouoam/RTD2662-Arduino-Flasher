namespace RTD2660
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonErase = new System.Windows.Forms.Button();
            this.buttonFlash = new System.Windows.Forms.Button();
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.fileInputTextBox = new System.Windows.Forms.TextBox();
            this.debugTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.serialPortcComboBox = new System.Windows.Forms.ComboBox();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.fileSaveTextBox = new System.Windows.Forms.TextBox();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(373, 12);
            this.buttonOpen.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(100, 28);
            this.buttonOpen.TabIndex = 2;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonErase
            // 
            this.buttonErase.BackColor = System.Drawing.SystemColors.Control;
            this.buttonErase.Enabled = false;
            this.buttonErase.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonErase.Location = new System.Drawing.Point(140, 134);
            this.buttonErase.Margin = new System.Windows.Forms.Padding(4);
            this.buttonErase.Name = "buttonErase";
            this.buttonErase.Size = new System.Drawing.Size(100, 28);
            this.buttonErase.TabIndex = 3;
            this.buttonErase.Text = "Erase";
            this.buttonErase.UseVisualStyleBackColor = false;
            this.buttonErase.Click += new System.EventHandler(this.buttonErase_Click);
            // 
            // buttonFlash
            // 
            this.buttonFlash.Enabled = false;
            this.buttonFlash.Location = new System.Drawing.Point(263, 134);
            this.buttonFlash.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFlash.Name = "buttonFlash";
            this.buttonFlash.Size = new System.Drawing.Size(100, 28);
            this.buttonFlash.TabIndex = 4;
            this.buttonFlash.Text = "Flash";
            this.buttonFlash.UseVisualStyleBackColor = true;
            this.buttonFlash.Click += new System.EventHandler(this.buttonFlash_Click);
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Location = new System.Drawing.Point(373, 62);
            this.buttonLoadFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(100, 28);
            this.buttonLoadFile.TabIndex = 5;
            this.buttonLoadFile.Text = "Flash From";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // fileInputTextBox
            // 
            this.fileInputTextBox.Location = new System.Drawing.Point(20, 64);
            this.fileInputTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.fileInputTextBox.Name = "fileInputTextBox";
            this.fileInputTextBox.ReadOnly = true;
            this.fileInputTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fileInputTextBox.Size = new System.Drawing.Size(344, 22);
            this.fileInputTextBox.TabIndex = 6;
            // 
            // debugTextBox
            // 
            this.debugTextBox.Location = new System.Drawing.Point(16, 231);
            this.debugTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.debugTextBox.Multiline = true;
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(456, 191);
            this.debugTextBox.TabIndex = 7;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "firmware.bin";
            // 
            // serialPortcComboBox
            // 
            this.serialPortcComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serialPortcComboBox.FormattingEnabled = true;
            this.serialPortcComboBox.Location = new System.Drawing.Point(232, 14);
            this.serialPortcComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.serialPortcComboBox.Name = "serialPortcComboBox";
            this.serialPortcComboBox.Size = new System.Drawing.Size(132, 24);
            this.serialPortcComboBox.TabIndex = 8;
            // 
            // buttonInfo
            // 
            this.buttonInfo.Location = new System.Drawing.Point(20, 134);
            this.buttonInfo.Margin = new System.Windows.Forms.Padding(4);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(100, 28);
            this.buttonInfo.TabIndex = 9;
            this.buttonInfo.Text = "Info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(16, 198);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(457, 25);
            this.progressBar.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 166);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 28);
            this.label2.TabIndex = 11;
            // 
            // fileSaveTextBox
            // 
            this.fileSaveTextBox.Location = new System.Drawing.Point(19, 101);
            this.fileSaveTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.fileSaveTextBox.Name = "fileSaveTextBox";
            this.fileSaveTextBox.ReadOnly = true;
            this.fileSaveTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.fileSaveTextBox.Size = new System.Drawing.Size(344, 22);
            this.fileSaveTextBox.TabIndex = 12;
            this.fileSaveTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(372, 98);
            this.buttonSaveFile.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(100, 28);
            this.buttonSaveFile.TabIndex = 13;
            this.buttonSaveFile.Text = "Save To";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(373, 134);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 28);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "firmware.bin";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 436);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.fileSaveTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.serialPortcComboBox);
            this.Controls.Add(this.debugTextBox);
            this.Controls.Add(this.fileInputTextBox);
            this.Controls.Add(this.buttonLoadFile);
            this.Controls.Add(this.buttonFlash);
            this.Controls.Add(this.buttonErase);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "RTD2660 Arduino Flasher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonErase;
        private System.Windows.Forms.Button buttonFlash;
        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.TextBox fileInputTextBox;
        private System.Windows.Forms.TextBox debugTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox serialPortcComboBox;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileSaveTextBox;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

