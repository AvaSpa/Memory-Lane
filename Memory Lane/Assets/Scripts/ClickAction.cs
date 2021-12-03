using UnityEngine;

public abstract class ClickAction : MonoBehaviour
{
    private float _mouseDownTime;

    private void OnMouseDown()
    {
        _mouseDownTime = Time.deltaTime;
    }

    private void OnMouseUp()
    {
        var currentTime = Time.deltaTime;
        var timeHeld = currentTime - _mouseDownTime;
        if (timeHeld > 0.1) return; //TODO: doesn't work

        Act();
    }

    protected abstract void Act();
}
