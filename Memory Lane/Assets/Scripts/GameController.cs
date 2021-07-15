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

        CurrentLevel++;
        PlayerPrefs.SetInt(CurrentLevelKey, CurrentLevel);

        var currentMaxLevel = PlayerPrefs.GetInt(MaxLevelKey, 1);
        if (currentMaxLevel <= CurrentLevel)
            PlayerPrefs.SetInt(MaxLevelKey, CurrentLevel);

        PlayerPrefs.Save();

        var buttonScript = ResetButton.GetComponent<ResetButtonHandler>();
        buttonScript.Next.SetActive(true);
        buttonScript.Restart.SetActive(false);

        Panel.SetActive(false);
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
