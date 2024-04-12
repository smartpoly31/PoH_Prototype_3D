using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // �ٸ� ������Ʈ�� �±װ� 'KeyKey'�� ���
        if (other.CompareTag("Key3"))
        {
            // boxmoves ��ũ��Ʈ�� �ν��Ͻ��� ã�Ƽ�
            boxmoves boxMovesScript = FindObjectOfType<boxmoves>();

            // �ش� ��ũ��Ʈ�� �����Ѵٸ� isKeyGoal�� true�� ����
            if (boxMovesScript != null)
            {
                boxMovesScript.isKeyGoal = true;
            }
        }
    }
}