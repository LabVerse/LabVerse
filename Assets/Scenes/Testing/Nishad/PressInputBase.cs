using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// For tutorial video, see my YouTube channel: <seealso href="https://www.youtube.com/@xiennastudio">YouTube channel</seealso>
/// 
/// Create a new input system with Pointer press as the input.
/// </summary>
[HelpURL("https://youtu.be/HkNVp04GOEI")]
public abstract class PressInputBase : MonoBehaviour
{
    protected InputAction m_PressAction;

    protected virtual void Awake()
    {
        // Create a new input within the script.
        m_PressAction = new InputAction("touch", binding: "<Pointer>/press");

        // If touch is being started, call the OnPressBegan function.
        m_PressAction.started += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPressBegan(device.position.ReadValue());
            }
        };

        // If touch is being performed, call the OnPress function.
        m_PressAction.performed += ctx =>
        {
            if (ctx.control.device is Pointer device)
            {
                OnPress(device.position.ReadValue());
            }
        };

        // If the existing touch is stopped or canceled, call the OnPressCancel function.
        m_PressAction.canceled += _ => OnPressCancel();
    }

    protected virtual void OnEnable()
    {
        m_PressAction.Enable();
    }

    protected virtual void OnDisable()
    {
        m_PressAction.Disable();
    }

    protected virtual void OnDestroy()
    {
        m_PressAction.Dispose();
    }

    protected virtual void OnPress(Vector3 position) { }

    protected virtual void OnPressBegan(Vector3 position) { }

    protected virtual void OnPressCancel() { }
}