using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlink : MonoBehaviour
{
    [Header("눈을 뜰 수 있는 시간")]
    public float eyeOpenTime = 5f;
    [Header("강제로 감겼을 때 다시 뜰 수 있을때까지 걸리는 시간")]
    public float forceClosedTimer = 10f;

    public AudioSource walk;
    public AudioSource jump;
    public AudioSource slide;
    

    [HideInInspector]
    public bool eyeOpend;
    private bool forcedClose;
    private float eyetime;  // 눈을 열고닫는 타이머
    private float fctime;  // 강제로 눈을 감게 하는 시간 타이머

    //2020-04-20 이주호 작성
    public float Eyetime
    {
        get => eyetime;
        private set => eyetime = value;
    }
    public bool ForcedClose
    {
        get => forcedClose;
        private set => forcedClose = value;
    }
    public float FcTime
    {
        get => fctime;
        private set => fctime = value;
    }

    private SpriteRenderer eyeSprite;
    [HideInInspector]
    public Sprite eyeOpenSprite;
    [HideInInspector]
    public Sprite eyeClosedSprite;

    [HideInInspector]
    public bool holding;
    [HideInInspector]
    public bool sliding;
    [HideInInspector]
    public bool jumping;
    [HideInInspector]
    public bool walking;

    private Vector3 eyePos;
    private Vector3 eyeRevPos;

    private KeySetting keySetting;

    private void Awake()
    {
        eyeSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        eyeOpend = false;
        forcedClose = false;
        eyetime = 0f;
        fctime = forceClosedTimer;
        
        eyePos = eyeSprite.gameObject.transform.position - transform.position;
        eyeRevPos = new Vector3(-eyePos.x, eyePos.y, eyePos.z);
        holding = false;
        sliding = false;
        jumping = false;

        keySetting = FindObjectOfType<KeySetting>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!WorldController.Instance.getIsPause() && WorldController.Instance.playerCanMove)
        {
            if (Input.GetKeyDown(keySetting.UserKey[KeyAction.BLINK]))
                eyeOpend = !eyeOpend;
            eyeTimer();
        }
        eyeSpriteControl();

        playerSoundControl();
    }

    private void playerSoundControl()
    {
        if (holding || sliding)
        {
            if (!slide.isPlaying)
                slide.Play();
        }
        else
            slide.Stop();

        if (walking && !(jumping || holding || sliding))
        {
            if (!walk.isPlaying)
                walk.Play();
        }
        else
            walk.Stop();
    }

    private void eyeTimer()
    {
        if (eyeOpend)
            eyetime += Time.deltaTime;
        else
            eyetime -= Time.deltaTime;
        if (eyetime < 0f)
            eyetime = 0f;
            
            
        if (eyetime > eyeOpenTime)
            forcedClose = true;
        if (forcedClose)
        {
            eyeOpend = false;
            eyetime = 0f;
            fctime -= Time.deltaTime;
        }
        if (fctime < 0f)
        {
            forcedClose = false;
            fctime = forceClosedTimer;
        }

        if (!WorldController.Instance.doBlinkFunc)
        {
            forcedClose = false;
            eyetime = 0f;
            fctime = 10f;
        }
    }

    private void eyeSpriteControl()
    {
        if (eyeOpend)
            eyeSprite.sprite = eyeOpenSprite;
        else
            eyeSprite.sprite = eyeClosedSprite;

        if (holding)
            eyeSprite.gameObject.transform.localScale = new Vector3(-1f, 1f, 0);
        else
            eyeSprite.gameObject.transform.localScale = new Vector3(1f, 1f, 0);

        if (transform.localScale.x > 0f)
        {
            if (holding)
                eyeSprite.gameObject.transform.position = transform.position + eyePos + new Vector3(-0.25f, 0.1f, 0f);
            else if (sliding)
                eyeSprite.gameObject.transform.position = transform.position + eyePos + new Vector3(0f, 0.2f, 0f);
            else if (jumping)
                eyeSprite.gameObject.transform.position = transform.position + eyePos + new Vector3(-0.05f, 0f, 0f);
            else
                eyeSprite.gameObject.transform.position = transform.position + eyePos;
        }
        else
        {
            if (holding)
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos + new Vector3(0.25f, 0.1f, 0f);
            else if (sliding)
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos + new Vector3(0f, 0.2f, 0f);
            else if (jumping)
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos + new Vector3(0.05f, 0f, 0f);
            else
                eyeSprite.gameObject.transform.position = transform.position + eyeRevPos;
        }
    }
}
