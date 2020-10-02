using System;
using UnityEngine;

public static class StorageSystem
{
	//const string _LevelPrefix = "level_";

	//const string _LastVisitedLevelKey = "last_visited_level";

	//const string _AllLevelCompletedShown = "all_level_completed_shown";

	//public static bool IsLevelCompleted(int number)
	//{
	//	return PlayerPrefs.GetInt(_LevelPrefix + number, 0) == 1;
	//}

	//public static void CompleteLevel(int number)
	//{
	//	PlayerPrefs.SetInt(_LevelPrefix + number, 1);
	//	PlayerPrefs.Save();
	//}

	//public static void ResetAllProgress()
	//{
	//	// Delete all prefs but preserve settings' values.
	//	var selectedLanguage = Settings.GetSelectedLanguage();

	//	PlayerPrefs.DeleteAll();
	//	PlayerPrefs.Save();

	//	Settings.SetSelectedLanguage(selectedLanguage);
	//}

	///// <returns>The last visited level number or -1 if failed</returns>
	//public static int LastVisitedLevel()
	//{
	//	return PlayerPrefs.GetInt(_LastVisitedLevelKey, -1);
	//}

	//public static void SetLastVisitedLevel(int number)
	//{
	//	PlayerPrefs.SetInt(_LastVisitedLevelKey, number);
	//	PlayerPrefs.Save();
	//}

	//public static bool IsAllLevelCompletedShown()
	//{
	//	return PlayerPrefs.GetInt(_AllLevelCompletedShown, 0) == 1;
	//}

	//public static void SetAllLevelCompletedShown(bool value)
	//{
	//	PlayerPrefs.SetInt(_AllLevelCompletedShown, value ? 1 : 0);
	//	PlayerPrefs.Save();
	//}

	public static class Settings
	{
		const string _SettingsSelectedLanguage = "settings_selected_language";

		public static string GetSelectedLanguage()
		{
			return PlayerPrefs.GetString(_SettingsSelectedLanguage, null);
		}

		public static void SetSelectedLanguage(string language)
		{
			PlayerPrefs.SetString(_SettingsSelectedLanguage, language);
			PlayerPrefs.Save();
		}
	}
}