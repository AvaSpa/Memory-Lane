using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Vector3 offset;

    private bool shouldProcessInput = true;

    public GameObject Body;
    public GameObject Center;
    public GameObject Forward;
    public GameObject Back;
    public GameObject Left;
    public GameObject Right;

    public int Step = 9;
    public float Speed = 0.01f;

    private void Update()
    {
        if (shouldProcessInput)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                shouldProcessInput = false;
                StartCoroutine("MoveForward");
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                shouldProcessInput = false;
                StartCoroutine("MoveBack");
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                shouldProcessInput = false;
                StartCoroutine("MoveLeft");
            }
            else if (Input.GetAxis("Horizontal") > 0)
            {
                shouldProcessInput = false;
                StartCoroutine("MoveRight");
            }
        }
    }

    private IEnumerator MoveForward()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Forward.transform.position, Vector3.right, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }

    private IEnumerator MoveBack()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Back.transform.position, Vector3.left, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }

    private IEnumerator MoveLeft()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Left.transform.position, Vector3.forward, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }

    private IEnumerator MoveRight()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Right.transform.position, Vector3.back, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }
}
