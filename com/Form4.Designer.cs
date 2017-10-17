namespace sf
{
    partial class Form4
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点8");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点9");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点10");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点11");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点12");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点13");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点14");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点3", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点2", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("节点4", new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点7";
            treeNode1.Text = "节点7";
            treeNode2.Name = "节点8";
            treeNode2.Text = "节点8";
            treeNode3.Name = "1";
            treeNode3.Text = "节点0";
            treeNode4.Name = "节点9";
            treeNode4.Text = "节点9";
            treeNode5.Name = "节点10";
            treeNode5.Text = "节点10";
            treeNode6.Name = "节点1";
            treeNode6.Text = "节点1";
            treeNode7.Name = "节点11";
            treeNode7.Text = "节点11";
            treeNode8.Name = "节点12";
            treeNode8.Text = "节点12";
            treeNode9.Name = "节点13";
            treeNode9.Text = "节点13";
            treeNode10.Name = "节点14";
            treeNode10.Text = "节点14";
            treeNode11.Name = "节点3";
            treeNode11.Text = "节点3";
            treeNode12.Name = "节点2";
            treeNode12.Text = "节点2";
            treeNode13.Name = "节点5";
            treeNode13.Text = "节点5";
            treeNode14.Name = "节点6";
            treeNode14.Text = "节点6";
            treeNode15.Name = "节点4";
            treeNode15.Text = "节点4";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode6,
            treeNode12,
            treeNode15});
            this.treeView1.Size = new System.Drawing.Size(455, 365);
            this.treeView1.TabIndex = 0;
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 365);
            this.Controls.Add(this.treeView1);
            this.Name = "Form4";
            this.Text = "树形显示";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
    }
}