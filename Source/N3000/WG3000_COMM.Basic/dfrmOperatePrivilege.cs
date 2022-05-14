using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmOperatePrivilege : frmN3000
	{
		private IContainer components;

		private Button btnOK;

		private Button btnFullControlAllOn;

		private Button btnReadAllOn;

		private Button btnFullControlOff;

		private Button btnReadAllOff;

		private DataGridView dgvOperatePrivilege;

		private Button btnCancel;

		private DataGridViewTextBoxColumn f_FunctionID;

		private DataGridViewTextBoxColumn f_FunctionName;

		private DataGridViewTextBoxColumn f_FunctionDisplayName;

		private DataGridViewCheckBoxColumn f_ReadOnly;

		private DataGridViewCheckBoxColumn f_FullControl;

		private DataGridViewTextBoxColumn f_DisplayID;

		public int operatorID = -1;

		private static DataGridViewCellStyle styleYellow = new DataGridViewCellStyle();

		private DataView dv;

		private DataTable tb;

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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmOperatePrivilege));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			this.dgvOperatePrivilege = new DataGridView();
			this.f_FunctionID = new DataGridViewTextBoxColumn();
			this.f_FunctionName = new DataGridViewTextBoxColumn();
			this.f_FunctionDisplayName = new DataGridViewTextBoxColumn();
			this.f_ReadOnly = new DataGridViewCheckBoxColumn();
			this.f_FullControl = new DataGridViewCheckBoxColumn();
			this.f_DisplayID = new DataGridViewTextBoxColumn();
			this.btnReadAllOff = new Button();
			this.btnFullControlOff = new Button();
			this.btnReadAllOn = new Button();
			this.btnFullControlAllOn = new Button();
			this.btnOK = new Button();
			this.btnCancel = new Button();
			((ISupportInitialize)this.dgvOperatePrivilege).BeginInit();
			base.SuspendLayout();
			this.dgvOperatePrivilege.AllowUserToAddRows = false;
			this.dgvOperatePrivilege.AllowUserToDeleteRows = false;
			componentResourceManager.ApplyResources(this.dgvOperatePrivilege, "dgvOperatePrivilege");
			this.dgvOperatePrivilege.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvOperatePrivilege.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvOperatePrivilege.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvOperatePrivilege.Columns.AddRange(new DataGridViewColumn[]
			{
				this.f_FunctionID,
				this.f_FunctionName,
				this.f_FunctionDisplayName,
				this.f_ReadOnly,
				this.f_FullControl,
				this.f_DisplayID
			});
			this.dgvOperatePrivilege.EnableHeadersVisualStyles = false;
			this.dgvOperatePrivilege.Name = "dgvOperatePrivilege";
			this.dgvOperatePrivilege.RowHeadersVisible = false;
			this.dgvOperatePrivilege.RowTemplate.Height = 23;
			this.dgvOperatePrivilege.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dgvOperatePrivilege_CellFormatting);
			componentResourceManager.ApplyResources(this.f_FunctionID, "f_FunctionID");
			this.f_FunctionID.Name = "f_FunctionID";
			componentResourceManager.ApplyResources(this.f_FunctionName, "f_FunctionName");
			this.f_FunctionName.Name = "f_FunctionName";
			componentResourceManager.ApplyResources(this.f_FunctionDisplayName, "f_FunctionDisplayName");
			this.f_FunctionDisplayName.Name = "f_FunctionDisplayName";
			this.f_FunctionDisplayName.SortMode = DataGridViewColumnSortMode.NotSortable;
			componentResourceManager.ApplyResources(this.f_ReadOnly, "f_ReadOnly");
			this.f_ReadOnly.Name = "f_ReadOnly";
			componentResourceManager.ApplyResources(this.f_FullControl, "f_FullControl");
			this.f_FullControl.Name = "f_FullControl";
			componentResourceManager.ApplyResources(this.f_DisplayID, "f_DisplayID");
			this.f_DisplayID.Name = "f_DisplayID";
			this.btnReadAllOff.BackColor = Color.Transparent;
			this.btnReadAllOff.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnReadAllOff, "btnReadAllOff");
			this.btnReadAllOff.ForeColor = Color.White;
			this.btnReadAllOff.Name = "btnReadAllOff";
			this.btnReadAllOff.UseVisualStyleBackColor = false;
			this.btnReadAllOff.Click += new EventHandler(this.btnReadAllOff_Click);
			this.btnFullControlOff.BackColor = Color.Transparent;
			this.btnFullControlOff.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnFullControlOff, "btnFullControlOff");
			this.btnFullControlOff.ForeColor = Color.White;
			this.btnFullControlOff.Name = "btnFullControlOff";
			this.btnFullControlOff.UseVisualStyleBackColor = false;
			this.btnFullControlOff.Click += new EventHandler(this.btnFullControlOff_Click);
			this.btnReadAllOn.BackColor = Color.Transparent;
			this.btnReadAllOn.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnReadAllOn, "btnReadAllOn");
			this.btnReadAllOn.ForeColor = Color.White;
			this.btnReadAllOn.Name = "btnReadAllOn";
			this.btnReadAllOn.UseVisualStyleBackColor = false;
			this.btnReadAllOn.Click += new EventHandler(this.btnReadAllOn_Click);
			this.btnFullControlAllOn.BackColor = Color.Transparent;
			this.btnFullControlAllOn.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnFullControlAllOn, "btnFullControlAllOn");
			this.btnFullControlAllOn.ForeColor = Color.White;
			this.btnFullControlAllOn.Name = "btnFullControlAllOn";
			this.btnFullControlAllOn.UseVisualStyleBackColor = false;
			this.btnFullControlAllOn.Click += new EventHandler(this.btnFullControlAllOn_Click);
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnFullControlAllOn);
			base.Controls.Add(this.btnReadAllOn);
			base.Controls.Add(this.dgvOperatePrivilege);
			base.Controls.Add(this.btnFullControlOff);
			base.Controls.Add(this.btnReadAllOff);
			base.Name = "dfrmOperatePrivilege";
			base.Load += new EventHandler(this.dfrmOperatePrivilege_Load);
			((ISupportInitialize)this.dgvOperatePrivilege).EndInit();
			base.ResumeLayout(false);
		}

		public dfrmOperatePrivilege()
		{
			this.InitializeComponent();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (icOperator.setOperatorPrivilege(this.operatorID, (this.dgvOperatePrivilege.DataSource as DataView).Table) > 0)
			{
				base.Close();
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void updateDr(ref DataRow dr, int id, string name, string display, bool read, bool fullControl)
		{
			dr[0] = id;
			dr[1] = name;
			dr[2] = display;
			dr[3] = read;
			dr[4] = fullControl;
		}

		private void dfrmOperatePrivilege_Load(object sender, EventArgs e)
		{
			this.dgvOperatePrivilege.AutoGenerateColumns = false;
			this.tb = new DataTable();
			this.tb.TableName = "OperatePrivilege";
			this.tb.Columns.Add("f_FunctionID");
			this.tb.Columns.Add("f_FunctionName");
			this.tb.Columns.Add("f_FunctionDisplayName");
			this.tb.Columns.Add("f_ReadOnly");
			this.tb.Columns.Add("f_FullControl");
			this.tb.Columns.Add("f_DisplayID");
			DataRow row = this.tb.NewRow();
			this.updateDr(ref row, 1, "frmControllers", "Controllers", false, true);
			this.tb.Rows.Add(row);
			this.tb.AcceptChanges();
			this.dgvOperatePrivilege.DataSource = this.tb;
			this.dgvOperatePrivilege.Columns[0].DataPropertyName = "f_FunctionID";
			this.dgvOperatePrivilege.Columns[1].DataPropertyName = "f_FunctionName";
			this.dgvOperatePrivilege.Columns[2].DataPropertyName = "f_FunctionDisplayName";
			this.dgvOperatePrivilege.Columns[3].DataPropertyName = "f_ReadOnly";
			this.dgvOperatePrivilege.Columns[4].DataPropertyName = "f_FullControl";
			this.dgvOperatePrivilege.Columns[5].DataPropertyName = "f_DisplayID";
			string info = "";
			DataTable operatorPrivilege = icOperator.getOperatorPrivilege(this.operatorID);
			if (operatorPrivilege != null)
			{
				operatorPrivilege.Columns.Add("f_DisplayID");
				string[] array = new string[]
				{
					"100",
					"mnu1BasicConfigure",
					"110",
					"mnuControllers",
					"120",
					"mnuGroups",
					"130",
					"mnuConsumers",
					"131",
					"mnuCardLost",
					"200",
					"mnu1DoorControl",
					"210",
					"mnuControlSeg",
					"220",
					"mnuPrivilege",
					"230",
					"mnuPeripheral",
					"240",
					"mnuPasswordManagement",
					"250",
					"mnuAntiBack",
					"260",
					"mnuInterLock",
					"270",
					"mnuMoreCards",
					"280",
					"mnuFirstCard",
					"300",
					"mnu1BasicOperate",
					"310",
					"mnuTotalControl",
					"311",
					"mnuCheckController",
					"312",
					"mnuAdjustTime",
					"313",
					"mnuUpload",
					"317",
					"mnuMonitor",
					"314",
					"mnuGetCardRecords",
					"316",
					"TotalControl_RemoteOpen",
					"320",
					"mnuCardRecords",
					"400",
					"mnu1Attendence",
					"410",
					"mnuShiftNormalConfigure",
					"420",
					"mnuShiftRule",
					"430",
					"mnuShiftSet",
					"440",
					"mnuShiftArrange",
					"450",
					"mnuHolidaySet",
					"460",
					"mnuLeave",
					"470",
					"mnuManualCardRecord",
					"480",
					"mnuAttendenceData",
					"500",
					"mnu1Tool",
					"510",
					"cmdChangePasswor",
					"520",
					"cmdOperatorManage",
					"530",
					"mnuDBBackup",
					"540",
					"mnuExtendedFunction",
					"550",
					"mnuOption",
					"560",
					"mnuTaskList",
					"570",
					"mnuLogQuery",
					"600",
					"mnu1Help",
					"610",
					"mnuAbout",
					"620",
					"mnuManual",
					"630",
					"mnuSystemCharacteristic",
					"318",
					"btnMaps",
					"580",
					"btnZoneManage",
					"315",
					"mnuRealtimeGetRecords",
					"581",
					"mnuPatrolDetailData",
					"582",
					"mnuConstMeal",
					"583",
					"mnuMeeting",
					"584",
					"mnuElevator",
					"",
					""
				};
				for (int i = 0; i < operatorPrivilege.Rows.Count; i++)
				{
					operatorPrivilege.Rows[i]["f_FunctionDisplayName"] = CommonStr.ResourceManager.GetString("strFunctionDisplayName_" + operatorPrivilege.Rows[i]["f_FunctionName"].ToString());
					operatorPrivilege.Rows[i]["f_FunctionDisplayName"] = wgAppConfig.ReplaceFloorRomm(operatorPrivilege.Rows[i]["f_FunctionDisplayName"] as string);
					for (int j = 0; j < array.Length; j += 2)
					{
						if (array[j + 1] == operatorPrivilege.Rows[i]["f_FunctionName"].ToString())
						{
							operatorPrivilege.Rows[i]["f_DisplayID"] = array[j];
						}
					}
				}
				this.dgvOperatePrivilege.DataSource = operatorPrivilege;
				this.dv = new DataView(operatorPrivilege);
				this.dv.Sort = "f_DisplayID";
				this.dgvOperatePrivilege.DataSource = this.dv;
			}
			wgTools.WgDebugWrite(info, new object[0]);
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			dfrmOperatePrivilege.styleYellow = new DataGridViewCellStyle
			{
				Alignment = DataGridViewContentAlignment.MiddleLeft,
				BackColor = Color.Yellow,
				Font = new Font("微软雅黑", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 134),
				ForeColor = Color.Blue,
				SelectionBackColor = SystemColors.Highlight,
				SelectionForeColor = SystemColors.HighlightText,
				WrapMode = DataGridViewTriState.False
			};
		}

		private void btnReadAllOn_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dgvOperatePrivilege.Columns.Count; i++)
			{
				if (this.dgvOperatePrivilege.Columns[i].Name.Equals("f_ReadOnly"))
				{
					for (int j = 0; j < this.dgvOperatePrivilege.Rows.Count; j++)
					{
						this.dgvOperatePrivilege[i, j].Value = true;
					}
				}
			}
		}

		private void btnFullControlAllOn_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dgvOperatePrivilege.Columns.Count; i++)
			{
				if (this.dgvOperatePrivilege.Columns[i].Name.Equals("f_FullControl"))
				{
					for (int j = 0; j < this.dgvOperatePrivilege.Rows.Count; j++)
					{
						this.dgvOperatePrivilege[i, j].Value = true;
					}
				}
			}
		}

		private void btnReadAllOff_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dgvOperatePrivilege.Columns.Count; i++)
			{
				if (this.dgvOperatePrivilege.Columns[i].Name.Equals("f_ReadOnly"))
				{
					for (int j = 0; j < this.dgvOperatePrivilege.Rows.Count; j++)
					{
						this.dgvOperatePrivilege[i, j].Value = false;
					}
				}
			}
		}

		private void btnFullControlOff_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.dgvOperatePrivilege.Columns.Count; i++)
			{
				if (this.dgvOperatePrivilege.Columns[i].Name.Equals("f_FullControl"))
				{
					for (int j = 0; j < this.dgvOperatePrivilege.Rows.Count; j++)
					{
						this.dgvOperatePrivilege[i, j].Value = false;
					}
				}
			}
		}

		private void dgvOperatePrivilege_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			DataGridViewCell dataGridViewCell = this.dgvOperatePrivilege[1, e.RowIndex];
			if (e.Value == null)
			{
				return;
			}
			ArrayList arrayList = new ArrayList
			{
				"mnu1BasicConfigure",
				"mnu1DoorControl",
				"mnu1BasicOperate",
				"mnu1Attendence",
				"mnu1Tool",
				"mnu1Help"
			};
			if (arrayList.IndexOf(dataGridViewCell.Value.ToString()) >= 0)
			{
				DataGridViewRow dataGridViewRow = this.dgvOperatePrivilege.Rows[e.RowIndex];
				dataGridViewRow.DefaultCellStyle = dfrmOperatePrivilege.styleYellow;
			}
		}
	}
}
