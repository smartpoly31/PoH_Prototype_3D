using System.Collections;
using UnityEngine;

public class DragObj : MonoBehaviour
{
    private Vector3 dragStartPos;
    private Camera cam;
    public float moveDistanceVertical = 2.63f;
    public float moveDistanceHorizontal = 3.44f;
    private bool dragging = false;
    private bool canMove = true; // 이동 가능 상태를 나타내는 새 변수

    private AudioSource audioSource; // AudioSource 컴포넌트를 저장할 변수

    // 각 방향으로의 충돌 상태를 저장하는 변수
    private bool collisionUp = false;
    private bool collisionDown = false;
    private bool collisionRight = false;
    private bool collisionLeft = false;
    private bool collisionUp2 = false;
    private bool collisionDown2 = false;
    private bool collisionRight2 = false;
    private bool collisionLeft2 = false;

    private void Start()
    {
        cam = Camera.main;
        audioSource = GetComponent<AudioSource>(); // 시작 시 AudioSource 컴포넌트를 가져옴
    }

    private void OnMouseDown()
    {
        if (canMove) // 이동 가능 상태일 때만 드래그 시작
        {
            dragStartPos = GetMouseWorldPos();
            dragging = true;
            audioSource.Play(); // 드래그 시작 시 사운드 재생
        }
    }

    private void OnMouseUp()
    {
        if (dragging && canMove)
        {
            Vector3 dragEndPos = GetMouseWorldPos();
            MoveInDirection(dragStartPos, dragEndPos);
            StartCoroutine(WaitBeforeNextMove()); // 이동 후 대기 코루틴 시작
            dragging = false;
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
        if (!dragging || !canMove) return; // 드래그 중이거나 이동 불가능 상태라면 이동하지 않음

        Vector3 direction = (end - start).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if ((angle < 45f && angle > -45f) && !(collisionRight || collisionRight2))
        {
            transform.position += Vector3.right * moveDistanceHorizontal;
        }
        if ((angle < 135f && angle >= 45f) && !(collisionUp || collisionUp2))
        {
            transform.position += Vector3.up * moveDistanceVertical;
        }
        if ((angle <= -45f && angle > -135f) && !(collisionDown || collisionDown2))
        {
            transform.position += Vector3.down * moveDistanceVertical;
        }
        if ((angle > 135f || angle <= -135f) && !(collisionLeft || collisionLeft2))
        {
            transform.position += Vector3.left * moveDistanceHorizontal;
        }
    }

    public void SetCollision(string direction, bool state)
    {
        switch (direction)
        {
            case "left": collisionLeft = state; break;
            case "left2": collisionLeft2 = state; break;
            case "up": collisionUp = state; break;
            case "up2": collisionUp2 = state; break;
            case "down": collisionDown = state; break;
            case "down2": collisionDown2 = state; break;
            case "right": collisionRight = state; break;
            case "right2": collisionRight2 = state; break;
        }
    }

    IEnumerator WaitBeforeNextMove()
    {
        canMove = false; // 이동 불가능 상태로 설정
        yield return new WaitForSeconds(0.5f); // 0.5초 대기
        canMove = true; // 이동 가능 상태로 복원
    }
}