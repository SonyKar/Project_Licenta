using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GameObject selectedObject;

    public void SetSelectedObject(GameObject clickedObject)
    {
        selectedObject = clickedObject;
    }
}
