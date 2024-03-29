using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObj : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Camera cam;
    public float moveDistanceVertical = 2.63f;
    public float moveDistanceHorizontal = 3.44f;
    private bool dragging = false;

    // 각 방향으로의 충돌 상태를 저장하는 변수
    private bool collisionUp = false;
    private bool collisionDown = false;
    private bool collisionRight = false;
    private bool collisionLeft = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        dragStartPos = GetMouseWorldPos();
        dragging = true; // 드래그 시작
    }

    private void OnMouseUp()
    {
        if (dragging)
        {
            Vector3 dragEndPos = GetMouseWorldPos();
            MoveInDirection(dragStartPos, dragEndPos);
            dragging = false; // 드래그 종료
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
        if (!dragging) return; // 드래그 중이 아니라면 이동하지 않음

        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 45f && angle > -45f && !collisionRight)
        {
            transform.position += Vector3.right * moveDistanceHorizontal;
        }
        else if (angle < 135f && angle >= 45f && !collisionUp)
        {
            transform.position += Vector3.up * moveDistanceVertical;
        }
        else if (angle <= -45f && angle > -135f && !collisionDown)
        {
            transform.position += Vector3.down * moveDistanceVertical;
        }
        else if ((angle > 135f || angle <= -135f) && !collisionLeft)
        {
            transform.position += Vector3.left * moveDistanceHorizontal;
        }
    }

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