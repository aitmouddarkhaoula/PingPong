using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Camera clientCamera;
    public Camera ServerCamera;
   
    public void SetCamera(bool isServer)
    {
        if (isServer)
        {
            clientCamera.gameObject.SetActive(false);
            ServerCamera.gameObject.SetActive(true);
        }
        else
        {
            clientCamera.gameObject.SetActive(true);
            ServerCamera.gameObject.SetActive(false);
        }
    }
    public Camera GetCamera(bool isClient, bool isHost)
    {
        if (isClient)
        {
            return clientCamera;
        }
        else
        {
            return ServerCamera;
        }
    }
}
