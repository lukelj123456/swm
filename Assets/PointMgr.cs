using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PointMgr : MonoBehaviour {

    public string[] titleStr1 = {};
    public string[] titleStr2 = {};
    public string[] titleStr3 = {};
    public string[] titleStr4 = { };
    public string[] titleStr5 = { };
    public string[] titleStr6 = { };
    public string[] titleStr7 = { };

    public string[] descStr1 = { };
    public string[] descStr2 = { };
    public string[] descStr3 = { };
    public string[] descStr4 = { };
    public string[] descStr5 = { };
    public string[] descStr6 = { };
    public string[] descStr7 = { };

    public string[] imgStr1 = { };
    public string[] imgStr2 = { };
    public string[] imgStr3 = { };
    public string[] imgStr4 = { };
    public string[] imgStr5 = { };
    public string[] imgStr6 = { };
    public string[] imgStr7 = { };

    public List<string[]> titleList = new List<string[]>();
    public List<string[]> descList = new List<string[]>();
    public List<string[]> imgList = new List<string[]>();

    //public Dictionary<GameObject, GameObject> imageActionMap1 = new Dictionary<GameObject, GameObject>();

    public ArrayList arra; 

    public List<GameObject> imageActionMap;
    public List<GameObject> imageStaticMap;

    private static PointMgr instance;

    public List<GameObject> pointComponentList;

    void Awake()
    {
        instance = this;
    }

    public static PointMgr getInstance()
    {
        return instance;
    }
    // Use this for initialization
    void Start () {

        titleList.Add(titleStr1);
        titleList.Add(titleStr2);
        titleList.Add(titleStr3);
        titleList.Add(titleStr4);
        titleList.Add(titleStr5);
        titleList.Add(titleStr6);
        titleList.Add(titleStr7);

        descList.Add(descStr1);
        descList.Add(descStr2);
        descList.Add(descStr3);
        descList.Add(descStr4);
        descList.Add(descStr5);
        descList.Add(descStr6);
        descList.Add(descStr7);

        imgList.Add(imgStr1);
        imgList.Add(imgStr2);
        imgList.Add(imgStr3);
        imgList.Add(imgStr4);
        imgList.Add(imgStr5);
        imgList.Add(imgStr6);
        imgList.Add(imgStr7);
                
    }

    public string getTitleByIndex(int sceneIndex,int index)
    {
        return titleList[sceneIndex][index];
    }

    public string getDescByIndex(int sceneIndex, int index)
    {
        return descList[sceneIndex][index];
    }

    public string getImgByIndex(int sceneIndex,int index)
    {
        Debug.Log("getImgByIndex  sceneIndex " + sceneIndex + " index  " + index);
        return imgList[sceneIndex][index-1];
    }

    public GameObject getImageAction(string actionName)
    {
        for (int i = 0; i < imageActionMap.Count; i++)
        {
            if(imageActionMap[i].name == actionName)
            {
                return imageActionMap[i];
            }
        }
        return null;
    }

    public GameObject getImageStatic(string actionName)
    {
        for(int i = 0;i < imageStaticMap.Count;i++)
        {
            if (imageStaticMap[i].name == actionName)
            {
                return imageStaticMap[i];
            }
        }
        return null;
    }
    // Update is called once per frame
    void Update () {
	}
}
