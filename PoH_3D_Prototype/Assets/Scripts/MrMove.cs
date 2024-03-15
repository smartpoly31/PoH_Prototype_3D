using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMove : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(5, -4, 23); // ���� ��ġ
    public Vector3 targetPosition = new Vector3(-4, -4, 23); // ��ǥ ��ġ
    private bool moveToTarget = false; // ��ǥ ��ġ�� �̵� ���� ����
    public float moveSpeed = 1.0f; // �̵� �ӵ�

    void Start()
    {
        transform.position = startPosition; // ���� ��ġ�� ����
    }

    void Update()
    {
        if (moveToTarget)
        {
            // ��ǥ ��ġ�� �ε巴�� �̵�
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        moveToTarget = true; // �̵� ����
    }
}