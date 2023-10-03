using System.Collections;
using System.Collections.Generic;
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
        if (!IsOwner) Destroy(this);
        // Rotate stuff so that the player is always at the bottom of the screen, and the other player is at the top
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId, out var networkClient)) {
            cam.transform.rotation = 
                Quaternion.Euler(0, 0, networkClient.PlayerObject.GetComponent<NetworkObject>().OwnerClientId == NetworkManager.Singleton.LocalClientId ? 
                        0 : 
                        180);
        }





    }

    private void Update() {
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
        if (transform.position.x <= -levelWidth && ray.direction.x < 0.0f) newPosition = new Vector3(-levelWidth, 0.0f, transform.position.z);
        else if (transform.position.x >= levelWidth && ray.direction.x > 0.0f) newPosition = new Vector3(levelWidth, 0.0f, transform.position.z);
        else newPosition = new Vector3(ray.GetPoint(moveSpeed).x, 0.0f, transform.position.z);

        transform.position = newPosition;
    }
}