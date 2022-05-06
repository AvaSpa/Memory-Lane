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

    private int _playedLevelCount;
    private int _maxReachedLevel;


    private void Awake()
    {
        _maxReachedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);

        Advertisement.Initialize(GameId, TestMode);

        Debug.Log("Initialized ads.");
    }

    public void SignalLevelCompleted()
    {
        if (_maxReachedLevel < MinimumLevel) return;

        if (_playedLevelCount == 0 && Advertisement.isInitialized)
            Advertisement.Show(PlacementId);

        _playedLevelCount++;
        if (_playedLevelCount >= LevelInterval)
            _playedLevelCount = 0;
    }
}
