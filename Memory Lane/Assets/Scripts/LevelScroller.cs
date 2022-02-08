using Assets.Scripts.Utils;
using DG.Tweening;
using GG.Infrastructure.Utils.Swipe;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public float Speed = 0.25f;
    public Transform CameraSupport;

    public void OnSwipeHandler(string id)
    {
        var cameraSupportRotation = CameraSupport.rotation.eulerAngles;
        if (cameraSupportRotation.y + 0.01f < 270f || cameraSupportRotation.y - 0.1f > 270) return;

        switch (id)
        {
            case DirectionId.ID_UP:
                var maxReachedLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.MaxLevelKey, 1);
                var maxIsEven = maxReachedLevel % 2 == 0;
                var limit = -1 * (20 - maxReachedLevel * 10) - (maxIsEven ? 20 : 40);
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
