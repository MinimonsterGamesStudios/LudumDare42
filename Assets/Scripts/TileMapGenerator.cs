using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    [Tooltip("Tiles that represent the ground")]
    [SerializeField]
    private List<TileModel> _groundTiles;
    [Tooltip("Tiles that represent the lava")]
    [SerializeField]
    private List<TileModel> _lavaTiles;
    [SerializeField]
    private GameObject _tilePrefab;

    private GameObject[,] _generatedTiles;

    [Header("Map Size")]
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();

        for (int i = 0; i < 5; i++)
        {
            SmoothMap();
        }
        DestroyAllTiles();
        GenerateTiles();
    }


    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
    }

    void SmoothMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);

                if (neighbourWallTiles > 4)
                    map[x, y] = 1;
                else if (neighbourWallTiles < 4)
                    map[x, y] = 0;

            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
        {
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                    {
                        wallCount += map[neighbourX, neighbourY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }

        return wallCount;
    }

    private void GenerateTiles()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f);
                var tile = Instantiate(_tilePrefab, position, Quaternion.identity);
                //tile.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
                if (map[x, y] == 0)
                {
                    int randomIndex = Random.Range(0, _lavaTiles.Count);
                    tile.GetComponent<Tile>().SetTileModel(_lavaTiles[randomIndex]);
                }
                else
                {
                    //int randomIndex = Random.Range(0, _groundTiles.Count);
                    //tile.GetComponent<Tile>().SetTileModel(_groundTiles[randomIndex]);
                }
                _generatedTiles[x, y] = tile;
            }
        }
    }

    private void DestroyAllTiles()
    {
        if(_generatedTiles != null)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (_generatedTiles[x, y] != null)
                        Destroy(_generatedTiles[x, y]);
                }
            }
        }
        _generatedTiles = new GameObject[width, height];
    }
}
