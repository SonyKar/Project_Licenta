using Targets;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GameObject selectedObject;
    public GameObject sawmills;

    private static Gameplay _instance;
    public static Gameplay GameplayObject => _instance;
    void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(this);
    }
    
    public Sawmill GetClosestSawmill(Vector3 currentPosition)
    {
        Sawmill[] sawmillsPositions = sawmills.GetComponentsInChildren<Sawmill>();
        
        Sawmill nearestSawmill = null;
        float minDist = Mathf.Infinity;
        foreach (Sawmill s in sawmillsPositions)
        {
            float dist = Vector3.Distance(s.transform.position, currentPosition);
            if (dist < minDist)
            {
                nearestSawmill = s;
                minDist = dist;
            }
        }

        return nearestSawmill;
    }
    
    public void SetSelectedObject(GameObject clickedObject)
    {
        selectedObject = clickedObject;
    }
}
