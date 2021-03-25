using UnityEngine;

public class ContinueButtonHandler : MonoBehaviour
{
    public SceneChanger SceneChanger;

    public void Continue()
    {
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }
}
