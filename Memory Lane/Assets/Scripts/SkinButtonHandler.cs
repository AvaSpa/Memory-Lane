using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Utils;
using UnityEngine;

public class SkinButtonHandler : ClickAction
{
    public int SkinId;

    [HideInInspector]
    public SkinList Skins;
    [HideInInspector]
    public SkinListManager SkinListManager;

    [SerializeField]
    private GameObject[] _quads;

    private void Start()
    {
        var visual = gameObject.transform.GetChild(0).gameObject;
        var skin = GameObject.Instantiate(Skins.Skins[SkinId].Model, visual.transform);

        skin.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

        StartCoroutine(SpinHelper.WinSpin(skin.transform));
    }

    protected override void Act()
    {
        SetSkin();
        SkinListManager.MarkSelectedSkin(SkinId);
    }

    public void ToggleSelected(bool selected)
    {
        foreach (var quad in _quads)
        {
            var color = selected ? new Color(0.3f, 1f, 0f) : new Color(0.3f, 0.3f, 0.3f);

            var renderer = quad.GetComponent<Renderer>();
            var material = renderer.material;
            material.color = color;
        }
    }

    private void SetSkin()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentSkinKey, SkinId);
    }
}
