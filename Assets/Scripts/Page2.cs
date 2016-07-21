using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

public class Page2 : MonoBehaviour {
        
    public List<GameObject> bottom1_1;
    public List<GameObject> bottom2_1;
    
    public List<Vector2> listPoint;

    
    //public Button shouzhi;
    
    /// <summary>
    /// 进度动画
    /// </summary>
    private int currentTotal = 0;

    public GameObject mainNavselect;

    private bool isStartPlay = false;

    public bool isForward = false;
    
    public List<GameObject> navBtnList;
    public GameObject layer;

    /// <summary>
    /// scrollUV
    /// </summary>
    public GameObject srollUV;
    public GameObject shouzhiBtn;

    /// <summary>
    /// SceneLink
    /// </summary>
    public GameObject sceneLink;
    /// <summary>
    /// ScreenInfo
    /// </summary>
    public GameObject sceneInfo;
    /// <summary>
    /// pointGroup
    /// </summary>
    public List<GameObject> pointClickList;
    public List<GameObject> pointGroup;
    public GameObject pointLayer;
    public GameObject mainLayer;
    //当前所在的场景
    public int currentSceneIndex = 1;
    // Use this for initialization
    void Start()
    {
        GameObject btnMain360 = GameObject.Find("btnMain360");
        Button btnObj = btnMain360.GetComponent<Button>();
        btnObj.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().handler360NeiShi();
        });

        for (int i = 0; i < navBtnList.Count; i++)
        {
            GameObject obj = navBtnList[i];
            Button btn = obj.GetComponent<Button>();
            btn.onClick.AddListener(delegate()
            {
                int index = int.Parse(btn.name.Split('_')[1]);
                setCurrentTotal(index);

                if (getStartPlay() == true)
                {
                        return;
                }
                changeTab(index - 1, true, true);
            });
        }

        RectTransform rect = mainNavselect.GetComponent<RectTransform>();
        rect.transform.localPosition = new Vector3(listPoint[0].x, listPoint[0].y, 0);
        
        for (int i = 0; i < bottom1_1.Count; i++)
        {
            if (i == 0)
            {
                bottom1_1[i].SetActive(false);
                bottom2_1[i].SetActive(true);
            }
            else
            {
                bottom1_1[i].SetActive(true);
                bottom2_1[i].SetActive(false);
            }
        }
        sceneInfo.SetActive(false);
    }

    void OnEnable()
    {
        listPoint = new List<Vector2>();                                                                                                                                  

        for(int i = 0; i < 7;i++)
        {
            listPoint.Add(new Vector2(-535f + (i * 140) , 0));
        }
        setCurrentTotal(0);
        initPointGroup();
        changeNavigation(0, false);
    }

    public void onClickHandlerDetailsImg(GameObject sender)
    {
        string parentName = sender.transform.parent.name;
        sceneInfo.SetActive(true);
        SceneInfo scene = sceneInfo.GetComponent<SceneInfo>();
        string srcStr = parentName.Split('_')[0] + "_" + sender.name.Split('_')[1];
        scene.OnLoad(srcStr);
    }

    public void setVisibleFlase()
    {
        ImageAlpha iAlpha = this.gameObject.GetComponent<ImageAlpha>();
        iAlpha.AlphaOnFalse();
    }

    public void changeTab(int index,bool _isForward,bool isGotoAndStop = false)
    {
        Debug.Log("changeTab  "+index +" isForward  "+_isForward+" isGotoAndStop  "+isGotoAndStop);
        Page6 page6 = SceneMgr.getInstance().sceneList[5].GetComponent<Page6>();
        if (index >= page6.sceneImagesList.Count)
                return;
        isForward = _isForward;        
        SceneMgr.getInstance().sceneList[5].SetActive(true);
        SceneMgr.getInstance().sceneList[5].transform.localScale = new Vector3(1,1,1);
        SceneMgr.getInstance().sceneList[5].transform.localPosition = new Vector3(0f, 0f, 0f);

        changeNavigation(index, _isForward);
        page6.isForward = isForward;
        if(isGotoAndStop == true)//点击对应按钮，直接跳到指定页面
        {
            page6.gotoAndStop(currentSceneIndex);
        }else
		    page6.playScene(index);
        if (currentSceneIndex > 0)
            shouzhiBtn.SetActive(false);
        else
            shouzhiBtn.SetActive(true);
    }

    void changeNavigation(int index,bool isForward)
    {
        Debug.Log("changeNavigation  "+index+" isForward  "+isForward.ToString());
        int navIndex = index;
        if (isForward)
            navIndex = index + 1;
        else
            navIndex = index;
        currentSceneIndex = navIndex;

        if(navIndex < listPoint.Count)
        {
                RectTransform rect = mainNavselect.GetComponent<RectTransform>();
                iTween.MoveTo(mainNavselect, iTween.Hash("time", 1.0f, "easeType", iTween.EaseType.easeOutExpo, "islocal", true,
                "position", new Vector3(listPoint[navIndex].x, listPoint[navIndex].y, 0), "oncomplete", "onCompleteNavigatioin1",
                "oncompletetarget", gameObject));

                for (int i = 0; i < pointGroup.Count; i++)
                {     
                    pointGroup[i].SetActive(false);
                }
                Invoke("changePointGroup", 0.6f);
        }

        for (int i = 0; i < bottom1_1.Count; i++)
        {
            if (i == navIndex)
            {
                bottom1_1[i].SetActive(false);
                bottom2_1[i].SetActive(true);
            }
            else
            {
                bottom1_1[i].SetActive(true);
                bottom2_1[i].SetActive(false);
            }
        }
        
    }

    void Update () {
        if (SceneMgr.getInstance().sceneList[0].activeSelf == true)
            return;
		if (this.gameObject.activeSelf == false)
        {
                setStartPlay(false);
                return;
        }
        if (SceneMgr.getInstance().isPageClickByName("page2") == false)
        {
                return;         
        }
        if (Input.touchCount > 0)
        {
            Vector2 touch2 = Input.GetTouch(0).deltaPosition;
            float HorizontalX = touch2.x;
            float VerticalY = touch2.y;
            Debug.Log("Touch HorizontalX.x " + HorizontalX + " VerticalY.y = " + VerticalY + " currentTotal " + currentTotal);

            bool isHandle = handlerShouzhi();
            if (isHandle == false && SceneMgr.getInstance().isPageClickByName("PageSrollUV") == false) 
                    touchHandler(HorizontalX, 40f, VerticalY, 40f);
        }
        else if (Input.GetMouseButton(0))
        {
            float HorizontalX = Input.GetAxis("Mouse X");
            float VerticalY = Input.GetAxis("Mouse Y");

            bool isHandle = handlerShouzhi();
            if (isHandle == false && SceneMgr.getInstance().isPageClickByName("PageSrollUV") == false)
                    touchHandler(HorizontalX, 1f, VerticalY, 1f);
        }
	}

    public bool handlerShouzhi()
    {
        //当前是否在pageScrollUV状态
        if (shouzhiBtn.gameObject.activeSelf == false || currentSceneIndex != 0 ||SceneMgr.getInstance().isPageClickByName("PageSrollUV") == true)
                return false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point,Color.red,5);
            GameObject gameObj = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + gameObj.name);
            if (gameObj.tag == "shouzhi")
            {
                PageScrollUV pageScroll = SceneMgr.getInstance().scrollUV.GetComponent<PageScrollUV>();
                if (pageScroll.isComplete == false)
                {
                    pageScroll.initData();
                    SceneMgr.getInstance().scrollUV.SetActive(true);
                    SceneMgr.getInstance().setPageClick("PageSrollUV", true);
                    return true;
                }
            }
        }
        return false;
    }

    public void touchHandler(float HorizontalX, float offSetX, float VerticalY,float offSetY)
    {
        if (getStartPlay() == true) return;
        if (VerticalY > offSetY && currentSceneIndex == 2)
        {
            GameObject scene6 = SceneMgr.getInstance().sceneList[5];
            SceneMgr.getInstance().setPageClick("Page6", false);
            GameObject scene2_4 = SceneMgr.getInstance().loadScene("scene2_4", 6);
            scene2_4.SetActive(true);
            setStartPlay(true);
            SceneMgr.getInstance().playScene(scene2_4, true);
            return;
        }
        else if (VerticalY < -offSetY && currentSceneIndex == 2)
        {
            GameObject scene2_4 = SceneMgr.getInstance().getScene(6);
            if (scene2_4 == null) return;
            setStartPlay(true);
            SceneMgr.getInstance().playScene(scene2_4, false);
            return;
        }
        else if (VerticalY > offSetY && currentSceneIndex == 6)
        {
            GameObject scene2_5 = SceneMgr.getInstance().loadScene("scene2_5", 7);
            pointGroup[6].SetActive(true);
            setStartPlay(true);
            scene2_5.transform.localPosition = new Vector3(0, 0, 0);
            SceneMgr.getInstance().playScene(scene2_5, false);
            return;
        }

        if (currentSceneIndex == 2)//判断如果当前是切换到2_4，并且为可用，直接返回
        {
                pointGroup[2].SetActive(true);
                GameObject temp = SceneMgr.getInstance().getScene(6);
                if(temp != null && temp.activeSelf == true)
                    return;
        }

        if (currentSceneIndex == 6)//判断如果当前是切换到2_6，并且为可用，直接返回
        {
                GameObject temp = SceneMgr.getInstance().getScene(7);
                if(temp != null && temp.activeSelf == true)
                    return;
        }

        if (HorizontalX > offSetX)     //向前滑动
        {
            if (getCurrentTotal() > 5)
                return;
            setStartPlay(true);
            changeTab(currentTotal, true);
            setCurrentTotal(getCurrentTotal() + 1);
            return;
        }
        else if (HorizontalX < -offSetX)
        {
            Page6 page6 = SceneMgr.getInstance().sceneList[5].GetComponent<Page6>();
            if (page6.currentShowIndex == 0)
                return;
            setStartPlay(true);
            if (currentTotal > 0)   //判断是否已经滑动到起点
                setCurrentTotal(currentTotal - 1);
            else
                return;
            if (isForward == true)
                changeTab(getCurrentTotal(), false);
            else if (isForward == false)
            {
                changeTab(getCurrentTotal(), false);
            }
        }
    }

    public void setCurrentTotal(int total)
    {
        currentTotal = total;
    }

    public int getCurrentTotal()
    {
        return currentTotal;
    }

    public void initPointGroup()
    {
        for (int i = 0; i<pointGroup.Count; i++)
        {
            GameObject obj = pointGroup[i];
            Transform[] transforms = obj.GetComponentsInChildren<Transform>();
            foreach (Transform node in transforms)
            {
                int index = 0;
                Button btn = node.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.AddListener(delegate()
                    {
                        this.onClickHandlerDetailsImg(btn.gameObject);
                    });
                }
                index++;
            }
            obj.SetActive(false);
        }
    }

    public void changePointGroup()
    {
        GameObject scene= pointGroup[currentSceneIndex];   
        if(scene.activeSelf == false & currentSceneIndex != 2 && currentSceneIndex != 6 )
            scene.SetActive(true);
    }


    void OnDisable()
    {
        //SceneMgr.getInstance().scrollUV.SetActive(false);
    }

    public void setStartPlay(bool _isStartPlay)
    {
        if(_isStartPlay == false)
        {
            Debug.Log("isStartPlay");
        }
        isStartPlay = _isStartPlay;
    }

    public bool getStartPlay()
    {
        return isStartPlay;
    }
}
