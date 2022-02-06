using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public GameObject[] ListItems;
    public Transform ButtonContainer;
    public GameObject SkinButtonPrefab;

    private List<SkinButtonHandler> _skinButtons = new List<SkinButtonHandler>();

    private void Start()
    {
        const int buttonOffset = 20;

        for (var i = 0; i < ListItems.Length; i++)
        {
            var listItem = GameObject.Instantiate(SkinButtonPrefab, ButtonContainer, false);
            var position = listItem.transform.position;

            listItem.transform.position = new Vector3(position.x, position.y + buttonOffset - i * 15, position.z);

            var skinButtonHandler = listItem.GetComponent<SkinButtonHandler>();
            skinButtonHandler.SkinId = i;
            skinButtonHandler.PossibleSkins = ListItems;
            skinButtonHandler.SkinManager = this;
            _skinButtons.Add(skinButtonHandler);

            SetVisibility(listItem, i);
        }
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
