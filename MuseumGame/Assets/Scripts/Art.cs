using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Art : MonoBehaviour
{
    public int index;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer.enabled = false;
        collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var status = GameManager.Instance.IsArtActivated(index);
        spriteRenderer.enabled = status;
        collider.enabled = status;
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
