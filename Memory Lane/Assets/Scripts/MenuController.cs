using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public LevelList Levels;
    public RectTransform UiList;
    public GameObject UiListItem;
    public SceneChanger SceneChanger;

    private const string CurrentLevelKey = "CurrentLevel";
    private const string MaxLevelKey = "MaxLevel";

    private void Start()
    {
        var maxReachedLevel = PlayerPrefs.GetInt(MaxLevelKey, 1);
        var currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);
        if (maxReachedLevel < currentLevel)
        {
            maxReachedLevel = currentLevel;
            PlayerPrefs.SetInt(MaxLevelKey, maxReachedLevel);
        }

        for (var i = 0; i < maxReachedLevel; i++)
        {
            var listItem = GameObject.Instantiate(UiListItem, UiList);
            var itemText = listItem.GetComponentInChildren<Text>();
            itemText.text += i + 1;

            var button = listItem.GetComponent<Button>();
            var buttonLevel = i + 1;
            button.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt(CurrentLevelKey, buttonLevel);
                SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Main);
            });
        }
    }
}
