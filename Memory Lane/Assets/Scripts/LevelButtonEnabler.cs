using UnityEngine;

public class LevelButtonEnabler : MonoBehaviour
{
    public void SetEnabled(GameObject gameObject, bool enabled)
    {
        ToggleVisual(gameObject, enabled);
        ToggleAction(gameObject, enabled);
    }

    private void OnCollisionExit(Collision collision)
    {
        SetEnabled(collision.gameObject, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetEnabled(collision.gameObject, true);
    }

    private void ToggleAction(GameObject gameObject, bool enabled)
    {
        var tag = gameObject.tag;

        if (tag.Contains("Level"))
        {
            var script = gameObject.GetComponentInChildren<NumberTagger>();
            script.ClickEnabled = enabled;
        }
    }

    private static void ToggleVisual(GameObject gameObject, bool visible)
    {
        var tag = gameObject.tag;

        if (tag.Contains("Level"))
        {
            var visual = gameObject.transform.GetChild(0).gameObject;
            visual.SetActive(visible);
        }
    }
}
