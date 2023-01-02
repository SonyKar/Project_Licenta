using ControllableUnit;
using Targets;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GameMode GameMode { get; set; } = GameMode.Free;
    public GameObject selectedObject;
    public GameObject sawmills;

    public static Gameplay Instance { get; private set; }
    void Awake()
    {
        if(Instance is null)
            Instance = this;
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
    
    public void SelectObject(GameObject clickedObject)
    {
        Selectable clickedObjectSelectable = clickedObject.GetComponent<Selectable>();

        if (clickedObjectSelectable is null) return;
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Selectable>().OnDeselect();
        }
        selectedObject = clickedObject;
        clickedObjectSelectable.OnSelect();
    }
}
