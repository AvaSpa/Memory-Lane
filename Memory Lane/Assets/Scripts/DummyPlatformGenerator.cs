using UnityEngine;
using UnityEngine.Events;

public class DummyPlatformGenerator : MonoBehaviour
{
    public TileGenerator TileGenerator;

    private void Start()
    {
        if (TileGenerator.DoneGenerating == null)
            TileGenerator.DoneGenerating = new UnityEvent();

        TileGenerator.DoneGenerating.AddListener(DoneCallback);
    }

    private void DoneCallback()
    {
        TileGenerator.HideLane();

        transform.position = new Vector3(-40, -120, 0); //weird how this is needed
    }
}
