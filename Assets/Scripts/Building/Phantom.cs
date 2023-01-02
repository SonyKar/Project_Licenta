using UnityEngine;

namespace Building
{
    public class Phantom : MonoBehaviour
    {
        [SerializeField] private Material okMaterial;
        [SerializeField] private Material errorMaterial;
        
        //private Camera _camera;
        private Transform _transform;
        private BoxCollider _phantomCollider;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            //_camera = Camera.main;
            _transform = transform;
            _phantomCollider = GetComponent<BoxCollider>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            Vector3 newPos = Controller.RayToMouse().point;
            newPos.y = _transform.localScale.y / 2;
            _transform.position = newPos;
            
            Collider[] results = new Collider[1];
            Physics.OverlapBoxNonAlloc(_transform.position, _phantomCollider.size * 2, results, Quaternion.identity, ~LayerMask.GetMask("Ground", "Ignore Raycast"));
            _meshRenderer.material = results[0] ? errorMaterial : okMaterial;
        }

        // private RaycastHit RayToMouse()
        // {
        //     Vector3 mousePos = Input.mousePosition; // Mouse.current.position.ReadValue()
        //     Ray ray = _camera.ScreenPointToRay(mousePos);
        //     Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, ~LayerMask.GetMask("Ignore Raycast"));
        //     
        //     return raycastHit;
        // }
    }
}
