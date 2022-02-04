using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class SpinHelper
    {
        public static IEnumerator WinSpin(Transform spinner, float speed = 75)
        {
            while (true)
            {
                spinner.Rotate(Vector3.forward * speed * Time.deltaTime, Space.Self);
                spinner.Rotate(Vector3.left * speed * Time.deltaTime, Space.Self);

                yield return new WaitForEndOfFrame();
            }
        }
    }
}