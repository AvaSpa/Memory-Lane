using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject LeftTutorial;
    public GameObject RightTutorial;
    public GameController GameController;

    private void Start()
    {
        if (GameController.CurrentLevel > 1)
        {
            LeftTutorial.SetActive(false);
            RightTutorial.SetActive(false);
        }
    }

    public void ShowSecondMessage()
    {
        RightTutorial.SetActive(true);
    }
}
