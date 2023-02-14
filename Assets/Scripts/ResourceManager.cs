using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("Resource Number")]
    [SerializeField] private int wood = 100;
    [SerializeField] private int stone = 100;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI stoneText;
    
    public static ResourceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void UpdateResource(ResourceType resourceType, int amountToAdd)
    {
        switch (resourceType)
        {
            case ResourceType.Wood:
                wood += amountToAdd;
                woodText.text = "Wood: " + wood;
                break;
            case ResourceType.Stone:
                stone += amountToAdd;
                stoneText.text = "Stone: " + stone;
                break;
        }
    }
}
