using UnityEngine;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tiles/New Tile")]
public class TileModel : ScriptableObject
{
    public string tag;
    public new string name;
    public Material material;
    [Range(0, 1f)]
    public float rarity;
}
