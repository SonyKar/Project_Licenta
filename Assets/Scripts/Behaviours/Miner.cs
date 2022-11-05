using Tree = Targets.Target;

namespace Behaviours
{
    public class Miner : Behaviour
    {
        private Walker _walker;

        private void Awake()
        {
            _walker = GetComponent<Walker>();
        }

        public override void DoForTree(Tree tree)
        {
            if (_walker != null)
            {
                _walker.DoForTree(tree);
            }
        }
    }
}
