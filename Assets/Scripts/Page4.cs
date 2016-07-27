using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page4 : MonoBehaviour
{
	public List<Button> btnSelectGroundList = new List<Button>();
	public List<Text> textList = new List<Text>();
	public GameObject selectBack;
	public int currentSelectIndex = 0;

	public GameObject pano;
    // Use this for initialization
    void Start()
    {
		selectBack.SetActive(false);
        GameObject scene4Home = GameObject.Find("scene4Home");
        Button homeBtn = scene4Home.GetComponent<Button>();
        homeBtn.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().changeScene(2);
        });
		
        GameObject neishi4Btn = GameObject.Find("neishi4Btn");
        Button neishi = neishi4Btn.GetComponent<Button>();
        neishi.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().changeScene(5);
        });

        GameObject waishi360Btn = GameObject.Find("waishi4Btn");
        Button waishi = waishi360Btn.GetComponent<Button>();
        waishi.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().changeScene(3);
        });


        GameObject huanjing4Btn = GameObject.Find("huanjing4Btn");
        Button huanjingBtn = huanjing4Btn.GetComponent<Button>();
        huanjingBtn.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().changeScene(5);
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

		pano.SetActive (true);
    }
    
    public void setVisibleFlase()
    {
        ImageAlpha iAlpha = this.gameObject.GetComponent<ImageAlpha>();
        iAlpha.AlphaOnFalse();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
