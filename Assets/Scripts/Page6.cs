using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page6 : MonoBehaviour {
    
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

    public Vector3 scaleImage =  new Vector3(1.3f,1.3f,1.0f);

    // Use this for initialization
    void Start () {
    }

    void OnEnable()
    {
        for (int i = 0; i < sceneImagesList.Count; i++)
        {
            GameObject obj = (sceneImagesList[i].transform.gameObject);
            obj.transform.localScale = scaleImage;
            Debug.Log("sceneImageList "+i);
            SceneMgr.getInstance().setVisible(obj, false);
        }
        this.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void gotoAndStopCallBack()
    {

    }

    public void gotoAndStop(int index)
    {
        string range = initSceneData(index);
        if(index == 6)
            startIndex = int.Parse(range.Split('_')[1]);
        else
            startIndex = int.Parse(range.Split('_')[0]);

        SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(gotoAndStopCallBack);
        for(int i = 0;i< sceneImagesList.Count; i++)
        {
                SceneMgr.getInstance().setVisible(sceneImagesList[i].gameObject, false);
        }
        StartCoroutine(SceneMgr.getInstance().playScene(startIndex, startIndex, sceneImagesList, startIndex, 
                this.gameObject, callBack, 0.001f, true));        
    }

    public string initSceneData(int index)
    {
        sceneRange3 = new string[6];
        if (index < 0)
            index = 0;
        else if (index >= sceneRange3.Length)
            index = sceneRange3.Length - 1;
        sceneRange3[0] = "0_13";
        sceneRange3[1] = "13_34";
        sceneRange3[2] = "34_47";
        sceneRange3[3] = "47_64";
        sceneRange3[4] = "64_91";
        sceneRange3[5] = "91_107";
        return sceneRange3[index];
    }

    public void playScene(int index)
    {
        string range = initSceneData(index);

        Debug.Log("current frame start = " + range+" index  "+index);
        startIndex = int.Parse(range.Split('_')[0]);
        endIndex = int.Parse(range.Split('_')[1]);
        currentImageIndex = startIndex;
        
		if (isForward) {
			    currentImageIndex = startIndex;
			    SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(setVisibleTrue);
			    StartCoroutine(SceneMgr.getInstance ().playScene (startIndex, endIndex, sceneImagesList, startIndex, this.gameObject,callBack,0.03f,isForward));
		} else{
			    SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(setVisibleFalse);
			    StartCoroutine(SceneMgr.getInstance ().playScene (endIndex, startIndex, sceneImagesList, endIndex, this.gameObject, callBack,0.03f,isForward));
			    currentImageIndex = endIndex;
		}
    }

	public void setVisibleTrue()
	{
		currentShowIndex = endIndex;
		SceneMgr.getInstance().setPage2Start(false);	
	}

	public void setVisibleFalse()
	{	
		    currentShowIndex = startIndex;
		    SceneMgr.getInstance().setPage2Start(false);	
	}
}
