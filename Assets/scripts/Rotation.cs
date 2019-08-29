using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    #region Init Variable
    public float rotationSpeed = 100f;
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0f,0f,rotationSpeed * Time.deltaTime);
    }

    
}
