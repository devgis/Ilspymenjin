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

namespace WG3000_COMM.ExtendFunc
{
	public class dfrmControllerTask : frmN3000
	{
		private DataTable dtDoors;

		private DataView dvDoors;

		private ArrayList arrDoorID = new ArrayList();

		private IContainer components;

		internal Button cmdCancel;

		internal Button cmdOK;

		private TextBox txtNote;

		private Label label3;

		private Label label2;

		private ComboBox cboAccessMethod;

		private Label label1;

		private ComboBox cboDoors;

		private CheckBox checkBox49;

		private CheckBox checkBox48;

		private CheckBox checkBox47;

		private CheckBox checkBox46;

		private CheckBox checkBox45;

		private CheckBox checkBox44;

		private CheckBox checkBox43;

		private Label label45;

		private DateTimePicker dtpTime;

		private Label label43;

		private Label label44;

		private DateTimePicker dtpEnd;

		private DateTimePicker dtpBegin;

		internal CheckBox chk5;

		internal CheckBox chk3;

		internal CheckBox chk4;

		internal CheckBox chk1;

		internal CheckBox chk2;

		internal GroupBox groupBox6;

		internal GroupBox groupBox1;

		internal GroupBox groupBox2;

		internal GroupBox groupBox5;

		internal GroupBox groupBox4;

		private Label lblTaskID;

		internal CheckBox chk6;

		public TextBox txtTaskIDs;

		internal GroupBox groupBox3;

		public dfrmControllerTask()
		{
			this.InitializeComponent();
		}

		private string getDateString(DateTimePicker dtp)
		{
			if (dtp == null)
			{
				return wgTools.PrepareStr("");
			}
			return wgTools.PrepareStr(dtp.Value.ToString(wgTools.YMDHMSFormat), true, wgTools.YMDHMSFormat);
		}

