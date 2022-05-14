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

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmControllerInterLock : frmN3000
	{
		private dfrmFind dfrmFind1 = new dfrmFind();

		private IContainer components;

		private Button btnCancel;

		private Button btnOK;

		private DataGridView dataGridView1;

		private DataGridViewTextBoxColumn f_ControllerID;

		private DataGridViewTextBoxColumn f_ControllerSN;

		private DataGridViewCheckBoxColumn f_InterLock12;

		private DataGridViewCheckBoxColumn f_InterLock34;

		private DataGridViewCheckBoxColumn f_InterLock123;

		private DataGridViewCheckBoxColumn f_InterLock1234;

		private DataGridViewTextBoxColumn f_DoorNames;

		internal CheckBox chkActiveInterlockShare;

		internal CheckBox chkGrouped;

		public dfrmControllerInterLock()
		{
			this.InitializeComponent();
		}

		private void dfrmControllerInterLock_Load(object sender, EventArgs e)
		{
			this.chkActiveInterlockShare.Checked = wgAppConfig.getParamValBoolByNO(61);
			this.chkActiveInterlockShare.Visible = this.chkActiveInterlockShare.Checked;
			if (this.chkActiveInterlockShare.Visible)
			{
				this.dataGridView1.Location = new Point(8, 40);
				if (wgAppConfig.getSystemParamByNO(61) == "2")
				{
					this.chkGrouped.Checked = true;
					this.chkGrouped.Visible = true;
					this.dataGridView1.Location = new Point(8, 72);
				}
				this.dataGridView1.Size = new Size(this.dataGridView1.Size.Width, this.dataGridView1.Size.Height + 8 - this.dataGridView1.Location.Y);
			}
			string text;
			if (wgAppConfig.IsAccessDB)
			{
				text = " SELECT ";
				text += " f_ControllerNO ";
				text += ", f_ControllerSN ";
				text += ",  IIF ( [f_Interlock]=1 OR [f_Interlock]=3 , 1 , 0) AS f_InterLock12  ";
				text += ",  IIF ( [f_Interlock]=2 OR [f_Interlock]=3 , 1 , 0) AS f_InterLock34  ";
				text += ", IIF ( [f_Interlock]=4 , 1 , 0) AS f_InterLock123 ";
				text += ", IIF ( [f_Interlock]=8 , 1 , 0) AS f_InterLock1234 ";
				text += ", f_DoorNames ";
				text += ", t_b_Controller.f_ZoneID ";
				text += " from t_b_Controller  ";
				text += " WHERE f_ControllerSN > 199999999 ";
				text += " ORDER BY f_ControllerNO ";
			}
			else
			{
				text = " SELECT ";
				text += " f_ControllerNO ";
				text += ", f_ControllerSN ";
				text += ",  CASE WHEN [f_Interlock]=1 OR [f_Interlock]=3  THEN 1 ELSE 0 END AS f_InterLock12  ";
				text += ",  CASE WHEN [f_Interlock]=2 OR [f_Interlock]=3  THEN 1 ELSE 0 END AS f_InterLock34  ";
				text += ", CASE WHEN [f_Interlock]=4 THEN 1 ELSE 0 END AS f_InterLock123 ";
				text += ", CASE WHEN [f_Interlock]=8 THEN 1 ELSE 0 END AS f_InterLock1234 ";
				text += ", f_DoorNames ";
				text += ", t_b_Controller.f_ZoneID ";
				text += " from t_b_Controller  ";
				text += " WHERE f_ControllerSN > 199999999 ";
				text += " ORDER BY f_ControllerNO ";
			}
			wgAppConfig.fillDGVData(ref this.dataGridView1, text);
			DataTable table = ((DataView)this.dataGridView1.DataSource).Table;
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref table);
			for (int i = 0; i < this.dataGridView1.RowCount; i++)
			{
				DataGridViewRow dataGridViewRow = this.dataGridView1.Rows[i];
				int controllerType = wgMjController.GetControllerType(int.Parse(dataGridViewRow.Cells[1].Value.ToString()));
				if (controllerType == 2)
				{
					dataGridViewRow.Cells[3].ReadOnly = true;
					dataGridViewRow.Cells[4].ReadOnly = true;
					dataGridViewRow.Cells[5].ReadOnly = true;
					dataGridViewRow.Cells[3].Style.BackColor = SystemPens.InactiveBorder.Color;
					dataGridViewRow.Cells[4].Style.BackColor = SystemPens.InactiveBorder.Color;
					dataGridViewRow.Cells[5].Style.BackColor = SystemPens.InactiveBorder.Color;
				}
			}
			this.chkActiveInterlockShare_CheckedChanged(null, null);
			this.loadOperatorPrivilege();
		}

		private void loadOperatorPrivilege()
		{
			bool flag = false;
			string funName = "mnuInterLock";
			if (icOperator.OperatePrivilegeVisible(funName, ref flag) && flag)
			{
				this.btnOK.Visible = false;
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
			wgAppConfig.setSystemParamValue(61, this.chkActiveInterlockShare.Checked ? (this.chkGrouped.Checked ? "2" : "1") : "0");
			if (wgAppConfig.IsAccessDB)
			{
				this.btnOK_Click_Acc(sender, e);
				return;
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
				{
					sqlConnection.Open();
					for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
					{
						int num = 0;
						for (int j = 2; j < 6; j++)
						{
							if (this.dataGridView1.Rows[i].Cells[j].Value.ToString() == "1")
							{
								switch (j)
								{
								case 2:
									num = 1;
									break;
								case 3:
									num += 2;
									break;
								case 4:
									num = 4;
									break;
								case 5:
									num = 8;
									break;
								}
							}
						}
						string text = " UPDATE t_b_Controller SET ";
						text += " f_InterLock = ";
						text += num.ToString();
						text = text + " WHERE f_ControllerNO = " + this.dataGridView1.Rows[i].Cells[0].Value.ToString();
						sqlCommand.CommandText = text;
						sqlCommand.ExecuteNonQuery();
					}
				}
			}
			base.Close();
		}

		private void btnOK_Click_Acc(object sender, EventArgs e)
		{
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand("", oleDbConnection))
				{
					oleDbConnection.Open();
					for (int i = 0; i <= this.dataGridView1.Rows.Count - 1; i++)
					{
						int num = 0;
						for (int j = 2; j < 6; j++)
						{
							if (this.dataGridView1.Rows[i].Cells[j].Value.ToString() == "1")
							{
								switch (j)
								{
								case 2:
									num = 1;
									break;
								case 3:
									num += 2;
									break;
								case 4:
									num = 4;
									break;
								case 5:
									num = 8;
									break;
								}
							}
						}
						string text = " UPDATE t_b_Controller SET ";
						text += " f_InterLock = ";
						text += num.ToString();
						text = text + " WHERE f_ControllerNO = " + this.dataGridView1.Rows[i].Cells[0].Value.ToString();
						oleDbCommand.CommandText = text;
						oleDbCommand.ExecuteNonQuery();
					}
				}
			}
			base.Close();
		}

		private void dfrmControllerInterLock_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.Control && e.KeyValue == 81 && e.Shift)
				{
					if (!this.chkGrouped.Visible)
					{
						if (this.chkActiveInterlockShare.Visible)
						{
							this.chkGrouped.Visible = true;
							this.dataGridView1.Location = new Point(8, 72);
							this.dataGridView1.Size = new Size(this.dataGridView1.Size.Width, this.dataGridView1.Size.Height + 40 - this.dataGridView1.Location.Y);
						}
						else
						{
							this.chkActiveInterlockShare.Visible = true;
							this.dataGridView1.Location = new Point(8, 40);
							this.dataGridView1.Size = new Size(this.dataGridView1.Size.Width, this.dataGridView1.Size.Height + 8 - this.dataGridView1.Location.Y);
						}
					}
					this.chkActiveInterlockShare_CheckedChanged(null, null);
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

		private void dfrmControllerInterLock_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void chkActiveInterlockShare_CheckedChanged(object sender, EventArgs e)
		{
			if (this.chkActiveInterlockShare.Checked)
			{
				for (int i = 0; i < this.dataGridView1.RowCount; i++)
				{
					DataGridViewRow dataGridViewRow = this.dataGridView1.Rows[i];
					int controllerType = wgMjController.GetControllerType(int.Parse(dataGridViewRow.Cells[1].Value.ToString()));
					if (controllerType == 2)
					{
						dataGridViewRow.Cells[3].ReadOnly = true;
						dataGridViewRow.Cells[4].ReadOnly = true;
						dataGridViewRow.Cells[5].ReadOnly = true;
						dataGridViewRow.Cells[3].Style.BackColor = SystemPens.InactiveBorder.Color;
						dataGridViewRow.Cells[4].Style.BackColor = SystemPens.InactiveBorder.Color;
						dataGridViewRow.Cells[5].Style.BackColor = SystemPens.InactiveBorder.Color;
					}
					else
					{
						dataGridViewRow.Cells[2].Value = 0;
						dataGridViewRow.Cells[3].Value = 0;
						dataGridViewRow.Cells[4].Value = 0;
						dataGridViewRow.Cells[2].ReadOnly = true;
						dataGridViewRow.Cells[3].ReadOnly = true;
						dataGridViewRow.Cells[4].ReadOnly = true;
						dataGridViewRow.Cells[2].Style.BackColor = SystemPens.InactiveBorder.Color;
						dataGridViewRow.Cells[3].Style.BackColor = SystemPens.InactiveBorder.Color;
						dataGridViewRow.Cells[4].Style.BackColor = SystemPens.InactiveBorder.Color;
					}
				}
			}
			if (this.chkActiveInterlockShare.Checked)
			{
				this.chkGrouped.Enabled = true;
				return;
			}
			this.chkGrouped.Enabled = false;
			this.chkGrouped.Checked = false;
		}

		private void chkGrouped_CheckedChanged(object sender, EventArgs e)
		{
		}

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerInterLock));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.chkGrouped = new CheckBox();
			this.chkActiveInterlockShare = new CheckBox();
			this.btnCancel = new Button();
			this.btnOK = new Button();
			this.dataGridView1 = new DataGridView();
			this.f_ControllerID = new DataGridViewTextBoxColumn();
			this.f_ControllerSN = new DataGridViewTextBoxColumn();
			this.f_InterLock12 = new DataGridViewCheckBoxColumn();
			this.f_InterLock34 = new DataGridViewCheckBoxColumn();
			this.f_InterLock123 = new DataGridViewCheckBoxColumn();
			this.f_InterLock1234 = new DataGridViewCheckBoxColumn();
			this.f_DoorNames = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dataGridView1).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.chkGrouped, "chkGrouped");
			this.chkGrouped.BackColor = Color.Transparent;
			this.chkGrouped.ForeColor = Color.White;
			this.chkGrouped.Name = "chkGrouped";
			this.chkGrouped.UseVisualStyleBackColor = false;
			this.chkGrouped.CheckedChanged += new EventHandler(this.chkGrouped_CheckedChanged);
			componentResourceManager.ApplyResources(this.chkActiveInterlockShare, "chkActiveInterlockShare");
			this.chkActiveInterlockShare.BackColor = Color.Transparent;
			this.chkActiveInterlockShare.ForeColor = Color.White;
			this.chkActiveInterlockShare.Name = "chkActiveInterlockShare";
			this.chkActiveInterlockShare.UseVisualStyleBackColor = false;
			this.chkActiveInterlockShare.CheckedChanged += new EventHandler(this.chkActiveInterlockShare_CheckedChanged);
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
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dataGridView1, "dataGridView1");
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
				this.f_InterLock12,
				this.f_InterLock34,
				this.f_InterLock123,
				this.f_InterLock1234,
				this.f_DoorNames
			});
			this.dataGridView1.EnableHeadersVisualStyles = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
			componentResourceManager.ApplyResources(this.f_ControllerID, "f_ControllerID");
			this.f_ControllerID.Name = "f_ControllerID";
			this.f_ControllerID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_ControllerSN, "f_ControllerSN");
			this.f_ControllerSN.Name = "f_ControllerSN";
			this.f_ControllerSN.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_InterLock12, "f_InterLock12");
			this.f_InterLock12.Name = "f_InterLock12";
			this.f_InterLock12.Resizable = DataGridViewTriState.True;
			this.f_InterLock12.SortMode = DataGridViewColumnSortMode.Automatic;
			componentResourceManager.ApplyResources(this.f_InterLock34, "f_InterLock34");
			this.f_InterLock34.Name = "f_InterLock34";
			componentResourceManager.ApplyResources(this.f_InterLock123, "f_InterLock123");
			this.f_InterLock123.Name = "f_InterLock123";
			componentResourceManager.ApplyResources(this.f_InterLock1234, "f_InterLock1234");
			this.f_InterLock1234.Name = "f_InterLock1234";
			this.f_DoorNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			componentResourceManager.ApplyResources(this.f_DoorNames, "f_DoorNames");
			this.f_DoorNames.Name = "f_DoorNames";
			this.f_DoorNames.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.chkGrouped);
			base.Controls.Add(this.chkActiveInterlockShare);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.dataGridView1);
			base.Name = "dfrmControllerInterLock";
			base.FormClosing += new FormClosingEventHandler(this.dfrmControllerInterLock_FormClosing);
			base.Load += new EventHandler(this.dfrmControllerInterLock_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmControllerInterLock_KeyDown);
			((ISupportInitialize)this.dataGridView1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
