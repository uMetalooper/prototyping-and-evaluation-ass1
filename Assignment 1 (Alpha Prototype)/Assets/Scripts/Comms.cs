using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comms : MonoBehaviour
{
public GameObject UI;
    public GameObject[] objects;
    public bool start;

    bool enable;
    // Start is called before the first frame update
    void Start()
    {

        enable = false;
        if (start)
        {

            //StartCoroutine(WaitForComms());
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void close()
    {  
        foreach (GameObject comm in objects)
        {
            comm.SetActive(false);
        }
        enable = true;
        Destroy(UI);       
    }

    //private IEnumerator WaitForComms()
    //{
    //    yield return new WaitForSeconds(10.0f);
    //    Destroy(UI);
    //    gameObject.SetActive(false);
    //}





    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && enable == false)
        {
            foreach (GameObject comm in objects)
            {
                if (comm != null && enable == false)
                {

                    comm.SetActive(true);
                    

                    

                }
            }
            enable = true;
        }
        
    }

    public void continuegame()
    {
        //isPaused = false;
        foreach (GameObject comm in objects)
        {
            comm.SetActive(false);
        }
    }
}
