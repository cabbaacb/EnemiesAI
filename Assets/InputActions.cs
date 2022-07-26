//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.1
//     from Assets/InputActions.inputactions
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

namespace Ziggurat
{
    public partial class @InputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""MainActionMap"",
            ""id"": ""f5683593-e969-4ee2-8670-25e97045a25b"",
            ""actions"": [
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9905e0ad-28e5-48b3-9785-38c4e5b37b6d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""a7ff2484-c970-49df-9906-02e69b88dc31"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CameraRotateToggle"",
                    ""type"": ""Button"",
                    ""id"": ""37f28395-2d25-484d-bb50-27dcafad74fe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraRotate"",
                    ""type"": ""Value"",
                    ""id"": ""4ade0c25-6c1d-49ae-92ce-fde4e453eb80"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b21bb1a1-5825-4a54-86fe-18f439f7ee26"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a4a56adc-2bfe-4020-ab3f-b4dc2036b0d4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""42d72825-1781-4c0b-aab3-b5f6222522d4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b691d48c-8397-4d10-8289-36fae5400a0a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9fa9e453-c2f4-4768-9ee0-381257c21587"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""ce2fb420-c769-4cd6-9ec9-708b590f72b9"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12f98033-f848-4e67-b8a8-e84a9a28f4e5"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotateToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""434faf10-5a5b-4403-b787-a4272e8590ea"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraRotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // MainActionMap
            m_MainActionMap = asset.FindActionMap("MainActionMap", throwIfNotFound: true);
            m_MainActionMap_CameraMovement = m_MainActionMap.FindAction("CameraMovement", throwIfNotFound: true);
            m_MainActionMap_CameraZoom = m_MainActionMap.FindAction("CameraZoom", throwIfNotFound: true);
            m_MainActionMap_CameraRotateToggle = m_MainActionMap.FindAction("CameraRotateToggle", throwIfNotFound: true);
            m_MainActionMap_CameraRotate = m_MainActionMap.FindAction("CameraRotate", throwIfNotFound: true);
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

        // MainActionMap
        private readonly InputActionMap m_MainActionMap;
        private IMainActionMapActions m_MainActionMapActionsCallbackInterface;
        private readonly InputAction m_MainActionMap_CameraMovement;
        private readonly InputAction m_MainActionMap_CameraZoom;
        private readonly InputAction m_MainActionMap_CameraRotateToggle;
        private readonly InputAction m_MainActionMap_CameraRotate;
        public struct MainActionMapActions
        {
            private @InputActions m_Wrapper;
            public MainActionMapActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @CameraMovement => m_Wrapper.m_MainActionMap_CameraMovement;
            public InputAction @CameraZoom => m_Wrapper.m_MainActionMap_CameraZoom;
            public InputAction @CameraRotateToggle => m_Wrapper.m_MainActionMap_CameraRotateToggle;
            public InputAction @CameraRotate => m_Wrapper.m_MainActionMap_CameraRotate;
            public InputActionMap Get() { return m_Wrapper.m_MainActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActionMapActions set) { return set.Get(); }
            public void SetCallbacks(IMainActionMapActions instance)
            {
                if (m_Wrapper.m_MainActionMapActionsCallbackInterface != null)
                {
                    @CameraMovement.started -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraMovement;
                    @CameraMovement.performed -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraMovement;
                    @CameraMovement.canceled -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraMovement;
                    @CameraZoom.started -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraZoom;
                    @CameraZoom.performed -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraZoom;
                    @CameraZoom.canceled -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraZoom;
                    @CameraRotateToggle.started -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraRotateToggle;
                    @CameraRotateToggle.performed -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraRotateToggle;
                    @CameraRotateToggle.canceled -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraRotateToggle;
                    @CameraRotate.started -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraRotate;
                    @CameraRotate.performed -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraRotate;
                    @CameraRotate.canceled -= m_Wrapper.m_MainActionMapActionsCallbackInterface.OnCameraRotate;
                }
                m_Wrapper.m_MainActionMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @CameraMovement.started += instance.OnCameraMovement;
                    @CameraMovement.performed += instance.OnCameraMovement;
                    @CameraMovement.canceled += instance.OnCameraMovement;
                    @CameraZoom.started += instance.OnCameraZoom;
                    @CameraZoom.performed += instance.OnCameraZoom;
                    @CameraZoom.canceled += instance.OnCameraZoom;
                    @CameraRotateToggle.started += instance.OnCameraRotateToggle;
                    @CameraRotateToggle.performed += instance.OnCameraRotateToggle;
                    @CameraRotateToggle.canceled += instance.OnCameraRotateToggle;
                    @CameraRotate.started += instance.OnCameraRotate;
                    @CameraRotate.performed += instance.OnCameraRotate;
                    @CameraRotate.canceled += instance.OnCameraRotate;
                }
            }
        }
        public MainActionMapActions @MainActionMap => new MainActionMapActions(this);
        public interface IMainActionMapActions
        {
            void OnCameraMovement(InputAction.CallbackContext context);
            void OnCameraZoom(InputAction.CallbackContext context);
            void OnCameraRotateToggle(InputAction.CallbackContext context);
            void OnCameraRotate(InputAction.CallbackContext context);
        }
    }
}
