using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public TileGenerator Platform;
    public Mover Mover;
    public Text UiLabel;
    public float Duration = 5;

    private float timer;
    private bool shouldUpdate;

    private void Start()
    {
        timer = Duration;
        shouldUpdate = true; //TODO: reset this when switching levels if scene reloading is not done for that purpose
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
                Mover.StartMoving();
                UiLabel.text = string.Empty;
                shouldUpdate = false;
            }
        }
    }
}
