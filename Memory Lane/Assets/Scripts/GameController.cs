using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public GameObject ResetButton;
    public CountDown CountDown;

    public void EndGame(bool fail)
    {
        if (fail)
            GameOver();
        else
            LevelComplete();
    }

    private void LevelComplete()
    {
        var button = ResetButton.GetComponent<Button>();
        var text = button.GetComponentInChildren<Text>();
        text.text = "Next";
        var buttonScript = ResetButton.GetComponent<ResetButtonHandler>();
        buttonScript.Success = true;

        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }

    private void GameOver()
    {
        var buttonScript = ResetButton.GetComponent<ResetButtonHandler>();
        buttonScript.Success = false;

        Mover.Kill();
        Platform.Collapse();
        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }
}
