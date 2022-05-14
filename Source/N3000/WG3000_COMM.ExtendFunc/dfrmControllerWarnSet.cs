using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmControllerWarnSet : frmN3000
	{
		private IContainer components;

		private Button btnCancel;

		private Button btnOK;

		private DataGridView dataGridView1;

		private Button btnExtension;

		private Button btnChangeThreatPassword;

		private Label lblThreatPassword;

		private Label lblOpenDoorTimeout;

		private NumericUpDown nudOpenDoorTimeout;

		private DataGridViewTextBoxColumn f_ControllerID;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewCheckBoxColumn f_InterLock123;

		private DataGridViewCheckBoxColumn f_InterLock34;

		private DataGridViewCheckBoxColumn f_ForcedOpen;

		private DataGridViewCheckBoxColumn f_InterLock1234;

		private DataGridViewTextBoxColumn f_Doors;

		internal CheckBox chkActiveFireSignalShare;

		internal CheckBox chkGrouped;

		private dfrmFind dfrmFind1 = new dfrmFind();

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dfrmFind1 != null)
			{
				this.dfrmFind1.Dispose();
			}
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerWarnSet));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.dataGridView1 = new DataGridView();
			this.f_ControllerID = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_InterLock123 = new DataGridViewCheckBoxColumn();
			this.f_InterLock34 = new DataGridViewCheckBoxColumn();
			this.f_ForcedOpen = new DataGridViewCheckBoxColumn();
			this.f_InterLock1234 = new DataGridViewCheckBoxColumn();
			this.f_Doors = new DataGridViewTextBoxColumn();
			this.btnExtension = new Button();
			this.btnChangeThreatPassword = new Button();
			this.lblThreatPassword = new Label();
			this.lblOpenDoorTimeout = new Label();
			this.nudOpenDoorTimeout = new NumericUpDown();
			this.chkActiveFireSignalShare = new CheckBox();
			this.chkGrouped = new CheckBox();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			((ISupportInitialize)this.nudOpenDoorTimeout).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_ControllerID,
				this.f_ControllerSN,
				this.f_InterLock123,
				this.f_InterLock34,
				this.f_ForcedOpen,
				this.f_InterLock1234,
				this.f_Doors
			});
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.DoubleClick += new EventHandler(this.dataGridView1_DoubleClick);
			componentResourceManager.ApplyResources(this.f_ControllerID, "f_ControllerID");
			this.f_ControllerID.Name = "f_ControllerID";
			this.f_ControllerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_InterLock123, "f_InterLock123");
			this.f_InterLock123.Name = "f_InterLock123";
			componentResourceManager.ApplyResources(this.f_InterLock34, "f_InterLock34");
			this.f_InterLock34.Name = "f_InterLock34";
			componentResourceManager.ApplyResources(this.f_ForcedOpen, "f_ForcedOpen");
			this.f_ForcedOpen.Name = "f_ForcedOpen";
			this.f_ForcedOpen.Resizable = DataGridViewTriState.True;
			this.f_ForcedOpen.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.f_InterLock1234, "f_InterLock1234");
			this.f_InterLock1234.Name = "f_InterLock1234";
			this.f_Doors.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_Doors, "f_Doors");
			this.f_Doors.Name = "f_Doors";
			this.f_Doors.ReadOnly = true;
			componentResourceManager.ApplyResources(this.btnExtension, "btnExtension");
			this.btnExtension.BackColor = Color.Transparent;
			this.btnExtension.BackgroundImage = Resources.pMain_button_normal;
			this.btnExtension.ForeColor = Color.White;
			this.btnExtension.Name = "btnExtension";
			this.btnExtension.UseVisualStyleBackColor = false;
			this.btnExtension.Click += new EventHandler(this.btnExtension_Click);
			componentResourceManager.ApplyResources(this.btnChangeThreatPassword, "btnChangeThreatPassword");
			this.btnChangeThreatPassword.BackColor = Color.Transparent;
			this.btnChangeThreatPassword.BackgroundImage = Resources.pMain_button_normal;
			this.btnChangeThreatPassword.ForeColor = Color.White;
			this.btnChangeThreatPassword.Name = "btnChangeThreatPassword";
			this.btnChangeThreatPassword.UseVisualStyleBackColor = false;
			this.btnChangeThreatPassword.Click += new EventHandler(this.btnChangeThreatPassword_Click);
			componentResourceManager.ApplyResources(this.lblThreatPassword, "lblThreatPassword");
			this.lblThreatPassword.BackColor = Color.Transparent;
			this.lblThreatPassword.ForeColor = Color.White;
			this.lblThreatPassword.Name = "lblThreatPassword";
			componentResourceManager.ApplyResources(this.lblOpenDoorTimeout, "lblOpenDoorTimeout");
			this.lblOpenDoorTimeout.BackColor = Color.Transparent;
			this.lblOpenDoorTimeout.ForeColor = Color.White;
			this.lblOpenDoorTimeout.Name = "lblOpenDoorTimeout";
			componentResourceManager.ApplyResources(this.nudOpenDoorTimeout, "nudOpenDoorTimeout");
			this.nudOpenDoorTimeout.BackColor = Color.White;
			NumericUpDown arg_5F8_0 = this.nudOpenDoorTimeout;
			int[] array = new int[4];
			array[0] = 650;
			arg_5F8_0.Maximum = new decimal(array);
			NumericUpDown arg_617_0 = this.nudOpenDoorTimeout;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_617_0.Minimum = new decimal(array2);
			this.nudOpenDoorTimeout.Name = "nudOpenDoorTimeout";
			this.nudOpenDoorTimeout.ReadOnly = true;
			NumericUpDown arg_653_0 = this.nudOpenDoorTimeout;
			int[] array3 = new int[4];
			array3[0] = 25;
			arg_653_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this.chkActiveFireSignalShare, "chkActiveFireSignalShare");
			this.chkActiveFireSignalShare.BackColor = Color.Transparent;
			this.chkActiveFireSignalShare.ForeColor = Color.White;
			this.chkActiveFireSignalShare.Name = "chkActiveFireSignalShare";
			this.chkActiveFireSignalShare.UseVisualStyleBackColor = false;
			this.chkActiveFireSignalShare.CheckedChanged += new EventHandler(this.chkActiveFireSignalShare_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkGrouped, "chkGrouped");
			this.chkGrouped.BackColor = Color.Transparent;
			this.chkGrouped.ForeColor = Color.White;
			this.chkGrouped.Name = "chkGrouped";
			this.chkGrouped.UseVisualStyleBackColor = false;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.chkGrouped);
			base.Controls.Add(this.chkActiveFireSignalShare);
			base.Controls.Add(this.nudOpenDoorTimeout);
			base.Controls.Add(this.lblOpenDoorTimeout);
			base.Controls.Add(this.lblThreatPassword);
			base.Controls.Add(this.btnChangeThreatPassword);
			base.Controls.Add(this.btnExtension);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.dataGridView1);
			base.Name = "dfrmControllerWarnSet";
			base.FormClosing += new FormClosingEventHandler(this.dfrmControllerWarnSet_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerInterLock_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControllerWarnSet_KeyDown);
			((ISupportInitialize)this.dataGridView1).EndInit();
			((ISupportInitialize)this.nudOpenDoorTimeout).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		public dfrmControllerWarnSet()
		{
			this.InitializeComponent();
		}

		private void loadData()
		{
			string text = " SELECT ";
			text += " f_ControllerNO ";
			text += ", f_ControllerSN ";
			text += ", f_ForceWarn ";
			text += ", f_DoorOpenTooLong ";
			text += ", f_DoorInvalidOpen ";
			text += ", f_InvalidCardWarn ";
			text += ", f_DoorNames ";
			text += ", f_ZoneID ";
			text += " from t_b_Controller  ";
			text += " ORDER BY f_ControllerNO ";
			wgAppConfig.fillDGVData(ref this.dataGridView1, text);
			DataTable table = ((DataView)this.dataGridView1.DataSource).Table;
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref table);
		}

		private void dfrmControllerInterLock_Load(object sender, EventArgs e)
		{
			this.lblThreatPassword.Text = "889988";
			this.loadData();
			this.lblThreatPassword.Text = wgAppConfig.getSystemParamByNO(24);
			this.chkActiveFireSignalShare.Checked = wgAppConfig.getParamValBoolByNO(60);
			this.chkActiveFireSignalShare.Visible = this.chkActiveFireSignalShare.Checked;
			if (this.chkActiveFireSignalShare.Visible && wgAppConfig.getSystemParamByNO(60) == "2")
			{
				this.chkGrouped.Checked = true;
				this.chkGrouped.Visible = true;
			}
			this.nudOpenDoorTimeout.Value = decimal.Parse(wgAppConfig.getSystemParamByNO(40));
			this.loadOperatorPrivilege();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuExtendedFunction";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnExtension.Visible = false;
				this.btnChangeThreatPassword.Enabled = false;
				this.btnOK.Visible = false;
				this.nudOpenDoorTimeout.ReadOnly = true;
				this.nudOpenDoorTimeout.Enabled = false;
				this.dataGridView1.ReadOnly = true;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.btnOK.Enabled = false;
			wgAppConfig.setSystemParamValue(60, this.chkActiveFireSignalShare.Checked ? (this.chkGrouped.Checked ? "2" : "1") : "0");
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			DataTable table = (this.dataGridView1.DataSource as DataView).Table;
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
				{
					sqlConnection.Open();
					for (int i = 0; i <= table.Rows.Count - 1; i++)
					{
						string text = " UPDATE t_b_Controller SET ";
						text = text + " f_DoorInvalidOpen = 0" + table.Rows[i]["f_DoorInvalidOpen"].ToString();
						text = text + ", f_DoorOpenTooLong = 0" + table.Rows[i]["f_DoorOpenTooLong"].ToString();
						text = text + ", f_ForceWarn  = 0" + table.Rows[i]["f_ForceWarn"].ToString();
						text = text + ", f_InvalidCardWarn = 0" + table.Rows[i]["f_InvalidCardWarn"].ToString();
						text = text + " WHERE f_ControllerNo = " + table.Rows[i]["f_ControllerNo"].ToString();
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
			wgAppConfig.setSystemParamValue(40, "", this.nudOpenDoorTimeout.Value.ToString(), "");
			base.Close();
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			DataTable table = (this.dataGridView1.DataSource as DataView).Table;
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					oleDbConnection.Open();
					for (int i = 0; i <= table.Rows.Count - 1; i++)
					{
						string text = " UPDATE t_b_Controller SET ";
						text = text + " f_DoorInvalidOpen = 0" + table.Rows[i]["f_DoorInvalidOpen"].ToString();
						text = text + ", f_DoorOpenTooLong = 0" + table.Rows[i]["f_DoorOpenTooLong"].ToString();
						text = text + ", f_ForceWarn  = 0" + table.Rows[i]["f_ForceWarn"].ToString();
						text = text + ", f_InvalidCardWarn = 0" + table.Rows[i]["f_InvalidCardWarn"].ToString();
						text = text + " WHERE f_ControllerNo = " + table.Rows[i]["f_ControllerNo"].ToString();
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
			}
			wgAppConfig.setSystemParamValue(40, "", this.nudOpenDoorTimeout.Value.ToString(), "");
			base.Close();
		}

		private void dataGridView1_DoubleClick(object sender, EventArgs e)
		{
			this.btnExtension.PerformClick();
		}

		private void btnExtension_Click(object sender, EventArgs e)
		{
			if (this.dataGridView1.SelectedRows.Count <= 0)
			{
				if (this.dataGridView1.SelectedCells.Count <= 0)
				{
					return;
				}
				int arg_3D_0 = this.dataGridView1.SelectedCells[0].RowIndex;
			}
			else
			{
				int arg_56_0 = this.dataGridView1.SelectedRows[0].Index;
			}
			int index = 0;
			DataGridView dataGridView = this.dataGridView1;
			if (dataGridView.Rows.Count > 0)
			{
				index = dataGridView.CurrentCell.RowIndex;
			}
			using (dfrmPeripheralControlBoard dfrmPeripheralControlBoard = new dfrmPeripheralControlBoard())
			{
				dfrmPeripheralControlBoard.ControllerNO = int.Parse(dataGridView.Rows[index].Cells[0].Value.ToString());
				dfrmPeripheralControlBoard.ControllerSN = int.Parse(dataGridView.Rows[index].Cells[1].Value.ToString());
				if (dfrmPeripheralControlBoard.ShowDialog(this) == DialogResult.OK)
				{
					this.loadData();
				}
			}
		}

		private void btnChangeThreatPassword_Click(object sender, EventArgs e)
		{
			using (dfrmSetPassword dfrmSetPassword = new dfrmSetPassword())
			{
				dfrmSetPassword.operatorID = 0;
				dfrmSetPassword.Text = this.btnChangeThreatPassword.Text;
				if (dfrmSetPassword.ShowDialog(this) == DialogResult.OK)
				{
					if (int.Parse(dfrmSetPassword.newPassword) >= 999999 || int.Parse(dfrmSetPassword.newPassword) <= 0)
					{
						XMessageBox.Show(this, CommonStr.strFailedNumeric999999, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else if (wgAppConfig.setSystemParamValue(24, "Threat Password", dfrmSetPassword.newPassword, "") == 1)
					{
						this.lblThreatPassword.Text = dfrmSetPassword.newPassword;
						XMessageBox.Show(this, "OK", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						XMessageBox.Show(this, CommonStr.strFailed, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
		}

		private void dfrmControllerWarnSet_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Control && e.KeyValue == 81 && e.Shift)
				{
					if (this.chkActiveFireSignalShare.Visible)
					{
						this.chkGrouped.Visible = true;
					}
					this.chkActiveFireSignalShare.Visible = true;
					this.chkActiveFireSignalShare_CheckedChanged(null, null);
				}
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
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmControllerWarnSet_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void chkActiveFireSignalShare_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkActiveFireSignalShare.Checked)
			{
				this.chkGrouped.Enabled = true;
				return;
			}
			this.chkGrouped.Enabled = false;
			this.chkGrouped.Checked = false;
		}
	}
}
