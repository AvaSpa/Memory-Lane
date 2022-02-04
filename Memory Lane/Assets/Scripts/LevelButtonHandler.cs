using UnityEngine;

public class LevelButtonHandler : ClickAction
{
    public int LevelNumber;

    [HideInInspector]
    public SceneChanger SceneChanger;

    private const string CurrentLevelKey = "CurrentLevel";

    protected override void Act()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        PlayerPrefs.SetInt(CurrentLevelKey, LevelNumber);
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }
}
