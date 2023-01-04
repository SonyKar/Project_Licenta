using Targets;
using UnityEngine;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] private BuildingTypeSo activeBuildingType;
        [SerializeField] private Transform phantomPrefab;
        [SerializeField] private Transform buildingParent;

        private static Transform _buildings;
        
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
            _buildings = buildingParent;
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
                {
                    Vector3 point = raycastHit.point;
                    point.y = 0.5f;
                    Instantiate((Object)activeBuildingType.constructionPrefab, point, Quaternion.identity, buildingParent);
                }
            }
        }
        
        private bool CanSpawnBuilding(Vector3 position)
        {
            Collider[] results = new Collider[1];
            Physics.OverlapBoxNonAlloc(position, _constructionPrefabCollider.size * 2, results, Quaternion.identity, ~LayerMask.GetMask("Ground", "Ignore Raycast"));

            return !results[0];
        }
        
        public static Target GetClosestSawmill(Vector3 currentPosition)
        {
            Sawmill[] sawmillsPositions = _buildings.GetComponentsInChildren<Sawmill>();
        
            Sawmill nearestSawmill = null;
            float minDist = Mathf.Infinity;
            foreach (Sawmill s in sawmillsPositions)
            {
                float dist = Vector3.Distance(s.transform.position, currentPosition);
                if (!(dist < minDist)) continue;
                nearestSawmill = s;
                minDist = dist;
            }

            return nearestSawmill;
        }
    }
}
