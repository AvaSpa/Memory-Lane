using UnityEngine;

public class Tile : MonoBehaviour
{
    /// <summary>
    /// TODO: This is becoming obsolete. Remove and set the lane/not lane colors through the Color field
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
