using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PageScrollUV : MonoBehaviour
{
    // Use this for initialization
    private Image spRender = null;
    public GameObject shouzhi;
    private float progressX = 0.25f;

    public bool isComplete = false;

    public Vector3 basePosition;
    public Vector2 baseScreenPosition;
    public Vector2 shouzhiScreenPosition;
    public Page2 page2;

    public bool isStartComplete = false;

    public enum platform
    {
        WIN,
        IOS,
        AND,
    }


    public platform platformType;

    // Use this for initialization
    void Start()
    {

    }
    void OnEnable()
    {
        page2 = SceneMgr.getInstance().sceneList[1].GetComponent<Page2>();
        spRender = this.gameObject.GetComponent<Image>();
        page2.mainLayer.SetActive(false);
        page2.pointLayer.SetActive(false);
        page2.sceneInfo.SetActive(false);
        
        spRender.material.SetFloat("_ProgressX", 0);
        spRender.material.SetFloat("_ProgressY", 0);
        isStartComplete = false;
        Invoke("StartComplete", 0.1f);
    }

    public void StartComplete()
    {
        isStartComplete = true;
    }

    public void initData()
    {
        basePosition = shouzhi.transform.position;
        baseScreenPosition = Input.mousePosition;
        RectTransform shouzhiRect = shouzhi.GetComponent<RectTransform>();
        shouzhiScreenPosition = new Vector2(-385f, -288f);
    }
    // Update is called once per frame
    public void Update()
    {
        if(platformType == platform.WIN)
        {
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0) == false)
            {
                hideScrollUV();
                return;
            }
            updateProgress();

        }else
        {
                if (Input.touchCount == 0)
                {
                    hideScrollUV();
                    return;
                }
                 if (Input.touchCount > 0)
                {
                    updateProgress();
                }
        }
    }

    public void updateProgress()
    {
        Vector3 position = Input.mousePosition;
        RectTransform shouzhiRect = shouzhi.GetComponent<RectTransform>();
        Canvas canvas = SceneMgr.getInstance().sceneList[1].GetComponent<Canvas>();
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos))
        {        
            pos.x = pos.x + 150;
            float positionY = (float)(pos.x - Screen.width/2) *((float)Screen.height/(float)Screen.width);
            shouzhiRect.anchoredPosition = new Vector2(pos.x - Screen.width/2, positionY);
        }

        Debug.Log("shouzhi anchoredPosition x =" + shouzhiRect.anchoredPosition.x+ " y = " + shouzhiRect.anchoredPosition.y);
        Debug.Log("Input.mousePosition  "+Input.mousePosition.x+ " input.mouseButton Y "+Input.mousePosition.y);

        progressX = position.x/Screen.width;
        if (progressX < 0)
            progressX = 0;

        if (progressX >= 1f)
        {
            hideScrollUV();
            return;
        }

        spRender.material.SetFloat("_ProgressX", progressX);
        spRender.material.SetFloat("_ProgressY", progressX);
    }

    public void hideScrollUV()
    {
        if(this.gameObject.activeSelf == true)
        {
            //ImageAlpha imageAlpha = this.gameObject.GetComponent<ImageAlpha>();
            //imageAlpha.AlphaOnFalse();
            shouzhi.SetActive(false);
            this.gameObject.SetActive(false);
            
            page2.mainLayer.SetActive(true);
            page2.pointLayer.SetActive(true);
            page2.sceneInfo.SetActive(false);
            Invoke("onCompleteActive", 0.3f);
            //onCompleteActive();
        }
    }

    void OnDisable()
    {
        Debug.Log(" PageScrollUV.Log");
    }

    private Vector2 ScreenToGUIPoint(Vector2 v)
    {
        return new Vector2(v.x - Screen.width/2, v.y - Screen.height/2);
    }
    public void onCompleteActive()
    {
        RectTransform shouzhiRect = shouzhi.GetComponent<RectTransform>();
        shouzhiRect.anchoredPosition =  shouzhiScreenPosition;
        shouzhi.SetActive(true);
        SceneMgr.getInstance().setPageClick("page2", true);
        SceneMgr.getInstance().setPageClick("page6", true);
        SceneMgr.getInstance().setPageClick("PageSrollUV", false);
    }
    void Awake()
    {
#if UNITY_ANDROID                         
        platformType = platform.AND;
#endif

#if UNITY_IPHONE
        platformType = platform.IOS;
#endif

#if UNITY_STANDALONE_WIN
        platformType = platform.WIN;
#endif
    }
}
