

using System.Collections;
using UnityEngine;

public class GridVillage : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject quad;
    public GameObject container;

    public int width;
    public int height;
    
    public float spacingX;
    public float spacingZ;
    private void OnValidate()
    {
        StartCoroutine(WaitForSecond());
    }

    IEnumerator WaitForSecond()
    {
        yield return new WaitForSeconds(0.01f);
        
        DestroyImmediate(container);

        container = GameObject.CreatePrimitive(PrimitiveType.Quad);
        container.transform.SetParent(transform);
        
        Vector3 basePosition = transform.position;
        
        float baseX = width * spacingX /2;
        float baseZ = width * spacingZ /2;

        for (int j = 0; j < width; j++)
        {
            GameObject obj1 = Instantiate(quad, basePosition 
                                                + new Vector3(0, -0.45f, spacingZ * j),
                Quaternion.identity, container.transform);
            obj1.transform.localScale = new Vector3(100, 1, 1);
        }
        
        for (int i = 0; i < height; i++)
        {
            GameObject obj = Instantiate(quad, basePosition +
                                               new Vector3(spacingX * i - baseX, -0.45f, baseZ),
                Quaternion.Euler(0, 90, 0), container.transform);
            obj.transform.localScale = new Vector3(100, 1, 100);
        }
        
        yield return null;
    }
}
