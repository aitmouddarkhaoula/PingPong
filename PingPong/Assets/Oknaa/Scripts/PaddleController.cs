using System.Collections;
using System.Collections.Generic;
using GameSystems;
using Unity.Netcode;
using UnityEngine;

public class PaddleController : NetworkBehaviour {
    public bool ActivateMouse;
    private Vector3 touchPosition;
    private float directionX;
    public float moveSpeed = 10f;
    public float levelWidth = 2.5f;
    private Camera cam;


    private void Start() {
        cam = Camera.main;
    }

    public override void OnNetworkSpawn() {
        if (!IsOwner) enabled = false;
        // Rotate stuff so that the player is always at the bottom of the screen, and the other player is at the top
        // if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkClient)) {
        //     cam.transform.rotation = 
        //         Quaternion.Euler(0, 0, networkClient.PlayerObject.GetComponent<NetworkObject>().OwnerClientId == NetworkManager.Singleton.LocalClientId ? 
        //             0 : 
        //             180);
        // }
        
        bool isPlayer1 = NetworkManager.Singleton.ConnectedClients.Count == 1;
        transform.position = new Vector3(0, isPlayer1 ? -6f : 6f, 0);
        cam.transform.rotation = Quaternion.Euler(0, 0, isPlayer1 ? 0 : 180);
        
    }
 

    private void Update() {
        if(GameStateSystem._currentState != GameState.Playing) return;
        Vector3 inputPosition;
        if (ActivateMouse) {
            if (Input.GetMouseButton(0)) inputPosition = Input.mousePosition;
            else return;
        }
        else {
            if (Input.touchCount > 0) inputPosition = Input.GetTouch(0).position;
            else return;
        }

        Ray ray = cam.ScreenPointToRay(inputPosition);
        Vector3 newPosition;
        if (transform.position.x <= -levelWidth && ray.direction.x < 0.0f) newPosition = new Vector3(-levelWidth, transform.position.y, 0);
        else if (transform.position.x >= levelWidth && ray.direction.x > 0.0f) newPosition = new Vector3(levelWidth, transform.position.y, 0);
        else newPosition = new Vector3(ray.GetPoint(moveSpeed).x, transform.position.y, 0);

        transform.position = newPosition;
        if(transform.position.x>3.7f) transform.position = new Vector3(3.7f, transform.position.y, 0);
        if(transform.position.x<-3.7f) transform.position = new Vector3(-3.7f, transform.position.y, 0);
    }

    public void Reset()
    {
        
    }
}