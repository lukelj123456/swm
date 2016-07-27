using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SceneInfo : MonoBehaviour {
    public string[] backGroundNameArray;
    public List<Sprite> sceneImageList;

    public int index = 0;
    private int currentIndex = 0;
    public Image backgroundImage;

    /// <summary>
    /// 缩略图
    /// </summary>
    public Image miniImage;
    public GameObject miniMapLayer;
    public GameObject showBigMapBtn;
    public GameObject bigMapLayer;
    public Text miniTitle;

    public GameObject closeBtn;
    public bool isShowMiniMap = true;

    /// <summary>
    /// Plus
    /// </summary>
    public GameObject plusBtn;
    public GameObject pointBackGround;

    /// <summary>
    /// Minus
    /// </summary>
    public GameObject minusBtn;
    public GameObject minusLayer;
    public Text bigMapTitle;
    public Text bigMapDesc;

    public GameObject closeMiniMapBtn;
    /// <summary>
    /// HasMap存储已经取到的Image
    /// </summary>
    public Dictionary<string, Sprite> spriteList = new Dictionary<string, Sprite>();

    Vector3 initMinusLayerPosition;

    //防止快速点击关闭了界面
    bool isAllowClose = false;
    // Use this for initialization
    void Start (){
        initMinusLayerPosition = minusLayer.transform.localPosition;
	}

    public void OnLoad(string name)
    {
        SceneMgr.getInstance().setPageClick("Page2", false);
        string sceneIndex = name.Split('_')[0];
        string pageIndex = name.Split('_')[1];
        setCurrentIndex(int.Parse(sceneIndex) - 1);
        
        this.gameObject.SetActive(true);
        Button close = closeBtn.GetComponent<Button>();
        close.onClick.AddListener(delegate()
        {
            closeSceneInfo();
        });

        Invoke("SetAllowClose", 0.5f);

		if (int.Parse(sceneIndex) == 6) {
			initBigMapLayer (name);				
			miniMapLayer.SetActive (false);
			bigMapLayer.SetActive (true);
			minusLayer.SetActive (false);
			return;
		}

        miniMapLayer.SetActive(true);
        bigMapLayer.SetActive(false);
        if (isShowMiniMap == true)//show min map
        {
            isShowMiniMap = false;
            miniImage.sprite = getSpriteByImageSrc(name);
            miniImage.gameObject.SetActive(true);
            miniMapLayer.SetActive(true);
            miniMapLayer.name = "mini_" + name;

            Button closeMiniMap = closeMiniMapBtn.GetComponent<Button>();
            closeMiniMapBtn.SetActive(false);

            closeMiniMap.onClick.AddListener(delegate()
            {
                closeSceneInfo();
            });

            closeMiniMapBtn.SetActive(true);

            Button miniBtn = showBigMapBtn.GetComponent<Button>();
            miniTitle.text = PointMgr.getInstance().getTitleByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            miniBtn.onClick.AddListener(delegate ()//show big map
            {
                ImageAlpha imageAlpha = miniMapLayer.GetComponent<ImageAlpha>();
                imageAlpha.AlphaOnFalse();
                string sceneIndex0 = miniMapLayer.name.Split('_')[1];
                string pageIndex0 = miniMapLayer.name.Split('_')[2];
                string bigName = sceneIndex0+"_"+pageIndex0;
                initBigMapLayer(bigName);
                bigMapLayer.SetActive(true);
                closeBtn.SetActive(true);                
            });
        }
    }

    public void SetAllowClose()
    {
        isShowMiniMap = true;
        isAllowClose = true;
    }


    public void closeSceneInfo()
    {
        if(isAllowClose == true)
        {
            ImageAlpha imageAlpha = this.gameObject.GetComponent<ImageAlpha>();
            imageAlpha.hideTime = 0.6f;
            imageAlpha.AlphaOnFalse();
            isAllowClose = false;
        }
    }

    public void OnEnable()
    {        
    }

    public void initBigMapLayer(string bigName)
    {
            string sceneIndex = bigName.Split('_')[0];
            string pageIndex = bigName.Split('_')[1];
            bigMapTitle.text = PointMgr.getInstance().getTitleByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            bigMapDesc.text = PointMgr.getInstance().getDescByIndex(int.Parse(sceneIndex) - 1, int.Parse(pageIndex) - 1);
            bigMapDesc.enabled =false;
            plusBtn.SetActive(true);
            pointBackGround.SetActive(false);

            Button minusLayerBtn = minusLayer.GetComponent<Button>();
            minusLayerBtn.onClick.AddListener(delegate()
            {
                pointBackGround.SetActive(true);
                minusBtn.SetActive(true);
                ImageMove imageAction = minusLayer.GetComponent<ImageMove>();
                imageAction.enabled = true;
                plusBtn.SetActive(false);
                bigMapDesc.enabled = true;
            });

            foreach (Transform node in bigMapLayer.transform)
            {
                if (node.name == sceneIndex)
                {
                        foreach (Transform nodeChild in node.transform)//判断子节点是否是对应显示的gameObject
                        {
                            if (nodeChild.name == bigName)
                            {
                                Image nodeImage = nodeChild.gameObject.GetComponent<Image>();
                                if(nodeImage != null)
                                {
                                    nodeImage.sprite = getSpriteByImageSrc(bigName);
                                    nodeImage.name = bigName;
                                    nodeChild.gameObject.SetActive(true);
                                }
                            }
                            else
                                nodeChild.gameObject.SetActive(false);
                        }
                        node.gameObject.SetActive(true);                
                }
                else if (node.name.Length == 1)
                {
                    foreach (Transform nodeChild in node.transform)//判断子节点是否是对应显示的gameObject
                    {
                        nodeChild.gameObject.SetActive(false);
                    }
                    node.gameObject.SetActive(false);
                }
                    
            }
            //Button pBtn = plusBtn.GetComponent<Button>();
            //pBtn.onClick.AddListener(delegate ()
            //{
            //        minusLayer.SetActive(true);
            //        plusLayer.SetActive(false);
            //});

            Button mBtn = minusBtn.GetComponent<Button>();
            mBtn.onClick.AddListener(delegate ()
            {
                    minusLayer.SetActive(false);
                    //plusLayer.SetActive(true);
            });
    }

    public void setCurrentIndex(int index)
    {
            this.currentIndex = index;
    }

    public int getCurrentIndeex()
    {
            return this.currentIndex;
    }

    void OnDisable()
    {
            isShowMiniMap = true;
            minusLayer.transform.localPosition = initMinusLayerPosition;
            minusLayer.GetComponent<ImageMove>().enabled = false;
            SceneMgr.getInstance().setPageClick("Page2", true);
    }

    // Update is called once per frame
    void Update () {
	
	}

    public Sprite getSpriteByImageSrc(string name)
    {
            string sceneIndex = name.Split('_')[0];
            string pageIndex = name.Split('_')[1];

            string imgSrc = PointMgr.getInstance().getImgByIndex(getCurrentIndeex(), int.Parse(pageIndex));
            Sprite sprite = SceneMgr.getInstance().getCacheImage(imgSrc);
            if(sprite == null)
            {
                Debug.Log("getSpriteByImageSrc  " + imgSrc);  
                Object obj = Resources.Load(imgSrc);
                if (obj == null)
                {
                    Debug.Log("getSpriteByImageSrc  " + imgSrc+" is null");  
                    return null;
                }
                
                Texture2D texture = (Texture2D)Instantiate(obj);
                sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                sprite.name = imgSrc;

                SceneMgr.getInstance().addCacheImage(imgSrc, sprite);
            }
            return sprite;
        }
}