		private void cmdOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.groupBox1.Enabled && this.dtpBegin.Value.Date > this.dtpEnd.Value.Date)
				{
					string strTimeInvalidParm = CommonStr.strTimeInvalidParm;
					XMessageBox.Show(strTimeInvalidParm, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else if (this.groupBox3.Enabled && !this.checkBox43.Checked && !this.checkBox44.Checked && !this.checkBox45.Checked && !this.checkBox46.Checked && !this.checkBox47.Checked && !this.checkBox48.Checked && !this.checkBox49.Checked)
				{
					string strTimeInvalidParm2 = CommonStr.strTimeInvalidParm;
					XMessageBox.Show(strTimeInvalidParm2, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else if (this.groupBox4.Enabled && this.cboDoors.SelectedIndex < 0)
				{
					XMessageBox.Show(this.label2.Text + "...?", wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
				else
				{
					if (this.txtTaskIDs.Text.IndexOf("(") < 0)
					{
						string text = " UPDATE t_b_ControllerTaskList SET ";
						text = text + " f_BeginYMD =" + this.getDateString(this.dtpBegin);
						text = text + " , f_EndYMD =" + this.getDateString(this.dtpEnd);
						text = text + " , f_OperateTime = " + this.getDateString(this.dtpTime);
						text = text + " , f_Monday =" + (this.checkBox43.Checked ? "1" : "0");
						text = text + " , f_Tuesday = " + (this.checkBox44.Checked ? "1" : "0");
						text = text + " , f_Wednesday = " + (this.checkBox45.Checked ? "1" : "0");
						text = text + " , f_Thursday = " + (this.checkBox46.Checked ? "1" : "0");
						text = text + " , f_Friday = " + (this.checkBox47.Checked ? "1" : "0");
						text = text + " , f_Saturday = " + (this.checkBox48.Checked ? "1" : "0");
						text = text + " , f_Sunday = " + (this.checkBox49.Checked ? "1" : "0");
						text = text + " , f_DoorID = " + this.arrDoorID[this.cboDoors.SelectedIndex];
						if (this.cboAccessMethod.SelectedIndex < 0)
						{
							this.cboAccessMethod.SelectedIndex = 0;
						}
						text = text + " , f_DoorControl = " + this.cboAccessMethod.SelectedIndex;
						text = text + " , f_Notes = " + wgTools.PrepareStr(this.txtNote.Text.Trim());
						text = text + " WHERE [f_Id]= " + this.txtTaskIDs.Text;
						int num = wgAppConfig.runUpdateSql(text);
						if (num <= 0)
						{
							return;
						}
						wgAppConfig.wgLog(string.Format("{0} {1}:{2} [{3}]", new object[]
						{
							this.Text,
							this.lblTaskID.Text,
							this.txtTaskIDs.Text,
							text
						}));
					}
					else
					{
						string text2 = "";
						string text3 = " UPDATE t_b_ControllerTaskList SET ";
						text2 += ((!this.chk1.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_BeginYMD =" + this.getDateString(this.dtpBegin)));
						text2 += ((!this.chk1.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_EndYMD =" + this.getDateString(this.dtpEnd)));
						text2 += ((!this.chk2.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_OperateTime = " + this.getDateString(this.dtpTime)));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Monday =" + (this.checkBox43.Checked ? "1" : "0")));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Tuesday = " + (this.checkBox44.Checked ? "1" : "0")));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Wednesday = " + (this.checkBox45.Checked ? "1" : "0")));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Thursday = " + (this.checkBox46.Checked ? "1" : "0")));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Friday = " + (this.checkBox47.Checked ? "1" : "0")));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Saturday = " + (this.checkBox48.Checked ? "1" : "0")));
						text2 += ((!this.chk3.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_Sunday = " + (this.checkBox49.Checked ? "1" : "0")));
						text2 += ((!this.chk4.Checked) ? "" : (((text2 == "") ? " " : " , ") + " f_DoorID = " + this.arrDoorID[this.cboDoors.SelectedIndex]));
						if (this.cboAccessMethod.SelectedIndex < 0)
						{
							this.cboAccessMethod.SelectedIndex = 0;
						}
						text2 += ((!this.chk5.Checked) ? "" : (((text2 == "") ? " " : " , ") + "  f_DoorControl = " + this.cboAccessMethod.SelectedIndex));
						text2 += ((!this.chk6.Checked) ? "" : (((text2 == "") ? " " : " , ") + "  f_Notes = " + wgTools.PrepareStr(this.txtNote.Text.Trim())));
						if (text2 != "")
						{
							text3 += text2;
							text3 = text3 + " WHERE [f_Id] IN " + this.txtTaskIDs.Text;
							int num2 = wgAppConfig.runUpdateSql(text3);
							if (num2 <= 0)
							{
								return;
							}
							wgAppConfig.wgLog(string.Format("{0} {1}:{2} [{3}]", new object[]
							{
								this.Text,
								this.lblTaskID.Text,
								this.txtTaskIDs.Text,
								text3
							}));
						}
					}
					base.DialogResult = DialogResult.OK;
					base.Close();
				}
			}
			catch (Exception ex)
			{
				wgAppConfig.wgLog(ex.ToString());
			}
		}

		private void cmdCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		private void loadDoorData()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			this.dtDoors = new DataTable();
			this.dvDoors = new DataView(this.dtDoors);
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
					{
						using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
						{
							oleDbDataAdapter.Fill(this.dtDoors);
						}
					}
					goto IL_E3;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						sqlDataAdapter.Fill(this.dtDoors);
					}
				}
			}
			IL_E3:
			int count = this.dtDoors.Rows.Count;
			icControllerZone icControllerZone = new icControllerZone();
			icControllerZone.getAllowedControllers(ref this.dtDoors);
			this.cboDoors.Items.Clear();
			if (count == this.dtDoors.Rows.Count)
			{
				this.cboDoors.Items.Add(CommonStr.strAll);
				this.arrDoorID.Add(0);
			}
			if (this.dvDoors.Count > 0)
			{
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					this.cboDoors.Items.Add(wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]));
					this.arrDoorID.Add(this.dvDoors[i]["f_DoorID"]);
				}
			}
			if (this.cboDoors.Items.Count > 0)
			{
				this.cboDoors.SelectedIndex = 0;
			}
		}

		private void dfrmControllerTask_Load(object sender, EventArgs e)
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.dfrmControllerTask_Load_Acc(sender, e);
				return;
			}
			this.dtpBegin.Value = DateTime.Now.Date;
			this.dtpTime.CustomFormat = "HH:mm";
			this.dtpTime.Format = DateTimePickerFormat.Custom;
			this.dtpTime.Value = DateTime.Parse("00:00:00");
			this.loadDoorData();
			if (this.cboAccessMethod.Items.Count > 0)
			{
				this.cboAccessMethod.SelectedIndex = 0;
			}
			if (!string.IsNullOrEmpty(this.txtTaskIDs.Text))
			{
				if (this.txtTaskIDs.Text.IndexOf("(") < 0)
				{
					this.groupBox6.Visible = true;
					string cmdText = " SELECT * FROM t_b_ControllerTaskList WHERE [f_Id]= " + this.txtTaskIDs.Text;
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
						{
							sqlConnection.Open();
							SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
							if (sqlDataReader.Read())
							{
								this.dtpBegin.Value = DateTime.Parse(wgTools.SetObjToStr(sqlDataReader["f_BeginYMD"]));
								this.dtpEnd.Value = DateTime.Parse(wgTools.SetObjToStr(sqlDataReader["f_EndYMD"]));
								this.dtpTime.Value = DateTime.Parse(wgTools.SetObjToStr(sqlDataReader["f_OperateTime"]));
								this.checkBox43.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Monday"]) == "1");
								this.checkBox44.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Tuesday"]) == "1");
								this.checkBox45.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Wednesday"]) == "1");
								this.checkBox46.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Thursday"]) == "1");
								this.checkBox47.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Friday"]) == "1");
								this.checkBox48.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Saturday"]) == "1");
								this.checkBox49.Checked = (wgTools.SetObjToStr(sqlDataReader["f_Sunday"]) == "1");
								this.cboDoors.SelectedIndex = this.arrDoorID.IndexOf(int.Parse(wgTools.SetObjToStr(sqlDataReader["f_DoorID"])));
								this.cboAccessMethod.SelectedIndex = int.Parse(wgTools.SetObjToStr(sqlDataReader["f_DoorControl"]));
								this.txtNote.Text = wgTools.SetObjToStr(sqlDataReader["f_Notes"]);
							}
							sqlDataReader.Close();
						}
						return;
					}
				}
				this.chk1.Visible = true;
				this.chk2.Visible = true;
				this.chk3.Visible = true;
				this.chk4.Visible = true;
				this.chk5.Visible = true;
				this.chk6.Visible = true;
				this.groupBox1.Enabled = false;
				this.groupBox2.Enabled = false;
				this.groupBox3.Enabled = false;
				this.groupBox4.Enabled = false;
				this.groupBox5.Enabled = false;
				this.groupBox6.Enabled = false;
			}
		}

		private void dfrmControllerTask_Load_Acc(object sender, EventArgs e)
		{
			bool arg_05_0 = wgAppConfig.IsAccessDB;
			this.dtpBegin.Value = DateTime.Now.Date;
			this.dtpTime.CustomFormat = "HH:mm";
			this.dtpTime.Format = DateTimePickerFormat.Custom;
			this.dtpTime.Value = DateTime.Parse("00:00:00");
			this.loadDoorData();
			if (this.cboAccessMethod.Items.Count > 0)
			{
				this.cboAccessMethod.SelectedIndex = 0;
			}
			if (!string.IsNullOrEmpty(this.txtTaskIDs.Text))
			{
				if (this.txtTaskIDs.Text.IndexOf("(") < 0)
				{
					this.groupBox6.Visible = true;
					string cmdText = " SELECT * FROM t_b_ControllerTaskList WHERE [f_Id]= " + this.txtTaskIDs.Text;
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
						{
							oleDbConnection.Open();
							OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
							if (oleDbDataReader.Read())
							{
								this.dtpBegin.Value = DateTime.Parse(wgTools.SetObjToStr(oleDbDataReader["f_BeginYMD"]));
								this.dtpEnd.Value = DateTime.Parse(wgTools.SetObjToStr(oleDbDataReader["f_EndYMD"]));
								this.dtpTime.Value = DateTime.Parse(wgTools.SetObjToStr(oleDbDataReader["f_OperateTime"]));
								this.checkBox43.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Monday"]) == "1");
								this.checkBox44.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Tuesday"]) == "1");
								this.checkBox45.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Wednesday"]) == "1");
								this.checkBox46.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Thursday"]) == "1");
								this.checkBox47.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Friday"]) == "1");
								this.checkBox48.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Saturday"]) == "1");
								this.checkBox49.Checked = (wgTools.SetObjToStr(oleDbDataReader["f_Sunday"]) == "1");
								this.cboDoors.SelectedIndex = this.arrDoorID.IndexOf(int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_DoorID"])));
								this.cboAccessMethod.SelectedIndex = int.Parse(wgTools.SetObjToStr(oleDbDataReader["f_DoorControl"]));
								this.txtNote.Text = wgTools.SetObjToStr(oleDbDataReader["f_Notes"]);
							}
							oleDbDataReader.Close();
						}
						return;
					}
				}
				this.chk1.Visible = true;
				this.chk2.Visible = true;
				this.chk3.Visible = true;
				this.chk4.Visible = true;
				this.chk5.Visible = true;
				this.chk6.Visible = true;
				this.groupBox1.Enabled = false;
				this.groupBox2.Enabled = false;
				this.groupBox3.Enabled = false;
				this.groupBox4.Enabled = false;
				this.groupBox5.Enabled = false;
				this.groupBox6.Enabled = false;
			}
		}

		private void chk1_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox1.Enabled = (!this.chk1.Visible || this.chk1.Checked);
		}

		private void chk2_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox2.Enabled = (!this.chk2.Visible || this.chk2.Checked);
		}

		private void chk3_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox3.Enabled = (!this.chk3.Visible || this.chk3.Checked);
		}

		private void chk4_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox4.Enabled = (!this.chk4.Visible || this.chk4.Checked);
		}

		private void chk5_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox5.Enabled = (!this.chk5.Visible || this.chk5.Checked);
		}

		private void chk6_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox6.Enabled = (!this.chk6.Visible || this.chk6.Checked);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmControllerTask));
			this.cmdCancel = new Button();
			this.cmdOK = new Button();
			this.txtNote = new TextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.cboAccessMethod = new ComboBox();
			this.label1 = new Label();
			this.cboDoors = new ComboBox();
			this.groupBox3 = new GroupBox();
			this.checkBox49 = new CheckBox();
			this.checkBox48 = new CheckBox();
			this.checkBox47 = new CheckBox();
			this.checkBox46 = new CheckBox();
			this.checkBox45 = new CheckBox();
			this.checkBox44 = new CheckBox();
			this.checkBox43 = new CheckBox();
			this.label45 = new Label();
			this.dtpTime = new DateTimePicker();
			this.label43 = new Label();
			this.label44 = new Label();
			this.dtpEnd = new DateTimePicker();
			this.dtpBegin = new DateTimePicker();
			this.chk5 = new CheckBox();
			this.chk3 = new CheckBox();
			this.chk4 = new CheckBox();
			this.chk1 = new CheckBox();
			this.chk2 = new CheckBox();
			this.groupBox6 = new GroupBox();
			this.groupBox1 = new GroupBox();
			this.groupBox2 = new GroupBox();
			this.groupBox5 = new GroupBox();
			this.groupBox4 = new GroupBox();
			this.txtTaskIDs = new TextBox();
			this.lblTaskID = new Label();
			this.chk6 = new CheckBox();
			this.groupBox3.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.cmdCancel, "cmdCancel");
			this.cmdCancel.BackColor = Color.Transparent;
			this.cmdCancel.BackgroundImage = Resources.pMain_button_normal;
			this.cmdCancel.DialogResult = DialogResult.Cancel;
			this.cmdCancel.ForeColor = Color.White;
			this.cmdCancel.Name = "cmdCancel";
			this.cmdCancel.UseVisualStyleBackColor = false;
			this.cmdCancel.Click += new EventHandler(this.cmdCancel_Click);
			componentResourceManager.ApplyResources(this.cmdOK, "cmdOK");
			this.cmdOK.BackColor = Color.Transparent;
			this.cmdOK.BackgroundImage = Resources.pMain_button_normal;
			this.cmdOK.ForeColor = Color.White;
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new EventHandler(this.cmdOK_Click);
			componentResourceManager.ApplyResources(this.txtNote, "txtNote");
			this.txtNote.Name = "txtNote";
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.BackColor = Color.Transparent;
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.BackColor = Color.Transparent;
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			this.cboAccessMethod.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboAccessMethod.FormattingEnabled = true;
			this.cboAccessMethod.Items.AddRange(new object[]
			{
				componentResourceManager.GetString("cboAccessMethod.Items"),
				componentResourceManager.GetString("cboAccessMethod.Items1"),
				componentResourceManager.GetString("cboAccessMethod.Items2"),
				componentResourceManager.GetString("cboAccessMethod.Items3"),
				componentResourceManager.GetString("cboAccessMethod.Items4"),
				componentResourceManager.GetString("cboAccessMethod.Items5"),
				componentResourceManager.GetString("cboAccessMethod.Items6"),
				componentResourceManager.GetString("cboAccessMethod.Items7"),
				componentResourceManager.GetString("cboAccessMethod.Items8"),
				componentResourceManager.GetString("cboAccessMethod.Items9"),
				componentResourceManager.GetString("cboAccessMethod.Items10")
			});
			componentResourceManager.ApplyResources(this.cboAccessMethod, "cboAccessMethod");
			this.cboAccessMethod.Name = "cboAccessMethod";
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			this.cboDoors.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboDoors.FormattingEnabled = true;
			componentResourceManager.ApplyResources(this.cboDoors, "cboDoors");
			this.cboDoors.Name = "cboDoors";
			this.groupBox3.BackColor = Color.Transparent;
			this.groupBox3.Controls.Add(this.checkBox49);
			this.groupBox3.Controls.Add(this.checkBox48);
			this.groupBox3.Controls.Add(this.checkBox47);
			this.groupBox3.Controls.Add(this.checkBox46);
			this.groupBox3.Controls.Add(this.checkBox45);
			this.groupBox3.Controls.Add(this.checkBox44);
			this.groupBox3.Controls.Add(this.checkBox43);
			this.groupBox3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.checkBox49, "checkBox49");
			this.checkBox49.Checked = true;
			this.checkBox49.CheckState = CheckState.Checked;
			this.checkBox49.Name = "checkBox49";
			this.checkBox49.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox48, "checkBox48");
			this.checkBox48.Checked = true;
			this.checkBox48.CheckState = CheckState.Checked;
			this.checkBox48.Name = "checkBox48";
			this.checkBox48.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox47, "checkBox47");
			this.checkBox47.Checked = true;
			this.checkBox47.CheckState = CheckState.Checked;
			this.checkBox47.Name = "checkBox47";
			this.checkBox47.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox46, "checkBox46");
			this.checkBox46.Checked = true;
			this.checkBox46.CheckState = CheckState.Checked;
			this.checkBox46.Name = "checkBox46";
			this.checkBox46.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox45, "checkBox45");
			this.checkBox45.Checked = true;
			this.checkBox45.CheckState = CheckState.Checked;
			this.checkBox45.Name = "checkBox45";
			this.checkBox45.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox44, "checkBox44");
			this.checkBox44.Checked = true;
			this.checkBox44.CheckState = CheckState.Checked;
			this.checkBox44.Name = "checkBox44";
			this.checkBox44.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.checkBox43, "checkBox43");
			this.checkBox43.Checked = true;
			this.checkBox43.CheckState = CheckState.Checked;
			this.checkBox43.Name = "checkBox43";
			this.checkBox43.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.label45, "label45");
			this.label45.BackColor = Color.Transparent;
			this.label45.ForeColor = Color.White;
			this.label45.Name = "label45";
			componentResourceManager.ApplyResources(this.dtpTime, "dtpTime");
			this.dtpTime.Name = "dtpTime";
			this.dtpTime.ShowUpDown = true;
			this.dtpTime.Value = new DateTime(2011, 11, 30, 0, 0, 0, 0);
			componentResourceManager.ApplyResources(this.label43, "label43");
			this.label43.BackColor = Color.Transparent;
			this.label43.ForeColor = Color.White;
			this.label43.Name = "label43";
			componentResourceManager.ApplyResources(this.label44, "label44");
			this.label44.BackColor = Color.Transparent;
			this.label44.ForeColor = Color.White;
			this.label44.Name = "label44";
			componentResourceManager.ApplyResources(this.dtpEnd, "dtpEnd");
			this.dtpEnd.Name = "dtpEnd";
			this.dtpEnd.Value = new DateTime(2029, 12, 31, 14, 44, 0, 0);
			componentResourceManager.ApplyResources(this.dtpBegin, "dtpBegin");
			this.dtpBegin.Name = "dtpBegin";
			this.dtpBegin.Value = new DateTime(2010, 1, 1, 18, 18, 0, 0);
			this.chk5.BackColor = Color.Transparent;
			this.chk5.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chk5, "chk5");
			this.chk5.Name = "chk5";
			this.chk5.UseVisualStyleBackColor = false;
			this.chk5.CheckedChanged += new EventHandler(this.chk5_CheckedChanged);
			this.chk3.BackColor = Color.Transparent;
			this.chk3.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chk3, "chk3");
			this.chk3.Name = "chk3";
			this.chk3.UseVisualStyleBackColor = false;
			this.chk3.CheckedChanged += new EventHandler(this.chk3_CheckedChanged);
			this.chk4.BackColor = Color.Transparent;
			this.chk4.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chk4, "chk4");
			this.chk4.Name = "chk4";
			this.chk4.UseVisualStyleBackColor = false;
			this.chk4.CheckedChanged += new EventHandler(this.chk4_CheckedChanged);
			this.chk1.BackColor = Color.Transparent;
			this.chk1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chk1, "chk1");
			this.chk1.Name = "chk1";
			this.chk1.UseVisualStyleBackColor = false;
			this.chk1.CheckedChanged += new EventHandler(this.chk1_CheckedChanged);
			this.chk2.BackColor = Color.Transparent;
			this.chk2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chk2, "chk2");
			this.chk2.Name = "chk2";
			this.chk2.UseVisualStyleBackColor = false;
			this.chk2.CheckedChanged += new EventHandler(this.chk2_CheckedChanged);
			this.groupBox6.BackColor = Color.Transparent;
			this.groupBox6.Controls.Add(this.txtNote);
			this.groupBox6.Controls.Add(this.label3);
			this.groupBox6.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox6, "groupBox6");
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.TabStop = false;
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.dtpBegin);
			this.groupBox1.Controls.Add(this.dtpEnd);
			this.groupBox1.Controls.Add(this.label44);
			this.groupBox1.Controls.Add(this.label43);
			this.groupBox1.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.dtpTime);
			this.groupBox2.Controls.Add(this.label45);
			this.groupBox2.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			this.groupBox5.BackColor = Color.Transparent;
			this.groupBox5.Controls.Add(this.cboAccessMethod);
			this.groupBox5.Controls.Add(this.label1);
			this.groupBox5.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox5, "groupBox5");
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.TabStop = false;
			this.groupBox4.BackColor = Color.Transparent;
			this.groupBox4.Controls.Add(this.cboDoors);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.txtTaskIDs, "txtTaskIDs");
			this.txtTaskIDs.Name = "txtTaskIDs";
			this.txtTaskIDs.ReadOnly = true;
			componentResourceManager.ApplyResources(this.lblTaskID, "lblTaskID");
			this.lblTaskID.BackColor = Color.Transparent;
			this.lblTaskID.ForeColor = Color.White;
			this.lblTaskID.Name = "lblTaskID";
			this.chk6.BackColor = Color.Transparent;
			this.chk6.ForeColor = Color.White;
			componentResourceManager.ApplyResources(this.chk6, "chk6");
			this.chk6.Name = "chk6";
			this.chk6.UseVisualStyleBackColor = false;
			this.chk6.CheckedChanged += new EventHandler(this.chk6_CheckedChanged);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.txtTaskIDs);
			base.Controls.Add(this.lblTaskID);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.cmdCancel);
			base.Controls.Add(this.cmdOK);
			base.Controls.Add(this.groupBox4);
			base.Controls.Add(this.groupBox5);
			base.Controls.Add(this.groupBox6);
			base.Controls.Add(this.chk6);
			base.Controls.Add(this.chk5);
			base.Controls.Add(this.chk3);
			base.Controls.Add(this.chk1);
			base.Controls.Add(this.chk2);
			base.Controls.Add(this.chk4);
			base.Name = "dfrmControllerTask";
			base.Load += new EventHandler(this.dfrmControllerTask_Load);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox6.ResumeLayout(false);
			this.groupBox6.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
