using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Camera cam;
    public bool isItemMovable = false;
    //Ray ray;
    //RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isItemMovable = !isItemMovable;
        }
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray, out hit))
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 5f);
        //}
        if(isItemMovable == false)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.35f));
        }
           
    }
}
