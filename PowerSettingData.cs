using System.Runtime.InteropServices;

namespace NIKA_CPS_V1
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PowerSettingData
    {
    	public byte lowPower;

    	public byte highPower;
    }
}
