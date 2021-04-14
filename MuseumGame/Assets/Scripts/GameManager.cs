using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject canvas;
    public GameObject spriteToFade;
    public GameObject events;

    public string sceneToLoad = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(events);

        }
        else
        {
            Destroy(gameObject);
            Destroy(canvas);
            Destroy(events);

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (sceneToLoad != "")
        {
            LoadLevel(sceneToLoad);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LerpFunction(Color.black, 0.25f));
        StartCoroutine(LoadSceneAsync(levelName));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
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

}

