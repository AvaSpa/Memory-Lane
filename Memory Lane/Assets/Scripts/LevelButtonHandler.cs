using UnityEngine;

public class LevelButtonHandler : ClickAction
{
    public int LevelNumber;
    public bool ClickEnabled = true;

    [HideInInspector]
    public ScrollOnSwipe SwipeScroller;
    [HideInInspector]
    public SceneChanger SceneChanger;

    private const string CurrentLevelKey = "CurrentLevel";

    protected override void Act()
    {
        LoadLevel();
    }

    private void LoadLevel()
    {
        if (!ClickEnabled) return;

        PlayerPrefs.SetInt(CurrentLevelKey, LevelNumber + 1);
        SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
    }
}
