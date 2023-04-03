using UnityEngine;

namespace ControllableUnit
{
    public class UnitAnimatorHandler : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private Animator animator;
        
        [Header("Chop Wood")]
        [SerializeField] private GameObject axe;
        
        [Header("Carry")]
        [SerializeField] private Inventory inventory;
        [SerializeField] private GameObject woodStack;

        [Header("Building")]
        [SerializeField] private GameObject hammer;
        
        private readonly static int IsChopping = Animator.StringToHash("isChopping");
        private readonly static int IsWalking = Animator.StringToHash("isWalking");
        private readonly static int IsCarrying = Animator.StringToHash("isCarrying");
        private readonly static int IsBuilding = Animator.StringToHash("isBuilding");

        public void SetChoppingAnimation()
        {
            EmptyHands();
            axe.SetActive(true);
            animator.SetBool(IsChopping, true);
        }
        
        private void SetCarryingAnimation()
        {
            if (inventory.GetResourceNumberHeld() <= 0)
            {
                animator.SetBool(IsCarrying, false);
                return;
            }
            
            switch (inventory.GetCurrentResourceType())
            {
                case ResourceType.Wood:
                    woodStack.SetActive(true);
                    break;
            }
            animator.SetBool(IsCarrying, true);
        }
        
        public void SetBuildingAnimation()
        {
            EmptyHands();
            hammer.SetActive(true);
            animator.SetBool(IsBuilding, true);
        }

        public void SetWalkingAnimation()
        {
            animator.SetBool(IsWalking, true);
        }

        private void EmptyHands()
        {
            axe.SetActive(false);
            woodStack.SetActive(false);
            hammer.SetActive(false);
        }
        
        public void ClearAnimation()
        {
            animator.SetBool(IsChopping, false);
            animator.SetBool(IsBuilding, false);
            animator.SetBool(IsWalking, false);
            
            EmptyHands();
            SetCarryingAnimation();
        }
    }
}
