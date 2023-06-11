using Behaviours;
using Model;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public abstract class Mineable : Target
    {
        [Header("General Info")]
        [SerializeField] private bool isDepleted;
        [SerializeField] private int health = 1;
        [SerializeField] private ResourceType resourceType;
        [Header("Animation Info")]
        [SerializeField] private Animator animator;
        [SerializeField] private string triggerName;
        [Header("Visuals")]
        [SerializeField] private Collider colliderToRemove;
        [SerializeField] private GameObject depletedModel;
        [SerializeField] private GameObject initialModel;

        public abstract override Behaviour BestBehaviour(BehaviourChooser behaviourChooser);
        public abstract override void Behave(Behaviour behaviour);

        public ResourceBundle TakeHit(int damage)
        {
            animator.SetTrigger(triggerName);
            int initialHealth = health;
            health -= damage;
            
            if (health <= 0)
            {
                colliderToRemove.enabled = false;
                initialModel.SetActive(false);
                depletedModel.SetActive(true);
                health = 0;
                isDepleted = true;
            }
            
            return new ResourceBundle(initialHealth - health, resourceType);
        }

        public ResourceType GetResourceType()
        {
            return resourceType;
        }

        public bool IsDepleted()
        {
            return isDepleted;
        }
    }
}