using System.Collections;
using ControllableUnit;
using Targets;
using UnityEngine;

namespace Actions
{
    public class Mine : Action
    {
        private readonly ActionDoer _activeObject;
        private readonly Mineable _mineable;
        private readonly Inventory _inventory;
        private readonly int _damage;
        private readonly float _secondsBetweenHits;
        private bool _isMining;

        public Mine(ActionDoer actionDoer, Inventory inventory, Mineable mineable, int damage, float secondsBetweenHits)
        {
            _activeObject = actionDoer;
            _mineable = mineable;
            _damage = damage;
            _secondsBetweenHits = secondsBetweenHits;
            _inventory = inventory;
        }
        
        public override void Do()
        {
            if (!_isMining)
            {
                _activeObject.StartNewCoroutine(MineResource());
            }
        }
        
        private IEnumerator MineResource()
        {
            while (true)
            {
                _isMining = true;
                yield return new WaitForSeconds(_secondsBetweenHits);
                if (_mineable == null)
                {
                    Debug.Log("No more tree");
                    _activeObject.ClearActionQueue();
                    break;
                }
                if (_inventory.CanGrabInHands(_mineable.resourceType) &&
                    _inventory.CanGrabMoreInHands(_mineable.resourceType))
                {
                    int collectedResources = _mineable.TakeHit(_damage);
                    _inventory.AddResources(collectedResources, _mineable.resourceType);
                }
                else
                {
                    Debug.Log("Finished");
                    _activeObject.ClearActionQueue();
                }
            }

            _activeObject.NextAction();
            yield return new WaitForSeconds(0f);
        }
    }
}
