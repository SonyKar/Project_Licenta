using UnityEngine;

public class Clickable : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("My name is: " + gameObject.name);
    }
}
