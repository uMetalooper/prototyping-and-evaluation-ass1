using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;
    public float smoothing = 5f;

    Vector3 offsetval;

    // Start is called before the first frame update
    void Start()
    {
        offsetval = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camTargetPosition = player.position + offsetval;
        transform.position = Vector3.Lerp(transform.position, camTargetPosition, smoothing * Time.deltaTime);
    }
}
