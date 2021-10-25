using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 Vec;

    // Update is called once per frame
    void Update()
    {
        Vec = transform.localPosition;  
        Vec.y += Input.GetAxis("Jump") * Time.deltaTime * 40;  
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20;  
        Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20;  
        transform.localPosition = Vec;
    }
}
