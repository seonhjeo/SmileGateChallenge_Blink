using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // ���� �̱���ȭ
    private static WorldController _instance;
    public static WorldController Instance { get { return _instance; } }

    // �÷��̾� �ν��Ͻ�
    private GameObject myPlayer;
    private PlayerBlink plBlink;
    [HideInInspector]
    public bool playerRestart;
    [HideInInspector]
    public bool playerCanMove;

    // ���̺� ����Ʈ �ν��Ͻ�
    public List<SavePoint> savePoints = new List<SavePoint>();
    [HideInInspector]
    public int saveSpot;
    [HideInInspector]
    public int hightestSpot = 0;

    [Header("�� ���� ��Ŀ���� ���� ����"), HideInInspector]
    public bool doBlinkFunc;
    [SerializeField, Header("���� ���ǵ��� ������� �����ϴ� �ð�")]
    private float startshadedTime = 1f;
    [SerializeField, Header("���� ���ǵ��� ������ ������µ� �ɸ��� �ð�")]
    private float shadedTimeTaken = 3f;
    [Header("UI ���̵���/�ƿ� ȿ���� ���� �ð�")]
    public float fadingTime = 1f;

    private float time;

    // �Ͻ����� ����
    private bool isPause;
    // true�� ����
    private bool worldBlackOut;
    // 1�� ������, 0�� ����
    private float worldAlpha;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        UIManager.instance.SetActiveSettingUI(false);
        myPlayer = GameObject.Find("Player");
        plBlink = myPlayer.GetComponent<PlayerBlink>();
        playerRestart = false;
        playerCanMove = true;

        doBlinkFunc = true;
        worldBlackOut = !plBlink.eyeOpend;
        worldAlpha = 1f;

        isPause = false;
    }

    // Update is called once per frame
    private void Update()
    {
        // ȯ�漳�� ����, Ż��
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
            EnterSetting();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
            ExitSetting();

        // �� ������ ����
        worldBlackOut = !plBlink.eyeOpend;
        if (doBlinkFunc)
            changeWorldAlpha();
        else
        {
            worldBlackOut = false;
            worldAlpha = 1f;
            time = 0f;
        }
    }

    private void changeWorldAlpha()
    {
        // ���� ����������
        if (worldBlackOut)
        {
            time += Time.deltaTime;
            if (time > shadedTimeTaken + startshadedTime)
                worldAlpha = 0f;
            else if (time > startshadedTime)
                worldAlpha = Mathf.Lerp(1f, 0f, (time - startshadedTime) / shadedTimeTaken);
        }
        else
        {
            time = 0f;
            worldAlpha = 1f;
        }  
    }

    public bool getWorldBlackOut()
    {
        return worldBlackOut;
    }

    public bool getIsPause()
    {
        return isPause;
    }    

    public float getWorldAlpha()
    {
        return worldAlpha;
    }

    public void IncreaseShadeTime(float fDeltaTime)
    {
        shadedTimeTaken += fDeltaTime;
    }

    private void EnterSetting()
    {
        isPause = true;
        Time.timeScale = 0f;
        UIManager.instance.SetActiveSettingUI(true);
    }

    public void ExitSetting()
    {
        UIManager.instance.SetActiveSettingUI(false);
        Time.timeScale = 1f;
        isPause = false;
    }
}
