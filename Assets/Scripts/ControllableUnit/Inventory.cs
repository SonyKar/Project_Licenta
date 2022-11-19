using Model;
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

        public int ResourcesUntilMax(ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Wood => maxWood - resourcesInHands,
                ResourceType.Stone => maxStone - resourcesInHands,
                _ => 0
            };
        }
        
        private bool IsMoreSpaceFor(ResourceType resourceType)
        {
            return resourceType switch
            {
                ResourceType.Wood => resourcesInHands < maxWood,
                ResourceType.Stone => resourcesInHands < maxStone,
                _ => false
            };
        }
        
        private bool CanGrabInHands(ResourceType resourceType)
        {
            if (resourcesInHands != 0 && resourceTypeInHands != resourceType) return false;
            return IsMoreSpaceFor(resourceType);
        }
        
        public bool AddResources(ResourceBundle resource)
        {
            if (CanGrabInHands(resource.ResourceType))
            {
                resourcesInHands += resource.ResourceNumber;
                resourceTypeInHands = resource.ResourceType;
                return true;
            }

            return false;
        }

        public void ClearInventory()
        {
            resourcesInHands = 0;
        }

        public ResourceType GetCurrentResourceType()
        {
            return resourceTypeInHands;
        }
    }
}
