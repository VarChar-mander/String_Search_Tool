namespace String_Search_Tool
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtFilepath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.source = new System.Windows.Forms.TextBox();
            this.output = new System.Windows.Forms.TextBox();
            this.Filetype = new System.Windows.Forms.TextBox();
            this.btnBrowse2 = new System.Windows.Forms.Button();
            this.btnBrowse3 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.oneDirectory = new System.Windows.Forms.CheckBox();
            this.printLines = new System.Windows.Forms.CheckBox();
            this.excludeTypes = new System.Windows.Forms.CheckBox();
            this.fullPath = new System.Windows.Forms.CheckBox();
            this.placeHolder = new System.Windows.Forms.CheckBox();
            this.notFound = new System.Windows.Forms.CheckBox();
            this.searchSQL = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(178, 146);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 36);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFilepath
            // 
            this.txtFilepath.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.txtFilepath.Location = new System.Drawing.Point(4, 11);
            this.txtFilepath.Name = "txtFilepath";
            this.txtFilepath.Size = new System.Drawing.Size(169, 20);
            this.txtFilepath.TabIndex = 1;
            this.txtFilepath.Text = "Select directory to search";
            this.txtFilepath.Click += new System.EventHandler(this.txtFilepath_Click);
            this.txtFilepath.TextChanged += new System.EventHandler(this.txtFilepath_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(179, 11);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(78, 19);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // source
            // 
            this.source.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.source.Location = new System.Drawing.Point(4, 38);
            this.source.Name = "source";
            this.source.Size = new System.Drawing.Size(169, 20);
            this.source.TabIndex = 3;
            this.source.Text = "Enter file of search criteria";
            this.source.Click += new System.EventHandler(this.source_Click);
            this.source.TextChanged += new System.EventHandler(this.source_TextChanged);
            // 
            // output
            // 
            this.output.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.output.Location = new System.Drawing.Point(4, 65);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(169, 20);
            this.output.TabIndex = 4;
            this.output.Text = "Enter folder location for results file";
            this.output.Click += new System.EventHandler(this.output_Click);
            this.output.TextChanged += new System.EventHandler(this.output_TextChanged);
            // 
            // Filetype
            // 
            this.Filetype.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.Filetype.Location = new System.Drawing.Point(4, 91);
            this.Filetype.Name = "Filetype";
            this.Filetype.Size = new System.Drawing.Size(71, 20);
            this.Filetype.TabIndex = 5;
            this.Filetype.Text = "Enter file type";
            this.Filetype.Click += new System.EventHandler(this.Filetype_Click);
            this.Filetype.TextChanged += new System.EventHandler(this.Filetype_TextChanged);
            // 
            // btnBrowse2
            // 
            this.btnBrowse2.Location = new System.Drawing.Point(179, 40);
            this.btnBrowse2.Name = "btnBrowse2";
            this.btnBrowse2.Size = new System.Drawing.Size(78, 19);
            this.btnBrowse2.TabIndex = 6;
            this.btnBrowse2.Text = "Browse...";
            this.btnBrowse2.UseVisualStyleBackColor = true;
            this.btnBrowse2.Click += new System.EventHandler(this.btnBrowse2_Click);
            // 
            // btnBrowse3
            // 
            this.btnBrowse3.Location = new System.Drawing.Point(179, 66);
            this.btnBrowse3.Name = "btnBrowse3";
            this.btnBrowse3.Size = new System.Drawing.Size(78, 19);
            this.btnBrowse3.TabIndex = 7;
            this.btnBrowse3.Text = "Browse...";
            this.btnBrowse3.UseVisualStyleBackColor = true;
            this.btnBrowse3.Click += new System.EventHandler(this.btnBrowse3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // oneDirectory
            // 
            this.oneDirectory.AutoSize = true;
            this.oneDirectory.Location = new System.Drawing.Point(4, 119);
            this.oneDirectory.Name = "oneDirectory";
            this.oneDirectory.Size = new System.Drawing.Size(90, 17);
            this.oneDirectory.TabIndex = 8;
            this.oneDirectory.Text = "Ignore folders";
            this.oneDirectory.UseVisualStyleBackColor = true;
            // 
            // printLines
            // 
            this.printLines.AutoSize = true;
            this.printLines.Location = new System.Drawing.Point(100, 165);
            this.printLines.Name = "printLines";
            this.printLines.Size = new System.Drawing.Size(71, 17);
            this.printLines.TabIndex = 9;
            this.printLines.Text = "Print lines";
            this.printLines.UseVisualStyleBackColor = true;
            // 
            // excludeTypes
            // 
            this.excludeTypes.AutoSize = true;
            this.excludeTypes.Enabled = false;
            this.excludeTypes.Location = new System.Drawing.Point(100, 96);
            this.excludeTypes.Name = "excludeTypes";
            this.excludeTypes.Size = new System.Drawing.Size(123, 17);
            this.excludeTypes.TabIndex = 10;
            this.excludeTypes.Text = "Ignore entered types";
            this.excludeTypes.UseVisualStyleBackColor = true;
            // 
            // fullPath
            // 
            this.fullPath.AutoSize = true;
            this.fullPath.Location = new System.Drawing.Point(4, 142);
            this.fullPath.Name = "fullPath";
            this.fullPath.Size = new System.Drawing.Size(87, 17);
            this.fullPath.TabIndex = 11;
            this.fullPath.Text = "Print file path";
            this.fullPath.UseVisualStyleBackColor = true;
            // 
            // placeHolder
            // 
            this.placeHolder.AutoSize = true;
            this.placeHolder.Enabled = false;
            this.placeHolder.Location = new System.Drawing.Point(100, 142);
            this.placeHolder.Name = "placeHolder";
            this.placeHolder.Size = new System.Drawing.Size(72, 17);
            this.placeHolder.TabIndex = 12;
            this.placeHolder.Text = "One entry";
            this.placeHolder.UseVisualStyleBackColor = true;
            this.placeHolder.CheckedChanged += new System.EventHandler(this.placeHolder_CheckedChanged);
            // 
            // notFound
            // 
            this.notFound.AutoSize = true;
            this.notFound.Location = new System.Drawing.Point(4, 165);
            this.notFound.Name = "notFound";
            this.notFound.Size = new System.Drawing.Size(89, 17);
            this.notFound.TabIndex = 13;
            this.notFound.Text = "Unfound only";
            this.notFound.UseVisualStyleBackColor = true;
            // 
            // searchSQL
            // 
            this.searchSQL.AutoSize = true;
            this.searchSQL.Location = new System.Drawing.Point(100, 119);
            this.searchSQL.Name = "searchSQL";
            this.searchSQL.Size = new System.Drawing.Size(121, 17);
            this.searchSQL.TabIndex = 14;
            this.searchSQL.Text = "Perform SQL search";
            this.searchSQL.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 186);
            this.Controls.Add(this.searchSQL);
            this.Controls.Add(this.notFound);
            this.Controls.Add(this.placeHolder);
            this.Controls.Add(this.fullPath);
            this.Controls.Add(this.excludeTypes);
            this.Controls.Add(this.printLines);
            this.Controls.Add(this.oneDirectory);
            this.Controls.Add(this.btnBrowse3);
            this.Controls.Add(this.btnBrowse2);
            this.Controls.Add(this.Filetype);
            this.Controls.Add(this.output);
            this.Controls.Add(this.source);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilepath);
            this.Controls.Add(this.btnSearch);
            this.Name = "Form1";
            this.Text = "String Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtFilepath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox source;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox Filetype;
        private System.Windows.Forms.Button btnBrowse2;
        private System.Windows.Forms.Button btnBrowse3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox oneDirectory;
        private System.Windows.Forms.CheckBox printLines;
        private System.Windows.Forms.CheckBox excludeTypes;
        private System.Windows.Forms.CheckBox fullPath;
        private System.Windows.Forms.CheckBox placeHolder;
        private System.Windows.Forms.CheckBox notFound;
        private System.Windows.Forms.CheckBox searchSQL;
    }
}

