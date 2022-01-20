using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Tiling : MonoBehaviour
{
    public int offsetX = 2;
    public bool HasRightBody = false;
    public bool HasLeftBody = false;
    public bool ReverseScale = false;
    float Spritewidth = 0f;
    Camera cam;
    Transform myTransform;
    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Spritewidth = renderer.sprite.bounds.size.x;
    }
    void Update()
    {
        if (!HasRightBody||!HasLeftBody)
        {
            float camHorizontalExtent = cam.orthographicSize * Screen.width / Screen.height;
            float edgeVisiblePosRight = (myTransform.position.x + Spritewidth / 2) - camHorizontalExtent;
            float edgeVisiblePosLeft = (myTransform.position.x - Spritewidth / 2) + camHorizontalExtent;
            if (cam.transform.position.x>= edgeVisiblePosRight-offsetX&&!HasRightBody)
            {
                MakeNewBody(1);
                HasRightBody = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePosLeft + offsetX && !HasLeftBody)
            {
                MakeNewBody(-1);
                HasLeftBody = true;
            }
        }
    }
    void MakeNewBody(int LeftOrRight)
    {
        Vector3 BodyBosition = new Vector3(myTransform.position.x+Spritewidth*LeftOrRight,myTransform.position.y,myTransform.position.z);
        Transform newBody = Instantiate(gameObject,BodyBosition,myTransform.rotation).transform;
        if (ReverseScale)
        {
            newBody.localScale = new Vector3(newBody.localScale.x * -1, newBody.localScale.y, newBody.localScale.z);
        }
        newBody.parent = myTransform.parent;
        if (LeftOrRight>0)
        {
            newBody.GetComponent<Tiling>().HasLeftBody = true;
        }
        else
        {
            newBody.GetComponent<Tiling>().HasRightBody = true;
        }
    }
}
