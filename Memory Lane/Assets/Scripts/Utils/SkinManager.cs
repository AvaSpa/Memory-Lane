using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class SkinManager
    {
        public static int GetMaxSkin(int availableSkinCount)
        {
            var currentSkin = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentSkinKey, 0);
            var maxLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1) - 1;

            var maxSkin = (maxLevel - 10) / 5 + 1;
            if (maxSkin < currentSkin)
                maxSkin = currentSkin;
            if (maxSkin >= availableSkinCount)
                maxSkin = availableSkinCount - 1;

            PlayerPrefs.SetInt(PlayerPrefsKeys.MaxSkinKey, maxSkin);
            PlayerPrefs.Save();

            return maxSkin;
        }
    }
}
