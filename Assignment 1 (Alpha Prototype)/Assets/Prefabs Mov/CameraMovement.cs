using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 7.5f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 pos = player.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, pos, (smoothSpeed * Time.deltaTime));
        transform.position = smoothPos;
        //this.transform.eulerAngles = new Vector3(45, player.transform.localRotation.eulerAngles.y, 0);
        //this.transform.position = new Vector3(player.transform.position.x, 3.5f, player.transform.position.z - 2.5f);
    }
}