using UnityEngine;

public class ButtonEnabler : MonoBehaviour
{
    public string TagToHandle;

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

        if (tag.Contains(TagToHandle))
        {
            var script = gameObject.GetComponent<ClickAction>();

            script.ClickEnabled = enabled;
        }
    }

    private void ToggleVisual(GameObject gameObject, bool visible)
    {
        var tag = gameObject.tag;

        if (tag.Contains(TagToHandle))
        {
            var visual = gameObject.transform.GetChild(0).gameObject;
            visual.SetActive(visible);
        }
    }
}
