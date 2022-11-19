using Actions;
using ControllableUnit;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Carrier : Behaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private Walker walker;
        
        public override void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) activeObject.ClearActionQueue();
            if (inventory != null)
            {
                if (inventory.GetCurrentResourceType() == ResourceType.Wood)
                {
                    if (walker != null)
                    {
                        walker.MoveToObject(sawmill);
                    }
                    
                    StockResource stockResource = new StockResource(activeObject, inventory);
                    activeObject.AddAction(stockResource);
                }
            }
        }
    }
}