namespace WindowsFormsApp1
{
    partial class chat_group
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
            this.sendfile_button = new System.Windows.Forms.Button();
            this.chatrecord_listView = new System.Windows.Forms.ListView();
            this.submit_button = new System.Windows.Forms.Button();
            this.enter_TextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.member_listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // sendfile_button
            // 
            this.sendfile_button.Location = new System.Drawing.Point(12, 357);
            this.sendfile_button.Name = "sendfile_button";
            this.sendfile_button.Size = new System.Drawing.Size(75, 23);
            this.sendfile_button.TabIndex = 8;
            this.sendfile_button.Text = "send file";
            this.sendfile_button.UseVisualStyleBackColor = true;
            // 
            // chatrecord_listView
            // 
            this.chatrecord_listView.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chatrecord_listView.HideSelection = false;
            this.chatrecord_listView.Location = new System.Drawing.Point(12, 9);
            this.chatrecord_listView.Name = "chatrecord_listView";
            this.chatrecord_listView.Size = new System.Drawing.Size(486, 341);
            this.chatrecord_listView.TabIndex = 6;
            this.chatrecord_listView.UseCompatibleStateImageBehavior = false;
            this.chatrecord_listView.View = System.Windows.Forms.View.List;
            // 
            // submit_button
            // 
            this.submit_button.Location = new System.Drawing.Point(423, 356);
            this.submit_button.Name = "submit_button";
            this.submit_button.Size = new System.Drawing.Size(75, 23);
            this.submit_button.TabIndex = 7;
            this.submit_button.Text = "submit";
            this.submit_button.UseVisualStyleBackColor = true;
            // 
            // enter_TextBox
            // 
            this.enter_TextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.enter_TextBox.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.enter_TextBox.Location = new System.Drawing.Point(12, 386);
            this.enter_TextBox.Name = "enter_TextBox";
            this.enter_TextBox.Size = new System.Drawing.Size(486, 82);
            this.enter_TextBox.TabIndex = 5;
            this.enter_TextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(514, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Members";
            // 
            // member_listView
            // 
            this.member_listView.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.member_listView.HideSelection = false;
            this.member_listView.Location = new System.Drawing.Point(504, 39);
            this.member_listView.Name = "member_listView";
            this.member_listView.Size = new System.Drawing.Size(160, 425);
            this.member_listView.TabIndex = 10;
            this.member_listView.UseCompatibleStateImageBehavior = false;
            this.member_listView.View = System.Windows.Forms.View.List;
            // 
            // chat_group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 476);
            this.Controls.Add(this.member_listView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendfile_button);
            this.Controls.Add(this.chatrecord_listView);
            this.Controls.Add(this.submit_button);
            this.Controls.Add(this.enter_TextBox);
            this.Name = "chat_group";
            this.Text = "chat_group";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button sendfile_button;
        private System.Windows.Forms.ListView chatrecord_listView;
        private System.Windows.Forms.Button submit_button;
        private System.Windows.Forms.RichTextBox enter_TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView member_listView;
    }
}