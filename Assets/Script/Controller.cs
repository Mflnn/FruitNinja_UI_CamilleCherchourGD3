using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    //public GameObject[] targetsArray;
    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public bool isGameActive;
    public Button RestartButton;
    public GameObject TitleScreen;
    public GameObject GameOverPanel;
    public GameObject ScoreUI;
    public GameObject Trail;

    public void Start()
    {
        PlayMusic();
    }
    public void StartGame (int difficulty)
    {
        isGameActive = true;
        score = 0;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        TitleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        ScoreUI.SetActive(true);
        Trail.SetActive(true);
    }
     public void UpdateScore(int scoreToAdd)
    {
        score+= scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
            Debug.Log("the random index name is :" + targets[index].name);
            Debug.Log("the random index nuber is :" + index);
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        GameOverPanel.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayMusic()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log ("music is playing");
    }
}
