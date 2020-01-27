using UnityEngine;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// TODO: Use this only internally to determine if tile is lane. Set the transparency of the color outside where it is decided if it's lane or not.
    /// </summary>
    public bool IsLane;
    public Color Color = Color.white;

    private void Start()
    {
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        if (!IsLane)
            Color.a = 0.35f;

        var material = GetComponent<Renderer>().material;
        material.color = Color;
        if (IsLane)
        {
            material.SetColor("_EmissionColor", Color);
            material.EnableKeyword("_EMISSION");
        }
    }
}
