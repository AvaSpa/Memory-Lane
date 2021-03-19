using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public GameObject ResetButton;
    public CountDown CountDown;
    public int CurrentLevel;

    private const string LevelKey = "Level";

    private void Awake()
    {
        CurrentLevel = PlayerPrefs.GetInt(LevelKey, 1);
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
        CurrentLevel++;
        PlayerPrefs.SetInt(LevelKey, CurrentLevel);
        PlayerPrefs.Save();

        var button = ResetButton.GetComponent<Button>();
        var text = button.GetComponentInChildren<Text>();
        text.text = "Next";

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
