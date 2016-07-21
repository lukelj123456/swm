using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PageScrollUV : MonoBehaviour
{
    // Use this for initialization
    private Image spRender = null;
    public GameObject shouzhi;
    private float progressX = 0.25f;

    public bool isStartPlay = false;
    public bool isComplete = false;

    public Vector3 basePosition;
    public Vector2 baseScreenPosition;
    public Vector2 shouzhiScreenPosition;
    public Page2 page2;

    public bool isTouch;

    public bool isStartComplete = false;
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
        updateProgress();
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
        shouzhiScreenPosition = shouzhiRect.anchoredPosition;
    }
    // Update is called once per frame
    public void Update()
    {
        if (isComplete == true) return;
        isTouch = false;
        if ((Input.touchCount > 0 || Input.GetMouseButton(0)))
        {
            isTouch = true;
            updateProgress();
        }
        else if (isTouch == false && isStartComplete == true)
        {
                 hideScrollUV();
        }
    }

    public void updateProgress()
    {
        Vector3 position = Input.mousePosition;
        RectTransform shouzhiRect = shouzhi.GetComponent<RectTransform>();
        float positionY = (float)(position.x - Screen.width/2) *((float)Screen.height/(float)Screen.width);
        shouzhiRect.anchoredPosition = new Vector2(position.x - Screen.width/2, positionY);

        Debug.Log("shouzhi anchoredPosition x =" + shouzhiRect.anchoredPosition.x+ " y = " + shouzhiRect.anchoredPosition.y);
        Debug.Log("Input.mousePosition  "+Input.mousePosition.x+ " input.mouseButton Y "+Input.mousePosition.y);

        //Debug.Log("shouzhi local x =" + shouzhi.transform.localPosition.x+ " y = " + shouzhi.transform.localPosition.y);
        //Debug.Log("shouzhi position x =" + shouzhi.transform.position.x+ " y = " + shouzhi.transform.position.y);            

        progressX = position.x/Screen.width;
        if (progressX < 0)
            progressX = 0;

        if (progressX >= 0.98f)
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
            ImageAlpha imageAlpha = this.gameObject.GetComponent<ImageAlpha>();
            imageAlpha.AlphaOnFalse();
            shouzhi.SetActive(false);
            page2.mainLayer.SetActive(true);
            page2.pointLayer.SetActive(true);
            page2.sceneInfo.SetActive(false);
            Invoke("onCompleteActive", 1f);
        }
    }

    private Vector2 ScreenToGUIPoint(Vector2 v)
    {
        return new Vector2(v.x - Screen.width/2, v.y - Screen.height/2);
    }
    public void onCompleteActive()
    {
        isStartPlay = false;
        isComplete = false;

        RectTransform shouzhiRect = shouzhi.GetComponent<RectTransform>();
        shouzhiRect.anchoredPosition =  shouzhiScreenPosition;

        shouzhi.SetActive(true);
        SceneMgr.getInstance().setPageClick("page2", true);
        SceneMgr.getInstance().setPageClick("page6", true);
        SceneMgr.getInstance().setPageClick("PageSrollUV", false);
    }
}
