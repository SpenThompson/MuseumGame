using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    public GameObject projectile;
    public int projSpeed;
    public float fireRate;
    public bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
	}

	IEnumerator SpawnProjectiles()
    {
        for (; ; )
        {



            GameObject clone = Instantiate(projectile, transform.position, transform.rotation);

            if (isRight)
            {
                clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, .5f);
                clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * projSpeed;
                transform.localScale = new Vector3(-1,transform.localScale.y,transform.localScale.z);

            }
            else
            {
                clone.transform.Rotate(new Vector3(0, 0, 180));
                clone.transform.position = new Vector3(clone.transform.position.x, clone.transform.position.y, .5f);
                clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * projSpeed;
            }

            yield return new WaitForSeconds(1 / fireRate);
        }
    }
}
