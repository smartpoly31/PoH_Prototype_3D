using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrMove : MonoBehaviour
{
    public Vector3 startPosition = new Vector3(5, -4, 23); // 시작 위치
    public Vector3 targetPosition = new Vector3(-4, -4, 23); // 목표 위치
    private bool moveToTarget = false; // 목표 위치로 이동 시작 여부
    public float moveSpeed = 1.0f; // 이동 속도

    void Start()
    {
        transform.position = startPosition; // 시작 위치로 설정
    }

    void Update()
    {
        if (moveToTarget)
        {
            // 목표 위치로 부드럽게 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        moveToTarget = true; // 이동 시작
    }
}