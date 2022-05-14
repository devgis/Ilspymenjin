using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.ExtendFunc.PCCheck
{
	public class dfrmPCCheckAccess : frmN3000
	{
		private IContainer components;

		private TextBox txtA0;

		private Label label4;

		internal GroupBox GroupBox1;

		private TextBox txtB0;

		private Label label1;

		private TextBox txtC0;

		private Label label3;

		private Label label2;

		private RichTextBox txtConsumers;

		private TextBox textBox3;

		private Button btnCancel;

		private Timer timer1;

		private SoundPlayer player;

		public bool bDealing;

		public string strGroupname;

		public string strConsumername;

		public string strDoorId;

		public string strDoorFullName;

		public string strNow;

		private string wavfile;

		private DateTime inputCardDate = DateTime.Now;

		private string inputCard = "";

		private icController contr4PCCheckAccess = new icController();

		private DataSet ds = new DataSet();

		private DataView dv;

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.contr4PCCheckAccess != null)
			{
				this.contr4PCCheckAccess.Dispose();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmPCCheckAccess));
			this.timer1 = new Timer(this.components);
			this.btnCancel = new Button();
			this.GroupBox1 = new GroupBox();
			this.txtConsumers = new RichTextBox();
			this.textBox3 = new TextBox();
			this.txtB0 = new TextBox();
			this.label1 = new Label();
			this.txtC0 = new TextBox();
			this.label3 = new Label();
			this.label2 = new Label();
			this.txtA0 = new TextBox();
			this.label4 = new Label();
			this.GroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.timer1.Tick += new EventHandler(this.timer1_Tick);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.BackColor = Color.Transparent;
			this.btnCancel.BackgroundImage = Resources.pMain_button_normal;
			this.btnCancel.DialogResult = DialogResult.Cancel;
			this.btnCancel.ForeColor = Color.White;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = false;
			this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
			componentResourceManager.ApplyResources(this.GroupBox1, "GroupBox1");
			this.GroupBox1.BackColor = Color.Transparent;
			this.GroupBox1.Controls.Add(this.txtConsumers);
			this.GroupBox1.Controls.Add(this.textBox3);
			this.GroupBox1.Controls.Add(this.txtB0);
			this.GroupBox1.Controls.Add(this.label1);
			this.GroupBox1.Controls.Add(this.txtC0);
			this.GroupBox1.Controls.Add(this.label3);
			this.GroupBox1.Controls.Add(this.label2);
			this.GroupBox1.Controls.Add(this.txtA0);
			this.GroupBox1.Controls.Add(this.label4);
			this.GroupBox1.ForeColor = Color.White;
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.TabStop = false;
			componentResourceManager.ApplyResources(this.txtConsumers, "txtConsumers");
			this.txtConsumers.BackColor = Color.White;
			this.txtConsumers.BorderStyle = BorderStyle.None;
			this.txtConsumers.ForeColor = Color.Black;
			this.txtConsumers.Name = "txtConsumers";
			this.txtConsumers.ReadOnly = true;
			componentResourceManager.ApplyResources(this.textBox3, "textBox3");
			this.textBox3.ForeColor = Color.FromArgb(0, 0, 192);
			this.textBox3.Name = "textBox3";
			this.textBox3.TabStop = false;
			componentResourceManager.ApplyResources(this.txtB0, "txtB0");
			this.txtB0.BackColor = Color.White;
			this.txtB0.Name = "txtB0";
			this.txtB0.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.txtC0, "txtC0");
			this.txtC0.BackColor = Color.White;
			this.txtC0.Name = "txtC0";
			this.txtC0.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label3, "label3");
			this.label3.ForeColor = Color.White;
			this.label3.Name = "label3";
			componentResourceManager.ApplyResources(this.label2, "label2");
			this.label2.ForeColor = Color.White;
			this.label2.Name = "label2";
			componentResourceManager.ApplyResources(this.txtA0, "txtA0");
			this.txtA0.BackColor = Color.White;
			this.txtA0.Name = "txtA0";
			this.txtA0.ReadOnly = true;
			componentResourceManager.ApplyResources(this.label4, "label4");
			this.label4.ForeColor = Color.White;
			this.label4.Name = "label4";
			componentResourceManager.ApplyResources(this, "$this");
			base.ControlBox = false;
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.GroupBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmPCCheckAccess";
			base.FormClosed += new FormClosedEventHandler(this.dfrmPCCheckAccess_FormClosed);
			base.Load += new EventHandler(this.dfrmPCCheckAccess_Load);
			base.VisibleChanged += new EventHandler(this.dfrmPCCheckAccess_VisibleChanged);
			base.KeyDown += new KeyEventHandler(this.dfrmPCCheckAccess_KeyDown);
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			base.ResumeLayout(false);
		}

		public dfrmPCCheckAccess()
		{
			this.InitializeComponent();
			this.player = new SoundPlayer();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			this.player.Stop();
			base.Hide();
			this.bDealing = false;
		}

		private void dfrmPCCheckAccess_VisibleChanged(object sender, EventArgs e)
		{
			try
			{
				if (base.Visible && !string.IsNullOrEmpty(this.strGroupname))
				{
					this.txtA0.Text = this.strGroupname;
					this.txtB0.Text = "";
					DateTime dateTime;
					if (DateTime.TryParse(this.strNow, out dateTime))
					{
						this.txtB0.Text = dateTime.ToString("HH:mm:ss");
					}
					this.txtC0.Text = this.strDoorFullName;
					this.txtConsumers.Text = this.strConsumername;
					string cmdText = " SELECT a.f_GroupID,a.f_GroupName,b.f_GroupType,b.f_CheckAccessActive,b.f_MoreCards, b.f_SoundFileName   from t_b_Group a ,t_b_group4PCCheckAccess b where a.f_GroupID = b.f_GroupID and a.f_GroupName =" + wgTools.PrepareStr(this.strGroupname);
					if (wgAppConfig.IsAccessDB)
					{
						using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
						{
							using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
							{
								oleDbConnection.Open();
								OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
								if (oleDbDataReader.Read())
								{
									this.wavfile = oleDbDataReader["f_SoundFileName"].ToString();
								}
								oleDbDataReader.Close();
							}
							goto IL_16A;
						}
					}
					using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
					{
						using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
						{
							sqlConnection.Open();
							SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
							if (sqlDataReader.Read())
							{
								this.wavfile = sqlDataReader["f_SoundFileName"].ToString();
							}
							sqlDataReader.Close();
						}
					}
					IL_16A:
					if (this.wavfile == "")
					{
						this.wavfile = "DoorBell.wav";
					}
					else if (wgAppConfig.FileIsExisted(wgAppConfig.Path4PhotoDefault() + this.wavfile))
					{
						this.player.SoundLocation = wgAppConfig.Path4PhotoDefault() + this.wavfile;
						this.player.PlayLooping();
					}
					this.strGroupname = "";
				}
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[0]);
			}
		}

		private void dfrmPCCheckAccess_KeyDown(object sender, KeyEventArgs e)
		{
			foreach (object current in base.Controls)
			{
				try
				{
					(current as Control).ImeMode = ImeMode.Off;
				}
				catch (Exception)
				{
				}
			}
			if (!e.Control && !e.Alt && !e.Shift && e.KeyValue >= 48 && e.KeyValue <= 57)
			{
				if (this.inputCard.Length == 0)
				{
					this.inputCardDate = DateTime.Now;
					this.timer1.Interval = 500;
					this.timer1.Enabled = true;
				}
				this.inputCard += (e.KeyValue - 48).ToString();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			this.timer1.Enabled = false;
			try
			{
				if (this.inputCard.Length >= 8)
				{
					SystemSounds.Beep.Play();
					this.dv.RowFilter = " f_CardNO = " + this.inputCard.ToString();
					if (this.dv.Count > 0)
					{
						this.contr4PCCheckAccess.GetInfoFromDBByDoorName(this.strDoorFullName);
						if (this.contr4PCCheckAccess.RemoteOpenDoorIP(this.strDoorFullName, (uint)icOperator.OperatorID, ulong.Parse(this.inputCard)) > 0)
						{
							wgRunInfoLog.addEvent(new InfoRow
							{
								desc = string.Format("{0}[{1:d}]", this.strDoorFullName, this.contr4PCCheckAccess.ControllerSN),
								information = string.Format("{0} {1}--[{2}]", this.dv[0]["f_ConsumerName"].ToString(), CommonStr.strSendRemoteOpenDoor, this.strConsumername.Replace("\r\n", ","))
							});
						}
						this.strGroupname = "";
						base.Hide();
						this.player.Stop();
						this.bDealing = false;
					}
					else
					{
						SystemSounds.Beep.Play();
					}
				}
			}
			catch (Exception)
			{
			}
			this.inputCard = "";
		}

		private void dfrmPCCheckAccess_Load(object sender, EventArgs e)
		{
			this.label4.Text = wgAppConfig.ReplaceFloorRomm(this.label4.Text);
			try
			{
				base.ImeMode = ImeMode.Disable;
				this.btnCancel.ImeMode = ImeMode.Disable;
				foreach (object current in base.Controls)
				{
					try
					{
						(current as Control).ImeMode = ImeMode.Off;
					}
					catch (Exception)
					{
					}
				}
				string cmdText = " SELECT a.f_ConsumerName, a.f_ConsumerID, a.f_CardNO from t_b_consumer a ,t_b_group4PCCheckAccess b where a.f_GroupID = b.f_GroupID and b.f_GroupType=1 ";
				if (wgAppConfig.IsAccessDB)
				{
					using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
					{
						using (OleDbCommand oleDbCommand = new OleDbCommand(cmdText, oleDbConnection))
						{
							using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand))
							{
								oleDbDataAdapter.Fill(this.ds, "groups");
							}
						}
						goto IL_133;
					}
				}
				using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
				{
					using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
					{
						using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
						{
							sqlDataAdapter.Fill(this.ds, "groups");
						}
					}
				}
				IL_133:
				this.dv = new DataView(this.ds.Tables["groups"]);
			}
			catch (Exception ex)
			{
				wgTools.WgDebugWrite(ex.ToString(), new object[]
				{
					EventLogEntryType.Error
				});
			}
		}

		private void dfrmPCCheckAccess_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.bDealing = false;
		}
	}
}
