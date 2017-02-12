using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z - 10);
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player != null) transform.position = player.transform.position + offset;
    }
}
