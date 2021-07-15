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
    public GameController GameController;
    public TileGenerator Platform;
    public AudioSource StepAudio;
    public AudioSource FallAudio;

    public int Step = 5;
    public float Speed = 0.01f;

    [HideInInspector]
    public Vector2 Position;

    public void MoveForward()
    {
        if (shouldProcessInput)
        {
            Position = new Vector2(Position.x, Position.y - 1);
            shouldProcessInput = false;
            StartCoroutine(InternalMoveForward());
        }
    }

    public void MoveBack()
    {
        if (shouldProcessInput)
        {
            Position = new Vector2(Position.x, Position.y + 1);
            shouldProcessInput = false;
            StartCoroutine(InternalMoveBack());
        }
    }

    internal void StartWinAnimation()
    {
        //throw new System.NotImplementedException();
    }

    public void MoveLeft()
    {
        if (shouldProcessInput)
        {
            Position = new Vector2(Position.x - 1, Position.y);
            shouldProcessInput = false;
            StartCoroutine(InternalMoveLeft());
        }
    }

    public void MoveRight()
    {
        if (shouldProcessInput)
        {
            Position = new Vector2(Position.x + 1, Position.y);
            shouldProcessInput = false;
            StartCoroutine(InternalMoveRight());
        }
    }

    public void Kill()
    {
        shouldProcessInput = false;
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        FallAudio.Play();
    }

    public void Reset()
    {
        shouldProcessInput = true;
        var rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
    }

    private void UpdateWalkedTile()
    {
        var currentTile = Platform.GetTileScript(Position);
        if (currentTile.IsLane)
        {
            currentTile.IsLocked = true;
            currentTile.UpdateVisuals();

            if (currentTile.IsLast)
                GameController.EndGame(false);
        }
        else
        {
            GameController.EndGame(true);
        }
    }

    private IEnumerator InternalMoveForward()
    {
        StepAudio.Play();
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Forward.transform.position, Vector3.right, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        UpdateWalkedTile();
        shouldProcessInput = true;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator InternalMoveBack()
    {
        StepAudio.Play();
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Back.transform.position, Vector3.left, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        UpdateWalkedTile();
        shouldProcessInput = true;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator InternalMoveLeft()
    {
        StepAudio.Play();
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Left.transform.position, Vector3.forward, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        UpdateWalkedTile();
        shouldProcessInput = true;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator InternalMoveRight()
    {
        StepAudio.Play();
        for (var i = 0; i < 90 / Step; i++)
        {
            Body.transform.RotateAround(Right.transform.position, Vector3.back, Step);
            yield return new WaitForSeconds(Speed);
        }
        Center.transform.position = Body.transform.position;
        UpdateWalkedTile();
        shouldProcessInput = true;
        yield return new WaitForEndOfFrame();
    }
}
