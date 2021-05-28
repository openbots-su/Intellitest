
using IntelliForm.Properties;
using System.Windows.Forms;

namespace IntelliForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.codeInput1 = new CustomTextBox();
            this._myImageList = new System.Windows.Forms.ImageList(this.components);
            this.codeInput2 = new System.Windows.Forms.TextBox();
            this.codeGridView1 = new IntelliForm.CodeGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new IntelliForm.CodeGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gListBox1 = new IntelliForm.GListBox();
            ((System.ComponentModel.ISupportInitialize)(this.codeGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // codeInput1
            // 
            this.codeInput1.AcceptsTab = true;
            this.codeInput1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codeInput1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeInput1.Location = new System.Drawing.Point(0, 0);
            this.codeInput1.Multiline = true;
            this.codeInput1.Name = "codeInput1";
            this.codeInput1.Size = new System.Drawing.Size(800, 450);
            this.codeInput1.TabIndex = 0;
            this.codeInput1.TextChanged += new System.EventHandler(this.textBoxCodeInput_textChanged);
            this.codeInput1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codeInput_keyDown);
            // 
            // _myImageList
            // 
            this._myImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_myImageList.ImageStream")));
            this._myImageList.TransparentColor = System.Drawing.Color.Lime;
            this._myImageList.Images.SetKeyName(0, "intellisense-field.png");
            this._myImageList.Images.SetKeyName(1, "intellisense-method.png");
            this._myImageList.Images.SetKeyName(2, "intellisense-extensionmethodV2.png");
            this._myImageList.Images.SetKeyName(3, "intellisense-propertyV2.png");
            this._myImageList.Images.SetKeyName(4, "intellisense-class.png");
            this._myImageList.Images.SetKeyName(5, "intellisense-enumerator.png");
            this._myImageList.Images.SetKeyName(6, "intellisense-event.png");
            this._myImageList.Images.SetKeyName(7, "intellisense-interface.png");
            this._myImageList.Images.SetKeyName(8, "intellisense-namespace.png");
            this._myImageList.Images.SetKeyName(9, "intellisense-structure.png");
            this._myImageList.Images.SetKeyName(10, "intellisense-delegate.png");
            // 
            // codeInput2
            // 
            this.codeInput2.Location = new System.Drawing.Point(0, 84);
            this.codeInput2.Name = "codeInput2";
            this.codeInput2.Size = new System.Drawing.Size(800, 22);
            this.codeInput2.TabIndex = 4;
            this.codeInput2.TextChanged += new System.EventHandler(this.textBoxCodeInput_textChanged);
            this.codeInput2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.codeInput_keyDown);
            // 
            // codeGridView1
            // 
            this.codeGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.codeGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column3,
            this.Column4});
            this.codeGridView1.Location = new System.Drawing.Point(0, 288);
            this.codeGridView1.Name = "codeGridView1";
            this.codeGridView1.RowHeadersWidth = 51;
            this.codeGridView1.RowTemplate.Height = 24;
            this.codeGridView1.Size = new System.Drawing.Size(788, 150);
            this.codeGridView1.TabIndex = 6;
            this.codeGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.Width = 200;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.Width = 200;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridView1.Location = new System.Drawing.Point(0, 132);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(788, 150);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.MinimumWidth = 10;
            this.Column1.Name = "Column1";
            this.Column1.Width = 700;
            // 
            // gListBox1
            // 
            this.gListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.gListBox1.ImageList = this._myImageList;
            this.gListBox1.Location = new System.Drawing.Point(12, 13);
            this.gListBox1.Name = "gListBox1";
            this.gListBox1.Size = new System.Drawing.Size(208, 93);
            this.gListBox1.TabIndex = 3;
            this.gListBox1.Visible = false;
            this.gListBox1.SelectedIndexChanged += new System.EventHandler(this.listBoxAutoComplete_SelectedIndexChanged);
            this.gListBox1.DoubleClick += new System.EventHandler(this.listBoxAutoComplete_DoubleClick);
            this.gListBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxAutoComplete_KeyDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.codeGridView1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.codeInput2);
            this.Controls.Add(this.gListBox1);
            this.Controls.Add(this.codeInput1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.codeGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox codeInput1;
        private ImageList _myImageList;
        private GListBox gListBox1;
        private TextBox codeInput2;
        private CodeGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private CodeGridView codeGridView1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
    }
}

