using UnityEngine;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private BuildingTypeSo activeBuildingType;
        [SerializeField] private Transform phantomPrefab;
        
        private BoxCollider _constructionPrefabCollider;
        private Transform _phantom;
        
        private static BuildingManager _instance;
        public static BuildingManager Instance => _instance;
        void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
                Destroy(this);
            
            _constructionPrefabCollider = activeBuildingType.constructionPrefab.GetComponent<BoxCollider>();
        }

        public void ToggleBuildMode()
        {
            if (Gameplay.Instance.GameMode == GameMode.Build)
            {
                _phantom ??= Instantiate(phantomPrefab);
            }
            else if (_phantom is not null)
            {
                Destroy(_phantom.gameObject);
                _phantom = null;
            }
        }

        public void Build(RaycastHit raycastHit)
        {
            if (CanSpawnBuilding(raycastHit.point))
            {
                if (raycastHit.transform.CompareTag("Ground"))
                    Instantiate((Object)activeBuildingType.constructionPrefab, raycastHit.point, Quaternion.identity);
            }
        }
        
        private bool CanSpawnBuilding(Vector3 position)
        {
            Collider[] results = new Collider[1];
            Physics.OverlapBoxNonAlloc(position, _constructionPrefabCollider.size * 2, results, Quaternion.identity, ~LayerMask.GetMask("Ground", "Ignore Raycast"));

            return !results[0];
        }
    }
}
