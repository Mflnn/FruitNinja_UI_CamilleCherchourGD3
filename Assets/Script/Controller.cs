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
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMenuMusic();

    }

    //Change le spawn rate en fonction de la difficulté choisie
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
        PlayGameMusic();
    }

    //Met à jour l'UI quand le score augmente
     public void UpdateScore(int scoreToAdd)
    {
        score+= scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    //Fait apparaitre les prefabs en piochant aléatoirement dans l'index
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
        //réactive l'UI quand on perd la partie + affiche l'UI de gameOver
        isGameActive = false;
        GameOverPanel.SetActive(true);
        gameoverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Joue la musique du menu (qui est dans la ligne 0 du tableau)
    public void PlayMenuMusic()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
        Debug.Log ("menu music is playing");
    }

    //Joue la musique du jeu (qui est dans la ligne 1 du tableau)
    public void PlayGameMusic()
    {
        audioSource.clip = audioClips[1];
        audioSource.Play();
    }
}
