namespace Recall
{
    partial class RecallForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ButtonAdd = new Button();
            ButtonRemove = new Button();
            ButtonLoad = new Button();
            textBox = new TextBox();
            comboBox1 = new ComboBox();
            SuspendLayout();
            // 
            // ButtonAdd
            // 
            ButtonAdd.Location = new Point(12, 53);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new Size(75, 23);
            ButtonAdd.TabIndex = 2;
            ButtonAdd.Text = "Add";
            ButtonAdd.UseVisualStyleBackColor = true;
            ButtonAdd.Click += ButtonAdd_Click;
            // 
            // ButtonRemove
            // 
            ButtonRemove.Location = new Point(12, 82);
            ButtonRemove.Name = "ButtonRemove";
            ButtonRemove.Size = new Size(75, 23);
            ButtonRemove.TabIndex = 3;
            ButtonRemove.Text = "Remove";
            ButtonRemove.UseVisualStyleBackColor = true;
            ButtonRemove.Click += ButtonRemove_Click;
            // 
            // ButtonLoad
            // 
            ButtonLoad.Location = new Point(12, 111);
            ButtonLoad.Name = "ButtonLoad";
            ButtonLoad.Size = new Size(75, 23);
            ButtonLoad.TabIndex = 4;
            ButtonLoad.Text = "Load";
            ButtonLoad.UseVisualStyleBackColor = true;
            ButtonLoad.Click += ButtonLoad_Click;
            // 
            // textBox
            // 
            textBox.AcceptsTab = true;
            textBox.BackColor = Color.Black;
            textBox.BorderStyle = BorderStyle.None;
            textBox.ForeColor = Color.White;
            textBox.Location = new Point(113, 7);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.Size = new Size(333, 127);
            textBox.TabIndex = 1;
            textBox.TextChanged += TextBox_TextChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(97, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // RecallForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.Gray;
            ClientSize = new Size(451, 142);
            Controls.Add(comboBox1);
            Controls.Add(textBox);
            Controls.Add(ButtonLoad);
            Controls.Add(ButtonRemove);
            Controls.Add(ButtonAdd);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "RecallForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Recall";
            TopMost = true;
            FormClosing += RecallForm_FormClosing;
            FormClosed += RecallForm_FormClosed;
            KeyDown += RecallForm_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        private Button ButtonAdd;
        private Button ButtonRemove;
        private Button ButtonLoad;
        private TextBox textBox;
        internal ComboBox comboBox1;
    }
}