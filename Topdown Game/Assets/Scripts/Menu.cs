using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public AudioClip startSound;
    public int SFXdelay = 3;
    public Canvas instructionCanvas;
    public Canvas mainCanvas;

    private void Start()
    {
        instructionCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    public void HowToPlay()
    {
        instructionCanvas.enabled = true;
        mainCanvas.enabled = false;
    }

    public void DisableInstructions()
    {
        instructionCanvas.enabled = false;
        mainCanvas.enabled = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void StartFirstLevel()
    {
        StartCoroutine(LoadFirstLevel());
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        var currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex + 1);
    }

    public void RetryLevel()
    {
        var currentsceneindex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentsceneindex);
    }

    private IEnumerator LoadFirstLevel()
    {
        AudioSource.PlayClipAtPoint(startSound, Camera.main.transform.position);
        yield return new WaitForSeconds(SFXdelay);
        SceneManager.LoadScene(1);
    }
}
