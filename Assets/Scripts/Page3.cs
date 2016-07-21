using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page3 : MonoBehaviour {

    public List<Button> btnGroup;

    public List<Image> hongseImageList;
    public GameObject hongseList;

    public List<Image> baiseImageList;
    public GameObject baiseList;

    public List<Image> heiseImageList;
    public GameObject heiseList;

    public List<Image> heseImageList;
    public GameObject heseList;
    
    public List<Image> yinseImageList;
    public GameObject yinseList;

    public List<Image> anhongImageList;
    public GameObject anhongList;

    public List<Image> plateImageList;
    public GameObject plateList;

    public GameObject baise360;
    public GameObject showWaiXing;
	public string animationName = "hongse";
    

    public List<Button> btnSelectGroundList = new List<Button>();
	public List<Text> textList = new List<Text>();
	public GameObject selectBack;
	public int currentSelectIndex = 0;
    public int animationIndex = 0;
    public int progressTotal = 0;

    public CanvasGroup canvasGroup;

    /// <summary>
    /// 全局选中的后的image list
    /// </summary>
    public List<Image> listImage;

    // Use this for initialization
    void Start () {
        btnGroup = new List<Button>();
		baise360.SetActive (false);

        for (int i = 1; i < 7; i++)
        {
            GameObject item1 = GameObject.Find("color2_" + i);
            Image image1 = item1.GetComponent<Image>();
            image1.enabled = false;            

            GameObject item = GameObject.Find("colorBtn_" + i);
            Button btn = item.GetComponent<Button>();
            btn.onClick.AddListener(delegate ()
            {
                this.btnClick(item);
            });
            btnGroup.Add(btn);
        }
			
        GameObject neishiBtn = GameObject.Find("neishi360Btn");
        Button neishi = neishiBtn.GetComponent<Button>();
        neishi.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().handler360Quanjing();
        });

        GameObject waishi360Btn = GameObject.Find("waishi360Btn");
        Button waishi = waishi360Btn.GetComponent<Button>();
        waishi.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().handler360NeiShi();
        });


        GameObject home360Btn = GameObject.Find("home360Btn");
        Button homeBtn = home360Btn.GetComponent<Button>();
        homeBtn.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().handlerHome();
        });
		
        GameObject huanjing360Btn = GameObject.Find("huanjing360Btn");
        Button huanjingBtn = huanjing360Btn.GetComponent<Button>();
        huanjingBtn.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().handler360Huanjing();
        });


		for (int m = 0; m < textList.Count; m++) {
			textList [m].text = SceneMgr.getInstance ().nameStr [m];
		}
		for (int i = 0; i < btnSelectGroundList.Count; i++) 
		{
			Button btn = btnSelectGroundList [i];		
			btn.onClick.AddListener (delegate() {
				Debug.Log("btn "+btn.name);
			
				currentSelectIndex = int.Parse(btn.name.Split('_')[1]);
				textList[currentSelectIndex].text = SceneMgr.getInstance().nameStr[4];
				textList[4].text = SceneMgr.getInstance().nameStr[currentSelectIndex];

				if(currentSelectIndex == 4 && selectBack.activeSelf == false)
				{
					selectBack.SetActive(true);
				}
				else
					selectBack.SetActive(false);					
			});
		}
        btnIndexSelect(1);
    }


    public void btnIndexSelect(int index)
    {
        for (int i = 1; i < 7; i++)
        {
            GameObject item0 = GameObject.Find("color2_" + i);
            Image image0 = item0.GetComponent<Image>();
            image0.enabled = false;
        }

        hongseList.SetActive(false);
        baiseList.SetActive(false);
        heseList.SetActive(false);
        yinseList.SetActive(false);
        heiseList.SetActive(false);
        anhongList.SetActive(false);
        currentSelectIndex = index;
        switch (index)
        {
            case 1:                
                listImage = hongseImageList;
                change360SelectImage(progressTotal);
                hongseList.SetActive(true);
                animationName = "hongse";
                break;
            case 2:
                animationName = "yinse";
                listImage = yinseImageList;
                change360SelectImage(progressTotal);
                yinseList.SetActive(true);
                break;
            case 3:
                animationName = "baise";
                listImage = baiseImageList;
                change360SelectImage(progressTotal);
                baiseList.SetActive(true);
                break;
            case 4:
                animationName = "heise";
                listImage = heiseImageList;
                change360SelectImage(progressTotal);
                heiseList.SetActive(true);
                break;
            case 5:
                animationName = "hese";
                listImage = heseImageList;
                change360SelectImage(progressTotal);
                heseList.SetActive(true);
                break;
            case 6:
                animationName = "anhong";
                listImage = anhongImageList;
                change360SelectImage(progressTotal);
                anhongList.SetActive(true);
                break;
        }

        GameObject item = GameObject.Find("color2_" + currentSelectIndex);
        Image image1 = item.GetComponent<Image>();
        image1.enabled = true;

        baise360.SetActive(true);
    }
    public void btnClick(GameObject obj)
    {
        btnIndexSelect(int.Parse(obj.name.Split('_')[1]));
    }

   public void change360SelectImage(int index)
    {
        if (index >= listImage.Count) return;
        for (int i = 0; i < listImage.Count; i++)
        {
            if (i == index)
            {
                    (listImage[i].transform.gameObject).SetActive(true);
                    SceneMgr.getInstance().setVisible((listImage[i].transform.gameObject), true);
                    plateImageList[i].gameObject.SetActive(true);
                    Debug.Log("change360SelectImage listImage  "+i);
            }
            else
            {
                    SceneMgr.getInstance().setVisible((listImage[i].transform.gameObject), false);
                    plateImageList[i].gameObject.SetActive(false);
            }
        }
    }

    public void setVisibleFlase()
    {
        ImageAlpha iAlpha = this.gameObject.GetComponent<ImageAlpha>();
        iAlpha.AlphaOnFalse();
    }
    
    // Update is called once per frame
    void Update () {
		if (this.gameObject.activeSelf == false)
			return;

        Vector3 mouseY = Input.mousePosition;
        if (mouseY.y < 300)
        {
            return;
        }
		if (Input.touchCount > 0)
		{
			Vector3 position = Input.GetTouch(0).position;

			float mouseX = Input.GetTouch(0).deltaPosition.x;
			int countTotal = (int)(Mathf.Floor((mouseX/ Screen.width)*listImage.Count));
			int tempCount = countTotal + progressTotal;
			if ((tempCount) < 0)
				progressTotal = tempCount + listImage.Count;
			else if (tempCount > listImage.Count)
				progressTotal = tempCount - listImage.Count;
			else
				progressTotal = tempCount;
			Debug.Log("touch position " + position.x + " y = " + position.y + " progressTotal = " + progressTotal + " deltaPosition " + mouseX + " countTotal " + countTotal);
			change360SelectImage(progressTotal);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 position = Input.mousePosition;
			float mouseX = Input.GetAxis("Mouse X");
			int countTotal = (int)Mathf.Floor(mouseX * listImage.Count * 0.1f);

			int tempCount = countTotal + progressTotal;
			if (tempCount < 0)
				progressTotal = tempCount + listImage.Count;
			else if (tempCount > listImage.Count)
				progressTotal = tempCount - listImage.Count;
			else
				progressTotal = tempCount;
            //Debug.Log("mouse position " + position.x + " y = " + position.y + " progressTotal = " + progressTotal + " mouseX " + mouseX + " countTotal " + countTotal);
			change360SelectImage(progressTotal);
		}
	}
}
