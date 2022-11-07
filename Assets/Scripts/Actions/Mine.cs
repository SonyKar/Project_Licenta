using System.Collections;
using Targets;
using UnityEngine;

namespace Actions
{
    public class Mine : Action
    {
        private readonly ActionDoer _activeObject;
        private readonly Mineable _mineable;
        private readonly int _damage;
        private readonly float _secondsBetweenHits;
        private bool _isMining;

        public Mine(ActionDoer actionDoer, Mineable mineable, int damage, float secondsBetweenHits)
        {
            _activeObject = actionDoer;
            _mineable = mineable;
            _damage = damage;
            _secondsBetweenHits = secondsBetweenHits;
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
                    break;
                }
                _mineable.TakeHit(_damage);
            }

            _activeObject.NextAction();
            yield return new WaitForSeconds(0f);
        }
    }
}
