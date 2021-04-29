using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paintShoot : MonoBehaviour
{
    public GameObject projectile;
    public int projSpeed;
    public float waitTime;
    private float horizontal;
    private float vertical;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E) && canShoot)
        {
            Debug.Log("shooting");
            canShoot = false;
            StartCoroutine(ProjTimer());
            GameObject proj = Instantiate(projectile, transform.position,transform.rotation);
            if (horizontal > 0 && vertical > 0) {
                proj.transform.Rotate(new Vector3(0, 0, 45));
            } else if (horizontal > 0 && vertical < 0) {
                proj.transform.Rotate(new Vector3(0, 0, -45));
            } else if (horizontal < 0 && vertical < 0) {
                proj.transform.Rotate(new Vector3(0, 0, -135));
            } else if (horizontal < 0 && vertical > 0) {
                proj.transform.Rotate(new Vector3(0, 0, 135));
            } else if (horizontal > 0) {
                proj.transform.Rotate(new Vector3(0, 0, 0));
            } else if (horizontal < 0) {
                proj.transform.Rotate(new Vector3(0, 0, 180));
            } else if (vertical > 0) {
                proj.transform.Rotate(new Vector3(0, 0, 90));
            } else if (vertical < 0) {
                proj.transform.Rotate(new Vector3(0, 0, -90));
            } else {
                if (GetComponent<movement>().faceRight)
                {
                    proj.transform.Rotate(new Vector3(0, 0, 0));
                }
                else
                {
                    proj.transform.Rotate(new Vector3(0, 0, 180));
                }
            }
            proj.GetComponent<Rigidbody2D>().velocity = proj.transform.right * projSpeed;
        }
    }

    IEnumerator ProjTimer()
    {
        yield return new WaitForSeconds(waitTime);
        canShoot = true;
    }

 
}
