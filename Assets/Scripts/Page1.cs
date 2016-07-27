using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page1 : MonoBehaviour {

    public Button logoBtn;
    public GameObject layer;
	public GameObject wenzi;

    // Use this for initialization
    void Start () {
        logoBtn.onClick.AddListener(delegate ()
        {
            SceneMgr.getInstance().sceneList[1].SetActive(true);
            
            //SceneMgr.getInstance().setPageClick("page2", false);
            //SceneMgr.getInstance().setPageClick("page6", false);

            GameObject objPage1 = SceneMgr.getInstance().sceneList[0];
            ImageAlpha page1 = objPage1.GetComponent<ImageAlpha>();
            page1.AlphaOnFalse();
            
            GameObject objPage2 = SceneMgr.getInstance().sceneList[1];
            objPage2.SetActive(true);

            //Page2 page = obj.GetComponent<Page2>();
            //page.layer.transform.localPosition = new Vector3(1280, 0, 0);

            //iTween.MoveTo(page.layer, iTween.Hash("speed", 2000f, "easeType", iTween.EaseType.linear, "islocal", true,
            //        "position", new Vector3(0, 0, 0),"oncompletetarget", gameObject));

            //iTween.MoveTo(layer, iTween.Hash("speed", 2000f, "easeType", iTween.EaseType.linear, "islocal", true,
            //        "position", new Vector3(-1300, 0, 0), "oncomplete","onCompleteActive", "oncompletetarget", gameObject));
        });        
		//Invoke("visibaleWenzi",0.5f);        
    }

	public void visibaleWenzi()
	{
		if (this.gameObject.activeSelf == false)
			return;
		if (wenzi.activeSelf == true) {
            //wenzi.SetActive (false);
            ImageAlpha alpha = wenzi.GetComponent<ImageAlpha>();
            alpha.AlphaOnFalse();
            Invoke("visibaleWenzi",0.3f);
		}
		else {
            CanvasGroup canvas = wenzi.GetComponent<CanvasGroup>();
            canvas.alpha = 0;
			wenzi.SetActive (true);
			Invoke("visibaleWenzi",0.8f);
		}
	}

    void onCompleteActive()
    {
        this.gameObject.SetActive(false);
        SceneMgr.getInstance().changeScene(2);
    }
    // Update is called once per frame
    void Update () {
	    
	}
}
