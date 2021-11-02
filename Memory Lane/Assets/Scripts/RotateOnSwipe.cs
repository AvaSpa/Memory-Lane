using GG.Infrastructure.Utils.Swipe;
using System.Collections;
using UnityEngine;

public class RotateOnSwipe : MonoBehaviour
{
    public float Speed = 100;

    private bool _rotating;
    private Vector3 _currentEuler;

    //TODO: not enough; need to hook to swipe start and end events
    public bool IsRotating => _rotating;

    public void OnSwipeHandler(string id)
    {
        if (_rotating) return;

        switch (id)
        {
            case DirectionId.ID_LEFT:
                StartCoroutine(Turn(true));
                break;
            case DirectionId.ID_RIGHT:
                StartCoroutine(Turn(false));
                break;
        }
    }

    private void Start()
    {
        _currentEuler = transform.eulerAngles;
    }

    private IEnumerator Turn(bool left)
    {
        _rotating = true;

        var newAngle = left ? _currentEuler.y - 90 : _currentEuler.y + 90;
        while (_currentEuler.y != newAngle)
        {
            _currentEuler.y = Mathf.MoveTowards(_currentEuler.y, newAngle, Speed * Time.deltaTime);
            transform.eulerAngles = _currentEuler;
            yield return new WaitForEndOfFrame();
        }

        _rotating = false;
    }
}
