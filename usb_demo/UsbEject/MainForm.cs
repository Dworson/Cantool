// UsbEject version 1.0 March 2006
// written by Simon Mourier <email: simon [underscore] mourier [at] hotmail [dot] com>

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using System.Data;
using UsbEject.Library;

namespace UsbEject
{
	public class MainForm: System.Windows.Forms.Form
    {
        private SplitContainer splitContainer1;
        private TreeView treeViewDisks;
        private PropertyGrid propertyGridDevice;
        private MenuStrip mainMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem aToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem usbOnlyToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private IContainer components;
        private ToolStripMenuItem ejectToolStripMenuItem;
        private bool _loading;

		private enum IconIndex
		{
			Volume = 0,
			MyComputer = 1,
			Box = 2,
			Disk = 3,
			Eject = 4
		}

		public MainForm()
		{
			InitializeComponent();

			// load icons from resource
			ResourceManager resourceManager = new ResourceManager("UsbEject.Icons", GetType().Module.Assembly);
			treeViewDisks.ImageList = new ImageList();
			treeViewDisks.ImageList.Images.Add((Icon)resourceManager.GetObject("ico9.ico"));
			treeViewDisks.ImageList.Images.Add((Icon)resourceManager.GetObject("ico16.ico"));
			treeViewDisks.ImageList.Images.Add((Icon)resourceManager.GetObject("ico27.ico"));
			treeViewDisks.ImageList.Images.Add((Icon)resourceManager.GetObject("ico233.ico"));
			treeViewDisks.ImageList.Images.Add((Icon)resourceManager.GetObject("UsbEject.ico"));

            LoadItems();
        }

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewDisks = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ejectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usbOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertyGridDevice = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewDisks);
            this.splitContainer1.Panel1.Controls.Add(this.mainMenuStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGridDevice);
            this.splitContainer1.Size = new System.Drawing.Size(541, 486);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewDisks
            // 
            this.treeViewDisks.ContextMenuStrip = this.contextMenuStrip;
            this.treeViewDisks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDisks.FullRowSelect = true;
            this.treeViewDisks.HideSelection = false;
            this.treeViewDisks.Location = new System.Drawing.Point(0, 24);
            this.treeViewDisks.Name = "treeViewDisks";
            this.treeViewDisks.Size = new System.Drawing.Size(541, 206);
            this.treeViewDisks.TabIndex = 1;
            this.treeViewDisks.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDisks_AfterSelect);
            this.treeViewDisks.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDisks_NodeMouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ejectToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 26);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // ejectToolStripMenuItem
            // 
            this.ejectToolStripMenuItem.Name = "ejectToolStripMenuItem";
            this.ejectToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ejectToolStripMenuItem.Text = "&Eject";
            this.ejectToolStripMenuItem.Click += new System.EventHandler(this.ejectToolStripMenuItem_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(541, 24);
            this.mainMenuStrip.TabIndex = 2;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usbOnlyToolStripMenuItem,
            this.toolStripSeparator1,
            this.refreshToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // usbOnlyToolStripMenuItem
            // 
            this.usbOnlyToolStripMenuItem.Checked = true;
            this.usbOnlyToolStripMenuItem.CheckOnClick = true;
            this.usbOnlyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usbOnlyToolStripMenuItem.Name = "usbOnlyToolStripMenuItem";
            this.usbOnlyToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.usbOnlyToolStripMenuItem.Text = "USB Only";
            this.usbOnlyToolStripMenuItem.Click += new System.EventHandler(this.usbOnlyToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(115, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.aToolStripMenuItem.Text = "&About...";
            this.aToolStripMenuItem.Click += new System.EventHandler(this.aToolStripMenuItem_Click);
            // 
            // propertyGridDevice
            // 
            this.propertyGridDevice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridDevice.HelpVisible = false;
            this.propertyGridDevice.Location = new System.Drawing.Point(0, 0);
            this.propertyGridDevice.Name = "propertyGridDevice";
            this.propertyGridDevice.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGridDevice.Size = new System.Drawing.Size(541, 252);
            this.propertyGridDevice.TabIndex = 0;
            this.propertyGridDevice.ToolbarVisible = false;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(541, 486);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "USB Disk Eject";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			About about = new About();
			about.ShowDialog(this);
		}

		private void menuItemRefresh_Click(object sender, System.EventArgs e)
		{
            LoadItems();
		}

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Native.WM_DEVICECHANGE)
            {
                if (!_loading)
                {
                    LoadItems();
                }
            }
            base.WndProc(ref m);
        }

        private void LoadItems()
		{
            _loading = true;
			treeViewDisks.Nodes.Clear();

			TreeNode root = treeViewDisks.Nodes.Add("Computer");
			root.ImageIndex = (int)IconIndex.MyComputer;
			root.SelectedImageIndex = root.ImageIndex;

            // display volumes
            VolumeDeviceClass volumeDeviceClass = new VolumeDeviceClass();
            TreeNode volumesNode = new TreeNode("Volumes");
            volumesNode.ImageIndex = (int)IconIndex.Volume;
            volumesNode.SelectedImageIndex = volumesNode.ImageIndex;
            root.Nodes.Add(volumesNode);

            foreach (Volume device in volumeDeviceClass.Devices)
            {
                if ((usbOnlyToolStripMenuItem.Checked) && (!device.IsUsb))
                    continue;

                string text = null;
                if ((device.LogicalDrive != null) && (device.LogicalDrive.Length > 0))
                {
                    text += device.LogicalDrive;
                }

                if (text != null)
                {
                    text += " ";
                }
                text += device.Description;
                if (device.FriendlyName != null)
                {
                    if (text != null)
                    {
                        text += " - ";
                    }
                    text += device.FriendlyName;
                }

                TreeNode deviceNode = volumesNode.Nodes.Add(text);

                if (device.IsUsb)
                {
                    deviceNode.ImageIndex = (int)IconIndex.Box;
                    deviceNode.SelectedImageIndex = deviceNode.ImageIndex;
                }
                deviceNode.Tag = device;

                foreach (Device disk in device.Disks)
                {
                    TreeNode diskNode = deviceNode.Nodes.Add(disk.Description + " - " + disk.FriendlyName);
                    diskNode.ImageIndex = deviceNode.ImageIndex;
                    diskNode.SelectedImageIndex = diskNode.ImageIndex;
                    diskNode.Tag = device;
                }
            }

            root.ExpandAll();
            _loading = false;
        }

        private void treeViewDisks_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // update property grid
            Device device = (Device)e.Node.Tag;
            if (device == null)
            {
                propertyGridDevice.SelectedObject = null;
                return;
            }

            propertyGridDevice.SelectedObject = device;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog(this);
        }

        private void usbOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void ejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Device device = GetSelectedDevice();
            if (device == null)
                return;

            string s = device.Eject(true);
            if (s != null)
            {
                MessageBox.Show(this, s, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void treeViewDisks_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewDisks.SelectedNode = e.Node;
        }

        private Device GetSelectedDevice()
        {
            TreeNode node = treeViewDisks.SelectedNode;
            if (node == null)
            {
                return null;
            }

            return (Device)node.Tag;
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            Device device = GetSelectedDevice();
            if ((device == null) || (!device.IsUsb))
            {
                e.Cancel = true;
                return;
            }
        }
	}
}
