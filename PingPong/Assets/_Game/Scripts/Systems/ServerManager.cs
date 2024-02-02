using System;
using System.Collections;
using System.Collections.Generic;
using Oknaa.Scripts;
using Unity.Netcode;
using UnityEngine;

public class ServerManager : NetworkBehaviour
{
    private void Start() 
    {
        
    }

   
    public void SetCamera()
    {
        CameraController.Instance.SetCamera(IsServer);
        
    }


}

