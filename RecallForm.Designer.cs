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
            ButtonEcho = new Button();
            SuspendLayout();
            // 
            // ButtonAdd
            // 
            ButtonAdd.Location = new Point(4, 68);
            ButtonAdd.Name = "ButtonAdd";
            ButtonAdd.Size = new Size(103, 23);
            ButtonAdd.TabIndex = 2;
            ButtonAdd.Text = "Add Item";
            ButtonAdd.UseVisualStyleBackColor = true;
            ButtonAdd.Click += ButtonAdd_Click;
            // 
            // ButtonRemove
            // 
            ButtonRemove.Location = new Point(4, 97);
            ButtonRemove.Name = "ButtonRemove";
            ButtonRemove.Size = new Size(103, 23);
            ButtonRemove.TabIndex = 3;
            ButtonRemove.Text = "Remove Item";
            ButtonRemove.UseVisualStyleBackColor = true;
            ButtonRemove.Click += ButtonRemove_Click;
            // 
            // ButtonLoad
            // 
            ButtonLoad.Location = new Point(4, 126);
            ButtonLoad.Name = "ButtonLoad";
            ButtonLoad.Size = new Size(103, 23);
            ButtonLoad.TabIndex = 4;
            ButtonLoad.Text = "Load File";
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
            textBox.ScrollBars = ScrollBars.Both;
            textBox.Size = new Size(333, 141);
            textBox.TabIndex = 1;
            textBox.TextChanged += TextBox_TextChanged;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(4, 7);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(105, 23);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            // 
            // ButtonEcho
            // 
            ButtonEcho.Location = new Point(4, 39);
            ButtonEcho.Name = "ButtonEcho";
            ButtonEcho.Size = new Size(103, 23);
            ButtonEcho.TabIndex = 5;
            ButtonEcho.Text = "Echo to Window";
            ButtonEcho.UseVisualStyleBackColor = true;
            ButtonEcho.Click += ButtonEcho_Click;
            // 
            // RecallForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.LightSlateGray;
            ClientSize = new Size(451, 156);
            Controls.Add(ButtonEcho);
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
        private Button ButtonEcho;
    }
}