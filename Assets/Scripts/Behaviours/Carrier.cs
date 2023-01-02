using System;
using Actions;
using ControllableUnit;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Carrier : Behaviour
    {
        public override void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) activeObject.ClearActionQueue();
            if (Inventory != null)
            {
                if (Inventory.GetCurrentResourceType() == ResourceType.Wood)
                {
                    if (Walker != null)
                    {
                        Walker.MoveToObject(sawmill);
                    }
                    
                    StockResource stockResource = new StockResource(activeObject, Inventory);
                    activeObject.AddAction(stockResource);
                }
            }
        }
    }
}