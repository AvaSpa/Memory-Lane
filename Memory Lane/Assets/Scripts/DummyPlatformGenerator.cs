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
    }
}
