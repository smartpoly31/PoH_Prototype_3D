using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float padding = 0.5f; // 캐릭터와 장애물 사이에 유지할 최소 거리

    public Texture upTexture;    // 위로 이동할 때의 텍스처
    public Texture downTexture;  // 아래로 이동할 때의 텍스처
    public Texture leftTexture;  // 왼쪽으로 이동할 때의 텍스처
    public Texture rightTexture; // 오른쪽으로 이동할 때의 텍스처

    private Renderer myRenderer; // Renderer 컴포넌트를 저장할 변수
    void Start()
    {
        transform.position = new Vector3(-5, -5.5f, 10);
        myRenderer = GetComponent<Renderer>(); // 시작할 때 Renderer 컴포넌트 참조
        myRenderer.material.mainTexture = downTexture; // 기본 텍스처로 설정
        myRenderer.material.renderQueue = 4000;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
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

                // 이동 방향에 따라 텍스처 변경
                ChangeTextureBasedOnDirection(direction);
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

                // 이동 방향에 따라 텍스처 변경
                ChangeTextureBasedOnDirection(direction);
            }
        }
    }

    private void ChangeTextureBasedOnDirection(Vector3 direction)
    {
        if (direction.z > 0) // 위로 이동
        {
            myRenderer.material.mainTexture = upTexture;
        }
        else if (direction.z < 0) // 아래로 이동
        {
            myRenderer.material.mainTexture = downTexture;
        }
        else if (direction.x > 0) // 오른쪽으로 이동
        {
            myRenderer.material.mainTexture = rightTexture;
        }
        else if (direction.x < 0) // 왼쪽으로 이동
        {
            myRenderer.material.mainTexture = leftTexture;
        }
    }
}