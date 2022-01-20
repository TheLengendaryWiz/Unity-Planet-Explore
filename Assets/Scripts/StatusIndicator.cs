using UnityEngine.UI;
using UnityEngine;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    RectTransform heathBar;
    [SerializeField]
    Text healthText;
    public void SetHealth(int _cur,int _max)
    {
        float value = (float)_cur / _max;
        heathBar.localScale = new Vector3(value,heathBar.localScale.y ,heathBar.localScale.z);
        healthText.text = _cur + "/" + _max + "HP";
    }
}