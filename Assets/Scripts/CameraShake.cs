using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Camera cam;
    float shakeAmt = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Shake(0.1f, 0.2f);
        }
    }
    private void Awake()
    {
        if (cam==null)
        {
            cam = Camera.main;
        }
    }
    public void Shake(float amt, float length)
    {
        shakeAmt = amt;
        InvokeRepeating("StartShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    void StartShake()
    {
        if (shakeAmt>0)
        {
            Vector3 camPos = cam.transform.position;
            float shakeX = Random.value * shakeAmt * 2 - shakeAmt;
            float shakeY = Random.value * shakeAmt * 2 - shakeAmt;
            camPos.x += shakeX;
            camPos.y += shakeY;
            cam.transform.position = camPos;
        }
    }

    // Update is called once per frame
    void StopShake()
    {
        CancelInvoke("StartShake");
        cam.transform.localPosition = Vector3.zero;
    }
}
