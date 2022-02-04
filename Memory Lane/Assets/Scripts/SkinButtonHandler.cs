using Assets.Scripts.Utils;
using UnityEngine;

public class SkinButtonHandler : ClickAction
{
    public int SkinId;

    [HideInInspector]
    public GameObject[] PossibleSkins;
    [HideInInspector]
    public SkinManager SkinManager;

    [SerializeField]
    private GameObject[] _quads;
    private const string CurrentSkinKey = "CurrentSkin";

    private void Start()
    {
        var visual = gameObject.transform.GetChild(0).gameObject;
        var skin = GameObject.Instantiate(PossibleSkins[SkinId], visual.transform);

        skin.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);

        StartCoroutine(SpinHelper.WinSpin(skin.transform));
    }

    protected override void Act()
    {
        SetSkin();
        SkinManager.MarkSelectedSkin(SkinId);
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
        PlayerPrefs.SetInt(CurrentSkinKey, SkinId);
    }
}
