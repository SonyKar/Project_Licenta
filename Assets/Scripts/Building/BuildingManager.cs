using UnityEngine;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private BuildingTypeSo activeBuildingType;
        [SerializeField] private Transform phantomPrefab;
    
        private Camera _mainCamera;
        private BoxCollider _constructionPrefabCollider;
        private Transform _phantom;
    
        private void Awake()
        {
            _mainCamera = Camera.main;
            _constructionPrefabCollider = activeBuildingType.constructionPrefab.GetComponent<BoxCollider>();
        }

        void Update()
        {
            _phantom ??= Instantiate(phantomPrefab);
            if (Input.GetMouseButton(0))
            {
                RaycastHit raycastHit = RayToMouse();
                if (CanSpawnBuilding(raycastHit.point))
                {
                    if (raycastHit.transform.CompareTag("Ground"))
                        Instantiate((Object)activeBuildingType.constructionPrefab, raycastHit.point, Quaternion.identity);
                }
            }
        }
    
        private RaycastHit RayToMouse()
        {
            Vector3 mousePos = Input.mousePosition; // Mouse.current.position.ReadValue()
            Ray ray = _mainCamera!.ScreenPointToRay(mousePos);
            Physics.Raycast(ray, out RaycastHit raycastHit, 1000f, ~LayerMask.GetMask("Ignore Raycast"));
            
            return raycastHit;
        }

        private bool CanSpawnBuilding(Vector3 position)
        {
            Collider[] results = new Collider[1];
            Physics.OverlapBoxNonAlloc(position, _constructionPrefabCollider.size * 2, results, Quaternion.identity, ~LayerMask.GetMask("Ground", "Ignore Raycast"));

            return !results[0];
        }
    }
}
