using UnityEngine;

public class SkinButtonHandler : ClickAction
{
    public int SkinId;

    [HideInInspector]
    public GameObject[] PossibleSkins;

    private const string CurrentSkinKey = "CurrentSkin";

    private void Start()
    {
        var visual = gameObject.transform.GetChild(0).gameObject;
        var skin = GameObject.Instantiate(PossibleSkins[SkinId], visual.transform);

        skin.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    protected override void Act()
    {
        SetSkin();
    }

    private void SetSkin()
    {
        PlayerPrefs.SetInt(CurrentSkinKey, SkinId);
    }
}
