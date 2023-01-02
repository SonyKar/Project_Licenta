using UnityEngine;

namespace Building
{
    [CreateAssetMenu]
    public class BuildingTypeSo : ScriptableObject
    {
        public Transform prefab;
        public Transform constructionPrefab;
        public int constructionHealth = 100;
    }
}
