using System.Collections;
using Behaviours;
using ControllableUnit;
using Model;
using Targets;
using UI;
using UnityEngine;

namespace Actions
{
    public class Mine : Action
    {
        private readonly CircleProgress _actionProgress;
        private readonly Mineable _mineable;
        private readonly Inventory _inventory;
        private readonly int _damage;
        private readonly float _secondsBetweenHits;
        private bool _isMining;

        public Mine(ActionDoer actionDoer, CircleProgress actionProgress, Inventory inventory, Mineable mineable, int damage, float secondsBetweenHits)
        : base(actionDoer)
        {
            _actionProgress = actionProgress;
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
            _actionProgress.ChangeProgress(
                _inventory.GetResourceNumberHeld() * 1.0f / 
                _inventory.GetMaxResourceAmount(_inventory.GetCurrentResourceType())
            );
            _actionProgress.Show();

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
                _actionProgress.ChangeProgress(
                    _inventory.GetResourceNumberHeld() * 1.0f / 
                    _inventory.GetMaxResourceAmount(_inventory.GetCurrentResourceType())
                );
            }

            _actionProgress.Hide();
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
