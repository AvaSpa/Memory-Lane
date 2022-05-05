using Assets.Scripts.Utils;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsLane;
    public TileIdentity Identity;
    public bool IsLocked;
    public bool IsLast;
    public GameObject Base;
    public GameObject Symbol;

    private void Start()
    {
        UpdateVisuals();
    }

    public override string ToString()
    {
        return $"IsLane: {IsLane} - IsLocked: {IsLocked}";
    }

    public void UpdateVisuals()
    {
        UpdateBaseVisuals();
        UpdateSymbol();
    }

    private void UpdateBaseVisuals()
    {
        var material = Base.GetComponent<Renderer>().material;

        if (Identity != null)
            material.color = Identity.Color;

        if (IsLocked)
            material.color = new Color(0.2f, 0.2f, 0.2f);
    }

    private void UpdateSymbol()
    {
        var material = Symbol.GetComponent<Renderer>().material;

        if (Identity == null || Identity.Symbol == null)
            material.color = new Color(1.0f, 1.0f, 1.0f, 0f);
        else
        {
            material.mainTexture = Identity.Symbol;
            material.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
        }
    }
}
