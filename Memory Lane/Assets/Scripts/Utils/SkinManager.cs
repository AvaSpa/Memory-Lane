using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class SkinManager
    {
        public static int GetMaxSkin(int availableSkinCount)
        {
            var currentSkin = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentSkinKey, 0);
            var maxLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);

            var maxSkin = (maxLevel - 10) / 5 + 1;
            if (maxSkin < currentSkin)
                maxSkin = currentSkin;
            if (maxSkin >= availableSkinCount)
                maxSkin = availableSkinCount - 1;

            PlayerPrefs.SetInt(PlayerPrefsKeys.MaxSkinKey, maxSkin);
            PlayerPrefs.Save();

            return maxSkin;
        }

        ///// <summary>
        ///// Gets the id of the skin that is unlocked by the current level.
        ///// </summary>
        ///// <param name="currentLevel">The current level</param>
        ///// <returns>the id of the unlocked skin if the current level unlocks a skin, -1 otherwise</returns>
        //public static int GetUnlockedSkinId(int currentLevel)
        //{
        //   return (currentLevel - 10) / 5 + 1;
        //}
    }
}
