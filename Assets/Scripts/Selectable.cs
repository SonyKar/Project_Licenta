using UnityEngine;

public class Selectable : MonoBehaviour
{
    public void OnSelect()
    {
        Debug.Log("My name is: " + gameObject.name);
    }
}
