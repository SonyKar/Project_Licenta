using Building;
using ControllableUnit;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public GameMode GameMode { get; private set; } = GameMode.Free;
    public GameObject selectedObject;
    public GameObject unitParent;

    public static Gameplay Instance { get; private set; }
    void Awake()
    {
        if(Instance is null)
            Instance = this;
        else
            Destroy(this);
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

    public void ToggleBuildMode()
    {
        GameMode = GameMode == GameMode.Free ? GameMode.Build : GameMode.Free;
        BuildingManager.Instance.ToggleBuildMode();
    }
}
