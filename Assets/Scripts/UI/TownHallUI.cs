using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TownHallUI : MonoBehaviour
    {
        [SerializeField] private Image menuUI;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private GameObject unitPrefab;
        [SerializeField] private Transform unitParent;

        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        void Update()
        {
            menuUI.transform.LookAt(_mainCamera.transform);
        }

        [UsedImplicitly]
        public void CreateNewUnit()
        {
            Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity, unitParent);
        }
    }
}
