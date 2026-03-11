using UnityEngine;

[CreateAssetMenu(fileName = "TileObject", menuName = "Scriptable Objects/TileObject")]
public class TileObject : ScriptableObject
{
    public GameObject Object;
    public float Weight;
}
