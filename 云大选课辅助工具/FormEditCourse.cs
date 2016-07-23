
using System;
using System.Drawing;
using System.Windows.Forms;
namespace 云大选课辅助工具
{
    public class FormEditCourse : Form
    {
        private TextBox textBoxID;
        private TextBox textBoxName;
        private TextBox textBoxAdd;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button buttonOK;
        private Button buttonCancel;
        public string id;
        public string name;
        public string add;
        public DialogResult res = DialogResult.Cancel;

        private void InitializeComponent()
        {
            textBoxID = new TextBox();
            textBoxName = new TextBox();
            textBoxAdd = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            buttonOK = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            textBoxID.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBoxID.Location = new Point(93, 12);
            textBoxID.Margin = new Padding(4);
            textBoxID.MaxLength = 20;
            textBoxID.Name = "textBoxID";
            textBoxID.Size = new Size(118, 26);
            textBoxID.TabIndex = 0;
            textBoxID.DoubleClick += new EventHandler(textBoxID_DoubleClick);
            textBoxID.KeyPress += new KeyPressEventHandler(textBoxID_KeyPress);
            textBoxName.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBoxName.Location = new Point(93, 55);
            textBoxName.Margin = new Padding(4);
            textBoxName.MaxLength = 30;
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(118, 26);
            textBoxName.TabIndex = 1;
            textBoxAdd.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            textBoxAdd.Location = new Point(93, 97);
            textBoxAdd.Margin = new Padding(4);
            textBoxAdd.MaxLength = 100;
            textBoxAdd.Name = "textBoxAdd";
            textBoxAdd.ScrollBars = ScrollBars.Vertical;
            textBoxAdd.Size = new Size(118, 26);
            textBoxAdd.TabIndex = 2;
            label1.AutoSize = true;
            label1.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            label1.Location = new Point(13, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(80, 16);
            label1.TabIndex = 3;
            label1.Text = "课程代码:";
            label2.AutoSize = true;
            label2.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            label2.Location = new Point(13, 64);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(80, 16);
            label2.TabIndex = 4;
            label2.Text = "课程名称:";
            label3.AutoSize = true;
            label3.Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            label3.Location = new Point(13, 100);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(80, 16);
            label3.TabIndex = 5;
            label3.Text = "备注内容:";
            buttonOK.Location = new Point(16, 130);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(95, 23);
            buttonOK.TabIndex = 6;
            buttonOK.Text = "确定";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += new EventHandler(buttonOK_Click);
            buttonCancel.Location = new Point(116, 130);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(95, 23);
            buttonCancel.TabIndex = 7;
            buttonCancel.Text = "取消";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(8f, 16f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(223, 168);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOK);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxAdd);
            Controls.Add(textBoxName);
            Controls.Add(textBoxID);
            Font = new Font("宋体", 12f, FontStyle.Regular, GraphicsUnit.Point, 134);
            KeyPreview = true;
            Margin = new Padding(4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormEditCourse";
            StartPosition = FormStartPosition.CenterParent;
            Text = "编辑课程";
            KeyDown += new KeyEventHandler(FormEditCourse_KeyDown);
            ResumeLayout(false);
            PerformLayout();
        }
        public FormEditCourse()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            FormClosing += new FormClosingEventHandler(FormEditCourse_FormClosing);
        }
        public FormEditCourse(string tl, string id, string name, string add, bool editable)
        {
            InitializeComponent();
            Text = tl;
            textBoxAdd.Text = add;
            textBoxID.Text = id;
            textBoxName.Text = name;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            FormClosing += new FormClosingEventHandler(FormEditCourse_FormClosing);
            if (editable)
            {
                textBoxID.ReadOnly = false;
                return;
            }
            textBoxID.ReadOnly = true;
        }
        private void FormEditCourse_FormClosing(object sender, FormClosingEventArgs e)
        {
            id = textBoxID.Text;
            add = textBoxAdd.Text;
            name = textBoxName.Text;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            res = DialogResult.OK;
            Close();
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            res = DialogResult.Cancel;
            Close();
        }
        private void textBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar >= ' ')
            {
                e.Handled = true;
            }
        }
        private void textBoxID_DoubleClick(object sender, EventArgs e)
        {
            textBoxID.ReadOnly = false;
        }
        private void FormEditCourse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                buttonCancel_Click(null, null);
            }
        }
    }
}
