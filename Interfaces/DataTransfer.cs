namespace NIKA_CPS_V1;

public class DataTransfer
{
	public enum DataMode
	{
		DataModeNone,
		DataModeReadFlash,
		DataModeReadEEPROM,
		DataModeWriteFlash,
		DataModeWriteEEPROM,
		DataModeReadMCUROM,
		DataModeReadScreenGrab,
		DataModeWriteWAV,
		DataModeReadAMBE,
		DataModeReadRadioInfo,
		DataModeReadSecureRegisters,
		DataModeReadSettings,
		DataModeWriteSettings,
		DataModeReadFactoryCalibrations,
		DataModeReadBandlimits
	}

	public enum CPSAction
	{
		NONE,
		BACKUP_EEPROM,
		RESTORE_EEPROM,
		BACKUP_FLASH,
		RESTORE_FLASH,
		BACKUP_CALIBRATION,
		RESTORE_CALIBRATION,
		READ_CODEPLUG,
		WRITE_CODEPLUG,
		BACKUP_MCU_ROM,
		DOWNLOAD_SCREENGRAB,
		COMPRESS_AUDIO,
		WRITE_VOICE_PROMPTS,
		WRITE_SATELLITE_KEPS,
		BACKUP_SETTINGS,
		RESTORE_SETTINGS,
		READ_THEME,
		WRITE_THEME,
		READ_SECURE_REGISTERS,
		SAVE_NMEA_LOG,
		READ_SETTINGS,
		WRITE_SETTINGS
	}

	public DataMode mode;

	public CPSAction action;

	public int flashAddress;

	public int transferLength;

	public int localAddress;

	public int dataSector;

	public byte[] dataBuffer;

	public int responseCode;

	public DataTransfer(CPSAction theAction = CPSAction.NONE)
	{
		action = theAction;
	}
}
