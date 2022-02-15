using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ItemManager : MonoBehaviour
{
    public Camera cam;
    public bool isItemMovable = false;
    public Transform leftLimit, rightLimit, upperLimit, bottomLimit;
    public Animator bottleAnimator, capAnimator;
    public GameObject bottleCap;
    public bool isPouringDrops;
    public GameObject eyeDropPrefab;
    public Transform eyeDropSpawnPosition;

    public Material redEyeMaterial;
    public Color materialColor;

    //Ray ray;
    //RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        materialColor = redEyeMaterial.color;
        materialColor.a = 1;
        redEyeMaterial.color = materialColor;
        //Debug.Log(materialColor.a);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            if(isPouringDrops == false)
            {
                isItemMovable = !isItemMovable;
            }
            StartCoroutine(EyeDrop());
            //isPouringDrops = false;
        }
        //ray = cam.ScreenPointToRay(Input.mousePosition);
        //if(Physics.Raycast(ray, out hit))
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, hit.point, Time.deltaTime * 5f);
        //}
        if(isItemMovable == false)
        {
            transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.3185f));
        }
           
    }

    IEnumerator EyeDrop()
    {
        while (isItemMovable == true)
        {
            if (this.transform.position.x >= leftLimit.position.x && this.transform.position.x <= rightLimit.position.x && this.transform.position.z >= bottomLimit.position.z && this.transform.position.z <= upperLimit.position.z)
            {
                isPouringDrops = true;
                capAnimator.SetBool("Cap", true);
                yield return new WaitForSecondsRealtime(2f);
                bottleCap.SetActive(false);
                bottleAnimator.SetBool("EyeDrops", true);
                yield return new WaitForSecondsRealtime(1f);
                LeanPool.Spawn(eyeDropPrefab, eyeDropSpawnPosition.position, Quaternion.identity);
                while (materialColor.a>=0.5f)
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                    materialColor.a -= 0.1f;
                    redEyeMaterial.color = materialColor;
                   // yield return new WaitForSecondsRealtime(1f);
                }
                LeanPool.Spawn(eyeDropPrefab, eyeDropSpawnPosition.position, Quaternion.identity);
                while (materialColor.a>0f && materialColor.a <0.5f)
                {
                    yield return new WaitForSecondsRealtime(0.5f);
                    materialColor.a -= 0.1f;
                   
                    //Debug.Log(materialColor.a);
                    redEyeMaterial.color = materialColor;
                    //yield return new WaitForSecondsRealtime(1f);
                }

                yield return new WaitForSecondsRealtime(2f);
                bottleAnimator.SetBool("EyeDrops", false);
               
                yield return new WaitForSecondsRealtime(1f);
                isPouringDrops = false;
                StopAllCoroutines();

            }
            yield return null;
        }
    }
}
