using Targets;
using UnityEngine;
using Tree = Targets.Tree;

public class Locator : MonoBehaviour
{
    [SerializeField] private GameObject treesParent;
    [SerializeField] private GameObject buildingParent;
    
    public static Locator Instance { get; private set; }
    private static GameObject _trees;
    private static GameObject _buildings;

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);

        _trees = treesParent;
        _buildings = buildingParent;
    }

    public static Target FindNearestTree(Vector3 currentPosition)
    {
        Tree[] treePositions = _trees.GetComponentsInChildren<Tree>();
        
        Target nearestTree = null;
        float minDist = Mathf.Infinity;
        foreach (Tree t in treePositions)
        {
            if (t.IsDepleted()) continue;
            float dist = Vector3.Distance(t.transform.position, currentPosition);
            if (!(dist < minDist)) continue;
            nearestTree = t;
            minDist = dist;
        }

        return nearestTree;
    }
    
    public static Target GetClosestSawmill(Vector3 currentPosition)
    {
        Sawmill[] sawmillsPositions = _buildings.GetComponentsInChildren<Sawmill>();
        
        Sawmill nearestSawmill = null;
        float minDist = Mathf.Infinity;
        foreach (Sawmill s in sawmillsPositions)
        {
            float dist = Vector3.Distance(s.transform.position, currentPosition);
            if (!(dist < minDist)) continue;
            nearestSawmill = s;
            minDist = dist;
        }

        return nearestSawmill;
    }
}