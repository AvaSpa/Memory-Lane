using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class SkinScroller : MonoBehaviour
{
    public float Speed = 0.25f;
    public Transform CameraSupport;
    public SkinManager SkinManager;

    public void OnSwipeHandler(string id)
    {
        var cameraSupportRotation = CameraSupport.rotation.eulerAngles;
        if (cameraSupportRotation.y + 0.01f < 180f || cameraSupportRotation.y - 0.1f > 180f) return;

        switch (id)
        {
            case DirectionId.ID_UP:
                var skinCount = SkinManager.Skins.Skins.Count;
                var maxIsEven = skinCount % 2 == 0;
                var limit = -1 * (20 - skinCount * 15) - (maxIsEven ? 20 : 40);
                if (transform.position.y >= limit) return;

                Move(true);
                break;
            case DirectionId.ID_DOWN:
                if (transform.position.y == 0) return;
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
