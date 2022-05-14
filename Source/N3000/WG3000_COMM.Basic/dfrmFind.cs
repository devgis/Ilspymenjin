using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WG3000_COMM.Core;
using WG3000_COMM.Properties;
using WG3000_COMM.ResStrings;

namespace WG3000_COMM.Basic
{
	public class dfrmFind : frmN3000
	{
		private object prevObjtofind;

		private object curObjtofind;

		private string prevTexttofind = "";

		private string curTexttofind = "";

		private int curRow;

		private int curCol;

		private long cntFound;

		private bool bFound;

		private object curfrm;

		public bool bClose;

		private IContainer components;

		private Label label1;

		private TextBox txtFind;

		private Button btnFind;

		private Button btnClose;

		private Label Found;

		private Label lblCount;

		public Button btnMarkAll;

		public dfrmFind()
		{
			this.InitializeComponent();
		}

		public void setObjtoFind(object obj, object frm)
		{
			try
			{
				bool flag = false;
				object obj2 = this.curObjtofind;
				if (this.curObjtofind != null)
				{
					flag = true;
				}
				this.curObjtofind = obj;
				if (this.curObjtofind is DataGridView)
				{
					this.btnMarkAll.Visible = true;
					this.btnMarkAll.Enabled = true;
					flag = true;
				}
				else if (this.curObjtofind is ListBox)
				{
					this.btnMarkAll.Visible = true;
					this.btnMarkAll.Enabled = true;
					flag = true;
				}
				else if (this.curObjtofind is ComboBox)
				{
					this.btnMarkAll.Visible = false;
					this.btnMarkAll.Enabled = false;
					flag = true;
				}
				else if (this.curObjtofind is ListView)
				{
					this.btnMarkAll.Visible = false;
					this.btnMarkAll.Enabled = false;
					flag = true;
				}
				else if (this.curObjtofind is CheckedListBox)
				{
					this.btnMarkAll.Visible = false;
					this.btnMarkAll.Enabled = false;
					flag = true;
				}
				else
				{
					this.curObjtofind = obj2;
				}
				this.cntFound = 0L;
				this.lblCount.Text = this.cntFound.ToString();
				this.selectTxtFind();
				if (flag)
				{
					this.curfrm = frm;
					base.Show();
					base.Focus();
				}
				else
				{
					base.Hide();
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void selectTxtFind()
		{
			try
			{
				base.ActiveControl = this.btnFind;
				if (this.txtFind.Text.Length > 0)
				{
					this.txtFind.SelectionStart = 0;
					this.txtFind.SelectionLength = this.txtFind.Text.Length;
				}
				base.ActiveControl = this.txtFind;
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void btnFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.curfrm != null && (this.curfrm as Form).ActiveControl != this.curObjtofind)
				{
					this.setObjtoFind((this.curfrm as Form).ActiveControl, this.curfrm);
				}
				this.curTexttofind = this.txtFind.Text.Trim().ToUpper();
				if (!(this.curTexttofind == ""))
				{
					bool flag = false;
					if (this.prevObjtofind == this.curObjtofind && wgTools.SetObjToStr(this.curTexttofind).ToUpper() == wgTools.SetObjToStr(this.prevTexttofind).ToUpper() && sender == this.btnFind)
					{
						flag = true;
					}
					if (!flag)
					{
						this.curRow = 0;
						this.curCol = 0;
						this.cntFound = 0L;
						this.prevObjtofind = this.curObjtofind;
						this.prevTexttofind = this.curTexttofind;
						this.bFound = false;
					}
					if (this.curObjtofind is DataGridView)
					{
						DataGridView dataGridView = (DataGridView)this.curObjtofind;
						int i = this.curRow;
						int j = this.curCol;
						dataGridView.ClearSelection();
						while (i < dataGridView.Rows.Count)
						{
							while (j < dataGridView.ColumnCount)
							{
								if (dataGridView.Columns[j].Visible)
								{
									object value = dataGridView.Rows[i].Cells[j].Value;
									if (wgTools.SetObjToStr(value).ToUpper().IndexOf(this.curTexttofind) >= 0)
									{
										dataGridView.FirstDisplayedScrollingRowIndex = i;
										dataGridView.Rows[i].Selected = true;
										this.bFound = true;
										this.curRow = i + 1;
										this.curCol = 0;
										this.cntFound += 1L;
										if (sender == this.btnFind)
										{
											this.lblCount.Text = this.cntFound.ToString();
											return;
										}
										if (sender == this.btnMarkAll)
										{
											this.lblCount.Text = this.cntFound.ToString();
											break;
										}
									}
								}
								j++;
							}
							j = 0;
							i++;
						}
						this.curRow = 0;
						this.curCol = 0;
						this.lblCount.Text = this.cntFound.ToString();
						if (this.bFound)
						{
							XMessageBox.Show(CommonStr.strFindComplete);
						}
						else
						{
							this.cntFound = 0L;
							this.lblCount.Text = this.cntFound.ToString();
							XMessageBox.Show(CommonStr.strNotFind);
						}
						this.cntFound = 0L;
						this.lblCount.Text = this.cntFound.ToString();
						this.selectTxtFind();
					}
					else if (this.curObjtofind is ComboBox)
					{
						ComboBox comboBox = (ComboBox)this.curObjtofind;
						for (int k = this.curRow; k < comboBox.Items.Count; k++)
						{
							object objToStr = comboBox.Items[k];
							if (wgTools.SetObjToStr(objToStr).ToUpper().IndexOf(this.curTexttofind) >= 0)
							{
								comboBox.SelectedItem = comboBox.Items[k];
								comboBox.SelectedIndex = k;
								this.bFound = true;
								this.curRow = k + 1;
								this.curCol = 0;
								this.cntFound += 1L;
								this.lblCount.Text = this.cntFound.ToString();
								return;
							}
						}
						this.curRow = 0;
						this.curCol = 0;
						this.lblCount.Text = this.cntFound.ToString();
						if (this.bFound)
						{
							XMessageBox.Show(CommonStr.strFindComplete);
						}
						else
						{
							this.cntFound = 0L;
							this.lblCount.Text = this.cntFound.ToString();
							XMessageBox.Show(CommonStr.strNotFind);
						}
						this.cntFound = 0L;
						this.selectTxtFind();
						this.lblCount.Text = this.cntFound.ToString();
					}
					else if (this.curObjtofind is ListBox)
					{
						ListBox listBox = (ListBox)this.curObjtofind;
						int l = this.curRow;
						listBox.ClearSelected();
						listBox.ClearSelected();
						while (l < listBox.Items.Count)
						{
							if (listBox.DisplayMember == "")
							{
								object objToStr2 = listBox.Items[l];
								if (wgTools.SetObjToStr(objToStr2).ToUpper().IndexOf(this.curTexttofind) >= 0)
								{
									listBox.SetSelected(l, true);
									this.bFound = true;
									this.curRow = l + 1;
									this.curCol = 0;
									this.cntFound += 1L;
									if (sender == this.btnFind)
									{
										this.lblCount.Text = this.cntFound.ToString();
										return;
									}
									Button arg_4D4_0 = this.btnMarkAll;
								}
								l++;
							}
							else
							{
								l++;
							}
						}
						this.curRow = 0;
						this.curCol = 0;
						this.lblCount.Text = this.cntFound.ToString();
						if (this.bFound)
						{
							XMessageBox.Show(CommonStr.strFindComplete);
						}
						else
						{
							this.cntFound = 0L;
							this.lblCount.Text = this.cntFound.ToString();
							XMessageBox.Show(CommonStr.strNotFind);
						}
						this.cntFound = 0L;
						this.selectTxtFind();
						this.lblCount.Text = this.cntFound.ToString();
					}
					else if (this.curObjtofind is CheckedListBox)
					{
						CheckedListBox checkedListBox = (CheckedListBox)this.curObjtofind;
						int m = this.curRow;
						checkedListBox.ClearSelected();
						checkedListBox.ClearSelected();
						while (m < checkedListBox.Items.Count)
						{
							if (checkedListBox.DisplayMember == "")
							{
								object objToStr3 = checkedListBox.Items[m];
								if (wgTools.SetObjToStr(objToStr3).ToUpper().IndexOf(this.curTexttofind) >= 0)
								{
									checkedListBox.SetSelected(m, true);
									this.bFound = true;
									this.curRow = m + 1;
									this.curCol = 0;
									this.cntFound += 1L;
									if (sender == this.btnFind)
									{
										this.lblCount.Text = this.cntFound.ToString();
										return;
									}
									Button arg_655_0 = this.btnMarkAll;
								}
								m++;
							}
							else
							{
								m++;
							}
						}
						this.curRow = 0;
						this.curCol = 0;
						this.lblCount.Text = this.cntFound.ToString();
						if (this.bFound)
						{
							XMessageBox.Show(CommonStr.strFindComplete);
						}
						else
						{
							this.cntFound = 0L;
							this.lblCount.Text = this.cntFound.ToString();
							XMessageBox.Show(CommonStr.strNotFind);
						}
						this.cntFound = 0L;
						this.selectTxtFind();
						this.lblCount.Text = this.cntFound.ToString();
					}
					else if (this.curObjtofind is ListView)
					{
						ListView listView = (ListView)this.curObjtofind;
						int n = this.curRow;
						listView.SelectedItems.Clear();
						while (n < listView.Items.Count)
						{
							object obj;
							if (listView.View == View.Details)
							{
								obj = "";
								for (int num = 0; num < listView.Items[n].SubItems.Count - 1; num++)
								{
									obj = obj + "    " + listView.Items[n].SubItems[num].Text;
								}
							}
							else
							{
								obj = listView.Items[n].Text;
							}
							if (wgTools.SetObjToStr(obj).ToUpper().IndexOf(this.curTexttofind) >= 0)
							{
								listView.Items[n].Selected = true;
								listView.Items[n].EnsureVisible();
								listView.Focus();
								this.bFound = true;
								this.curRow = n + 1;
								this.curCol = 0;
								this.cntFound += 1L;
								if (sender == this.btnFind)
								{
									this.lblCount.Text = this.cntFound.ToString();
									return;
								}
							}
							n++;
						}
						this.curRow = 0;
						this.curCol = 0;
						this.lblCount.Text = this.cntFound.ToString();
						if (this.bFound)
						{
							XMessageBox.Show(CommonStr.strFindComplete);
						}
						else
						{
							this.cntFound = 0L;
							this.lblCount.Text = this.cntFound.ToString();
							XMessageBox.Show(CommonStr.strNotFind);
						}
						this.cntFound = 0L;
						this.lblCount.Text = this.cntFound.ToString();
						this.selectTxtFind();
					}
				}
			}
			catch (Exception ex)
			{
				wgTools.WriteLine(ex.ToString());
			}
		}

		private void txtFind_TextChanged(object sender, EventArgs e)
		{
			if (this.txtFind.Text.Length == 0)
			{
				this.btnFind.Enabled = false;
				return;
			}
			this.btnFind.Enabled = true;
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			base.Hide();
		}

		private void txtFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == 13 && this.btnFind.Enabled)
			{
				this.btnFind.PerformClick();
			}
		}

