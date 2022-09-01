using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    // 월드 싱글톤화
    private static WorldController _instance;
    public static WorldController Instance { get { return _instance; } }

    // 플레이어 인스턴스
    private GameObject myPlayer;
    private PlayerBlink plBlink;
    [HideInInspector]
    public bool playerRestart;
    [HideInInspector]
    public bool playerCanMove;

    // 세이브 포인트 인스턴스
    public List<SavePoint> savePoints = new List<SavePoint>();
    [HideInInspector]
    public int saveSpot;
    [HideInInspector]
    public int hightestSpot = 0;

    [Header("눈 감기 매커니즘 실행 여부"), HideInInspector]
    public bool doBlinkFunc;
    [SerializeField, Header("맵의 발판들이 흐려지기 시작하는 시간")]
    private float startshadedTime = 1f;
    [SerializeField, Header("맵의 발판들이 완전히 흐려지는데 걸리는 시간")]
    private float shadedTimeTaken = 3f;
    [Header("UI 페이드인/아웃 효과에 사용될 시간")]
    public float fadingTime = 1f;

    private float time;

    // 일시정지 여부
    private bool isPause;
    // true시 암전
    private bool worldBlackOut;
    // 1이 불투명, 0이 투명
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
        // 환경설정 진입, 탈출
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
            EnterSetting();
        else if (Input.GetKeyDown(KeyCode.Escape) && isPause)
            ExitSetting();

        // 눈 깜빡임 관련
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
        // 눈이 감겨있으면
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
