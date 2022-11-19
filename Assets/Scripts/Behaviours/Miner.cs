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

        [Header("Additional Behaviours")]
        [SerializeField] private Inventory inventory;
        [SerializeField] private Carrier carrier;
        [SerializeField] private Walker walker;

        private void Update()
        {
            // if (_target.IsDepleted())
            // {
            //     _target
            //     _target.Behave(this);
            // }
        }

        public override void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) activeObject.ClearActionQueue();
            if (walker != null)
            {
                walker.MoveToObject(tree);
            }

            Mine mine = new Mine(activeObject, inventory, (Mineable)tree, resourcesFromHit, secondsBetweenHits);
            activeObject.AddAction(mine);
            
            Sawmill sawmill = Gameplay.GameplayObject.GetClosestSawmill(transform.position);
            if (carrier != null)
            {
                carrier.DoForSawmill(sawmill, false);
            }
        }
    }
}
