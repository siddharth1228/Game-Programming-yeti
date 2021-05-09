using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Update is called once per frame
    public Transform player;
     void Update()
    {
        transform.position=new Vector3(player.position.x,player.position.y,player.position.z);
    }
}
