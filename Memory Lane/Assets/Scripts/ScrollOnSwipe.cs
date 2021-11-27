using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class ScrollOnSwipe : MonoBehaviour
{
    public float Speed = 0.25f;
    public Transform CameraSupport;

    private Vector3 _currentPosition; //TODO: use this to control the bounds of the scroll

    public void OnSwipeHandler(string id)
    {
        var cameraSupportRotation = CameraSupport.rotation.eulerAngles;
        if (cameraSupportRotation.y + 0.01f < 270f || cameraSupportRotation.y - 0.1f > 270) return;

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
        var currentPos = transform.position;
        var newY = up ? currentPos.y + 20 : currentPos.y - 20;
        transform.DOMove(new Vector3(currentPos.x, newY, currentPos.z), Speed, true);
    }
}
