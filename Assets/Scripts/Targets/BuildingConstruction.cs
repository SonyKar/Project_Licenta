using Behaviours;
using Building;
using UI;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public class BuildingConstruction : Target
    {
        [SerializeField] private BuildingTypeSo constructionInfo;

        [SerializeField] private CircleProgress constructionProgressUI;
        [SerializeField] private int constructionProgress;
        [SerializeField] private ParticleSystem constructionFog;

        private void Update()
        {
            constructionProgressUI.Show();
            constructionProgressUI.ChangeProgress(constructionProgress * 1.0f / constructionInfo.constructionHealth);
            if (IsConstructed())
            {
                Transform constructionTransform = transform;
                Instantiate(constructionInfo.prefab, constructionTransform.position, Quaternion.identity, constructionTransform.parent);
                Destroy(gameObject);
            }
        }

        public void BuildTick(int healthToAdd)
        {
            if (constructionFog is not null)
            {
                constructionFog.Play();
            }
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
