using UnityEngine;
using UnityEngine.UI;

public class SelectItem3 : MonoBehaviour {

    public float triggerDuration = 2;
    private bool _isTriggered;
    public bool goToScene6A;
    public bool goToScene6B;

    public Vector3 scene6ADirection = new Vector3(0.8f, -0.6f, 0.3f);
    public GameObject scene6ACanvas;
    public Image scene6ATriggerIcon;
    private float _scene6ATriggerTimer;

    public Vector3 scene6BDirection = new Vector3(0.9f, -0.4f, -0.1f);
    public GameObject scene6BCanvas;
    public Image scene6BTriggerIcon;
    private float _scene6BTriggerTimer;

    private void Update() {

        Vector3 cameraDirection = transform.forward;

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene6ADirection) > 0.97) {
            _scene6ATriggerTimer += Time.deltaTime;
            scene6ATriggerIcon.fillAmount = _scene6ATriggerTimer / triggerDuration;
            if (_scene6ATriggerTimer > triggerDuration) {
                _isTriggered = true;
                goToScene6A = true;
            }
        }
        else {
            _scene6ATriggerTimer = 0;
            scene6ATriggerIcon.fillAmount = 0;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene6BDirection) > 0.97) {
            _scene6BTriggerTimer += Time.deltaTime;
            scene6BTriggerIcon.fillAmount = _scene6BTriggerTimer / triggerDuration;
            if (_scene6BTriggerTimer > triggerDuration) {
                _isTriggered = true;
                goToScene6B = true;
            }
        }
        else {
            _scene6BTriggerTimer = 0;
            scene6BTriggerIcon.fillAmount = 0;
        }

    }

    private void OnEnable() {
        scene6ACanvas.SetActive(true);
        scene6BCanvas.SetActive(true);
    }

    private void OnDisable() {
        if (scene6ACanvas) scene6ACanvas.SetActive(false);
        if (scene6BCanvas) scene6BCanvas.SetActive(false);
        _scene6ATriggerTimer = 0;
        _scene6BTriggerTimer = 0;
        _isTriggered = false;
        goToScene6A = false;
        goToScene6B = false;
    }

}
