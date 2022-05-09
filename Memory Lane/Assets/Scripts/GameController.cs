using Assets.Scripts;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public GameObject ResetButton;
    public Text LevelText;
    public CountDown CountDown;
    public int CurrentLevel;
    public AudioSource WinMusic;
    public CameraController CameraController;
    public GameObject EndMessage;
    public AdsController AdsController;
    public SkinList Skins;

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey, 1);
        LevelText.text = $"Level: {CurrentLevel}";
    }

    public void EndGame(bool fail)
    {
        if (fail)
            GameOver();
        else
            LevelComplete();
    }

    private void LevelComplete()
    {
        WinMusic.Play();
        CameraController.ShowPlayerInDetail();
        Mover.StartWinAnimation();

        CurrentLevel++;
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelKey, CurrentLevel);

        var currentMaxLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);
        if (currentMaxLevel <= CurrentLevel)
            PlayerPrefs.SetInt(PlayerPrefsKeys.MaxLevelKey, CurrentLevel);

        PlayerPrefs.Save();

        Panel.SetActive(false);

        if (CurrentLevel > Platform.Levels.Levels.Count)
        {
            EndMessage.SetActive(true);
            CurrentLevel--;
            PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelKey, CurrentLevel);
            PlayerPrefs.SetInt(PlayerPrefsKeys.MaxLevelKey, currentMaxLevel);
            PlayerPrefs.Save();
            return;
        }

        var buttonScript = ResetButton.GetComponent<ResetButtonHandler>();
        buttonScript.Next.SetActive(true);
        buttonScript.Restart.SetActive(false);

        ResetButton.SetActive(true);

        AdsController.SignalLevelCompleted();

        var latestUnlockedSkin = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxSkinKey, 0);
        var availableSkinCount = Skins.Skins.Count;
        var maxSkin = SkinManager.GetMaxSkin(availableSkinCount);

        if (maxSkin > latestUnlockedSkin)
        {
            var skin = Skins.Skins[maxSkin];
            var playerVisual = Mover.Body.transform;
            var skinObject = Instantiate(skin.Model, playerVisual);
            skinObject.transform.localPosition = new Vector3(0, 10, 0);
        }
    }

    private void GameOver()
    {
        Mover.Kill();
        Platform.Collapse();
        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }
}
