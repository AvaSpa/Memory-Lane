using UnityEngine;

public class ContinueButtonHandler : MonoBehaviour
{
    public SceneChanger SceneChanger;

    private const string CurrentLevelKey = "CurrentLevel";
    private const string MaxLevelKey = "MaxLevel";

    public void Continue()
    {
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }

    //TODO: get selected level from list and set that as current level before fading to main
}
