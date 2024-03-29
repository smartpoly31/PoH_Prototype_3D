using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBoxPush : MonoBehaviour
{
    private bool isPlayerNear = false; // �÷��̾ �ڽ��� �浹�ϰ� �ִ��� ����

    private Vector3 targetPosition; // �̵��� ��ǥ ��ġ
    private bool isMoving = false; // ���� �̵� ������ ����

    void Update()
    {
        // �÷��̾ �ڽ��� �浹�ϰ� �ְ� X Ű�� ������
        if (isPlayerNear && Input.GetKeyDown(KeyCode.X))
        {
            // �ڽ��� ���� ��ġ�� �����ɴϴ�.
            Vector3 boxPosition = transform.position;

            // �ڽ��� x���� 175�� �����մϴ�.
            targetPosition = new Vector3(175f, boxPosition.y, boxPosition.z);
            isMoving = true; // �̵� ����
        }

        // �̵� ���̸�
        if (isMoving)
        {
            // �ε巯�� �̵��� ���� ���� ���� ����
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);

            // �̵��� �Ϸ�Ǹ� �̵� ���¸� false�� ����
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    // �÷��̾�� �浹�� ������Ʈ�� �ڽ����� Ȯ���մϴ�.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // �÷��̾ �ڽ��� �浹�� ������� Ȯ���մϴ�.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}