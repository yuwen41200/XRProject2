using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour {

    [SerializeField] private float unitPerSecond = 25;

    // Start is called before the first frame update
    private void Start() {
    }

    // Update is called once per frame
    private void Update() {
        Debug.Log("Camera Direction: " + transform.forward);
        var eulers = new Vector3(-Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), 0);
        eulers *= Time.deltaTime * unitPerSecond;
        transform.Rotate(eulers, Space.Self);
    }

}
