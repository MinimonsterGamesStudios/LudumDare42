using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [Header("Tiles")]
    [Tooltip("Tiles that represent the ground")]
    [SerializeField]
    private GameObject _groundTilePrefab;
    [SerializeField]
    private List<TileModel> _groundTiles;
    [Space(10)]
    [Tooltip("Tiles that represent the lava")]
    [SerializeField]
    private GameObject _lavaTilePrefab;

    private List<GameObject> _generatedTiles;
    bool _shouldGenerate = true;

    [Header("Map Size")]
    public int width;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    int[,] map;
    Vector3 refPosition = Vector3.zero;

    void Start()
    {
        _shouldGenerate = true;
        _generatedTiles = new List<GameObject>();
        StartCoroutine("GenerateTileRows");
    }

    void GenerateMap()
    {
        var map = new int[width];
        RandomFillMap(map);
        GenerateTiles(map);
    }

    void RandomFillMap(int[] map)
    {
        seed = Time.time.ToString();

        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            if (x == 0 || x == width - 1)
            {
                map[x] = 1;
            }
            else
            {
                map[x] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
            }
        }
    }

    private void GenerateTiles(int[] map)
    {
        for (int x = 0; x < width; x++)
        {
            //Vector3 position = new Vector3(-width / 2 + x + .5f, -height / 2 + y + .5f) + refPosition;
            Vector3 position = new Vector3(x, 0) + refPosition;
            if (map[x] == 1)
            {
                var tile = Instantiate(_lavaTilePrefab, position, Quaternion.identity);
                _generatedTiles.Add(tile);
            }
            else
            {
                Debug.Log("Ground!");
                var tile = Instantiate(_groundTilePrefab, position, Quaternion.identity);
                int randomIndex = Random.Range(0, _groundTiles.Count);
                tile.GetComponent<Tile>().SetTileModel(_groundTiles[randomIndex]);
                _generatedTiles.Add(tile);
            }
        }
    }

    private IEnumerator GenerateTileRows()
    {
        for (int i = 0; i < 5; i++)
        {
            refPosition += Vector3.up;
            for (int x = 0; x < width; x++)
            {
                Vector3 position = new Vector3(x, 0) + refPosition;
                var tile = Instantiate(_groundTilePrefab, position, Quaternion.identity);
                int randomIndex = Random.Range(0, _groundTiles.Count);
                tile.GetComponent<Tile>().SetTileModel(_groundTiles[randomIndex]);
                _generatedTiles.Add(tile);
            }
        }
        while (_shouldGenerate)
        {
            yield return new WaitForSeconds(.5f);
            refPosition += Vector3.up;
            GenerateMap();
        }

    }

    void GenerateInitialMap()
    {
        
    }
}
