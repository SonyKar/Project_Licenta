using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 40f;
    
    private Vector3 _moveInput;

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.Translate(_moveInput * (Time.deltaTime * cameraSpeed), Space.World);
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        _moveInput = new Vector3(moveInput.x, 0, moveInput.y);
    }
}
