using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    [Tooltip("Tiles that the generator can use.")]
    [SerializeField]
    private List<TileModel> tileTypes;
    [SerializeField]
    private GameObject _tilePrefab;
    [Header("Map Properties")]
    [SerializeField]
    private Vector2 _mapScale;

    // Use this for initialization
    void Start()
    {
        GenerateTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateTiles()
    {
        for (int x = 0; x < _mapScale.x; x++)
        {
            for (int y = 0; y < _mapScale.y; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                var tile = Instantiate(_tilePrefab, position, Quaternion.identity);
                tile.transform.rotation = Quaternion.AngleAxis(90f, Vector3.right);
                int randomIndex = Random.Range(0, tileTypes.Count);
                tile.GetComponent<Tile>().SetTileModel(tileTypes[randomIndex]);
            }
        }
    }
}
