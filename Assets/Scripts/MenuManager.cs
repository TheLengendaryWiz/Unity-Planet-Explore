using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //https://youtu.be/u4tmb4YiTDk?list=PLPV2KyIb3jR42oVBU6K2DIL6Y22Ry9J1c&t=1536
    AudioManager audioManager;
    public Transform storyMenu;
    private void Start()
    {
        audioManager = AudioManager.instance;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        AudioManager.instance.PlaySound("ButtonPress");
    }
    public void QuitGame()
    {
        AudioManager.instance.PlaySound("ButtonPress");
        storyMenu.gameObject.SetActive(true);
    }
    public void OnMouseOver()
    {
        audioManager.PlaySound("ButtonHover");
    }
    public void Back()
    {
        storyMenu.gameObject.SetActive(false);
    }
}