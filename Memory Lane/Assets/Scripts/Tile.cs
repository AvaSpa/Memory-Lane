using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsLane;
    public Color Color = Color.white;
    public bool IsLocked;

    private void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        var material = GetComponent<Renderer>().material;
        material.color = Color;
        if (IsLocked)
        {
            material.color = new Color(0.2f, 0.2f, 0.2f);
        }
    }
}
