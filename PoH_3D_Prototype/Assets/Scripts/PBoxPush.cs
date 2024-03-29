using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBoxPush : MonoBehaviour
{
    private bool isPlayerNear = false; // 플레이어가 박스와 충돌하고 있는지 여부

    private Vector3 targetPosition; // 이동할 목표 위치
    private bool isMoving = false; // 현재 이동 중인지 여부

    void Update()
    {
        // 플레이어가 박스와 충돌하고 있고 X 키를 누르면
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
        {
            // 박스의 현재 위치를 가져옵니다.
            Vector3 boxPosition = transform.position;

            // 박스의 x값을 175로 변경합니다.
            targetPosition = new Vector3(175f, boxPosition.y, boxPosition.z);
            isMoving = true; // 이동 시작
        }

        // 이동 중이면
        if (isMoving)
        {
            // 부드러운 이동을 위해 선형 보간 수행
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

            // 이동이 완료되면 이동 상태를 false로 변경
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    // 플레이어와 충돌한 오브젝트가 박스인지 확인합니다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // 플레이어가 박스와 충돌을 벗어났는지 확인합니다.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}