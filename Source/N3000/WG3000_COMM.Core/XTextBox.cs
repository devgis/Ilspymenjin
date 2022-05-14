using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace WG3000_COMM.Core
{
	public class XTextBox : TextBox
	{
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public int LinesCount()
		{
			Message message = Message.Create(base.Handle, 186, IntPtr.Zero, IntPtr.Zero);
			base.DefWndProc(ref message);
			return message.Result.ToInt32();
		}
	}
}
