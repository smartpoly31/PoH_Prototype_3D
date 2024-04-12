using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 다른 오브젝트의 태그가 'KeyKey'인 경우
        if (other.CompareTag("Key3"))
        {
            // boxmoves 스크립트의 인스턴스를 찾아서
            boxmoves boxMovesScript = FindObjectOfType<boxmoves>();

            // 해당 스크립트가 존재한다면 isKeyGoal을 true로 설정
            if (boxMovesScript != null)
            {
                boxMovesScript.isKeyGoal = true;
            }
        }
    }
}