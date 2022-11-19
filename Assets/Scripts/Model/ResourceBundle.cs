namespace Model
{
    public class ResourceBundle
    {
        public ResourceType ResourceType { get; }
        public int ResourceNumber { get; }

        public ResourceBundle(int resourceNumber, ResourceType resourceType)
        {
            ResourceNumber = resourceNumber;
            ResourceType = resourceType;
        }
    }
}