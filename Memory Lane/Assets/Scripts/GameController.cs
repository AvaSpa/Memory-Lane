using Assets.Scripts;
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
        var availableSkinCount = Mover.Skins.Skins.Count;
        var maxSkin = SkinManager.GetMaxSkin(availableSkinCount);

        if (maxSkin > latestUnlockedSkin)
        {
            var playerVisualRenderer = Mover.Body.GetComponent<MeshRenderer>();
            playerVisualRenderer.enabled = false;

            var skin = Mover.Skins.Skins[maxSkin];
            var playerVisual = Mover.Body.transform;
            Instantiate(skin.Model, playerVisual);

            //TODO: show message just like when ending the game "new skin unlocked; go to menu to select it"
            Debug.Log("Congratulations! You have unlocked a new skin. Go to the main menu to switch skins.");
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
