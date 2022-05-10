using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsController : MonoBehaviour
{
    private const string GameId = "4611537";
    private const string PlacementId = "Interstitial_Android";
    private const int LevelInterval = 2;
    private const int MinimumLevel = 7;
    private const bool TestMode = false;

    private void Awake()
    {
        if (!Advertisement.isInitialized)
            Advertisement.Initialize(GameId, TestMode);
    }

    public void SignalLevelCompleted()
    {
        var maxReachedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);
        if (maxReachedLevel < MinimumLevel) return;

        var playedLevelCount = PlayerPrefs.GetInt(PlayerPrefsKeys.PlayedLevelCountKey, -1);
        if (playedLevelCount == 0 && Advertisement.isInitialized)
            Advertisement.Show(PlacementId);

        if (playedLevelCount >= 0)
            playedLevelCount++;
        else
            playedLevelCount = 1;
        PlayerPrefs.SetInt(PlayerPrefsKeys.PlayedLevelCountKey, playedLevelCount);
        PlayerPrefs.Save();

        if (playedLevelCount >= LevelInterval)
        {
            playedLevelCount = 0;
            PlayerPrefs.SetInt(PlayerPrefsKeys.PlayedLevelCountKey, playedLevelCount);
            PlayerPrefs.Save();
        }
    }
}
