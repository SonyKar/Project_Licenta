using System;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 40f;
    [Header("Rotate")]
    [SerializeField] private float rotateSpeed = 30f;
    [Header("Zoom")]
    [SerializeField] private float zoomSpeed = 20f;
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
        MoveCamera();
        RotateCamera();
        ZoomCamera();
    }

    private void MoveCamera()
    {
        Transform cameraTransform = transform;
        Vector3 moveValue = _moveInput.x * cameraTransform.right + _moveInput.z * cameraTransform.forward;
        moveValue.y = 0;
        transform.Translate(moveValue * (Time.deltaTime * moveSpeed), Space.World);
    }

    private void RotateCamera()
    {
        var cameraTransform = transform;
        var position = cameraTransform.position;
        var rotation = cameraTransform.rotation;

        transform.rotation = Quaternion.Euler(rotation.eulerAngles.x, rotation.eulerAngles.y + _rotateInput * rotateSpeed * Time.deltaTime, rotation.eulerAngles.z);

        Vector3 newCameraPosition = new Vector3(position.x + -_rotateInput * Time.deltaTime * rotateSpeed, position.y, position.z);
        transform.position = Vector3.Lerp(position, newCameraPosition, Time.deltaTime);
    }

    private void ZoomCamera()
    {
        var localPosition = transform.localPosition;
        Vector3 zoomTarget = new Vector3(localPosition.x, _zoomHeight, localPosition.z);

        localPosition = Vector3.Lerp(localPosition, zoomTarget, Time.deltaTime * zoomSpeed);
        transform.localPosition = localPosition;
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
            float localZoomHeight = transform.localPosition.y + value;
            if (localZoomHeight < minZoom) localZoomHeight = minZoom;
            else if (localZoomHeight > maxZoom) localZoomHeight = maxZoom;
            _zoomHeight = localZoomHeight;
        }
    }
}
