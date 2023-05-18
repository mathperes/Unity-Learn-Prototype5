using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject tittleScreen;
    public GameObject pausedScreen;
    
    public int lives;
    public bool isGameActive;

    private float spawnRate = 1.5f;
    private int score;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLives();
        PauseMenu();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }

    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        score = 0;
        lives = 3;
        isGameActive = true;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        tittleScreen.gameObject.SetActive(false);
    }

    public void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            Time.timeScale = 0;
            isGameActive = false;
            pausedScreen.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.P) && !isGameActive)
        {
            Time.timeScale = 1;
            isGameActive = true;
            pausedScreen.gameObject.SetActive(false);
        }
    }
}
