using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LanguageSelectorManager : MonoBehaviour
{
    public static LanguageSelectorManager instance { get; private set; }

    bool active = false;
    int languageID;



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        if (PlayerPrefs.GetString("Busan_Player_ID").Equals(""))
        {
            if (Application.systemLanguage.Equals(SystemLanguage.Korean))
                PlayerPrefs.SetString("Busan_Language", "KO");
            else if (Application.systemLanguage.Equals(SystemLanguage.English))
                PlayerPrefs.SetString("Busan_Language", "EN");
        }

        if (PlayerPrefs.GetString("Busan_Language").Equals("KO"))
            languageID = 1;
        else if (PlayerPrefs.GetString("Busan_Language").Equals("EN")) 
            languageID = 0;

        ChanageLanguage(languageID);
    }

    public void ChanageLanguage(int _languageID)
    {
        if (active.Equals(true))
            return;
        StartCoroutine(SetLanguage(_languageID));
    }

    IEnumerator SetLanguage(int _languageID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_languageID];

        if (_languageID.Equals(1))
            PlayerPrefs.SetString("Busan_Language", "KO");
        else if (_languageID.Equals(0))
            PlayerPrefs.SetString("Busan_Language", "EN");
        active = false;
    }


    public void EndPanelClose()
    {
        Application.Quit();
    }
}
