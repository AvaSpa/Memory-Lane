using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class RotateOnSwipe : MonoBehaviour
{
    public float Speed = 1;

    public void OnSwipeHandler(string id)
    {
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
        transform.DORotate(new Vector3(0, left ? -90 : 90, 0), Speed, RotateMode.LocalAxisAdd);
    }
}
