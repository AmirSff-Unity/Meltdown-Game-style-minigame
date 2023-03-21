using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public ObstacleParent obstacleParent;
    public TMP_Text countdownText;
    public GameObject pauseDialog;
    public GameObject gameOverDialog;


    private void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        obstacleParent.Active = false;
        StartCoroutine(StartGameCountDown());
    }

    IEnumerator StartGameCountDown()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i + "";
            countdownText.GetComponent<Animator>().SetTrigger("CountDown");
            yield return new WaitForSeconds(1);
        }

        countdownText.gameObject.SetActive(false);
        obstacleParent.Active = true;
    }

    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 1 : 0;
        pauseDialog.SetActive(pause);
    }

    public void GameOver()
    {
        StartCoroutine(OpenGameOverMenu());
    }
    private IEnumerator OpenGameOverMenu()
    {
        yield return new WaitForSeconds(2);

        gameOverDialog.SetActive(true);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
