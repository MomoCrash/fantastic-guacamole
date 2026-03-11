using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour
{
    private Camera cam;
    public int lifes;
    public Coroutine startedCor;
    public Vector3 baseScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        lifes = Random.Range(3, 8);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
    }
    
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(other.name);
            baseScale = transform.localScale;
            startedCor = StartCoroutine(CutTreeNearPlayer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(startedCor);
        transform.localScale = baseScale;
    }

    IEnumerator CutTreeNearPlayer()
    {
        while (lifes > 0)
        {
            transform.DOScale(new Vector3(baseScale.x * 1.2f, baseScale.y * 1.05f, baseScale.z * 1.2f), 0.2f);
            yield return new WaitForSeconds(0.2f);
            transform.DOScale(baseScale, 0.2f);
            yield return new WaitForSeconds(0.2f);
            lifes--;
        }
        
        transform.DOScale(new Vector3(0, baseScale.y, 0), 0.1f);
        Destroy(gameObject, 0.1f);
        
    }
}
