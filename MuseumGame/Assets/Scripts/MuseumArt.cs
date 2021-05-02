using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuseumArt : MonoBehaviour
{
    public int artId = 0;
    public GameObject artworkToEnable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        artworkToEnable.SetActive(GameManager.Instance.enabledArt[artId]); 
    }
}
