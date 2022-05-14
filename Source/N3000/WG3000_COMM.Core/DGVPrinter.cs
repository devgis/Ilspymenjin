using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace WG3000_COMM.Core
{
	internal class DGVPrinter : IDisposable
	{
		private class PageDef
		{
			public IList colstoprint;

			public List<float> colwidths;

			public List<float> colwidthsoverride;

			public float coltotalwidth;

			public Margins margins;

			public PageDef(Margins m, int count)
			{
				this.colstoprint = new List<object>(count);
				this.colwidths = new List<float>(count);
				this.colwidthsoverride = new List<float>(count);
				this.coltotalwidth = 0f;
				this.margins = (Margins)m.Clone();
			}
		}

		public class PrintDialogSettingsClass
		{
			public bool AllowSelection = true;

			public bool AllowSomePages = true;

			public bool AllowCurrentPage = true;

			public bool AllowPrintToFile;

			public bool ShowHelp = true;

			public bool ShowNetwork = true;

			public bool UseEXDialog = true;
		}

		public enum Alignment
		{
			NotSet,
			Left,
			Right,
			Center
		}

		public enum RowHeightSetting
		{
			StringHeight,
			CellHeight
		}

		private IList<DGVPrinter.PageDef> pagesets;

		private int currentpageset;

		private DataGridView dgv;

		private PrintDocument printDoc;

		private IList rowstoprint;

		private IList colstoprint;

		private int lastrowprinted = -1;

		private int fromPage;

		private int toPage = -1;

		private int pageHeight;

		private int pageWidth;

		private int printWidth;

		private float rowheaderwidth;

		private int CurrentPage;

		private PrintRange printRange;

		private float headerHeight;

		private float footerHeight;

		private float pagenumberHeight;

		private float colheaderheight;

		private List<float> rowheights;

		private List<float> colwidths;

		protected Form _Owner;

		protected double _PrintPreviewZoom = 1.0;

		private DGVPrinter.PrintDialogSettingsClass printDialogSettings = new DGVPrinter.PrintDialogSettingsClass();

		private string printerName;

		private Icon ppvIcon;

		private bool printHeader = true;

		private bool printFooter = true;

		private bool printColumnHeaders = true;

		private bool overridetitleformat;

		private string title;

		private string docName;

		private Font titlefont;

		private Color titlecolor;

		private StringFormat titleformat;

		private bool overridesubtitleformat;

		private string subtitle;

		private Font subtitlefont;

		private Color subtitlecolor;

		private StringFormat subtitleformat;

		private bool overridefooterformat;

		private string footer;

		private Font footerfont;

		private Color footercolor;

		private StringFormat footerformat;

		private float footerspacing;

		private bool overridepagenumberformat;

		private bool pageno = true;

		private Font pagenofont;

		private Color pagenocolor;

		private StringFormat pagenumberformat;

		private bool pagenumberontop;

		private bool pagenumberonseparateline;

		private int totalpages;

		private bool showtotalpagenumber;

		private string pageseparator = " of ";

		private string pagetext = "Page ";

		private string parttext = " - Part ";

		private StringFormat headercellformat;

		private StringAlignment headercellalignment;

		private StringFormatFlags headercellformatflags;

		private StringFormat cellformat;

		private StringAlignment cellalignment;

		private StringFormatFlags cellformatflags;

		private List<float> colwidthsoverride = new List<float>();

		private Dictionary<string, float> publicwidthoverrides = new Dictionary<string, float>();

		private Dictionary<string, DataGridViewCellStyle> colstyles = new Dictionary<string, DataGridViewCellStyle>();

		private bool porportionalcolumns;

		private DGVPrinter.Alignment tablealignment;

		private DGVPrinter.RowHeightSetting _rowheight;

		private SolidBrush SolidBrush1;

		private Pen lines;

		public Form Owner
		{
			get
			{
				return this._Owner;
			}
			set
			{
				this._Owner = value;
			}
		}

		public double PrintPreviewZoom
		{
			get
			{
				return this._PrintPreviewZoom;
			}
			set
			{
				this._PrintPreviewZoom = value;
			}
		}

		public PrinterSettings PrintSettings
		{
			get
			{
				return this.printDoc.PrinterSettings;
			}
		}

		public DGVPrinter.PrintDialogSettingsClass PrintDialogSettings
		{
			get
			{
				return this.printDialogSettings;
			}
		}

		public string PrinterName
		{
			get
			{
				return this.printerName;
			}
			set
			{
				this.printerName = value;
			}
		}

		public PrintDocument printDocument
		{
			get
			{
				return this.printDoc;
			}
			set
			{
				this.printDoc = value;
			}
		}

		public Icon PreviewDialogIcon
		{
			get
			{
				return this.ppvIcon;
			}
			set
			{
				this.ppvIcon = value;
			}
		}

		public bool PrintHeader
		{
			get
			{
				return this.printHeader;
			}
			set
			{
				this.printHeader = value;
			}
		}

		public bool PrintFooter
		{
			get
			{
				return this.printFooter;
			}
			set
			{
				this.printFooter = value;
			}
		}

		public bool PrintColumnHeaders
		{
			get
			{
				return this.printColumnHeaders;
			}
			set
			{
				this.printColumnHeaders = value;
			}
		}

		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				if (this.docName == null)
				{
					this.printDoc.DocumentName = value;
				}
			}
		}

		public string DocName
		{
			get
			{
				return this.docName;
			}
			set
			{
				this.printDoc.DocumentName = value;
				this.docName = value;
			}
		}

		public Font TitleFont
		{
			get
			{
				return this.titlefont;
			}
			set
			{
				this.titlefont = value;
			}
		}

		public Color TitleColor
		{
			get
			{
				return this.titlecolor;
			}
			set
			{
				this.titlecolor = value;
			}
		}

		public StringFormat TitleFormat
		{
			get
			{
				return this.titleformat;
			}
			set
			{
				this.titleformat = value;
				this.overridetitleformat = true;
			}
		}

		public StringAlignment TitleAlignment
		{
			get
			{
				return this.titleformat.Alignment;
			}
			set
			{
				this.titleformat.Alignment = value;
				this.overridetitleformat = true;
			}
		}

		public StringFormatFlags TitleFormatFlags
		{
			get
			{
				return this.titleformat.FormatFlags;
			}
			set
			{
				this.titleformat.FormatFlags = value;
				this.overridetitleformat = true;
			}
		}

		public string SubTitle
		{
			get
			{
				return this.subtitle;
			}
			set
			{
				this.subtitle = value;
			}
		}

		public Font SubTitleFont
		{
			get
			{
				return this.subtitlefont;
			}
			set
			{
				this.subtitlefont = value;
			}
		}

		public Color SubTitleColor
		{
			get
			{
				return this.subtitlecolor;
			}
			set
			{
				this.subtitlecolor = value;
			}
		}

		public StringFormat SubTitleFormat
		{
			get
			{
				return this.subtitleformat;
			}
			set
			{
				this.subtitleformat = value;
				this.overridesubtitleformat = true;
			}
		}

		public StringAlignment SubTitleAlignment
		{
			get
			{
				return this.subtitleformat.Alignment;
			}
			set
			{
				this.subtitleformat.Alignment = value;
				this.overridesubtitleformat = true;
			}
		}

		public StringFormatFlags SubTitleFormatFlags
		{
			get
			{
				return this.subtitleformat.FormatFlags;
			}
			set
			{
				this.subtitleformat.FormatFlags = value;
				this.overridesubtitleformat = true;
			}
		}

		public string Footer
		{
			get
			{
				return this.footer;
			}
			set
			{
				this.footer = value;
			}
		}

		public Font FooterFont
		{
			get
			{
				return this.footerfont;
			}
			set
			{
				this.footerfont = value;
			}
		}

		public Color FooterColor
		{
			get
			{
				return this.footercolor;
			}
			set
			{
				this.footercolor = value;
			}
		}

		public StringFormat FooterFormat
		{
			get
			{
				return this.footerformat;
			}
			set
			{
				this.footerformat = value;
				this.overridefooterformat = true;
			}
		}

		public StringAlignment FooterAlignment
		{
			get
			{
				return this.footerformat.Alignment;
			}
			set
			{
				this.footerformat.Alignment = value;
				this.overridefooterformat = true;
			}
		}

		public StringFormatFlags FooterFormatFlags
		{
			get
			{
				return this.footerformat.FormatFlags;
			}
			set
			{
				this.footerformat.FormatFlags = value;
				this.overridefooterformat = true;
			}
		}

		public float FooterSpacing
		{
			get
			{
				return this.footerspacing;
			}
			set
			{
				this.footerspacing = value;
			}
		}

		public bool PageNumbers
		{
			get
			{
				return this.pageno;
			}
			set
			{
				this.pageno = value;
			}
		}

		public Font PageNumberFont
		{
			get
			{
				return this.pagenofont;
			}
			set
			{
				this.pagenofont = value;
			}
		}

		public Color PageNumberColor
		{
			get
			{
				return this.pagenocolor;
			}
			set
			{
				this.pagenocolor = value;
			}
		}

		public StringFormat PageNumberFormat
		{
			get
			{
				return this.pagenumberformat;
			}
			set
			{
				this.pagenumberformat = value;
				this.overridepagenumberformat = true;
			}
		}

		public StringAlignment PageNumberAlignment
		{
			get
			{
				return this.pagenumberformat.Alignment;
			}
			set
			{
				this.pagenumberformat.Alignment = value;
				this.overridepagenumberformat = true;
			}
		}

		public StringFormatFlags PageNumberFormatFlags
		{
			get
			{
				return this.pagenumberformat.FormatFlags;
			}
			set
			{
				this.pagenumberformat.FormatFlags = value;
				this.overridepagenumberformat = true;
			}
		}

		public bool PageNumberInHeader
		{
			get
			{
				return this.pagenumberontop;
			}
			set
			{
				this.pagenumberontop = value;
			}
		}

		public bool PageNumberOnSeparateLine
		{
			get
			{
				return this.pagenumberonseparateline;
			}
			set
			{
				this.pagenumberonseparateline = value;
			}
		}

		public bool ShowTotalPageNumber
		{
			get
			{
				return this.showtotalpagenumber;
			}
			set
			{
				this.showtotalpagenumber = value;
			}
		}

		public string PageSeparator
		{
			get
			{
				return this.pageseparator;
			}
			set
			{
				this.pageseparator = value;
			}
		}

		public string PageText
		{
			get
			{
				return this.pagetext;
			}
			set
			{
				this.pagetext = value;
			}
		}

		public string PartText
		{
			get
			{
				return this.parttext;
			}
			set
			{
				this.parttext = value;
			}
		}

		public StringAlignment HeaderCellAlignment
		{
			get
			{
				return this.headercellalignment;
			}
			set
			{
				this.headercellalignment = value;
			}
		}

		public StringFormatFlags HeaderCellFormatFlags
		{
			get
			{
				return this.headercellformatflags;
			}
			set
			{
				this.headercellformatflags = value;
			}
		}

		public StringAlignment CellAlignment
		{
			get
			{
				return this.cellalignment;
			}
			set
			{
				this.cellalignment = value;
			}
		}

		public StringFormatFlags CellFormatFlags
		{
			get
			{
				return this.cellformatflags;
			}
			set
			{
				this.cellformatflags = value;
			}
		}

		public Dictionary<string, float> ColumnWidths
		{
			get
			{
				return this.publicwidthoverrides;
			}
		}

		public Dictionary<string, DataGridViewCellStyle> ColumnStyles
		{
			get
			{
				return this.colstyles;
			}
		}

		public Margins PrintMargins
		{
			get
			{
				return this.PageSettings.Margins;
			}
			set
			{
				this.PageSettings.Margins = value;
			}
		}

		public PageSettings PageSettings
		{
			get
			{
				return this.printDoc.DefaultPageSettings;
			}
		}

		public bool PorportionalColumns
		{
			get
			{
				return this.porportionalcolumns;
			}
			set
			{
				this.porportionalcolumns = value;
			}
		}

		public DGVPrinter.Alignment TableAlignment
		{
			get
			{
				return this.tablealignment;
			}
			set
			{
				this.tablealignment = value;
			}
		}

		public DGVPrinter.RowHeightSetting RowHeight
		{
			get
			{
				return this._rowheight;
			}
			set
			{
				this._rowheight = value;
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.SolidBrush1 != null)
				{
					this.SolidBrush1.Dispose();
				}
				if (this.lines != null)
				{
					this.lines.Dispose();
				}
				if (this.headercellformat != null)
				{
					this.headercellformat.Dispose();
				}
				if (this.cellformat != null)
				{
					this.cellformat.Dispose();
				}
				if (this.footerfont != null)
				{
					this.footerfont.Dispose();
				}
				if (this.pagenofont != null)
				{
					this.pagenofont.Dispose();
				}
				if (this.printDoc != null)
				{
					this.printDoc.Dispose();
				}
				if (this.subtitlefont != null)
				{
					this.subtitlefont.Dispose();
				}
				if (this.titlefont != null)
				{
					this.titlefont.Dispose();
				}
			}
		}

		public StringFormat GetHeaderCellFormat(DataGridView grid)
		{
			if (grid != null && this.headercellformat == null)
			{
				this.buildstringformat(ref this.headercellformat, grid.Columns[0].HeaderCell.InheritedStyle, this.headercellalignment, StringAlignment.Near, this.headercellformatflags, StringTrimming.Word);
			}
			if (this.headercellformat == null)
			{
				this.headercellformat = new StringFormat(this.headercellformatflags);
			}
			return this.headercellformat;
		}

		public StringFormat GetCellFormat(DataGridView grid)
		{
			if (grid != null && this.cellformat == null)
			{
				this.buildstringformat(ref this.cellformat, grid.Rows[0].Cells[0].InheritedStyle, this.cellalignment, StringAlignment.Near, this.cellformatflags, StringTrimming.Word);
			}
			if (this.cellformat == null)
			{
				this.cellformat = new StringFormat(this.cellformatflags);
			}
			return this.cellformat;
		}

		private int PreviewDisplayWidth()
		{
			double num = (double)((float)this.printDoc.DefaultPageSettings.Bounds.Width + 3f * this.printDoc.DefaultPageSettings.HardMarginY);
			return (int)(num * this.PrintPreviewZoom);
		}

		private int PreviewDisplayHeight()
		{
			double num = (double)((float)this.printDoc.DefaultPageSettings.Bounds.Height + 3f * this.printDoc.DefaultPageSettings.HardMarginX);
			return (int)(num * this.PrintPreviewZoom);
		}

		public DGVPrinter()
		{
			this.printDoc = new PrintDocument();
			this.printDoc.PrintPage += new PrintPageEventHandler(this.printDoc_PrintPage);
			this.printDoc.BeginPrint += new PrintEventHandler(this.printDoc_BeginPrint);
			this.PrintMargins = new Margins(60, 60, 40, 40);
			this.pagenofont = new Font("Tahoma", 8f, FontStyle.Regular, GraphicsUnit.Point);
			this.pagenocolor = Color.Black;
			this.titlefont = new Font("Tahoma", 18f, FontStyle.Bold, GraphicsUnit.Point);
			this.titlecolor = Color.Black;
			this.subtitlefont = new Font("Tahoma", 12f, FontStyle.Bold, GraphicsUnit.Point);
			this.subtitlecolor = Color.Black;
			this.footerfont = new Font("Tahoma", 10f, FontStyle.Bold, GraphicsUnit.Point);
			this.footercolor = Color.Black;
			this.buildstringformat(ref this.titleformat, null, StringAlignment.Center, StringAlignment.Center, StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
			this.buildstringformat(ref this.subtitleformat, null, StringAlignment.Center, StringAlignment.Center, StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
			this.buildstringformat(ref this.footerformat, null, StringAlignment.Center, StringAlignment.Center, StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
			this.buildstringformat(ref this.pagenumberformat, null, StringAlignment.Far, StringAlignment.Center, StringFormatFlags.NoWrap | StringFormatFlags.LineLimit | StringFormatFlags.NoClip, StringTrimming.Word);
			this.headercellformat = null;
			this.cellformat = null;
			this.Owner = null;
			this.PrintPreviewZoom = 1.0;
			this.headercellalignment = StringAlignment.Near;
			this.headercellformatflags = (StringFormatFlags.LineLimit | StringFormatFlags.NoClip);
			this.cellalignment = StringAlignment.Near;
			this.cellformatflags = (StringFormatFlags.LineLimit | StringFormatFlags.NoClip);
		}

		public void PrintDataGridView(DataGridView dgv)
		{
			if (dgv == null)
			{
				throw new Exception("Null Parameter passed to DGVPrinter.");
			}
			if (typeof(DataGridView) != dgv.GetType())
			{
				throw new Exception("Invalid Parameter passed to DGVPrinter.");
			}
			this.dgv = dgv;
			if (DialogResult.OK == this.DisplayPrintDialog())
			{
				this.SetupPrint();
				this.printDoc.Print();
			}
		}

		public void PrintPreviewDataGridView(DataGridView dgv)
		{
			if (dgv == null)
			{
				throw new Exception("Null Parameter passed to DGVPrinter.");
			}
			if (typeof(DataGridView) != dgv.GetType())
			{
				throw new Exception("Invalid Parameter passed to DGVPrinter.");
			}
			this.dgv = dgv;
			if (DialogResult.OK == this.DisplayPrintDialog())
			{
				this.PrintPreviewNoDisplay(dgv);
			}
		}

		public DialogResult DisplayPrintDialog()
		{
			DialogResult result;
			using (PrintDialog printDialog = new PrintDialog())
			{
				printDialog.UseEXDialog = this.printDialogSettings.UseEXDialog;
				printDialog.AllowSelection = this.printDialogSettings.AllowSelection;
				printDialog.AllowSomePages = this.printDialogSettings.AllowSomePages;
				printDialog.AllowCurrentPage = this.printDialogSettings.AllowCurrentPage;
				printDialog.AllowPrintToFile = this.printDialogSettings.AllowPrintToFile;
				printDialog.ShowHelp = this.printDialogSettings.ShowHelp;
				printDialog.ShowNetwork = this.printDialogSettings.ShowNetwork;
				printDialog.Document = this.printDoc;
				if (!string.IsNullOrEmpty(this.printerName))
				{
					this.printDoc.PrinterSettings.PrinterName = this.printerName;
				}
				this.printDoc.DefaultPageSettings.Landscape = printDialog.PrinterSettings.DefaultPageSettings.Landscape;
				this.printDoc.DefaultPageSettings.PaperSize = new PaperSize(printDialog.PrinterSettings.DefaultPageSettings.PaperSize.PaperName, printDialog.PrinterSettings.DefaultPageSettings.PaperSize.Width, printDialog.PrinterSettings.DefaultPageSettings.PaperSize.Height);
				result = printDialog.ShowDialog();
			}
			return result;
		}

		public void PrintNoDisplay(DataGridView dgv)
		{
			if (dgv == null)
			{
				throw new Exception("Null Parameter passed to DGVPrinter.");
			}
			if (typeof(DataGridView) != dgv.GetType())
			{
				throw new Exception("Invalid Parameter passed to DGVPrinter.");
			}
			this.dgv = dgv;
			this.SetupPrint();
			this.printDoc.Print();
		}

		public void PrintPreviewNoDisplay(DataGridView dgv)
		{
			if (dgv == null)
			{
				throw new Exception("Null Parameter passed to DGVPrinter.");
			}
			if (typeof(DataGridView) != dgv.GetType())
			{
				throw new Exception("Invalid Parameter passed to DGVPrinter.");
			}
			this.dgv = dgv;
			this.SetupPrint();
			using (PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog())
			{
				printPreviewDialog.Document = this.printDoc;
				printPreviewDialog.UseAntiAlias = true;
				printPreviewDialog.Owner = this.Owner;
				printPreviewDialog.PrintPreviewControl.Zoom = this.PrintPreviewZoom;
				printPreviewDialog.Width = this.PreviewDisplayWidth();
				printPreviewDialog.Height = this.PreviewDisplayHeight();
				if (this.ppvIcon != null)
				{
					printPreviewDialog.Icon = this.ppvIcon;
				}
				printPreviewDialog.ShowDialog();
			}
		}

		public bool EmbeddedPrint(DataGridView dgv, Graphics g, Rectangle area)
		{
			if (dgv == null || g == null)
			{
				throw new Exception("Null Parameter passed to DGVPrinter.");
			}
			this.dgv = dgv;
			Margins arg_1E_0 = this.PrintMargins;
			this.PrintMargins.Top = area.Top;
			this.PrintMargins.Bottom = 0;
			this.PrintMargins.Left = area.Left;
			this.PrintMargins.Right = 0;
			this.pageHeight = area.Height + area.Top;
			this.printWidth = area.Width;
			this.pageWidth = area.Width + area.Left;
			this.fromPage = 0;
			this.toPage = 2147483647;
			this.PrintHeader = false;
			this.PrintFooter = false;
			if (this.cellformat == null)
			{
				this.buildstringformat(ref this.cellformat, dgv.DefaultCellStyle, this.cellalignment, StringAlignment.Near, this.cellformatflags, StringTrimming.Word);
			}
			this.rowstoprint = new List<object>(dgv.Rows.Count);
			foreach (DataGridViewRow dataGridViewRow in ((IEnumerable)dgv.Rows))
			{
				if (dataGridViewRow.Visible)
				{
					this.rowstoprint.Add(dataGridViewRow);
				}
			}
			this.colstoprint = new List<object>(dgv.Columns.Count);
			foreach (DataGridViewColumn dataGridViewColumn in dgv.Columns)
			{
				if (dataGridViewColumn.Visible)
				{
					this.colstoprint.Add(dataGridViewColumn);
				}
			}
			SortedList sortedList = new SortedList(this.colstoprint.Count);
			foreach (DataGridViewColumn dataGridViewColumn2 in this.colstoprint)
			{
				sortedList.Add(dataGridViewColumn2.DisplayIndex, dataGridViewColumn2);
			}
			this.colstoprint.Clear();
			foreach (object current in sortedList.Values)
			{
				this.colstoprint.Add(current);
			}
			foreach (DataGridViewColumn dataGridViewColumn3 in this.colstoprint)
			{
				if (this.publicwidthoverrides.ContainsKey(dataGridViewColumn3.Name))
				{
					this.colwidthsoverride.Add(this.publicwidthoverrides[dataGridViewColumn3.Name]);
				}
				else
				{
					this.colwidthsoverride.Add(-1f);
				}
			}
			this.measureprintarea(g);
			this.totalpages = this.TotalPages();
			this.currentpageset = 0;
			this.lastrowprinted = -1;
			this.CurrentPage = 0;
			return this.PrintPage(g);
		}

		private void printDoc_BeginPrint(object sender, PrintEventArgs e)
		{
			this.currentpageset = 0;
			this.lastrowprinted = -1;
			this.CurrentPage = 0;
		}

		private void printDoc_PrintPage(object sender, PrintPageEventArgs e)
		{
			e.HasMorePages = this.PrintPage(e.Graphics);
		}

		private void SetupPrint()
		{
			if (this.headercellformat == null)
			{
				this.buildstringformat(ref this.headercellformat, this.dgv.Columns[0].HeaderCell.InheritedStyle, this.headercellalignment, StringAlignment.Near, this.headercellformatflags, StringTrimming.Word);
			}
			if (this.cellformat == null)
			{
				this.buildstringformat(ref this.cellformat, this.dgv.DefaultCellStyle, this.cellalignment, StringAlignment.Near, this.cellformatflags, StringTrimming.Word);
			}
			int num = (int)Math.Round((double)this.printDoc.DefaultPageSettings.HardMarginX);
			int num2 = (int)Math.Round((double)this.printDoc.DefaultPageSettings.HardMarginY);
			int num3;
			if (this.printDoc.DefaultPageSettings.Landscape)
			{
				num3 = (int)Math.Round((double)this.printDoc.DefaultPageSettings.PrintableArea.Height);
			}
			else
			{
				num3 = (int)Math.Round((double)this.printDoc.DefaultPageSettings.PrintableArea.Width);
			}
			this.pageHeight = this.printDoc.DefaultPageSettings.Bounds.Height;
			this.pageWidth = this.printDoc.DefaultPageSettings.Bounds.Width;
			this.PrintMargins = this.printDoc.DefaultPageSettings.Margins;
			this.PrintMargins.Right = ((num > this.PrintMargins.Right) ? num : this.PrintMargins.Right);
			this.PrintMargins.Left = ((num > this.PrintMargins.Left) ? num : this.PrintMargins.Left);
			this.PrintMargins.Top = ((num2 > this.PrintMargins.Top) ? num2 : this.PrintMargins.Top);
			this.PrintMargins.Bottom = ((num2 > this.PrintMargins.Bottom) ? num2 : this.PrintMargins.Bottom);
			this.printWidth = this.pageWidth - this.PrintMargins.Left - this.PrintMargins.Right;
			this.printWidth = ((this.printWidth > num3) ? num3 : this.printWidth);
			this.printRange = this.printDoc.PrinterSettings.PrintRange;
			if (PrintRange.SomePages == this.printRange)
			{
				this.fromPage = this.printDoc.PrinterSettings.FromPage;
				this.toPage = this.printDoc.PrinterSettings.ToPage;
			}
			else
			{
				this.fromPage = 0;
				this.toPage = 2147483647;
			}
			if (PrintRange.Selection == this.printRange)
			{
				SortedList sortedList;
				if (this.dgv.SelectedRows.Count != 0)
				{
					sortedList = new SortedList(this.dgv.SelectedRows.Count);
					foreach (DataGridViewRow dataGridViewRow in this.dgv.SelectedRows)
					{
						sortedList.Add(dataGridViewRow.Index, dataGridViewRow);
					}
					sortedList.Values.GetEnumerator();
					this.rowstoprint = new List<object>(sortedList.Count);
					foreach (object current in sortedList.Values)
					{
						this.rowstoprint.Add(current);
					}
					this.colstoprint = new List<object>(this.dgv.Columns.Count);
					using (IEnumerator enumerator3 = this.dgv.Columns.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)enumerator3.Current;
							if (dataGridViewColumn.Visible)
							{
								this.colstoprint.Add(dataGridViewColumn);
							}
						}
						goto IL_864;
					}
				}
				SortedList sortedList2;
				if (this.dgv.SelectedColumns.Count != 0)
				{
					this.rowstoprint = this.dgv.Rows;
					sortedList2 = new SortedList(this.dgv.SelectedColumns.Count);
					foreach (DataGridViewRow dataGridViewRow2 in this.dgv.SelectedColumns)
					{
						sortedList2.Add(dataGridViewRow2.Index, dataGridViewRow2);
					}
					this.colstoprint = new List<object>(sortedList2.Count);
					using (IEnumerator enumerator5 = sortedList2.Values.GetEnumerator())
					{
						while (enumerator5.MoveNext())
						{
							object current2 = enumerator5.Current;
							this.colstoprint.Add(current2);
						}
						goto IL_864;
					}
				}
				sortedList = new SortedList(this.dgv.SelectedCells.Count);
				sortedList2 = new SortedList(this.dgv.SelectedCells.Count);
				foreach (DataGridViewCell dataGridViewCell in this.dgv.SelectedCells)
				{
					int columnIndex = dataGridViewCell.ColumnIndex;
					int rowIndex = dataGridViewCell.RowIndex;
					if (!sortedList.Contains(rowIndex))
					{
						sortedList.Add(rowIndex, this.dgv.Rows[rowIndex]);
					}
					if (!sortedList2.Contains(columnIndex))
					{
						sortedList2.Add(columnIndex, this.dgv.Columns[columnIndex]);
					}
				}
				this.rowstoprint = new List<object>(sortedList.Count);
				foreach (object current3 in sortedList.Values)
				{
					this.rowstoprint.Add(current3);
				}
				this.colstoprint = new List<object>(sortedList2.Count);
				using (IEnumerator enumerator8 = sortedList2.Values.GetEnumerator())
				{
					while (enumerator8.MoveNext())
					{
						object current4 = enumerator8.Current;
						this.colstoprint.Add(current4);
					}
					goto IL_864;
				}
			}
			if (PrintRange.CurrentPage == this.printRange)
			{
				this.rowstoprint = new List<object>(this.dgv.DisplayedRowCount(true));
				this.colstoprint = new List<object>(this.dgv.Columns.Count);
				for (int i = this.dgv.FirstDisplayedScrollingRowIndex; i < this.dgv.FirstDisplayedScrollingRowIndex + this.dgv.DisplayedRowCount(true); i++)
				{
					DataGridViewRow dataGridViewRow3 = this.dgv.Rows[i];
					if (dataGridViewRow3.Visible)
					{
						this.rowstoprint.Add(dataGridViewRow3);
					}
				}
				this.colstoprint = new List<object>(this.dgv.Columns.Count);
				using (IEnumerator enumerator9 = this.dgv.Columns.GetEnumerator())
				{
					while (enumerator9.MoveNext())
					{
						DataGridViewColumn dataGridViewColumn2 = (DataGridViewColumn)enumerator9.Current;
						if (dataGridViewColumn2.Visible)
						{
							this.colstoprint.Add(dataGridViewColumn2);
						}
					}
					goto IL_864;
				}
			}
			this.rowstoprint = new List<object>(this.dgv.Rows.Count);
			foreach (DataGridViewRow dataGridViewRow4 in ((IEnumerable)this.dgv.Rows))
			{
				if (dataGridViewRow4.Visible)
				{
					this.rowstoprint.Add(dataGridViewRow4);
				}
			}
			this.colstoprint = new List<object>(this.dgv.Columns.Count);
			foreach (DataGridViewColumn dataGridViewColumn3 in this.dgv.Columns)
			{
				if (dataGridViewColumn3.Visible)
				{
					this.colstoprint.Add(dataGridViewColumn3);
				}
			}
			IL_864:
			SortedList sortedList3 = new SortedList(this.colstoprint.Count);
			foreach (DataGridViewColumn dataGridViewColumn4 in this.colstoprint)
			{
				sortedList3.Add(dataGridViewColumn4.DisplayIndex, dataGridViewColumn4);
			}
			this.colstoprint.Clear();
			foreach (object current5 in sortedList3.Values)
			{
				this.colstoprint.Add(current5);
			}
			foreach (DataGridViewColumn dataGridViewColumn5 in this.colstoprint)
			{
				if (this.publicwidthoverrides.ContainsKey(dataGridViewColumn5.Name))
				{
					this.colwidthsoverride.Add(this.publicwidthoverrides[dataGridViewColumn5.Name]);
				}
				else
				{
					this.colwidthsoverride.Add(-1f);
				}
			}
			this.measureprintarea(this.printDoc.PrinterSettings.CreateMeasurementGraphics());
			this.totalpages = this.TotalPages();
		}

		private void buildstringformat(ref StringFormat format, DataGridViewCellStyle controlstyle, StringAlignment alignment, StringAlignment linealignment, StringFormatFlags flags, StringTrimming trim)
		{
			if (format == null)
			{
				format = new StringFormat();
			}
			format.Alignment = alignment;
			format.LineAlignment = linealignment;
			format.FormatFlags = flags;
			format.Trimming = trim;
			if (controlstyle != null)
			{
				DataGridViewContentAlignment alignment2 = controlstyle.Alignment;
				if (alignment2.ToString().Contains("Center"))
				{
					format.Alignment = StringAlignment.Center;
				}
				else if (alignment2.ToString().Contains("Left"))
				{
					format.Alignment = StringAlignment.Near;
				}
				else if (alignment2.ToString().Contains("Right"))
				{
					format.Alignment = StringAlignment.Far;
				}
				if (alignment2.ToString().Contains("Top"))
				{
					format.LineAlignment = StringAlignment.Near;
					return;
				}
				if (alignment2.ToString().Contains("Middle"))
				{
					format.LineAlignment = StringAlignment.Center;
					return;
				}
				if (alignment2.ToString().Contains("Bottom"))
				{
					format.LineAlignment = StringAlignment.Far;
				}
			}
		}

		private void measureprintarea(Graphics g)
		{
			this.rowheights = new List<float>(this.rowstoprint.Count);
			this.colwidths = new List<float>(this.colstoprint.Count);
			this.headerHeight = 0f;
			this.footerHeight = 0f;
			Font font = this.dgv.ColumnHeadersDefaultCellStyle.Font;
			if (font == null)
			{
				font = this.dgv.DefaultCellStyle.Font;
			}
			for (int i = 0; i < this.colstoprint.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.colstoprint[i];
				float width;
				if (0f < this.colwidthsoverride[i])
				{
					width = this.colwidthsoverride[i];
				}
				else
				{
					width = (float)this.printWidth;
				}
				SizeF sizeF = g.MeasureString(dataGridViewColumn.HeaderText, font, new SizeF(width, 2.14748365E+09f), this.headercellformat);
				this.colwidths.Add(sizeF.Width);
				this.colheaderheight = ((this.colheaderheight < sizeF.Height) ? sizeF.Height : this.colheaderheight);
			}
			if (this.pageno)
			{
				this.pagenumberHeight = g.MeasureString("Page", this.pagenofont, this.printWidth, this.pagenumberformat).Height;
			}
			if (this.PrintHeader)
			{
				if (this.pagenumberontop && !this.pagenumberonseparateline)
				{
					this.headerHeight += this.pagenumberHeight;
				}
				if (!string.IsNullOrEmpty(this.title))
				{
					this.headerHeight += g.MeasureString(this.title, this.titlefont, this.printWidth, this.titleformat).Height;
				}
				if (!string.IsNullOrEmpty(this.subtitle))
				{
					this.headerHeight += g.MeasureString(this.subtitle, this.subtitlefont, this.printWidth, this.subtitleformat).Height;
				}
				this.headerHeight += this.colheaderheight;
			}
			if (this.PrintFooter)
			{
				if (!string.IsNullOrEmpty(this.footer))
				{
					this.footerHeight += g.MeasureString(this.footer, this.footerfont, this.printWidth, this.footerformat).Height;
				}
				if (!this.pagenumberontop && this.pagenumberonseparateline)
				{
					this.footerHeight += this.pagenumberHeight;
				}
				this.footerHeight += this.footerspacing;
			}
			for (int i = 0; i < this.rowstoprint.Count; i++)
			{
				DataGridViewRow dataGridViewRow = (DataGridViewRow)this.rowstoprint[i];
				this.rowheights.Add(0f);
				if (this.dgv.RowHeadersVisible)
				{
					SizeF sizeF2 = g.MeasureString(dataGridViewRow.HeaderCell.EditedFormattedValue.ToString(), font);
					this.rowheaderwidth = ((this.rowheaderwidth < sizeF2.Width) ? sizeF2.Width : this.rowheaderwidth);
				}
				for (int j = 0; j < this.colstoprint.Count; j++)
				{
					DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)this.colstoprint[j];
					string text = dataGridViewRow.Cells[dataGridViewColumn.Index].EditedFormattedValue.ToString();
					StringFormat stringFormat = null;
					DataGridViewCellStyle dataGridViewCellStyle;
					if (this.ColumnStyles.ContainsKey(dataGridViewColumn.Name))
					{
						dataGridViewCellStyle = this.colstyles[dataGridViewColumn.Name];
						this.buildstringformat(ref stringFormat, dataGridViewCellStyle, this.cellformat.Alignment, this.cellformat.LineAlignment, this.cellformat.FormatFlags, this.cellformat.Trimming);
					}
					else if (dataGridViewColumn.HasDefaultCellStyle || dataGridViewRow.Cells[dataGridViewColumn.Index].HasStyle)
					{
						dataGridViewCellStyle = dataGridViewRow.Cells[dataGridViewColumn.Index].InheritedStyle;
						this.buildstringformat(ref stringFormat, dataGridViewCellStyle, this.cellformat.Alignment, this.cellformat.LineAlignment, this.cellformat.FormatFlags, this.cellformat.Trimming);
					}
					else
					{
						stringFormat = this.cellformat;
						dataGridViewCellStyle = this.dgv.DefaultCellStyle;
					}
					SizeF sizeF3;
					if (DGVPrinter.RowHeightSetting.CellHeight == this.RowHeight)
					{
						sizeF3 = dataGridViewRow.Cells[dataGridViewColumn.Index].Size;
					}
					else
					{
						sizeF3 = g.MeasureString(text, dataGridViewCellStyle.Font);
					}
					if (0f < this.colwidthsoverride[j] || sizeF3.Width > (float)this.printWidth)
					{
						if (0f < this.colwidthsoverride[j])
						{
							this.colwidths[j] = this.colwidthsoverride[j];
						}
						else if (sizeF3.Width > (float)this.printWidth)
						{
							this.colwidths[j] = (float)this.printWidth;
						}
						int num;
						int num2;
						float height = g.MeasureString(text, dataGridViewCellStyle.Font, new SizeF(this.colwidths[j], 2.14748365E+09f), stringFormat, out num, out num2).Height;
						this.rowheights[i] = ((this.rowheights[i] < height) ? height : this.rowheights[i]);
					}
					else
					{
						this.colwidths[j] = ((this.colwidths[j] < sizeF3.Width) ? sizeF3.Width : this.colwidths[j]);
						this.rowheights[i] = ((this.rowheights[i] < sizeF3.Height) ? sizeF3.Height : this.rowheights[i]);
					}
				}
			}
			this.pagesets = new List<DGVPrinter.PageDef>();
			this.pagesets.Add(new DGVPrinter.PageDef(this.PrintMargins, this.colstoprint.Count));
			int num3 = 0;
			this.pagesets[num3].coltotalwidth = this.rowheaderwidth;
			for (int i = 0; i < this.colstoprint.Count; i++)
			{
				float num4 = (this.colwidthsoverride[i] >= 0f) ? this.colwidthsoverride[i] : this.colwidths[i];
				if ((float)this.printWidth < this.pagesets[num3].coltotalwidth + num4 && i != 0)
				{
					this.pagesets.Add(new DGVPrinter.PageDef(this.PrintMargins, this.colstoprint.Count));
					num3++;
					this.pagesets[num3].coltotalwidth = this.rowheaderwidth;
				}
				this.pagesets[num3].colstoprint.Add(this.colstoprint[i]);
				this.pagesets[num3].colwidths.Add(this.colwidths[i]);
				this.pagesets[num3].colwidthsoverride.Add(this.colwidthsoverride[i]);
				this.pagesets[num3].coltotalwidth += num4;
			}
			for (int i = 0; i < this.pagesets.Count; i++)
			{
				this.AdjustPageSets(g, this.pagesets[i]);
			}
		}

		private void AdjustPageSets(Graphics g, DGVPrinter.PageDef pageset)
		{
			float num = this.rowheaderwidth;
			float num2 = 0f;
			for (int i = 0; i < pageset.colwidthsoverride.Count; i++)
			{
				if (pageset.colwidthsoverride[i] >= 0f)
				{
					num += pageset.colwidthsoverride[i];
				}
			}
			for (int i = 0; i < pageset.colwidths.Count; i++)
			{
				if (pageset.colwidthsoverride[i] < 0f)
				{
					num2 += pageset.colwidths[i];
				}
			}
			float num3;
			if (this.porportionalcolumns && 0f < num2)
			{
				num3 = ((float)this.printWidth - num) / num2;
			}
			else
			{
				num3 = 1f;
			}
			pageset.coltotalwidth = this.rowheaderwidth;
			for (int i = 0; i < pageset.colwidths.Count; i++)
			{
				if (pageset.colwidthsoverride[i] >= 0f)
				{
					pageset.colwidths[i] = pageset.colwidthsoverride[i];
				}
				else
				{
					pageset.colwidths[i] = pageset.colwidths[i] * num3;
				}
				pageset.coltotalwidth += pageset.colwidths[i];
			}
			if (DGVPrinter.Alignment.Left == this.tablealignment)
			{
				pageset.margins.Right = this.pageWidth - pageset.margins.Left - (int)pageset.coltotalwidth;
				if (0 > pageset.margins.Right)
				{
					pageset.margins.Right = 0;
					return;
				}
			}
			else if (DGVPrinter.Alignment.Right == this.tablealignment)
			{
				pageset.margins.Left = this.pageWidth - pageset.margins.Right - (int)pageset.coltotalwidth;
				if (0 > pageset.margins.Left)
				{
					pageset.margins.Left = 0;
					return;
				}
			}
			else if (DGVPrinter.Alignment.Center == this.tablealignment)
			{
				pageset.margins.Left = (this.pageWidth - (int)pageset.coltotalwidth) / 2;
				if (0 > pageset.margins.Left)
				{
					pageset.margins.Left = 0;
				}
				pageset.margins.Right = pageset.margins.Left;
			}
		}

		private int TotalPages()
		{
			int num = 1;
			float num2 = 0f;
			float num3 = (float)this.pageHeight - this.headerHeight - this.footerHeight - (float)this.PrintMargins.Top - (float)this.PrintMargins.Bottom;
			for (int i = 0; i < this.rowheights.Count; i++)
			{
				if (num2 + this.rowheights[i] > num3)
				{
					num++;
					num2 = 0f;
				}
				num2 += this.rowheights[i];
			}
			return num;
		}

		private bool DetermineHasMorePages()
		{
			this.currentpageset++;
			return this.currentpageset < this.pagesets.Count;
		}

		private bool PrintPage(Graphics g)
		{
			bool flag = false;
			float num = (float)this.pagesets[this.currentpageset].margins.Top;
			this.CurrentPage++;
			if (this.CurrentPage >= this.fromPage && this.CurrentPage <= this.toPage)
			{
				flag = true;
			}
			float num2 = (float)this.pageHeight - this.footerHeight - (float)this.pagesets[this.currentpageset].margins.Bottom;
			float num3;
			bool result;
			while (!flag)
			{
				num = (float)this.pagesets[this.currentpageset].margins.Top + this.headerHeight;
				bool flag2 = false;
				num3 = ((this.lastrowprinted < this.rowheights.Count) ? this.rowheights[this.lastrowprinted + 1] : 0f);
				while (!flag2)
				{
					if (this.lastrowprinted >= this.rowstoprint.Count - 1)
					{
						flag2 = true;
					}
					else if (num + num3 >= num2)
					{
						flag2 = true;
					}
					else
					{
						this.lastrowprinted++;
						num += this.rowheights[this.lastrowprinted];
						num3 = ((this.lastrowprinted + 1 < this.rowheights.Count) ? this.rowheights[this.lastrowprinted + 1] : 0f);
					}
				}
				this.CurrentPage++;
				if (this.CurrentPage >= this.fromPage && this.CurrentPage <= this.toPage)
				{
					flag = true;
				}
				if (this.lastrowprinted >= this.rowstoprint.Count - 1 || this.CurrentPage > this.toPage)
				{
					result = this.DetermineHasMorePages();
					this.lastrowprinted = -1;
					this.CurrentPage = 0;
					return result;
				}
			}
			num = (float)this.pagesets[this.currentpageset].margins.Top;
			if (this.PrintHeader)
			{
				if (this.pagenumberontop && this.pageno)
				{
					string text = this.pagetext + this.CurrentPage.ToString(CultureInfo.CurrentCulture);
					if (this.showtotalpagenumber)
					{
						text = text + this.pageseparator + this.totalpages.ToString();
					}
					if (1 < this.pagesets.Count)
					{
						text = text + this.parttext + (this.currentpageset + 1).ToString(CultureInfo.CurrentCulture);
					}
					this.printsection(g, ref num, text, this.pagenofont, this.pagenocolor, this.pagenumberformat, this.overridepagenumberformat, this.pagesets[this.currentpageset].margins);
					if (!this.pagenumberonseparateline)
					{
						num -= this.pagenumberHeight;
					}
				}
				if (!string.IsNullOrEmpty(this.title))
				{
					this.printsection(g, ref num, this.title, this.titlefont, this.titlecolor, this.titleformat, this.overridetitleformat, this.pagesets[this.currentpageset].margins);
				}
				if (!string.IsNullOrEmpty(this.subtitle))
				{
					this.printsection(g, ref num, this.subtitle, this.subtitlefont, this.subtitlecolor, this.subtitleformat, this.overridesubtitleformat, this.pagesets[this.currentpageset].margins);
				}
			}
			if (this.PrintColumnHeaders)
			{
				this.printcolumnheaders(g, ref num, this.pagesets[this.currentpageset]);
			}
			num3 = ((this.lastrowprinted < this.rowheights.Count) ? this.rowheights[this.lastrowprinted + 1] : 0f);
			while (num + num3 < num2)
			{
				this.lastrowprinted++;
				this.printrow(g, ref num, (DataGridViewRow)this.rowstoprint[this.lastrowprinted], this.pagesets[this.currentpageset]);
				if (this.lastrowprinted >= this.rowstoprint.Count - 1)
				{
					this.printfooter(g, ref num, this.pagesets[this.currentpageset].margins);
					result = this.DetermineHasMorePages();
					this.lastrowprinted = -1;
					this.CurrentPage = 0;
					return result;
				}
				num3 = ((this.lastrowprinted < this.rowheights.Count) ? this.rowheights[this.lastrowprinted + 1] : 0f);
			}
			if (this.PrintFooter)
			{
				this.printfooter(g, ref num, this.pagesets[this.currentpageset].margins);
			}
			if (this.CurrentPage >= this.toPage)
			{
				result = this.DetermineHasMorePages();
				this.lastrowprinted = -1;
				this.CurrentPage = 0;
			}
			else
			{
				result = true;
			}
			return result;
		}

		private void printsection(Graphics g, ref float pos, string text, Font font, Color color, StringFormat format, bool useroverride, Margins margins)
		{
			SizeF sizeF = g.MeasureString(text, font, this.printWidth, format);
			RectangleF layoutRectangle = new RectangleF((float)margins.Left, pos, (float)this.printWidth, sizeF.Height);
			this.SolidBrush1 = new SolidBrush(color);
			g.DrawString(text, font, this.SolidBrush1, layoutRectangle, format);
			pos += sizeF.Height;
		}

		private void printfooter(Graphics g, ref float pos, Margins margins)
		{
			pos = (float)this.pageHeight - this.footerHeight - (float)margins.Bottom;
			pos += this.footerspacing;
			this.printsection(g, ref pos, this.footer, this.footerfont, this.footercolor, this.footerformat, this.overridefooterformat, margins);
			if (!this.pagenumberontop && this.pageno)
			{
				string text = this.pagetext + this.CurrentPage.ToString(CultureInfo.CurrentCulture);
				if (this.showtotalpagenumber)
				{
					text = text + this.pageseparator + this.totalpages.ToString();
				}
				if (1 < this.pagesets.Count)
				{
					text = text + this.parttext + (this.currentpageset + 1).ToString(CultureInfo.CurrentCulture);
				}
				if (!this.pagenumberonseparateline)
				{
					pos -= this.pagenumberHeight;
				}
				this.printsection(g, ref pos, text, this.pagenofont, this.pagenocolor, this.pagenumberformat, this.overridepagenumberformat, margins);
			}
		}

		private void printcolumnheaders(Graphics g, ref float pos, DGVPrinter.PageDef pageset)
		{
			float num = (float)pageset.margins.Left + this.rowheaderwidth;
			this.lines = new Pen(this.dgv.GridColor, 1f);
			for (int i = 0; i < pageset.colstoprint.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)pageset.colstoprint[i];
				float width = (pageset.colwidths[i] > (float)this.printWidth - this.rowheaderwidth) ? ((float)this.printWidth - this.rowheaderwidth) : pageset.colwidths[i];
				DataGridViewCellStyle inheritedStyle = dataGridViewColumn.HeaderCell.InheritedStyle;
				RectangleF rectangleF = new RectangleF(num, pos, width, this.colheaderheight);
				g.FillRectangle(this.SolidBrush1 = new SolidBrush(inheritedStyle.BackColor), rectangleF);
				g.DrawString(dataGridViewColumn.HeaderText, inheritedStyle.Font, this.SolidBrush1 = new SolidBrush(inheritedStyle.ForeColor), rectangleF, this.headercellformat);
				if (this.dgv.ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
				{
					g.DrawRectangle(this.lines, num, pos, width, this.colheaderheight);
				}
				num += pageset.colwidths[i];
			}
			pos += this.colheaderheight + ((this.dgv.ColumnHeadersBorderStyle != DataGridViewHeaderBorderStyle.None) ? this.lines.Width : 0f);
		}

		private void printrow(Graphics g, ref float pos, DataGridViewRow row, DGVPrinter.PageDef pageset)
		{
			float num = (float)pageset.margins.Left;
			this.lines = new Pen(this.dgv.GridColor, 1f);
			DataGridViewCellStyle inheritedStyle = row.InheritedStyle;
			float width = (pageset.coltotalwidth > (float)this.printWidth) ? ((float)this.printWidth) : pageset.coltotalwidth;
			RectangleF rect = new RectangleF(num, pos, width, this.rowheights[this.lastrowprinted]);
			this.SolidBrush1 = new SolidBrush(inheritedStyle.BackColor);
			g.FillRectangle(this.SolidBrush1, rect);
			if (this.dgv.RowHeadersVisible)
			{
				DataGridViewCellStyle inheritedStyle2 = row.HeaderCell.InheritedStyle;
				RectangleF rectangleF = new RectangleF(num, pos, this.rowheaderwidth, this.rowheights[this.lastrowprinted]);
				this.SolidBrush1 = new SolidBrush(inheritedStyle2.BackColor);
				g.FillRectangle(this.SolidBrush1, rectangleF);
				g.DrawString(row.HeaderCell.EditedFormattedValue.ToString(), inheritedStyle2.Font, this.SolidBrush1 = new SolidBrush(inheritedStyle2.ForeColor), rectangleF, this.headercellformat);
				if (this.dgv.RowHeadersBorderStyle != DataGridViewHeaderBorderStyle.None)
				{
					g.DrawRectangle(this.lines, num, pos, this.rowheaderwidth, this.rowheights[this.lastrowprinted]);
				}
				num += this.rowheaderwidth;
			}
			for (int i = 0; i < pageset.colstoprint.Count; i++)
			{
				DataGridViewColumn dataGridViewColumn = (DataGridViewColumn)pageset.colstoprint[i];
				string text = row.Cells[dataGridViewColumn.Index].EditedFormattedValue.ToString();
				float width2 = (pageset.colwidths[i] > (float)this.printWidth - this.rowheaderwidth) ? ((float)this.printWidth - this.rowheaderwidth) : pageset.colwidths[i];
				StringFormat format = null;
				DataGridViewCellStyle dataGridViewCellStyle;
				if (this.ColumnStyles.ContainsKey(dataGridViewColumn.Name))
				{
					dataGridViewCellStyle = this.colstyles[dataGridViewColumn.Name];
					this.buildstringformat(ref format, dataGridViewCellStyle, this.cellformat.Alignment, this.cellformat.LineAlignment, this.cellformat.FormatFlags, this.cellformat.Trimming);
					Font arg_246_0 = dataGridViewCellStyle.Font;
				}
				else if (dataGridViewColumn.HasDefaultCellStyle || row.Cells[dataGridViewColumn.Index].HasStyle)
				{
					dataGridViewCellStyle = row.Cells[dataGridViewColumn.Index].InheritedStyle;
					this.buildstringformat(ref format, dataGridViewCellStyle, this.cellformat.Alignment, this.cellformat.LineAlignment, this.cellformat.FormatFlags, this.cellformat.Trimming);
					Font arg_2C4_0 = dataGridViewCellStyle.Font;
				}
				else
				{
					format = this.cellformat;
					dataGridViewCellStyle = row.Cells[dataGridViewColumn.Index].InheritedStyle;
				}
				RectangleF rectangleF2 = new RectangleF(num, pos, width2, this.rowheights[this.lastrowprinted]);
				g.FillRectangle(this.SolidBrush1 = new SolidBrush(dataGridViewCellStyle.BackColor), rectangleF2);
				if ("DataGridViewImageCell" == dataGridViewColumn.CellType.Name)
				{
					this.DrawImageCell(g, (DataGridViewImageCell)row.Cells[dataGridViewColumn.Index], rectangleF2);
				}
				else
				{
					if ("DataGridViewCheckBoxCell" == dataGridViewColumn.CellType.Name)
					{
						if (text == "True")
						{
							text = "√";
						}
						else
						{
							text = " ";
						}
					}
					g.DrawString(text, dataGridViewCellStyle.Font, this.SolidBrush1 = new SolidBrush(dataGridViewCellStyle.ForeColor), rectangleF2, format);
				}
				if (this.dgv.CellBorderStyle != DataGridViewCellBorderStyle.None)
				{
					g.DrawRectangle(this.lines, num, pos, width2, this.rowheights[this.lastrowprinted]);
				}
				num += pageset.colwidths[i];
			}
			pos += this.rowheights[this.lastrowprinted];
		}

		private void DrawImageCell(Graphics g, DataGridViewImageCell imagecell, RectangleF rectf)
		{
			Image image = (Image)imagecell.Value;
			Rectangle r = default(Rectangle);
			int num;
			int num2;
			if (DataGridViewImageCellLayout.Normal == imagecell.ImageLayout || imagecell.ImageLayout == DataGridViewImageCellLayout.NotSet)
			{
				num = image.Width - (int)rectf.Width;
				num2 = image.Height - (int)rectf.Height;
				if (0 > num)
				{
					rectf.Width = (float)(r.Width = image.Width);
				}
				else
				{
					r.Width = (int)rectf.Width;
				}
				if (0 > num2)
				{
					rectf.Height = (float)(r.Height = image.Height);
				}
				else
				{
					r.Height = (int)rectf.Height;
				}
			}
			else if (DataGridViewImageCellLayout.Stretch == imagecell.ImageLayout)
			{
				r.Width = image.Width;
				r.Height = image.Height;
				num = 0;
				num2 = 0;
			}
			else
			{
				r.Width = image.Width;
				r.Height = image.Height;
				float num3 = rectf.Height / (float)r.Height;
				float num4 = rectf.Width / (float)r.Width;
				float num5;
				if (num3 > num4)
				{
					num5 = num4;
					num = 0;
					num2 = (int)((float)r.Height * num5 - rectf.Height);
				}
				else
				{
					num5 = num3;
					num2 = 0;
					num = (int)((float)r.Width * num5 - rectf.Width);
				}
				rectf.Width = (float)r.Width * num5;
				rectf.Height = (float)r.Height * num5;
			}
			DataGridViewContentAlignment alignment = imagecell.InheritedStyle.Alignment;
			if (alignment <= DataGridViewContentAlignment.MiddleCenter)
			{
				switch (alignment)
				{
				case DataGridViewContentAlignment.NotSet:
					if (0 > num2)
					{
						rectf.Y -= (float)(num2 / 2);
					}
					else
					{
						r.Y = num2 / 2;
					}
					if (0 > num)
					{
						rectf.X -= (float)(num / 2);
					}
					else
					{
						r.X = num / 2;
					}
					break;
				case DataGridViewContentAlignment.TopLeft:
					r.Y = 0;
					r.X = 0;
					break;
				case DataGridViewContentAlignment.TopCenter:
					r.Y = 0;
					if (0 > num)
					{
						rectf.X -= (float)(num / 2);
					}
					else
					{
						r.X = num / 2;
					}
					break;
				case (DataGridViewContentAlignment)3:
					break;
				case DataGridViewContentAlignment.TopRight:
					r.Y = 0;
					if (0 > num)
					{
						rectf.X -= (float)num;
					}
					else
					{
						r.X = num;
					}
					break;
				default:
					if (alignment != DataGridViewContentAlignment.MiddleLeft)
					{
						if (alignment == DataGridViewContentAlignment.MiddleCenter)
						{
							if (0 > num2)
							{
								rectf.Y -= (float)(num2 / 2);
							}
							else
							{
								r.Y = num2 / 2;
							}
							if (0 > num)
							{
								rectf.X -= (float)(num / 2);
							}
							else
							{
								r.X = num / 2;
							}
						}
					}
					else
					{
						if (0 > num2)
						{
							rectf.Y -= (float)(num2 / 2);
						}
						else
						{
							r.Y = num2 / 2;
						}
						r.X = 0;
					}
					break;
				}
			}
			else if (alignment <= DataGridViewContentAlignment.BottomLeft)
			{
				if (alignment != DataGridViewContentAlignment.MiddleRight)
				{
					if (alignment == DataGridViewContentAlignment.BottomLeft)
					{
						if (0 > num2)
						{
							rectf.Y -= (float)num2;
						}
						else
						{
							r.Y = num2;
						}
						r.X = 0;
					}
				}
				else
				{
					if (0 > num2)
					{
						rectf.Y -= (float)(num2 / 2);
					}
					else
					{
						r.Y = num2 / 2;
					}
					if (0 > num)
					{
						rectf.X -= (float)num;
					}
					else
					{
						r.X = num;
					}
				}
			}
			else if (alignment != DataGridViewContentAlignment.BottomCenter)
			{
				if (alignment == DataGridViewContentAlignment.BottomRight)
				{
					if (0 > num2)
					{
						rectf.Y -= (float)num2;
					}
					else
					{
						r.Y = num2;
					}
					if (0 > num)
					{
						rectf.X -= (float)num;
					}
					else
					{
						r.X = num;
					}
				}
			}
			else
			{
				if (0 > num2)
				{
					rectf.Y -= (float)num2;
				}
				else
				{
					r.Y = num2;
				}
				if (0 > num)
				{
					rectf.X -= (float)(num / 2);
				}
				else
				{
					r.X = num / 2;
				}
			}
			g.DrawImage(image, rectf, r, GraphicsUnit.Pixel);
		}
	}
}
