using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.PlayerSettings;

public class MapGeneration : MonoBehaviour
{
    public Vector3 Position;
    public GameObject[] Tiles;
    public Vector2Int GridSize;
    public Transform Template;

    Dictionary<Vector4Int, List<GameObject>> Mapping;

    Vector4Int[,] GridRect;
    GameObject[,] GridObject;

    void InitMapping()
    {
        Mapping = new Dictionary<Vector4Int, List<GameObject>>();

        foreach (var tile in Tiles)
        {
            for (int i = 0; i < 4; i++)
            {
                Rotation rot = (Rotation)i;
                Vector4Int v = tile.GetComponent<Tile>().GetRect(rot);
                // if (Mapping.ContainsKey(v)) continue;

                GameObject obj = Instantiate(tile, Template);
                obj.GetComponent<Tile>().tile_rotation = rot;
                obj.transform.position = new Vector2(1000, 1000); // off screen
                if (!Mapping.ContainsKey(v)) Mapping.Add(v, new List<GameObject>());

                Mapping[v].Add(obj);
            }
        }
    }

    void CreateGridEmpty()
    {
        GridRect = new Vector4Int[GridSize.x, GridSize.y];
        GridObject = new GameObject[GridSize.x, GridSize.y];

        for(var x = 0; x < GridSize.x; ++x)
        {
            for (var y = 0; y < GridSize.y; ++y)
            {
                GridRect[x, y] = new Vector4Int(false, false, false, false);
                GridObject[x, y] = CreateObject(GridRect[x, y], new Vector2Int(x, y));
            }
        }
    }

    void CreateGridRandom()
    {
        GridRect = new Vector4Int[GridSize.x, GridSize.y];
        GridObject = new GameObject[GridSize.x, GridSize.y];

        for (var x = 0; x < GridSize.x; ++x)
        {
            for (var y = 0; y < GridSize.y; ++y)
            {
                GridRect[x, y] = new Vector4Int(Random.value > 0.5f, Random.value > 0.5f, Random.value > 0.5f, Random.value > 0.5f);
                GridObject[x, y] = CreateObject(GridRect[x, y], new Vector2Int(x, y));
            }
        }
    }

    GameObject RandomTile(Vector4Int r)
    {
        float rand = Random.value;
        int index = (int)(rand * Mapping[r].Count);
        return Mapping[r][index];
    }

    GameObject CreateObject(Vector4Int r, Vector2Int pos)
    {
        GameObject obj = Instantiate(RandomTile(r), transform);
        obj.transform.position = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 + 1);
        obj.GetComponent<Tile>().tile_pos = pos;
        return obj;
    }

    void SwapSubTile(Vector2Int tile, Vector2Int sub_tile)
    {
        Vector4Int rect = GridRect[tile.x, tile.y];
        GameObject obj = GridObject[tile.x, tile.y];
        Vector4Int new_rect = rect;

        if (sub_tile.x == 0)
        {
            if (sub_tile.y == 0)
            {
                // (0, 0)
                new_rect.BL = -new_rect.BL + 1;
            }
            else
            {
                // (0, 1)
                new_rect.TL = -new_rect.TL + 1;
            }
        }
        else
        {
            if (sub_tile.y == 0)
            {
                // (1, 0)
                new_rect.BR = -new_rect.BR + 1;
            }
            else
            {
                // (1, 1)
                new_rect.TR = -new_rect.TR + 1;
            }
        }

        Destroy(obj);
        GridRect[tile.x, tile.y] = new_rect;
        Debug.Log("Generating Tile : " + tile);
        GridObject[tile.x, tile.y] = CreateObject(new_rect, tile);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitMapping();
        // CreateGridEmpty();
        CreateGridRandom();
    }

    // Update is called once per frame
    void Update()
    {
        var MousePosition = Mouse.current.position.value;
        Ray ray  = Camera.main.ScreenPointToRay(MousePosition);
        var hits = Physics.RaycastAll(ray);
        if (hits.Length != 0)
        {
            Position = hits[0].point;
        }

        Vector2Int sub_tile1 = new Vector2Int((int)Position.x, (int)Position.z);
        Vector2Int tile = sub_tile1 / 2;
        Vector2Int sub_tile2 = sub_tile1 - 2 * tile;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("| Tile : " + tile + " | Sous Tile : " + sub_tile2 + " | Click : " + sub_tile1 + " |");
            SwapSubTile(tile, sub_tile2);
        }

        // foreach(var obj in GridObject)
        // {
        //     if(obj.GetComponent<Tile>().tile_pos == tile)
        //         obj.transform.localScale = Vector3.one * 0.95f;
        //     else
        //         obj.transform.localScale = Vector3.one;
        // }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(Position, 0.1f);
    }
}
