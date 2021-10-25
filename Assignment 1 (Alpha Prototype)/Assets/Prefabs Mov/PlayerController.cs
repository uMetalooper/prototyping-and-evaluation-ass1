using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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



    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        isGrounded = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I))
        {
            this.GetComponent<Renderer>().enabled = false;
            this.GetComponent<Explode>().enabled = true;
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

            else
            {
                isGrounded = false;
            }

            //Endpoint
            if (Physics.Raycast(endpoint, dir, dist))
            {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
                isGrounded = true;
                speed = 5f;
            }

            else
            {
                isGrounded = false;
            }

            //Startpoint
            if (Physics.Raycast(startpoint, dir, dist))
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
            }

            if(Input.GetKeyUp(KeyCode.G))
            {
                Destroy(block.GetComponent<FixedJoint>());
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
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(moveHorizontal * (speed + 2.5f), rigidBody.velocity.y, moveVertical * (speed + 2.5f));
        rigidBody.velocity = movement;
    }

    //explode test

}
