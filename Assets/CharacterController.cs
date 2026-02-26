using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    public float Speed;
    public CinemachineCamera CinemachineCamera;

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(-Input.GetAxis("Horizontal") - Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") - Input.GetAxis("Vertical"));
        transform.position += movement * (Speed * Time.deltaTime);

        CinemachineCamera.Lens.OrthographicSize -= Input.mouseScrollDelta.y;

    }

}
