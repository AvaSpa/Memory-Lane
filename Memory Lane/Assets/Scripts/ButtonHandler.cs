using Assets.Scripts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;
    public Color Color;

    public void Move()//TODO: remove tag "Finish" from the yellow button in the editor
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
    }

    private DirectionEnum GetDirection()
    {
        var curentPosition = Mover.Position;
        var list = new List<Tile>();
        var nextTileUp = Platform.GetTileScript(new Tuple<int, int>(curentPosition.Item1, curentPosition.Item2 - 1));
        list.Add(nextTileUp);
        var nextTileDown = Platform.GetTileScript(new Tuple<int, int>(curentPosition.Item1, curentPosition.Item2 + 1));
        list.Add(nextTileDown);
        var nextTileLeft = Platform.GetTileScript(new Tuple<int, int>(curentPosition.Item1 - 1, curentPosition.Item2));
        list.Add(nextTileLeft);
        var nextTileRight = Platform.GetTileScript(new Tuple<int, int>(curentPosition.Item1 + 1, curentPosition.Item2));
        list.Add(nextTileRight);

        var correctTile = list.FirstOrDefault(t => t != null && t.Color == Color); //TODO: in case of 2 or more with same color pick the one on the lane

        if (correctTile == null) return DirectionEnum.None;

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
