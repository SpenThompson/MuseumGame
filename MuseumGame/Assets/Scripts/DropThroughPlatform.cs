using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropThroughPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxisRaw("Vertical"));
    }

    //https://forum.unity.com/threads/jumping-down-through-a-platform.414902/
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && (Input.GetAxisRaw("Vertical") < 0)){
            TogglePlatform();
            Invoke("TogglePlatform", .75f);
        }
    }
    public void TogglePlatform()
    {
        gameObject.GetComponent<Collider2D>().enabled = !gameObject.GetComponent<Collider2D>().enabled;
    }
}

