namespace C969Project
{
    partial class customerForm
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
            this.activeBox = new System.Windows.Forms.CheckBox();
            this.submitButton = new System.Windows.Forms.Button();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.customernameBox = new System.Windows.Forms.TextBox();
            this.address2Box = new System.Windows.Forms.TextBox();
            this.cityBox = new System.Windows.Forms.TextBox();
            this.countryBox = new System.Windows.Forms.TextBox();
            this.zipcodeBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.phoneBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // activeBox
            // 
            this.activeBox.AutoSize = true;
            this.activeBox.Location = new System.Drawing.Point(247, 21);
            this.activeBox.Name = "activeBox";
            this.activeBox.Size = new System.Drawing.Size(103, 17);
            this.activeBox.TabIndex = 0;
            this.activeBox.Text = "Customer Active";
            this.activeBox.UseVisualStyleBackColor = true;
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(247, 366);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 1;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(13, 91);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(274, 20);
            this.addressBox.TabIndex = 3;
            // 
            // customernameBox
            // 
            this.customernameBox.Location = new System.Drawing.Point(13, 53);
            this.customernameBox.Name = "customernameBox";
            this.customernameBox.Size = new System.Drawing.Size(274, 20);
            this.customernameBox.TabIndex = 4;
            // 
            // address2Box
            // 
            this.address2Box.Location = new System.Drawing.Point(12, 128);
            this.address2Box.Name = "address2Box";
            this.address2Box.Size = new System.Drawing.Size(274, 20);
            this.address2Box.TabIndex = 5;
            // 
            // cityBox
            // 
            this.cityBox.Location = new System.Drawing.Point(13, 168);
            this.cityBox.Name = "cityBox";
            this.cityBox.Size = new System.Drawing.Size(274, 20);
            this.cityBox.TabIndex = 6;
            // 
            // countryBox
            // 
            this.countryBox.Location = new System.Drawing.Point(12, 255);
            this.countryBox.Name = "countryBox";
            this.countryBox.Size = new System.Drawing.Size(274, 20);
            this.countryBox.TabIndex = 7;
            // 
            // zipcodeBox
            // 
            this.zipcodeBox.Location = new System.Drawing.Point(12, 209);
            this.zipcodeBox.Name = "zipcodeBox";
            this.zipcodeBox.Size = new System.Drawing.Size(274, 20);
            this.zipcodeBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Customer Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Address Line Cont\'d";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "City";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Zipcode / Postal Code";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Country";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 278);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Phone Number";
            // 
            // phoneBox
            // 
            this.phoneBox.Location = new System.Drawing.Point(12, 294);
            this.phoneBox.Name = "phoneBox";
            this.phoneBox.Size = new System.Drawing.Size(125, 20);
            this.phoneBox.TabIndex = 16;
            this.phoneBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.phoneBox_KeyPress);
            // 
            // customerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 412);
            this.Controls.Add(this.phoneBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zipcodeBox);
            this.Controls.Add(this.countryBox);
            this.Controls.Add(this.cityBox);
            this.Controls.Add(this.address2Box);
            this.Controls.Add(this.customernameBox);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.activeBox);
            this.MaximumSize = new System.Drawing.Size(400, 451);
            this.MinimumSize = new System.Drawing.Size(400, 451);
            this.Name = "customerForm";
            this.Text = "Customer Record";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox activeBox;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.TextBox customernameBox;
        private System.Windows.Forms.TextBox address2Box;
        private System.Windows.Forms.TextBox cityBox;
        private System.Windows.Forms.TextBox countryBox;
        private System.Windows.Forms.TextBox zipcodeBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox phoneBox;
    }
}