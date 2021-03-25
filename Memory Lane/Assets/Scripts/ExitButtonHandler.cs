using UnityEngine;

public class ExitButtonHandler : MonoBehaviour
{
    public SceneChanger SceneChanger;

    public void GoToMenu()
    {
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Menu);
    }
}
