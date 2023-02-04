using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAim : MonoBehaviour
{
    private bool isForwardRotate;
    private Transform aimTransform;
    private void Awake()
    {
        isForwardRotate = true;
        aimTransform = transform.Find("Aim");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


       
        Debug.Log(angle);

        if (isForwardRotate)
        {
            if (Mathf.Abs(angle) > 90)
            {
                MirrorPlayer();
                isForwardRotate = false;
                angle = -1 * angle;
            }
        }
        else
        {
            if (Mathf.Abs(angle) < 90)
            {
                MirrorPlayer();
                isForwardRotate = true;
                angle = -1 * angle;
            }
        }

        aimTransform.eulerAngles = new Vector3(0, 0, angle);


    }

    void MirrorPlayer()
    {
        transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
        //aimTransform.localScale = new Vector2(-1 * aimTransform.localScale.x, aimTransform.localScale.y);
        //transform.localScale = -1 * transform.localScale;
    }
}
