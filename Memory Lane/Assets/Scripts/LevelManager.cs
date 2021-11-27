using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelList Levels;
    public GameObject ListItem;
    public Transform ButtonContainer;

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
            var listItem = GameObject.Instantiate(ListItem, ButtonContainer, false); //TODO: it's not instantiated in the right place
            var position = listItem.transform.position;
            listItem.transform.position = new Vector3(position.x, position.y + buttonOffset - i * 10, position.z);

            var itemText = listItem.GetComponentInChildren<TextMeshPro>();
            itemText.text += i + 1;

            var tagger = ListItem.GetComponentInChildren<NumberTagger>();
            tagger.Number = i + 1;
        }
    }
}
