using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem3 : MonoBehaviour {

    public float triggerDuration = 2;
    public float activeDuration = 10;
    private bool _isTriggered;
    public bool goToScene6A;
    public bool goToScene6B;

    public Vector3 photoDirection = new Vector3(0.8f, -0.6f, 0.3f);
    public GameObject photoCanvas;
    public Image photoTriggerIcon;
    private float _photoTriggerTimer;
    public GameObject photoTriggerImage;

    public Vector3 headsetDirection = new Vector3(0.9f, -0.4f, -0.1f);
    public GameObject headsetCanvas;
    public Image headsetTriggerIcon;
    private float _headsetTriggerTimer;
    public GameObject headsetTriggerImage;

    private void Update() {

        Vector3 cameraDirection = transform.forward;

        if (!_isTriggered && Vector3.Dot(cameraDirection, headsetDirection) > 0.97) {
            _headsetTriggerTimer += Time.deltaTime;
            headsetTriggerIcon.fillAmount = _headsetTriggerTimer / triggerDuration;
            if (_headsetTriggerTimer > triggerDuration) {
                // headsetTriggerImage.SetActive(true);
                _isTriggered = true;
                // StartCoroutine(WaitAndDeactivate(headsetTriggerImage));
                goToScene6A = true;
            }
        }
        else {
            _headsetTriggerTimer = 0;
            headsetTriggerIcon.fillAmount = 0;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, photoDirection) > 0.97) {
            _photoTriggerTimer += Time.deltaTime;
            photoTriggerIcon.fillAmount = _photoTriggerTimer / triggerDuration;
            if (_photoTriggerTimer > triggerDuration) {
                photoTriggerImage.SetActive(true);
                _isTriggered = true;
                StartCoroutine(WaitAndDeactivate(photoTriggerImage));
            }
        }
        else {
            _photoTriggerTimer = 0;
            photoTriggerIcon.fillAmount = 0;
        }

    }

    private IEnumerator WaitAndDeactivate(GameObject triggerImage) {
        yield return new WaitForSeconds(activeDuration);
        triggerImage.SetActive(false);
        _isTriggered = false;
    }

    private void OnEnable() {
        photoCanvas.SetActive(true);
        headsetCanvas.SetActive(true);
        _photoTriggerTimer = 0;
        _headsetTriggerTimer = 0;
        _isTriggered = false;
        goToScene6A = false;
        goToScene6B = false;
    }

    private void OnDisable() {
        photoCanvas.SetActive(false);
        headsetCanvas.SetActive(false);
        if (photoTriggerImage.activeSelf)
            photoTriggerImage.SetActive(false);
        if (headsetTriggerImage.activeSelf)
            headsetTriggerImage.SetActive(false);
    }

}
