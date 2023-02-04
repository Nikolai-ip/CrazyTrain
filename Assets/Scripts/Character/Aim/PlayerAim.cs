using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAim : MonoBehaviour
{
    private bool isForwardRotate;
    private Transform aimTransform;
    private Transform weaponTransform;
    private void Awake()
    {
        isForwardRotate = true;
        aimTransform = transform.Find("Aim");
        weaponTransform = aimTransform.Find("Revolver");
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
            }
        }
        else
        {
            if (Mathf.Abs(angle) < 90)
            {
                MirrorPlayer();
                isForwardRotate = true;
            }
        }

        aimTransform.eulerAngles = new Vector3(0, 0, angle);


    }

    void MirrorPlayer()
    {
        transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        aimTransform.localScale = new Vector3(-1 * aimTransform.localScale.x, aimTransform.localScale.y, aimTransform.localScale.z);
        weaponTransform.localScale = new Vector3(weaponTransform.localScale.x, -1 * weaponTransform.localScale.y, weaponTransform.localScale.z);
    }
}
