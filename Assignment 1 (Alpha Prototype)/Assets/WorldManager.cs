using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject player;
    public GameObject Cam;
    Transform Playerpos;
    public Vector3 Playerpos2;
    public GameObject UI;
    public GameObject Deathscreeen1;
    public GameObject Deathscreeen2;

    PlayerInv inv;

    //UI
    public Text elapsedTimeDisplay;
    public Slider Progress;
    float elapsedtime;


    // Start is called before the first frame update
    void Start()
    {
        GameObject world = GameObject.FindGameObjectWithTag("WorldManager");
        if (world && world != this.gameObject)
        {
            //gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("Fire");
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
            DontDestroyOnLoad(Cam);
            DontDestroyOnLoad(player);
        }
        if (inv == null)
        {
            inv = player.GetComponent<PlayerInv>();
        }
        elapsedtime += Time.deltaTime;

        elapsedTimeDisplay.text = Mathf.FloorToInt(elapsedtime / 60).ToString("00") + ":" + Mathf.FloorToInt(elapsedtime % 60).ToString("00");

       // Debug.Log("pos" + Playerpos2);
       //Debug.Log("active scene" + SceneManager.GetActiveScene().name);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {

            if(player.GetComponent<PlayerController>().health < 1)
            {
                //changing = true;
                if (SceneManager.GetActiveScene().name == "Fire" || SceneManager.GetActiveScene().name == "Alpha_Prototype")
                {
                    StartCoroutine(World2());

                   
                }
                else if (SceneManager.GetActiveScene().name == "Ice")
                {
                    StartCoroutine(World1());

                }
            }
        }


            Playerpos = player.transform;
            Playerpos2 = player.transform.position;

        if(inv.CP1)
        {
            Progress.value = 25f;
        }
        if(inv.CP1 && inv.CP2)
        {
            Progress.value = 50f;
        }
        if (inv.CP1 && inv.CP2 && inv.CP3)
        {
            Progress.value = 75f;
        }
        if (inv.CP1 && inv.CP2 && inv.CP3 && inv.CP4)
        {
            Progress.value = 100f;
        }
    }


    IEnumerator World2()
    {
        player.GetComponent<PlayerController>().health = 10;
        yield return new WaitForSeconds(2);
        Deathscreeen1.SetActive(true);
        Debug.Log("Has run");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Ice");
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        DontDestroyOnLoad(Cam);
        DontDestroyOnLoad(player);
        player.GetComponent<Renderer>().enabled = true;
        Deathscreeen1.SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        StopAllCoroutines(); //can change to timer later if it messes with others

    }

    IEnumerator World1()
    {
        player.GetComponent<PlayerController>().health = 10;
        yield return new WaitForSeconds(2);
        Deathscreeen2.SetActive(true);
        Debug.Log("Has run");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Fire");
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        DontDestroyOnLoad(Cam);
        DontDestroyOnLoad(player);
        player.GetComponent<Renderer>().enabled = true;
        Deathscreeen2.SetActive(false);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        StopAllCoroutines(); //can change to timer later if it messes with others



    }



    //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
}
