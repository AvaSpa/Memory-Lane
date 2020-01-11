using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public Mover Mover;
    public TileGenerator Platform;

    private int currentPositionIndex = 0;

    public void AdvanceOnLane()
    {
        if (currentPositionIndex >= Platform.Lane.Count - 2)
            gameObject.SetActive(false);

        if (currentPositionIndex < Platform.Lane.Count - 1)
        {
            var coordinates = Platform.Lane[currentPositionIndex];
            currentPositionIndex++;
            var nextCoordinates = Platform.Lane[currentPositionIndex];

            if (coordinates.Item1 == nextCoordinates.Item1 && coordinates.Item2 > nextCoordinates.Item2)
                Mover.MoveForward();

            if (coordinates.Item1 == nextCoordinates.Item1 && coordinates.Item2 < nextCoordinates.Item2)
                Mover.MoveBack();

            if (coordinates.Item1 > nextCoordinates.Item1 && coordinates.Item2 == nextCoordinates.Item2)
                Mover.MoveLeft();

            if (coordinates.Item1 < nextCoordinates.Item1 && coordinates.Item2 == nextCoordinates.Item2)
                Mover.MoveRight();
        }
    }
}
