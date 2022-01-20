using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    AudioManager audioManager;
    private void Start()
    {
        audioManager = AudioManager.instance;   
    }
    public void OnMouseOver()
    {
        audioManager.PlaySound("ButtonHover");
    }
    public void Quit()
    {
        print("QUIT");
        audioManager.PlaySound("ButtonPress");
        Application.Quit();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}