namespace WindowsFormsApp3
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.search = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.platform = new System.Windows.Forms.ComboBox();
            this.upendata = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.office = new System.Windows.Forms.Label();
            this.market = new System.Windows.Forms.Label();
            this.officeDataGrid = new System.Windows.Forms.DataGridView();
            this.info = new System.Windows.Forms.Label();
            this.marketData = new System.Windows.Forms.DataGridView();
            this.clear = new System.Windows.Forms.Button();
            this.point = new System.Windows.Forms.Label();
            this.sort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.languagelabe = new System.Windows.Forms.Label();
            this.language = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.officeDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.marketData)).BeginInit();
            this.SuspendLayout();
            // 
            // search
            // 
            this.search.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.search.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.search.Location = new System.Drawing.Point(217, 26);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(460, 21);
            this.search.TabIndex = 0;
            this.search.TextChanged += new System.EventHandler(this.search_TextChanged);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(683, 26);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(127, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // platform
            // 
            this.platform.FormattingEnabled = true;
            this.platform.Items.AddRange(new object[] {
            "pc",
            "xbox",
            "ps4",
            "switch"});
            this.platform.Location = new System.Drawing.Point(443, 53);
            this.platform.Name = "platform";
            this.platform.Size = new System.Drawing.Size(91, 20);
            this.platform.TabIndex = 2;
            // 
            // upendata
            // 
            this.upendata.Location = new System.Drawing.Point(708, 470);
            this.upendata.Name = "upendata";
            this.upendata.Size = new System.Drawing.Size(102, 24);
            this.upendata.TabIndex = 3;
            this.upendata.Text = "updata";
            this.upendata.UseVisualStyleBackColor = true;
            this.upendata.Click += new System.EventHandler(this.upendata_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "platform";
            // 
            // office
            // 
            this.office.AutoSize = true;
            this.office.Location = new System.Drawing.Point(71, 76);
            this.office.Name = "office";
            this.office.Size = new System.Drawing.Size(0, 12);
            this.office.TabIndex = 5;
            // 
            // market
            // 
            this.market.AutoSize = true;
            this.market.Location = new System.Drawing.Point(71, 187);
            this.market.Name = "market";
            this.market.Size = new System.Drawing.Size(0, 12);
            this.market.TabIndex = 6;
            // 
            // officeDataGrid
            // 
            this.officeDataGrid.BackgroundColor = System.Drawing.SystemColors.Menu;
            this.officeDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.officeDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.officeDataGrid.Location = new System.Drawing.Point(73, 100);
            this.officeDataGrid.Name = "officeDataGrid";
            this.officeDataGrid.RowTemplate.Height = 23;
            this.officeDataGrid.Size = new System.Drawing.Size(843, 68);
            this.officeDataGrid.TabIndex = 7;
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.ForeColor = System.Drawing.Color.Red;
            this.info.Location = new System.Drawing.Point(626, 76);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(0, 12);
            this.info.TabIndex = 8;
            // 
            // marketData
            // 
            this.marketData.BackgroundColor = System.Drawing.SystemColors.Control;
            this.marketData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.marketData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.marketData.Location = new System.Drawing.Point(73, 211);
            this.marketData.Name = "marketData";
            this.marketData.RowTemplate.Height = 23;
            this.marketData.Size = new System.Drawing.Size(843, 253);
            this.marketData.TabIndex = 10;
            this.marketData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.marketData_CellContentClick);
            // 
            // clear
            // 
            this.clear.Location = new System.Drawing.Point(816, 471);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(100, 23);
            this.clear.TabIndex = 11;
            this.clear.Text = "cleardata";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // point
            // 
            this.point.AutoSize = true;
            this.point.ForeColor = System.Drawing.Color.DarkOrange;
            this.point.Location = new System.Drawing.Point(71, 476);
            this.point.Name = "point";
            this.point.Size = new System.Drawing.Size(0, 12);
            this.point.TabIndex = 12;
            // 
            // sort
            // 
            this.sort.FormattingEnabled = true;
            this.sort.Items.AddRange(new object[] {
            "default",
            "price asc",
            "price desc"});
            this.sort.Location = new System.Drawing.Point(586, 53);
            this.sort.Name = "sort";
            this.sort.Size = new System.Drawing.Size(91, 20);
            this.sort.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(551, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "sort";
            // 
            // languagelabe
            // 
            this.languagelabe.AutoSize = true;
            this.languagelabe.Location = new System.Drawing.Point(215, 56);
            this.languagelabe.Name = "languagelabe";
            this.languagelabe.Size = new System.Drawing.Size(53, 12);
            this.languagelabe.TabIndex = 17;
            this.languagelabe.Text = "language";
            // 
            // language
            // 
            this.language.FormattingEnabled = true;
            this.language.Items.AddRange(new object[] {
            "简体中文",
            "繁體中文",
            "English",
            "Русский",
            "Polski",
            "한국어",
            "Deutsch",
            "Français",
            "Português",
            "Español",
            "Svenska"});
            this.language.Location = new System.Drawing.Point(274, 53);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(91, 20);
            this.language.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(325, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 18;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(984, 516);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.languagelabe);
            this.Controls.Add(this.language);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sort);
            this.Controls.Add(this.point);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.marketData);
            this.Controls.Add(this.info);
            this.Controls.Add(this.officeDataGrid);
            this.Controls.Add(this.market);
            this.Controls.Add(this.office);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.upendata);
            this.Controls.Add(this.platform);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.search);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Warfram Search";
            ((System.ComponentModel.ISupportInitialize)(this.officeDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.marketData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ComboBox platform;
        private System.Windows.Forms.Button upendata;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label office;
        private System.Windows.Forms.Label market;
        private System.Windows.Forms.DataGridView officeDataGrid;
        private System.Windows.Forms.Label info;
        private System.Windows.Forms.DataGridView marketData;
        private System.Windows.Forms.Button clear;
        private System.Windows.Forms.Label point;
        private System.Windows.Forms.ComboBox sort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label languagelabe;
        private System.Windows.Forms.ComboBox language;
        private System.Windows.Forms.Label label3;
    }
}

