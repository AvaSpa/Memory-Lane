using Assets.Scripts.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionButtonHandler : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public Color Color;

    public void Move()
    {
        var direction = GetDirection();
        switch (direction)
        {
            case Direction.Forward:
                Mover.MoveForward();
                break;
            case Direction.Back:
                Mover.MoveBack();
                break;
            case Direction.Left:
                Mover.MoveLeft();
                break;
            case Direction.Right:
                Mover.MoveRight();
                break;
            case Direction.None:
                break;
        }
    }

    private Direction GetDirection()
    {
        var curentPosition = Mover.Position;
        var list = new List<Tile>();
        var nextTileUp = Platform.GetTileScript(curentPosition.x, curentPosition.y - 1);
        list.Add(nextTileUp);
        var nextTileDown = Platform.GetTileScript(curentPosition.x, curentPosition.y + 1);
        list.Add(nextTileDown);
        var nextTileLeft = Platform.GetTileScript(curentPosition.x - 1, curentPosition.y);
        list.Add(nextTileLeft);
        var nextTileRight = Platform.GetTileScript(curentPosition.x + 1, curentPosition.y);
        list.Add(nextTileRight);

        var candidateTiles = list.Where(t => t != null && t.Color == Color);
        var correctTile = candidateTiles.FirstOrDefault(t => t.IsLane && !t.IsLocked);

        if (correctTile == null) correctTile = candidateTiles.FirstOrDefault(t => !t.IsLocked);

        if (correctTile == null) return Direction.None;

        switch (list.IndexOf(correctTile))
        {
            case 0:
                return Direction.Forward;
            case 1:
                return Direction.Back;
            case 2:
                return Direction.Left;
            case 3:
                return Direction.Right;
            default:
                return Direction.None;
        }
    }
}
