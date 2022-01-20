using Pathfinding;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    #region #variables;
    public Transform target;
    public float updateRate = 2;
    Seeker seeker;
    Rigidbody2D rb;
    public Path path;
    public float Speed = 300f;
    public ForceMode2D fmode;
    [HideInInspector]
    public bool pathIsEnded = false;
    public float nextWayPointDistance = 3;
    int currentWayPoint;
    bool searchingForPlayer = false;
    #endregion
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        if (target==null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(searchForPlayer());
            }
            return;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        StartCoroutine(UpdatePath());

    }
    IEnumerator searchForPlayer()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if (sResult==null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(searchForPlayer());
        }
        else
        {
            target = sResult.transform;
            StartCoroutine(UpdatePath());
            searchingForPlayer = false;
            yield break;
        }
    }
    IEnumerator UpdatePath()
    {
        if (target==null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(searchForPlayer());
            }
            yield break;
        }
        seeker.StartPath(transform.position, target.position, OnPathComplete);
        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }
    private void FixedUpdate()
    {
        if (target==null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(searchForPlayer());
            }
            return;
        }
        if (path==null)
        {
            return;
        }
        if (currentWayPoint>=path.vectorPath.Count)
        {
            if (pathIsEnded)
            {
                return;
            }
            print("End of path");
            pathIsEnded = true;
            return;
        }
        pathIsEnded = false;
        Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;
        rb.AddForce(dir, fmode);
        float dist = Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]);
        if (dist<nextWayPointDistance)
        {
            currentWayPoint++;
            return;
        }
    }
}