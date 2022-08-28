using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimationController : MonoBehaviour
{
  [SerializeField] InputActionReference controllerActionGrip;
  [SerializeField] InputActionReference controllerActionTrigger;

  private Animator _handAnimator;

  private void Awake()
  {
    controllerActionGrip.action.performed += GripPress;
    controllerActionTrigger.action.performed += TriggerPress;

    _handAnimator = GetComponent<Animator>();
  }

  private void TriggerPress(InputAction.CallbackContext obj)
  {
    _handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    // Debug.Log("TriggerPress");
  }

  private void GripPress(InputAction.CallbackContext obj)
  {
    _handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    // Debug.Log("Grip");
  }
}