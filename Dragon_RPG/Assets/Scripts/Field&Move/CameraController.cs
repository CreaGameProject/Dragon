using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject followTarget;
    private Vector3 offset;
	// Use this for initialization
	void Start () {
        offset = transform.position - followTarget.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = followTarget.transform.position + offset;
        //targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y,0f);
        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
	}
}
