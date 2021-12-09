using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TileGenerator : MonoBehaviour
{
    private GameObject[,] tiles;
    private const float StartingX = 2;
    private const float StartingZ = -2;
    private List<Color> colors = new List<Color> { Color.red, new Color(1, 1, 0, 1), Color.green, Color.blue };

    private List<Vector2> _lane = new List<Vector2>();

    public GameObject TileTemplate;
    public GameObject LaneTileBaseTemplate;
    public Mover Mover;
    public GameController GameController;
    public int GridWidth = 9;
    public int GridHeight = 9;
    public LevelList Levels;
    public SceneChanger SceneChanger;

    [HideInInspector]
    public UnityEvent DoneGenerating;

    void Start()
    {
        tiles = new GameObject[GridWidth, GridHeight];

        if (GameController != null)
            LoadLevel(GameController.CurrentLevel);

        GenerateTiles();
        DoneGenerating?.Invoke();
    }

    private void LoadLevel(int currentLevel)
    {
        if (Levels.Levels.Count >= currentLevel)
            _lane = Levels.Levels[currentLevel - 1].Tiles;
        else
            SceneChanger.FadeToScene(Assets.Scripts.Enums.SceneIdentity.Menu);
    }

    public void HideLane()
    {
        for (var i = 0; i < GridWidth; i++)
        {
            for (var j = 0; j < GridHeight; j++)
            {
                var currentCoordinates = new Vector2(i, j);

                var tileScript = GetTileScript(currentCoordinates);
                tileScript.Color = GenerateColor(currentCoordinates);
                tileScript.UpdateVisuals();
            }
        }

        InitializeMoverPosition();
    }

    public void Collapse()
    {
        SetNonLaneTilesActive(false);
    }

    private void RestoreTiles()
    {
        SetNonLaneTilesActive(true);
        SetLaneTilesBlocked(false);
    }

    private void SetLaneTilesBlocked(bool v)
    {
        foreach (var tile in _lane)
        {
            var script = GetTileScript(tile);
            script.IsLocked = false;
            script.UpdateVisuals();
        }
    }

    private void SetNonLaneTilesActive(bool active)
    {
        for (var i = 0; i < GridWidth; i++)
        {
            for (var j = 0; j < GridHeight; j++)
            {
                var currentCoordinates = new Vector2(i, j);

                var tileScript = GetTileScript(currentCoordinates);
                if (!tileScript.IsLane)
                    tileScript.gameObject.SetActive(active);
            }
        }
    }

    private void InitializeMoverPosition()
    {
        if (Mover == null) return;

        var firstTileCoordinates = _lane.FirstOrDefault();
        var firstTile = tiles[(int)firstTileCoordinates.x, (int)firstTileCoordinates.y];

        var tileScript = GetTileScript(firstTileCoordinates);
        tileScript.IsLocked = true;
        tileScript.UpdateVisuals();

        Mover.transform.position = new Vector3(firstTile.transform.position.x, 0, firstTile.transform.position.z);
        Mover.Position = firstTileCoordinates;
    }

    private Color GenerateColor(Vector2 coordinates)
    {
        var generatedColor = GetRandomColor();
        var previousTileColors = GetPreviousTileColors(coordinates);

        var maxTryCount = 10;

        while (maxTryCount >= 0 && previousTileColors.Contains(generatedColor))
        {
            generatedColor = GetRandomColor();

            maxTryCount--;
        }

        return generatedColor;
    }

    private List<Color> GetPreviousTileColors(Vector2 coordinates)
    {
        var result = new List<Color>();

        var previous1 = GetTileScript(new Vector2(coordinates.x - 2, coordinates.y));
        if (previous1 != null) result.Add(previous1.Color);
        var previous2 = GetTileScript(new Vector2(coordinates.x, coordinates.y - 2));
        if (previous2 != null) result.Add(previous2.Color);
        var previous3 = GetTileScript(new Vector2(coordinates.x - 1, coordinates.y - 1));
        if (previous3 != null) result.Add(previous3.Color);
        var previous4 = GetTileScript(new Vector2(coordinates.x - 1, coordinates.y + 1));
        if (previous4 != null) result.Add(previous4.Color);

        return result;
    }

    private Color GetRandomColor()
    {
        var index = Random.Range(0, colors.Count);
        return colors[index];
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
                tileScript.Color = new Color(0.5f, 0.5f, 0.5f);
                tileScript.UpdateVisuals();
            }
        }

        foreach (var laneStep in _lane)
        {
            var position = new Vector3(StartingX + laneStep.x * 4, -10.005f, StartingZ - laneStep.y * 4);
            Instantiate(LaneTileBaseTemplate, position, Quaternion.identity, transform);

            var tileScript = GetTileScript(laneStep);
            tileScript.IsLane = true;
            tileScript.Color = new Color(0.2f, 0.2f, 0.2f);
            tileScript.UpdateVisuals();
        }

        if (_lane.Count == 0) return;
        var firstTileScript = GetTileScript(_lane.First());
        firstTileScript.Color = Color.white;
        firstTileScript.UpdateVisuals();
        var lastTileScript = GetTileScript(_lane.Last());
        lastTileScript.IsLast = true;
        lastTileScript.Color = Color.black;
        lastTileScript.UpdateVisuals();

        RestoreTiles(); //TODO: needed?
    }

    public Tile GetTileScript(float i, float j)
    {
        return GetTileScript(new Vector2(i, j));
    }

    public Tile GetTileScript(Vector2 coordinates)
    {
        if (coordinates.x < 0 || coordinates.x > GridWidth - 1 || coordinates.y < 0 || coordinates.y > GridHeight - 1)
            return null;

        var tile = tiles[(int)coordinates.x, (int)coordinates.y];
        return tile?.GetComponent<Tile>();
    }
}
