using UnityEngine;

public abstract class ClickAction : MonoBehaviour
{
    private float _mouseDownTime;

    public bool ClickEnabled = true;


    private void OnMouseDown()
    {
        _mouseDownTime = Time.time;
    }

    private void OnMouseUp()
    {
        var currentTime = Time.time;
        var timeHeld = currentTime - _mouseDownTime;
        if (timeHeld > 0.1f) return;

        if (!ClickEnabled) return;

        Act();
    }

    protected abstract void Act();
}
