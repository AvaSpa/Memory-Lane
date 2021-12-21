using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public TileGenerator Platform;
    public Mover Mover;
    public Text UiLabel;
    public GameObject Panel;
    public GameController GameController;
    public TutorialController TutorialController;

    public float Duration = 5;

    private float timer;
    private bool shouldUpdate;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        if (GameController.CurrentLevel == 1)
            timer = 10;
        else
            timer = Duration;

        shouldUpdate = true;
    }

    private void Update()
    {
        if (shouldUpdate)
        {
            timer -= Time.deltaTime;
            UiLabel.text = timer.ToString("0");

            if (timer <= 0)
            {
                Platform.HideLane();
                UiLabel.text = string.Empty;
                Panel.SetActive(true);
                TutorialController.ShowSecondMessage();
                shouldUpdate = false;
            }
        }
    }
}
