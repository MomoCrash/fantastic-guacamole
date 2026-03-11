using UnityEngine;

[ExecuteAlways]
public class Auto_Redimention : MonoBehaviour
{
    public Material material;
    public Sprite sprite;
    public float Factor;
    public Vector2 FactorAxis;
    public Vector3 Offset;

    Vector2 TextureSize;

    private void Start()
    {

    }

    void Update()
    {
        material.SetTexture("_BaseMap", sprite.texture);
        TextureSize = new Vector2(sprite.texture.width, sprite.texture.height);

        Vector3 size = new Vector3(.001f * TextureSize.x * FactorAxis.x, .001f * TextureSize.y * 1.4f * FactorAxis.y, 1) * Factor;
        transform.localScale = size;
        transform.position = new Vector3(transform.position.x, size.y * 0.5f, transform.position.z) + Offset;
    }
}
