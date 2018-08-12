using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private TileModel _tile;
    private Renderer _renderer;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material = _tile.material;
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
