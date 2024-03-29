using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct DoorOpenedData
    {
        public float timeSinceLoad;
        public int Attempt;
        public Vector3 DoorPosition;
    }

    public PlayerScript playerScript; // PlayerScript에 대한 참조
    public DialogScript dialogScript; // DialogScript에 대한 참조
    private Transform canvasTransform;

    // 카메라와 플레이어의 새 위치를 정의합니다.
    private Vector3 CameraPosition1 = new Vector3(0, 1.2f, -9);
    private Vector3 CameraPosition2 = new Vector3(80, 1.2f, -9);
    private Vector3 CameraPosition3 = new Vector3(160, 1.2f, -9);
    private Vector3 CameraPosition4 = new Vector3(240, 1.2f, -9);
    private Vector3 PlayerPosition1A = new Vector3(16, -5.5f, 12);
    private Vector3 PlayerPosition2B = new Vector3(65, -5.5f, 12);
    private Vector3 PlayerPosition2C = new Vector3(70, -5.5f, 21);
    private Vector3 PlayerPosition3A = new Vector3(144, -5.5f, 12);
    private int door_open = 0;

    private void OnTriggerEnter(Collider other)
    {
        // 트리거에 닿은 오브젝트가 DoorA_R1인지 확인합니다.
        if (other.gameObject.name == "DoorA_R1")
        {
            door_open += 1;

            // 카메라의 위치를 변경합니다.
            Camera.main.transform.position = CameraPosition2;

            // 플레이어의 위치를 변경합니다.
            transform.position = PlayerPosition2B;

            DoorOpenedData doorOpenedData = new DoorOpenedData
            {
                timeSinceLoad = Time.timeSinceLevelLoad,
                Attempt = door_open,
                DoorPosition = PlayerPosition2B
            };
            TelemetryLogger.Log(this, "Door opened", doorOpenedData);
        }

        if (other.gameObject.name == "DoorB_R2")
        {
            door_open += 1;

            // 카메라의 위치를 변경합니다.
            Camera.main.transform.position = CameraPosition1;

            // 플레이어의 위치를 변경합니다.
            transform.position = PlayerPosition1A;

            DoorOpenedData doorOpenedData = new DoorOpenedData
            {
                timeSinceLoad = Time.timeSinceLevelLoad,
                Attempt = door_open,
                DoorPosition = PlayerPosition1A
            };
            TelemetryLogger.Log(this, "Door opened", doorOpenedData);
        }

        if (other.gameObject.name == "DoorC_R2")
        {
            if (playerScript.GRedKey)
            {
                door_open += 1;
                // 카메라의 위치를 변경합니다.
                Camera.main.transform.position = CameraPosition3;

                // 플레이어의 위치를 변경합니다.
                transform.position = PlayerPosition3A;

                DoorOpenedData doorOpenedData = new DoorOpenedData
                {
                    timeSinceLoad = Time.timeSinceLevelLoad,
                    Attempt = door_open,
                    DoorPosition = PlayerPosition3A
                };
                TelemetryLogger.Log(this, "Door opened", doorOpenedData);
            }
            else
            {
                TelemetryLogger.Log(this, "Attempt without key");
                // GRedKey가 거짓일 경우, NeedKey 프리팹을 캔버스에 인스턴스화하고 3초 후에 파괴
                dialogScript.ShowNeedKeyMessage(); // 이 메소드는 DialogScript에서 구현해야 합니다.
            }
        }

        if (other.gameObject.name == "DoorA_R3")
        {
            door_open += 1;

            // 카메라의 위치를 변경합니다.
            Camera.main.transform.position = CameraPosition2;

            // 플레이어의 위치를 변경합니다.
            transform.position = PlayerPosition2C;

            DoorOpenedData doorOpenedData = new DoorOpenedData
            {
                timeSinceLoad = Time.timeSinceLevelLoad,
                Attempt = door_open,
                DoorPosition = PlayerPosition2C
            };
            TelemetryLogger.Log(this, "Door opened", doorOpenedData);
        }

        if (other.gameObject.name == "DoorA_R2")
        {
            if (playerScript.GGreenKey)
            {
                door_open += 1;

                // 카메라의 위치를 변경합니다.
                Camera.main.transform.position = CameraPosition4;

                DoorOpenedData doorOpenedData = new DoorOpenedData
                {
                    timeSinceLoad = Time.timeSinceLevelLoad,
                    Attempt = door_open,
                    DoorPosition = PlayerPosition2C
                };
                TelemetryLogger.Log(this, "Door opened", doorOpenedData);
            }
            else
            {
                TelemetryLogger.Log(this, "Attempt without key");
                // GGreenKey가 거짓일 경우, NeedKey 프리팹을 캔버스에 인스턴스화하고 3초 후에 파괴
                dialogScript.ShowNeedKeyMessage(); // 이 메소드는 DialogScript에서 구현해야 합니다.
            }
        }
    }
}