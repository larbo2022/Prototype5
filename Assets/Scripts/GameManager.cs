using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource gameSound;

    public Button restartButton;
    public GameObject titleScreen;
    
    private int score;
    public float spawnRate = 1.0f;
    public bool isGameActive;

    private void Start()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
          
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
       
        score += scoreToAdd;
        scoreText.text = "Score : " + score; 
        shootSound.Play();
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
        isGameActive = true;
        score = 0;
        spawnRate = (spawnRate * 2) / difficulty ;
        
        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        titleScreen.gameObject.SetActive(false);
    }

    
}
