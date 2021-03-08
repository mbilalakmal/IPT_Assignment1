
namespace k173669_Q3
{
    partial class StocksPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scripsDataGridView = new System.Windows.Forms.DataGridView();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scripsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // scripsDataGridView
            // 
            this.scripsDataGridView.AccessibleName = "ScripsDataGridView";
            this.scripsDataGridView.AllowUserToAddRows = false;
            this.scripsDataGridView.AllowUserToDeleteRows = false;
            this.scripsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scripsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.scripsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scripsDataGridView.Location = new System.Drawing.Point(12, 179);
            this.scripsDataGridView.Name = "scripsDataGridView";
            this.scripsDataGridView.ReadOnly = true;
            this.scripsDataGridView.RowHeadersWidth = 51;
            this.scripsDataGridView.RowTemplate.Height = 29;
            this.scripsDataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.scripsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.scripsDataGridView.Size = new System.Drawing.Size(776, 259);
            this.scripsDataGridView.TabIndex = 0;
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.categoryComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.categoryComboBox.DropDownWidth = 360;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(12, 121);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(360, 28);
            this.categoryComboBox.TabIndex = 1;
            this.categoryComboBox.SelectionChangeCommitted += new System.EventHandler(this.categoryComboBox_SelectionChangeCommitted);
            this.categoryComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.categoryComboBox_KeyDown);
            // 
            // refreshButton
            // 
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshButton.Location = new System.Drawing.Point(390, 121);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(118, 28);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "REFRESH";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // StocksPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.scripsDataGridView);
            this.Name = "StocksPage";
            this.Text = "Stocks";
            ((System.ComponentModel.ISupportInitialize)(this.scripsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView scripsDataGridView;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Button refreshButton;
    }
}

