using UnityEngine;

public abstract class ClickAction : MonoBehaviour
{
    private float _mouseDownTime;

    private void OnMouseDown()
    {
        _mouseDownTime = Time.time;
    }

    private void OnMouseUp()
    {
        var currentTime = Time.time;
        var timeHeld = currentTime - _mouseDownTime;
        if (timeHeld > 0.1f) return;

        Act();
    }

    protected abstract void Act();
}
