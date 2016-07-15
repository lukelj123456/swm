using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page5 : MonoBehaviour
{

    public List<Button> btnGroup;

    public GameObject showHuanjing;
    public GameObject back3d;
    public GameObject huangjing;
    public GameObject color2_1;
    public GameObject color2_2;
    public GameObject color2_3;
    private int currentIndex = 1;

	public List<Button> btnSelectGroundList = new List<Button>();
	public List<Text> textList = new List<Text>();
	public GameObject selectBack;
	public int currentSelectIndex = 0;


	public List<Image> hongseImageList;
	public GameObject hongseList;

	public List<Image> baiseImageList;
	public GameObject baiseList;

	public List<Image> heseImageList;
	public GameObject heseList;



    public List<Image> hongseCarImageList;
    public GameObject hongseCarList;

    public List<Image> baiseCarImageList;
    public GameObject baiseCarList;

    public List<Image> heseCarImageList;
    public GameObject heseCarList;


    public List<Image> plateImageList;
    public GameObject plateList;

	public int animationIndex = 0;
	public int progressTotal = 0;

    public GameObject waishi5Btn;
    public GameObject neishiBtn;
    public GameObject homeHuanjingBtn;

    public List<Image> carImageList;
    private GameObject carObjList;

    public List<Image> listImage;
    // Use this for initialization
    void Start()
    {
		selectBack.SetActive(false);
        Button neishi = neishiBtn.GetComponent<Button>();
        neishi.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().changeScene(4);
        });
        
        Button waishi = waishi5Btn.GetComponent<Button>();
        waishi.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().changeScene(3);
        });
                
        
		Button homeBtn = homeHuanjingBtn.GetComponent<Button>();
		homeBtn.onClick.AddListener(delegate ()
			{
				SceneMgr.getInstance().changeScene(2);
			});
		
        for(int i = 1;i<4;i++)
        {
            GameObject scene5_color_btn = GameObject.Find("scene5_colorBtn_" + i);
            Button btn = scene5_color_btn.GetComponent<Button>();
            btn.onClick.AddListener(delegate ()
            {
                string index = btn.name.Split('_')[2];
                changeBtn(int.Parse(index));
            });
        }

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

		changeBtn (1);
    }

    public void changeBtn(int index)
    {
        color2_1.SetActive(false);
        color2_2.SetActive(false);
        color2_3.SetActive(false);

		hongseList.SetActive (false);
		heseList.SetActive (false);
		baiseList.SetActive (false);

        hongseCarList.SetActive(false);
        heseCarList.SetActive(false);
        baiseCarList.SetActive(false);

        switch (index)
        {
			case 1:
				color2_1.SetActive (true);
				hongseList.SetActive (true);
				listImage = hongseImageList;
                carImageList = hongseCarImageList;
				change360SelectImage (progressTotal);
                carObjList = hongseCarList;
                carObjList.SetActive(true);
                break;
            case 2:
                color2_2.SetActive(true);
                baiseList.SetActive(true);
                carImageList = baiseCarImageList;
                carObjList = baiseCarList;
                carObjList.SetActive(true);
                listImage = baiseImageList;
                change360SelectImage(progressTotal);
				break;
            case 3:
                color2_3.SetActive(true);
                heseList.SetActive(true);
                carImageList = heseCarImageList;
                carObjList = heseCarList;
                carObjList.SetActive(true);
                listImage = heseImageList;                
                change360SelectImage(progressTotal);
                break;
        }
        currentIndex = index;
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
		if (Input.touchCount > 0)
		{
			Vector3 position = Input.GetTouch(0).position;
			if (position.y < 100)
			{
				return;
			}
			float mouseX = Input.GetTouch(0).deltaPosition.x;
			int countTotal = (int)(Mathf.Floor((mouseX/ Screen.width)*listImage.Count))/2;
			int tempCount = countTotal + progressTotal;
			if ((tempCount) < 0)
				progressTotal = tempCount + listImage.Count;
			else if (tempCount > listImage.Count)
				progressTotal = tempCount - listImage.Count;
			else
				progressTotal = tempCount;
			Debug.Log("scene width "+Screen.width);
			Debug.Log("touch position " + position.x + " y = " + position.y + " progressTotal = " + progressTotal + " deltaPosition " + mouseX + " countTotal " + countTotal);
			change360SelectImage(progressTotal);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			if (mousePosition.y < 100)
			{
				return;
			}
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
			Debug.Log("mouse position " + position.x + " y = " + position.y + " progressTotal = " + progressTotal + " mouseX " + mouseX + " countTotal " + countTotal);
			change360SelectImage(progressTotal);
		}
	}

	public void change360SelectImage(int index)
	{
        if (index >= listImage.Count) return;
		Debug.Log ("change360SelectImage  "+index + "count "+listImage.Count);
		for (int i = 0; i < listImage.Count; i++)
		{
			if (i == index)
			{
                SceneMgr.getInstance().setVisible((listImage[i].transform.gameObject), true);
                SceneMgr.getInstance().setVisible((carImageList[i].transform.gameObject),true);
                plateImageList[i].gameObject.SetActive(true);
            }
			else
            {
                SceneMgr.getInstance().setVisible((listImage[i].transform.gameObject), false);
                SceneMgr.getInstance().setVisible((carImageList[i].transform.gameObject), false);
                plateImageList[i].gameObject.SetActive(false);
            }
		}

        //if(index >= listImage.Count && listImage.Count > 0)
        //{
        //    SceneMgr.getInstance().setVisible((listImage[0].transform.gameObject), false);
        //    SceneMgr.getInstance().setVisible((carImageList[0].transform.gameObject), false);
        //    plateImageList[0].gameObject.SetActive(false);
        //}
            
    }
}
