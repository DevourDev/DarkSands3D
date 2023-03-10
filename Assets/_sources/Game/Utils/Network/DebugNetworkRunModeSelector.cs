using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Utils.Network
{
    public sealed class DebugNetworkRunModeSelector : NetworkBehaviour
    {
        [SerializeField] private InputAction _startHost;
        [SerializeField] private InputAction _startServer;
        [SerializeField] private InputAction _startClient;


        private void Awake()
        {
            _startHost.performed += StartHost;
            _startServer.performed += StartServer;
            _startClient.performed += StartClient;

            _startHost.Enable();
            _startServer.Enable();
            _startClient.Enable();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            _startHost.Dispose();
            _startServer.Dispose();
            _startClient.Dispose();
        }


        private void StartHost(InputAction.CallbackContext obj)
        {
            NetworkManager.StartHost();
        }

        private void StartServer(InputAction.CallbackContext obj)
        {
            NetworkManager.StartServer();
        }

        private void StartClient(InputAction.CallbackContext obj)
        {
            NetworkManager.StartClient();
        }
    }
}
