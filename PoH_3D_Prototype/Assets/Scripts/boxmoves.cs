using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxmoves : MonoBehaviour
{
    public GameObject puzzlePrefab; // ������ Puzzle01 �����տ� ���� ����
    public GameObject fakeBoxPrefab; // �ν��Ͻ�ȭ�� fakeBox �����տ� ���� ����

    public bool isKeyGoal = false; // Ű�� ��ǥ ��ġ�� �����ߴ��� ����
    private bool isMoving = false; // ���� �̵� ������ ����

    private Vector3 targetPosition; // �ڽ� ������Ʈ�� ��ǥ ��ġ
    private float moveSpeed = 5f; // �ڽ� �̵� �ӵ�

    private void Start()
    {
        targetPosition = transform.position; // �ʱ� ��ǥ ��ġ�� ���� ��ġ�� ����
    }

    private void Update()
    {
        if (isKeyGoal && !isMoving)
        {
            // �������� ���� �������� �ʾҴٸ� ������ ����
            if (puzzlePrefab != null)
            {
                Destroy(puzzlePrefab);
                puzzlePrefab = null;
                // ��ǥ ��ġ ������Ʈ
                targetPosition = new Vector3(175f, -6f, 13f);
                isMoving = true; // �̵� ����
            }
        }

        // �ڽ��� ��ǥ ��ġ�� �ε巴�� �̵���Ŵ
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // �̵� �Ϸ� üũ
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false; // �̵� �� ���� ����
                // �̵��� �������� fakeBox �ν��Ͻ�ȭ
                if (fakeBoxPrefab != null)
                {
                    Instantiate(fakeBoxPrefab, transform.position, Quaternion.identity);
                    // ���� ������Ʈ ��Ȱ��ȭ
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}