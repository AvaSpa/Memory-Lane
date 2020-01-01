using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    private const float StartingX = 2;
    private const float StartingZ = -2;
    private List<GameObject> tiles = new List<GameObject>();

    public GameObject TileTemplate;
    public int GridWidth = 10;
    public int GridHeight = 10;

    // Start is called before the first frame update
    void Start()
    {
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        //foreach (var tile in tiles)
        //    Destroy(tile);
        //tiles.Clear();

        for (var i = 0; i < GridWidth - 1; i++)
        {
            for (int j = 0; j < GridHeight - 1; j++)
            {
                var position = new Vector3(StartingX + i * 4, 0, StartingZ - j * 4);
                var tile = Instantiate(TileTemplate, position, Quaternion.identity, transform);
                tiles.Add(tile);

                if (i == j)
                {
                    var material = tile.GetComponent<Renderer>().material;
                    material.color = Color.red;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
