using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CircleProgress : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        private Camera _camera;
        
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            Hide();
        }

        private void Update()
        {
            transform.LookAt(_camera.transform);
        }

        public void ChangeProgress(float newProgress)
        {
            fillImage.fillAmount = newProgress;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
