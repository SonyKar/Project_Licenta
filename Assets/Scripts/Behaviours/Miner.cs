using Actions;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Miner : Behaviour
    {
        [Header("Miner Properties")]
        [SerializeField] private int resourcesFromHit = 50;
        [SerializeField] private float secondsBetweenHits = 1.5f;

        public override void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            if (Walker == null) 
            {
                Debug.Log("No Walker Behaviour");
                return;
            }
            Walker.MoveToObject(tree);

            if (Inventory == null) 
            {
                Debug.Log("No Inventory");
                return;
            }
            
            Mine mine = new Mine(ActiveObject, ActionProgress, Inventory, (Mineable)tree, resourcesFromHit, secondsBetweenHits);
            ActiveObject.AddAction(mine);

            if (Carrier != null)
            {
                Walker.MoveToNearest(Locator.GetClosestSawmill);
            
                StockResource stockResource = new StockResource(ActiveObject, Inventory);
                ActiveObject.AddAction(stockResource);   
            }
        }
    }
}
