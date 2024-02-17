using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class EdgeColliderCreator : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;

    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;


    private bool goingMax;
    private void Awake()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();

        // �������� ������� ������
        Camera mainCamera = Camera.main;
        Vector2 bottomLeft = mainCamera.ViewportToWorldPoint(Vector3.zero);
        Vector2 topRight = mainCamera.ViewportToWorldPoint(Vector3.one);

        // ������� ������ ����� ��� EdgeCollider2D
        Vector2[] points = new Vector2[5];
        points[0] = new Vector2(bottomLeft.x -edgeCollider.edgeRadius, bottomLeft.y - edgeCollider.edgeRadius); // ����� ������ ����
        points[1] = new Vector2(bottomLeft.x - edgeCollider.edgeRadius, topRight.y + edgeCollider.edgeRadius);    // ����� ������� ����
        points[2] = new Vector2(topRight.x + edgeCollider.edgeRadius, topRight.y + edgeCollider.edgeRadius);      // ������ ������� ����
        points[3] = new Vector2(topRight.x + edgeCollider.edgeRadius, bottomLeft.y - edgeCollider.edgeRadius);    // ������ ������ ����
        points[4] = points[0]; // �������� ������

        // ������������� ����� � EdgeCollider2D
        edgeCollider.points = points;
        goingMax = true;
    }
    private void FixedUpdate()
    {
        if (goingMax)
        {
            edgeCollider.edgeRadius += Time.fixedDeltaTime / 1;
            if (edgeCollider.edgeRadius >= maxRadius)
                goingMax = false;
        }
        else
        {
            edgeCollider.edgeRadius -= Time.fixedDeltaTime / 1;
            if (edgeCollider.edgeRadius <= minRadius)
                goingMax = true;
        }
    }
}
