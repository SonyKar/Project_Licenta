using System.Collections;
using ControllableUnit;
using Targets;
using UnityEngine;

namespace Actions
{
    public class Build : Action
    {
        private readonly float _secondsBetweenHits;
        private readonly BuildingConstruction _construction;
        private readonly int _healthToAdd;
        private bool _isBuilding;
        
        public Build(ActionDoer actionDoer, float secondsBetweenHits, BuildingConstruction construction, int healthToAdd) : base(actionDoer)
        {
            _secondsBetweenHits = secondsBetweenHits;
            _construction = construction;
            _healthToAdd = healthToAdd;
        }
        
        public override void Do()
        {
            if (!_isBuilding)
            {
                ActiveObject.StartNewCoroutine(Construct());
            }
        }
        
        private IEnumerator Construct()
        {
            ActiveObject.transform.LookAt(_construction.transform);
            ActiveObject.GetAnimator().SetBuildingAnimation();
            
            while (true)
            {
                _isBuilding = true;
                if (_construction is null || _construction.IsConstructed()) break;
                yield return new WaitForSeconds(_secondsBetweenHits);
                // if (_construction is null || _construction.IsConstructed()) break;

                _construction.BuildTick(_healthToAdd);
            }

            _isBuilding = false;
            if (_construction is null || _construction.IsConstructed()) ActiveObject.ClearActionQueue();
            else ActiveObject.NextAction();
            yield return new WaitForSeconds(0f);
        }
    }
}
