using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public SkinList Skins;
    public Transform ButtonContainer;
    public GameObject SkinButtonPrefab;

    private List<SkinButtonHandler> _skinButtons = new List<SkinButtonHandler>();

    private void Start()
    {
        var currentSkin = PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentSkinKey, 0);
        var maxLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);

        var maxSkin = (maxLevel - 10) / 5 + 1;
        if (maxSkin < currentSkin)
            maxSkin = currentSkin;
        if (maxSkin >= Skins.Skins.Count)
            maxSkin = Skins.Skins.Count - 1;

        if (maxSkin == 0)
            AddButton(0);
        else
            for (var i = 0; i <= maxSkin; i++)
                AddButton(i);

        MarkSelectedSkin(currentSkin);
    }

    private void AddButton(int i)
    {
        const int buttonOffset = 20;

        var listItem = GameObject.Instantiate(SkinButtonPrefab, ButtonContainer, false);
        var position = listItem.transform.position;

        listItem.transform.position = new Vector3(position.x, position.y + buttonOffset - i * 15, position.z);

        var skinButtonHandler = listItem.GetComponent<SkinButtonHandler>();
        skinButtonHandler.SkinId = i;
        skinButtonHandler.Skins = Skins;
        skinButtonHandler.SkinManager = this;
        _skinButtons.Add(skinButtonHandler);

        SetVisibility(listItem, i);
    }

    private void SetVisibility(GameObject listItem, int index)
    {
        var enabler = GetComponentInChildren<ButtonEnabler>();

        if (index > 3) enabler.SetEnabled(listItem, false);
    }

    public void MarkSelectedSkin(int selectedId)
    {
        foreach (var skin in _skinButtons)
            skin.ToggleSelected(skin.SkinId == selectedId);
    }
}
