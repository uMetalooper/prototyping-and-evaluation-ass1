using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public GameObject player;
    //public GameObject Cam;
    Transform Playerpos;
    public Vector3 Playerpos2;
    public GameObject UI;
    public GameObject Deathscreeen1;
    public GameObject Deathscreeen2;
    public GameObject Winscreen;

    PlayerInv inv;
    //public Material dissolveMatt;

    //UI
    public Text elapsedTimeDisplay;
    public Slider Progress;
    //reset slider for death loading screen
    public Slider Deathscreeen1prog;
    public Slider Deathscreeen2prog;
    float elapsedtime;

    public bool fire = true;
    public bool ice = false;

    private static WorldManager managerinst;

    void Awake()
    {

    }
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

            if (player.GetComponent<PlayerController>().health < 1)
            {
                //changing = true;
                if (fire)
                {
                    StartCoroutine(World2());
                }
                else if (ice)
                {
                    StartCoroutine(World1());
                }
            }
        }


        Playerpos = player.transform;
        Playerpos2 = player.transform.position;

        if (inv.CP1)
        {
            Progress.value = 25f;
        }
        if (inv.CP1 && inv.CP2)
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
            Winscreen.SetActive(true);

        }
    }


    IEnumerator World2()
    {
        player.GetComponent<PlayerController>().health = 10;
        yield return new WaitForSeconds(2);
        Deathscreeen1.SetActive(true);
        Debug.Log("Has run");
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("Ice");
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        //DontDestroyOnLoad(Cam);
        //DontDestroyOnLoad(player);
        player.GetComponent<Renderer>().enabled = true;
        Deathscreeen1.SetActive(false);
        Deathscreeen1prog.value = 0;
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        if(player.GetComponent<PlayerController>().phase2 == true)
        {
            player.transform.position = new Vector3(player.GetComponent<PlayerController>().Playerposition.x + 200,
                player.GetComponent<PlayerController>().Playerposition.y + 10,
                player.GetComponent<PlayerController>().Playerposition.z
                );
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x + 200, player.transform.position.y + 5, player.transform.position.z);
        }    
        
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //player.GetComponent<PlayerController>().isDead = false;
        fire = false;
        ice = true;
        StopAllCoroutines(); //can change to timer later if it messes with others

    }

    IEnumerator World1()
    {
        player.GetComponent<PlayerController>().health = 10;
        yield return new WaitForSeconds(2);
        Deathscreeen2.SetActive(true);
        Debug.Log("Has run");
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("Fire");
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        //DontDestroyOnLoad(Cam);
        //DontDestroyOnLoad(player);
        player.GetComponent<Renderer>().enabled = true;
        Deathscreeen2.SetActive(false);
        Deathscreeen2prog.value = 0;
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        if (player.GetComponent<PlayerController>().phase2 == true)
        {
            player.transform.position = new Vector3(player.GetComponent<PlayerController>().Playerposition.x - 200,
                player.GetComponent<PlayerController>().Playerposition.y + 10,
                player.GetComponent<PlayerController>().Playerposition.z
                );
        }
        else
        {
            player.transform.position = new Vector3(player.transform.position.x - 200, player.transform.position.y + 5, player.transform.position.z);
        }
        //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //player.GetComponent<PlayerController>().isDead = false;
        ice = false;
        fire = true;
        StopAllCoroutines(); //can change to timer later if it messes with others
    }


}
