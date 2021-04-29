using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject canvas;
    public GameObject spriteToFade;
    public GameObject events;
    public GameObject player;
    public GameObject dialogBox;
    public GameObject dialogText;
    public GameObject healthbar;
    public GameObject startButton;
    public GameObject title;
    public GameObject panel;

    public Sprite[] art;
    public string[] artInfo;
    public GameObject artImage;


    public GameObject powerUps;
    public GameObject gliderPowerupButton;
    public GameObject boxPowerupButton;
    public GameObject doubleJumpPowerupButton;
    public GameObject shrinkPowerupButton;
    public GameObject otherPowerupButton;
    public GameObject powerupText;



    private AudioSource sound;
    public string sceneToLoad = "";

    public AudioClip[] music;

    public GameObject enemyHealthBar;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);
            DontDestroyOnLoad(player);
        }
        else
        {
            Destroy(gameObject);
            Destroy(canvas);
            Destroy(events);
            Destroy(player);
            Destroy(powerUps);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       /* if (sceneToLoad != "")
        {
            LoadLevel(sceneToLoad, new Vector3(0, 0, 0));
        }*/
        sound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Spawn" || SceneManager.GetActiveScene().name == "EnemyTunnel" || SceneManager.GetActiveScene().name == "RouteToTower" || SceneManager.GetActiveScene().name == "Tower" || SceneManager.GetActiveScene().name == "PathToBoss" || SceneManager.GetActiveScene().name == "BossRoom") {
            sound.clip = music[0];
        } else if (SceneManager.GetActiveScene().name == "Museum") {
            sound.clip = music[1];
        } else if (SceneManager.GetActiveScene().name == "DogLevelSpawn" || SceneManager.GetActiveScene().name == "DogBoss") {
        }
        if (!sound.isPlaying)
        {
            sound.Play();
        }
    }

    public void LoadLevel(string levelName, Vector3 whereTo)
    {
        StartCoroutine(LerpFunction(Color.black, 0.25f));
        StartCoroutine(LoadSceneAsync(levelName, whereTo));
    }

    IEnumerator LoadSceneAsync(string scene, Vector3 whereTo)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        player.transform.position = whereTo;
    }

    IEnumerator LerpFunction(Color endValue, float duration)
    {
        Image sprite = spriteToFade.GetComponent<Image>();
        spriteToFade.SetActive(true);

        float time = 0;
        Color startValue = sprite.color;

        while (time < duration)
        {
            sprite.color = Color.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        sprite.color = endValue;
        spriteToFade.SetActive(false);
    }

    public GameObject GetPlayer()
    {
        return player;
    }
    public void StartDialog(int index)
    {
        dialogBox.SetActive(true);
        dialogText.GetComponent<TextMeshProUGUI>().text = artInfo[index];
        artImage.SetActive(true);
        artImage.GetComponent<Image>().sprite = art[index];
    }
    public void HideDialog()
    {
        dialogBox.SetActive(false);
        artImage.SetActive(false);
    }
    public void PlayButton()
    {
        startButton.SetActive(false);
        title.SetActive(false);
        panel.SetActive(false);
        healthbar.SetActive(true);
        powerUps.SetActive(true);
        disableAllPowerups();
        LoadLevel(sceneToLoad, new Vector3(0, 0, 0));


    }
    public GameObject GetEnemyHealthBar()
    {
        return enemyHealthBar;
    }

    public void disablePowerup(int powerupID)
    {
        if (powerupID >= 0 && powerupID <= 4)
        {
            var powerup = powerUps.gameObject.transform.GetChild(powerupID);
            powerup.gameObject.SetActive(false);
        }
    }


    public void enablePowerup(int powerupID)
    {
        if (powerupID >= 0 && powerupID <= 4)
        {
            var powerup = powerUps.gameObject.transform.GetChild(powerupID);
            powerup.gameObject.SetActive(true);
        }
    }

    public void disableAllPowerups()
    {
        for(int i=0; i<=4; i++)
        {
            disablePowerup(i);
        }
    }

    public void GliderButtonClicked()
    {
        Debug.Log("Is Gliding? " + player.GetComponent<Glider>().IsGliding);
        player.GetComponent<Glider>().ToggleGliding();
    }

    public void ShrinkButtonClicked()
    {
        player.GetComponent<Shrink>().startShrinking();
        Debug.Log("Start Shrink Logic");
    }

    public void DoubleJumpClicked()
    {
        player.GetComponent<movement>().maxJumps = 2;
    }
}

