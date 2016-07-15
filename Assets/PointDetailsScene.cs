using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PointDetailsScene : MonoBehaviour {

    public GameObject pointDetailsMinMap;

    public int index = 0;
    public List<Image> imageList;


    public GameObject point3AnimationObj;
    public List<GameObject> point3Animation;
    //动画索引值
    public int pointAnimationIndex;

    public int delatIndex;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnEnable()
    {
        //pointAnimationIndex = 0;
        //Invoke("startVisibleTrue", 5);
        
        //Image image = pointDetailsMinMap.GetComponent<Image>();
        //image.sprite = Resources.Load("Frame/scene2_1/hou___00001") as Sprite;
    }

    public void startVisibleTrue()
    {
        //point3AnimationObj.SetActive(true);
    }
}
