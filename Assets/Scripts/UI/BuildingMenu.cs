using Building;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace UI
{
    public class BuildingMenu : MonoBehaviour
    {
        [SerializeField] private GameObject buildingMenu;
        [SerializeField] private TextMeshProUGUI buildingButtonText;

        public static BuildingMenu Instance { get; private set; }
        void Awake()
        {
            if(Instance is null)
                Instance = this;
            else
                Destroy(this);
        }

        [UsedImplicitly]
        public void ToggleBuildingMenu()
        {
            if (Gameplay.Instance.GameMode == GameMode.Build)
            {
                Gameplay.Instance.ToggleBuildMode();
                buildingButtonText.SetText("Build");
            }
            else buildingMenu.SetActive(!buildingMenu.activeSelf);
        }

        [UsedImplicitly]
        public void Build(BuildingTypeSo buildingTypeSo)
        {
            buildingButtonText.SetText("Cancel");
            BuildingManager.Instance.ChangeActiveBuildingType(buildingTypeSo);
            
            ToggleBuildingMenu();
            
            Gameplay.Instance.ToggleBuildMode();
        }
    }
}
