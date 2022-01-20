using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds;
    private float[] parallaxScales;
    public float smoothing=1;
    private Transform cam;
    private Vector3 previousCamPosition;
    private void Awake()
    {
        cam = Camera.main.transform;
    }
    void Start()
    {
        previousCamPosition = cam.position;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = -backgrounds[i].position.z;
        }
    }
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];
            float TargetBackGroundPosX = backgrounds[i].position.x + parallax;
            Vector3 BackgroundTargetPos = new Vector3(TargetBackGroundPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, BackgroundTargetPos, smoothing * Time.deltaTime);
        }
        previousCamPosition = cam.position;
    }
}
