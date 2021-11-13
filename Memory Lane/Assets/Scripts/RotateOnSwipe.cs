using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class RotateOnSwipe : MonoBehaviour
{
    public float Speed = 1;

    private bool _rotating;
    private Vector3 _currentEuler; //TODO: use this to determine if levels are in view in order to control the scroll

    //TODO: not enough; need to hook to swipe start and end events
    public bool IsRotating => _rotating;

    public void OnSwipeHandler(string id)
    {
        if (_rotating) return;

        switch (id)
        {
            case DirectionId.ID_LEFT:
                Turn(true);
                break;
            case DirectionId.ID_RIGHT:
                Turn(false);
                break;
        }
    }

    private void Turn(bool left)
    {
        _rotating = true;

        transform.DORotate(new Vector3(0, left ? -90 : 90, 0), Speed, RotateMode.LocalAxisAdd);

        _rotating = false;
    }
}
