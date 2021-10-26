using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInv : MonoBehaviour
{
    bool Inv1;
    bool Inv2;
    bool Inv3;


    public bool CP1;
    public bool CP2;
    public bool CP3;
    public bool CP4;


    public Image Invslots;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            Invslots.enabled = true;
            Destroy(other.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            Invslots.enabled = false;
        }


        if (other.gameObject.tag == "CK1")
        {
            CP1 = true;
        }
        else if (other.gameObject.tag == "CK2")
        {
            CP2 = true;
        }
        else if (other.gameObject.tag == "CK3")
        {
            CP3 = true;
        }
        else if (other.gameObject.tag == "CK4")
        {
            CP4 = true;
        }
    }
}
