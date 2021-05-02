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

    public GameObject foregroundCanvas;
    public GameObject backgroundCanvas;

    public GameObject spriteToFade;
    public GameObject events;
    public GameObject player;
    public GameObject dialogBox;
    public GameObject dialogText;
    public GameObject healthbar;
    public GameObject startButton;
    public GameObject galleryButton;
    public GameObject pauseButton;
    public GameObject title;
    public GameObject panel;

    public Sprite[] art;
    [TextArea]
    public string[] artInfo;
    public GameObject artImage;
    public GameObject baseImage;
    public GameObject baseDialog;
    public GameObject scrollStuff;
    public GameObject scrollSpace;
    public GameObject hideGalleryButton;
    private GameObject[] galleryArt;
    private GameObject[] galleryDialog;

    public Sprite[] powerupSprites;
    [TextArea]
    public string[] powerupInfo;
    private Powerup powerIndex = Powerup.Other;

    public GameObject powerUps;
    public GameObject gliderPowerupButton;
    public GameObject boxPowerupButton;
    public GameObject doubleJumpPowerupButton;
    public GameObject shrinkPowerupButton;
    public GameObject otherPowerupButton;
    public GameObject powerupText;



    private AudioSource sound;
    public string sceneToLoad = "";
    public Vector3 positionToLoad = new Vector3(0.0f, 0.0f, 0.0f);

    public AudioClip[] music;

    public GameObject enemyHealthBar;

    private bool shrinkButtonPressed = false;

    private bool paused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(foregroundCanvas);
            DontDestroyOnLoad(backgroundCanvas);
            DontDestroyOnLoad(events);
            DontDestroyOnLoad(player);
        }
        else
        {
            Destroy(gameObject);
            Destroy(foregroundCanvas);
            Destroy(backgroundCanvas);

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
        //GameManager.Instance.enablePowerup(3);
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
        if (Input.GetKeyDown(KeyCode.Q)){
            ShrinkButtonClicked();
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

    //Displays art after in Museum
    public void StartDialog(int index)
    {
        dialogBox.SetActive(true);
        dialogText.GetComponent<TextMeshProUGUI>().text = artInfo[index];
        artImage.SetActive(true);
        artImage.GetComponent<Image>().sprite = art[index];
        artImage.GetComponent<Image>().SetNativeSize();
        artImage.GetComponent<RectTransform>().anchoredPosition = new Vector2 (-artImage.GetComponent<Image>().sprite.rect.width / 2, 0);
        
    }
    //Hides art in Museum
    public void HideDialog()
    {
        dialogBox.SetActive(false);
        artImage.SetActive(false);
        //hideGalleryButton.SetActive(false);

    }


    public void PlayButton()
    {
        paused = false;
        startButton.SetActive(false);
        galleryButton.SetActive(false);
        title.SetActive(false);
        panel.SetActive(false);
        healthbar.SetActive(true);
        powerUps.SetActive(true);
        pauseButton.SetActive(true);
        disableAllPowerups();
        player.SetActive(true);
        LoadLevel(sceneToLoad, positionToLoad);
        title.GetComponent<TextMeshProUGUI>().text = "Menu";
        startButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Restart";


    }
    public void GalleryButton()
    {
        scrollSpace.SetActive(true);
        hideGalleryButton.SetActive(true);

        float scrollStuffSize = 0;

        galleryArt = new GameObject[art.Length];
        galleryDialog = new GameObject[artInfo.Length];


        for (int i = 0; i < art.Length;i++) {
            scrollStuffSize += (300 + 20);
            GameObject b = Instantiate(baseImage);
            b.transform.SetParent(scrollStuff.transform,false);
            b.GetComponent<Image>().sprite = art[i];
            b.GetComponent<Image>().SetNativeSize();
            galleryArt[i] = b;
        }
        scrollStuff.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollStuff.GetComponent<RectTransform>().sizeDelta.x, scrollStuffSize );
        GameObject temp = null;
        float totDist = 0;
        for (int i = 0; i < galleryArt.Length; i++) {
            float sx = (-scrollStuff.GetComponent<RectTransform>().sizeDelta.x / 2) + (150);
            float b;
            
            if (i == 0)
            {

                 b = (scrollStuff.GetComponent<RectTransform>().sizeDelta.y / 2)- (art[i].rect.height/2);

            }
            else
            {
                totDist += (300 + 20);
                b = (scrollStuff.GetComponent<RectTransform>().sizeDelta.y / 2) - (150) - (totDist);
            }
            galleryArt[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(sx, b);
            temp = galleryArt[i];
        }
        for (int i = 0; i < artInfo.Length; i++) {
            GameObject d = Instantiate(baseDialog);
            d.transform.SetParent(scrollStuff.transform,false);
            d.GetComponent<RectTransform>().anchoredPosition = new Vector2((scrollStuff.GetComponent<RectTransform>().sizeDelta.x / 2) - (150), galleryArt[i].GetComponent<RectTransform>().anchoredPosition.y);
            d.transform.Find("Box").GetComponent<TextMeshProUGUI>().text = artInfo[i];
            galleryDialog[i] = d;
        }
        scrollStuff.GetComponent<RectTransform>().anchoredPosition = new Vector2(scrollStuff.GetComponent<RectTransform>().anchoredPosition.x, (-scrollStuff.GetComponent<RectTransform>().sizeDelta.y/2) + 150);
    }

    public void CloseButton()
    {
        if (scrollSpace.activeSelf)
        {
            for (int i = 0; i < galleryArt.Length; i++)
            {
                Destroy(galleryArt[i]);
                Destroy(galleryDialog[i]);
            }
            scrollSpace.SetActive(false);
            if (!paused)
            {
                hideGalleryButton.SetActive(false);
            }
        } else if (powerIndex != Powerup.Other) {
            dialogBox.SetActive(true);
            dialogText.GetComponent<TextMeshProUGUI>().text = "Congratulations! You received a new powerup! Click the powerup icon on the Powerup Pallet to activate the powerup. " + powerupInfo[(int) powerIndex];
            artImage.SetActive(true);
            artImage.GetComponent<Image>().sprite = powerupSprites[(int) powerIndex];
            artImage.GetComponent<RectTransform>().sizeDelta = new Vector2(300,300);
            hideGalleryButton.SetActive(true);
            powerIndex = Powerup.Other;
        }
        else
        {
            paused = false;
            pauseButton.SetActive(true);
            hideGalleryButton.SetActive(false);
            startButton.SetActive(false);
            galleryButton.SetActive(false);
            title.SetActive(false);
            panel.SetActive(false);
            healthbar.SetActive(true);
            powerUps.SetActive(true);
            artImage.SetActive(false);
            dialogBox.SetActive(false);
        }
    }

    public void ArtReceived(int artIndex, Powerup powerupIndex)
    {
        hideGalleryButton.SetActive(true);
        StartDialog(artIndex);
        powerIndex = powerupIndex;
        
       /* artImage.GetComponent<Image>().SetNativeSize();
        artImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-artImage.GetComponent<Image>().sprite.rect.width / 2, 0);*/
    }

    public void PauseButton()
    {
        paused = true;
        pauseButton.SetActive(false);
        hideGalleryButton.SetActive(true);
        startButton.SetActive(true);
        galleryButton.SetActive(true);
        title.SetActive(true);
        panel.SetActive(true);
        healthbar.SetActive(false);
        powerUps.SetActive(false);
        HideDialog();



    }

    public bool isPaused()
    {
        return paused;
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


    public void enablePowerup(Powerup powerupID)
    {
        print("Enabling powerup " + (int)powerupID);
        if (powerupID >= 0 && powerupID!=Powerup.Other)
        {
            var powerup = powerUps.gameObject.transform.GetChild((int) powerupID);
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
        StartCoroutine(ShrinkButtonAction());
    }

    public IEnumerator ShrinkButtonAction()
    {
        if (!shrinkButtonPressed)
        {
            shrinkButtonPressed = true;
            Debug.Log("Start Shrink Logic");
            player.GetComponent<Shrink>().startShrinking();
            yield return new WaitForSeconds(1);
            shrinkButtonPressed = false;
        }
    }

    public void DoubleJumpClicked()
    {
        player.GetComponent<movement>().maxJumps = 2;
    }
}

