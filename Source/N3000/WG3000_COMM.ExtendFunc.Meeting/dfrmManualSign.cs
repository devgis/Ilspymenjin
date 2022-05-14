using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Basic;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;

namespace WG3000_COMM.ExtendFunc.Meeting
{
	public class dfrmManualSign : frmN3000
	{
		public string curMeetingNo = "";

		private DataGridView dgvSelectedUsers;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

		private DataGridViewTextBoxColumn Identity;

		private DataGridViewTextBoxColumn IdentityStr2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

		private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

		private DataGridViewTextBoxColumn f_MoreCards_GrpID;

		private DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;

		private DataGridViewTextBoxColumn f_SelectedGroup;

		public string curMode = "";

		private Container components;

		internal Button btnOk;

		internal Button btnCancel;

		internal Label Label4;

		internal DateTimePicker dtpMeetingDate;

		internal DateTimePicker dtpMeetingTime;

		internal Button btnDelete;

		private DataSet ds = new DataSet();

		private DataTable dtUser1;

		private dfrmFind dfrmFind1 = new dfrmFind();

		public dfrmManualSign()
		{
			this.InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmManualSign));
			DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			this.btnOk = new Button();
			this.btnCancel = new Button();
			this.Label4 = new Label();
			this.dtpMeetingDate = new DateTimePicker();
			this.dtpMeetingTime = new DateTimePicker();
			this.btnDelete = new Button();
			this.dgvSelectedUsers = new DataGridView();
			this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
			this.Identity = new DataGridViewTextBoxColumn();
			this.IdentityStr2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
			this.f_MoreCards_GrpID = new DataGridViewTextBoxColumn();
			this.dataGridViewCheckBoxColumn1 = new DataGridViewCheckBoxColumn();
			this.f_SelectedGroup = new DataGridViewTextBoxColumn();
			((ISupportInitialize)this.dgvSelectedUsers).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.btnOk, "btnOk");
			this.btnOk.BackColor = Color.Transparent;
			this.btnOk.BackgroundImage = Resources.pMain_button_normal;
			this.btnOk.ForeColor = Color.White;
			this.btnOk.Name = "btnOk";
			this.btnOk.UseVisualStyleBackColor = false;
			this.btnOk.Click += new EventHandler(this.btnOk_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.Label4, "Label4");
			this.Label4.BackColor = Color.Transparent;
			this.Label4.ForeColor = Color.White;
			this.Label4.Name = "Label4";
			componentResourceManager.ApplyResources(this.dtpMeetingDate, "dtpMeetingDate");
			this.dtpMeetingDate.Name = "dtpMeetingDate";
			this.dtpMeetingDate.Value = new DateTime(2008, 2, 21, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.dtpMeetingTime, "dtpMeetingTime");
			this.dtpMeetingTime.Format = DateTimePickerFormat.Time;
			this.dtpMeetingTime.Name = "dtpMeetingTime";
			this.dtpMeetingTime.ShowUpDown = true;
			this.dtpMeetingTime.Value = new DateTime(2008, 2, 21, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.btnDelete, "btnDelete");
			this.btnDelete.BackColor = Color.Transparent;
			this.btnDelete.BackgroundImage = Resources.pMain_button_normal;
			this.btnDelete.ForeColor = Color.White;
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.UseVisualStyleBackColor = false;
			this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
			this.dgvSelectedUsers.AllowUserToAddRows = false;
			this.dgvSelectedUsers.AllowUserToDeleteRows = false;
			this.dgvSelectedUsers.AllowUserToOrderColumns = true;
			componentResourceManager.ApplyResources(this.dgvSelectedUsers, "dgvSelectedUsers");
			this.dgvSelectedUsers.BackgroundColor = SystemColors.Window;
			dataGridViewCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle.BackColor = Color.FromArgb(124, 125, 156);
			dataGridViewCellStyle.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle.ForeColor = Color.White;
			dataGridViewCellStyle.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = DataGridViewTriState.True;
			this.dgvSelectedUsers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
			this.dgvSelectedUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			this.dgvSelectedUsers.Columns.AddRange(new DataGridViewColumn[]
			{
				this.dataGridViewTextBoxColumn1,
				this.Identity,
				this.IdentityStr2,
				this.dataGridViewTextBoxColumn2,
				this.dataGridViewTextBoxColumn3,
				this.dataGridViewTextBoxColumn4,
				this.f_MoreCards_GrpID,
				this.dataGridViewCheckBoxColumn1,
				this.f_SelectedGroup
			});
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = SystemColors.Window;
			dataGridViewCellStyle2.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			this.dgvSelectedUsers.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSelectedUsers.EnableHeadersVisualStyles = false;
			this.dgvSelectedUsers.Name = "dgvSelectedUsers";
			this.dgvSelectedUsers.ReadOnly = true;
			this.dgvSelectedUsers.RowTemplate.Height = 23;
			this.dgvSelectedUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.Identity, "Identity");
			this.Identity.Name = "Identity";
			this.Identity.ReadOnly = true;
			componentResourceManager.ApplyResources(this.IdentityStr2, "IdentityStr2");
			this.IdentityStr2.Name = "IdentityStr2";
			this.IdentityStr2.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			this.dataGridViewTextBoxColumn3.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			this.dataGridViewTextBoxColumn4.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_MoreCards_GrpID, "f_MoreCards_GrpID");
			this.f_MoreCards_GrpID.Name = "f_MoreCards_GrpID";
			this.f_MoreCards_GrpID.ReadOnly = true;
			componentResourceManager.ApplyResources(this.dataGridViewCheckBoxColumn1, "dataGridViewCheckBoxColumn1");
			this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
			this.dataGridViewCheckBoxColumn1.ReadOnly = true;
			componentResourceManager.ApplyResources(this.f_SelectedGroup, "f_SelectedGroup");
			this.f_SelectedGroup.Name = "f_SelectedGroup";
			this.f_SelectedGroup.ReadOnly = true;
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.dgvSelectedUsers);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.dtpMeetingDate);
			base.Controls.Add(this.dtpMeetingTime);
			base.Controls.Add(this.btnOk);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnDelete);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmManualSign";
			base.FormClosing += new FormClosingEventHandler(this.dfrmManualSign_FormClosing);
			base.Load += new EventHandler(this.dfrmManualSign_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmManualSign_KeyDown);
			((ISupportInitialize)this.dgvSelectedUsers).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void dfrmManualSign_Load(object sender, EventArgs e)
		{
			Cursor current = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			base.KeyPreview = true;
			try
			{
				if (this.curMeetingNo == "")
				{
					base.Close();
					return;
				}
				this.dtpMeetingDate.Value = DateTime.Now.Date;
				this.dtpMeetingTime.Value = DateTime.Parse(Strings.Format(DateTime.Now, "yyyy-MM-dd HH:mm:ss"));
				this.dtpMeetingTime.CustomFormat = "HH:mm:ss";
				this.dtpMeetingTime.Format = DateTimePickerFormat.Custom;
				this.dtUser1 = new DataTable();
				string text;
				if (wgAppConfig.IsAccessDB)
				{
					text = " SELECT  t_b_Consumer.f_ConsumerID ";
					text += " , f_MeetingIdentity,' ' as  f_MeetingIdentityStr, f_ConsumerNO, f_ConsumerName, f_CardNO ";
					text += " , f_Seat ";
					text += " ,IIF (t_d_MeetingConsumer.f_MeetingIdentity IS NULL, 0,  IIF (  t_d_MeetingConsumer.f_MeetingIdentity <0 , 0 , 1 )) AS f_Selected ";
					text += " , f_GroupID ";
					text += " FROM t_b_Consumer ";
					text = text + " INNER JOIN t_d_MeetingConsumer ON ( t_b_Consumer.f_ConsumerID = t_d_MeetingConsumer.f_ConsumerID AND f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo) + ")";
					text += " ORDER BY f_ConsumerNO ASC ";
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
						{
							using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
							{
								oleDbDataAdapter.Fill(this.dtUser1);
							}
						}
						goto IL_21F;
					}
				}
				text = " SELECT  t_b_Consumer.f_ConsumerID ";
				text += " , f_MeetingIdentity,' ' as f_MeetingIdentityStr, f_ConsumerNO, f_ConsumerName, f_CardNO ";
				text += " , f_Seat ";
				text += " , CASE WHEN t_d_MeetingConsumer.f_MeetingIdentity IS NULL THEN 0 ELSE CASE WHEN t_d_MeetingConsumer.f_MeetingIdentity < 0 THEN 0 ELSE 1 END END AS f_Selected ";
				text += " , f_GroupID ";
				text += " FROM t_b_Consumer ";
				text = text + " INNER JOIN t_d_MeetingConsumer ON ( t_b_Consumer.f_ConsumerID = t_d_MeetingConsumer.f_ConsumerID AND f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo) + ")";
				text += " ORDER BY f_ConsumerNO ASC ";
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlDataAdapter.Fill(this.dtUser1);
						}
					}
				}
				IL_21F:
				for (int i = 0; i < this.dtUser1.Rows.Count; i++)
				{
					DataRow dataRow = this.dtUser1.Rows[i];
					if (!string.IsNullOrEmpty(dataRow["f_MeetingIdentity"].ToString()) && (int)dataRow["f_MeetingIdentity"] >= 0)
					{
						dataRow["f_MeetingIdentityStr"] = frmMeetings.getStrMeetingIdentity((long)((int)dataRow["f_MeetingIdentity"]));
					}
				}
				this.dtUser1.AcceptChanges();
				DataView dataView = new DataView(this.dtUser1);
				for (int j = 0; j < dataView.Table.Columns.Count; j++)
				{
					this.dgvSelectedUsers.Columns[j].DataPropertyName = this.dtUser1.Columns[j].ColumnName;
				}
				this.dgvSelectedUsers.DataSource = dataView;
				this.dgvSelectedUsers.DefaultCellStyle.ForeColor = Color.Black;
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
			Cursor.Current = current;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvSelectedUsers.SelectedRows.Count <= 0)
				{
					if (this.dgvSelectedUsers.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvSelectedUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvSelectedUsers.SelectedRows[0].Index;
				}
				if (this.curMode == "" || this.curMode.ToUpper() == "ManualSign".ToUpper())
				{
					string text = " UPDATE t_d_MeetingConsumer ";
					text += "SET f_SignWay = 1 ";
					text = text + " , f_SignRealTime = " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpMeetingTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text += " , f_RecID = 0 ";
					text = text + " WHERE  f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
					text = text + " AND f_ConsumerID = " + this.dgvSelectedUsers.Rows[index].Cells[0].Value.ToString();
					int num = wgAppConfig.runUpdateSql(text);
					if (num == 1)
					{
						base.Close();
						base.DialogResult = DialogResult.OK;
					}
				}
				if (this.curMode.ToUpper() == "Leave".ToUpper())
				{
					string text2 = " UPDATE t_d_MeetingConsumer ";
					text2 += "SET f_SignWay = 2 ";
					text2 = text2 + " , f_SignRealTime = " + wgTools.PrepareStr(this.dtpMeetingDate.Value.ToString("yyyy-MM-dd") + " " + this.dtpMeetingTime.Value.ToString("HH:mm:ss"), true, "yyyy-MM-dd HH:mm:ss");
					text2 += " , f_RecID = 0 ";
					text2 = text2 + " WHERE  f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
					text2 = text2 + " AND f_ConsumerID = " + this.dgvSelectedUsers.Rows[index].Cells[0].Value.ToString();
					int num2 = wgAppConfig.runUpdateSql(text2);
					if (num2 == 1)
					{
						base.Close();
						base.DialogResult = DialogResult.OK;
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			try
			{
				int index;
				if (this.dgvSelectedUsers.SelectedRows.Count <= 0)
				{
					if (this.dgvSelectedUsers.SelectedCells.Count <= 0)
					{
						return;
					}
					index = this.dgvSelectedUsers.SelectedCells[0].RowIndex;
				}
				else
				{
					index = this.dgvSelectedUsers.SelectedRows[0].Index;
				}
				string text = " UPDATE t_d_MeetingConsumer ";
				text += "SET f_SignWay = 0 ";
				text += " , f_SignRealTime = NULL ";
				text += " , f_RecID = 0 ";
				text = text + " WHERE  f_MeetingNO = " + wgTools.PrepareStr(this.curMeetingNo);
				text = text + " AND f_ConsumerID = " + this.dgvSelectedUsers.Rows[index].Cells[0].Value.ToString();
				int num = wgAppConfig.runUpdateSql(text);
				if (num == 1)
				{
					base.Close();
					base.DialogResult = DialogResult.OK;
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dfrmManualSign_KeyDown(object sender, KeyEventArgs e)
		{
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
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmManualSign_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dfrmFind1 != null)
			{
				this.dfrmFind1.ReallyCloseForm();
			}
		}
	}
}
