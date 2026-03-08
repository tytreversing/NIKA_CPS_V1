using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

internal class RegistryOperations
{
	private static string iniPath;

	private static string keyName;

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	private static extern long WritePrivateProfileString(string string_0, string string_1, string string_2, string string_3);

	[DllImport("kernel32.DLL ", CharSet = CharSet.Auto)]
	private static extern int GetPrivateProfileInt(string string_0, string string_1, int int_0, string string_2);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	private static extern int GetPrivateProfileString(string string_0, string string_1, string string_2, StringBuilder stringBuilder_0, int int_0, string string_3);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern int GetPrivateProfileSectionNames(IntPtr intptr_0, int int_0, string string_0);

	[DllImport("kernel32.DLL ", CharSet = CharSet.Auto)]
	private static extern int GetPrivateProfileSection(string string_0, byte[] byte_0, int int_0, string string_1);

	private const string _SETUP = "Setup";

	public static string getIniFilePath()
	{
		return iniPath;
	}

	public static void setIniFilePath(string path)
	{
		iniPath = path;
	}


	public static string getProfileStringWithDefault(string key, string defval)
	{
		if (iniPath != null)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			GetPrivateProfileString(_SETUP, key, defval, stringBuilder, 1024, iniPath);
			return stringBuilder.ToString();
		}
		object value = Registry.GetValue(keyName, key, defval);
		if (value != null)
		{
			return (string)value;
		}
		return defval;
	}

	public static int getProfileIntWithDefault(string key, int defval)
	{
		if (iniPath != null)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			GetPrivateProfileString(_SETUP, key, defval.ToString(), stringBuilder, 1024, iniPath);
			try
			{
				return int.Parse(stringBuilder.ToString());
			}
			catch (Exception)
			{
				return defval;
			}
		}
		object value = Registry.GetValue(keyName, key, defval);
		if (value != null)
		{
			return (int)value;
		}
		return defval;
	}

	public static bool IsFlagSet(string key, bool defvalue = true)
	{
		int retval = getProfileIntWithDefault(key, defvalue ? 1 : 0);
		return retval != 0;
	}

	public static void SetFlag(string key, bool value)
	{
		WriteProfileInt(key, value ? 1 : 0);
	}


    public static void WriteProfileString(string key, string value)
	{
		if (iniPath != null)
		{
			WritePrivateProfileString(_SETUP, key, value, iniPath);
		}
		else
		{
			Registry.SetValue(keyName, key, value, RegistryValueKind.String);
		}
	}

	public static void WriteProfileInt(string key, int value)
	{
		if (iniPath != null)
		{
			WritePrivateProfileString(_SETUP, key, value.ToString(), iniPath);
		}
		else
		{
			Registry.SetValue(keyName, key, value, RegistryValueKind.DWord);
		}
	}

    
    static RegistryOperations()
	{
		keyName = "HKEY_CURRENT_USER\\Software\\NIKA";
		iniPath = null;
		if (File.Exists(Application.StartupPath + Path.DirectorySeparatorChar + "Setup.ini"))
		{
			iniPath = Application.StartupPath + Path.DirectorySeparatorChar + "Setup.ini";
		}
	}
}
