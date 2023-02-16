using System.Collections;
using ControllableUnit;
using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;
    
    public GameMode GameMode { get; set; } = GameMode.Free;
    public GameObject selectedObject;
    
    private Coroutine _errorMessageCoroutine;

    public static Gameplay Instance { get; private set; }
    void Awake()
    {
        if(Instance is null)
            Instance = this;
        else
            Destroy(this);
    }

    public void SelectObject(GameObject clickedObject)
    {
        Selectable clickedObjectSelectable = clickedObject.GetComponent<Selectable>();

        if (clickedObjectSelectable is null) return;
        if (selectedObject != null)
        {
            selectedObject.GetComponent<Selectable>().OnDeselect();
        }
        selectedObject = clickedObject;
        clickedObjectSelectable.OnSelect();
    }

    public void ShowError(string errorMessage)
    {
        if (_errorMessageCoroutine != null) StopCoroutine(_errorMessageCoroutine);
        _errorMessageCoroutine = StartCoroutine(ShowErrorCoroutine(errorMessage));
    }

    private IEnumerator ShowErrorCoroutine(string errorMessage)
    {
        errorText.text = errorMessage;
        yield return new WaitForSeconds(2);
        errorText.text = "";
    }
}
