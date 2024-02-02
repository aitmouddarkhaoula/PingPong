using System;
using GameSystems;
using Oknaa.Scripts;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaddleController : NetworkBehaviour
{
    public bool ActivateMouse;
    public float moveSpeed = 10f;
    public float levelWidth = 2.5f;
    private Camera cam;
    private ulong[] clientsIDs;
    private float directionX;
    private Vector3 touchPosition;

    

    private void Start()
    {
        Ball.Instance.OnScoreChanged += (value1,value2)=> AddScoreServerRpc(value1, value2);
    }

    
    private void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject()) return;
        if (GameStateSystem._currentState != GameState.Playing) return;
        Vector3 inputPosition;
        if (ActivateMouse)
        {
            if (Input.GetMouseButton(0)) inputPosition = Input.mousePosition;
            else return;
        }
        else
        {
            if (Input.touchCount > 0) inputPosition = Input.GetTouch(0).position;
            else return;
        }
        // if (IsClient) inputPosition.x = -inputPosition.x;
        if(Camera.main.isActiveAndEnabled) cam = Camera.main;
        var ray = cam.ScreenPointToRay(inputPosition);
        Vector3 newPosition;
        if (transform.position.x <= -levelWidth && ray.direction.x < 0.0f)
            newPosition = new Vector3(-levelWidth, transform.position.y, 0);
        else if (transform.position.x >= levelWidth && ray.direction.x > 0.0f)
            newPosition = new Vector3(levelWidth, transform.position.y, 0);
        else newPosition = new Vector3(ray.GetPoint(moveSpeed).x, transform.position.y, 0);

        transform.position = newPosition;
        if (transform.position.x > 3.7f) transform.position = new Vector3(3.7f, transform.position.y, 0);
        if (transform.position.x < -3.7f) transform.position = new Vector3(-3.7f, transform.position.y, 0);
    }


    public override void OnNetworkSpawn()
    {
        if (!IsOwner) enabled = false;
        // Rotate stuff so that the player is always at the bottom of the screen, and the other player is at the top
        // if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkClient)) {
        //     cam.transform.rotation = 
        //         Quaternion.Euler(0, 0, networkClient.PlayerObject.GetComponent<NetworkObject>().OwnerClientId == NetworkManager.Singleton.LocalClientId ? 
        //             0 : 
        //             180);
        // }
        var isPlayer1 =NetworkManager.Singleton.ConnectedClients.Count == 1;
        transform.position = new Vector3(0, isPlayer1 ? -6f : 6f, 0); 

    }

        [ServerRpc(RequireOwnership = false)]
    public void AddScoreServerRpc( int playerScore1, int playerScore2, ServerRpcParams serverRpcParams = default)
    {
        if (!IsServer) return;
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            var data = new SyncedDataMessage
            {
                // playerName = playerName,
                playerScore1 = playerScore1,
                playerScore2 = playerScore2,
                
            };
            var clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = clientsIDs
                }
            };
            AddScoreClientRpc(data, clientRpcParams);
        }
    }

    [ClientRpc]
    public void AddScoreClientRpc(SyncedDataMessage data, ClientRpcParams clientRpcParams = default)
    {
        // if (IsOwner) return;
        // Update UI with received data
       Ball.Instance.UpdateUI(data.playerScore1, data.playerScore2);
    }
    
    
    [ServerRpc(RequireOwnership = false)]
    public void ShowEmojiVFXServerRpc(int emojiID, ServerRpcParams serverRpcParams = default)
    {
        if (!IsServer) return;
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            
            var clientRpcParams = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = clientsIDs
                }
            };
            ShowEmojiVFXClientRpc(emojiID, clientRpcParams);
        }
    }

    [ClientRpc]
    private void ShowEmojiVFXClientRpc(int emojiID, ClientRpcParams clientRpcParams = default)
    {
        bool isServer = GameManager.Instance.serverManager.IsServer;
        if(isServer) GameManager.Instance._emojisPanelServer.LaunchVFX(emojiID, IsOwner);
        else GameManager.Instance._emojisPanelClient.LaunchVFX(emojiID, IsOwner);
    }


    private void Reset()
    {
        Ball.Instance.OnScoreChanged -= (value1,value2)=> AddScoreServerRpc(value1, value2);

    }
    
    
    
}
[Serializable]
public class SyncedDataMessage : INetworkSerializable
{
    public int playerScore1;
    public int playerScore2;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref playerScore1);
        serializer.SerializeValue(ref playerScore2);
    }
}