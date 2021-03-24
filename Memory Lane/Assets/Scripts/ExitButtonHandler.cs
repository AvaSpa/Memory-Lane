using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonHandler : MonoBehaviour
{
    public void Exit()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
