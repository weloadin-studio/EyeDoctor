using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Camera cam;
    public bool isItemMovable = false;
    public Transform leftLimit, rightLimit, upperLimit, bottomLimit;
    public Animator bottleAnimator, capAnimator;
    public GameObject bottleCap;
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
            StartCoroutine(EyeDrop());
        }
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray, out hit))
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 5f);
        //}
        if(isItemMovable == false)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.32f));
        }
           
    }

    IEnumerator EyeDrop()
    {
        while (isItemMovable == true)
        {
            if (this.transform.position.x >= leftLimit.position.x && this.transform.position.x <= rightLimit.position.x && this.transform.position.z >= bottomLimit.position.z && this.transform.position.z <= upperLimit.position.z)
            {
                capAnimator.SetBool("Cap", true);
                yield return new WaitForSeconds(1f);
                bottleCap.SetActive(false);
                bottleAnimator.SetBool("EyeDrops", true);
                yield return new WaitForSeconds(2f);
                bottleAnimator.SetBool("EyeDrops", false);

            }
            yield return null;
        }
    }
}
