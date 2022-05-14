using Microsoft.VisualBasic;
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
	public class frmControllers : frmN3000
	{
		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton btnAdd;

		private ToolStripButton btnEdit;

		private ToolStripButton btnDelete;

		private DataGridView dgvControllers;

		private ToolStripButton btnPrint;

		private ToolStripButton btnSearchController;

		private ToolStripButton btnExportToExcel;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem batchUpdateSelectToolStripMenuItem;

		private DataGridViewTextBoxColumn f_ControllerID;

		private DataGridViewTextBoxColumn f_ControllerNO;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewCheckBoxColumn f_Enabled;

		private DataGridViewTextBoxColumn f_IP;

		private DataGridViewTextBoxColumn f_PORT;

		private DataGridViewTextBoxColumn f_ZoneName;

		private DataGridViewTextBoxColumn f_Note;

		private DataGridViewTextBoxColumn f_DoorNames;

		private ToolStripComboBox cboZone;

		private ArrayList arrZoneName = new ArrayList();

		private ArrayList arrZoneID = new ArrayList();

		private ArrayList arrZoneNO = new ArrayList();

		private DataTable dtController;

		private DataView dv;

		private dfrmFind dfrmFind1;

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dtController != null)
				{
					this.dtController.Dispose();
				}
				if (this.dv != null)
				{
					this.dv.Dispose();
				}
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(frmControllers));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.batchUpdateSelectToolStripMenuItem = new ToolStripMenuItem();
			this.dgvControllers = new DataGridView();
			this.f_ControllerID = new DataGridViewTextBoxColumn();
			this.f_ControllerNO = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_Enabled = new DataGridViewCheckBoxColumn();
			this.f_IP = new DataGridViewTextBoxColumn();
			this.f_PORT = new DataGridViewTextBoxColumn();
			this.f_ZoneName = new DataGridViewTextBoxColumn();
			this.f_Note = new DataGridViewTextBoxColumn();
			this.f_DoorNames = new DataGridViewTextBoxColumn();
			this.toolStrip1 = new ToolStrip();
			this.btnSearchController = new ToolStripButton();
			this.btnAdd = new ToolStripButton();
			this.btnEdit = new ToolStripButton();
			this.btnDelete = new ToolStripButton();
			this.btnPrint = new ToolStripButton();
			this.btnExportToExcel = new ToolStripButton();
			this.cboZone = new ToolStripComboBox();
			this.contextMenuStrip1.SuspendLayout();
			((ISupportInitialize)this.dgvControllers).BeginInit();
			this.toolStrip1.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.batchUpdateSelectToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			componentResourceManager.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
			this.batchUpdateSelectToolStripMenuItem.Name = "batchUpdateSelectToolStripMenuItem";
			componentResourceManager.ApplyResources(this.batchUpdateSelectToolStripMenuItem, "batchUpdateSelectToolStripMenuItem");
			this.batchUpdateSelectToolStripMenuItem.Click += new EventHandler(this.batchUpdateSelectToolStripMenuItem_Click);
			this.dgvControllers.AllowUserToAddRows = false;
			this.dgvControllers.AllowUserToDeleteRows = false;
			this.dgvControllers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvControllers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvControllers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvControllers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ControllerID,
				this.f_ControllerNO,
				this.f_ControllerSN,
				this.f_Enabled,
				this.f_IP,
				this.f_PORT,
				this.f_ZoneName,
				this.f_Note,
				this.f_DoorNames
			});
			componentResourceManager.ApplyResources(this.dgvControllers, "dgvControllers");
			this.dgvControllers.EnableHeadersVisualStyles = false;
			this.dgvControllers.Name = "dgvControllers";
			this.dgvControllers.ReadOnly = true;
			this.dgvControllers.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(146, 150, 177);
			dataGridViewCellStyle2.Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			this.dgvControllers.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
			this.dgvControllers.RowTemplate.Height = 23;
			this.dgvControllers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.dgvControllers.MouseDoubleClick += new MouseEventHandler(this.dgvControllers_MouseDoubleClick);
			componentResourceManager.ApplyResources(this.f_ControllerID, "f_ControllerID");
			this.f_ControllerID.Name = "f_ControllerID";
			this.f_ControllerID.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ControllerNO.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.f_ControllerNO, "f_ControllerNO");
			this.f_ControllerNO.Name = "f_ControllerNO";
			this.f_ControllerNO.ReadOnly = true;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_ControllerSN.DefaultCellStyle = dataGridViewCellStyle4;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.ForeColor = Color.Black;
			dataGridViewCellStyle5.NullValue = false;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
			this.f_Enabled.DefaultCellStyle = dataGridViewCellStyle5;
			componentResourceManager.ApplyResources(this.f_Enabled, "f_Enabled");
			this.f_Enabled.Name = "f_Enabled";
			this.f_Enabled.ReadOnly = true;
			this.f_Enabled.Resizable = DataGridViewTriState.True;
			this.f_Enabled.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.f_IP, "f_IP");
			this.f_IP.Name = "f_IP";
			this.f_IP.ReadOnly = true;
			dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.f_PORT.DefaultCellStyle = dataGridViewCellStyle6;
			componentResourceManager.ApplyResources(this.f_PORT, "f_PORT");
			this.f_PORT.Name = "f_PORT";
			this.f_PORT.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ZoneName, "f_ZoneName");
			this.f_ZoneName.Name = "f_ZoneName";
			this.f_ZoneName.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_Note, "f_Note");
			this.f_Note.Name = "f_Note";
			this.f_Note.ReadOnly = true;
			this.f_DoorNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_DoorNames, "f_DoorNames");
			this.f_DoorNames.Name = "f_DoorNames";
			this.f_DoorNames.ReadOnly = true;
			this.toolStrip1.BackColor = Color.Transparent;
			this.toolStrip1.BackgroundImage = Resources.pChild_title;
			componentResourceManager.ApplyResources(this.toolStrip1, "toolStrip1");
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.btnSearchController,
				this.btnAdd,
				this.btnEdit,
				this.btnDelete,
				this.btnPrint,
				this.btnExportToExcel,
				this.cboZone
			});
			this.toolStrip1.Name = "toolStrip1";
			this.btnSearchController.ForeColor = Color.White;
			this.btnSearchController.Image = Resources.pTools_SearchNet;
			componentResourceManager.ApplyResources(this.btnSearchController, "btnSearchController");
			this.btnSearchController.Name = "btnSearchController";
			this.btnSearchController.Click += new EventHandler(this.btnSearchController_Click);
			this.btnAdd.ForeColor = Color.White;
			this.btnAdd.Image = Resources.pTools_Add;
			componentResourceManager.ApplyResources(this.btnAdd, "btnAdd");
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			this.btnEdit.ForeColor = Color.White;
			this.btnEdit.Image = Resources.pTools_Edit;
			componentResourceManager.ApplyResources(this.btnEdit, "btnEdit");
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
			this.btnDelete.ForeColor = Color.White;
			this.btnDelete.Image = Resources.pTools_Del;
			componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.btnPrint.ForeColor = Color.White;
			this.btnPrint.Image = Resources.pTools_Print;
			componentResourceManager.ApplyResources(this.btnPrint, "btnPrint");
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Click += new EventHandler(this.btnPrint_Click);
			this.btnExportToExcel.ForeColor = Color.White;
			this.btnExportToExcel.Image = Resources.pTools_ExportToExcel;
			componentResourceManager.ApplyResources(this.btnExportToExcel, "btnExportToExcel");
			this.btnExportToExcel.Name = "btnExportToExcel";
			this.btnExportToExcel.Click += new EventHandler(this.btnExportToExcel_Click);
			this.cboZone.DropDownStyle = ComboBoxStyle.DropDownList;
			componentResourceManager.ApplyResources(this.cboZone, "cboZone");
			this.cboZone.Name = "cboZone";
			this.cboZone.SelectedIndexChanged += new EventHandler(this.cboZone_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackgroundImage = Resources.pMain_content_bkg;
			base.Controls.Add(this.dgvControllers);
			base.Controls.Add(this.toolStrip1);
			base.Name = "frmControllers";
			base.FormClosing += new FormClosingEventHandler(this.frmControllers_FormClosing);
			base.Load += new EventHandler(this.frmControllers_Load);
			base.KeyDown += new KeyEventHandler(this.frmControllers_KeyDown);
			this.contextMenuStrip1.ResumeLayout(false);
			((ISupportInitialize)this.dgvControllers).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public frmControllers()
		{
			this.InitializeComponent();
		}

		private void loadZoneInfo()
		{
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getZone(ref this.arrZoneName, ref this.arrZoneID, ref this.arrZoneNO);
			int i = this.arrZoneID.Count;
			this.cboZone.Items.Clear();
			for (i = 0; i < this.arrZoneID.Count; i++)
			{
				if (i == 0 && string.IsNullOrEmpty(this.arrZoneName[i].ToString()))
				{
					this.cboZone.Items.Add(CommonStr.strAllZones);
				}
				else
				{
					this.cboZone.Items.Add(this.arrZoneName[i].ToString());
				}
			}
			if (this.cboZone.Items.Count > 0)
			{
				this.cboZone.SelectedIndex = 0;
			}
			bool visible = true;
			this.cboZone.Visible = visible;
		}

		private void frmControllers_Load(object sender, EventArgs e)
		{
			this.loadZoneInfo();
			this.loadOperatorPrivilege();
			this.loadControllerData();
			this.dgvControllers.ContextMenuStrip = this.contextMenuStrip1;
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuControllers";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnAdd.Visible = false;
				this.btnEdit.Visible = false;
				this.btnDelete.Visible = false;
				this.btnSearchController.Visible = false;
			}
		}

		private void loadControllerData()
		{
			this.dtController = new DataTable();
			this.dv = new DataView(this.dtController);
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT f_ControllerID, f_ControllerNO, f_ControllerSN, f_Enabled, f_IP, f_PORT, f_ZoneName, f_Note, f_DoorNames,  t_b_Controller.f_ZoneID ";
				text += " FROM t_b_Controller LEFT OUTER JOIN t_b_Controller_Zone ON t_b_Controller.f_ZoneID = t_b_Controller_Zone.f_ZoneID";
				text += "  ORDER BY [f_ControllerNO]";
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtController);
						}
					}
					goto IL_101;
				}
			}
			text = " SELECT f_ControllerID, f_ControllerNO, f_ControllerSN, f_Enabled, f_IP, f_PORT, f_ZoneName, f_Note, f_DoorNames,  t_b_Controller.f_ZoneID ";
			text += " FROM t_b_Controller LEFT OUTER JOIN t_b_Controller_Zone ON t_b_Controller.f_ZoneID = t_b_Controller_Zone.f_ZoneID";
			text += "  ORDER BY f_ControllerNO ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtController);
					}
				}
			}
			IL_101:
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtController);
			this.dgvControllers.AutoGenerateColumns = false;
			this.dgvControllers.DataSource = this.dv;
			for (int i = 0; i < this.dv.Table.Columns.Count - 1; i++)
			{
				this.dgvControllers.Columns[i].DataPropertyName = this.dv.Table.Columns[i].ColumnName;
			}
			bool visible = true;
			this.dgvControllers.Columns[6].Visible = visible;
			if (this.dv.Count > 0)
			{
				this.btnAdd.Enabled = true;
				this.btnEdit.Enabled = true;
				this.btnDelete.Enabled = true;
				this.btnPrint.Enabled = true;
			}
			else
			{
				this.btnAdd.Enabled = true;
				this.btnEdit.Enabled = false;
				this.btnDelete.Enabled = false;
				this.btnPrint.Enabled = false;
			}
			wgAppRunInfo.raiseAppRunInfoLoadNums(this.dv.Count.ToString());
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			using (dfrmController dfrmController = new dfrmController())
			{
				int count = this.dv.Table.Rows.Count;
				int count2 = this.dv.Count;
				dfrmController.ShowDialog(this);
				this.loadControllerData();
				this.cboZone_SelectedIndexChanged(null, null);
				if (count != this.dv.Table.Rows.Count && count2 + 1 != this.dv.Count)
				{
					this.loadZoneInfo();
				}
			}
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (this.dgvControllers.Rows.Count <= 0)
			{
				return;
			}
			int num = 0;
			if (this.dgvControllers.Rows.Count > 0)
			{
				num = this.dgvControllers.CurrentCell.RowIndex;
			}
			int rowCount = this.dgvControllers.RowCount;
			using (dfrmController dfrmController = new dfrmController())
			{
				dfrmController.OperateNew = false;
				dfrmController.ControllerID = int.Parse(this.dgvControllers.Rows[num].Cells[0].Value.ToString());
				dfrmController.ShowDialog(this);
				this.loadControllerData();
				if (dfrmController.bEditZone)
				{
					this.loadZoneInfo();
				}
				this.cboZone_SelectedIndexChanged(null, null);
			}
			if (this.dgvControllers.RowCount == 0 || rowCount != this.dgvControllers.RowCount)
			{
				this.loadZoneInfo();
				return;
			}
			if (this.dgvControllers.RowCount > 0)
			{
				if (this.dgvControllers.RowCount > num)
				{
					this.dgvControllers.CurrentCell = this.dgvControllers[1, num];
					return;
				}
				this.dgvControllers.CurrentCell = this.dgvControllers[1, this.dgvControllers.RowCount - 1];
			}
		}

		private void dgvControllers_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.btnEdit.Enabled)
			{
				this.btnEdit.PerformClick();
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.dgvControllers.SelectedRows.Count <= 1)
			{
				int index = this.dgvControllers.SelectedRows[0].Index;
				if (XMessageBox.Show(this, string.Concat(new string[]
				{
					CommonStr.strDelete,
					" ",
					this.dgvControllers[1, index].Value.ToString(),
					":",
					this.dgvControllers[2, index].Value.ToString(),
					"?"
				}), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
				{
					return;
				}
			}
			else if (XMessageBox.Show(this, CommonStr.strDeleteSelected + this.dgvControllers.SelectedRows.Count.ToString() + "?", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.Cancel)
			{
				return;
			}
			int arg_E5_0 = this.dgvControllers.FirstDisplayedScrollingRowIndex;
			string text = "";
			if (this.dgvControllers.SelectedRows.Count <= 1)
			{
				int index = this.dgvControllers.SelectedRows[0].Index;
				icController.DeleteControllerFromDB(int.Parse(this.dgvControllers[0, index].Value.ToString()));
				text = string.Concat(new string[]
				{
					text,
					"(",
					this.dgvControllers[1, index].Value.ToString(),
					")",
					this.dgvControllers[2, index].Value.ToString()
				});
			}
			else
			{
				foreach (DataGridViewRow dataGridViewRow in this.dgvControllers.SelectedRows)
				{
					text = string.Concat(new string[]
					{
						text,
						"(",
						dataGridViewRow.Cells[1].Value.ToString(),
						")",
						dataGridViewRow.Cells[2].Value.ToString(),
						","
					});
					icController.DeleteControllerFromDB(int.Parse(dataGridViewRow.Cells[0].Value.ToString()));
				}
			}
			wgAppConfig.wgLog(CommonStr.strDelete + CommonStr.strController + ":" + text);
			this.loadControllerData();
			this.cboZone_SelectedIndexChanged(null, null);
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			wgAppConfig.printdgv(this.dgvControllers, this.Text);
		}

		private void btnExportToExcel_Click(object sender, EventArgs e)
		{
			wgAppConfig.exportToExcel(this.dgvControllers, this.Text);
		}

		private void btnSearchController_Click(object sender, EventArgs e)
		{
			using (dfrmNetControllerConfig dfrmNetControllerConfig = new dfrmNetControllerConfig())
			{
				int count = this.dv.Table.Rows.Count;
				int arg_27_0 = this.dv.Count;
				dfrmNetControllerConfig.ShowDialog(this);
				this.loadControllerData();
				this.cboZone_SelectedIndexChanged(null, null);
				if (count != this.dv.Table.Rows.Count)
				{
					this.loadZoneInfo();
				}
			}
		}

		private void funcCtrlShiftQ()
		{
			try
			{
				string strNewName;
				using (dfrmInputNewName dfrmInputNewName = new dfrmInputNewName())
				{
					dfrmInputNewName.Text = CommonStr.strControllerBeginNo;
					dfrmInputNewName.label1.Text = CommonStr.strControllerSN;
					dfrmInputNewName.strNewName = "";
					if (dfrmInputNewName.ShowDialog(this) != DialogResult.OK)
					{
						return;
					}
					uint num;
					if (!uint.TryParse(dfrmInputNewName.strNewName, out num))
					{
						return;
					}
					strNewName = dfrmInputNewName.strNewName;
				}
				string text;
				using (dfrmInputNewName dfrmInputNewName2 = new dfrmInputNewName())
				{
					dfrmInputNewName2.Text = CommonStr.strControllerEndNo;
					dfrmInputNewName2.label1.Text = CommonStr.strControllerSN;
					dfrmInputNewName2.strNewName = "";
					if (dfrmInputNewName2.ShowDialog(this) == DialogResult.OK)
					{
						uint num;
						if (!uint.TryParse(dfrmInputNewName2.strNewName, out num))
						{
							text = strNewName;
						}
						else
						{
							text = dfrmInputNewName2.strNewName;
						}
					}
					else
					{
						text = strNewName;
					}
				}
				if (Information.IsNumeric(strNewName) && Information.IsNumeric(text))
				{
					if (int.Parse(strNewName) <= int.Parse(text) && wgMjController.GetControllerType(int.Parse(strNewName)) >= 0 && wgMjController.GetControllerType(int.Parse(text)) >= 0)
					{
						using (dfrmController dfrmController = new dfrmController())
						{
							dfrmController.Show();
							for (long num2 = (long)int.Parse(strNewName); num2 <= (long)int.Parse(text); num2 += 1L)
							{
								dfrmController.Text = num2.ToString();
								dfrmController.mtxtbControllerSN.Text = num2.ToString();
								dfrmController.mtxtbControllerNO.Text = ((long)((int)(num2 / 100000000L) * 10000) + num2 % 10000L).ToString();
								dfrmController.btnNext.PerformClick();
								dfrmController.btnOK_Click(null, null);
								Application.DoEvents();
							}
							goto IL_1D2;
						}
					}
					XMessageBox.Show(CommonStr.strSNWrong);
				}
				else
				{
					XMessageBox.Show(CommonStr.strSNWrong);
				}
				IL_1D2:;
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
			this.loadControllerData();
		}

		public void frmControllers_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.funcCtrlShiftQ();
			}
			try
			{
				if ((e.Control && e.KeyValue == 70) || e.KeyValue == 114)
				{
					if (this.dfrmFind1 == null)
					{
						this.dfrmFind1 = new dfrmFind();
					}
					this.dfrmFind1.setObjtoFind(base.ActiveControl, this);
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void frmControllers_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void batchUpdateSelectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.dgvControllers.SelectedRows.Count <= 0)
			{
				return;
			}
			using (dfrmControllerZoneSelect dfrmControllerZoneSelect = new dfrmControllerZoneSelect())
			{
				string text = "";
				for (int i = 0; i < this.dgvControllers.SelectedRows.Count; i++)
				{
					int index = this.dgvControllers.SelectedRows[i].Index;
					int num = int.Parse(this.dgvControllers.Rows[index].Cells[0].Value.ToString());
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += num.ToString();
				}
				dfrmControllerZoneSelect.Text = string.Format("{0}: [{1}]", sender.ToString(), this.dgvControllers.SelectedRows.Count.ToString());
				if (dfrmControllerZoneSelect.ShowDialog(this) == DialogResult.OK)
				{
					string strSql = string.Format(" UPDATE t_b_Controller SET f_ZoneID= {0} WHERE  f_ControllerID IN ({1}) ", dfrmControllerZoneSelect.selectZoneId, text);
					wgAppConfig.runUpdateSql(strSql);
					this.loadControllerData();
				}
			}
		}

		private void cboZone_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.dv != null)
				{
					if (this.cboZone.SelectedIndex < 0 || (this.cboZone.SelectedIndex == 0 && (int)this.arrZoneID[0] == 0))
					{
						this.dv.RowFilter = "";
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dv.Count.ToString());
					}
					else
					{
						this.dv.RowFilter = "f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
						string arg = " f_ZoneID =" + this.arrZoneID[this.cboZone.SelectedIndex];
						int num = (int)this.arrZoneID[this.cboZone.SelectedIndex];
						int num2 = (int)this.arrZoneNO[this.cboZone.SelectedIndex];
						int zoneChildMaxNo = icControllerZone.getZoneChildMaxNo(this.cboZone.Text, this.arrZoneName, this.arrZoneNO);
						if (num2 > 0)
						{
							if (num2 >= zoneChildMaxNo)
							{
								this.dv.RowFilter = string.Format(" f_ZoneID ={0:d} ", num);
								arg = string.Format(" f_ZoneID ={0:d} ", num);
							}
							else
							{
								this.dv.RowFilter = "";
								string text = "";
								for (int i = 0; i < this.arrZoneNO.Count; i++)
								{
									if ((int)this.arrZoneNO[i] <= zoneChildMaxNo && (int)this.arrZoneNO[i] >= num2)
									{
										if (text == "")
										{
											text += string.Format(" f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
										}
										else
										{
											text += string.Format(" OR f_ZoneID ={0:d} ", (int)this.arrZoneID[i]);
										}
									}
								}
								this.dv.RowFilter = string.Format("  {0} ", text);
								arg = string.Format("  {0} ", text);
							}
						}
						this.dv.RowFilter = string.Format(" {0} ", arg);
						wgTools.WriteLine("foreach (ListViewItem itm in listViewNotDisplay.Items)");
						wgAppRunInfo.raiseAppRunInfoLoadNums(this.dv.Count.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}
	}
}
