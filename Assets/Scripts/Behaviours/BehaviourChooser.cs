using Targets;
using UnityEngine;
using Tree = Targets.Tree;

namespace Behaviours
{
    public class BehaviourChooser : MonoBehaviour
    {
        public Behaviour ChooseBehaviour(Tree tree)
        {
            Behaviour behaviour = gameObject.GetComponent<Miner>() ?? (Behaviour)gameObject.GetComponent<Walker>();
            return behaviour;
        }
        
        public Behaviour ChooseBehaviour(Sawmill sawmill)
        {
            Behaviour behaviour = gameObject.GetComponent<Carrier>() ?? (Behaviour)gameObject.GetComponent<Walker>();
            return behaviour;
        }
        
        public Behaviour ChooseBehaviour(BuildingConstruction construction)
        {
            Behaviour behaviour = gameObject.GetComponent<Builder>() ?? (Behaviour)gameObject.GetComponent<Walker>();
            return behaviour;
        }
    }
}
