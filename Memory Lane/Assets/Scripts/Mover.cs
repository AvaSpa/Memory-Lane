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
    public Vector2 Position;

    public void MoveForward()
    {
        Position = new Vector2(Position.x, Position.y - 1);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveForward");
        }
    }

    public void MoveBack()
    {
        Position = new Vector2(Position.x, Position.y + 1);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveBack");
        }
    }

    public void MoveLeft()
    {
        Position = new Vector2(Position.x - 1, Position.y);
        if (shouldProcessInput)
        {
            shouldProcessInput = false;
            StartCoroutine("InternalMoveLeft");
        }
    }

    public void MoveRight()
    {
        Position = new Vector2(Position.x + 1, Position.y);
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

    public void Reset()
    {
        shouldProcessInput = true;
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
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
