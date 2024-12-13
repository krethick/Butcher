using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount = 5;
    
    private InputAction aimAction;
    private CinemachineVirtualCamera virtualCamera;
    private int originalPriority;  // Store the original priority
    private bool isAiming = false;  // Track if currently aiming

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
        originalPriority = virtualCamera.Priority;  // Save the initial priority
    }

    private void OnEnable() {
        aimAction.performed += _ => ToggleAim();        
        aimAction.canceled += _ => CancelAim();        
    }

    private void OnDisable() {
        aimAction.performed -= _ => ToggleAim();
        aimAction.canceled -= _ => CancelAim();        
    }

    private void ToggleAim(){
        if (!isAiming)
        {
            virtualCamera.Priority = originalPriority + priorityBoostAmount;  // Boost the priority
            Debug.Log("Move Forward");
            isAiming = true;  // Set aiming state to true
        }
    }

    private void CancelAim(){
        if (isAiming)
        {
            virtualCamera.Priority = originalPriority - priorityBoostAmount;  // Reset to the original priority
            Debug.Log("Move Backward");
            isAiming = false;  // Set aiming state to false
        }
    }
}
