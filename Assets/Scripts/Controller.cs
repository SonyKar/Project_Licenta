using Behaviours;
using Building;
using JetBrains.Annotations;
using Targets;
using UnityEngine;
using UnityEngine.InputSystem;
using Behaviour = Behaviours.Behaviour;

public class Controller : MonoBehaviour
{
    [SerializeField] private CameraControl moveCamera;

    [UsedImplicitly]
    private void OnPerformAction()
    {
        RaycastHit hit = RayToMouse();
        if (hit.transform != null)
        {
            GameObject clickedObject = hit.transform.gameObject;
        
            if (clickedObject.CompareTag("Ground") && Gameplay.Instance.selectedObject)
            {
                Walker walker = Gameplay.Instance.selectedObject.GetComponent<Walker>();
                if (walker != null)
                {
                    walker.DoForGround(hit.point);
                }
            }
            else if (Gameplay.Instance.selectedObject)
            {
                BehaviourChooser behaviourChooser = Gameplay.Instance.selectedObject.GetComponent<BehaviourChooser>();
                if (behaviourChooser != null)
                {
                    Target target = clickedObject.GetComponent<Target>();
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
        if (hit.transform == null) return;
        if (Gameplay.Instance.GameMode == GameMode.Free)
        {
            Gameplay.Instance.SelectObject(hit.transform.gameObject);
        }
        else if (Gameplay.Instance.GameMode == GameMode.Build)
        {
            BuildingManager.Instance.Build(hit);
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

    [UsedImplicitly]
    private void OnToggleBuild()
    {
        Gameplay.Instance.GameMode = Gameplay.Instance.GameMode == GameMode.Build ?
        GameMode.Free :
        GameMode.Build;
        
        BuildingManager.Instance.ToggleBuildMode();
    }

    private RaycastHit RayToMouse()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main!.ScreenPointToRay(mousePos);
        Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, ~LayerMask.GetMask("Ignore Raycast"));
            
        return raycastHit;
    }
}
