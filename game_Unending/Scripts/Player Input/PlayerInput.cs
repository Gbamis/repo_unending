//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Games/Repository/game_Unending/Scripts/Player Input/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Jet"",
            ""id"": ""3f3680d7-b469-4100-a69b-1aff369887bf"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Value"",
                    ""id"": ""0451bfc7-3b1e-47c2-baae-4b6b875bb5f0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shield"",
                    ""type"": ""Value"",
                    ""id"": ""8feaf254-9a17-4e45-b648-ed9199818dd3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Nitro"",
                    ""type"": ""Value"",
                    ""id"": ""5f5d5ea8-3f28-49f7-993a-92c591ee33b5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attractor"",
                    ""type"": ""Value"",
                    ""id"": ""756f99cc-168f-442b-8296-3ba0b5a776e4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c436a9b1-996f-4170-badb-266d1983b431"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bf276b55-08e9-4d48-8283-5acf3a232534"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3fe4cab-6d3d-4870-8fa9-448cee4503ce"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Nitro"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""178c2e2f-b7dc-4062-aa27-78b62d5ff134"",
                    ""path"": ""<Keyboard>/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attractor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Jet
        m_Jet = asset.FindActionMap("Jet", throwIfNotFound: true);
        m_Jet_Shoot = m_Jet.FindAction("Shoot", throwIfNotFound: true);
        m_Jet_Shield = m_Jet.FindAction("Shield", throwIfNotFound: true);
        m_Jet_Nitro = m_Jet.FindAction("Nitro", throwIfNotFound: true);
        m_Jet_Attractor = m_Jet.FindAction("Attractor", throwIfNotFound: true);
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

    // Jet
    private readonly InputActionMap m_Jet;
    private IJetActions m_JetActionsCallbackInterface;
    private readonly InputAction m_Jet_Shoot;
    private readonly InputAction m_Jet_Shield;
    private readonly InputAction m_Jet_Nitro;
    private readonly InputAction m_Jet_Attractor;
    public struct JetActions
    {
        private @PlayerInput m_Wrapper;
        public JetActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_Jet_Shoot;
        public InputAction @Shield => m_Wrapper.m_Jet_Shield;
        public InputAction @Nitro => m_Wrapper.m_Jet_Nitro;
        public InputAction @Attractor => m_Wrapper.m_Jet_Attractor;
        public InputActionMap Get() { return m_Wrapper.m_Jet; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JetActions set) { return set.Get(); }
        public void SetCallbacks(IJetActions instance)
        {
            if (m_Wrapper.m_JetActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_JetActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_JetActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_JetActionsCallbackInterface.OnShoot;
                @Shield.started -= m_Wrapper.m_JetActionsCallbackInterface.OnShield;
                @Shield.performed -= m_Wrapper.m_JetActionsCallbackInterface.OnShield;
                @Shield.canceled -= m_Wrapper.m_JetActionsCallbackInterface.OnShield;
                @Nitro.started -= m_Wrapper.m_JetActionsCallbackInterface.OnNitro;
                @Nitro.performed -= m_Wrapper.m_JetActionsCallbackInterface.OnNitro;
                @Nitro.canceled -= m_Wrapper.m_JetActionsCallbackInterface.OnNitro;
                @Attractor.started -= m_Wrapper.m_JetActionsCallbackInterface.OnAttractor;
                @Attractor.performed -= m_Wrapper.m_JetActionsCallbackInterface.OnAttractor;
                @Attractor.canceled -= m_Wrapper.m_JetActionsCallbackInterface.OnAttractor;
            }
            m_Wrapper.m_JetActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Shield.started += instance.OnShield;
                @Shield.performed += instance.OnShield;
                @Shield.canceled += instance.OnShield;
                @Nitro.started += instance.OnNitro;
                @Nitro.performed += instance.OnNitro;
                @Nitro.canceled += instance.OnNitro;
                @Attractor.started += instance.OnAttractor;
                @Attractor.performed += instance.OnAttractor;
                @Attractor.canceled += instance.OnAttractor;
            }
        }
    }
    public JetActions @Jet => new JetActions(this);
    public interface IJetActions
    {
        void OnShoot(InputAction.CallbackContext context);
        void OnShield(InputAction.CallbackContext context);
        void OnNitro(InputAction.CallbackContext context);
        void OnAttractor(InputAction.CallbackContext context);
    }
}
