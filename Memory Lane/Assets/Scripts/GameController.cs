using UnityEngine;

public class GameController : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public GameObject ResetButton;
    public CountDown CountDown;

    public void EndGame()
    {
        Mover.Kill();
        Platform.Collapse();
        Panel.SetActive(false);
        ResetButton.SetActive(true);
    }

    public void ResetLevel()
    {
        CountDown.Initialize();
        Platform.GenerateTiles();
        Mover.Reset();
    }
}
