using UnityEngine;
using UnityEngine.UI;

namespace ControllableUnit
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private GameObject highlight;
        [SerializeField] private Image ui;
        
        public void OnSelect()
        {
            highlight.SetActive(true);
            if (ui != null) ui.gameObject.SetActive(true);
        }
    
        public void OnDeselect()
        {
            highlight.SetActive(false);
            if (ui != null) ui.gameObject.SetActive(false);
        }
    }
}
