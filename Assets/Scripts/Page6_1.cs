using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Page6_1 : MonoBehaviour {

	public List<GameObject> btn1List;//熄火
	public List<GameObject> btn2List;//dakai

	public List<GameObject> nav1List;
	public List<GameObject>nav2List;

	public List<GameObject> navHandlerList;
	public List<GameObject> navTop1List;
	public List<GameObject> navTop2List;
	public List<GameObject> navTopHandlerList;

    public GameObject backFog;
    public GameObject fontFog;
    public GameObject RedJianTou;
    public GameObject BlueJianTou;


	public GameObject car1;
	public GameObject phone;
	public GameObject level2Group;


    // Use this for initialization
    void Start () {
		for (int i = 0; i < navHandlerList.Count; i++) {
			Button btnObj = navHandlerList[i].GetComponent<Button>();
			Debug.Log ("btnObj  " + btnObj.name);
			btnObj.onClick.AddListener(delegate ()
				{
					Debug.Log("Page6_1 btnObj ");
					int index = int.Parse(btnObj.name.Split('_')[1]) - 1;
					changeNavBtnSelect(index);
				});
			
		}

		for (int i = 0; i < navTop1List.Count; i++) {
			Button btnObj = navTopHandlerList [i].GetComponent<Button> ();
			btnObj.onClick.AddListener (delegate() {
				int index = int.Parse(btnObj.name.Split('_')[1]) - 1;
				changeTopNavBtnSelect(index);
			});
		}
    }


    void OnEnable()
    {
		for (int i = 0; i < btn1List.Count; i++) {

			btn1List [i].SetActive (true);
			btn2List [i].SetActive (false);
		}

		changeNavBtnSelect(0);
		changeTopNavBtnSelect (0);
    }

	public void changeBtnSelect(int index)
	{
		for (int i = 0; i < btn1List.Count; i++) {
			btn1List [i].SetActive (true);
			btn2List [i].SetActive (false);

		}

		Debug.Log ("BtnList  "+btn2List[2].name);
		btn1List [index].SetActive (false);
		btn2List [index].SetActive (true);

	}

	public void changeTopNavBtnSelect(int index)
	{
		for (int i = 0; i < navTop1List.Count; i++) {
			navTop1List [i].SetActive (true);
			navTop2List [i].SetActive (false);
		}
		navTop1List [index].SetActive (false);
		navTop2List [index].SetActive (true);
		switch (index) {
		case 0:
			level2Group.SetActive (true);
			car1.SetActive (true);
			phone.SetActive (true);
			break;
		case 1:
			level2Group.SetActive (false);
			car1.SetActive (false);
			phone.SetActive (false);
			break;
		case 2:
			break;
		}
	}

	public void changeNavBtnSelect(int index)
	{
		for (int i = 0; i < nav1List.Count; i++) {
			nav1List [i].SetActive (true);
			nav2List [i].SetActive (false);
		}

		nav1List [index].SetActive (false);
		nav2List [index].SetActive (true);

		switch (index) {
			case 0:
			changeBtnSelect (0);
			break;
		case 1:
			changeBtnSelect (1);
			break;
		case 2:
			changeBtnSelect (2);
			break;
		case 3:
			changeBtnSelect (3);
			break;
		case 4:
			changeBtnSelect (4);
			break;
		}

	}
	public void changeNavBtnCancel(int index)
	{
		for (int i = 0; i < nav2List.Count; i++) {
			nav1List [i].SetActive (true);
			nav2List [i].SetActive (false);
		}

		nav1List [index].SetActive (false);
		nav2List [index].SetActive (true);

	}
	public void changeBtnCancle(int index)
	{
		for (int i = 0; i < 16; i++) {
			btn2List [i].SetActive (false);
			btn1List [i].SetActive (true);
		}
		btn2List [index].SetActive (false);
		btn1List [index].SetActive (true);
	}
}
