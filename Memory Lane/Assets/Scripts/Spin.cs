using UnityEngine;

public class Spin : MonoBehaviour
{
    public float Speed = 500;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Speed * Time.deltaTime, Space.Self);
    }
}
