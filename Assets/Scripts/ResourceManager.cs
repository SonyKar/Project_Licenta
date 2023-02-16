using System;
using System.Collections.Generic;
using System.Linq;
using Model;
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
                break;
            case ResourceType.Stone:
                stone += amountToAdd;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }
        UpdateResourcesUI();
    }

    private bool IsEnoughResources(IEnumerable<ResourceBundle> resources)
    {
        foreach (ResourceBundle resource in resources)
        {
            switch (resource.ResourceType)
            {
                case ResourceType.Wood:
                    if (wood < resource.ResourceNumber) return false;
                    break;
                case ResourceType.Stone:
                    if (stone < resource.ResourceNumber) return false;
                    break;
                default:
                    return false;
            }
        }
        return true;
    }
    public bool SpendResources(IEnumerable<ResourceBundle> requiredResources)
    {
        requiredResources = requiredResources.ToList();
        if (!IsEnoughResources(requiredResources)) return false;
        foreach (ResourceBundle resource in requiredResources)
        {
            switch (resource.ResourceType)
            {
                case ResourceType.Wood:
                    wood -= resource.ResourceNumber;
                    break;
                case ResourceType.Stone:
                    stone -= resource.ResourceNumber;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        UpdateResourcesUI();
        return true;
    }
    
    private void UpdateResourcesUI()
    {
        woodText.text = "Wood: " + wood;
        stoneText.text = "Stone: " + stone;
    }
}
