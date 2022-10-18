using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private GameObject viewCenter;
    
    [Header("Move")]
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float edgeTolerance = 0.01f;
    [SerializeField] private bool edgeMoveEnable = true;
    
    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 30f;
    [SerializeField] private float rotateRadius = 15f;

    [Header("Zoom")] 
    [SerializeField] private float zoomStepSize = 1f;
    [SerializeField] private float zoomSpeed = 100f;
    [SerializeField] private float maxZoom = 30f;
    [SerializeField] private float minZoom = 8f;
    
    private Vector3 _moveInput;
    private float _rotateInput;
    private float _zoomHeight;

    private void Start()
    {
        _zoomHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        MoveViewCenter();
        CheckMouseAtScreenEdge();
        RotateViewCenter();
        ActualiseCameraPosition();
    }

    private void MoveViewCenter()
    {
        Transform viewCenterTransform = viewCenter.transform;
        Vector3 moveValue = _moveInput.x * viewCenterTransform.right + _moveInput.z * viewCenterTransform.forward;
        moveValue.y = 0;
        viewCenter.transform.position += moveValue * (Time.deltaTime * moveSpeed);
    }

    private void CheckMouseAtScreenEdge()
    {
        if (edgeMoveEnable)
        {
            //mouse position is in pixels
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 moveDirection = Vector3.zero;

            //horizontal scrolling
            if (mousePosition.x < edgeTolerance * Screen.width)
                moveDirection += -Vector3.right;
            else if (mousePosition.x > (1f - edgeTolerance) * Screen.width)
                moveDirection += Vector3.right;

            //vertical scrolling
            if (mousePosition.y < edgeTolerance * Screen.height)
                moveDirection += -Vector3.forward;
            else if (mousePosition.y > (1f - edgeTolerance) * Screen.height)
                moveDirection += Vector3.forward;

            viewCenter.transform.position += moveDirection * (Time.deltaTime * moveSpeed);
        }
    }

    private void RotateViewCenter()
    {
        var rotation = viewCenter.transform.rotation;
        float newAngle = rotation.eulerAngles.y + _rotateInput * rotateSpeed * Time.deltaTime;
        
        viewCenter.transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, newAngle, rotation.eulerAngles.z);
    }

    private void ActualiseCameraPosition()
    {
        var position = transform.position;
        float newAngle = viewCenter.transform.eulerAngles.y;

        Vector3 newCameraPosition = new Vector3(-rotateRadius * (float)Math.Sin(Mathf.Deg2Rad * newAngle),
                                        _zoomHeight,
                                        -rotateRadius * (float)Math.Cos(Mathf.Deg2Rad * newAngle))
                                    + viewCenter.transform.position;

        position = new Vector3(newCameraPosition.x, position.y, newCameraPosition.z);
        transform.position = Vector3.Lerp(position, newCameraPosition, Time.deltaTime * zoomSpeed);
        transform.LookAt(viewCenter.transform);
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        _moveInput = new Vector3(moveInput.x, 0, moveInput.y);
    }

    public void SetRotateInput(Vector2 rotateInput)
    {
        _rotateInput = rotateInput.x;
    }

    public void SetZoomInput(Vector2 zoomInput)
    {
        float value = -zoomInput.y / 100f;
        
        // If value != 0
        if (Math.Abs(value) > 0.1f)
        {
            _zoomHeight = transform.localPosition.y + value * zoomStepSize;
            if (_zoomHeight < minZoom) _zoomHeight = minZoom;
            else if (_zoomHeight > maxZoom) _zoomHeight = maxZoom;
        }
    }
}
