
namespace MoodleXML
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ReadFilebtn = new System.Windows.Forms.Button();
            this.ExportXML = new System.Windows.Forms.Button();
            this.ReturnRemain = new System.Windows.Forms.Button();
            this.chkdoubleTasks = new System.Windows.Forms.CheckBox();
            this.checkStd = new System.Windows.Forms.CheckBox();
            this.btnDoc = new System.Windows.Forms.Button();
            this.chkRandOrder = new System.Windows.Forms.CheckBox();
            this.textStartorig = new System.Windows.Forms.TextBox();
            this.textStartchange = new System.Windows.Forms.TextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // ReadFilebtn
            // 
            resources.ApplyResources(this.ReadFilebtn, "ReadFilebtn");
            this.ReadFilebtn.Name = "ReadFilebtn";
            this.ReadFilebtn.UseVisualStyleBackColor = true;
            this.ReadFilebtn.Click += new System.EventHandler(this.ReadFilebtn_Click);
            // 
            // ExportXML
            // 
            resources.ApplyResources(this.ExportXML, "ExportXML");
            this.ExportXML.Name = "ExportXML";
            this.ExportXML.UseVisualStyleBackColor = true;
            this.ExportXML.Click += new System.EventHandler(this.RandomOrder_Click);
            // 
            // ReturnRemain
            // 
            resources.ApplyResources(this.ReturnRemain, "ReturnRemain");
            this.ReturnRemain.Name = "ReturnRemain";
            this.ReturnRemain.UseVisualStyleBackColor = true;
            this.ReturnRemain.Click += new System.EventHandler(this.ReturnRemain_Click);
            // 
            // chkdoubleTasks
            // 
            resources.ApplyResources(this.chkdoubleTasks, "chkdoubleTasks");
            this.chkdoubleTasks.Name = "chkdoubleTasks";
            this.chkdoubleTasks.UseVisualStyleBackColor = true;
            this.chkdoubleTasks.CheckedChanged += new System.EventHandler(this.chkdoubleTasks_CheckedChanged);
            // 
            // checkStd
            // 
            resources.ApplyResources(this.checkStd, "checkStd");
            this.checkStd.Name = "checkStd";
            this.checkStd.UseVisualStyleBackColor = true;
            this.checkStd.CheckedChanged += new System.EventHandler(this.checkStd_CheckedChanged);
            // 
            // btnDoc
            // 
            resources.ApplyResources(this.btnDoc, "btnDoc");
            this.btnDoc.Name = "btnDoc";
            this.btnDoc.UseVisualStyleBackColor = true;
            this.btnDoc.Click += new System.EventHandler(this.btnDoc_Click);
            // 
            // chkRandOrder
            // 
            resources.ApplyResources(this.chkRandOrder, "chkRandOrder");
            this.chkRandOrder.Name = "chkRandOrder";
            this.chkRandOrder.UseVisualStyleBackColor = true;
            // 
            // textStartorig
            // 
            resources.ApplyResources(this.textStartorig, "textStartorig");
            this.textStartorig.Name = "textStartorig";
            this.textStartorig.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textStartorig_KeyPress);
            // 
            // textStartchange
            // 
            resources.ApplyResources(this.textStartchange, "textStartchange");
            this.textStartchange.Name = "textStartchange";
            this.textStartchange.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textStartchange_KeyPress);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.textStartchange);
            this.Controls.Add(this.textStartorig);
            this.Controls.Add(this.chkRandOrder);
            this.Controls.Add(this.btnDoc);
            this.Controls.Add(this.checkStd);
            this.Controls.Add(this.chkdoubleTasks);
            this.Controls.Add(this.ReturnRemain);
            this.Controls.Add(this.ReadFilebtn);
            this.Controls.Add(this.ExportXML);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReadFilebtn;
        private System.Windows.Forms.Button ExportXML;
        private System.Windows.Forms.Button ReturnRemain;
        private System.Windows.Forms.CheckBox chkdoubleTasks;
        private System.Windows.Forms.CheckBox checkStd;
        private System.Windows.Forms.Button btnDoc;
        private System.Windows.Forms.CheckBox chkRandOrder;
        private System.Windows.Forms.TextBox textStartorig;
        private System.Windows.Forms.TextBox textStartchange;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

