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
        // 씬에서 Canvas 오브젝트를 찾아서 저장합니다.
        canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    public void GKey()
    {
        if (GetRedKey != null && canvasTransform != null)
        {
            // 텍스트 프리팹을 Canvas의 자식으로 인스턴스화합니다.
            GameObject messageInstance = Instantiate(GetRedKey, canvasTransform);
            // 3초 후에 메시지 인스턴스를 파괴합니다.
            Destroy(messageInstance, 3.0f);
        }
    }
    public void ShowNeedKeyMessage()
    {
        if (needKeyPrefab != null && canvasTransform != null)
        {
            GameObject messageInstance = Instantiate(needKeyPrefab, canvasTransform);
            Destroy(messageInstance, 3.0f); // 3초 후에 메시지 인스턴스를 파괴합니다.
        }
    }

    public void BoxInteraction()
    {
        if (BoxMovement != null && canvasTransform != null)
        {
            GameObject messageInstance = Instantiate(BoxMovement, canvasTransform);
            Destroy(messageInstance, 1.0f); // 1초 후에 메시지 인스턴스를 파괴합니다.
        }
    }
}