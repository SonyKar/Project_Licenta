using Building;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuildingMenu : MonoBehaviour
    {
        [SerializeField] private GameObject buildingMenu;
        [SerializeField] private Image buildButtonImage;
        [SerializeField] private Sprite buildSprite;
        [SerializeField] private Sprite cancelSprite;

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
                buildButtonImage.sprite = buildSprite;
            }
            else buildingMenu.SetActive(!buildingMenu.activeSelf);
        }

        [UsedImplicitly]
        public void Build(BuildingTypeSo buildingTypeSo)
        {
            buildButtonImage.sprite = cancelSprite;
            BuildingManager.Instance.ChangeActiveBuildingType(buildingTypeSo);
            
            ToggleBuildingMenu();
            
            Gameplay.Instance.ToggleBuildMode();
        }
    }
}
