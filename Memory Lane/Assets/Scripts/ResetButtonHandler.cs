using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButtonHandler : MonoBehaviour
{
    public GameController GameController;

    [HideInInspector]
    public bool Success;

    public void RestartLevel()
    {
        Debug.Log($"Restarting level with success: {Success}");
        //if(Success)
        //   send next level parameter; on scene loaded tile generator should generate the proper level based on this parameter

        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        gameObject.SetActive(false);
    }
}
