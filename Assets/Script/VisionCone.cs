using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    public float meshResolution;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;

    public AudioSource Audio2;

    public float timeRemaining = 4;
    public bool timerIsRunning = false;

    void Start()
    {
        viewMesh = new Mesh ();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;

        StartCoroutine ("FindTargetsWithDelay", 0.2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds (delay);
            FindVisibleTargets ();
        }
    }

    void deathMethod()
    {
        timerIsRunning = true;
        player.transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
    }

    void LateUpdate()
    {
        DrawFieldOfView ();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                Time.timeScale = 1;
            }
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear ();
        Collider[] targetsInViewRadius = Physics.OverlapSphere (transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle (transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance (transform.position, target.position);
                if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    // Respawn code
                    visibleTargets.Add (target);
                    Audio2.Play();
                    deathMethod();
                    // player.transform.position = respawnPoint.transform.position;
                    // Physics.SyncTransforms();
                    Debug.Log("ca marche sssaamere");
                }
            }
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3> ();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
            // Debug.DrawLine (transform.position, transform.position + DirFromAngle (angle, true) * viewRadius, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2) * 3];

        vertices [0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

            if (i < vertexCount - 2) 
            {
                triangles [i * 3] = 0;
                triangles [i * 3 + 1] = i + 1;
                triangles [i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear ();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals ();
    }

    ViewCastInfo ViewCast(float globalAngle) {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
        } else {
            return new ViewCastInfo (false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _dst, float _angle)
        {
            hit = _hit;
            point = _point;
            dst = _dst;
            angle = _angle;
        }
    }
}

// Debug.Log("ca marche sssaamere");