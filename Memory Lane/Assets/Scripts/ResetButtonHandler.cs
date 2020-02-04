using UnityEngine;

public class ResetButtonHandler : MonoBehaviour
{
    public GameController GameController;

    public void RestartLevel()
    {
        GameController.ResetLevel();
        gameObject.SetActive(false);
    }
}
