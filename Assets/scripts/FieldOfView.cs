using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Mesh mesh;
    private float fov;
    private Vector3 origin;
    private float startingAngle = 0;

    private List<SpriteRenderer> enemyRenderers;
    public GameObject[] enemiess;

    private GameObject player;

    public LayerMask detectorLayerMask;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360f;
        origin = Vector3.zero;

        enemiess = GameObject.FindGameObjectsWithTag("Enemy");

        player = GameObject.FindGameObjectWithTag("Player");

        //foreach (GameObject enemy in enemies)
        //{
        //    SpriteRenderer sr = enemy.GetComponent<SpriteRenderer>();
        //    enemyRenderers.Add(sr);
        //}

        detectorLayerMask |= (1 << 7);
        detectorLayerMask |= (1 << 9);
        detectorLayerMask |= (1 << 13);
    }

    private void LateUpdate()
    {
        
        int rayCount = 250;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 10f;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;


        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);

            if (raycastHit2D.collider == null)
            {
                // no hit
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            } else
            {
                //hit
                vertex = raycastHit2D.point;
                //vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }


            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            ++vertexIndex;

            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    
        foreach (GameObject enemy in enemiess)
        {
            RaycastHit2D enemyHit = Physics2D.Raycast(player.transform.position, (enemy.transform.position - player.transform.position).normalized, viewDistance, detectorLayerMask);
            
            if (enemyHit.collider.CompareTag("Enemy"))
            {
                enemy.GetComponent<SpriteRenderer>().enabled = true;
                Debug.Log("aaa");
            }
            else
            {
                enemy.GetComponent<SpriteRenderer>().enabled = false;
                Debug.Log("bbb");
            }
            Debug.Log(enemyHit.rigidbody);

            Debug.DrawRay(player.transform.position, enemy.transform.position);
        }

    }




    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad),Mathf.Sin(angleRad));
    }

    public void SetOrigin(Vector3 origin)
    {
       this.origin = origin;
    }

}
