using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 40f;
    [SerializeField] private float rotateSpeed = 20f;
    
    private Vector3 _moveInput;
    private float _rotateInput;

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        RotateCamera();
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
        transform.Rotate(0, _rotateInput * Time.deltaTime * rotateSpeed, 0, Space.World);
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        _moveInput = new Vector3(moveInput.x, 0, moveInput.y);
    }

    public void SetRotateInput(Vector2 rotateInput)
    {
        _rotateInput = rotateInput.x;
    }
}
