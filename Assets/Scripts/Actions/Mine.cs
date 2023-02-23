using System.Collections;
using Behaviours;
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
            ActiveObject.UpdateProgress(
                _inventory.GetResourceNumberHeld() * 1.0f / 
                _inventory.GetMaxResourceAmount(_inventory.GetCurrentResourceType())
            );
            ActiveObject.ShowProgress();

            while (true)
            {
                _isMining = true;
                int resourcesToGet = _inventory.ResourcesUntilMax(_mineable.GetResourceType());
                if (resourcesToGet <= 0) break;
                
                yield return new WaitForSeconds(_secondsBetweenHits);
                
                if (_mineable.IsDepleted()) break;
                if (resourcesToGet >= _damage) resourcesToGet = _damage;
                
                ResourceBundle collectedResources = _mineable.TakeHit(resourcesToGet);
                if (!_inventory.AddResources(collectedResources))
                {
                    break;
                }
                ActiveObject.UpdateProgress(
                    _inventory.GetResourceNumberHeld() * 1.0f / 
                    _inventory.GetMaxResourceAmount(_inventory.GetCurrentResourceType())
                );
            }

            ActiveObject.HideProgress();
            _isMining = false;
            if (_mineable.IsDepleted())
            {
                Target tree = Locator.FindNearestTree(_mineable.transform.position);
                if (tree is null) ActiveObject.ClearActionQueue();
                else ActiveObject.GetComponent<Miner>().DoForTree(tree);
            }
            else ActiveObject.NextAction();
            yield return new WaitForSeconds(0f);
        }
    }
}
