using ControllableUnit;

namespace Actions
{
    public class StockResource : Action
    {
        private readonly Inventory _inventory;
        private readonly bool _clearActionQueueOnStop;

        public StockResource(ActionDoer actionDoer, Inventory inventory, bool clearActionQueueOnStop = false)
        : base(actionDoer)
        {
            _inventory = inventory;
            _clearActionQueueOnStop = clearActionQueueOnStop;
        }
        
        public override void Do()
        {
            ResourceManager.Instance.UpdateResource(_inventory.GetCurrentResourceType(), _inventory.GetResourceNumberHeld());
            _inventory.ClearInventory();
            if (_clearActionQueueOnStop) ActiveObject.ClearActionQueue();
            else ActiveObject.NextAction();
        }
    }
}