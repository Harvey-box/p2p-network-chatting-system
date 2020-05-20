namespace WindowsFormsApp1
{
    partial class chat_private
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
            this.enter_TextBox = new System.Windows.Forms.RichTextBox();
            this.submit_button = new System.Windows.Forms.Button();
            this.chatrecord_listView = new System.Windows.Forms.ListView();
            this.sendfile_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // enter_TextBox
            // 
            this.enter_TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.enter_TextBox.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.enter_TextBox.Location = new System.Drawing.Point(12, 389);
            this.enter_TextBox.Name = "enter_TextBox";
            this.enter_TextBox.Size = new System.Drawing.Size(486, 82);
            this.enter_TextBox.TabIndex = 0;
            this.enter_TextBox.Text = "";
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(423, 359);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(75, 23);
            this.submit_button.TabIndex = 3;
            this.submit_button.Text = "submit";
            this.submit_button.UseVisualStyleBackColor = true;
            this.submit_button.Click += new System.EventHandler(this.submit_button_Click);
            // 
            // chatrecord_listView
            // 
            this.chatrecord_listView.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chatrecord_listView.HideSelection = false;
            this.chatrecord_listView.Location = new System.Drawing.Point(12, 12);
            this.chatrecord_listView.Name = "chatrecord_listView";
            this.chatrecord_listView.Size = new System.Drawing.Size(486, 341);
            this.chatrecord_listView.TabIndex = 1;
            this.chatrecord_listView.UseCompatibleStateImageBehavior = false;
            this.chatrecord_listView.View = System.Windows.Forms.View.List;
            // 
            // sendfile_button
            // 
            this.sendfile_button.Location = new System.Drawing.Point(12, 360);
            this.sendfile_button.Name = "sendfile_button";
            this.sendfile_button.Size = new System.Drawing.Size(75, 23);
            this.sendfile_button.TabIndex = 4;
            this.sendfile_button.Text = "send file";
            this.sendfile_button.UseVisualStyleBackColor = true;
            this.sendfile_button.Click += new System.EventHandler(this.sendfile_button_Click);
            // 
            // chat_private
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 483);
            this.Controls.Add(this.sendfile_button);
            this.Controls.Add(this.chatrecord_listView);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.enter_TextBox);
            this.Name = "chat_private";
            this.Text = "chat_private";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox enter_TextBox;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.ListView chatrecord_listView;
        private System.Windows.Forms.Button sendfile_button;
    }
}