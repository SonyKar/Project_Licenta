using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MessageHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI messageText;
    
        private Coroutine _messageCoroutine;
        
        public static MessageHandler Instance { get; private set; }
        void Awake()
        {
            if(Instance is null)
                Instance = this;
            else
                Destroy(this);
        }

        public void ShowBuildingPlacementError()
        {
            ShowError("Cannot build here");
        }

        public void ShowResourceError()
        {
            ShowError("You don't have enough resources");
        }
        
        private void ShowError(string errorMessage)
        {
            if (_messageCoroutine != null) StopCoroutine(_messageCoroutine);
            _messageCoroutine = StartCoroutine(ShowMessageCoroutine(errorMessage));
        }

        private IEnumerator ShowMessageCoroutine(string message)
        {
            messageText.text = message;
            yield return new WaitForSeconds(2);
            messageText.text = "";
        }
    }
}
