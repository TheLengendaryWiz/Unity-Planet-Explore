using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public int MoveSpeed=230;
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
        Destroy(gameObject, 1);
    }
}