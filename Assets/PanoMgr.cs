using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanoMgr : MonoBehaviour {

    public GameObject obj;
    // Use this for initialization
    void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            Debug.Log("log");
        });


        //if (isCollider && hitInfo.collider.tag == Tags.ground)//判断射线是否和地面接触碰撞
        //{
        //}
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//拿到鼠标按下的点
            RaycastHit hitInfo;
            bool isCollider = Physics.Raycast(ray, out hitInfo);//定义一个射线
            if(isCollider)
            {
                Debug.Log("collider name  "+hitInfo.collider.name);
            }
        }
    }
}
