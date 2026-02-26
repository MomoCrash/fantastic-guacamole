using UnityEngine;

public struct Vector4Int { 
    public int TL, TR, BL, BR; 

    public Vector4Int(bool tl, bool tr, bool bl, bool br)
    {
        TL = tl ? 1 : 0;
        TR = tr ? 1 : 0;
        BL = bl ? 1 : 0;
        BR = br ? 1 : 0;
    }
};
public enum Rotation
{
    ROTATE0 = 0,
    ROTATE90 = 1,
    ROTATE180 = 2,
    ROTATE270 = 3
}

[ExecuteAlways]
public class Tile : MonoBehaviour
{

    public TileData tile_data;
    public Rotation tile_rotation;
    public Vector2Int tile_pos;

    public Vector4Int GetRect(Rotation rotation)
    {
        Vector4Int rect;
        rect.TL = tile_data.TopLeft ? 1 : 0;
        rect.TR = tile_data.TopRight ? 1 : 0;
        rect.BL = tile_data.BottomLeft ? 1 : 0;
        rect.BR = tile_data.BottomRight ? 1 : 0;

        for(int i = 0; i < (int)rotation; i++)
        {
            Vector4Int r;
            r.TL = rect.BL;
            r.TR = rect.TL;
            r.BL = rect.BR;
            r.BR = rect.TR;
            rect = r;
        }

        return rect;
    }

    public Vector4Int GetRect()
    {
        return GetRect(tile_rotation);
    }


    public
        // Update is called once per frame
        void Update()
    {
        transform.rotation = Quaternion.Euler(0, 90 * (int)tile_rotation, 0);
    }
}
