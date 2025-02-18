namespace LabFilters
{
    partial class SettingsForm
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
            this.settingsTabControl = new System.Windows.Forms.TabControl();
            this.settingsMenu = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.historyValueTextBox = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.settingsTabControl.SuspendLayout();
            this.settingsMenu.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsTabControl
            // 
            this.settingsTabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.settingsTabControl.Controls.Add(this.settingsMenu);
            this.settingsTabControl.Controls.Add(this.tabPage2);
            this.settingsTabControl.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.settingsTabControl.Location = new System.Drawing.Point(-1, -3);
            this.settingsTabControl.Name = "settingsTabControl";
            this.settingsTabControl.SelectedIndex = 0;
            this.settingsTabControl.Size = new System.Drawing.Size(770, 475);
            this.settingsTabControl.TabIndex = 0;
            // 
            // settingsMenu
            // 
            this.settingsMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.settingsMenu.Controls.Add(this.cancelButton);
            this.settingsMenu.Controls.Add(this.saveButton);
            this.settingsMenu.Controls.Add(this.historyValueTextBox);
            this.settingsMenu.Controls.Add(this.label1);
            this.settingsMenu.Location = new System.Drawing.Point(4, 27);
            this.settingsMenu.Name = "settingsMenu";
            this.settingsMenu.Padding = new System.Windows.Forms.Padding(3);
            this.settingsMenu.Size = new System.Drawing.Size(762, 444);
            this.settingsMenu.TabIndex = 0;
            this.settingsMenu.Text = "Производительность";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(762, 444);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Внешний вид";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "История действий:";
            // 
            // historyValueTextBox
            // 
            this.historyValueTextBox.Location = new System.Drawing.Point(125, 10);
            this.historyValueTextBox.Name = "historyValueTextBox";
            this.historyValueTextBox.Size = new System.Drawing.Size(78, 23);
            this.historyValueTextBox.TabIndex = 1;
            this.historyValueTextBox.Text = "10";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(679, 414);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(598, 414);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Отмена";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Цветовой акцент:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(769, 471);
            this.Controls.Add(this.settingsTabControl);
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this.settingsTabControl.ResumeLayout(false);
            this.settingsMenu.ResumeLayout(false);
            this.settingsMenu.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingsTabControl;
        private System.Windows.Forms.TabPage settingsMenu;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox historyValueTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label2;
    }
}