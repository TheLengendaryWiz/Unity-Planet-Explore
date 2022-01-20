using UnityEngine;
using UnityEngine.UI;

public class LivesCounterUI : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = "Lives: "+ GameManger.RemainingLives;
    }
}