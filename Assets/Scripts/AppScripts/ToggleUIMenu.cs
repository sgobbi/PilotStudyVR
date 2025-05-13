using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleUIMenu : MonoBehaviour
{
    public GameObject uiMenu; // Reference to your UI menu
    private XRInputSystem inputActions; // Reference to the generated input actions

    private void Awake()
    {
        inputActions = new XRInputSystem();
        uiMenu.SetActive(false);
    }

    private void OnEnable()
    {
        inputActions.XRController.ToggleHandMenu.performed += ToggleMenu;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.XRController.ToggleHandMenu.performed -= ToggleMenu;
        inputActions.Disable();
    }

    private void ToggleMenu(InputAction.CallbackContext context)
    {
        uiMenu.SetActive(!uiMenu.activeSelf);
    }
}
