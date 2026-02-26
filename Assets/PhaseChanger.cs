using UnityEngine;

public class PhaseChanger : MonoBehaviour
{
    public GameObject Village;
    public GameObject Terrain;
    public GameObject UI;
    public GameObject UIExploration;

    public void StartExploration()
    {

        Village.SetActive(false);
        Terrain.SetActive(true);
        UI.SetActive(false);
        UIExploration.SetActive(false);
        
    }
    
    
}
