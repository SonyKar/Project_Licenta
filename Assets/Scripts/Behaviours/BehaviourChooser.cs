using UnityEngine;
using Tree = Targets.Tree;

namespace Behaviours
{
    public class BehaviourChooser : MonoBehaviour
    {
        public Behaviour ChooseBehaviour(Tree tree)
        {
            Behaviour behaviour = gameObject.GetComponent<Miner>();
            if (behaviour == null)
            {
                behaviour = gameObject.GetComponent<Walker>();
            }
            return behaviour;
        }
    }
}
