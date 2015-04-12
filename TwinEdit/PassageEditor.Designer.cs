namespace TwinEdit
{
    partial class PassageEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTags = new System.Windows.Forms.TextBox();
            this.rdbPassage = new System.Windows.Forms.RadioButton();
            this.rdbJavaScript = new System.Windows.Forms.RadioButton();
            this.cmbCategories = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.codeEdit1 = new TwinEdit.CodeEdit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(53, 17);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(474, 20);
            this.txtTitle.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tags:";
            // 
            // txtTags
            // 
            this.txtTags.Location = new System.Drawing.Point(57, 43);
            this.txtTags.Name = "txtTags";
            this.txtTags.Size = new System.Drawing.Size(470, 20);
            this.txtTags.TabIndex = 3;
            // 
            // rdbPassage
            // 
            this.rdbPassage.AutoSize = true;
            this.rdbPassage.Checked = true;
            this.rdbPassage.Location = new System.Drawing.Point(20, 96);
            this.rdbPassage.Name = "rdbPassage";
            this.rdbPassage.Size = new System.Drawing.Size(92, 17);
            this.rdbPassage.TabIndex = 4;
            this.rdbPassage.TabStop = true;
            this.rdbPassage.Text = "Passage View";
            this.rdbPassage.UseVisualStyleBackColor = true;
            this.rdbPassage.CheckedChanged += new System.EventHandler(this.rdbPassage_CheckedChanged);
            // 
            // rdbJavaScript
            // 
            this.rdbJavaScript.AutoSize = true;
            this.rdbJavaScript.Location = new System.Drawing.Point(118, 96);
            this.rdbJavaScript.Name = "rdbJavaScript";
            this.rdbJavaScript.Size = new System.Drawing.Size(101, 17);
            this.rdbJavaScript.TabIndex = 6;
            this.rdbJavaScript.Text = "JavaScript View";
            this.rdbJavaScript.UseVisualStyleBackColor = true;
            this.rdbJavaScript.CheckedChanged += new System.EventHandler(this.rdbJavaScript_CheckedChanged);
            // 
            // cmbCategories
            // 
            this.cmbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategories.FormattingEnabled = true;
            this.cmbCategories.Location = new System.Drawing.Point(75, 69);
            this.cmbCategories.Name = "cmbCategories";
            this.cmbCategories.Size = new System.Drawing.Size(315, 21);
            this.cmbCategories.TabIndex = 7;
            this.cmbCategories.SelectedIndexChanged += new System.EventHandler(this.cmbCategories_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Category:";
            // 
            // codeEdit1
            // 
            this.codeEdit1.AutoScrollMinSize = new System.Drawing.Size(88, 15);
            this.codeEdit1.BackBrush = null;
            this.codeEdit1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codeEdit1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.codeEdit1.Location = new System.Drawing.Point(20, 119);
            this.codeEdit1.Name = "codeEdit1";
            this.codeEdit1.Paddings = new System.Windows.Forms.Padding(0);
            this.codeEdit1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codeEdit1.Size = new System.Drawing.Size(507, 294);
            this.codeEdit1.TabIndex = 9;
            this.codeEdit1.Text = "codeEdit1";
            // 
            // PassageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 425);
            this.Controls.Add(this.codeEdit1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbCategories);
            this.Controls.Add(this.rdbJavaScript);
            this.Controls.Add(this.rdbPassage);
            this.Controls.Add(this.txtTags);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PassageEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Passage Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PassageEditor_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTags;
        private System.Windows.Forms.RadioButton rdbPassage;
        private System.Windows.Forms.RadioButton rdbJavaScript;
        private System.Windows.Forms.ComboBox cmbCategories;
        private System.Windows.Forms.Label label3;
        private CodeEdit codeEdit1;
    }
}