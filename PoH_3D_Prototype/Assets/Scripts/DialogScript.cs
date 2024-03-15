using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript : MonoBehaviour
{
    public GameObject needKeyPrefab;
    public GameObject GetRedKey;
    public GameObject BoxMovement;
    private Transform canvasTransform;

    void Start()
    {
        // ������ Canvas ������Ʈ�� ã�Ƽ� �����մϴ�.
        canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    public void GKey()
    {
        if (GetRedKey != null && canvasTransform != null)
        {
            // �ؽ�Ʈ �������� Canvas�� �ڽ����� �ν��Ͻ�ȭ�մϴ�.
            GameObject messageInstance = Instantiate(GetRedKey, canvasTransform);
            // 3�� �Ŀ� �޽��� �ν��Ͻ��� �ı��մϴ�.
            Destroy(messageInstance, 3.0f);
        }
    }
    public void ShowNeedKeyMessage()
    {
        if (needKeyPrefab != null && canvasTransform != null)
        {
            GameObject messageInstance = Instantiate(needKeyPrefab, canvasTransform);
            Destroy(messageInstance, 3.0f); // 3�� �Ŀ� �޽��� �ν��Ͻ��� �ı��մϴ�.
        }
    }

    public void BoxInteraction()
    {
        if (BoxMovement != null && canvasTransform != null)
        {
            GameObject messageInstance = Instantiate(BoxMovement, canvasTransform);
            Destroy(messageInstance, 1.0f); // 1�� �Ŀ� �޽��� �ν��Ͻ��� �ı��մϴ�.
        }
    }
}