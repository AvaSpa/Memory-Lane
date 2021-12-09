using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DummyTileGenerator : MonoBehaviour
    {
        public int GridWidth = 9;
        public int GridHeight = 9;
        public GameObject TileTemplate;

        private GameObject[,] tiles;

        private List<Color> colors = new List<Color> { Color.red, new Color(1, 1, 0, 1), Color.green, Color.blue };

        private const float StartingX = 2;
        private const float StartingZ = -2;

        private void Start()
        {
            tiles = new GameObject[GridWidth, GridHeight];

            GenerateTiles();
        }

        private void GenerateTiles()
        {
            for (var i = 0; i < GridWidth; i++)
            {
                for (var j = 0; j < GridHeight; j++)
                {
                    var position = new Vector3(StartingX + i * 4, 0, StartingZ - j * 4);

                    GameObject tile = null;
                    if (tiles[i, j] == null)
                    {
                        tile = Instantiate(TileTemplate, position, Quaternion.identity, transform);
                        tiles[i, j] = tile;
                    }
                    else
                        tile = tiles[i, j];

                    var tileScript = tile.GetComponent<Tile>();
                    tileScript.Color = GenerateColor(i, j);
                    tileScript.UpdateVisuals();
                }
            }
        }

        private Color GenerateColor(int i, int j)
        {
            var generatedColor = GetRandomColor();
            var previousTileColors = GetPreviousTileColors(i, j);

            var maxTryCount = 10;

            while (maxTryCount >= 0 && previousTileColors.Contains(generatedColor))
            {
                generatedColor = GetRandomColor();

                maxTryCount--;
            }

            return generatedColor;
        }

        private List<Color> GetPreviousTileColors(int i, int j)
        {
            var result = new List<Color>();

            var previous1 = GetTileScript(i - 2, j);
            if (previous1 != null) result.Add(previous1.Color);
            var previous2 = GetTileScript(i, j - 2);
            if (previous2 != null) result.Add(previous2.Color);
            var previous3 = GetTileScript(i - 1, j - 1);
            if (previous3 != null) result.Add(previous3.Color);
            var previous4 = GetTileScript(i - 1, j + 1);
            if (previous4 != null) result.Add(previous4.Color);

            return result;
        }

        private Tile GetTileScript(int i, int j)
        {
            if (i < 0 || i > GridWidth - 1 || j < 0 || j > GridHeight - 1)
                return null;

            var tile = tiles[i, j];
            return tile.GetComponent<Tile>();
        }

        private Color GetRandomColor()
        {
            var index = Random.Range(0, colors.Count);
            return colors[index];
        }
    }
}