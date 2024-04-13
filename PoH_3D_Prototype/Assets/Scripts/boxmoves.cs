using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxmoves : MonoBehaviour
{
    public GameObject puzzlePrefab; // 삭제할 Puzzle01 프리팹에 대한 참조
    public GameObject fakeBoxPrefab; // 인스턴스화할 fakeBox 프리팹에 대한 참조

    public bool isKeyGoal = false; // 키가 목표 위치에 도달했는지 여부
    private bool isMoving = false; // 현재 이동 중인지 여부

    private Vector3 targetPosition; // 박스 오브젝트의 목표 위치
    private float moveSpeed = 5f; // 박스 이동 속도

    private AudioSource audioSource; // AudioSource 컴포넌트를 저장할 변수

    private void Start()
    {
        puzzlePrefab.SetActive(true);
        targetPosition = transform.position; // 초기 목표 위치를 현재 위치로 설정
        audioSource = GetComponent<AudioSource>(); // 시작 시 AudioSource 컴포넌트를 가져옴
    }

    private void Update()
    {
        if (isKeyGoal && !isMoving)
        {
            // 프리팹이 아직 삭제되지 않았다면 프리팹 삭제
            if (puzzlePrefab != null)
            {
                puzzlePrefab.SetActive(false);
                puzzlePrefab = null;
                // 목표 위치 업데이트
                targetPosition = new Vector3(175f, -6f, 13f);
                audioSource.Play(); // 드래그 시작 시 사운드 재생
                isMoving = true; // 이동 시작
            }
        }

        // 박스를 목표 위치로 부드럽게 이동시킴
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // 이동 완료 체크
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false; // 이동 중 상태 해제
                // 이동이 끝났으니 fakeBox 인스턴스화
                if (fakeBoxPrefab != null)
                {
                    Instantiate(fakeBoxPrefab, transform.position, Quaternion.identity);
                    // 현재 오브젝트 비활성화
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}