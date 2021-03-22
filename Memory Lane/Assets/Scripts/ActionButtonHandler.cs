using Assets.Scripts.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionButtonHandler : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public GameObject Panel;
    public GameController GameController;
    public Color Color;

    public void Move()
    {
        var direction = GetDirection();
        switch (direction)
        {
            case DirectionEnum.Forward:
                Mover.MoveForward();
                break;
            case DirectionEnum.Back:
                Mover.MoveBack();
                break;
            case DirectionEnum.Left:
                Mover.MoveLeft();
                break;
            case DirectionEnum.Right:
                Mover.MoveRight();
                break;
            case DirectionEnum.None:
                break;
        }

        var currentTile = Platform.GetTileScript(Mover.Position);
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

    private DirectionEnum GetDirection()
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

        if (correctTile == null) correctTile = candidateTiles.FirstOrDefault();

        if (correctTile == null) return DirectionEnum.None;

        if (correctTile.IsLocked) return DirectionEnum.None;

        correctTile.IsLocked = true;

        switch (list.IndexOf(correctTile))
        {
            case 0:
                return DirectionEnum.Forward;
            case 1:
                return DirectionEnum.Back;
            case 2:
                return DirectionEnum.Left;
            case 3:
                return DirectionEnum.Right;
            default:
                return DirectionEnum.None;
        }
    }
}
