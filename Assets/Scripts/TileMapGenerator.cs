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
    float _sumOfGroundRarities = 0;
    void Start()
    {
        _shouldGenerate = true;
        for (int i = 0; i < _groundTiles.Count; i++)
        {
            _sumOfGroundRarities += _groundTiles[i].rarity;
        }
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
            if (map[x] == 1)
            {
                Vector3 position = new Vector3(x, 0, 0.25f) + refPosition;
                var tile = Instantiate(_lavaTilePrefab, position, Quaternion.identity);
                _generatedTiles.Add(tile);
            }
            else
            {
                GenerateRandomGroundTile(x);
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
                GenerateRandomGroundTile(x);
            }
        }
        while (_shouldGenerate)
        {
            yield return new WaitForSeconds(.1f);
            refPosition += Vector3.up;
            GenerateMap();
        }

    }

    void GenerateRandomGroundTile(int x)
    {
        float randomWeight = Random.Range(0, _sumOfGroundRarities);
        int randomIndex = 0;
        for (int i = 0; i < _groundTiles.Count; i++)
        {
            randomWeight -= _groundTiles[i].rarity;
            if (randomWeight <= 0)
            {
                randomIndex = i;
                break;
            }
        }
        Vector3 position = new Vector3(x, 0) + refPosition;
        var tile = Instantiate(_groundTilePrefab, position, Quaternion.Euler(0, 180f, 0));
        tile.GetComponent<Tile>().SetTileModel(_groundTiles[randomIndex]);
        _generatedTiles.Add(tile);
    }
}
