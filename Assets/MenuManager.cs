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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
