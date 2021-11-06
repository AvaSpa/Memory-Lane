using UnityEngine;

public class ExitDeleter : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        ToggleVisual(collision, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ToggleVisual(collision, true);
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
