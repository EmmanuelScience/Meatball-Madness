using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueShaft : MonoBehaviour
{
    public tongue tongue_tip;
    public GameObject tongue_base;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = (tongue_tip.transform.position + tongue_base.transform.position) / 2.0F;
        float scaleY = Vector3.Distance(tongue_tip.transform.position, tongue_base.transform.position);
        this.transform.localScale = new Vector3(0.1F, 0.5F * scaleY, 0.1F);
        transform.LookAt(tongue_tip.transform);
        transform.Rotate(90F, 0F, 0F);
    }
}
