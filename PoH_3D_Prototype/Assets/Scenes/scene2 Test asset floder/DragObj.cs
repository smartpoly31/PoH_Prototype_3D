using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Camera cam;
    private float moveDistance = 1.0f; // 한 번에 이동할 거리

    // 각 방향으로의 충돌 상태를 저장하는 변수
    private bool collisionUp = false;
    private bool collisionDown = false;
    private bool collisionRight = false;
    private bool collisionLeft = false;

    private void Start()
    {
        cam = Camera.main; // 메인 카메라를 찾습니다.
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPos = GetMouseWorldPos();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 dragEndPos = GetMouseWorldPos();
            MoveInDirection(dragStartPos, dragEndPos);
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    private void MoveInDirection(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 이동 방향에 따른 충돌 검사 및 이동 실행
        if (angle < 45f && angle > -45f && !collisionRight)
        {
            transform.position += Vector3.right * moveDistance;
        }
        else if (angle < 135f && angle >= 45f && !collisionUp)
        {
            transform.position += Vector3.up * moveDistance;
        }
        else if (angle <= -45f && angle > -135f && !collisionDown)
        {
            transform.position += Vector3.down * moveDistance;
        }
        else if ((angle > 135f || angle <= -135f) && !collisionLeft)
        {
            transform.position += Vector3.left * moveDistance;
        }
    }

    // 충돌 상태 설정 메소드
    public void SetCollision(string direction, bool state)
    {
        switch (direction)
        {
            case "up":
                collisionUp = state;
                break;
            case "down":
                collisionDown = state;
                break;
            case "right":
                collisionRight = state;
                break;
            case "left":
                collisionLeft = state;
                break;
        }
    }
}