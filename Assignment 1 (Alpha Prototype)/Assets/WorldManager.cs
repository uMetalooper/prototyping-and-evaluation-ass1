using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public GameObject player;
    Transform Playerpos;
    public Vector3 Playerpos2;
    public GameObject UI;
    public GameObject Deathscreeen1;
    public GameObject Deathscreeen2;

    public bool fire;
    public bool ice;
    bool changing = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("pos" + Playerpos2);
        //Debug.Log("active scene" + SceneManager.GetActiveScene().name);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {

            if(player.GetComponent<PlayerMov>().isdead)
            {
                changing = true;
                if (SceneManager.GetActiveScene().name == "EnvironmentUI")
                {
                    Deathscreeen1.SetActive(true);
                    UI.SetActive(false);
                    StartCoroutine(World2());

                }
                else
                {
                    Deathscreeen2.SetActive(true);
                    UI.SetActive(false);
                    StartCoroutine(World1());
                }
                //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
            }
        }

        if (changing == false)
        {
            Playerpos = player.transform;
            Playerpos2 = player.transform.position;
        }
    }


    IEnumerator World2()
    {
        Debug.Log("Has run");
        player.GetComponent<PlayerMov>().isdead = false;
        yield return new WaitForSeconds(3);
        player.GetComponent<PlayerMov>().isdead = false;
        SceneManager.LoadScene("EnvironmentUI 1");
        player = GameObject.FindGameObjectWithTag("Player");
        ice = true;
        fire = false;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        //yield return new WaitForSeconds(2);
        player.transform.position = new Vector3(Playerpos2.x, Playerpos2.y + 5, Playerpos2.z);
        Deathscreeen1.SetActive(false);
        UI.SetActive(true);
        changing = false;
        StopAllCoroutines();
        
    }

    IEnumerator World1()
    {
        Debug.Log("Has run");
        player.GetComponent<PlayerMov>().isdead = false;
        yield return new WaitForSeconds(3);
        player.GetComponent<PlayerMov>().isdead = false;
        SceneManager.LoadScene("EnvironmentUI");
        player = GameObject.FindGameObjectWithTag("Player");
        fire = true;
        ice = false;
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        //yield return new WaitForSeconds(2);
        player.transform.position = new Vector3(Playerpos2.x, Playerpos2.y + 5, Playerpos2.z);
        Deathscreeen2.SetActive(false);
        UI.SetActive(true);
        changing = false;
        StopAllCoroutines();

    }
}
