using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteFrame : MonoBehaviour {
    public List<GameObject> listImage;
    public int speed = 20;
    private int index = 0;

    public int currentIndex = 0;

    public Vector3 pointVisileFlase = new Vector3(100,1000,0);
    public Vector3 pointVisileTrue = new Vector3(0,0,0);
    public float delayTime = 1.0f;
    public GameObject layer;
    public bool isStartPlay = false;
    // Use this for initialization
    void Start()
    {
        
    }

    public void onCompleteActive()
    {

    }

    public void OnEnable()
    {
        isStartPlay = false;
        for (int i = 0; i < listImage.Count; i++)
        {
            if (i == 0)
            {
                listImage[i].transform.localPosition = pointVisileTrue;
            }
            else
                listImage[i].transform.localPosition = pointVisileFlase;
        }
        index = 0;
        Invoke("startVisibleTrue", delayTime);
    }

    public void startVisibleTrue()
    {
        isStartPlay = true;
    }
    // Update is called once per frame
    void Update () {
        if (isStartPlay == false) return;
        index++;
        if (index % speed == 0)
        {
            if (currentIndex == listImage.Count)
            {
                currentIndex = 0;
                for (int m = 0; m < listImage.Count; m++)
                {
                    listImage[m].transform.localPosition = pointVisileFlase;
                }
                return;
            }
            for (int i = 0; i < listImage.Count; i++)
            {
                if (i <= currentIndex)
                    //listImage[i].SetActive(true);
                    listImage[i].transform.localPosition = pointVisileTrue;
                else
                {
                    listImage[i].transform.localPosition = pointVisileFlase;
                }
                //listImage[i].SetActive(false);
            }
            currentIndex++;
        }
    }
}
