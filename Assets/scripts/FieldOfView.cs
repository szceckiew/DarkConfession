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

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360f;
        origin = Vector3.zero;

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
