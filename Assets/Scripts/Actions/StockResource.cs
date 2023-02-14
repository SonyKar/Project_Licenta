using ControllableUnit;

namespace Actions
{
    public class StockResource : Action
    {
        private readonly Inventory _inventory;

        public StockResource(ActionDoer actionDoer, Inventory inventory)
        : base(actionDoer)
        {
            _inventory = inventory;
        }
        
        public override void Do()
        {
            ResourceManager.Instance.UpdateResource(_inventory.GetCurrentResourceType(), _inventory.GetResourceNumberHeld());
            _inventory.ClearInventory();
            ActiveObject.NextAction();
        }
    }
}