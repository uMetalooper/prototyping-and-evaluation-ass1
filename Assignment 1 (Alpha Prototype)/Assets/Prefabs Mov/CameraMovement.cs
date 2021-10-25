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
    }
}
