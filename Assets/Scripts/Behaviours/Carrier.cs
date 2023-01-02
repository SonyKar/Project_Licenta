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
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            
            if (Inventory == null)
            {
                Debug.Log("No Inventory");
                return;
            }
            if (Inventory.GetCurrentResourceType() != ResourceType.Wood) return;
            
            if (Walker == null) Debug.Log("No Walker Behaviour");
            Walker.MoveToObject(sawmill);

            StockResource stockResource = new StockResource(ActiveObject, Inventory);
            ActiveObject.AddAction(stockResource);
        }
    }
}