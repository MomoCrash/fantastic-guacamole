using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
public class TileData : ScriptableObject
{
    public bool TopLeft;
    public bool TopRight;
    public bool BottomLeft;
    public bool BottomRight;

}
