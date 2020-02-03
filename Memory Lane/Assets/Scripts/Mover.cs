using System;
using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private bool shouldProcessInput = true;

    public GameObject Body;
    public GameObject Center;
    public GameObject Forward;
    public GameObject Back;
    public GameObject Left;
    public GameObject Right;

    public int Step = 5;
    public float Speed = 0.01f;

    [HideInInspector]
    public Tuple<int, int> Position;

    public void MoveForward()
    {
        Position = new Tuple<int, int>(Position.Item1, Position.Item2 - 1);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveForward");
        }
    }

    public void MoveBack()
    {
        Position = new Tuple<int, int>(Position.Item1, Position.Item2 + 1);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveBack");
        }
    }

    public void MoveLeft()
    {
        Position = new Tuple<int, int>(Position.Item1 - 1, Position.Item2);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveLeft");
        }
    }

    public void MoveRight()
    {
        Position = new Tuple<int, int>(Position.Item1 + 1, Position.Item2);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveRight");
        }
    }

    public void Kill()
    {
        shouldProcessInput = false;
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
    }

    private IEnumerator InternalMoveForward()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Forward.transform.position, Vector3.right, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }

    private IEnumerator InternalMoveBack()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Back.transform.position, Vector3.left, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }

    private IEnumerator InternalMoveLeft()
    {
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Left.transform.position, Vector3.forward, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        shouldProcessInput = true;
    }

    private IEnumerator InternalMoveRight()
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
