using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region UI Objects

    [SerializeField] private GameObject Wave;
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject HighScore;
    [SerializeField] private GameObject Lives;
    [SerializeField] private GameObject Resume;
    [SerializeField] private GameObject PlayGame;
    [SerializeField] private GameObject ExitGame;
    [SerializeField] private GameObject MainMenu;

    #endregion

    #region GameObjects

    public GameObject MeteorBig;
    public GameObject MeteorMid;
    public GameObject MeteorSmall;
    public GameObject UfoBig;
    public GameObject UfoSmall;
    public GameObject Player;

    public int Enemies
    {
        get { return _enemies; }
        set { _enemies = value; }
    }

    #endregion

    #region Private Fields

    private int _score;
    private int _highScore;
    private int _lives;
    private int _wave;
    private const int INCREASE_PER_WAVE = 3;
    private int _enemies;
    private int _meteors;

    #endregion

    #region Load and Start

    void Awake()
    {
        MainMenu.SetActive(true);
        _highScore = PlayerPrefs.GetInt("_highScore", 0);
        HighScore.GetComponent<Text>().text = $"HIGH SCORE= {_highScore}";
        PlayGame.GetComponent<Button>().onClick.AddListener(StartGame);
        Resume.GetComponent<Button>().onClick.AddListener(UnPause);
        Resume.SetActive(false);
        ExitGame.GetComponent<Button>().onClick.AddListener(ExitApp);
        Pause();
    }

    void LoadPrefabs()
    {
        MeteorBig = Resources.Load<GameObject>("Prefabs/Meteor_Big");
        MeteorMid = Resources.Load<GameObject>("Prefabs/Meteor_Mid");
        MeteorSmall = Resources.Load<GameObject>("Prefabs/Meteor_Small");
        UfoBig = Resources.Load<GameObject>("Prefabs/Ufo_Big");
        UfoSmall = Resources.Load<GameObject>("Prefabs/Ufo_Small");
        Player = Resources.Load<GameObject>("Prefabs/Player");
    }

    void StartGame()
    {
        ClearGame();
        LoadPrefabs();
        _score = 0;
        _lives = 3;
        _wave = 0;
        MainMenu.SetActive(false);
        Instantiate(Player, new Vector2(0, 0), transform.rotation);
        UnPause();
    }

    #endregion

    #region InGameMethods

    void SpawnEnemies()
    {
            _meteors = _wave * INCREASE_PER_WAVE;
            for (int i = 0; i < _meteors; i++)
            {
                float x = Random.Range(-8.5f, 8.5f);
                float y = Mathf.Sign(Random.Range(-1f, 1f))*4.5f;
                Instantiate(MeteorBig, new Vector2(x, y), transform.rotation);
                Enemies++;
            }

            if (_score > 2000)
            {
                for (int j = 0; j < (_score / 1500); j++)
                {
                    float x = Random.Range(-8.5f, 8.5f);
                    float y = Mathf.Sign(Random.Range(-1f, 1f));
                    Instantiate(UfoBig, new Vector2(x, y), transform.rotation);
                    Enemies++;
                }
            }

            if (_score > 5000)
            {
                for (int j = 0; j < (_score / 3000); j++)
                {
                    float x = Random.Range(-8.5f, 8.5f);
                    float y = Mathf.Sign(Random.Range(-1f, 1f));
                    Instantiate(UfoSmall, new Vector2(x, y), transform.rotation);
                    Enemies++;
                }
            }

    }
    

    public void GetScore(int score)
    {
        _score += score;
    }

    public void Die()
    {
        _lives--;
        if (_lives < 1)
        {
            GameOver();
        }
        else
        {
            Instantiate(Player, new Vector2(0, 0), transform.rotation);
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        MainMenu.SetActive(true);
    }

    void UnPause()
    {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
    }
    void GameOver()
    {
        Pause();
    }
    void ExitApp()
    {
        Application.Quit();
    }

    void ClearGame()
    {
        List<GameObject> units = new List<GameObject>();
        units.AddRange(GameObject.FindGameObjectsWithTag("UFO"));
        units.AddRange(GameObject.FindGameObjectsWithTag("Bullet"));
        units.AddRange(GameObject.FindGameObjectsWithTag("MBig"));
        units.AddRange(GameObject.FindGameObjectsWithTag("MMid"));
        units.AddRange(GameObject.FindGameObjectsWithTag("MSmall"));
        units.AddRange(GameObject.FindGameObjectsWithTag("UFObullet"));
        units.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        foreach (GameObject unit in units)
        {
            Destroy(unit);
        }

        _enemies = 0;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (MainMenu.activeInHierarchy == false)
            {
                Pause();
                Resume.SetActive(true);
            }

        if (_enemies == 0)
        {
            _wave++;
            SpawnEnemies();
        }

        Score.GetComponent<Text>().text = $"SCORE: {_score}";
        Lives.GetComponent<Text>().text = $"LIVES: {_lives}";
        Wave.GetComponent<Text>().text = $"WAVES: {_wave}";

        if (_score > _highScore)
        {
            HighScore.GetComponent<Text>().text = $"HIGHSCORE: {_highScore}";
        }
    }

    #endregion
}
