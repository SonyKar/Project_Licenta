using Actions;
using ControllableUnit;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Miner : Behaviour
    {
        [SerializeField] private int resourcesFromHit = 50;
        [SerializeField] private float secondsBetweenHits = 1.5f;

        [SerializeField] private Inventory inventory;
        
        private Walker _walker;

        private void Awake()
        {
            _walker = GetComponent<Walker>();
        }

        public override void DoForTree(ITarget tree)
        {
            if (_walker != null)
            {
                _walker.DoForTree(tree);
            }

            Mine mine = new Mine(doerObject, inventory, (Mineable)tree, resourcesFromHit, secondsBetweenHits);
            doerObject.AddAction(mine);
        }
    }
}
