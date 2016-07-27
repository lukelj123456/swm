using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page2_5 : MonoBehaviour
{
    public GameObject sceneImage;

    public string[] sceneRange3;
    public int currentImageIndex = 0;
    //当前显示图片的位置
    public int currentShowIndex = 0;

    //当前滑动360图片位置
    public int current360ShowIndex = 0;
    public int startIndex = 0;
    public int endIndex = 0;
    public List<Image> sceneImagesList;

    public bool isForward = true;
    public int updateIndex = 0;

    public bool isHanderMouse = false;
    public int progressTotal = 0;
    public Vector3 scaleImage = new Vector3(1.3f, 1.3f, 1.0f);

    public List<Image> sceneAreaImagesList;
    private int startAreaIndex = 4;
    private int endAreaIndex = 35;

    public bool isPlayAreaImage = true;
    //判断是否回复到原位
    public bool isBackFrame = false;
    // Use this for initialization
    Page2 page2;
    void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 1);
		this.gameObject.SetActive (true);
        page2 = SceneMgr.getInstance().sceneList[1].GetComponent<Page2>();
    }

    void OnEnable()
    {
        current360ShowIndex = 0;
        sceneAreaImagesList = new List<Image>();
		SceneMgr.getInstance().setPageClick("page2_5", true);
        for (int i = 0; i < sceneImagesList.Count; i++)
        {
            GameObject obj = (sceneImagesList[i].transform.gameObject);
            obj.transform.localScale = scaleImage;
            obj.SetActive(true);
            SceneMgr.getInstance().setVisible(obj, false);
            if (i >= startAreaIndex && i <= endAreaIndex)
            {
                //将选中360的Image放入单独数据处理
                sceneAreaImagesList.Add(sceneImagesList[i]);
            }                
        }
        this.transform.localPosition = new Vector3(0, 0, 0);
        isForward = true;

        //SceneMgr.getInstance().setPageClick("page2", false);
        SceneMgr.getInstance().setCurrentScene(this.gameObject);
    }
    
    public void setVisibleFalse()
    {
        this.gameObject.SetActive(false);
    }

	public void setVisibleTrue()
	{
        setPlayAreaImage(false);
        SceneMgr.getInstance().setPage2Start(false);
	}

    public void playScene(int index)
    {
        current360ShowIndex = 0;
        sceneRange3 = new string[6];
        if (index < 0)
            index = 0;
        else if (index >= sceneRange3.Length)
            index = sceneRange3.Length - 1;
        sceneRange3[0] = "0_6";
        sceneRange3[1] = "7_36";
        string range = sceneRange3[index];

        Debug.Log("current frame start = " + range + " index  " + index);
        startIndex = int.Parse(range.Split('_')[0]);
        endIndex = int.Parse(range.Split('_')[1]);

        SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(setVisibleTrue);
        //展开
        StartCoroutine(SceneMgr.getInstance().playScene(startIndex, endIndex, sceneImagesList, startIndex, this.gameObject, callBack,0.001f));   
    }

    void OnDisable()
    {
        for (int i = 0; i < sceneImagesList.Count; i++)
        {
            GameObject obj = (sceneImagesList[i].transform.gameObject);
            obj.transform.localScale = scaleImage;
            SceneMgr.getInstance().setVisible(obj, false);
        }
		SceneMgr.getInstance().setPageClick("page2_5", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            hander360SelectImage();
        }else if(Input.touchCount > 0)
        {
            hander360SelectImage();
        }
    }

    public void backFrameComplete()
    {
        //SceneMgr.getInstance().setPageClick("page2", true);
        isBackFrame = false;
        this.gameObject.SetActive(false);
        Invoke("DestoryObject", 0.2f);        
    }

    public void DestoryObject()
    {
        page2.pointGroup[6].SetActive(false);
        Destroy(this.gameObject);
    }

    public void hander360SelectImage()
    {
        Vector3 position = Input.mousePosition;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if(Input.touchCount > 0)
        {
            Vector2 touch = Input.GetTouch(0).deltaPosition;
            mouseX = touch.x /500;
            mouseY = touch.y/100;
        } else
        {
            mouseX = mouseX/10;
        }

        if (isBackFrame == true)
                return;
        if (mouseY < -1f)//向下滑动后，播放复原序列帧动画
        {
            Debug.Log("page2_5 复原" + current360ShowIndex);
            SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(backFrameComplete);
            isBackFrame = true;
            if(current360ShowIndex < 15) //如果旋转的图片小于中轴，则倒播到第一帧
                    StartCoroutine(SceneMgr.getInstance().playScene(current360ShowIndex, 0, sceneImagesList, current360ShowIndex, this.gameObject,callBack,0.01f,false));
            else  //如果旋转的图片小于中轴，则继续播放到最后帧                                                                                                                                                                                                                                              
                    StartCoroutine(SceneMgr.getInstance().playScene(current360ShowIndex + startAreaIndex, sceneImagesList.Count - 1, sceneImagesList, current360ShowIndex + startAreaIndex, this.gameObject, callBack,0.02f));
            return;
        }
        int countTotal ;
        if(mouseX * (sceneAreaImagesList.Count) < 0)
            countTotal  = (int)Mathf.Ceil((-mouseX * sceneAreaImagesList.Count)/20);
        else
            countTotal  = (int)Mathf.Floor((-mouseX * sceneAreaImagesList.Count)/20);
        //Debug.Log("Mouth "+(mouseX * (sceneAreaImagesList.Count))+" mouseX  "+mouseX+" sceneAreaImageList " +
        //    sceneAreaImagesList.Count+" floor "+Mathf.Floor(mouseX * (sceneAreaImagesList.Count)));
        if (countTotal != 0 && getPlayAreaImage() == false)
        {
            int tempCount = countTotal + progressTotal;
            if (tempCount < 0)
                progressTotal = tempCount + sceneAreaImagesList.Count;
            else if (tempCount > sceneAreaImagesList.Count)
                progressTotal = tempCount - sceneAreaImagesList.Count;
            else
                progressTotal = tempCount;

            //显示锚点
            if (progressTotal >= sceneAreaImagesList.Count - 5)
            {
                Page2 page2 = SceneMgr.getInstance().sceneList[1].GetComponent<Page2>();
                page2.pointGroup[6].SetActive(true);
            }
            Debug.Log("Page2_5  "+progressTotal +" countTotal  "+countTotal);
            change360SelectImage(progressTotal);
        }
    }

    public void change360SelectImage(int index)
    {
        //Debug.Log(" (change360SelectImage " + "  index " + index);
        if (index <= 0 || index >= sceneAreaImagesList.Count)
            return;

        for (int i = 0; i < sceneAreaImagesList.Count; i++)
        {
            if (i == index)
            {
                Debug.Log(" (sceneAreaImagesList "+sceneAreaImagesList[i].name+"  index "+ index);
                current360ShowIndex = i;
                SceneMgr.getInstance().setVisible((sceneAreaImagesList[i].transform.gameObject), true);
            }
            else
                SceneMgr.getInstance().setVisible((sceneAreaImagesList[i].transform.gameObject), false);
        }
    }

    public void setPlayAreaImage( bool _isPlayAreaImage)
    {
        isPlayAreaImage = _isPlayAreaImage;
    }

    public bool getPlayAreaImage()
    {
        return isPlayAreaImage;
    }
}
