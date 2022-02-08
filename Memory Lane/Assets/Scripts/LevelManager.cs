using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelList Levels;
    public GameObject[] ListItems;
    public Transform ButtonContainer;
    public SceneChanger SceneChanger;

    private void Start()
    {
        const int buttonOffset = 20;

        var maxReachedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);
        var currentLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentLevelKey, 1);
        if (maxReachedLevel < currentLevel)
        {
            maxReachedLevel = currentLevel;
            PlayerPrefs.SetInt(PlayerPrefsKeys.MaxLevelKey, maxReachedLevel);
        }

        for (var i = 0; i < maxReachedLevel; i++)
        {
            var prefabIndex = i % ListItems.Length;
            var prefab = ListItems[prefabIndex];

            var listItem = GameObject.Instantiate(prefab, ButtonContainer, false);
            var position = listItem.transform.position;
            listItem.transform.position = new Vector3(position.x, position.y + buttonOffset - i * 10, position.z);

            var itemText = listItem.GetComponentInChildren<TextMeshPro>();
            itemText.text += i + 1;

            var levelButtonHandler = listItem.GetComponent<LevelButtonHandler>();
            levelButtonHandler.LevelNumber = i + 1;
            levelButtonHandler.SceneChanger = SceneChanger;

            SetVisibility(listItem, i);
        }
    }

    private void SetVisibility(GameObject listItem, int index)
    {
        var enabler = GetComponentInChildren<ButtonEnabler>();

        if (index > 4) enabler.SetEnabled(listItem, false);
    }
}
