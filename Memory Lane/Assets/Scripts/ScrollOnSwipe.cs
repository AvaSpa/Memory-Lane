using GG.Infrastructure.Utils.Swipe;
using System.Collections;
using UnityEngine;

public class ScrollOnSwipe : MonoBehaviour
{
    public float Speed = 100;

    public bool IsScrolling => _scrolling;

    private bool _scrolling;
    private Vector3 _currentPosition;

    public void OnSwipeHandler(string id)
    {
        if (_scrolling) return;

        switch (id)
        {
            case DirectionId.ID_UP:
                StartCoroutine(Move(true));
                break;
            case DirectionId.ID_DOWN:
                StartCoroutine(Move(false));
                break;
        }
    }

    private void Start()
    {
        _currentPosition = transform.position;
    }

    private IEnumerator Move(bool up)
    {
        _scrolling = true;

        var newPosition = up ? _currentPosition.y + 20 : _currentPosition.y - 20;
        while (_currentPosition.y != newPosition)
        {
            _currentPosition.y = Mathf.MoveTowards(_currentPosition.y, newPosition, Speed * Time.deltaTime);
            transform.position = _currentPosition;
            yield return new WaitForEndOfFrame();
        }

        _scrolling = false;
    }
}
