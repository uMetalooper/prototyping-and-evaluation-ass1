using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Playerfinder : MonoBehaviour
{
    public CinemachineFreeLook cine;
    // Start is called before the first frame update
    void Start()
    {
        cine = gameObject.GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        cine.Follow = GameObject.FindGameObjectWithTag("Player").transform;
        cine.LookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
