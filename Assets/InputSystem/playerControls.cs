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
                    ""type"": ""Button"",
                    ""id"": ""2c9a512e-4c16-4ecf-be62-b508d94d4068"",
                    ""expectedControlType"": ""Button"",
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
                },
                {
                    ""name"": ""playerInteract"",
                    ""type"": ""Button"",
                    ""id"": ""fca5abd5-d478-48c5-ae3b-6def299259d8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""playerInventory"",
                    ""type"": ""Button"",
                    ""id"": ""268ba2d3-762d-4ba4-8475-9c38b9c11da6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""playerDash"",
                    ""type"": ""Button"",
                    ""id"": ""0ee68391-0e55-4c39-92a0-2c43b9ff38cd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""changeActiveItem"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cc7346ea-343e-4e8c-816c-b790cb540355"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""playerChangeItemMode"",
                    ""type"": ""Button"",
                    ""id"": ""9bea4666-403e-419c-b8f9-17afe6658311"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""playerActiveBuff"",
                    ""type"": ""Button"",
                    ""id"": ""c68c9021-928f-4973-a787-4260ca5291ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
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
                    ""path"": ""<Mouse>/leftButton"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""4948b960-f1e5-4201-9638-f055a75e43a1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a792d22-7e2b-4b3e-9936-b20522e3546c"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a7d40db0-7a7c-40c1-ba86-470144991615"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""882557e9-00d8-4fa2-a7ea-4fb2ac7a16f0"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""changeActiveItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdbefb90-d1c8-4336-81e5-75e9f74c5d44"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerChangeItemMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19b1c9f2-4ed7-4b9e-a741-1746651b0b95"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerActiveBuff"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5739132-2658-4aad-94cf-23daae926263"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KB+M"",
                    ""action"": ""playerActiveBuff"",
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
        m_playerInput_playerInteract = m_playerInput.FindAction("playerInteract", throwIfNotFound: true);
        m_playerInput_playerInventory = m_playerInput.FindAction("playerInventory", throwIfNotFound: true);
        m_playerInput_playerDash = m_playerInput.FindAction("playerDash", throwIfNotFound: true);
        m_playerInput_changeActiveItem = m_playerInput.FindAction("changeActiveItem", throwIfNotFound: true);
        m_playerInput_playerChangeItemMode = m_playerInput.FindAction("playerChangeItemMode", throwIfNotFound: true);
        m_playerInput_playerActiveBuff = m_playerInput.FindAction("playerActiveBuff", throwIfNotFound: true);
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
    private readonly InputAction m_playerInput_playerInteract;
    private readonly InputAction m_playerInput_playerInventory;
    private readonly InputAction m_playerInput_playerDash;
    private readonly InputAction m_playerInput_changeActiveItem;
    private readonly InputAction m_playerInput_playerChangeItemMode;
    private readonly InputAction m_playerInput_playerActiveBuff;
    public struct PlayerInputActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerInputActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @playerMovement => m_Wrapper.m_playerInput_playerMovement;
        public InputAction @playerJump => m_Wrapper.m_playerInput_playerJump;
        public InputAction @playerFire => m_Wrapper.m_playerInput_playerFire;
        public InputAction @playerMouseX => m_Wrapper.m_playerInput_playerMouseX;
        public InputAction @playerMouseY => m_Wrapper.m_playerInput_playerMouseY;
        public InputAction @playerInteract => m_Wrapper.m_playerInput_playerInteract;
        public InputAction @playerInventory => m_Wrapper.m_playerInput_playerInventory;
        public InputAction @playerDash => m_Wrapper.m_playerInput_playerDash;
        public InputAction @changeActiveItem => m_Wrapper.m_playerInput_changeActiveItem;
        public InputAction @playerChangeItemMode => m_Wrapper.m_playerInput_playerChangeItemMode;
        public InputAction @playerActiveBuff => m_Wrapper.m_playerInput_playerActiveBuff;
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
                @playerInteract.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInteract;
                @playerInteract.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInteract;
                @playerInteract.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInteract;
                @playerInventory.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInventory;
                @playerInventory.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInventory;
                @playerInventory.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerInventory;
                @playerDash.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerDash;
                @playerDash.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerDash;
                @playerDash.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerDash;
                @changeActiveItem.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnChangeActiveItem;
                @changeActiveItem.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnChangeActiveItem;
                @changeActiveItem.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnChangeActiveItem;
                @playerChangeItemMode.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerChangeItemMode;
                @playerChangeItemMode.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerChangeItemMode;
                @playerChangeItemMode.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerChangeItemMode;
                @playerActiveBuff.started -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerActiveBuff;
                @playerActiveBuff.performed -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerActiveBuff;
                @playerActiveBuff.canceled -= m_Wrapper.m_PlayerInputActionsCallbackInterface.OnPlayerActiveBuff;
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
                @playerInteract.started += instance.OnPlayerInteract;
                @playerInteract.performed += instance.OnPlayerInteract;
                @playerInteract.canceled += instance.OnPlayerInteract;
                @playerInventory.started += instance.OnPlayerInventory;
                @playerInventory.performed += instance.OnPlayerInventory;
                @playerInventory.canceled += instance.OnPlayerInventory;
                @playerDash.started += instance.OnPlayerDash;
                @playerDash.performed += instance.OnPlayerDash;
                @playerDash.canceled += instance.OnPlayerDash;
                @changeActiveItem.started += instance.OnChangeActiveItem;
                @changeActiveItem.performed += instance.OnChangeActiveItem;
                @changeActiveItem.canceled += instance.OnChangeActiveItem;
                @playerChangeItemMode.started += instance.OnPlayerChangeItemMode;
                @playerChangeItemMode.performed += instance.OnPlayerChangeItemMode;
                @playerChangeItemMode.canceled += instance.OnPlayerChangeItemMode;
                @playerActiveBuff.started += instance.OnPlayerActiveBuff;
                @playerActiveBuff.performed += instance.OnPlayerActiveBuff;
                @playerActiveBuff.canceled += instance.OnPlayerActiveBuff;
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
        void OnPlayerInteract(InputAction.CallbackContext context);
        void OnPlayerInventory(InputAction.CallbackContext context);
        void OnPlayerDash(InputAction.CallbackContext context);
        void OnChangeActiveItem(InputAction.CallbackContext context);
        void OnPlayerChangeItemMode(InputAction.CallbackContext context);
        void OnPlayerActiveBuff(InputAction.CallbackContext context);
    }
}
