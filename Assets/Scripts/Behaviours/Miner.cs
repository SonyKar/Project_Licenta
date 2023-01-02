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
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            if (Walker == null) 
            {
                Debug.Log("No Walker Behaviour");
                return;
            }
            Walker.MoveToObject(tree);

            Mine mine = new Mine(ActiveObject, Inventory, (Mineable)tree, resourcesFromHit, secondsBetweenHits);
            ActiveObject.AddAction(mine);
            
            Sawmill sawmill = Gameplay.Instance.GetClosestSawmill(tree.transform.position);
            if (Carrier == null) 
            {
                Carrier.DoForSawmill(sawmill, false);
                Debug.Log("No Carrier Behaviour");
            }
        }
    }
}
