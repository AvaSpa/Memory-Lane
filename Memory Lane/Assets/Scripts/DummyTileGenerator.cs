﻿using Assets.Scripts.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class DummyTileGenerator : MonoBehaviour
    {
        public GameObject TileTemplate;

        public int GridWidth = 9;
        public int GridHeight = 9;
        public Texture[] Symbols = new Texture[4];

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

                    GameObject tile;
                    if (tiles[i, j] == null)
                    {
                        tile = Instantiate(TileTemplate, transform, false);
                        tile.transform.localPosition = position;
                        tiles[i, j] = tile;
                    }
                    else
                        tile = tiles[i, j];

                    var tileScript = tile.GetComponent<Tile>();
                    tileScript.Identity = GenerateIdentity(i, j);
                    tileScript.UpdateVisuals();
                }
            }
        }

        private TileIdentity GenerateIdentity(int i, int j)
        {
            var generatedIndex = GetRandomColorIndex();

            var generatedColor = colors[generatedIndex];
            var previousTileColors = GetPreviousTileColors(i, j);

            var maxTryCount = 10;

            while (maxTryCount >= 0 && previousTileColors.Contains(generatedColor))
            {
                generatedIndex = GetRandomColorIndex();
                generatedColor = colors[generatedIndex];

                maxTryCount--;
            }

            var result = new TileIdentity(generatedColor, Symbols[generatedIndex]);

            return result;
        }

        private List<Color> GetPreviousTileColors(int i, int j)
        {
            var result = new List<Color>();

            var previous1 = GetTileScript(i - 2, j);
            if (previous1 != null) result.Add(previous1.Identity.Color);
            var previous2 = GetTileScript(i, j - 2);
            if (previous2 != null) result.Add(previous2.Identity.Color);
            var previous3 = GetTileScript(i - 1, j - 1);
            if (previous3 != null) result.Add(previous3.Identity.Color);
            var previous4 = GetTileScript(i - 1, j + 1);
            if (previous4 != null) result.Add(previous4.Identity.Color);

            return result;
        }

        private Tile GetTileScript(int i, int j)
        {
            if (i < 0 || i > GridWidth - 1 || j < 0 || j > GridHeight - 1)
                return null;

            var tile = tiles[i, j];
            return tile.GetComponent<Tile>();
        }

        private int GetRandomColorIndex()
        {
            return Random.Range(0, colors.Count);
        }
    }
}