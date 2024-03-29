using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBoxPush : MonoBehaviour
{
    private bool isPlayerNear = false; // 플레이어가 박스와 충돌하고 있는지 여부
    public GameObject puzzlePrefab; // 생성할 Puzzle01 프리팹
    private GameObject instantiatedPuzzle = null; // 생성된 Puzzle01 프리팹의 인스턴스를 저장할 변수

    void Update()
    {
        // 플레이어가 박스와 충돌하고 있고 X 키를 누르면
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
        {
            // 이미 프리팹이 생성되어 있지 않다면 새로 생성
            if (instantiatedPuzzle == null)
            {
                instantiatedPuzzle = Instantiate(puzzlePrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
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

    // 플레이어가 박스와 충돌을 벗어났는지 확인하고, 생성된 프리팹을 삭제합니다.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // 생성된 프리팹이 있다면 삭제
            if (instantiatedPuzzle != null)
            {
                Destroy(instantiatedPuzzle);
                instantiatedPuzzle = null; // 참조도 null로 초기화
            }
        }
    }
}