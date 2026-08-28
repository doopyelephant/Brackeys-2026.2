using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Play()
    {
        Debug.Log("play");
        SceneManager.LoadScene("WakeUp");
    }

    public void Restart()
    {
        Debug.Log("restart");
        Time.timeScale = 1f;
        if (GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>().hascompletedtutorial)
        {
            if (!GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>().skiptutorial)
            {
                GameObject.FindGameObjectWithTag("Player").transform.parent.GetComponent<Player>().skiptutorial = true;
            }

            var skipper = new GameObject("Skipper");
            skipper.AddComponent<TutorialSkipper>();
            DontDestroyOnLoad(skipper);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
