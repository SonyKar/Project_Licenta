using System.Collections;
using ControllableUnit;
using Model;
using Targets;
using UnityEngine;

namespace Actions
{
    public class Mine : Action
    {
        private readonly Mineable _mineable;
        private readonly Inventory _inventory;
        private readonly int _damage;
        private readonly float _secondsBetweenHits;
        private bool _isMining;

        public Mine(ActionDoer actionDoer, Inventory inventory, Mineable mineable, int damage, float secondsBetweenHits)
        : base(actionDoer)
        {
            _mineable = mineable;
            _damage = damage;
            _secondsBetweenHits = secondsBetweenHits;
            _inventory = inventory;
        }
        
        public override void Do()
        {
            if (!_isMining)
            {
                ActiveObject.StartNewCoroutine(MineResource());
            }
        }
        
        private IEnumerator MineResource()
        {
            while (true)
            {
                _isMining = true;
                yield return new WaitForSeconds(_secondsBetweenHits);
                if (_mineable.IsDepleted())
                {
                    Debug.Log("The tree is depleted");
                    // TODO find another tree
                    break;
                }

                int resourcesToGet = _inventory.ResourcesUntilMax(_mineable.GetResourceType());
                if (resourcesToGet >= _damage) resourcesToGet = _damage;
                
                ResourceBundle collectedResources = _mineable.TakeHit(resourcesToGet);
                if (!_inventory.AddResources(collectedResources))
                {
                    Debug.Log("Finished!");
                    break;
                }
            }

            _isMining = false;
            if (_mineable.IsDepleted()) ActiveObject.ClearActionQueue();
            else ActiveObject.NextAction();
            yield return new WaitForSeconds(0f);
        }
    }
}
