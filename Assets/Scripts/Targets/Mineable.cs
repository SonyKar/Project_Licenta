using Behaviours;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public abstract class Mineable : MonoBehaviour, ITarget
    {
        [SerializeField] private int health = 1;
        [SerializeField] private ResourceType resourceType;

        public abstract Behaviour BestBehaviour(BehaviourChooser behaviourChooser);
        public abstract void Behave(Behaviour behaviour);

        public int TakeHit(int damage)
        {
            int initialHealth = health;
            health -= damage;
            
            if (health <= 0)
            {
                health = 0;
                Destroy(gameObject);
            }

            return initialHealth - health;
        }
    }
}