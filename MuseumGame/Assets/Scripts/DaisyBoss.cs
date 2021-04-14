using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaisyBoss : MonoBehaviour
{
    public int numProjectiles;
    public GameObject projectile;
    public int projSpeed;
    public float timeBetween;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator SpawnProjectiles()
    {
        for (; ; )
        {
            for (int i = numProjectiles; i > 0; i--)
            {
                GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
                clone.transform.Rotate(new Vector3(0, 0, (i * (180 / (numProjectiles - 1)) * -1) + (180 / (numProjectiles - 1))));
                clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * projSpeed;
            }

            yield return new WaitForSeconds(timeBetween);
        }
    }
}
