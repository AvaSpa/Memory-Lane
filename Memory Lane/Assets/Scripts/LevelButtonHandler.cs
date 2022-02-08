using Assets.Scripts.Utils;
using UnityEngine;

public class LevelButtonHandler : ClickAction
{
    public int LevelNumber;

    [HideInInspector]
    public SceneChanger SceneChanger;

    protected override void Act()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentLevelKey, LevelNumber);
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }
}
