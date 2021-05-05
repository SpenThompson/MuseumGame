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
    public GameObject helpButton;
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
    public GameObject closeButton;
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

    public bool[] artActivated;

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
        //GameManager.Instance.enablePowerup(3);
        artActivated = new bool[art.Length]; //new bool[] always starts off as false
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Spawn" || SceneManager.GetActiveScene().name == "EnemyTunnel" || SceneManager.GetActiveScene().name == "RouteToTower" || SceneManager.GetActiveScene().name == "Tower" || SceneManager.GetActiveScene().name == "PathToBoss" || SceneManager.GetActiveScene().name == "BossRoom") {
            sound.clip = music[0];
        } else if (SceneManager.GetActiveScene().name == "Museum" || SceneManager.GetActiveScene().name == "MainMenu") {
            sound.clip = music[1];
        } else if (SceneManager.GetActiveScene().name == "DogLevelSpawn" || SceneManager.GetActiveScene().name == "DogBoss" || SceneManager.GetActiveScene().name == "DogTunnel" || SceneManager.GetActiveScene().name == "NewDogSong" || SceneManager.GetActiveScene().name == "ShrinkPowerup" || SceneManager.GetActiveScene().name == "ToShrinkPowerup") {
            sound.clip = music[2];
        } else if (SceneManager.GetActiveScene().name == "WinterOne" || SceneManager.GetActiveScene().name == "WinterTwo" || SceneManager.GetActiveScene().name == "WinterThree" || SceneManager.GetActiveScene().name == "WinterFour" || SceneManager.GetActiveScene().name == "WinterBoss") {
            sound.clip = music[3];
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
        helpButton.SetActive(false);
        title.SetActive(false);
        panel.SetActive(false);
        healthbar.SetActive(true);
        powerUps.SetActive(true);
        pauseButton.SetActive(true);
        disableAllPowerups();
        artActivated = new bool[artActivated.Length]; //Resets to [false,...]

        player.SetActive(true);
        Time.timeScale = 1; // Fixes infinite loading bug
        LoadLevel(sceneToLoad, positionToLoad);
        title.GetComponent<TextMeshProUGUI>().text = "Paused";
        startButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Restart";
        closeButton.SetActive(false);//Remove gallery button
        HelpButton();



    }
    public void GalleryButton()
    {
        scrollSpace.SetActive(true);
        closeButton.SetActive(true);

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
            if (artActivated[i])
            {
                d.transform.Find("Box").GetComponent<TextMeshProUGUI>().text += "\nFOUND";
            }
            else
            {
                d.transform.Find("Box").GetComponent<TextMeshProUGUI>().text += "\nNOT FOUND";

            }
            galleryDialog[i] = d;
        }
        scrollStuff.GetComponent<RectTransform>().anchoredPosition = new Vector2(scrollStuff.GetComponent<RectTransform>().anchoredPosition.x, (-scrollStuff.GetComponent<RectTransform>().sizeDelta.y/2) + 150);
    }

    public void HelpButton()
    {
        scrollSpace.SetActive(true);
        closeButton.SetActive(true);

        float scrollStuffSize = 0;

        string[] helpStuff = new string[9];
        helpStuff[0] = "OH NO! The Art is Going Wild!\nCollect Every Piece of Art Before They Get Completely Out of Control.";
        helpStuff[1] = "-Move With the Arrow Keys\n-Jump With Space\n-Fire Projectile With E\n-Avoid Enemy Projectiles and Other Possible Dangers";
        helpStuff[2] = "Some Paintings Come with Unique Powerups the Grant You Special Abilities:";
        helpStuff[3] = "The Glider Powerup allows you to glide through the air and travel farther before landing";
        helpStuff[4] = "The Shrink Powerup shrinks you so you can fit in small places. Deactivate the powerup to return to normal size";
        helpStuff[5] = "The Double Jump Powerup allows you to jump twice before landing";
        helpStuff[6] = "The Box Stack Powerup allows you to move certain boxes to your convinience, providing cover from enemies";
        helpStuff[7] = "The HealthUp powerup increases your max health";
        helpStuff[8] = "Check out the Gallery for Information on the Art as well as to view which pieces of Art you have found!";

        galleryDialog = new GameObject[helpStuff.Length];
        galleryArt = new GameObject[helpStuff.Length];
        


        for (int i = 0; i < helpStuff.Length; i++)
        {
            scrollStuffSize += (300);

            GameObject d = Instantiate(baseDialog);
            d.transform.SetParent(scrollStuff.transform, false);
            d.transform.Find("Box").GetComponent<TextMeshProUGUI>().text = helpStuff[i];
            galleryDialog[i] = d;
            galleryArt[i] = d;
        }
        scrollStuff.GetComponent<RectTransform>().sizeDelta = new Vector2(scrollStuff.GetComponent<RectTransform>().sizeDelta.x, scrollStuffSize);
        GameObject temp = null;
        float totDist = 0;
        for (int i = 0; i < helpStuff.Length; i++)
        {
            float sx = (-scrollStuff.GetComponent<RectTransform>().sizeDelta.x / 2) + (300);
            float b;

            if (i == 0)
            {

                b = (scrollStuff.GetComponent<RectTransform>().sizeDelta.y / 2) - (150);

            }
            else
            {
                totDist += (300);
                b = (scrollStuff.GetComponent<RectTransform>().sizeDelta.y / 2) - (150) - (totDist);
            }
            galleryDialog[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(sx, b);
            temp = galleryDialog[i];
        }
        
        scrollStuff.GetComponent<RectTransform>().anchoredPosition = new Vector2(scrollStuff.GetComponent<RectTransform>().anchoredPosition.x, (-scrollStuff.GetComponent<RectTransform>().sizeDelta.y / 2) + 150);

    }
    public void CloseButton()
    {
        if (scrollSpace.activeSelf)
        {
            for (int i = 0; i < galleryDialog.Length; i++)
            {
                
                Destroy(galleryArt[i]);
                
                Destroy(galleryDialog[i]);
            }
            scrollSpace.SetActive(false);
            if (!paused)
            {
                closeButton.SetActive(false);
            }
        } else if (powerIndex != Powerup.Other) {
            dialogBox.SetActive(true);
            dialogText.GetComponent<TextMeshProUGUI>().text = "Congratulations! You received a new powerup! Click the powerup icon on the Powerup Pallet to activate the powerup. " + powerupInfo[(int) powerIndex];
            artImage.SetActive(true);
            artImage.GetComponent<Image>().sprite = powerupSprites[(int) powerIndex];
            artImage.GetComponent<RectTransform>().sizeDelta = new Vector2(300,300);
            closeButton.SetActive(true);
            powerIndex = Powerup.Other;
        }
        else
        {
            paused = false;
            pauseButton.SetActive(true);
            closeButton.SetActive(false);
            startButton.SetActive(false);
            galleryButton.SetActive(false);
            helpButton.SetActive(false);
            title.SetActive(false);
            panel.SetActive(false);
            healthbar.SetActive(true);
            powerUps.SetActive(true);
            artImage.SetActive(false);
            dialogBox.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ArtReceived(int artIndex, Powerup powerupIndex)
    {
        closeButton.SetActive(true);
        StartDialog(artIndex);
        artActivated[artIndex] = true;
        powerIndex = powerupIndex;
        
       /* artImage.GetComponent<Image>().SetNativeSize();
        artImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(-artImage.GetComponent<Image>().sprite.rect.width / 2, 0);*/
    }
    public bool IsArtActivated(int artIndex)
    {
        return artActivated[artIndex];
    }

    public void PauseButton()
    {
        paused = true;
        pauseButton.SetActive(false);
        closeButton.SetActive(true);
        startButton.SetActive(true);
        galleryButton.SetActive(true);
        helpButton.SetActive(true);
        title.SetActive(true);
        panel.SetActive(true);
        healthbar.SetActive(false);
        powerUps.SetActive(false);
        HideDialog();
        Time.timeScale = 0;

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

