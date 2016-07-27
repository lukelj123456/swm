using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SceneMgr : MonoBehaviour {

    public GameObject panoramic;
    public List<GameObject> sceneList;

    public CanvasGroup canvasGroupOne = null;
    public CanvasGroup canvasGroupZero = null;

    public GameObject scrollUV;

    public string[] nameStr = {"2016款 1.8T 四驱尊享版 5座","2016款 1.8T 四驱精英版 7座","2016款 1.8T 两驱精英版 7座","2016款 1.8T 两驱尊享版 5座","2016款 1.8T 四驱尊享版 7座"};
    private static SceneMgr instance;

    public Vector3 showPoint = new Vector3(0, 0, 0);
    public Vector3 hidePoint = new Vector3(1000, 1000, 0);

    private GameObject currentScene;

    private Hashtable cacheMap = new Hashtable();

    // Use this for initialization
    void Start()
    {
        GameObject scene6 = (GameObject)Instantiate(Resources.Load("prefab/scene6"));
        sceneList[5] = scene6;
        Page2 page2 = sceneList[1].GetComponent<Page2>();
        scene6.transform.parent = page2.sceneLink.transform;
    }


	public enum SceneType{
		scene1 = 0,
		scene2 = 1,
		scene3 = 2,
		scene4 = 3,
		scene5 = 4,
		scene6 = 5,
		scene2_4 = 6,
		scene2_5 = 7,
		scene2_6 = 8,
	} 

    public Dictionary<string, bool> pageKeyMap = new Dictionary<string, bool>();
    void Awake()
    {
        instance = this;
    }

    public void changeFadeToZero(GameObject _canvasGroup)
    {
        canvasGroupZero = _canvasGroup.GetComponent<CanvasGroup>();

        iTween.FadeTo(_canvasGroup.GetComponent<CanvasGroup>().gameObject,0.2f,10);
        //iTween.ValueTo(this.gameObject, iTween.Hash("from", 1, "to", 0, "time", 0.8f,
        //    "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "updateFadeToZeroComplete",
        //    "oncompleteparams", canvasGroupZero.gameObject));
    }

    public void updateFadeToZeroComplete(GameObject target)
    {
        target.SetActive(false);
        Debug.Log("complete");
    }

    public void changeFadeToOne(GameObject _canvasGroup)
    {
        canvasGroupOne = _canvasGroup.GetComponent<CanvasGroup>();
        canvasGroupOne.alpha = 1;
        iTween.ValueTo(this.gameObject, iTween.Hash("from", 0, "to", 1, "time", 0.8f,
            "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "updateFadeToOneComplete",
            "oncompleteparams", canvasGroupOne.gameObject));
    }

    public void updateFadeToOneComplete(GameObject target)
    {
        Debug.Log("updateFadeToOneComplete");
    }

    void OnUpdateFadeToOneTween(float value)
    {
        canvasGroupOne.alpha = value;
    }

    void OnUpdateFadeToZeroTween(float value)
    {
        canvasGroupZero.alpha = value;
    }

    public static SceneMgr getInstance()
    {
        return instance;
    }


	public GameObject loadScene(string nameStr,int index)
    {
		if (sceneList [index] != null)
			return sceneList [index];
		else {
			Page2 page2 = sceneList[1].GetComponent<Page2>();
			GameObject scene = (GameObject)Instantiate(Resources.Load("Frame/"+nameStr));
			scene.transform.parent = page2.sceneLink.transform;
			sceneList[index] = scene;
			return scene;
		}
    }

    public GameObject getScene(int index)
    {
        return sceneList[index];
    }


    public void setCurrentScene(GameObject _scene)
    {
        currentScene = _scene;
    }

    public GameObject getCurrentScene()
    {
        return currentScene;
    }

    public void changeScene(int index)
    {
        for (int i = 0; i < 6; i++)
        {
            if (sceneList[i].activeSelf == true && i != index - 1)
            {
                Page1 page1 = sceneList[i].GetComponent<Page1>();
                Page2 page2 = sceneList[i].GetComponent<Page2>();
                Page3 page3 = sceneList[i].GetComponent<Page3>();
                Page4 page4 = sceneList[i].GetComponent<Page4>();
                Page5 page5 = sceneList[i].GetComponent<Page5>();

				if (page2 != null) {
					for(int m = 0 ; m < sceneList.Count ; m++ )
					{
						Page6 p6 = sceneList [m].GetComponent<Page6> ();
						Page2_5 p2_5 = sceneList [m].GetComponent<Page2_5> ();
						if (p6 != null) {
							sceneList [m].SetActive (false);
							break;
						}
					}
							
					page2.setVisibleFlase ();
				}
                else if (page3 != null)
                    page3.setVisibleFlase();
                else if (page4 != null)
                    page4.setVisibleFlase();
                else if(page5 != null)
                    page5.setVisibleFlase();
//                sceneList[i].SetActive(false);
			}
        }
		if(index == 2)
		{
			Page2 page = sceneList [1].GetComponent<Page2> ();
            page.setStartPlay(false);
		}
		if(index == 4)
        	panoramic.SetActive(true);
		else
			panoramic.SetActive(false);
        sceneList[index-1].SetActive(true);                
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void setPageClick(string name,bool isClick)
    {
		pageKeyMap[name.ToLower()] = isClick;
    }

    public bool isPageClickByName(string name)
    {
		if (pageKeyMap.ContainsKey(name.ToLower()) == false)
            return false;
        else
			return pageKeyMap[name.ToLower()];
    }


    public delegate void callBackPlayScene();
    public callBackPlayScene delegateMethod;

    public IEnumerator playScene(int startIndex, int endIndex, List<Image> listImage,int currentIndex,GameObject page, 
		callBackPlayScene callBack = null,float frameTime = 0.001f,bool isForward = true)
    {
		if ((startIndex <= endIndex) && isForward == true)
        {
            if (startIndex < 0)
                startIndex = 0;
            if (endIndex > listImage.Count)
                endIndex = listImage.Count;
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (listImage[i].IsDestroyed())
                    yield return 0;
                if (i == currentIndex)
                    setVisible(listImage[i].transform.gameObject, true);
                else
                    setVisible(listImage[i].transform.gameObject, false);
            }
            currentIndex++;
            if (currentIndex <= endIndex)
            {
                yield return new WaitForSeconds(frameTime);
                if (page != null && page.activeSelf == true)
                {
                    Page2_5 p2_5 = page.GetComponent<Page2_5>();
                    Page2_6 p2_6 = page.GetComponent<Page2_6>();
                    if (p2_5 != null && p2_5.getPlayAreaImage())
                    {
                        p2_5.current360ShowIndex = currentIndex;
                    }
                    else if (p2_6 != null)
                    {
                        p2_6.currentShowIndex = currentIndex;
                    }
                }

                StartCoroutine(playScene(startIndex, endIndex, listImage, currentIndex, page, callBack, frameTime, isForward));
            }
            else
            {
                if (page != null && page.activeSelf == true)
                {
                    Page2_6 p2_6 = page.GetComponent<Page2_6>();
                    Page2_5 p2_5 = page.GetComponent<Page2_5>();
                    if (p2_5 != null)
                    {
                        p2_5.setPlayAreaImage(false);
                    }
                    else if (p2_6 != null)
                    {
                        p2_6.currentShowIndex = currentIndex;
                    }

                    if (callBack != null)
                        callBack.Invoke();
                }
            }
        }
		else if((startIndex >= endIndex) && isForward == false)
        {
            if (endIndex < 0)
                endIndex = 0;
            if (startIndex >= listImage.Count)
                startIndex = listImage.Count - 1;

            for (int i = startIndex; i >= endIndex; i--)
            {
                if (i == currentIndex)
                    setVisible(listImage[i].transform.gameObject, true);
                else
                    setVisible(listImage[i].transform.gameObject, false);
            }
            currentIndex--;
            if (currentIndex >= endIndex)
            {
                yield return new WaitForSeconds(frameTime);
                Page2_5 p2_5 = page.GetComponent<Page2_5>();
				Page2_6 p2_6 = page.GetComponent<Page2_6>();

				if (p2_5 != null && p2_5.isPlayAreaImage) {
					p2_5.current360ShowIndex = currentIndex;
				}
				StartCoroutine(playScene(startIndex, endIndex, listImage, currentIndex, page,callBack,frameTime,isForward));
            }
            else
            {
                Page2_5 p2_5 = page.GetComponent<Page2_5>();
				Page2_6 p2_6 = page.GetComponent<Page2_6>();
                if (p2_5 != null)
                {
                    p2_5.setPlayAreaImage(false);
                }
                if (callBack != null)
                    callBack.Invoke();
            }
        }

        yield return 0;
    }

    public void handler360NeiShi()
    {
        SceneMgr.getInstance().changeScene(3);
    }

    public void handler360Quanjing()
    {
        SceneMgr.getInstance().changeScene(4);
    }

    public void handler360Huanjing()
    {
        SceneMgr.getInstance().changeScene(5);
    }

    public void handlerHome()
    {
        SceneMgr.getInstance().changeScene(2);
    }

    public void playScene(GameObject scene,bool isForward)
	{
		Page2_4 page2_4 = scene.GetComponent<Page2_4>();
		Page2_5 page2_5 = scene.GetComponent<Page2_5>();
		Page2_6 page2_6 = scene.GetComponent<Page2_6>();
		if (page2_4 != null) {
			page2_4.isForward = isForward;
			page2_4.playScene (0);
		}
		else if (page2_5 != null) {
			page2_5.isForward = isForward;
			page2_5.playScene (0);
		}else if (page2_6 != null) {
			page2_6.isForward = isForward;
			page2_6.playScene (0);
		}
	}

    public void addCacheImage(string url,Sprite instance)
    {
        cacheMap.Add(url, instance);
    }

    public Sprite getCacheImage(string url)
    {
        return (Sprite)cacheMap[url];
    }

	public void setPage2Start(bool isStart)
	{
		Page2 page2 = SceneMgr.getInstance().sceneList[1].GetComponent<Page2>();
		page2.setStartPlay (isStart);
	}
    public void setVisible(GameObject obj,bool isTrue)
    {
        if (obj == null)
            return;
		

		Debug.Log ("gameObject.name =  "+obj.name+" isTrue  "+isTrue.ToString());
        if (isTrue)
        {
            if (obj.activeSelf == false)
                obj.SetActive(true);
            obj.transform.localPosition = showPoint;
        }
        else
        {
            obj.transform.localPosition = hidePoint;
        }            
    }
}
