using UnityEngine;
using UnityEngine.UI;

public class MoneyCounterUI : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        text.text = "money: " + GameManger.money;
    }
}