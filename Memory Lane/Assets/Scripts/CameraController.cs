using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public float TranslationSpeed = 15.1f;
        public float RotationSpeed = 21.5f;

        public Transform PlayerVisual;

        private Vector3 PlayViewPosition = new Vector3(20, 40, -30);
        private Vector3 PlayViewRotation = new Vector3(75, 0, 0);
        private Vector3 PlayerViewRotation = new Vector3(0, 0, 0);

        private const int CameraPlayerDistance = 13;
        private const int CameraHeightDifference = 5;

        private void Start()
        {
            StartCoroutine(MoveCameraToPosition(PlayViewPosition, TranslationSpeed));
            StartCoroutine(RotateCameraToPosition(PlayViewRotation, RotationSpeed));
        }

        public void ShowPlayerInDetail()
        {
            var playerViewPosition = new Vector3(PlayerVisual.position.x, PlayerVisual.position.y + CameraHeightDifference, PlayerVisual.position.z - CameraPlayerDistance);
            StartCoroutine(MoveCameraToPosition(playerViewPosition, TranslationSpeed));
            StartCoroutine(RotateCameraToPosition(PlayerViewRotation, RotationSpeed));
        }

        private IEnumerator MoveCameraToPosition(Vector3 position, float speed = 1)
        {
            while (transform.position != position)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator RotateCameraToPosition(Vector3 rotation, float speed = 1)
        {
            while (transform.rotation.eulerAngles != rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(rotation), speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}