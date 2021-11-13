using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class ScrollOnSwipe : MonoBehaviour
{
    public float Speed = 0.25f;

    public bool IsScrolling => _scrolling;

    private bool _scrolling;
    private Vector3 _currentPosition;//TODO: use this to control the bounds of the scroll

    public void OnSwipeHandler(string id)
    {
        if (_scrolling) return;

        switch (id)
        {
            case DirectionId.ID_UP:
                Move(true);
                break;
            case DirectionId.ID_DOWN:
                Move(false);
                break;
        }
    }

    private void Move(bool up)
    {
        _scrolling = true;

        var currentPos = transform.position;
        var newY = up ? currentPos.y + 20 : currentPos.y - 20;
        transform.DOMove(new Vector3(currentPos.x, newY, currentPos.z), Speed, true);

        _scrolling = false;
    }
}
