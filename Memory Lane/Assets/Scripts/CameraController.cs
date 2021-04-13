using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraController : MonoBehaviour
    {
        public float TranslationSpeed = 15.1f;
        public float RotationSpeed = 21.5f;

        private void Start()
        {
            StartCoroutine(MoveCameraToPlayPosition(TranslationSpeed));
            StartCoroutine(RotateCameraToPlayPosition(RotationSpeed));
        }

        private IEnumerator MoveCameraToPlayPosition(float speed = 1)
        {
            var destination = new Vector3(20, 40, -30);
            while (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator RotateCameraToPlayPosition(float speed = 1)
        {
            var destination = new Vector3(75, 0, 0);
            while (transform.rotation.eulerAngles != destination)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(destination), speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}