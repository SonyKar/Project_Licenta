using UnityEngine;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private BuildingTypeSo activeBuildingType;
        [SerializeField] private Transform phantomPrefab;
        [SerializeField] private Transform buildingParent;

        private BoxCollider _constructionPrefabCollider;
        private Transform _phantom;

        public static BuildingManager Instance { get; private set; }
        void Awake()
        {
            if (Instance == null)
                Instance = this;
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
            if (!CanSpawnBuilding(raycastHit.point))
            {
                Gameplay.Instance.ShowError("Cannot build here");
                return;
            }
            if (!raycastHit.transform.CompareTag("Ground"))
            {
                Gameplay.Instance.ShowError("Cannot build here");
                return;
            }
            if (!ResourceManager.Instance.SpendResources(activeBuildingType.GetResourceRequirements()))
            {
                Gameplay.Instance.ShowError("You don't have enough resources");
                return;
            }
            
            Vector3 point = raycastHit.point;
            point.y = 0.5f;
            Instantiate((Object)activeBuildingType.constructionPrefab, point, Quaternion.identity, buildingParent);
        }
        
        public bool CanSpawnBuilding(Vector3 position)
        {
            Collider[] results = new Collider[1];
            Physics.OverlapBoxNonAlloc(position, _constructionPrefabCollider.size * 2, results, Quaternion.identity, ~LayerMask.GetMask("Ground", "Ignore Raycast"));

            return !results[0];
        }
    }
}
