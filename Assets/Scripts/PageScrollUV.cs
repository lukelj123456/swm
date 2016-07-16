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
    // Use this for initialization
    void Start()
    {
        spRender = this.gameObject.GetComponent<Image>();
        spRender.material.SetFloat("_ProgressX", 0);
        spRender.material.SetFloat("_ProgressY", 0);
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

        if ((Input.touchCount > 0 || Input.GetMouseButton(0)) && 
            SceneMgr.getInstance().isPageClickByName("PageSrollUV"))
        {
            isStartPlay = true;

            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 position = Input.mousePosition;

            if ((position.x /Screen.width)  < 0.2f || (position.y/Screen.height)< 0.2f)
                    return;

            float offSetX = position.x - baseScreenPosition.x;
            float offSetY = position.y - baseScreenPosition.y;
            if(offSetX > offSetY)
            {
                offSetY = position.x * ((float)Screen.height / (float)Screen.width)  - baseScreenPosition.y;
            }else
                offSetX =   position.y * ((float)Screen.width / (float)Screen.height)  - baseScreenPosition.x;

            RectTransform shouzhiRect = shouzhi.GetComponent<RectTransform>();
            shouzhiRect.anchoredPosition = new Vector2(shouzhiScreenPosition.x + offSetX,shouzhiScreenPosition.y + offSetY);

            //Debug.Log("shouzhi anchoredPosition x =" + shouzhiRect.anchoredPosition.x+ " y = " + shouzhiRect.anchoredPosition.y);
            //Debug.Log("shouzhi local x =" + shouzhi.transform.localPosition.x+ " y = " + shouzhi.transform.localPosition.y);
            //Debug.Log("shouzhi position x =" + shouzhi.transform.position.x+ " y = " + shouzhi.transform.position.y);            

            progressX = position.x/Screen.width;
            if (progressX < 0)
                    progressX = 0;
        }
        if(isStartPlay)
        {
            if (progressX >= 0.9f)
            {
                shouzhi.SetActive(false);
                ImageAlpha imageAlpha = this.gameObject.GetComponent<ImageAlpha>();
                imageAlpha.AlphaOnFalse();
                Invoke("onCompleteActive", 2f);
                return;
            }
            spRender.material.SetFloat("_ProgressX", progressX);
            spRender.material.SetFloat("_ProgressY", progressX);
        }
    }

    private Vector2 ScreenToGUIPoint(Vector2 v)
    {
        return new Vector2(v.x - Screen.width/2, v.y - Screen.height/2);
    }
    public void onCompleteActive()
    {
        isStartPlay = false;
        isComplete = true;
        
        SceneMgr.getInstance().setPageClick("page2", true);
        SceneMgr.getInstance().setPageClick("page6", true);
        SceneMgr.getInstance().setPageClick("PageSrollUV", false);
    }
}
