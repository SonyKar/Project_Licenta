using UnityEngine;

namespace ControllableUnit
{
    public class Inventory : MonoBehaviour
    {
        [Header("Read Only")]
        [SerializeField] private int resourcesInHands;
        [SerializeField] private ResourceType resourceTypeInHands;

        [Header("Max Carry Capacity")] 
        [SerializeField] private int maxWood = 10;
        [SerializeField] private int maxStone = 10;

        public bool CanGrabMoreInHands(ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Wood => resourcesInHands < maxWood,
                ResourceType.Stone => resourcesInHands < maxStone,
                _ => false
            };
        }
        
        public bool CanGrabInHands(ResourceType resourceType)
        {
            if (resourcesInHands != 0 && resourceTypeInHands != resourceType) return false;
            return true;
        }
        
        public void AddResources(int resourceNumber, ResourceType resourceType)
        {
            if (CanGrabInHands(resourceType) && 
                CanGrabMoreInHands(resourceType))
            {
                resourcesInHands += resourceNumber;
                resourceTypeInHands = resourceType;
            }
        }
    }
}
