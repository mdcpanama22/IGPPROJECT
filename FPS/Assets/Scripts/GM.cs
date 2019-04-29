using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public static GM instance = null;
    public Vector3 worldBounds;
    private Camera mainCamera;

    public GameObject[] TransitionBarriers;


    private Text HighScoreT;
    private Text LivesT;
    private int lives;
    private float oldHighScore;
    private Text CurrentScoreT;
    private int CurrentScoreR;

    public bool lostLife;
    private float lInitialT;
    private float lFinalT;

    public GameObject Player;
    public GameObject PlayerPrefab;
    public GameObject[] bullet;
    public AudioClip[] Sounds;

    private GameObject PanelInfo;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 screen;
        screen.x = Screen.width;
        screen.y = Screen.height;
        screen.z = 0;

        worldBounds = Camera.main.ScreenToWorldPoint(screen);

        //GUI
        
                HighScoreT = GameObject.Find("HighScore").GetComponent<Text>();
                oldHighScore = PlayerPrefs.GetInt("HS", 1000);
                HighScore(oldHighScore);
                CurrentScoreT = GameObject.Find("Score").GetComponent<Text>();

        PanelInfo = GameObject.Find("PanelInfo");
        PanelInfo.transform.GetChild(0).gameObject.SetActive(true);
        PanelInfo.SetActive(true);

        //Score
        CurrentScoreR = 0;
                

        LivesT = GameObject.Find("Lives").GetComponent<Text>();
        lives = 3;
        LivesT.text = "Lives: " + lives.ToString();


        //Camera
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            int x = 0;
            int y = 0;
            if (Player.transform.position.x >= -14f && Player.transform.position.x < 14f)
                x = 0;
            else if (Player.transform.position.x >= 14f && Player.transform.position.x <= 42f)
                x = 1;
            else
                x = 2;
            if (Player.transform.position.y >= -4f && Player.transform.position.y < 16f)
                y = 0;
            else if (Player.transform.position.y >= 16f && Player.transform.position.y <= 37f)
                y = 1;
            else
                y = 2;
            StartCoroutine(moveBottomLeft(mainCamera.transform.position, x, y));
        }
    }

    public void ChangeScore(int score)
    {
       // GetComponent<AudioSource>().clip = Sounds[1];
       // GetComponent<AudioSource>().Play();
        CurrentScoreR += score;
        string CS = CurrentScoreR.ToString();
        string REAL = "";
        for (int i = 0; i < 6 - CS.Length; ++i)
        {
            REAL += "0";
        }
        REAL += CS;
        CurrentScoreT.text = "Score: " + REAL;
    }
    public void HighScore(float score)
    {
        string CS = score.ToString();
        string REAL = "";
        for (int i = 0; i < 6 - CS.Length; ++i)
        {
            REAL += "0";
        }
        REAL += CS;
        HighScoreT.text = "High Score: " + REAL;
    }
    public void LoseLife(GameObject oldPlayer)
    {
        Destroy(oldPlayer);
        GetComponent<AudioSource>().clip = Sounds[0];
        GetComponent<AudioSource>().Play();
        lives--;

        if (lives <= 0)
        {
            //END GAME SCENE
            EndGame();
        }
        else
        {
            Player = Instantiate(PlayerPrefab);
            StartCoroutine(moveBottomLeft(mainCamera.transform.position, 0, 0));
            LivesT.text = "Lives: " + lives.ToString(); 
        }

    }

    public IEnumerator moveBottomLeft(Vector3 currentP, int x, int y)
    {
        Vector3 newP = new Vector3(27.7f * x, 5.6f + (21.3f * y), -10f);
        for (float t = 0f; t < 1; t += Time.deltaTime)
        {
            mainCamera.transform.position = Vector3.Lerp(currentP, newP, t / 1);
            yield return 0;
        }
        mainCamera.transform.position = newP;
    }
    public void StartGame()
    {
        Player = Instantiate(PlayerPrefab);
        moveBottomLeft(mainCamera.transform.position, 0, 0);
        LivesT.text = "Lives: " + lives.ToString();

        PanelInfo.transform.GetChild(0).gameObject.SetActive(false);
        PanelInfo.SetActive(false);
    }
    public void EndGame()
    {
        if (oldHighScore < CurrentScoreR)
        {
            PlayerPrefs.SetInt("HS", CurrentScoreR);
            HighScore(CurrentScoreR);
        }
        PanelInfo.transform.GetChild(1).gameObject.SetActive(true);
        PanelInfo.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
        PanelInfo.transform.GetChild(1).gameObject.SetActive(false);
    }

}
