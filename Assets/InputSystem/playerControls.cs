//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem/playerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""playerControls"",
    ""maps"": [
        {
            ""name"": ""playerInput"",
            ""id"": ""04f1b36e-e516-4fab-bd6c-a9fea4692b93"",
            ""actions"": [
                {
                    ""name"": ""playerMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d6311de5-9d0a-4dcb-90ec-9befcd059f54"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""playerJump"",
                    ""type"": ""Button"",
                    ""id"": ""a04cf000-a729-45e2-bf69-aedc6ab43311"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""playerFire"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2c9a512e-4c16-4ecf-be62-b508d94d4068"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""playerMouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""76aaaac2-40dd-44dc-a10d-b7302a729dbb"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""playerMouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""27e7c594-2ca8-443a-b5a2-8018ffb048e1"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d5521592-2850-4dea-922f-5084eab90f30"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""playerMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f9335648-63be-4650-b0b2-3aac973a9458"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""725d9db0-09ed-47fd-9bb6-573e9dd82e74"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""22a55e2b-301e-46f4-bf3c-d86166f40321"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e392bac7-8a67-4b23-90ac-951c388c6bbc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ffd4d81e-5094-4ce6-b13c-da83fce66a16"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerJump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebaac7fa-63c3-4c82-a6ba-7667bc24417d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c63fdd13-9b0d-4746-985d-30357d053047"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerMouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""230ce2d9-2ccb-456d-a56b-67ca13830e2e"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerMouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KB+M"",
            ""bindingGroup"": ""KB+M"",
            ""devices"": []
        }
    ]
}");
        // playerInput
        m_playerInput = asset.FindActionMap("playerInput", throwIfNotFound: true);
        m_playerInput_playerMovement = m_playerInput.FindAction("playerMovement", throwIfNotFound: true);
        m_playerInput_playerJump = m_playerInput.FindAction("playerJump", throwIfNotFound: true);
        m_playerInput_playerFire = m_playerInput.FindAction("playerFire", throwIfNotFound: true);
        m_playerInput_playerMouseX = m_playerInput.FindAction("playerMouseX", throwIfNotFound: true);
        m_playerInput_playerMouseY = m_playerInput.FindAction("playerMouseY", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // playerInput
    private readonly InputActionMap m_playerInput;
    private IPlayerInputActions m_PlayerInputActionsCallbackInterface;
    private readonly InputAction m_playerInput_playerMovement;
    private readonly InputAction m_playerInput_playerJump;
    private readonly InputAction m_playerInput_playerFire;
    private readonly InputAction m_playerInput_playerMouseX;
    private readonly InputAction m_playerInput_playerMouseY;
    public struct PlayerInputActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @playerMovement => m_Wrapper.m_playerInput_playerMovement;
        public InputAction @playerJump => m_Wrapper.m_playerInput_playerJump;
        public InputAction @playerFire => m_Wrapper.m_playerInput_playerFire;
        public InputAction @playerMouseX => m_Wrapper.m_playerInput_playerMouseX;
        public InputAction @playerMouseY => m_Wrapper.m_playerInput_playerMouseY;
        public InputActionMap Get() { return m_Wrapper.m_playerInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputActions instance)
        {
            if (m_Wrapper.m_PlayerInputActionsCallbackInterface != null)
            {
                @playerMovement.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMovement;
                @playerMovement.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMovement;
                @playerMovement.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMovement;
                @playerJump.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerJump;
                @playerJump.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerJump;
                @playerJump.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerJump;
                @playerFire.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerFire;
                @playerFire.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerFire;
                @playerFire.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerFire;
                @playerMouseX.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMouseX;
                @playerMouseX.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMouseX;
                @playerMouseX.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMouseX;
                @playerMouseY.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMouseY;
                @playerMouseY.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMouseY;
                @playerMouseY.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerMouseY;
            }
            m_Wrapper.m_PlayerInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @playerMovement.started += instance.OnPlayerMovement;
                @playerMovement.performed += instance.OnPlayerMovement;
                @playerMovement.canceled += instance.OnPlayerMovement;
                @playerJump.started += instance.OnPlayerJump;
                @playerJump.performed += instance.OnPlayerJump;
                @playerJump.canceled += instance.OnPlayerJump;
                @playerFire.started += instance.OnPlayerFire;
                @playerFire.performed += instance.OnPlayerFire;
                @playerFire.canceled += instance.OnPlayerFire;
                @playerMouseX.started += instance.OnPlayerMouseX;
                @playerMouseX.performed += instance.OnPlayerMouseX;
                @playerMouseX.canceled += instance.OnPlayerMouseX;
                @playerMouseY.started += instance.OnPlayerMouseY;
                @playerMouseY.performed += instance.OnPlayerMouseY;
                @playerMouseY.canceled += instance.OnPlayerMouseY;
            }
        }
    }
    public PlayerInputActions @playerInput => new PlayerInputActions(this);
    private int m_KBMSchemeIndex = -1;
    public InputControlScheme KBMScheme
    {
        get
        {
            if (m_KBMSchemeIndex == -1) m_KBMSchemeIndex = asset.FindControlSchemeIndex("KB+M");
            return asset.controlSchemes[m_KBMSchemeIndex];
        }
    }
    public interface IPlayerInputActions
    {
        void OnPlayerMovement(InputAction.CallbackContext context);
        void OnPlayerJump(InputAction.CallbackContext context);
        void OnPlayerFire(InputAction.CallbackContext context);
        void OnPlayerMouseX(InputAction.CallbackContext context);
        void OnPlayerMouseY(InputAction.CallbackContext context);
    }
}
