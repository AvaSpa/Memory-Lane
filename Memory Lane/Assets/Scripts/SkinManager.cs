using Assets.Scripts.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public SkinList Skins;
    public Transform ButtonContainer;
    public GameObject SkinButtonPrefab;

    private List<SkinButtonHandler> _skinButtons = new List<SkinButtonHandler>();

    private const string CurrentSkinKey = "CurrentSkin";

    private void Start()
    {
        const int buttonOffset = 20;

        for (var i = 0; i < Skins.Skins.Count; i++)
        {
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

        var currentSkinId = PlayerPrefs.GetInt(CurrentSkinKey, 0);
        MarkSelectedSkin(currentSkinId);
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
