using UnityEngine;

namespace Building
{
    public class BuildingConstruction : MonoBehaviour
    {
        [SerializeField] private BuildingTypeSo constructedPrefab;
        [SerializeField] private float timeToConstruct = 2f;

        private float _constructionTimer;
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        private void Start()
        {
            // Instantiate(particles)
            // _material.SetFloat("_Progress", 0f);
        }

        private void Update()
        {
            _constructionTimer += Time.deltaTime / timeToConstruct;

            if (_constructionTimer >= 1f)
            {
                Transform constructionTransform = transform;
                Instantiate((Object)constructedPrefab.prefab, constructionTransform.position, Quaternion.identity, constructionTransform.parent);
                Destroy(gameObject);
            }
        }
    }
}
