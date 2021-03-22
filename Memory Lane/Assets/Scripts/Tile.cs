using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsLane;
    public Color Color = Color.white;
    public bool IsLocked;
    public bool IsLast;

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
        var material = GetComponent<Renderer>().material;
        if (IsLocked)
            material.color = new Color(0.2f, 0.2f, 0.2f);
        else
            material.color = Color;
    }
}
