using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float padding = 0.5f; // 캐릭터와 장애물 사이에 유지할 최소 거리


    void Start()
    {
        // 시작할 때 플레이어의 위치를 -5, -5.5, 10으로 설정
        transform.position = new Vector3(-5, -5.5f, 10);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // 실제 이동 거리 계산
            float distance = moveSpeed * Time.deltaTime;

            RaycastHit hit;

            // Raycast를 사용하여 이동 방향에 장애물이 있는지 확인
            if (!Physics.Raycast(transform.position, direction, out hit, distance + padding))
            {
                // 장애물이 없거나 충분히 멀리 있다면 이동 계산
                Vector3 newPosition = transform.position + direction * distance;

                // Z값이 7보다 작아지지 않도록 조정
                if (newPosition.z < 7)
                {
                    newPosition.z = 7; // Z값이 7 이하로 떨어지지 않도록 고정
                }

                // 조정된 위치로 플레이어 이동
                transform.position = newPosition;
            }
            else
            {
                // Raycast에 의해 감지된 장애물과의 거리를 기반으로 이동 거리 조정
                float adjustedDistance = Mathf.Min(hit.distance - padding, distance);
                Vector3 newPosition = transform.position + direction * adjustedDistance;

                // Z값이 7보다 작아지지 않도록 조정
                if (newPosition.z < 7)
                {
                    newPosition.z = 7; // Z값이 7 이하로 떨어지지 않도록 고정
                }

                // 조정된 위치로 플레이어 이동
                transform.position = newPosition;
            }
        }
    }
}