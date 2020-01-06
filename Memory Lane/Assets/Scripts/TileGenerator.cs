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
    private List<Tuple<int, int>> lane = new List<Tuple<int, int>>();

    public GameObject TileTemplate;
    public int GridWidth = 9;
    public int GridHeight = 9;

    // Start is called before the first frame update
    void Start()
    {
        GenerateExampleLane();

        GenerateTiles();
    }

    private void GenerateExampleLane()
    {
        lane.Add(new Tuple<int, int>(4, 7));
        lane.Add(new Tuple<int, int>(4, 6));
        lane.Add(new Tuple<int, int>(4, 5));
        lane.Add(new Tuple<int, int>(4, 4));
        lane.Add(new Tuple<int, int>(5, 4));
        lane.Add(new Tuple<int, int>(6, 4));
    }

    public void HideLane()
    {
        var color = Color.white;
        for (var i = 0; i < GridWidth; i++)
        {
            for (var j = 0; j < GridHeight; j++)
            {
                var generatedColor = GetRandomColor();

                while (generatedColor == color)
                {
                    generatedColor = GetRandomColor();
                }

                color = generatedColor;
                var tileScript = GetTileScript(new Tuple<int, int>(i, j));
                tileScript.Color = color;
                tileScript.IsLane = false;
                tileScript.UpdateVisuals();
            }
        }
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

        foreach (var laneStep in lane)
        {
            var tileScript = GetTileScript(laneStep);
            tileScript.IsLane = true;
        }

        var firstTileScript = GetTileScript(lane.First());
        firstTileScript.Color = Color.white;
        var lastTileScript = GetTileScript(lane.Last());
        lastTileScript.Color = Color.black;
    }

    private Tile GetTileScript(Tuple<int, int> coordinates)
    {
        var tile = tiles[coordinates.Item1, coordinates.Item2];
        return tile.GetComponent<Tile>();
    }
}
