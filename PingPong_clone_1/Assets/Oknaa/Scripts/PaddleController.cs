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
        // get the different clients to spawn the paddle in different positions 5 and -5 on the z axis
        if (NetworkManager.Singleton.ConnectedClients.TryGetValue(OwnerClientId, out var networkClient)) {
            transform.position = new Vector3(0, networkClient.ClientId == 0 ? -5 : 5, 0);
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
        if (transform.position.x <= -levelWidth && ray.direction.x < 0.0f) newPosition = new Vector3(-levelWidth, transform.position.y, 0);
        else if (transform.position.x >= levelWidth && ray.direction.x > 0.0f) newPosition = new Vector3(levelWidth, transform.position.y, 0);
        else newPosition = new Vector3(ray.GetPoint(moveSpeed).x, transform.position.y, 0);

        transform.position = newPosition;
        if(transform.position.x>3.7f) transform.position = new Vector3(3.7f, transform.position.y, 0);
        if(transform.position.x<-3.7f) transform.position = new Vector3(-3.7f, transform.position.y, 0);
    }
}