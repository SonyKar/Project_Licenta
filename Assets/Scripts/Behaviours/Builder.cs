using Actions;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Builder : Behaviour
    {
        [SerializeField] private int healthToAddAtHit = 10;
        [SerializeField] private float secondsBetweenHits = 1f;

        public override void DoForConstruction(Target construction, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            
            if (Walker == null) Debug.Log("No Walker Behaviour");
            Walker.MoveToObject(construction);
            
            ActiveObject.AddAction(new Build(ActiveObject, secondsBetweenHits, (BuildingConstruction)construction, healthToAddAtHit));
        }
    }
}
