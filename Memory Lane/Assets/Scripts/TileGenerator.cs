using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private GameObject[,] tiles;
    private const float StartingX = 2;
    private const float StartingZ = -2;
    private List<Color> colors = new List<Color> { Color.red, Color.yellow, Color.green, Color.blue, Color.magenta, new Color(0.5f, 0.25f, 0) };

    [HideInInspector]
    public List<Tuple<int, int>> Lane { get; } = new List<Tuple<int, int>>();

    public GameObject TileTemplate;
    public Mover Mover;
    public int GridWidth = 9;
    public int GridHeight = 9;

    void Start()
    {
        GenerateExampleLane();

        GenerateTiles();
    }

    private void GenerateExampleLane()
    {
        Lane.Add(new Tuple<int, int>(4, 7));
        Lane.Add(new Tuple<int, int>(4, 6));
        Lane.Add(new Tuple<int, int>(4, 5));
        Lane.Add(new Tuple<int, int>(4, 4));
        Lane.Add(new Tuple<int, int>(5, 4));
        Lane.Add(new Tuple<int, int>(6, 4));
    }

    public void HideLane()
    {
        for (var i = 0; i < GridWidth; i++)
        {
            for (var j = 0; j < GridHeight; j++)
            {
                var currentCoordinates = new Tuple<int, int>(i, j);

                var tileScript = GetTileScript(currentCoordinates);
                tileScript.Color = GenerateColor(currentCoordinates);
                tileScript.IsLane = true;
                tileScript.UpdateVisuals();
            }
        }

        InitializeMoverPosition();
    }

    private void InitializeMoverPosition()
    {
        var firstTileCoordinates = Lane.FirstOrDefault();
        var firstTile = tiles[firstTileCoordinates.Item1, firstTileCoordinates.Item2];
        Mover.transform.position = new Vector3(firstTile.transform.position.x, 0, firstTile.transform.position.z);
    }

    private Color GenerateColor(Tuple<int, int> coordinates)
    {
        var generatedColor = GetRandomColor();
        var previousTileColors = GetPreviousTileColors(coordinates);
        while (previousTileColors.Contains(generatedColor))
        {
            generatedColor = GetRandomColor();
        }

        return generatedColor;
    }

    private List<Color> GetPreviousTileColors(Tuple<int, int> coordinates)
    {
        var result = new List<Color>();

        var previous1 = GetTileScript(new Tuple<int, int>(coordinates.Item1, coordinates.Item2 - 1));
        if (previous1 != null) result.Add(previous1.Color);
        var previous2 = GetTileScript(new Tuple<int, int>(coordinates.Item1 - 1, coordinates.Item2 - 1));
        if (previous2 != null) result.Add(previous2.Color);
        var previous3 = GetTileScript(new Tuple<int, int>(coordinates.Item1 - 1, coordinates.Item2));
        if (previous3 != null) result.Add(previous3.Color);
        var previous4 = GetTileScript(new Tuple<int, int>(coordinates.Item1 - 1, coordinates.Item2 + 1));
        if (previous4 != null) result.Add(previous4.Color);

        return result;
    }

    private Color GetRandomColor()
    {
        var index = UnityEngine.Random.Range(0, colors.Count - 1);
        return colors[index];
    }

    private void GenerateTiles()
    {
        tiles = new GameObject[GridWidth, GridHeight];

        for (var i = 0; i < GridWidth; i++)
        {
            for (int j = 0; j < GridHeight; j++)
            {
                var position = new Vector3(StartingX + i * 4, 0, StartingZ - j * 4);
                var tile = Instantiate(TileTemplate, position, Quaternion.identity, transform);
                tiles[i, j] = tile;

                var tileScript = tile.GetComponent<Tile>();
                tileScript.Color = new Color(0.1f, 0.1f, 0.1f);
            }
        }

        foreach (var laneStep in Lane)
        {
            var tileScript = GetTileScript(laneStep);
            tileScript.IsLane = true;
        }

        var firstTileScript = GetTileScript(Lane.First());
        firstTileScript.Color = Color.white;
        var lastTileScript = GetTileScript(Lane.Last());
        lastTileScript.Color = Color.black;
    }

    private Tile GetTileScript(Tuple<int, int> coordinates)
    {
        if (coordinates.Item1 < 0 || coordinates.Item1 > GridWidth - 1 || coordinates.Item2 < 0 || coordinates.Item2 > GridHeight - 1)
            return null;

        var tile = tiles[coordinates.Item1, coordinates.Item2];
        return tile.GetComponent<Tile>();
    }
}
