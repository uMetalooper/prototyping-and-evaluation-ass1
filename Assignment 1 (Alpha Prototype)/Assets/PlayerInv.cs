using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInv : MonoBehaviour
{
    public bool Inv1;
    public bool Inv2;
    bool Inv3;


    public bool CP1;
    public bool CP2;
    public bool CP3;
    public bool CP4;


    public Image Invslots;
    public Image Invslots2;

    public AudioSource ASPickUpB;

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
            Inv1 = true;
            gameObject.GetComponent<PlayerController>().health = -1;
            ASPickUpB.Play();
        }

        if (other.gameObject.tag == "PickUpB")
        {
            Invslots2.enabled = true;
            Destroy(other.gameObject);
            Inv2 = true;

            ASPickUpB.Play();
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {
            Invslots.enabled = false;
            Inv1 = false;
        }

        if (other.gameObject.tag == "DoorBreaker")
        {
            Invslots2.enabled = false;
            Inv2 = false;
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
