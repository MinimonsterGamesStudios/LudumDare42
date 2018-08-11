using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    TileModel _tile;
    Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.material = _tile.material;
        gameObject.tag = _tile.tag;

    }

    public TileModel GetTileModel()
    {
        return _tile;
    }

    public void SetTileModel(TileModel tileModel)
    {
        _tile = tileModel;
    }

}
