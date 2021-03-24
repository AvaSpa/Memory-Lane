using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButtonHandler : MonoBehaviour
{
    public GameController GameController;
    public GameObject Next;
    public GameObject Restart;

    public void StartLevel()
    {
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        gameObject.SetActive(false);
    }
}
