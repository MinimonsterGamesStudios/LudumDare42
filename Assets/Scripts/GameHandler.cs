using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    public GameObject player;
    public GameObject MapGenerator;
    public GameObject uiCanvas;
    [HideInInspector]
    public GameHandler instance;

    // Use this for initialization
    void Awake()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        uiCanvas.SetActive(false);
        MapGenerator.SetActive(true);
        player.SetActive(true);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverWithDelay());
    }

    IEnumerator GameOverWithDelay()
    {
        GetComponent<AudioSource>().Play();
        player.GetComponent<RigibodyCharacter>().enabled = false;
        yield return new WaitForSeconds(3f);
        GroundBreaker.ResetTimeValues();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