		private void dfrmFind_Load(object sender, EventArgs e)
		{
		}

		public void ReallyCloseForm()
		{
			this.bClose = true;
			base.Close();
		}

		private void dfrmFind_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!this.bClose)
			{
				base.Hide();
				e.Cancel = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(dfrmFind));
			this.label1 = new Label();
			this.txtFind = new TextBox();
			this.btnFind = new Button();
			this.btnMarkAll = new Button();
			this.btnClose = new Button();
			this.Found = new Label();
			this.lblCount = new Label();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.label1, "label1");
			this.label1.BackColor = Color.Transparent;
			this.label1.ForeColor = Color.White;
			this.label1.Name = "label1";
			componentResourceManager.ApplyResources(this.txtFind, "txtFind");
			this.txtFind.Name = "txtFind";
			this.txtFind.TextChanged += new EventHandler(this.txtFind_TextChanged);
			this.txtFind.KeyDown += new KeyEventHandler(this.txtFind_KeyDown);
			this.btnFind.BackColor = Color.Transparent;
			this.btnFind.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnFind, "btnFind");
			this.btnFind.ForeColor = Color.White;
			this.btnFind.Name = "btnFind";
			this.btnFind.UseVisualStyleBackColor = false;
			this.btnFind.Click += new EventHandler(this.btnFind_Click);
			this.btnMarkAll.BackColor = Color.Transparent;
			this.btnMarkAll.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnMarkAll, "btnMarkAll");
			this.btnMarkAll.ForeColor = Color.White;
			this.btnMarkAll.Name = "btnMarkAll";
			this.btnMarkAll.UseVisualStyleBackColor = false;
			this.btnMarkAll.Click += new EventHandler(this.btnFind_Click);
			this.btnClose.BackColor = Color.Transparent;
			this.btnClose.BackgroundImage = Resources.pMain_button_normal;
			componentResourceManager.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.DialogResult = DialogResult.Cancel;
			this.btnClose.ForeColor = Color.White;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new EventHandler(this.btnClose_Click);
			componentResourceManager.ApplyResources(this.Found, "Found");
			this.Found.BackColor = Color.Transparent;
			this.Found.ForeColor = Color.White;
			this.Found.Name = "Found";
			componentResourceManager.ApplyResources(this.lblCount, "lblCount");
			this.lblCount.BackColor = Color.Transparent;
			this.lblCount.ForeColor = Color.White;
			this.lblCount.Name = "lblCount";
			base.AcceptButton = this.btnFind;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = AutoScaleMode.Font;
			base.CancelButton = this.btnClose;
			base.Controls.Add(this.lblCount);
			base.Controls.Add(this.Found);
			base.Controls.Add(this.btnClose);
			base.Controls.Add(this.btnMarkAll);
			base.Controls.Add(this.btnFind);
			base.Controls.Add(this.txtFind);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.Name = "dfrmFind";
			base.TopMost = true;
			base.FormClosing += new FormClosingEventHandler(this.dfrmFind_FormClosing);
			base.Load += new EventHandler(this.dfrmFind_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
