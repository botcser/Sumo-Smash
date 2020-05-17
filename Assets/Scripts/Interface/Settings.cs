using Assets.Scripts.Data;
using Assets.SimpleLocalization;
using UnityEngine.UI;

namespace Assets.Scripts.Interface
{
    public class Settings : BaseInterface
    {
        public Text LanguageText;

        public static Settings Instance;
        
        public void Awake()
        {
            Instance = this;
        }

        public void SetLanguage(string language)
        {
            Profile.Instance.Settings.Language = language;
            LocalizationManager.Language = language;
            Profile.Instance.Save();
            LocalizationManager.Localize("Congrat.Winner", "RedPlayer");
            LanguageText.text = LocalizationManager.Localize("Language." + language);
        }
    }
}
