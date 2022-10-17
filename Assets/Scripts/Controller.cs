using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private Gameplay gameplay;
    [SerializeField] private CameraControl moveCamera;
    
    [UsedImplicitly]
    private void OnPerformAction()
    {
        RaycastHit hit = RayToMouse();
        GameObject clickedObject = hit.transform.gameObject;
        
        if (clickedObject.CompareTag("Ground") && gameplay.selectedObject)
        {
            NavMeshAgent navMeshAgent = gameplay.selectedObject.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.destination = hit.point;
            }
        }
    }
    
    [UsedImplicitly]
    private void OnSelectActor()
    {
        RaycastHit hit = RayToMouse();
        GameObject clickedObject = hit.transform.gameObject;
        Selectable clickedObjectSelectable = clickedObject.GetComponent<Selectable>();
        
        if (clickedObjectSelectable != null)
        {
            if (gameplay.selectedObject != null)
            {
                gameplay.selectedObject.GetComponent<Selectable>().OnDeselect();
            }
            gameplay.SetSelectedObject(clickedObject);
            clickedObjectSelectable.OnSelect();
        }
    }

    [UsedImplicitly]
    private void OnMoveCamera(InputValue value)
    {
        moveCamera.SetMoveInput(value.Get<Vector2>());
    }

    [UsedImplicitly]
    private void OnRotateCamera(InputValue value)
    {
        if (Mouse.current.middleButton.isPressed)
        {
            moveCamera.SetRotateInput(value.Get<Vector2>());
        }
    }

    private RaycastHit RayToMouse()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main!.ScreenPointToRay(mousePos);
        Physics.Raycast(ray, out RaycastHit raycastHit);
            
        return raycastHit;
    }
}
