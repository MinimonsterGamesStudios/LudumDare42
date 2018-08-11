using UnityEngine;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tiles/New Tile")]
public class TileModel : ScriptableObject
{
    public string tag;
    public Sprite sprite;
}
