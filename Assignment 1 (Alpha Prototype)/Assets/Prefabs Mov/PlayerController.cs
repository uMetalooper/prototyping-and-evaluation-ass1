using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class PlayerController : MonoBehaviour
{
    public float health = 10f;
    public float speed = 5f;
    public float rotationSpeed = 10f;
    public static float distance;
    public bool rotating = false;

    public GameObject block;
    //public GameObject camera;
    public LayerMask blockMask;

    public bool isGrounded;
    private float jumpForce = -375f;
    private float dist;
    private float groundedTimer = 0;
    private float grabDist = 1.5f;
   
    private Rigidbody rigidBody;
    private RaycastHit hit;
    private Vector3 dir;
    private Vector3 movement;
    

    public Slider HealthSlider;
    public Material dissolveMatt;

    float DissolveTime = 0f;
    float DissolveWaitTime = 1.0f;
    float DissolveOutWaitTime = 2f;

    private static PlayerController playerinst;

    void Awake()
    {
        DontDestroyOnLoad (this);

        if(playerinst == null)
        {
            playerinst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    void Update()
    {


        if(this.GetComponent<Renderer>().enabled == true)
        {
            if(DissolveWaitTime > 0 )
            {
                DissolveWaitTime -= Time.deltaTime;
                
            }
            else{

                if(DissolveTime < 2.2)
                {
                    DissolveTime += Time.deltaTime;
                    dissolveMatt.SetFloat("Vector1_2b71c9ec7b1645d8b2d84d91a8412af7", DissolveTime / 2);
                    this.GetComponent<Renderer>().material.SetFloat("Vector1_2b71c9ec7b1645d8b2d84d91a8412af7", DissolveTime / 2);
                    DissolveOutWaitTime = 2f;
                }
            }
        }
        else
        {
            if(DissolveOutWaitTime > 0)
            {
                DissolveOutWaitTime -=Time.deltaTime;
            }
            else
            {
                DissolveTime = 0f;
                DissolveWaitTime = 1.0f;
                dissolveMatt.SetFloat("Vector1_2b71c9ec7b1645d8b2d84d91a8412af7", 0);
                this.GetComponent<Renderer>().material.SetFloat("Vector1_2b71c9ec7b1645d8b2d84d91a8412af7", 0);
            }
        }

        //Debug.Log("Dissolvetime alpha " + DissolveTime/2);

        if(Input.GetKeyDown(KeyCode.H))
        {
            health -= 1;
        }
        HealthSlider.value = health;
        HealthSlider.maxValue = 10.0f;

        if(block == null)
        {
            block = GameObject.FindGameObjectWithTag("Block");
        }
        if (Input.GetKeyDown(KeyCode.I) || health < 0)
        {
            //rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
            health = 0;
            //this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<Explode>().enabled = true;
            this.GetComponent<Explode>().Invoke("Main", 0);
            rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        }


        dist = 0.5f;
        dir = new Vector3(0, -1, 0);

        Vector3 endpoint = transform.position + new Vector3(1, 0, 0);
        Vector3 startpoint = transform.position + new Vector3(-1, 0, 0);

        groundedTimer += Time.deltaTime;

        //Position
        if (!isGrounded && groundedTimer >= 0.2f)
        {
            if (Physics.Raycast(transform.position, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 5f;
            }

            //Endpoint
            else if (Physics.Raycast(endpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 5f;
            }

            //Startpoint
            else if (Physics.Raycast(startpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 5f;
            }

            else
            {
                isGrounded = false;
            }
        }

        if (Input.GetButtonDown("Restart"))
        {
            //SceneManager.LoadScene(0);
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidBody.AddForce(new Vector3(0, -1, 0) * jumpForce);

            //Set airspeed
            speed = 3.5f;
            groundedTimer = 0;
            isGrounded = false;
        }

        distance = transform.localPosition.x;

        //Correct y component after landing
        if (transform.position.y < 0 && Physics.Raycast(transform.position, dir, dist))
        {
            transform.position = transform.position + new Vector3(0, (0 - transform.position.y), 0);
        }

        //Camera rotation
        if (Input.GetKeyDown(KeyCode.Q) && !rotating)
        {
            StartCoroutine(RotateLeft());
        }

        if (Input.GetKeyDown(KeyCode.E) && !rotating)
        {
            StartCoroutine(RotateRight());
        }

        //Correct rotation if necessary - if an angle goes above or below the desired 90 degree rotation
        if (!rotating && (transform.rotation.y != 90 || transform.rotation.y != 0 || transform.rotation.y != -90 || transform.rotation.y != 180))
        {
            var vec = transform.eulerAngles;
            vec.y = Mathf.Round(vec.y / 90) * 90;
            transform.eulerAngles = vec;
        }

        //Grab objects to move. Will only affect a certain type of block which will be defined by layers. Layers are currently set very generic.
        Ray ray = new Ray(this.transform.position, this.transform.forward);

        if (Physics.Raycast(ray, out hit, grabDist, blockMask))
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                block = hit.transform.gameObject;
                block.AddComponent<FixedJoint>();
                block.GetComponent<FixedJoint>().connectedBody = this.GetComponent<Rigidbody>();
                block.GetComponent<Rigidbody>().mass = 10;
            }

            if(Input.GetKeyUp(KeyCode.G))
            {
                Destroy(block.GetComponent<FixedJoint>());
                 block.GetComponent<Rigidbody>().mass = 1000000;
            }
        }
    }

    IEnumerator RotateLeft() // A couroutine can be run each frame so we can do animation.
    {
        rotating = true;

        float endTime = Time.time + 0.1f; // When to end the coroutine
        float step = 1f / 0.1f; // How much to step by per sec
        var fromAngle = transform.eulerAngles; // start rotation
        var targetRot = transform.eulerAngles + new Vector3(0, -90, 0); // where we want to be at the end
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            yield return 0;
        }

        rotating = false;
    }

    IEnumerator RotateRight() // A couroutine can be run each frame so we can do animation.
    {
        rotating = true;

        float endTime = Time.time + 0.1f; // When to end the coroutine
        float step = 1f / 0.1f; // How much to step by per sec
        var fromAngle = transform.eulerAngles; // start rotation
        var targetRot = transform.eulerAngles + new Vector3(0, 90, 0); // where we want to be at the end
        float t = 0; // how far we are. 0-1
        while (Time.time <= endTime)
        {
            t += step * Time.deltaTime;
            transform.eulerAngles = Vector3.Lerp(fromAngle, targetRot, t);
            yield return 0;
        }

        rotating = false;
    }

    void FixedUpdate()
    {
        float moveHorizontal;
        float moveVertical;

        if (this.transform.eulerAngles.y == 0 || this.transform.eulerAngles.y == 180)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }

        else
        {
            moveHorizontal = Input.GetAxis("Vertical");
        }

        if (this.transform.eulerAngles.y == 0 || this.transform.eulerAngles.y == 180)
        {
            moveVertical = Input.GetAxis("Vertical");
        }

        else
        {
            moveVertical = Input.GetAxis("Horizontal");
        }

        if (this.transform.eulerAngles.y == 0)
        {
            movement = new Vector3(moveHorizontal * (speed + 2.5f), rigidBody.velocity.y, moveVertical * (speed + 2.5f));
        }

        else if (this.transform.eulerAngles.y == 90)
        {
            movement = new Vector3(moveHorizontal * (speed + 2.5f), rigidBody.velocity.y, -moveVertical * (speed + 2.5f));
        }

        else if (this.transform.eulerAngles.y == 270)
        {
            movement = new Vector3(-moveHorizontal * (speed + 2.5f), rigidBody.velocity.y, moveVertical * (speed + 2.5f));
        }

        else 
        {
            movement = new Vector3(-moveHorizontal * (speed + 2.5f), rigidBody.velocity.y, -moveVertical * (speed + 2.5f));
        }

        rigidBody.velocity = movement;
    }



    

}
