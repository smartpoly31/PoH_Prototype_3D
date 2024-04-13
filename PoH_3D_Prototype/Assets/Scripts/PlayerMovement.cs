using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float padding = 0.5f; // 캐릭터와 장애물 사이에 유지할 최소 거리

    public Material backMaterial; // 위로 이동할 때 사용할 메테리얼
    public Material leftMaterial; // 왼쪽으로 이동할 때 사용할 메테리얼
    public Material rightMaterial; // 오른쪽으로 이동할 때 사용할 메테리얼
    public Material defaultMaterial; // 기본 메테리얼 (아래로 이동할 때)

    private Renderer myRenderer; // Renderer 컴포넌트를 저장할 변수

    void Start()
    {
        transform.position = new Vector3(-5, -5.5f, 10);
        myRenderer = GetComponent<Renderer>(); // 시작할 때 Renderer 컴포넌트 참조
        myRenderer.material = defaultMaterial; // 기본 메테리얼로 설정
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

            if (!Physics.Raycast(transform.position, direction, out hit, distance + padding))
            {
                MoveAndSetMaterial(direction, distance);
            }
            else
            {
                float adjustedDistance = Mathf.Min(hit.distance - padding, distance);
                MoveAndSetMaterial(direction, adjustedDistance);
            }
        }
    }

    void MoveAndSetMaterial(Vector3 direction, float distance)
    {
        Vector3 newPosition = transform.position + direction * distance;
        if (newPosition.z < 7)
        {
            newPosition.z = 7;
        }

        transform.position = newPosition;

        // 이동 방향에 따라 메테리얼 변경
        
        if (direction.z < 0) // 아래로 이동
        {
            myRenderer.material = defaultMaterial;
        }
        else if (direction.x > 0) // 오른쪽으로 이동
        {
            myRenderer.material = rightMaterial;
        }

        else if (direction.z > 0) // 위로 이동
        {
            myRenderer.material = backMaterial;
        }
        else if (direction.x < 0) // 왼쪽으로 이동
        {
            myRenderer.material = leftMaterial;
        }
    }
}