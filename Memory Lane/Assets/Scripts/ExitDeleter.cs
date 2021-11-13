using UnityEngine;

public class ExitDeleter : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        ToggleVisual(collision, false);
        ToggleAction(collision, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ToggleVisual(collision, true);
        ToggleAction(collision, true);
    }

    private void ToggleAction(Collision collision, bool enabled)
    {
        var tag = collision.gameObject.tag;

        if (tag.Contains("Level"))
        {
            var script = collision.gameObject.GetComponent<NumberTagger>();
            script.ClickEnabled = enabled;
        }
    }

    private static void ToggleVisual(Collision collision, bool visible)
    {
        var tag = collision.gameObject.tag;

        if (tag.Contains("Level"))
        {
            var visual = collision.gameObject.transform.GetChild(0).gameObject;
            visual.SetActive(visible);
        }
    }
}
