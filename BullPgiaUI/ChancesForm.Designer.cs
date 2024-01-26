namespace BullPgiaUI
{
    partial class ChancesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChancesForm));
            this.numberOfChancesButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // numberOfChancesButton
            // 
            resources.ApplyResources(this.numberOfChancesButton, "numberOfChancesButton");
            this.numberOfChancesButton.Name = "numberOfChancesButton";
            this.numberOfChancesButton.UseVisualStyleBackColor = true;
            this.numberOfChancesButton.Click += new System.EventHandler(this.chancesButton_Click);
            // 
            // startButton
            // 
            resources.ApplyResources(this.startButton, "startButton");
            this.startButton.BackColor = System.Drawing.SystemColors.Menu;
            this.startButton.Name = "startButton";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ChancesForm
            // 
            this.AcceptButton = this.startButton;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.numberOfChancesButton);
            this.Name = "ChancesForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChancesForm_FormClosed);
            this.Load += new System.EventHandler(this.ChancesForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button numberOfChancesButton;
        private System.Windows.Forms.Button startButton;
    }
}