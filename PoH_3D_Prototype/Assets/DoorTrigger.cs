using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public PlayerScript playerScript; // PlayerScript�� ���� ����
    public DialogScript dialogScript; // DialogScript�� ���� ����

    private Transform canvasTransform;

    // ī�޶�� �÷��̾��� �� ��ġ�� �����մϴ�.
    private Vector3 CameraPosition1 = new Vector3(0, 6.5f, -9);
    private Vector3 CameraPosition2 = new Vector3(80, 6.5f, -9);
    private Vector3 CameraPosition3 = new Vector3(160, 6.5f, -9);
    private Vector3 CameraPosition4 = new Vector3(240, 6.5f, -9);
    private Vector3 PlayerPosition1A = new Vector3(16, -5.5f, 12);
    private Vector3 PlayerPosition2B = new Vector3(65, -5.5f, 12);
    private Vector3 PlayerPosition2C = new Vector3(70, -5.5f, 21);
    private Vector3 PlayerPosition3A = new Vector3(144, -5.5f, 12);


    private void OnTriggerEnter(Collider other)
    {
        // Ʈ���ſ� ���� ������Ʈ�� DoorA_R1���� Ȯ���մϴ�.
        if (other.gameObject.name == "DoorA_R1")
        {
            // ī�޶��� ��ġ�� �����մϴ�.
            Camera.main.transform.position = CameraPosition2;

            // �÷��̾��� ��ġ�� �����մϴ�.
            transform.position = PlayerPosition2B;
        }

        if (other.gameObject.name == "DoorB_R2")
        {
            // ī�޶��� ��ġ�� �����մϴ�.
            Camera.main.transform.position = CameraPosition1;

            // �÷��̾��� ��ġ�� �����մϴ�.
            transform.position = PlayerPosition1A;
        }

        if (other.gameObject.name == "DoorC_R2")
        {
            if (playerScript.GRedKey)
            {
                // ī�޶��� ��ġ�� �����մϴ�.
                Camera.main.transform.position = CameraPosition3;

                // �÷��̾��� ��ġ�� �����մϴ�.
                transform.position = PlayerPosition3A;
            }
            else
            {
                // GRedKey�� ������ ���, NeedKey �������� ĵ������ �ν��Ͻ�ȭ�ϰ� 3�� �Ŀ� �ı�
                dialogScript.ShowNeedKeyMessage(); // �� �޼ҵ�� DialogScript���� �����ؾ� �մϴ�.
            }
        }

        if (other.gameObject.name == "DoorA_R3")
        {
            // ī�޶��� ��ġ�� �����մϴ�.
            Camera.main.transform.position = CameraPosition2;

            // �÷��̾��� ��ġ�� �����մϴ�.
            transform.position = PlayerPosition2C;
        }

        if (other.gameObject.name == "DoorA_R2")
        {
            if (playerScript.GGreenKey)
            {
                // ī�޶��� ��ġ�� �����մϴ�.
                Camera.main.transform.position = CameraPosition4;

            }
            else
            {
                // GGreenKey�� ������ ���, NeedKey �������� ĵ������ �ν��Ͻ�ȭ�ϰ� 3�� �Ŀ� �ı�
                dialogScript.ShowNeedKeyMessage(); // �� �޼ҵ�� DialogScript���� �����ؾ� �մϴ�.
            }
        }
    }
}