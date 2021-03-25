using UnityEngine;

public class ResetButtonHandler : MonoBehaviour
{
    public GameController GameController;
    public GameObject Next;
    public GameObject Restart;
    public SceneChanger SceneChanger;

    public void StartLevel()
    {
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
        gameObject.SetActive(false);
    }
}
