using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FrameAnimation : MonoBehaviour
{
    public int delatTime = 10;

    public int repeatCount = 0;
    
    public List<GameObject> listImage;
    //动画索引值
    public int pointAnimationIndex;

    public int delatIndex;

    public float delayTime =0f;

    public bool isStartPlay = false;
    // Use this for initialization
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (isStartPlay == false) return;
        if (this.gameObject.activeSelf == true)
        {
            delatIndex++;
            if (delatIndex % delatTime == 0)
            {
                if (pointAnimationIndex < listImage.Count)
                {
                    for (int i = 0; i < listImage.Count; i++)
                    {
                        if (pointAnimationIndex == i)
                        {
                            listImage[i].SetActive(true);
                        }
                        else
                            listImage[i].SetActive(false);
                    }
                    pointAnimationIndex++;
                } else if(repeatCount == -1)
                {
                    pointAnimationIndex = 0;
                    delatIndex = 0;
                }
            }
        }
    }

    public void OnEnable()
    {
        pointAnimationIndex = 0;
        isStartPlay = false;
        listImage[0].SetActive(true);
        Invoke("startVisibleTrue", delayTime);
    }

    public void startVisibleTrue()
    {
        isStartPlay = true;
    }
}
