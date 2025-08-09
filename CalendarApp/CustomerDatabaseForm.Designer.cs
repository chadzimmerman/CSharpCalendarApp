namespace CalendarApp
{
    partial class CustomerDatabaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerDatabaseForm));
            this.DatagridViewCustomers = new System.Windows.Forms.DataGridView();
            this.AddButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.PhoneTextbox = new System.Windows.Forms.TextBox();
            this.AddressTextbox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.PhoneLabel = new System.Windows.Forms.Label();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.Address2Label = new System.Windows.Forms.Label();
            this.PostalCodeLabel = new System.Windows.Forms.Label();
            this.CityLabel = new System.Windows.Forms.Label();
            this.Address2Textbox = new System.Windows.Forms.TextBox();
            this.PostalCodeTextbox = new System.Windows.Forms.TextBox();
            this.IsActiveCheckbox = new System.Windows.Forms.CheckBox();
            this.IsActiveLabel = new System.Windows.Forms.Label();
            this.CountryLabel = new System.Windows.Forms.Label();
            this.CityComboBox = new System.Windows.Forms.ComboBox();
            this.CountryComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DatagridViewCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // DatagridViewCustomers
            // 
            this.DatagridViewCustomers.AllowUserToAddRows = false;
            this.DatagridViewCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.DatagridViewCustomers, "DatagridViewCustomers");
            this.DatagridViewCustomers.Name = "DatagridViewCustomers";
            this.DatagridViewCustomers.ReadOnly = true;
            this.DatagridViewCustomers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.DatagridViewCustomers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DatagridViewCustomers_CellDoubleClick);
            // 
            // AddButton
            // 
            resources.ApplyResources(this.AddButton, "AddButton");
            this.AddButton.Name = "AddButton";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // UpdateButton
            // 
            resources.ApplyResources(this.UpdateButton, "UpdateButton");
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // DeleteButton
            // 
            resources.ApplyResources(this.DeleteButton, "DeleteButton");
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // RefreshButton
            // 
            resources.ApplyResources(this.RefreshButton, "RefreshButton");
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // NameTextbox
            // 
            resources.ApplyResources(this.NameTextbox, "NameTextbox");
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.TextChanged += new System.EventHandler(this.NameTextbox_TextChanged);
            // 
            // PhoneTextbox
            // 
            resources.ApplyResources(this.PhoneTextbox, "PhoneTextbox");
            this.PhoneTextbox.Name = "PhoneTextbox";
            // 
            // AddressTextbox
            // 
            resources.ApplyResources(this.AddressTextbox, "AddressTextbox");
            this.AddressTextbox.Name = "AddressTextbox";
            // 
            // NameLabel
            // 
            resources.ApplyResources(this.NameLabel, "NameLabel");
            this.NameLabel.Name = "NameLabel";
            // 
            // PhoneLabel
            // 
            resources.ApplyResources(this.PhoneLabel, "PhoneLabel");
            this.PhoneLabel.Name = "PhoneLabel";
            // 
            // AddressLabel
            // 
            resources.ApplyResources(this.AddressLabel, "AddressLabel");
            this.AddressLabel.Name = "AddressLabel";
            // 
            // Address2Label
            // 
            resources.ApplyResources(this.Address2Label, "Address2Label");
            this.Address2Label.Name = "Address2Label";
            this.Address2Label.Click += new System.EventHandler(this.label1_Click);
            // 
            // PostalCodeLabel
            // 
            resources.ApplyResources(this.PostalCodeLabel, "PostalCodeLabel");
            this.PostalCodeLabel.Name = "PostalCodeLabel";
            this.PostalCodeLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // CityLabel
            // 
            resources.ApplyResources(this.CityLabel, "CityLabel");
            this.CityLabel.Name = "CityLabel";
            this.CityLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // Address2Textbox
            // 
            resources.ApplyResources(this.Address2Textbox, "Address2Textbox");
            this.Address2Textbox.Name = "Address2Textbox";
            this.Address2Textbox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PostalCodeTextbox
            // 
            resources.ApplyResources(this.PostalCodeTextbox, "PostalCodeTextbox");
            this.PostalCodeTextbox.Name = "PostalCodeTextbox";
            this.PostalCodeTextbox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // IsActiveCheckbox
            // 
            resources.ApplyResources(this.IsActiveCheckbox, "IsActiveCheckbox");
            this.IsActiveCheckbox.Checked = true;
            this.IsActiveCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IsActiveCheckbox.Name = "IsActiveCheckbox";
            this.IsActiveCheckbox.UseVisualStyleBackColor = true;
            // 
            // IsActiveLabel
            // 
            resources.ApplyResources(this.IsActiveLabel, "IsActiveLabel");
            this.IsActiveLabel.Name = "IsActiveLabel";
            // 
            // CountryLabel
            // 
            resources.ApplyResources(this.CountryLabel, "CountryLabel");
            this.CountryLabel.Name = "CountryLabel";
            this.CountryLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // CityComboBox
            // 
            this.CityComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.CityComboBox, "CityComboBox");
            this.CityComboBox.Name = "CityComboBox";
            // 
            // CountryComboBox
            // 
            this.CountryComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.CountryComboBox, "CountryComboBox");
            this.CountryComboBox.Name = "CountryComboBox";
            // 
            // CustomerDatabaseForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.CountryComboBox);
            this.Controls.Add(this.CityComboBox);
            this.Controls.Add(this.CountryLabel);
            this.Controls.Add(this.IsActiveLabel);
            this.Controls.Add(this.IsActiveCheckbox);
            this.Controls.Add(this.Address2Label);
            this.Controls.Add(this.PostalCodeLabel);
            this.Controls.Add(this.CityLabel);
            this.Controls.Add(this.Address2Textbox);
            this.Controls.Add(this.PostalCodeTextbox);
            this.Controls.Add(this.AddressLabel);
            this.Controls.Add(this.PhoneLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.AddressTextbox);
            this.Controls.Add(this.PhoneTextbox);
            this.Controls.Add(this.NameTextbox);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.DatagridViewCustomers);
            this.Name = "CustomerDatabaseForm";
            ((System.ComponentModel.ISupportInitialize)(this.DatagridViewCustomers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DatagridViewCustomers;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.TextBox PhoneTextbox;
        private System.Windows.Forms.TextBox AddressTextbox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label PhoneLabel;
        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.Label Address2Label;
        private System.Windows.Forms.Label PostalCodeLabel;
        private System.Windows.Forms.Label CityLabel;
        private System.Windows.Forms.TextBox Address2Textbox;
        private System.Windows.Forms.TextBox PostalCodeTextbox;
        private System.Windows.Forms.CheckBox IsActiveCheckbox;
        private System.Windows.Forms.Label IsActiveLabel;
        private System.Windows.Forms.Label CountryLabel;
        private System.Windows.Forms.ComboBox CityComboBox;
        private System.Windows.Forms.ComboBox CountryComboBox;
    }
}