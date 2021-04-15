using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Art : MonoBehaviour
{
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // if (Input.GetKeyDown("space")) {
                GameManager.Instance.StartDialog(index);
           // }
        }
    }
    private void OnTriggerExit2D()
    {
        GameManager.Instance.HideDialog();
    }
}
