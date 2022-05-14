using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.DataOper;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmDeleteRecords : frmN3000
	{
		private IContainer components;

		private Label label1;

		private Button btnExit;

		private Button btnDeleteAllSwipeRecords;

		private Button btnDeleteLog;

		private Button btnDeleteOldSwipeRecords;

		private GroupBox groupBox2;

		private Label lblIndex;

		internal NumericUpDown nudSwipeRecordIndex;

		private Button btnBackupDatabase;

		internal NumericUpDown nudIndexMin;

		private Label lblIndexMin;

		public dfrmDeleteRecords()
		{
			this.InitializeComponent();
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		private void btnDeleteAllSwipeRecords_Click(object sender, EventArgs e)
		{
			if (XMessageBox.Show((sender as Button).Text + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			try
			{
				if (wgAppConfig.IsAccessDB)
				{
					wgAppConfig.runUpdateSql("Delete From t_d_SwipeRecord");
				}
				else
				{
					wgAppConfig.runUpdateSql("TRUNCATE TABLE t_d_SwipeRecord");
				}
				wgAppConfig.wgLog((sender as Button).Text);
				XMessageBox.Show(this.Text + CommonStr.strSuccessfully);
				base.Close();
			}
			catch (Exception)
			{
				XMessageBox.Show(this.Text + CommonStr.strFailed);
			}
		}

		private void btnDeleteOldSwipeRecords_Click(object sender, EventArgs e)
		{
			string text = "";
			text = (sender as Button).Text + ": " + this.lblIndex.Text + this.nudSwipeRecordIndex.Value.ToString();
			if (this.nudIndexMin.Visible)
			{
				text = text + " ,   " + this.lblIndexMin.Text + this.nudIndexMin.Value.ToString();
			}
			text += "? ";
			if (XMessageBox.Show(text, wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			int num = -1;
			Cursor.Current = Cursors.WaitCursor;
			string text2 = "DELETE FROM t_d_SwipeRecord Where f_RecID <" + this.nudSwipeRecordIndex.Value.ToString();
			if (this.nudIndexMin.Visible)
			{
				text2 = text2 + "  AND  f_RecID >= " + this.nudIndexMin.Value.ToString();
			}
			if (wgAppConfig.IsAccessDB)
			{
				using (OleDbConnection oleDbConnection = new OleDbConnection(wgAppConfig.dbConString))
				{
					if (oleDbConnection.State != ConnectionState.Open)
					{
						oleDbConnection.Open();
					}
					using (OleDbCommand oleDbCommand = new OleDbCommand(text2, oleDbConnection))
					{
						oleDbCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
						num = oleDbCommand.ExecuteNonQuery();
					}
					goto IL_198;
				}
			}
			using (SqlConnection sqlConnection = new SqlConnection(wgAppConfig.dbConString))
			{
				if (sqlConnection.State != ConnectionState.Open)
				{
					sqlConnection.Open();
				}
				using (SqlCommand sqlCommand = new SqlCommand(text2, sqlConnection))
				{
					sqlCommand.CommandTimeout = wgAppConfig.dbCommandTimeout;
					num = sqlCommand.ExecuteNonQuery();
				}
			}
			IL_198:
			Cursor.Current = Cursors.Default;
			if (num >= 0)
			{
				wgAppConfig.wgLog((sender as Button).Text + ": " + text2);
				wgAppConfig.wgLogWithoutDB(text + "\r\n" + text2, EventLogEntryType.Information, null);
				XMessageBox.Show(this.Text + CommonStr.strSuccessfully);
				base.Close();
				return;
			}
			XMessageBox.Show(this.Text + CommonStr.strFailed);
		}

		private void btnDeleteLog_Click(object sender, EventArgs e)
		{
			if (XMessageBox.Show((sender as Button).Text + "? ", wgTools.MSGTITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
			{
				return;
			}
			try
			{
				if (wgAppConfig.IsAccessDB)
				{
					wgAppConfig.runUpdateSql("Delete From t_s_wglog");
				}
				else
				{
					wgAppConfig.runUpdateSql("TRUNCATE TABLE t_s_wglog");
				}
				wgAppConfig.wgLog((sender as Button).Text);
				XMessageBox.Show(this.Text + CommonStr.strSuccessfully);
				base.Close();
			}
			catch (Exception)
			{
				XMessageBox.Show(this.Text + CommonStr.strFailed);
			}
		}

		private void btnBackupDatabase_Click(object sender, EventArgs e)
		{
			using (dfrmDbCompact dfrmDbCompact = new dfrmDbCompact())
			{
				dfrmDbCompact.ShowDialog(this);
			}
		}

		private void dfrmDeleteRecords_Load(object sender, EventArgs e)
		{
		}

		private void dfrmDeleteRecords_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.Shift && e.KeyValue == 81)
			{
				if (icOperator.OperatorID != 1)
				{
					XMessageBox.Show(this, CommonStr.strNeedSuperPrivilege, wgTools.MSGTITLE, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				this.lblIndexMin.Visible = true;
				this.nudIndexMin.Visible = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmDeleteRecords));
			this.label1 = new Label();
			this.btnExit = new Button();
			this.btnDeleteAllSwipeRecords = new Button();
			this.btnDeleteLog = new Button();
			this.btnDeleteOldSwipeRecords = new Button();
			this.groupBox2 = new GroupBox();
			this.nudIndexMin = new NumericUpDown();
			this.lblIndexMin = new Label();
			this.nudSwipeRecordIndex = new NumericUpDown();
			this.lblIndex = new Label();
			this.btnBackupDatabase = new Button();
			this.groupBox2.SuspendLayout();
			((ISupportInitialize)this.nudIndexMin).BeginInit();
			((ISupportInitialize)this.nudSwipeRecordIndex).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.Yellow;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.btnExit, "btnExit");
			this.btnExit.BackColor = Color.Transparent;
			this.btnExit.BackgroundImage = Resources.pMain_button_normal;
			this.btnExit.ForeColor = Color.White;
			this.btnExit.Name = "btnExit";
			this.btnExit.UseVisualStyleBackColor = false;
			this.btnExit.Click += new EventHandler(this.btnExit_Click);
			componentResourceManager.ApplyResources(this.btnDeleteAllSwipeRecords, "btnDeleteAllSwipeRecords");
			this.btnDeleteAllSwipeRecords.BackColor = Color.Transparent;
			this.btnDeleteAllSwipeRecords.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeleteAllSwipeRecords.ForeColor = Color.White;
			this.btnDeleteAllSwipeRecords.Name = "btnDeleteAllSwipeRecords";
			this.btnDeleteAllSwipeRecords.UseVisualStyleBackColor = false;
			this.btnDeleteAllSwipeRecords.Click += new EventHandler(this.btnDeleteAllSwipeRecords_Click);
			componentResourceManager.ApplyResources(this.btnDeleteLog, "btnDeleteLog");
			this.btnDeleteLog.BackColor = Color.Transparent;
			this.btnDeleteLog.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeleteLog.ForeColor = Color.White;
			this.btnDeleteLog.Name = "btnDeleteLog";
			this.btnDeleteLog.UseVisualStyleBackColor = false;
			this.btnDeleteLog.Click += new EventHandler(this.btnDeleteLog_Click);
			componentResourceManager.ApplyResources(this.btnDeleteOldSwipeRecords, "btnDeleteOldSwipeRecords");
			this.btnDeleteOldSwipeRecords.BackColor = Color.Transparent;
			this.btnDeleteOldSwipeRecords.BackgroundImage = Resources.pMain_button_normal;
			this.btnDeleteOldSwipeRecords.ForeColor = Color.White;
			this.btnDeleteOldSwipeRecords.Name = "btnDeleteOldSwipeRecords";
			this.btnDeleteOldSwipeRecords.UseVisualStyleBackColor = false;
			this.btnDeleteOldSwipeRecords.Click += new EventHandler(this.btnDeleteOldSwipeRecords_Click);
			componentResourceManager.ApplyResources(this.groupBox2, "groupBox2");
			this.groupBox2.BackColor = Color.Transparent;
			this.groupBox2.Controls.Add(this.nudIndexMin);
			this.groupBox2.Controls.Add(this.lblIndexMin);
			this.groupBox2.Controls.Add(this.nudSwipeRecordIndex);
			this.groupBox2.Controls.Add(this.lblIndex);
			this.groupBox2.Controls.Add(this.btnDeleteOldSwipeRecords);
			this.groupBox2.ForeColor = Color.White;
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.TabStop = false;
			componentResourceManager.ApplyResources(this.nudIndexMin, "nudIndexMin");
			this.nudIndexMin.BackColor = Color.White;
			NumericUpDown arg_3B8_0 = this.nudIndexMin;
			int[] array = new int[4];
			array[0] = 1000;
			arg_3B8_0.Increment = new decimal(array);
			NumericUpDown arg_3D8_0 = this.nudIndexMin;
			int[] array2 = new int[4];
			array2[0] = -1;
			array2[1] = -1;
			arg_3D8_0.Maximum = new decimal(array2);
			this.nudIndexMin.Name = "nudIndexMin";
			componentResourceManager.ApplyResources(this.lblIndexMin, "lblIndexMin");
			this.lblIndexMin.Name = "lblIndexMin";
			componentResourceManager.ApplyResources(this.nudSwipeRecordIndex, "nudSwipeRecordIndex");
			this.nudSwipeRecordIndex.BackColor = Color.White;
			NumericUpDown arg_44A_0 = this.nudSwipeRecordIndex;
			int[] array3 = new int[4];
			array3[0] = 1000;
			arg_44A_0.Increment = new decimal(array3);
			NumericUpDown arg_46E_0 = this.nudSwipeRecordIndex;
			int[] array4 = new int[4];
			array4[0] = -1;
			array4[1] = -1;
			arg_46E_0.Maximum = new decimal(array4);
			this.nudSwipeRecordIndex.Name = "nudSwipeRecordIndex";
			componentResourceManager.ApplyResources(this.lblIndex, "lblIndex");
			this.lblIndex.Name = "lblIndex";
			componentResourceManager.ApplyResources(this.btnBackupDatabase, "btnBackupDatabase");
			this.btnBackupDatabase.BackColor = Color.Transparent;
			this.btnBackupDatabase.BackgroundImage = Resources.pMain_button_normal;
			this.btnBackupDatabase.ForeColor = Color.White;
			this.btnBackupDatabase.Name = "btnBackupDatabase";
			this.btnBackupDatabase.UseVisualStyleBackColor = false;
			this.btnBackupDatabase.Click += new EventHandler(this.btnBackupDatabase_Click);
			componentResourceManager.ApplyResources(this, "$this");
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.btnExit);
			base.Controls.Add(this.btnDeleteLog);
			base.Controls.Add(this.btnBackupDatabase);
			base.Controls.Add(this.btnDeleteAllSwipeRecords);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "dfrmDeleteRecords";
			base.Load += new EventHandler(this.dfrmDeleteRecords_Load);
			base.KeyDown += new KeyEventHandler(this.dfrmDeleteRecords_KeyDown);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((ISupportInitialize)this.nudIndexMin).EndInit();
			((ISupportInitialize)this.nudSwipeRecordIndex).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
