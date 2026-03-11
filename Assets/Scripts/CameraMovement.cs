using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public bool HasClick;
    public GameObject Target;
    public Vector3 baseClick;
    public Vector3 lastClick;

    public Camera cam;

    public float speedX = 1;
    public float speedY = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (HasClick)
        {
            baseClick.x = Input.mousePosition.x;
            baseClick.z = cam.pixelHeight - Input.mousePosition.y;
            
            Vector3 offsetVec = baseClick - lastClick;
            float offset = Vector3.Magnitude(offsetVec);
            if (offset != 0)
            {
                lastClick = baseClick;
                Target.transform.position += new Vector3(offsetVec.x / 2 * speedX + offsetVec.z / 2 * speedY, 0, -offsetVec.x / 2 * speedX + offsetVec.z / 2 * speedY) * Time.deltaTime;
            }
            if (Input.GetMouseButtonUp(0))
            {
                HasClick = false;
                baseClick = Vector3.zero;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                HasClick = true;
                baseClick.x = Input.mousePosition.x;
                baseClick.z = cam.pixelHeight - Input.mousePosition.y;
                lastClick = baseClick;
            }
        }
        
        

    }
}
