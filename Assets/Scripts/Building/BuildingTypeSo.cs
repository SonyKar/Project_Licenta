using System.Collections.Generic;
using Model;
using UnityEngine;

namespace Building
{
    [CreateAssetMenu]
    public class BuildingTypeSo : ScriptableObject
    {
        [Header("Prefab info")]
        public Transform prefab;
        public Transform constructionPrefab;
        public int constructionHealth = 100;
        
        [Header("Requirements")]
        [SerializeField] private int wood;
        [SerializeField] private int stone;

        public IEnumerable<ResourceBundle> GetResourceRequirements()
        {
            List<ResourceBundle> resourceBundle = new List<ResourceBundle>();
            if (wood > 0) resourceBundle.Add(new ResourceBundle(wood, ResourceType.Wood));
            if (stone > 0) resourceBundle.Add(new ResourceBundle(stone, ResourceType.Stone));

            return resourceBundle;
        }
    }
}
