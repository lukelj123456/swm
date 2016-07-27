using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page2_4 : MonoBehaviour {   
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

    public Vector3 scaleImage = new Vector3(1.3f, 1.3f, 1.0f);

    // Use this for initialization
    Page2 page2 = null;
    void Start()
    {
            page2 = SceneMgr.getInstance().sceneList[1].GetComponent<Page2>();
            this.transform.localScale = new Vector3(1, 1, 1);
    }

    void OnEnable()
    {
		SceneMgr.getInstance().setPageClick("page2_4", true);
            for (int i = 0; i < sceneImagesList.Count; i++)
            {
                GameObject obj = (sceneImagesList[i].transform.gameObject);
                obj.transform.localScale = scaleImage;
                SceneMgr.getInstance().setVisible(obj, false);
            }
            this.transform.localPosition = new Vector3(0, 0, 0);
            SceneMgr.getInstance().setCurrentScene(this.gameObject);
    }

    public void playScene(int index)
    {
        sceneRange3 = new string[6];
        if (index < 0)
            index = 0;
        else if (index >= sceneRange3.Length)
            index = sceneRange3.Length - 1;
        sceneRange3[0] = "0_11";

        string range = sceneRange3[index];

        Debug.Log("current frame start = " + range + " index  " + index);
        startIndex = int.Parse(range.Split('_')[0]);
        endIndex = int.Parse(range.Split('_')[1]);
        currentImageIndex = startIndex;

		if (isForward) {
			    currentImageIndex = startIndex;
			    SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(setVisibleTrue);
			    StartCoroutine(SceneMgr.getInstance ().playScene (startIndex, endIndex, sceneImagesList, startIndex, this.gameObject,callBack,0.001f,isForward));
		} else{
			    SceneMgr.callBackPlayScene callBack = new SceneMgr.callBackPlayScene(setVisibleFalse);
			    StartCoroutine(SceneMgr.getInstance ().playScene (endIndex, startIndex, sceneImagesList, endIndex, this.gameObject, callBack,0.001f,isForward));
			    currentImageIndex = endIndex;
		}
    }

	public void setVisibleTrue()
	{
		SceneMgr.getInstance().setPage2Start(false);	
	}

	public void setVisibleFalse()
	{
        //SceneMgr.getInstance().setPageClick("page2", true);
		SceneMgr.getInstance().setPage2Start(false);	
		Destroy (this.gameObject);
	}

    void OnDisable()
    {
		SceneMgr.getInstance().setPageClick("page2_4", false);
        page2.pointGroup[2].SetActive(false);
//        for (int i = 0; i < sceneImagesList.Count; i++)
//        {
//            GameObject obj = (sceneImagesList[i].transform.gameObject);
//            obj.transform.localScale = scaleImage;
//            SceneMgr.getInstance().setVisible(obj, false);
//        }
	}
}
