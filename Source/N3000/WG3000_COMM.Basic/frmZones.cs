using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class frmZones : frmN3000
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAddSuper;

		private ToolStripButton btnAdd;

		private ToolStripSeparator toolStripSplitButton1;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton btnEditDept;

		private ToolStripSeparator toolStripSeparator2;

		private ToolStripButton btnDeleteDept;

		private ToolStrip toolStrip2;

		private ToolStripLabel toolStripLabel1;

		private ToolStripTextBox txtSelectedDept;

		private TreeView trvDepartments;

		public frmZones()
		{
			this.InitializeComponent();
		}

		private void FindRecursive(TreeNode treeNode, string ParentNodeText, out TreeNode foundNode)
		{
			foundNode = null;
			if (treeNode.Tag.ToString() == ParentNodeText)
			{
				foundNode = treeNode;
				return;
			}
			if (foundNode == null)
			{
				foreach (TreeNode treeNode2 in treeNode.Nodes)
				{
					if (foundNode != null)
					{
						break;
					}
					this.FindRecursive(treeNode2, ParentNodeText, out foundNode);
				}
			}
		}

		private void loadZone()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadZone_Acc();
				return;
			}
			this.trvDepartments.Nodes.Clear();
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("SELECT f_ZoneName,f_ZoneNO FROM t_b_Controller_Zone ORDER BY f_ZoneName ASC", sqlConnection))
				{
					sqlConnection.Open();
					SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.Default);
					while (sqlDataReader.Read())
					{
						TreeNode treeNode = new TreeNode();
						treeNode.Text = wgTools.SetObjToStr(sqlDataReader[0]);
						treeNode.Tag = wgTools.SetObjToStr(sqlDataReader[0]);
						if (treeNode.Text.LastIndexOf("\\") > 0)
						{
							string parentNodeText = treeNode.Text.Substring(0, treeNode.Text.LastIndexOf("\\"));
							treeNode.Text = treeNode.Text.Substring(treeNode.Text.LastIndexOf("\\") + 1);
							using (IEnumerator enumerator = this.trvDepartments.Nodes.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									TreeNode treeNode2 = (TreeNode)enumerator.Current;
									TreeNode treeNode3;
									this.FindRecursive(treeNode2, parentNodeText, out treeNode3);
									if (treeNode3 != null)
									{
										treeNode3.Nodes.Add(treeNode);
									}
								}
								continue;
							}
						}
						this.trvDepartments.Nodes.Add(treeNode);
					}
					sqlDataReader.Close();
					this.trvDepartments.ExpandAll();
				}
			}
		}

		private void loadZone_Acc()
		{
			this.trvDepartments.Nodes.Clear();
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("SELECT f_ZoneName,f_ZoneNO FROM t_b_Controller_Zone ORDER BY f_ZoneName ASC", oleDbConnection))
				{
					oleDbConnection.Open();
					OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader(CommandBehavior.Default);
					while (oleDbDataReader.Read())
					{
						TreeNode treeNode = new TreeNode();
						treeNode.Text = wgTools.SetObjToStr(oleDbDataReader[0]);
						treeNode.Tag = wgTools.SetObjToStr(oleDbDataReader[0]);
						if (treeNode.Text.LastIndexOf("\\") > 0)
						{
							string parentNodeText = treeNode.Text.Substring(0, treeNode.Text.LastIndexOf("\\"));
							treeNode.Text = treeNode.Text.Substring(treeNode.Text.LastIndexOf("\\") + 1);
							using (IEnumerator enumerator = this.trvDepartments.Nodes.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									TreeNode treeNode2 = (TreeNode)enumerator.Current;
									TreeNode treeNode3;
									this.FindRecursive(treeNode2, parentNodeText, out treeNode3);
									if (treeNode3 != null)
									{
										treeNode3.Nodes.Add(treeNode);
									}
								}
								continue;
							}
						}
						this.trvDepartments.Nodes.Add(treeNode);
					}
					oleDbDataReader.Close();
					this.trvDepartments.ExpandAll();
				}
			}
		}

		private void trvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (this.trvDepartments.SelectedNode != null)
			{
				this.txtSelectedDept.Text = this.trvDepartments.SelectedNode.Tag.ToString();
			}
		}

		private void frmDepartments_Load(object sender, EventArgs e)
		{
			this.txtSelectedDept_TextChanged(null, null);
			this.loadOperatorPrivilege();
			this.loadZone();
			this.txtSelectedDept.Text = "";
			this.trvDepartments.SelectedNode = null;
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuZones";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnAddSuper.Visible = false;
				this.btnDeleteDept.Visible = false;
				this.btnEditDept.Visible = false;
				this.toolStrip1.Visible = false;
			}
		}

		private void txtSelectedDept_TextChanged(object sender, EventArgs e)
		{
			if (this.txtSelectedDept.Text.Length > 0)
			{
				this.btnDeleteDept.Enabled = true;
				this.btnAdd.Enabled = true;
				this.btnEditDept.Enabled = true;
				return;
			}
			this.btnDeleteDept.Enabled = false;
			this.btnAdd.Enabled = false;
			this.btnEditDept.Enabled = false;
		}

		private void btnAddSuper_Click(object sender, EventArgs e)
		{
			this.trvDepartments.SelectedNode = null;
			this.btnAdd_Click(sender, e);
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			string text;
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = (sender as ToolStripButton).Text;
				dfrmInputNewName.label1.Text = CommonStr.strZone;
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				text = dfrmInputNewName.strNewName;
			}
			if (string.IsNullOrEmpty(text))
			{
				XMessageBox.Show(this, CommonStr.strNameNotEmpty, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				if (text.Trim() == "")
				{
					XMessageBox.Show(this, CommonStr.strNameNotEmpty, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (text.LastIndexOf("\\") >= 0)
				{
					XMessageBox.Show(this, CommonStr.strNotIncludeBackSlash, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				text = text.Trim();
				string text2 = text;
				if (sender == this.btnAddSuper)
				{
					this.trvDepartments.SelectedNode = null;
				}
				if (this.trvDepartments.SelectedNode == null)
				{
					using (IEnumerator enumerator = this.trvDepartments.Nodes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TreeNode treeNode = (TreeNode)enumerator.Current;
							if (treeNode.Tag.ToString() == text2)
							{
								XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								return;
							}
						}
						goto IL_1DA;
					}
				}
				text2 = this.trvDepartments.SelectedNode.Tag + "\\" + text2;
				foreach (TreeNode treeNode2 in this.trvDepartments.SelectedNode.Nodes)
				{
					if (treeNode2.Tag.ToString() == text2)
					{
						XMessageBox.Show(this, CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
				}
				IL_1DA:
				icControllerZone icControllerZone = new icControllerZone();
				if (icControllerZone.checkExisted(text2))
				{
					XMessageBox.Show(this, text2 + "\r\n\r\n" + CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				icControllerZone.addNew(text2);
				TreeNode treeNode3 = new TreeNode();
				treeNode3.Text = text;
				treeNode3.Tag = text2;
				if (this.trvDepartments.SelectedNode == null)
				{
					this.trvDepartments.Nodes.Add(treeNode3);
					this.trvDepartments.ExpandAll();
					return;
				}
				this.trvDepartments.SelectedNode.Nodes.Add(treeNode3);
				this.trvDepartments.SelectedNode.Expand();
				return;
			}
		}

		private void btnEditDept_Click(object sender, EventArgs e)
		{
			string strNewName;
			using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
			{
				dfrmInputNewName.Text = (sender as ToolStripButton).Text;
				dfrmInputNewName.label1.Text = CommonStr.strZone;
				if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				strNewName = dfrmInputNewName.strNewName;
			}
			if (string.IsNullOrEmpty(strNewName))
			{
				XMessageBox.Show(this, CommonStr.strNameNotEmpty, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				if (strNewName.Trim() == "")
				{
					XMessageBox.Show(this, CommonStr.strNameNotEmpty, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (strNewName.LastIndexOf("\\") >= 0)
				{
					XMessageBox.Show(this, CommonStr.strNotIncludeBackSlash, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				icControllerZone icControllerZone = new icControllerZone();
				string text;
				if (this.txtSelectedDept.Text.LastIndexOf("\\") < 0)
				{
					text = strNewName;
				}
				else
				{
					text = this.txtSelectedDept.Text.Substring(0, this.txtSelectedDept.Text.LastIndexOf("\\")) + "\\" + strNewName;
				}
				if (icControllerZone.checkExisted(text))
				{
					XMessageBox.Show(this, text + "\r\n\r\n" + CommonStr.strNameDuplicated, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				icControllerZone.Update(this.txtSelectedDept.Text, text);
				if (this.trvDepartments.SelectedNode != null)
				{
					this.trvDepartments.SelectedNode.Text = strNewName;
					this.trvDepartments.SelectedNode.Tag = text;
					this.txtSelectedDept.Text = text;
				}
				else
				{
					this.txtSelectedDept.Text = "";
				}
				this.loadZone();
				return;
			}
		}

		private void btnDeleteDept_Click(object sender, EventArgs e)
		{
			if (XMessageBox.Show(this.btnDeleteDept.Text + "\r\n\r\n" + this.txtSelectedDept.Text + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel) == DialogResult.OK)
			{
				icControllerZone icControllerZone = new icControllerZone();
				icControllerZone.delete(this.txtSelectedDept.Text);
				this.trvDepartments.Nodes.Remove(this.trvDepartments.SelectedNode);
				this.txtSelectedDept.Text = "";
				this.trvDepartments.SelectedNode = null;
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmZones));
			this.toolStrip1 = new ToolStrip();
			this.btnAddSuper = new ToolStripButton();
			this.toolStripSplitButton1 = new ToolStripSeparator();
			this.btnAdd = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.btnEditDept = new ToolStripButton();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.btnDeleteDept = new ToolStripButton();
			this.toolStrip2 = new ToolStrip();
			this.toolStripLabel1 = new ToolStripLabel();
			this.txtSelectedDept = new ToolStripTextBox();
			this.trvDepartments = new TreeView();
			this.toolStrip1.SuspendLayout();
			this.toolStrip2.SuspendLayout();
			base.SuspendLayout();
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnAddSuper,
				this.toolStripSplitButton1,
				this.btnAdd,
				this.toolStripSeparator1,
				this.btnEditDept,
				this.toolStripSeparator2,
				this.btnDeleteDept
			});
			this.toolStrip1.Name = "toolStrip1";
			componentResourceManager.ApplyResources(this.btnAddSuper, "btnAddSuper");
			this.btnAddSuper.ForeColor = Color.White;
			this.btnAddSuper.Image = Resources.pTools_Add_Top;
			this.btnAddSuper.Name = "btnAddSuper";
			this.btnAddSuper.Click += new EventHandler(this.btnAddSuper_Click);
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			componentResourceManager.ApplyResources(this.toolStripSplitButton1, "toolStripSplitButton1");
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Image = Resources.pTools_Add_Child;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			componentResourceManager.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			componentResourceManager.ApplyResources(this.btnEditDept, "btnEditDept");
			this.btnEditDept.ForeColor = Color.White;
			this.btnEditDept.Image = Resources.pTools_Edit;
			this.btnEditDept.Name = "btnEditDept";
			this.btnEditDept.Click += new EventHandler(this.btnEditDept_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			componentResourceManager.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			componentResourceManager.ApplyResources(this.btnDeleteDept, "btnDeleteDept");
			this.btnDeleteDept.ForeColor = Color.White;
			this.btnDeleteDept.Image = Resources.pTools_Del;
			this.btnDeleteDept.Name = "btnDeleteDept";
			this.btnDeleteDept.Click += new EventHandler(this.btnDeleteDept_Click);
			this.toolStrip2.BackColor = Color.Transparent;
			this.toolStrip2.BackgroundImage = Resources.pTools_second_title;
			componentResourceManager.ApplyResources(this.toolStrip2, "toolStrip2");
			this.toolStrip2.Items.AddRange(new ToolStripItem[]
			{
				this.toolStripLabel1,
				this.txtSelectedDept
			});
			this.toolStrip2.Name = "toolStrip2";
			this.toolStripLabel1.ForeColor = Color.White;
			this.toolStripLabel1.Name = "toolStripLabel1";
			componentResourceManager.ApplyResources(this.toolStripLabel1, "toolStripLabel1");
			this.txtSelectedDept.BackColor = SystemColors.Control;
			this.txtSelectedDept.Name = "txtSelectedDept";
			this.txtSelectedDept.ReadOnly = true;
			componentResourceManager.ApplyResources(this.txtSelectedDept, "txtSelectedDept");
			this.txtSelectedDept.TextChanged += new EventHandler(this.txtSelectedDept_TextChanged);
			componentResourceManager.ApplyResources(this.trvDepartments, "trvDepartments");
			this.trvDepartments.Name = "trvDepartments";
			this.trvDepartments.AfterSelect += new TreeViewEventHandler(this.trvDepartments_AfterSelect);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.trvDepartments);
			base.Controls.Add(this.toolStrip2);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmZones";
			base.Load += new EventHandler(this.frmDepartments_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
