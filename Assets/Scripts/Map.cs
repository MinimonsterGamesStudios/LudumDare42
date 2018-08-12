using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

internal enum TileType
{
    Ground,
    Lava
}

public class Map : MonoBehaviour
{

    [Header("Tiles")]
    [Tooltip("Tiles that represent the ground")]
    [SerializeField]
    private GameObject _groundTilePrefab;

    [SerializeField]
    private List<TileModel> _groundTiles;

    [Space(10)]
    [SerializeField]
    private GameObject _lavaTilePrefab;

    [SerializeField]
    [Range(0, 100)]
    private int randomFillPercent;

    [Header("Map Strip Width")]
    private int _mapStripWidth = 10;

    private Vector3 _mapUpperPosition = Vector3.zero;
    private float _groundRaritiesSum = 0;

    private GameObject _groupObject;
    private bool _shouldGenerate = true;


    void Start()
    {
        _groundRaritiesSum = _groundTiles.Sum(tile => tile.rarity);
        _groupObject = new GameObject("Ethan The King");

        CreateStartingPlatform(5);
        GenerateMap(20);

        StartCoroutine("GenerateTilesAutomatically");
    }

    public GameObject GetTileAt(int x, int y)
    {
        Transform groupTransform = _groupObject.transform;

        for (int i = 0; i < groupTransform.childCount; i++)
        {
            Transform tile = groupTransform.GetChild(i);
            if (tile.position.x == x && tile.position.y == y)
            {
                return tile.gameObject;
            }
        }

        return null;
    }

    private void CreateStartingPlatform(int strips)
    {
        for (int i = 0; i < strips; i++)
        {
            for (int x = 0; x < _mapStripWidth; x++)
            {
                Vector2Int pos = new Vector2Int(x, (int)_mapUpperPosition.y + i);
                GameObject tile = CreateRandomGroundTile(x, _mapUpperPosition);
            }

            _mapUpperPosition += Vector3.up;
        }
    }

    private void GenerateMap(int strips)
    {
        for (int i = 0; i < strips; i++)
        {
            TileType[] stripPlan = GenerateRandomMapStripPlan(_mapStripWidth);

            List<GameObject> tiles = CreateTilesFromStripPlan(stripPlan, _mapUpperPosition);
            _mapUpperPosition += Vector3.up;
        }
    }

    private TileType[] GenerateRandomMapStripPlan(int width)
    {
        TileType[] mapStripPlan = new TileType[width];
        string seed = Time.time.ToString();

        for (int x = 0; x < width; x++)
        {
            if (x == 0 || x == width - 1)
            {
                mapStripPlan[x] = TileType.Ground;
            }
            else
            {
                mapStripPlan[x] = (Random.Range(0, 100) < randomFillPercent) ? TileType.Ground : TileType.Lava;
            }
        }

        return mapStripPlan;
    }

    private List<GameObject> CreateTilesFromStripPlan(TileType[] stripPlan, Vector3 startPosition)
    {
        List<GameObject> createdTiles = new List<GameObject>();

        for (int x = 0; x < stripPlan.Length; x++)
        {
            TileType tileType = stripPlan[x];
            if (tileType == TileType.Ground)
            {
                Vector3 position = new Vector3(x, 0, 0.25f) + startPosition;
                GameObject lava = Instantiate(_lavaTilePrefab, position, Quaternion.identity);
                lava.transform.SetParent(_groupObject.transform);
                createdTiles.Add(lava);
            }
            else
            {
                GameObject ground = CreateRandomGroundTile(x, startPosition);
                createdTiles.Add(ground);
            }
        }

        return createdTiles;
    }

    private GameObject CreateRandomGroundTile(int x, Vector3 startPosition)
    {
        float randomWeight = Random.Range(0, _groundRaritiesSum);
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

        Vector3 position = new Vector3(x, 0) + startPosition;
        var tile = Instantiate(_groundTilePrefab, position, Quaternion.Euler(0, 180f, 0));
        tile.transform.SetParent(_groupObject.transform);

        tile.GetComponent<Tile>().SetTileModel(_groundTiles[randomIndex]);

        return tile;
    }

    private IEnumerator GenerateTilesAutomatically()
    {
        while (_shouldGenerate)
        {
            yield return new WaitForSeconds(1);
            GenerateMap(2);
        }
    }
}
