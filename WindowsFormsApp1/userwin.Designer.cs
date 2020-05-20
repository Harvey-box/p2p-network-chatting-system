namespace WindowsFormsApp1
{
    partial class userwin
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
            this.frd_username_text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.start_button = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.userdata_listView = new System.Windows.Forms.ListView();
            this.chat_button = new System.Windows.Forms.Button();
            this.update_button = new System.Windows.Forms.Button();
            this.logout_button = new System.Windows.Forms.Button();
            this.group_chat_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // frd_username_text
            // 
            this.frd_username_text.Location = new System.Drawing.Point(12, 60);
            this.frd_username_text.Name = "frd_username_text";
            this.frd_username_text.Size = new System.Drawing.Size(100, 25);
            this.frd_username_text.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(9, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "find your friend: ";
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(146, 61);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 2;
            this.start_button.Text = "find";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.chat_private_button_Click);
            // 
            // userdata_listView
            // 
            this.userdata_listView.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.userdata_listView.GridLines = true;
            this.userdata_listView.HideSelection = false;
            this.userdata_listView.Location = new System.Drawing.Point(12, 104);
            this.userdata_listView.Name = "userdata_listView";
            this.userdata_listView.Size = new System.Drawing.Size(401, 334);
            this.userdata_listView.TabIndex = 2;
            this.userdata_listView.UseCompatibleStateImageBehavior = false;
            this.userdata_listView.View = System.Windows.Forms.View.Details;
            // 
            // chat_button
            // 
            this.chat_button.Location = new System.Drawing.Point(455, 137);
            this.chat_button.Name = "chat_button";
            this.chat_button.Size = new System.Drawing.Size(75, 23);
            this.chat_button.TabIndex = 2;
            this.chat_button.Text = "chat";
            this.chat_button.UseVisualStyleBackColor = true;
            this.chat_button.Click += new System.EventHandler(this.chat_button_Click);
            // 
            // update_button
            // 
            this.update_button.Location = new System.Drawing.Point(244, 62);
            this.update_button.Name = "update_button";
            this.update_button.Size = new System.Drawing.Size(75, 23);
            this.update_button.TabIndex = 3;
            this.update_button.Text = "update";
            this.update_button.UseVisualStyleBackColor = true;
            this.update_button.Click += new System.EventHandler(this.update_button_Click);
            // 
            // logout_button
            // 
            this.logout_button.Location = new System.Drawing.Point(338, 62);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(75, 23);
            this.logout_button.TabIndex = 4;
            this.logout_button.Text = "logout";
            this.logout_button.UseVisualStyleBackColor = true;
            this.logout_button.Click += new System.EventHandler(this.logout_button_Click);
            // 
            // group_chat_button
            // 
            this.group_chat_button.Location = new System.Drawing.Point(455, 193);
            this.group_chat_button.Name = "group_chat_button";
            this.group_chat_button.Size = new System.Drawing.Size(75, 23);
            this.group_chat_button.TabIndex = 5;
            this.group_chat_button.Text = "group chat";
            this.group_chat_button.UseVisualStyleBackColor = true;
            this.group_chat_button.Click += new System.EventHandler(this.group_chat_button_Click);
            // 
            // userwin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 450);
            this.Controls.Add(this.group_chat_button);
            this.Controls.Add(this.logout_button);
            this.Controls.Add(this.update_button);
            this.Controls.Add(this.chat_button);
            this.Controls.Add(this.userdata_listView);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.frd_username_text);
            this.Name = "userwin";
            this.Text = "userwin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox frd_username_text;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button start_button;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView userdata_listView;
        private System.Windows.Forms.Button chat_button;
        private System.Windows.Forms.Button update_button;
        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.Button group_chat_button;
    }
}