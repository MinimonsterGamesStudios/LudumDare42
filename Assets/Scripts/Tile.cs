using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    TileModel _tile;
    SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _tile.sprite;
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
