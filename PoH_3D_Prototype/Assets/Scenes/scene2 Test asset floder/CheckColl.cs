using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColl : MonoBehaviour
{
    public DragObj parentScript; // 부모 스크립트에 대한 참조
    public string direction; // 이 오브젝트가 감지하는 방향 ("up", "down", "right", "left")

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parentScript.SetCollision(direction, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parentScript.SetCollision(direction, false);
    }
}