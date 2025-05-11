using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    public AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip levelMusic;

    private string currentScene;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        PlayMusicForCurrentScene();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (currentScene != scene.name)
        {
            currentScene = scene.name;
            PlayMusicForCurrentScene();
        }
    }

    void PlayMusicForCurrentScene()
    {
        // Menu/Selection scenes
        if (currentScene == "main" || currentScene == "Selection" || currentScene == "Level Selection")
        {
            PlayMusic(menuMusic);
        }
        // Level scenes (starts with "Level" and a number)
        else if (currentScene.StartsWith("Level"))
        {
            PlayMusic(levelMusic);
        }
    }

    void PlayMusic(AudioClip music)
    {
        if (music != null && audioSource.clip != music)
        {
            audioSource.clip = music;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}