using System;

namespace WG3000_COMM.Core
{
	public class ControllerRunInformation : wgMjControllerRunInformation
	{
		protected internal new MjRec[] newSwipes = new MjRec[10];

		public void update(byte[] wgpktData, int startIndex, uint ControllerSN)
		{
			base.UpdateInfo(wgpktData, startIndex, ControllerSN);
			for (int i = 0; i < 10; i++)
			{
				if (this.newSwipes[i] == null)
				{
					this.newSwipes[i] = new MjRec(wgpktData, (uint)(startIndex - 20 + 68 + i * 20), ControllerSN, BitConverter.ToUInt32(wgpktData, startIndex - 20 + 64 + i * 20));
				}
				else
				{
					this.newSwipes[i].Update(wgpktData, (uint)(startIndex - 20 + 68 + i * 20), ControllerSN, BitConverter.ToUInt32(wgpktData, startIndex - 20 + 64 + i * 20));
				}
			}
		}
	}
}
