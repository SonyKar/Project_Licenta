using Behaviours;
using Model;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public abstract class Mineable : Target
    {
        [SerializeField] private bool isDepleted;
        [SerializeField] private int health = 1;
        [SerializeField] private ResourceType resourceType;

        public abstract override Behaviour BestBehaviour(BehaviourChooser behaviourChooser);
        public abstract override void Behave(Behaviour behaviour);

        public ResourceBundle TakeHit(int damage)
        {
            int initialHealth = health;
            health -= damage;
            
            if (health <= 0)
            {
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