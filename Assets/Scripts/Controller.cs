using System;
using Behaviours;
using Building;
using JetBrains.Annotations;
using Targets;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Behaviour = Behaviours.Behaviour;

public class Controller : MonoBehaviour
{
    [SerializeField] private CameraControl moveCamera;

    private static Camera _camera;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    [UsedImplicitly]
    private void OnPerformAction()
    {
        RaycastHit hit = RayToMouse();
        if (hit.transform is null) return;
        GameObject clickedObject = hit.transform.gameObject;

        if (!Gameplay.Instance.selectedObject) return;
        if (clickedObject.CompareTag("Ground"))
        {
            Walker walker = Gameplay.Instance.selectedObject.GetComponent<Walker>();
            if (walker != null)
            {
                walker.DoForGround(hit.point);
            }
        }
        else
        {
            BehaviourChooser behaviourChooser = Gameplay.Instance.selectedObject.GetComponent<BehaviourChooser>();
            if (behaviourChooser is null) return;
                
            Target target = clickedObject.GetComponent<Target>();
            if (target is null) return;
                
            Behaviour behaviour = target.BestBehaviour(behaviourChooser);
            if (behaviour is not null)
            {
                target.Behave(behaviour);
            }
        }
    }
    
    [UsedImplicitly]
    private void OnSelectActor()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        RaycastHit hit = RayToMouse();
        if (hit.transform is null) return;
        switch (Gameplay.Instance.GameMode)
        {
            case GameMode.Free:
                Gameplay.Instance.SelectObject(hit.transform.gameObject);
                break;
            case GameMode.Build:
                BuildingManager.Instance.Build(hit);
                break;
            default:
                throw new ArgumentOutOfRangeException();
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
    public void OnToggleBuild()
    {
        BuildingMenu.Instance.ToggleBuildingMenu();
    }

    public static RaycastHit RayToMouse()
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        Ray ray = _camera.ScreenPointToRay(mousePos);
        Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, ~LayerMask.GetMask("Ignore Raycast"));
            
        return raycastHit;
    }
}
