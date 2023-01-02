using System;
using Actions;
using ControllableUnit;
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
            if (doCleanActionQueue) activeObject.ClearActionQueue();
            if (Walker != null)
            {
                Walker.MoveToObject(tree);
            }

            Mine mine = new Mine(activeObject, Inventory, (Mineable)tree, resourcesFromHit, secondsBetweenHits);
            activeObject.AddAction(mine);
            
            Sawmill sawmill = Gameplay.Instance.GetClosestSawmill(tree.transform.position);
            if (Carrier != null)
            {
                Carrier.DoForSawmill(sawmill, false);
            }
        }
    }
}
