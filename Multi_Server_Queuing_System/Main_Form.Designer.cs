namespace Single_Server_Queuing_System
{
    partial class Main_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.button_Multi_Server = new System.Windows.Forms.Button();
            this.label_Header = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Multi_Server
            // 
            this.button_Multi_Server.BackColor = System.Drawing.Color.MintCream;
            this.button_Multi_Server.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Multi_Server.ForeColor = System.Drawing.Color.DimGray;
            this.button_Multi_Server.Location = new System.Drawing.Point(12, 115);
            this.button_Multi_Server.Name = "button_Multi_Server";
            this.button_Multi_Server.Size = new System.Drawing.Size(353, 94);
            this.button_Multi_Server.TabIndex = 0;
            this.button_Multi_Server.Text = "Multi Server Queueing System";
            this.button_Multi_Server.UseVisualStyleBackColor = false;
            this.button_Multi_Server.Click += new System.EventHandler(this.button_Multi_Server_Click);
            // 
            // label_Header
            // 
            this.label_Header.Font = new System.Drawing.Font("Showcard Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Header.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_Header.Location = new System.Drawing.Point(58, 37);
            this.label_Header.Name = "label_Header";
            this.label_Header.Size = new System.Drawing.Size(267, 37);
            this.label_Header.TabIndex = 1;
            this.label_Header.Text = "Simulation   Lab   Work";
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(397, 261);
            this.Controls.Add(this.label_Header);
            this.Controls.Add(this.button_Multi_Server);
            this.Name = "Main_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main_Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Multi_Server;
        private System.Windows.Forms.Label label_Header;
    }
}