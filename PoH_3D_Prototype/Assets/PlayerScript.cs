using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private bool waitingForClear = false; // ��ȭ �ؽ�Ʈ�� ���� ���� ��� ������ ���θ� ��Ÿ���� ����
    private float waitTimer = 0f; // ��� �ð��� �����ϴ� ����
    private const float waitDuration = 2f; // ����� �ð�(��)

    public TextMeshProUGUI dialogueText; // TextMeshPro �ؽ�Ʈ ������Ʈ�� ���� ����
    private int MrDialogueState = 0;
    private bool isNearMr = false;

    [SerializeField] private GameObject Mr; // Mr ������Ʈ�� ���� ����
    public GameObject mrMoveScriptHolder; // MrMove ��ũ��Ʈ�� ������ ���� ������Ʈ�� ���� ����

    public GameObject TakeX; // �ν��Ͻ�ȭ�� �����տ� ���� ����
    public GameObject PushX;
    private GameObject instantiatedPrefab; // �ν��Ͻ�ȭ�� �������� �ν��Ͻ��� ����
    private Transform canvasTransform; // ĵ������ Transform�� ����
    private bool isNearKey = false; // ĳ���Ͱ� Key ������Ʈ ��ó�� �ִ��� ���θ� ����
    private bool isNearKey2 = false; // ĳ���Ͱ� Key ������Ʈ ��ó�� �ִ��� ���θ� ����
    private bool isNearBox = false;

    public GameObject RedKey; // Ȱ��ȭ/��Ȱ��ȭ�� Ű ������Ʈ�� ����
    public bool GRedKey = false; // RedKey ����

    public GameObject GreenKey; // Ȱ��ȭ/��Ȱ��ȭ�� ��� Ű ������Ʈ�� ����
    public bool GGreenKey = false; // GreenKey ����

    public DialogScript dialogScript;
    void Start()
    {
        GRedKey = false;
        GGreenKey = false;
        // ������ Canvas ������Ʈ�� ã�Ƽ� �����մϴ�. (ĵ������ Ȯ���� �ϳ��� �ִ� ���)
        canvasTransform = FindObjectOfType<Canvas>().transform;
    }

    void Update()
    {
        //�ð� ���
        if (waitingForClear)
        {
            waitTimer += Time.deltaTime; // ����� �ð� ����

            // ��� �ð��� ������ ��ȭ �ؽ�Ʈ�� ���
            if (waitTimer >= waitDuration)
            {
                dialogueText.text = ""; // ��ȭ �ؽ�Ʈ ����
                waitingForClear = false; // ��� ����
                waitTimer = 0f; // Ÿ�̸� �ʱ�ȭ
            }
        }


        // ĳ���Ͱ� Mr ��ó�� �ְ�, X Ű�� ������
        if (isNearMr && Input.GetKeyDown(KeyCode.X))
        {
            ProceedDialogue(); // ��ȭ�� �����մϴ�.
        }

        // ĳ���Ͱ� Key ������Ʈ ��ó�� �ְ�, 'X' Ű�� ������ �޽��� ���
        if (isNearKey && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("I got the Red key!");
            // ���⿡ Ű�� ����� ���� �߰� ������ ������ �� �ֽ��ϴ�.
            // ��: Ű ������Ʈ�� ��Ȱ��ȭ�ϰų�, �κ��丮�� Ű �߰� ��
            // Ű ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            if (RedKey != null && isNearKey) // Ű ������Ʈ�� ��ó�� �ִ��� Ȯ���մϴ�.
            {
                GRedKey = true;
                dialogScript.GKey(); // DialogScript�� �ν��Ͻ��� �����ؾ� �մϴ�.
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
            // ���⿡ Ű�� ����� ���� �߰� ������ ������ �� �ֽ��ϴ�.
            // ��: Ű ������Ʈ�� ��Ȱ��ȭ�ϰų�, �κ��丮�� Ű �߰� ��
            // Ű ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            if (GreenKey != null && isNearKey2) // Ű ������Ʈ�� ��ó�� �ִ��� Ȯ���մϴ�.
            {
                GGreenKey = true;
                dialogScript.GKey(); // DialogScript�� �ν��Ͻ��� �����ؾ� �մϴ�.
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
            // ���⿡ Ű�� ����� ���� �߰� ������ ������ �� �ֽ��ϴ�.
            // ��: Ű ������Ʈ�� ��Ȱ��ȭ�ϰų�, �κ��丮�� Ű �߰� ��
            // Ű ������Ʈ�� ��Ȱ��ȭ�մϴ�.
            if (isNearBox) // Ű ������Ʈ�� ��ó�� �ִ��� Ȯ���մϴ�.
            {
                dialogScript.BoxInteraction(); // DialogScript�� �ν��Ͻ��� �����ؾ� �մϴ�.
                if (instantiatedPrefab != null)
                {
                    Destroy(instantiatedPrefab);
                    instantiatedPrefab = null;
                }
                isNearBox = false;
            }
        }
    }

    // Ʈ���� ������ ������ �� ȣ���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key")) // 'Key' �±׸� ���� ������Ʈ���� �浹 ����
        {
            if (instantiatedPrefab == null && canvasTransform != null) // ���� �ν��Ͻ�ȭ���� �ʾҰ�, ĵ������ �����Ǿ��ٸ�
            {
                // �������� Canvas�� �ڽ����� �ν��Ͻ�ȭ�մϴ�.
                instantiatedPrefab = Instantiate(TakeX, canvasTransform);
                // ��ġ �� ������ ������ �ʿ��� �� �ֽ��ϴ�.
                //instantiatedPrefab.transform.localPosition = Vector3.zero; // �ʿ信 ���� ��ġ ����
                //instantiatedPrefab.transform.localScale = Vector3.one; // �������� �⺻������ ����
            }
            isNearKey = true;
        }

        if (other.gameObject.CompareTag("Key2")) // 'Key2' �±׸� ���� ������Ʈ���� �浹 ����
        {
            if (instantiatedPrefab == null && canvasTransform != null) // ���� �ν��Ͻ�ȭ���� �ʾҰ�, ĵ������ �����Ǿ��ٸ�
            {
                // �������� Canvas�� �ڽ����� �ν��Ͻ�ȭ�մϴ�.
                instantiatedPrefab = Instantiate(TakeX, canvasTransform);
                // ��ġ �� ������ ������ �ʿ��� �� �ֽ��ϴ�.
                //instantiatedPrefab.transform.localPosition = Vector3.zero; // �ʿ信 ���� ��ġ ����
                //instantiatedPrefab.transform.localScale = Vector3.one; // �������� �⺻������ ����
            }
            isNearKey2 = true;
        }

        if (other.gameObject.CompareTag("Box")) // 'Box' �±׸� ���� ������Ʈ���� �浹 ����
        {
            if (instantiatedPrefab == null && canvasTransform != null) // ���� �ν��Ͻ�ȭ���� �ʾҰ�, ĵ������ �����Ǿ��ٸ�
            {
                // �������� Canvas�� �ڽ����� �ν��Ͻ�ȭ�մϴ�.
                instantiatedPrefab = Instantiate(PushX, canvasTransform);
                // ��ġ �� ������ ������ �ʿ��� �� �ֽ��ϴ�.
                //instantiatedPrefab.transform.localPosition = Vector3.zero; // �ʿ信 ���� ��ġ ����
                //instantiatedPrefab.transform.localScale = Vector3.one; // �������� �⺻������ ����
            }
            isNearBox = true;
        }

        if (other.gameObject.name == "Mr")
        {
            isNearMr = true;
            MrDialogueState = 0; // ��ȭ�� ó������ ����
            ProceedDialogue(); // ��� ù ��° �޽����� ǥ��
        }
    }

    // Ʈ���� �������� ��� �� ȣ���
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            if (instantiatedPrefab != null) // �ν��Ͻ�ȭ�� �������� �ִٸ�
            {
                // �ν��Ͻ�ȭ�� ������ ����
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
            isNearKey = false;
        }

        if (other.gameObject.CompareTag("Key2"))
        {
            if (instantiatedPrefab != null) // �ν��Ͻ�ȭ�� �������� �ִٸ�
            {
                // �ν��Ͻ�ȭ�� ������ ����
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
            isNearKey2 = false;
        }

        if (other.gameObject.CompareTag("Box"))
        {
            if (instantiatedPrefab != null) // �ν��Ͻ�ȭ�� �������� �ִٸ�
            {
                // �ν��Ͻ�ȭ�� ������ ����
                Destroy(instantiatedPrefab);
                instantiatedPrefab = null;
            }
            isNearBox = false;
        }

        if (other.gameObject.name == "Mr")
        {
            isNearMr = false;
            dialogueText.text = ""; // ��ȭ �ؽ�Ʈ�� ���ϴ�
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

        // ���� ��ȭ ���¿� �´� �ؽ�Ʈ�� ǥ��
        dialogueText.text = dialogues[MrDialogueState];


        // ��ȭ ���� ������Ʈ ���� ���� ���� üũ
        if (MrDialogueState == 3) // ������ ��ȭ�� ������ ��
        {
            Mr.GetComponent<MrMove>().StartMoving(); // Mr ������Ʈ�� �̵� ����
            isNearMr = false;
            Mr.GetComponent<BoxCollider>().enabled = false; // BoxCollider ��Ȱ��ȭ
            waitingForClear = true;
        }
        // ���� ��ȭ ���·� ������Ʈ
        MrDialogueState = (MrDialogueState + 1) % dialogues.Length;
    }
}