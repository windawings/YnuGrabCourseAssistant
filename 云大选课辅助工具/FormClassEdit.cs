using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace 云大选课辅助工具
{
    public class FormClassEdit : Form
    {
        public List<ClassCourse> mClassItems;
        private const string FileFiter = "课程列表文件(*.csl)|*.csl|所有文件(*.*)|*.*";
        private SplitContainer splitContainerEdit;
        private OpenFileDialog openFileDialogImport;
        private SaveFileDialog saveFileDialogOutport;
        private GroupBox groupBoxEdit;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button buttonAdd;
        private Button buttonOutport;
        private Button buttonImport;
        private Button buttonDel;
        private Button buttonEdit;
        private GroupBox groupBox1;
        private ListView listViewWillChooseCourse;
        private ColumnHeader columnHeaderID;
        private ColumnHeader columnHeaderName;
        private ColumnHeader columnHeaderAdd;
        private Button buttonOK;
        private Button buttonCancel;
        private Button buttonClear;
        private Button buttonUp;
        private Button buttonDown;
        public FormClassEdit(IEnumerable<ClassCourse> items)
        {
            InitializeComponent();
            listViewWillChooseCourse.Items.Clear();
            foreach (var current in items)
            {
                var listViewItem = new ListViewItem(current.Id);
                listViewItem.SubItems.Add(current.Name);
                listViewItem.SubItems.Add(current.Remark);
                listViewItem.SubItems.Add("未知");
                listViewWillChooseCourse.Items.Add(listViewItem);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormEditCourse formEditCourse = new FormEditCourse("添加课程", "课程代码", "课程名称(选填)", "备注(选填)", true);
            formEditCourse.ShowDialog();
            if (formEditCourse.res == DialogResult.OK)
            {
                ListViewItem listViewItem = new ListViewItem(formEditCourse.id.Trim());
                listViewItem.SubItems.Add(formEditCourse.name.Trim());
                listViewItem.SubItems.Add(formEditCourse.add.Trim());
                listViewItem.SubItems.Add("");
                if (listViewItem.Text.Trim().Length == 0)
                {
                    MessageBox.Show("课程名为空!");
                    return;
                }
                listViewWillChooseCourse.Items.Add(listViewItem);
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listViewWillChooseCourse.SelectedItems.Count > 0)
            {
                var text = "将删除:";
                foreach (ListViewItem listViewItem in listViewWillChooseCourse.SelectedItems)
                {
                    object obj = text;
                    text = string.Concat(new object[]
                   {
                       obj, 
                       Environment.NewLine, 
                       "\t", 
                       listViewItem.Text, 
                       '(', 
                       listViewItem.SubItems[1].Text, 
                       ')'
                   });
                }
                var text2 = text;
                text = string.Concat(

                    text2,
                    Environment.NewLine,
                    "一共",
                    listViewWillChooseCourse.SelectedItems.Count.ToString(),
                    "项,确定吗?"
                    );
                if (MessageBox.Show(text, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foreach (ListViewItem item in listViewWillChooseCourse.SelectedItems)
                    {
                        listViewWillChooseCourse.Items.Remove(item);
                    }
                }
            }
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            if (listViewWillChooseCourse.Items.Count > 0 && MessageBox.Show("将清空所有课程，是否继续?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                listViewWillChooseCourse.Items.Clear();
            }
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewWillChooseCourse.SelectedItems.Count > 0)
            {
                var listViewItem = listViewWillChooseCourse.SelectedItems[0];
                var index = listViewWillChooseCourse.Items.IndexOf(listViewItem);
                var formEditCourse = new FormEditCourse("编辑课程", listViewItem.Text, listViewItem.SubItems[1].Text, listViewItem.SubItems[2].Text, false);
                formEditCourse.ShowDialog();
                if (formEditCourse.res == DialogResult.OK)
                {
                    listViewItem = new ListViewItem(formEditCourse.id.Trim());
                    listViewItem.SubItems.Add(formEditCourse.name.Trim());
                    listViewItem.SubItems.Add(formEditCourse.add.Trim());
                    listViewItem.SubItems.Add(listViewWillChooseCourse.Items[index].SubItems[3].Text);
                    if (listViewItem.Text.Trim().Length == 0)
                    {
                        MessageBox.Show("课程名为空!");
                        return;
                    }
                    listViewWillChooseCourse.Items[index] = listViewItem;
                }
            }
        }
        private void buttonImport_Click(object sender, EventArgs e)
        {
            openFileDialogImport.Filter = FileFiter;
            if (openFileDialogImport.ShowDialog() == DialogResult.OK)
            {
                if (listViewWillChooseCourse.Items.Count > 0)
                {
                    DialogResult dialogResult = MessageBox.Show("原来的选课表不为空,是否先清空原来选课表里面的数据?", "提示", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        return;
                    }
                    if (dialogResult == DialogResult.Yes)
                    {
                        listViewWillChooseCourse.Items.Clear();
                    }
                }
                var fileInfo = new FileInfo(openFileDialogImport.FileName);
                var streamReader = fileInfo.OpenText();
                string text;
                while ((text = streamReader.ReadLine()) != null)
                {
                    string[] array = text.Split(new char[]
                   {
                       '\t'
                   });
                    if (array.Length > 0)
                    {
                        var listViewItem = new ListViewItem(array[0].Trim());
                        if (array.Length > 1)
                        {
                            listViewItem.SubItems.Add(array[1].Trim());
                        }
                        if (array.Length > 2)
                        {
                            listViewItem.SubItems.Add(array[2].Trim());
                        }
                        if (listViewItem.Text.Trim().Length == 0)
                        {
                            return;
                        }
                        for (var i = array.Length; i < 4; i++)
                        {
                            listViewItem.SubItems.Add("未知");
                        }
                        listViewWillChooseCourse.Items.Add(listViewItem);
                    }
                    else
                    {
                        if (MessageBox.Show("格式错误,忽略:\n" + text + "\n是否继续读数据?") != DialogResult.Yes)
                        {
                            break;
                        }
                    }
                }
                streamReader.Close();
            }
        }
        private void buttonOutport_Click(object sender, EventArgs e)
        {
            if (listViewWillChooseCourse.Items.Count == 0)
            {
                MessageBox.Show("课程表为空!");
                return;
            }
            saveFileDialogOutport.Filter = FileFiter;
            if (saveFileDialogOutport.ShowDialog() == DialogResult.OK)
            {
                var fileInfo = new FileInfo(saveFileDialogOutport.FileName);
                var streamWriter = fileInfo.CreateText();
                foreach (ListViewItem listViewItem in listViewWillChooseCourse.Items)
                {
                    string text = listViewItem.Text + '\t';
                    if (listViewItem.SubItems.Count > 1)
                    {
                        text = text + listViewItem.SubItems[1].Text + '\t';
                    }
                    if (listViewItem.SubItems.Count > 2)
                    {
                        text += listViewItem.SubItems[2].Text;
                    }
                    streamWriter.WriteLine(text);
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
        }
        private void buttonUp_Click(object sender, EventArgs e)
        {
            var listView = listViewWillChooseCourse;
            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[0].Index != 0)
            {
                listView.BeginUpdate();
                foreach (ListViewItem listViewItem in listView.SelectedItems)
                {
                    var item = listViewItem;
                    var index = listViewItem.Index;
                    listView.Items.RemoveAt(index);
                    listView.Items.Insert(index - 1, item);
                }
                listView.EndUpdate();
            }
            listView.Focus();
        }
        private void buttonDown_Click(object sender, EventArgs e)
        {
            var listView = listViewWillChooseCourse;
            if (listView.SelectedItems.Count > 0 && listView.SelectedItems[listView.SelectedItems.Count - 1].Index < listView.Items.Count - 1)
            {
                listView.BeginUpdate();
                var count = listView.SelectedItems.Count;
                foreach (ListViewItem listViewItem in listView.SelectedItems)
                {
                    var item = listViewItem;
                    var index = listViewItem.Index;
                    listView.Items.RemoveAt(index);
                    listView.Items.Insert(index + count, item);
                }
                listView.EndUpdate();
            }
            listView.Focus();
        }
        private void FormClassEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            mClassItems = new List<ClassCourse>();
            foreach (ListViewItem listViewItem in listViewWillChooseCourse.Items)
            {
                mClassItems.Add(new ClassCourse(listViewItem.SubItems[0].Text, listViewItem.SubItems[1].Text, listViewItem.SubItems[2].Text));
            }
        }
        public List<ClassCourse> GetClassCourses()
        {
            return mClassItems;
        }
        private void InitializeComponent()
        {
            splitContainerEdit = new SplitContainer();
            groupBox1 = new GroupBox();
            listViewWillChooseCourse = new ListView();
            columnHeaderID = new ColumnHeader();
            columnHeaderName = new ColumnHeader();
            columnHeaderAdd = new ColumnHeader();
            groupBoxEdit = new GroupBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            buttonAdd = new Button();
            buttonOutport = new Button();
            buttonImport = new Button();
            buttonDel = new Button();
            buttonClear = new Button();
            buttonUp = new Button();
            buttonDown = new Button();
            buttonEdit = new Button();
            buttonOK = new Button();
            buttonCancel = new Button();
            openFileDialogImport = new OpenFileDialog();
            saveFileDialogOutport = new SaveFileDialog();
            splitContainerEdit.Panel1.SuspendLayout();
            splitContainerEdit.Panel2.SuspendLayout();
            splitContainerEdit.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBoxEdit.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            splitContainerEdit.Dock = DockStyle.Fill;
            splitContainerEdit.FixedPanel = FixedPanel.Panel2;
            splitContainerEdit.Location = new Point(0, 0);
            splitContainerEdit.Name = "splitContainerEdit";
            splitContainerEdit.Panel1.Controls.Add(groupBox1);
            splitContainerEdit.Panel2.Controls.Add(groupBoxEdit);
            splitContainerEdit.Size = new Size(483, 314);
            splitContainerEdit.SplitterDistance = 392;
            splitContainerEdit.TabIndex = 3;
            groupBox1.Controls.Add(listViewWillChooseCourse);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(392, 314);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "选课表";
            listViewWillChooseCourse.Columns.AddRange(new ColumnHeader[]
           {
               columnHeaderID, 
               columnHeaderName, 
               columnHeaderAdd
           });
            listViewWillChooseCourse.Dock = DockStyle.Fill;
            listViewWillChooseCourse.Font = new Font("微软雅黑", 10.5f, FontStyle.Regular, GraphicsUnit.Point, 134);
            listViewWillChooseCourse.FullRowSelect = true;
            listViewWillChooseCourse.Location = new Point(3, 17);
            listViewWillChooseCourse.Name = "listViewWillChooseCourse";
            listViewWillChooseCourse.Size = new Size(386, 294);
            listViewWillChooseCourse.TabIndex = 4;
            listViewWillChooseCourse.UseCompatibleStateImageBehavior = false;
            listViewWillChooseCourse.View = View.Details;
            listViewWillChooseCourse.DoubleClick += new EventHandler(buttonEdit_Click);
            columnHeaderID.Text = "课程代码";
            columnHeaderID.Width = 94;
            columnHeaderName.Text = "课程名称";
            columnHeaderName.Width = 130;
            columnHeaderAdd.Text = "备注";
            columnHeaderAdd.Width = 158;
            groupBoxEdit.Controls.Add(flowLayoutPanel2);
            groupBoxEdit.Dock = DockStyle.Fill;
            groupBoxEdit.Location = new Point(0, 0);
            groupBoxEdit.Name = "groupBoxEdit";
            groupBoxEdit.Size = new Size(87, 314);
            groupBoxEdit.TabIndex = 0;
            groupBoxEdit.TabStop = false;
            groupBoxEdit.Text = "编辑栏";
            flowLayoutPanel2.Controls.Add(buttonAdd);
            flowLayoutPanel2.Controls.Add(buttonOutport);
            flowLayoutPanel2.Controls.Add(buttonImport);
            flowLayoutPanel2.Controls.Add(buttonDel);
            flowLayoutPanel2.Controls.Add(buttonClear);
            flowLayoutPanel2.Controls.Add(buttonUp);
            flowLayoutPanel2.Controls.Add(buttonDown);
            flowLayoutPanel2.Controls.Add(buttonEdit);
            flowLayoutPanel2.Controls.Add(buttonOK);
            flowLayoutPanel2.Controls.Add(buttonCancel);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(3, 17);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(81, 294);
            flowLayoutPanel2.TabIndex = 5;
            buttonAdd.Location = new Point(3, 3);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(75, 23);
            buttonAdd.TabIndex = 6;
            buttonAdd.Text = "添加课程";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += new EventHandler(buttonAdd_Click);
            buttonOutport.Location = new Point(3, 32);
            buttonOutport.Name = "buttonOutport";
            buttonOutport.Size = new Size(75, 23);
            buttonOutport.TabIndex = 10;
            buttonOutport.Text = "导出课程";
            buttonOutport.UseVisualStyleBackColor = true;
            buttonOutport.Click += new EventHandler(buttonOutport_Click);
            buttonImport.Location = new Point(3, 61);
            buttonImport.Name = "buttonImport";
            buttonImport.Size = new Size(75, 23);
            buttonImport.TabIndex = 9;
            buttonImport.Text = "导入课程";
            buttonImport.UseVisualStyleBackColor = true;
            buttonImport.Click += new EventHandler(buttonImport_Click);
            buttonDel.Location = new Point(3, 90);
            buttonDel.Name = "buttonDel";
            buttonDel.Size = new Size(75, 23);
            buttonDel.TabIndex = 7;
            buttonDel.Text = "删除课程";
            buttonDel.UseVisualStyleBackColor = true;
            buttonDel.Click += new EventHandler(buttonDel_Click);
            buttonClear.Location = new Point(3, 119);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(75, 23);
            buttonClear.TabIndex = 15;
            buttonClear.Text = "清空课程";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += new EventHandler(buttonClear_Click);
            buttonUp.Location = new Point(3, 148);
            buttonUp.Name = "buttonUp";
            buttonUp.Size = new Size(75, 23);
            buttonUp.TabIndex = 13;
            buttonUp.Text = "往上移动";
            buttonUp.UseVisualStyleBackColor = true;
            buttonUp.Click += new EventHandler(buttonUp_Click);
            buttonDown.Location = new Point(3, 177);
            buttonDown.Name = "buttonDown";
            buttonDown.Size = new Size(75, 23);
            buttonDown.TabIndex = 14;
            buttonDown.Text = "往下移动";
            buttonDown.UseVisualStyleBackColor = true;
            buttonDown.Click += new EventHandler(buttonDown_Click);
            buttonEdit.Location = new Point(3, 206);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(75, 23);
            buttonEdit.TabIndex = 8;
            buttonEdit.Text = "编辑课程";
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += new EventHandler(buttonEdit_Click);
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Location = new Point(3, 235);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new Size(75, 23);
            buttonOK.TabIndex = 11;
            buttonOK.Text = "确定";
            buttonOK.UseVisualStyleBackColor = true;
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(3, 264);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 12;
            buttonCancel.Text = "取消";
            buttonCancel.UseVisualStyleBackColor = true;
            AcceptButton = buttonOK;
            AutoScaleDimensions = new SizeF(6f, 12f);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(483, 314);
            Controls.Add(splitContainerEdit);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormClassEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "选课表编辑";
            FormClosing += new FormClosingEventHandler(FormClassEdit_FormClosing);
            splitContainerEdit.Panel1.ResumeLayout(false);
            splitContainerEdit.Panel2.ResumeLayout(false);
            splitContainerEdit.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBoxEdit.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
