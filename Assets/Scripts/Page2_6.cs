using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page2_6 : MonoBehaviour
{
    public GameObject sceneImage;

    public string[] sceneRange3;
    public int currentImageIndex = 0;
    //当前显示图片的位置
    public int currentShowIndex = 0;
    
    public int startIndex = 0;
    public int endIndex = 0;
    public List<Image> sceneImagesList;

    public bool isForward = true;
    public int updateIndex = 0;

    public bool isHanderMouse = false;
    public int progressTotal = 0;
    public Vector3 scaleImage = new Vector3(1.3f, 1.3f, 1.0f);

    public List<Image> sceneAreaImagesList;
    public int startAreaIndex = 14;
    public int endAreaIndex = 47;

    public bool isPlayAreaImage = false;
    // Use this for initialization
    Page2 page2 = null;
    void Start()
    {
        page2 = SceneMgr.getInstance().sceneList[1].GetComponent<Page2>();
        this.transform.localScale = new Vector3(1, 1, 1);
    }

    void OnEnable()
    {
        for (int i = 0; i < sceneImagesList.Count; i++)
        {
            GameObject obj = (sceneImagesList[i].transform.gameObject);
            obj.transform.localScale = scaleImage;
            obj.SetActive(true);
            SceneMgr.getInstance().setVisible(obj, false);
        }
        this.transform.localPosition = new Vector3(0, 0, 0);
        isForward = true;
        playScene(0);

        SceneMgr.getInstance().setCurrentScene(this.gameObject);
    }
    
    public void setVisibleFalse()
    {
        //this.gameObject.SetActive(false);
    }
    public void playScene(int index)
    {
        sceneRange3 = new string[6];
        if (index < 0)
            index = 0;
        else if (index >= sceneRange3.Length)
            index = sceneRange3.Length - 1;
        sceneRange3[0] = "4_19";
        string range = sceneRange3[index];

        Debug.Log("current frame start = " + range + " index  " + index);
        startIndex = int.Parse(range.Split('_')[0]);
        endIndex = int.Parse(range.Split('_')[1]);
        SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(setVisibleFalse);
		StartCoroutine(SceneMgr.getInstance().playScene(startIndex, endIndex,sceneImagesList, startIndex,this.gameObject,callBack,0.08f));
    }

    void OnDisable()
    {
        for (int i = 0; i < sceneImagesList.Count; i++)
        {
            GameObject obj = (sceneImagesList[i].transform.gameObject);
            obj.transform.localScale = scaleImage;
            SceneMgr.getInstance().setVisible(obj, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
        }
    }
}
