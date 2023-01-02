using Behaviours;
using Building;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public class BuildingConstruction : Target
    {
        [SerializeField] private BuildingTypeSo constructionInfo;
        
        [SerializeField] private int constructionProgress;

        private void Update()
        {
            if (IsConstructed())
            {
                Transform constructionTransform = transform;
                Instantiate(constructionInfo.prefab, constructionTransform.position, Quaternion.identity, constructionTransform.parent);
                Destroy(gameObject);
            }
        }

        public void BuildTick(int healthToAdd)
        {
            constructionProgress += healthToAdd;
        }

        public bool IsConstructed()
        {
            return constructionProgress >= constructionInfo.constructionHealth;
        }
        
        public override Behaviour BestBehaviour(BehaviourChooser behaviourChooser)
        {
            return behaviourChooser.ChooseBehaviour(this);
        }

        public override void Behave(Behaviour behaviour)
        {
            behaviour.DoForConstruction(this);
        }
    }
}
