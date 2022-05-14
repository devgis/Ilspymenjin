using System;
using System.Collections;
using System.Collections.Generic;
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
	public class dfrmUserAutoAdd : frmN3000
	{
		private delegate void txtInfoUpdate(object info);

		private const int Mode_USBReader = 1;

		private const int Mode_ControllerReader = 2;

		private const int Mode_ManualInput = 3;

		private ArrayList arrGroupName = new ArrayList();

		private ArrayList arrGroupID = new ArrayList();

		private ArrayList arrGroupNO = new ArrayList();

		private int inputMode;

		private DataView dvDoors;

		private DataView dvDoors4Watching;

		private DataTable dt;

		public bool bAutoAddBySwiping;

		private dfrmWait dfrmWait1 = new dfrmWait();

		public WatchingService watching;

		private bool bDisposeWatching;

		public Form frmCall;

		private int selectedDoorNO;

		private int selectedControllerSN;

		private icController control;

		private string inputCard = "";

		private DataTable dtPrivilege;

		private IContainer components;

		private GroupBox groupBox1;

		private ComboBox cboDoors;

		private RadioButton optController;

		private RadioButton optUSBReader;

		private Button btnNext;

		private Button btnCancel;

		private ComboBox cbof_GroupID;

		private Label label4;

		private GroupBox groupBox2;

		private Label label2;

		private Label label1;

		private ListBox lstSwipe;

		private Button btnExit;

		private Button btnOK;

		private GroupBox groupBox3;

		private MaskedTextBox txtEndNO;

		private MaskedTextBox txtStartNO;

		private Label label3;

		private RadioButton optManualInput;

		private Timer timer1;

		private Label lblInfo;

		private Label lblCount;

		private Label label5;

		private Button btnDirectGetFromtheController;

		private GroupBox groupBox4;

		private NumericUpDown nudNOLength;

		private CheckBox chkConst;

		private Label label6;

		private TextBox txtNOStartCaption;

		private CheckBox chkOption;

		private Button button1;

		public dfrmUserAutoAdd()
		{
			this.InitializeComponent();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnCancel2_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			int num = -1;
			Cursor.Current = Cursors.WaitCursor;
			if (this.inputMode == 3)
			{
				num = this._manualInput();
			}
			else if (this.inputMode == 1 || this.inputMode == 2)
			{
				num = this.usbReaderInput();
			}
			Cursor.Current = Cursors.Default;
			if (num >= 0)
			{
				try
				{
					string text = wgAppConfig.GetKeyVal("UserAutoAddSet");
					if (this.chkOption.Checked)
					{
						if (this.chkConst.Checked)
						{
							text = this.nudNOLength.Value.ToString();
						}
						else
						{
							text = "0";
						}
						text = text + "," + this.txtNOStartCaption.Text;
					}
					wgAppConfig.UpdateKeyVal("UserAutoAddSet", text);
				}
				catch (Exception)
				{
				}
				base.DialogResult = DialogResult.OK;
				icConsumerShare.setUpdateLog();
				base.Close();
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.lstSwipe.Items.Clear();
			this.lblInfo.Text = "";
			this.lblCount.Text = "";
			try
			{
				string keyVal = wgAppConfig.GetKeyVal("UserAutoAddSet");
				if (!string.IsNullOrEmpty(keyVal) && keyVal.IndexOf(",") > 0)
				{
					string s = keyVal.Substring(0, keyVal.IndexOf(","));
					string objToStr = keyVal.Substring(keyVal.IndexOf(",") + 1);
					if (int.Parse(s) > 0)
					{
						this.chkConst.Checked = true;
						this.nudNOLength.Value = int.Parse(s);
						this.nudNOLength.Enabled = true;
					}
					this.txtNOStartCaption.Text = wgTools.SetObjToStr(objToStr);
					this.chkOption.Checked = true;
					this.groupBox4.Visible = true;
				}
			}
			catch (Exception)
			{
			}
			if (this.optController.Checked && string.IsNullOrEmpty(this.cboDoors.Text))
			{
				return;
			}
			if (this.optManualInput.Checked)
			{
				this.label3.Visible = false;
				this.groupBox3.Visible = true;
				this.inputMode = 3;
			}
			else
			{
				this.label3.Visible = true;
				this.groupBox3.Visible = false;
				if (this.optUSBReader.Checked)
				{
					this.inputMode = 1;
				}
				else
				{
					this.inputMode = 2;
					this.controllerReaderInput();
				}
			}
			icGroup icGroup = new icGroup();
			icGroup.getGroup(ref this.arrGroupName, ref this.arrGroupID, ref this.arrGroupNO);
			int i = this.arrGroupID.Count;
			for (i = 0; i < this.arrGroupID.Count; i++)
			{
				this.cbof_GroupID.Items.Add(this.arrGroupName[i].ToString());
			}
			if (this.cbof_GroupID.Items.Count > 0)
			{
				this.cbof_GroupID.SelectedIndex = 0;
			}
			this.groupBox2.Location = new Point(this.groupBox1.Location.X, this.groupBox1.Location.Y);
			this.groupBox2.Visible = true;
			if (wgAppConfig.IsChineseSet(wgTools.SetObjToStr(wgAppConfig.GetKeyVal("Language"))))
			{
				base.Size = new Size(base.Size.Width, 360);
				return;
			}
			base.Size = new Size(base.Size.Width, 380);
		}

		private void loadDoorData()
		{
			if (wgAppConfig.IsAccessDB)
			{
				this.loadDoorData_Acc();
				return;
			}
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				using (SqlCommand sqlCommand = new SqlCommand(text, sqlConnection))
				{
					using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
					{
						this.dt = new DataTable();
						this.dvDoors = new DataView(this.dt);
						this.dvDoors4Watching = new DataView(this.dt);
						sqlDataAdapter.Fill(this.dt);
						icControllerZone icControllerZone = new icControllerZone();
						icControllerZone.getAllowedControllers(ref this.dt);
						try
						{
							DataColumn[] primaryKey = new DataColumn[]
							{
								this.dt.Columns[0]
							};
							this.dt.PrimaryKey = primaryKey;
						}
						catch (Exception ex)
						{
							wgAppConfig.wgLog(ex.ToString());
						}
						this.cboDoors.Items.Clear();
						if (this.dvDoors.Count > 0)
						{
							for (int i = 0; i < this.dvDoors.Count; i++)
							{
								this.cboDoors.Items.Add(wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]));
							}
							if (this.cboDoors.Items.Count > 0)
							{
								this.cboDoors.SelectedIndex = 0;
							}
						}
					}
				}
			}
		}

		private void loadDoorData_Acc()
		{
			string text = " SELECT a.f_DoorID, a.f_DoorName , a.f_DoorNO, b.f_ControllerSN, b.f_IP,b.f_PORT, 0 as f_ConnectState, b.f_ZoneID ";
			text += " FROM t_b_Door a, t_b_Controller b WHERE a.f_DoorEnabled > 0 and b.f_Enabled >0 and a.f_ControllerID=b.f_ControllerID ";
			text += " ORDER BY  a.f_DoorName ";
			using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(text, oleDbConnection))
				{
					using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
					{
						this.dt = new DataTable();
						this.dvDoors = new DataView(this.dt);
						this.dvDoors4Watching = new DataView(this.dt);
						oleDbDataAdapter.Fill(this.dt);
						icControllerZone icControllerZone = new icControllerZone();
						icControllerZone.getAllowedControllers(ref this.dt);
						try
						{
							DataColumn[] primaryKey = new DataColumn[]
							{
								this.dt.Columns[0]
							};
							this.dt.PrimaryKey = primaryKey;
						}
						catch (Exception ex)
						{
							wgAppConfig.wgLog(ex.ToString());
						}
						this.cboDoors.Items.Clear();
						if (this.dvDoors.Count > 0)
						{
							for (int i = 0; i < this.dvDoors.Count; i++)
							{
								this.cboDoors.Items.Add(wgTools.SetObjToStr(this.dvDoors[i]["f_DoorName"]));
							}
							if (this.cboDoors.Items.Count > 0)
							{
								this.cboDoors.SelectedIndex = 0;
							}
						}
					}
				}
			}
		}

		private void dfrmUserAutoAdd_Load(object sender, EventArgs e)
		{
			this.txtStartNO.Mask = "9999999999";
			this.txtEndNO.Mask = "9999999999";
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			this.chkOption.Text = wgAppConfig.ReplaceWorkNO(this.chkOption.Text);
			this.txtNOStartCaption.Text = wgAppConfig.ReplaceWorkNO(this.txtNOStartCaption.Text);
			this.loadDoorData();
			if (this.bAutoAddBySwiping && this.dvDoors.Count > 0)
			{
				int num = -1;
				bool flag = true;
				for (int i = 0; i < this.dvDoors.Count; i++)
				{
					if (num == -1)
					{
						num = (int)this.dvDoors[i]["f_ControllerSN"];
					}
					else if (num != (int)this.dvDoors[i]["f_ControllerSN"])
					{
						flag = false;
						break;
					}
				}
				this.optController.Checked = true;
				if (flag)
				{
					this.btnNext.PerformClick();
				}
			}
		}

		private int _manualInput()
		{
			int result = -1;
			if (string.IsNullOrEmpty(this.txtStartNO.Text) || string.IsNullOrEmpty(this.txtEndNO.Text))
			{
				XMessageBox.Show(this, CommonStr.strCheckCard, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return result;
			}
			long num = long.Parse(this.txtStartNO.Text);
			long num2 = long.Parse(this.txtEndNO.Text);
			if (num <= 0L || num2 <= 0L)
			{
				XMessageBox.Show(this, CommonStr.strCheckCard, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return result;
			}
			if (XMessageBox.Show(this, string.Format(CommonStr.strAreYouSureAutoAddCard + ": {0:d}--{1:d} [{2:d}] ?", num, num2, (num2 - num + 1L > 0L) ? (num2 - num + 1L) : 1L), wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
			{
				return result;
			}
			icConsumer icConsumer = new icConsumer();
			int groupID = int.Parse(this.arrGroupID[this.cbof_GroupID.SelectedIndex].ToString());
			string text = "";
			if (this.chkOption.Checked && this.txtNOStartCaption.Text.Trim().Length > 0)
			{
				text = this.txtNOStartCaption.Text;
			}
			long num3 = icConsumer.ConsumerNONext(text);
			if (num3 < 0L)
			{
				XMessageBox.Show(this, wgAppConfig.ReplaceWorkNO(CommonStr.strAutoAddCardErrConsumerNO), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return result;
			}
			int num4 = 0;
			if (num2 - num + 1L > 10L)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			for (long num5 = num; num5 <= num2; num5 += 1L)
			{
				string text2;
				if (string.IsNullOrEmpty(text))
				{
					text2 = num3.ToString();
				}
				else if (this.chkConst.Checked && this.nudNOLength.Value - this.txtNOStartCaption.Text.Length > 0m)
				{
					text2 = string.Format("{0}{1}", text, num3.ToString().PadLeft((int)(this.nudNOLength.Value - this.txtNOStartCaption.Text.Length), '0'));
				}
				else
				{
					text2 = string.Format("{0}{1}", text, num3.ToString());
				}
				int num6 = icConsumer.addNew(text2.ToString(), "N" + num5.ToString(), groupID, 1, 0, 1, DateTime.Now, DateTime.Parse("2029-12-31"), 345678, num5);
				if (num6 >= 0)
				{
					num3 += 1L;
					num4++;
				}
				if (num4 % 100 == 0)
				{
					wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1:d}", CommonStr.strAutoAddCard, num4));
					Application.DoEvents();
				}
			}
			wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1:d}", CommonStr.strAutoAddCard, num4));
			this.dfrmWait1.Hide();
			this.dfrmWait1.Refresh();
			Application.DoEvents();
			XMessageBox.Show(this, CommonStr.strAutoAddCard + "\r\n\r\n" + num4.ToString(), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 1;
		}

		private int usbReaderInput()
		{
			int result = -1;
			if (this.lstSwipe.Items.Count <= 0)
			{
				return result;
			}
			icConsumer icConsumer = new icConsumer();
			int groupID = int.Parse(this.arrGroupID[this.cbof_GroupID.SelectedIndex].ToString());
			string text = "";
			if (this.chkOption.Checked && this.txtNOStartCaption.Text.Trim().Length > 0)
			{
				text = this.txtNOStartCaption.Text;
			}
			long num = icConsumer.ConsumerNONext(text);
			int num2 = 0;
			if (num < 0L)
			{
				XMessageBox.Show(this, wgAppConfig.ReplaceWorkNO(CommonStr.strAutoAddCardErrConsumerNO), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return result;
			}
			if (this.lstSwipe.Items.Count > 10)
			{
				this.dfrmWait1.Show();
				this.dfrmWait1.Refresh();
			}
			for (int i = 0; i < this.lstSwipe.Items.Count; i++)
			{
				int num3;
				if (this.lstSwipe.Items[i].ToString().IndexOf("_") <= 0)
				{
					long cardNO = long.Parse(this.lstSwipe.Items[i].ToString());
					string text2;
					if (string.IsNullOrEmpty(text))
					{
						text2 = num.ToString();
					}
					else if (this.chkConst.Checked && this.nudNOLength.Value - this.txtNOStartCaption.Text.Length > 0m)
					{
						text2 = string.Format("{0}{1}", text, num.ToString().PadLeft((int)(this.nudNOLength.Value - this.txtNOStartCaption.Text.Length), '0'));
					}
					else
					{
						text2 = string.Format("{0}{1}", text, num.ToString());
					}
					num3 = icConsumer.addNew(text2.ToString(), "N" + cardNO.ToString(), groupID, 1, 0, 1, DateTime.Now, DateTime.Parse("2029-12-31"), 345678, cardNO);
				}
				else
				{
					long cardNO = long.Parse(this.lstSwipe.Items[i].ToString().Substring(0, this.lstSwipe.Items[i].ToString().IndexOf("_")));
					string text3 = this.lstSwipe.Items[i].ToString().Substring(this.lstSwipe.Items[i].ToString().IndexOf("_") + 1);
					if (string.IsNullOrEmpty(text3))
					{
						text3 = "N" + cardNO.ToString();
					}
					num3 = icConsumer.addNew(num.ToString(), text3, groupID, 1, 0, 1, DateTime.Now, DateTime.Parse("2029-12-31"), 345678, cardNO);
				}
				if (num3 >= 0)
				{
					num += 1L;
					num2++;
				}
				if (num2 % 100 == 0)
				{
					wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1:d}", CommonStr.strAutoAddCard, num2));
					Application.DoEvents();
				}
			}
			wgAppRunInfo.raiseAppRunInfoCommStatus(string.Format("{0}: {1:d}", CommonStr.strAutoAddCard, num2));
			this.dfrmWait1.Hide();
			this.dfrmWait1.Refresh();
			Application.DoEvents();
			XMessageBox.Show(this, CommonStr.strAutoAddCard + "\r\n\r\n" + num2.ToString(), wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return 1;
		}

		private void evtNewInfoCallBack(string text)
		{
			wgTools.WgDebugWrite("Got text through callback! {0}", new object[]
			{
				text
			});
			this.lstSwipe.Invoke(new dfrmUserAutoAdd.txtInfoUpdate(this.txtInfoUpdateEntry), new object[]
			{
				text
			});
		}

		private void txtInfoUpdateEntry(object info)
		{
			MjRec mjRec = new MjRec(info as string);
			if (mjRec.ControllerSN > 0u && (ulong)mjRec.ControllerSN == (ulong)((long)this.selectedControllerSN))
			{
				InfoRow infoRow = new InfoRow();
				infoRow.category = mjRec.eventCategory;
				infoRow.desc = "";
				if (mjRec.IsSwipeRecord)
				{
					bool flag = false;
					string text = mjRec.CardID.ToString();
					foreach (object current in this.lstSwipe.Items)
					{
						if (current as string == text)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						this.lstSwipe.Items.Add(text);
						this.lblInfo.Text = text;
					}
					else
					{
						this.lblInfo.Text = text + CommonStr.strCardNOIsAdded;
					}
					this.lblCount.Text = this.lstSwipe.Items.Count.ToString();
				}
			}
		}

		private void controllerReaderInput()
		{
			if (this.watching == null)
			{
				if (this.frmCall == null)
				{
					this.watching = new WatchingService();
				}
				else
				{
					(this.frmCall as frmUsers).startWatch();
					this.watching = (this.frmCall as frmUsers).watching;
				}
			}
			this.watching.EventHandler += new OnEventHandler(this.evtNewInfoCallBack);
			Dictionary<int, icController> dictionary = new Dictionary<int, icController>();
			this.control = new icController();
			this.control.GetInfoFromDBByDoorName(this.cboDoors.Text);
			this.dvDoors4Watching.RowFilter = "f_DoorName = " + wgTools.PrepareStr(this.cboDoors.Text);
			this.selectedDoorNO = int.Parse(this.dvDoors4Watching[0]["f_DoorNO"].ToString());
			this.selectedControllerSN = this.control.ControllerSN;
			dictionary.Add(this.control.ControllerSN, this.control);
			this.watching.WatchingController = dictionary;
		}

		private void dfrmUserAutoAdd_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.inputMode == 1)
			{
				foreach (object current in base.Controls)
				{
					try
					{
						(current as Control).ImeMode = ImeMode.Off;
					}
					catch
					{
					}
				}
				if (!e.Control && !e.Alt && !e.Shift && e.KeyValue >= 48 && e.KeyValue <= 57)
				{
					if (this.inputCard.Length == 0)
					{
						this.timer1.Interval = 500;
						this.timer1.Enabled = true;
					}
					this.inputCard += (e.KeyValue - 48).ToString();
					return;
				}
			}
			else
			{
				if (2 == this.inputMode && e.KeyValue == 81 && e.Control && e.Shift)
				{
					this.btnDirectGetFromtheController.Visible = true;
				}
				if (e.KeyValue == 67 && e.Control)
				{
					string text = "";
					for (int i = 0; i < this.lstSwipe.Items.Count; i++)
					{
						text += this.lstSwipe.Items[i].ToString();
						text += "\r\n";
					}
					Clipboard.SetDataObject(text, false);
				}
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			if (this.inputCard.Length >= 8)
			{
				bool flag = false;
				foreach (object current in this.lstSwipe.Items)
				{
					if (current as string == this.inputCard)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.lstSwipe.Items.Add(this.inputCard);
					this.lblInfo.Text = this.inputCard;
				}
				else
				{
					this.lblInfo.Text = this.inputCard + CommonStr.strCardNOIsAdded;
				}
				this.lblCount.Text = this.lstSwipe.Items.Count.ToString();
			}
			this.inputCard = "";
		}

		private void txtStartNO_KeyPress(object sender, KeyPressEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtStartNO);
		}

		private void txtEndNO_KeyPress(object sender, KeyPressEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtEndNO);
		}

		private void txtStartNO_KeyUp(object sender, KeyEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtStartNO);
		}

		private void txtEndNO_KeyUp(object sender, KeyEventArgs e)
		{
			wgAppConfig.CardIDInput(ref this.txtEndNO);
		}

		private void dfrmUserAutoAdd_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.watching != null)
			{
				this.watching.EventHandler -= new OnEventHandler(this.evtNewInfoCallBack);
				if (this.frmCall == null)
				{
					this.watching.StopWatch();
				}
			}
			try
			{
				if (this.dfrmWait1 != null)
				{
					this.dfrmWait1.Close();
				}
			}
			catch (Exception)
			{
			}
		}

		private void optController_CheckedChanged(object sender, EventArgs e)
		{
			this.cboDoors.Enabled = this.optController.Checked;
		}

		private void btnDirectGetFromtheController_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.watching != null)
				{
					this.watching.EventHandler -= new OnEventHandler(this.evtNewInfoCallBack);
					this.watching.StopWatch();
				}
				this.watching = null;
			}
			catch (Exception)
			{
			}
			this.btnDirectGetFromtheController.Enabled = false;
			Cursor.Current = Cursors.WaitCursor;
			using (icController icController = new icController())
			{
				using (wgMjControllerPrivilege wgMjControllerPrivilege = new wgMjControllerPrivilege())
				{
					icController.GetInfoFromDBByDoorName(this.cboDoors.Text);
					wgMjControllerPrivilege.AllowDownload();
					if (this.dtPrivilege != null)
					{
						this.dtPrivilege.Rows.Clear();
						this.dtPrivilege.Dispose();
						this.dtPrivilege = null;
						GC.Collect();
					}
					this.dtPrivilege = new DataTable("Privilege");
					this.dtPrivilege.Columns.Add("f_CardNO", Type.GetType("System.UInt32"));
					this.dtPrivilege.Columns.Add("f_BeginYMD", Type.GetType("System.DateTime"));
					this.dtPrivilege.Columns.Add("f_EndYMD", Type.GetType("System.DateTime"));
					this.dtPrivilege.Columns.Add("f_PIN", Type.GetType("System.String"));
					this.dtPrivilege.Columns.Add("f_ControlSegID1", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID1"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID2", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID2"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID3", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID3"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ControlSegID4", Type.GetType("System.Byte"));
					this.dtPrivilege.Columns["f_ControlSegID4"].DefaultValue = 0;
					this.dtPrivilege.Columns.Add("f_ConsumerName", Type.GetType("System.String"));
					this.label3.Text = this.btnDirectGetFromtheController.Text + " ...";
					wgAppConfig.wgLog(this.btnDirectGetFromtheController.Text + " Start");
					this.Refresh();
					if (wgMjControllerPrivilege.DownloadIP(icController.ControllerSN, icController.IP, icController.PORT, "", ref this.dtPrivilege) > 0)
					{
						if (this.dtPrivilege.Rows.Count >= 0)
						{
							this.lblCount.Text = (this.lstSwipe.Items.Count + this.dtPrivilege.Rows.Count).ToString();
							wgAppConfig.wgLog(this.btnDirectGetFromtheController.Text + " Complete");
							this.label3.Text = CommonStr.strSuccessfully;
							this.Refresh();
							for (int i = 0; i < this.dtPrivilege.Rows.Count; i++)
							{
								if (string.IsNullOrEmpty(wgTools.SetObjToStr(this.dtPrivilege.Rows[i]["f_ConsumerName"])))
								{
									this.lstSwipe.Items.Add(this.dtPrivilege.Rows[i]["f_CardNO"].ToString());
								}
								else
								{
									this.lstSwipe.Items.Add(string.Format("{0}_{1}", this.dtPrivilege.Rows[i]["f_CardNO"].ToString(), wgTools.SetObjToStr(this.dtPrivilege.Rows[i]["f_ConsumerName"])));
								}
							}
						}
						else
						{
							this.label3.Text = CommonStr.strCommFail;
							wgAppConfig.wgLog(this.btnDirectGetFromtheController.Text + " Failed");
						}
					}
					else
					{
						this.label3.Text = CommonStr.strCommFail;
						wgAppConfig.wgLog(this.btnDirectGetFromtheController.Text + " Failed");
					}
					Cursor.Current = Cursors.Default;
					this.btnDirectGetFromtheController.Enabled = true;
				}
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.nudNOLength.Enabled = this.chkConst.Checked;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			this.groupBox4.Visible = this.chkOption.Checked;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.groupBox2.Visible = false;
			base.Size = new Size(base.Size.Width, 266);
			if (this.optController.Checked)
			{
				try
				{
					this.watching.EventHandler -= new OnEventHandler(this.evtNewInfoCallBack);
				}
				catch
				{
				}
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.dfrmWait1 != null)
			{
				this.dfrmWait1.Dispose();
			}
			if (disposing && this.control != null)
			{
				this.control.Dispose();
			}
			if (this.bDisposeWatching && disposing && this.watching != null)
			{
				this.watching.Dispose();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmUserAutoAdd));
			this.groupBox1 = new GroupBox();
			this.optManualInput = new RadioButton();
			this.cboDoors = new ComboBox();
			this.optController = new RadioButton();
			this.optUSBReader = new RadioButton();
			this.btnNext = new Button();
			this.btnCancel = new Button();
			this.cbof_GroupID = new ComboBox();
			this.label4 = new Label();
			this.groupBox2 = new GroupBox();
			this.chkOption = new CheckBox();
			this.label3 = new Label();
			this.groupBox4 = new GroupBox();
			this.txtNOStartCaption = new TextBox();
			this.nudNOLength = new NumericUpDown();
			this.chkConst = new CheckBox();
			this.label6 = new Label();
			this.btnDirectGetFromtheController = new Button();
			this.lblCount = new Label();
			this.lblInfo = new Label();
			this.label5 = new Label();
			this.groupBox3 = new GroupBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.txtEndNO = new MaskedTextBox();
			this.txtStartNO = new MaskedTextBox();
			this.lstSwipe = new ListBox();
			this.btnExit = new Button();
			this.btnOK = new Button();
			this.timer1 = new Timer(this.components);
			this.button1 = new Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox4.SuspendLayout();
			((ISupportInitialize)this.nudNOLength).BeginInit();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.BackColor = Color.Transparent;
			this.groupBox1.Controls.Add(this.optManualInput);
			this.groupBox1.Controls.Add(this.cboDoors);
			this.groupBox1.Controls.Add(this.optController);
			this.groupBox1.Controls.Add(this.optUSBReader);
			this.groupBox1.ForeColor = Color.White;
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.optManualInput, "optManualInput");
			this.optManualInput.Name = "optManualInput";
			this.optManualInput.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.cboDoors, "cboDoors");
			this.cboDoors.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cboDoors.FormattingEnabled = true;
			this.cboDoors.Name = "cboDoors";
			componentResourceManager.ApplyResources(this.optController, "optController");
			this.optController.Name = "optController";
			this.optController.UseVisualStyleBackColor = true;
			this.optController.CheckedChanged += new EventHandler(this.optController_CheckedChanged);
			componentResourceManager.ApplyResources(this.optUSBReader, "optUSBReader");
			this.optUSBReader.Checked = true;
			this.optUSBReader.Name = "optUSBReader";
			this.optUSBReader.TabStop = true;
			this.optUSBReader.UseVisualStyleBackColor = true;
			componentResourceManager.ApplyResources(this.btnNext, "btnNext");
			this.btnNext.BackColor = Color.Transparent;
			this.btnNext.BackgroundImage = Resources.pMain_button_normal;
			this.btnNext.ForeColor = Color.White;
			this.btnNext.Name = "btnNext";
			this.btnNext.UseVisualStyleBackColor = false;
			this.btnNext.Click += new EventHandler(this.btnNext_Click);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.cbof_GroupID, "cbof_GroupID");
			this.cbof_GroupID.DropDownStyle = ComboBoxStyle.DropDownList;
			this.cbof_GroupID.FormattingEnabled = true;
			this.cbof_GroupID.Name = "cbof_GroupID";
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.chkOption);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.groupBox4);
			this.groupBox2.Controls.Add(this.btnDirectGetFromtheController);
			this.groupBox2.Controls.Add(this.lblCount);
			this.groupBox2.Controls.Add(this.lblInfo);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.groupBox3);
			this.groupBox2.Controls.Add(this.lstSwipe);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cbof_GroupID);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.chkOption, "chkOption");
			this.chkOption.Name = "chkOption";
			this.chkOption.UseVisualStyleBackColor = true;
			this.chkOption.CheckedChanged += new EventHandler(this.checkBox2_CheckedChanged);
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.groupBox4, "groupBox4");
			this.groupBox4.Controls.Add(this.txtNOStartCaption);
			this.groupBox4.Controls.Add(this.nudNOLength);
			this.groupBox4.Controls.Add(this.chkConst);
			this.groupBox4.Controls.Add(this.label6);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.TabStop = false;
			componentResourceManager.ApplyResources(this.txtNOStartCaption, "txtNOStartCaption");
			this.txtNOStartCaption.Name = "txtNOStartCaption";
			componentResourceManager.ApplyResources(this.nudNOLength, "nudNOLength");
			NumericUpDown arg_6FC_0 = this.nudNOLength;
			int[] array = new int[4];
			array[0] = 20;
			arg_6FC_0.Maximum = new decimal(array);
			NumericUpDown arg_718_0 = this.nudNOLength;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_718_0.Minimum = new decimal(array2);
			this.nudNOLength.Name = "nudNOLength";
			NumericUpDown arg_744_0 = this.nudNOLength;
			int[] array3 = new int[4];
			array3[0] = 8;
			arg_744_0.Value = new decimal(array3);
			componentResourceManager.ApplyResources(this.chkConst, "chkConst");
			this.chkConst.Name = "chkConst";
			this.chkConst.UseVisualStyleBackColor = true;
			this.chkConst.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
			componentResourceManager.ApplyResources(this.label6, "label6");
			this.label6.Name = "label6";
			componentResourceManager.ApplyResources(this.btnDirectGetFromtheController, "btnDirectGetFromtheController");
			this.btnDirectGetFromtheController.BackColor = Color.Transparent;
			this.btnDirectGetFromtheController.BackgroundImage = Resources.pMain_button_normal;
			this.btnDirectGetFromtheController.ForeColor = Color.White;
			this.btnDirectGetFromtheController.Name = "btnDirectGetFromtheController";
			this.btnDirectGetFromtheController.UseVisualStyleBackColor = false;
			this.btnDirectGetFromtheController.Click += new EventHandler(this.btnDirectGetFromtheController_Click);
			componentResourceManager.ApplyResources(this.lblCount, "lblCount");
			this.lblCount.Name = "lblCount";
			componentResourceManager.ApplyResources(this.lblInfo, "lblInfo");
			this.lblInfo.Name = "lblInfo";
			componentResourceManager.ApplyResources(this.label5, "label5");
			this.label5.Name = "label5";
			componentResourceManager.ApplyResources(this.groupBox3, "groupBox3");
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.txtEndNO);
			this.groupBox3.Controls.Add(this.txtStartNO);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.txtEndNO, "txtEndNO");
			this.txtEndNO.Name = "txtEndNO";
			this.txtEndNO.KeyPress += new KeyPressEventHandler(this.txtEndNO_KeyPress);
			this.txtEndNO.KeyUp += new KeyEventHandler(this.txtEndNO_KeyUp);
			componentResourceManager.ApplyResources(this.txtStartNO, "txtStartNO");
			this.txtStartNO.Name = "txtStartNO";
			this.txtStartNO.KeyPress += new KeyPressEventHandler(this.txtStartNO_KeyPress);
			this.txtStartNO.KeyUp += new KeyEventHandler(this.txtStartNO_KeyUp);
			componentResourceManager.ApplyResources(this.lstSwipe, "lstSwipe");
			this.lstSwipe.FormattingEnabled = true;
			this.lstSwipe.Name = "lstSwipe";
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnCancel2_Click);
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.BackColor = Color.Transparent;
			this.btnOK.BackgroundImage = Resources.pMain_button_normal;
			this.btnOK.ForeColor = Color.White;
			this.btnOK.Name = "btnOK";
			this.btnOK.UseVisualStyleBackColor = false;
			this.btnOK.Click += new EventHandler(this.btnOK_Click);
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.button1, "button1");
			this.button1.BackColor = Color.Transparent;
			this.button1.BackgroundImage = Resources.pMain_button_normal;
			this.button1.ForeColor = Color.White;
			this.button1.Name = "button1";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.button1);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnNext);
			base.Controls.Add(this.groupBox1);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmUserAutoAdd";
			base.FormClosing += new FormClosingEventHandler(this.dfrmUserAutoAdd_FormClosing);
			base.Load += new EventHandler(this.dfrmUserAutoAdd_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmUserAutoAdd_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			((ISupportInitialize)this.nudNOLength).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
