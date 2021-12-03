using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelList Levels;
    public GameObject ListItem;
    public Transform ButtonContainer;
    public ScrollOnSwipe SwipeScroller;
    public SceneChanger SceneChanger;

    private const string CurrentLevelKey = "CurrentLevel";
    private const string MaxLevelKey = "MaxLevel";

    private void Start()
    {
        const int buttonOffset = 20;

        var maxReachedLevel = PlayerPrefs.GetInt(MaxLevelKey, 1);
        var currentLevel = PlayerPrefs.GetInt(CurrentLevelKey, 1);
        if (maxReachedLevel < currentLevel)
        {
            maxReachedLevel = currentLevel;
            PlayerPrefs.SetInt(MaxLevelKey, maxReachedLevel);
        }

        for (var i = 0; i < maxReachedLevel; i++)
        {
            var listItem = GameObject.Instantiate(ListItem, ButtonContainer, false);
            var position = listItem.transform.position;
            listItem.transform.position = new Vector3(position.x, position.y + buttonOffset - i * 10, position.z);

            var itemText = listItem.GetComponentInChildren<TextMeshPro>();
            itemText.text += i + 1;

            var levelButtonHandler = ListItem.GetComponentInChildren<LevelButtonHandler>();
            levelButtonHandler.LevelNumber = i + 1;
            levelButtonHandler.SwipeScroller = SwipeScroller;
            levelButtonHandler.SceneChanger = SceneChanger;

            SetVisibility(listItem, i);
        }
    }

    private void SetVisibility(GameObject listItem, int index)
    {
        var enabler = GetComponentInChildren<LevelButtonEnabler>();

        if (index > 4) enabler.SetEnabled(listItem, false);
    }
}
