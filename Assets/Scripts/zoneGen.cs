using TMPro;
using UnityEngine;

[ExecuteAlways]
public class zoneGen : MonoBehaviour
{
    public int WhitePoint;
    public int BlackPoint;
    public int ResolutionScale;
    public bool RECALC;

    Material _Material;
    Vector4[] _Points;

    void Recalc()
    {
        _Points = new Vector4[WhitePoint + BlackPoint];
        for (int i = 0; i < WhitePoint; i++)
        {
            float x = Random.value;
            float y = Random.value;
            Vector2 final = new Vector2(x, y);
            if (
                x > 0.25f && x < 0.75f && 
                y > 0.25f && y < 0.75f
                )
            {
                float factor = Random.value >= 0.5f ? 1.0f : 0.0f;
                Vector2 offset = 
                    new Vector2((Random.value >= 0.5f ? 1.0f : -1.0f), 0) * factor + 
                    new Vector2(0, (Random.value >= 0.5f ? 1.0f : -1.0f)) * (1 - factor);
                final += 0.5f * offset;
            }

            if(final.x < 0.0f) final.x = 0.0f;
            if(final.y < 0.0f) final.y = 0.0f;
            if(final.x > 1.0f) final.x = 1.0f;
            if(final.y > 1.0f) final.y = 1.0f;
            

            _Points[i] = new Vector4(
                final.x,
                final.y,
                0,
                0
            );
        }
        for (int i = 0; i < BlackPoint; i++)
        {
            _Points[WhitePoint + i] = new Vector4(
                0.3f + Random.value * 0.4f,
                0.3f + Random.value * 0.4f,
                1,
                0
            );
        }
    }

    private void Start()
    {
        _Material = GetComponent<MeshRenderer>().sharedMaterial;
        Recalc();   
    }

    private void Update()
    {
        if(RECALC)
        {
            Recalc();
            RECALC = false;
        }

        _Material.SetVectorArray("_Points", _Points);
        _Material.SetInteger("_PointCount", _Points.Length);
        _Material.SetInteger("_ResolutionScale", ResolutionScale);

    }

    private void OnDrawGizmos()
    {
        foreach (var point in _Points) {
            Gizmos.color = point.z == 0 ? Color.white : Color.black;
            Gizmos.DrawSphere(
                transform.position + new Vector3(point.x * transform.localScale.x, point.y * transform.localScale.y, 0) - new Vector3(0.5f, 0.5f, 0),
                0.01f * transform.localScale.z
            );
        }
    }
}
