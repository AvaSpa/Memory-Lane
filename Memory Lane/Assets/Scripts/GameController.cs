using Assets.Scripts;
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

    private const string CurrentLevelKey = "CurrentLevel";
    private const string MaxLevelKey = "MaxLevel";

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);
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
        PlayerPrefs.SetInt(CurrentLevelKey, CurrentLevel);

        var currentMaxLevel = PlayerPrefs.GetInt(MaxLevelKey, 1);
        if (currentMaxLevel <= CurrentLevel)
            PlayerPrefs.SetInt(MaxLevelKey, CurrentLevel);

        PlayerPrefs.Save();

        Panel.SetActive(false);

        if (CurrentLevel > Platform.Levels.Levels.Count)
        {
            EndMessage.SetActive(true);
            CurrentLevel--;
            PlayerPrefs.SetInt(CurrentLevelKey, CurrentLevel);
            PlayerPrefs.SetInt(MaxLevelKey, currentMaxLevel);
            return;
        }

        var buttonScript = ResetButton.GetComponent<ResetButtonHandler>();
        buttonScript.Next.SetActive(true);
        buttonScript.Restart.SetActive(false);

        ResetButton.SetActive(true);
    }

    private void GameOver()
    {
        Mover.Kill();
        Platform.Collapse();
        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }
}
