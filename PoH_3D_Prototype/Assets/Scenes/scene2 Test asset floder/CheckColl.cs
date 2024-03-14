using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckColl : MonoBehaviour
{
    public DragObj parentScript; // �θ� ��ũ��Ʈ�� ���� ����
    public string direction; // �� ������Ʈ�� �����ϴ� ���� ("up", "down", "right", "left")

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parentScript.SetCollision(direction, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        parentScript.SetCollision(direction, false);
    }
}