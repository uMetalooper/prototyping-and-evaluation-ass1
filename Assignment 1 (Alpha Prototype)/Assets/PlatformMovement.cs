using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public float maxheight;
    public float minheight;
    public GameObject ConnectedBlock;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        float posy = gameObject.transform.position.y;
        float posy2 = ConnectedBlock.transform.position.y;

        if(collision.gameObject.tag == "Player")
        {
            if (posy > minheight)
            {
                posy -= Time.deltaTime / 10;
                posy2 += Time.deltaTime / 10;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, posy, gameObject.transform.position.z);
                ConnectedBlock.transform.position = new Vector3(ConnectedBlock.transform.position.x, posy2, ConnectedBlock.transform.position.z);
            }

        }
    }
}
