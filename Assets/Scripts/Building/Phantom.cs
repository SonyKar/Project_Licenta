using UnityEngine;

namespace Building
{
    public class Phantom : MonoBehaviour
    {
        [SerializeField] private Material okMaterial;
        [SerializeField] private Material errorMaterial;
        
        private Transform _transform;
        private BoxCollider _phantomCollider;
        private MeshRenderer _meshRenderer;

        private void Awake()
        {
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
    }
}
