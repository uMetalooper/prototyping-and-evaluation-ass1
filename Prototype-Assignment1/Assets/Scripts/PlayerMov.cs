using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMov : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Transform groundcheck;
    public LayerMask ground;

    public float speed = 6f;
    public float jumpHeight = 4f;
    public float grounddistnce;

    bool isgrounded;
    public bool isdead = false;


    public float smoothing = 0.1f;
    float smoothingvelocity;

    Vector3 gravVelocity;
    public float gravity = -9.8f;

    // Update is called once per frame
    private void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>().Playerpos2;
    }

    void Update()
    {
        isgrounded = Physics.CheckSphere(groundcheck.position, grounddistnce, ground);

        if(isgrounded && gravVelocity.y < 0)
        {
            gravVelocity.y = -2f;
        }

        if(isgrounded && Input.GetButtonDown("Jump") && isgrounded)
        {
            gravVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y; // mathematical that
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothingvelocity , smoothing);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        gravVelocity.y += gravity * Time.deltaTime;
        controller.Move(gravVelocity * Time.deltaTime);

        if(Input.GetKey(KeyCode.K))
        {
            Debug.Log("K");
            isdead = true;
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("WorldManager"));
        }
    }
}
