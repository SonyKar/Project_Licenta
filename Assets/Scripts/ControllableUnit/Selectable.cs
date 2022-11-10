using UnityEngine;

namespace ControllableUnit
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private GameObject highlight;
        public void OnSelect()
        {
            highlight.SetActive(true);
        }
    
        public void OnDeselect()
        {
            highlight.SetActive(false);
        }
    }
}
