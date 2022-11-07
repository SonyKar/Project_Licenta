using Behaviours;
using JetBrains.Annotations;
using Targets;
using UnityEngine;
using UnityEngine.InputSystem;
using Behaviour = Behaviours.Behaviour;

public class Controller : MonoBehaviour
{
    [SerializeField] private Gameplay gameplay;
    [SerializeField] private CameraControl moveCamera;
    
    [UsedImplicitly]
    private void OnPerformAction()
    {
        RaycastHit hit = RayToMouse();
        if (hit.transform != null)
        {
            GameObject clickedObject = hit.transform.gameObject;
        
            if (clickedObject.CompareTag("Ground") && gameplay.selectedObject)
            {
                Walker walker = gameplay.selectedObject.GetComponent<Walker>();
                if (walker != null)
                {
                    walker.DoForGround(hit.point);
                }
            }
            else if (gameplay.selectedObject)
            {
                BehaviourChooser behaviourChooser = gameplay.selectedObject.GetComponent<BehaviourChooser>();
                if (behaviourChooser != null)
                {
                    ITarget target = clickedObject.GetComponent<ITarget>();
                    if (target != null)
                    {
                        Behaviour behaviour = target.BestBehaviour(behaviourChooser);
                        if (behaviour != null)
                        {
                            target.Behave(behaviour);
                        }
                    }
                }
            }
        }
    }
    
    [UsedImplicitly]
    private void OnSelectActor()
    {
        RaycastHit hit = RayToMouse();
        if (hit.transform != null)
        {
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
    }

    [UsedImplicitly]
    private void OnMoveCamera(InputValue value)
    {
        moveCamera.SetMoveInput(value.Get<Vector2>());
    }

    [UsedImplicitly]
    private void OnRotateCamera(InputValue value)
    {
        moveCamera.SetRotateInput(Mouse.current.middleButton.isPressed ? 
            value.Get<Vector2>() : 
            new Vector2(0, 0)
        );
    }

    [UsedImplicitly]
    private void OnZoomCamera(InputValue value)
    {
        moveCamera.SetZoomInput(value.Get<Vector2>());
    }

    private RaycastHit RayToMouse()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main!.ScreenPointToRay(mousePos);
        Physics.Raycast(ray, out RaycastHit raycastHit);
            
        return raycastHit;
    }
}
