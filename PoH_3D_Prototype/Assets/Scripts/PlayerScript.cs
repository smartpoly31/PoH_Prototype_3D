using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private bool waitingForClear = false; // 대화 텍스트를 비우기 위해 대기 중인지 여부를 나타내는 변수
    private float waitTimer = 0f; // 대기 시간을 추적하는 변수
    private const float waitDuration = 2f; // 대기할 시간(초)

    public TextMeshProUGUI dialogueText; // TextMeshPro 텍스트 컴포넌트에 대한 참조
    private int MrDialogueState = 0;
    private bool isNearMr = false;

    [SerializeField] private GameObject Mr; // Mr 오브젝트에 대한 참조
    public GameObject mrMoveScriptHolder; // MrMove 스크립트가 부착된 게임 오브젝트에 대한 참조

    public GameObject TakeX; // 인스턴스화할 프리팹에 대한 참조
    public GameObject PushX;
    private GameObject instantiatedPrefab; // 인스턴스화된 프리팹의 인스턴스를 저장
    private Transform canvasTransform; // 캔버스의 Transform을 저장
    private bool isNearKey = false; // 캐릭터가 Key 오브젝트 근처에 있는지 여부를 저장
    private bool isNearKey2 = false; // 캐릭터가 Key 오브젝트 근처에 있는지 여부를 저장
    private bool isNearBox = false;

    public GameObject RedKey; // 활성화/비활성화할 키 오브젝트를 저장
    public bool GRedKey = false; // RedKey 변수

    public GameObject GreenKey; // 활성화/비활성화할 녹색 키 오브젝트를 저장
    public bool GGreenKey = false; // GreenKey 변수

    public DialogScript dialogScript;
    void Start()
    {
        GRedKey = false;
        GGreenKey = false;
        // 씬에서 Canvas 오브젝트를 찾아서 저장합니다. (캔버스가 확실히 하나만 있는 경우)
        canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    void Update()
    {
        //시간 대기
        if (waitingForClear)
        {
            waitTimer += Time.deltaTime; // 경과한 시간 누적

            // 대기 시간이 지나면 대화 텍스트를 비움
            if (waitTimer >= waitDuration)
            {
                dialogueText.text = ""; // 대화 텍스트 비우기
                waitingForClear = false; // 대기 종료
                waitTimer = 0f; // 타이머 초기화
            }
        }


        // 캐릭터가 Mr 근처에 있고, X 키를 누르면
        if (isNearMr && Input.GetKeyDown(KeyCode.X))
        {
            ProceedDialogue(); // 대화를 진행합니다.
        }

        // 캐릭터가 Key 오브젝트 근처에 있고, 'X' 키를 누르면 메시지 출력
        if (isNearKey && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("I got the Red key!");
            // 여기에 키를 얻었을 때의 추가 로직을 구현할 수 있습니다.
            // 예: 키 오브젝트를 비활성화하거나, 인벤토리에 키 추가 등
            // 키 오브젝트를 비활성화합니다.
            if (RedKey != null && isNearKey) // 키 오브젝트가 근처에 있는지 확인합니다.
            {
                GRedKey = true;
                dialogScript.GKey(); // DialogScript의 인스턴스를 참조해야 합니다.
                if (instantiatedPrefab != null)
                {
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;
                }
                RedKey.SetActive(false);
                isNearKey = false;
            }
        }

        if (isNearKey2 && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("I got the Green key!");
            // 여기에 키를 얻었을 때의 추가 로직을 구현할 수 있습니다.
            // 예: 키 오브젝트를 비활성화하거나, 인벤토리에 키 추가 등
            // 키 오브젝트를 비활성화합니다.
            if (GreenKey != null && isNearKey2) // 키 오브젝트가 근처에 있는지 확인합니다.
            {
                GGreenKey = true;
                dialogScript.GKey(); // DialogScript의 인스턴스를 참조해야 합니다.
                if (instantiatedPrefab != null)
                {
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;
                }
                GreenKey.SetActive(false);
                isNearKey2 = false;
            }
        }

        if (isNearBox && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("AHH");
            // 여기에 키를 얻었을 때의 추가 로직을 구현할 수 있습니다.
            // 예: 키 오브젝트를 비활성화하거나, 인벤토리에 키 추가 등
            // 키 오브젝트를 비활성화합니다.
            if (isNearBox) // 키 오브젝트가 근처에 있는지 확인합니다.
            {
                dialogScript.BoxInteraction(); // DialogScript의 인스턴스를 참조해야 합니다.
                if (instantiatedPrefab != null)
                {
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;
                }
                isNearBox = false;
            }
        }
    }

    // 트리거 영역에 진입할 때 호출됨
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key")) // 'Key' 태그를 가진 오브젝트와의 충돌 감지
        {
            if (instantiatedPrefab == null && canvasTransform != null) // 아직 인스턴스화되지 않았고, 캔버스가 설정되었다면
            {
                // 프리팹을 Canvas의 자식으로 인스턴스화합니다.
                instantiatedPrefab = Instantiate(TakeX, canvasTransform);
                // 위치 및 스케일 조정이 필요할 수 있습니다.
                //instantiatedPrefab.transform.localPosition = Vector3.zero; // 필요에 따라 위치 조정
                //instantiatedPrefab.transform.localScale = Vector3.one; // 스케일을 기본값으로 설정
            }
            isNearKey = true;
        }

        if (other.gameObject.CompareTag("Key2")) // 'Key2' 태그를 가진 오브젝트와의 충돌 감지
        {
            if (instantiatedPrefab == null && canvasTransform != null) // 아직 인스턴스화되지 않았고, 캔버스가 설정되었다면
            {
                // 프리팹을 Canvas의 자식으로 인스턴스화합니다.
                instantiatedPrefab = Instantiate(TakeX, canvasTransform);
                // 위치 및 스케일 조정이 필요할 수 있습니다.
                //instantiatedPrefab.transform.localPosition = Vector3.zero; // 필요에 따라 위치 조정
                //instantiatedPrefab.transform.localScale = Vector3.one; // 스케일을 기본값으로 설정
            }
            isNearKey2 = true;
        }

        if (other.gameObject.CompareTag("Box")) // 'Box' 태그를 가진 오브젝트와의 충돌 감지
        {
            if (instantiatedPrefab == null && canvasTransform != null) // 아직 인스턴스화되지 않았고, 캔버스가 설정되었다면
            {
                // 프리팹을 Canvas의 자식으로 인스턴스화합니다.
                instantiatedPrefab = Instantiate(PushX, canvasTransform);
                // 위치 및 스케일 조정이 필요할 수 있습니다.
                //instantiatedPrefab.transform.localPosition = Vector3.zero; // 필요에 따라 위치 조정
                //instantiatedPrefab.transform.localScale = Vector3.one; // 스케일을 기본값으로 설정
            }
            isNearBox = true;
        }

        if (other.gameObject.name == "Mr")
        {
            isNearMr = true;
            MrDialogueState = 0; // 대화를 처음부터 시작
            ProceedDialogue(); // 즉시 첫 번째 메시지를 표시
        }
    }

    // 트리거 영역에서 벗어날 때 호출됨
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            if (instantiatedPrefab != null) // 인스턴스화된 프리팹이 있다면
            {
                // 인스턴스화된 프리팹 삭제
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
            isNearKey = false;
        }

        if (other.gameObject.CompareTag("Key2"))
        {
            if (instantiatedPrefab != null) // 인스턴스화된 프리팹이 있다면
            {
                // 인스턴스화된 프리팹 삭제
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
            isNearKey2 = false;
        }

        if (other.gameObject.CompareTag("Box"))
        {
            if (instantiatedPrefab != null) // 인스턴스화된 프리팹이 있다면
            {
                // 인스턴스화된 프리팹 삭제
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
            isNearBox = false;
        }

        if (other.gameObject.name == "Mr")
        {
            isNearMr = false;
            dialogueText.text = ""; // 대화 텍스트를 비웁니다
        }
    }

    private void ProceedDialogue()
    {
        string[] dialogues = new string[] {
            "Hello!",
            "Wonderful day, kid.",
            "Get away",
            "Okay.."
        };

        // 현재 대화 상태에 맞는 텍스트를 표시
        dialogueText.text = dialogues[MrDialogueState];


        // 대화 상태 업데이트 전에 현재 상태 체크
        if (MrDialogueState == 3) // 마지막 대화가 끝났을 때
        {
            Mr.GetComponent<MrMove>().StartMoving(); // Mr 오브젝트의 이동 시작
            isNearMr = false;
            Mr.GetComponent<BoxCollider>().enabled = false; // BoxCollider 비활성화
            waitingForClear = true;
        }
        // 다음 대화 상태로 업데이트
        MrDialogueState = (MrDialogueState + 1) % dialogues.Length;
    }
}