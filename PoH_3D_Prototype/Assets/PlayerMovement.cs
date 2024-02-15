using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float padding = 0.5f; // ĳ���Ϳ� ��ֹ� ���̿� ������ �ּ� �Ÿ�

    void Start()
    {
        // ������ �� �÷��̾��� ��ġ�� -5, -5.5, 10���� ����
        transform.position = new Vector3(-5, -5.5f, 10);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // ���� �̵� �Ÿ� ���
            float distance = moveSpeed * Time.deltaTime;

            RaycastHit hit;

            // Raycast�� ����Ͽ� �̵� ���⿡ ��ֹ��� �ִ��� Ȯ��
            if (!Physics.Raycast(transform.position, direction, out hit, distance + padding))
            {
                // ��ֹ��� ���ų� ����� �ָ� �ִٸ� �̵� ���
                Vector3 newPosition = transform.position + direction * distance;

                // Z���� 7���� �۾����� �ʵ��� ����
                if (newPosition.z < 7)
                {
                    newPosition.z = 7; // Z���� 7 ���Ϸ� �������� �ʵ��� ����
                }

                // ������ ��ġ�� �÷��̾� �̵�
                transform.position = newPosition;
            }
            else
            {
                // Raycast�� ���� ������ ��ֹ����� �Ÿ��� ������� �̵� �Ÿ� ����
                float adjustedDistance = Mathf.Min(hit.distance - padding, distance);
                Vector3 newPosition = transform.position + direction * adjustedDistance;

                // Z���� 7���� �۾����� �ʵ��� ����
                if (newPosition.z < 7)
                {
                    newPosition.z = 7; // Z���� 7 ���Ϸ� �������� �ʵ��� ����
                }

                // ������ ��ġ�� �÷��̾� �̵�
                transform.position = newPosition;
            }
        }
    }
}