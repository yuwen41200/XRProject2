using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem1 : MonoBehaviour {

    public float triggerDuration = 2;
    public float activeDuration = 10;
    private bool _isTriggered;
    public bool needToBeDisabled; // TODO

    public Vector3 photoDirection = new Vector3(0.8f, -0.6f, 0.3f);
    public Image photoTriggerIcon;
    private float _photoTriggerTimer;
    public GameObject photoTriggerImage;

    public Vector3 headsetDirection = new Vector3(0.9f, -0.4f, -0.1f);
    public Image headsetTriggerIcon;
    private float _headsetTriggerTimer;
    public GameObject headsetTriggerImage;

    private void Update() {

        Vector3 cameraDirection = transform.forward;

        if (!_isTriggered && Vector3.Dot(cameraDirection, headsetDirection) > 0.97) {
            _headsetTriggerTimer += Time.deltaTime;
            headsetTriggerIcon.fillAmount = _headsetTriggerTimer / triggerDuration;
            if (_headsetTriggerTimer > triggerDuration) {
                headsetTriggerImage.SetActive(true);
                _isTriggered = true;
                StartCoroutine(WaitAndDeactivate(headsetTriggerImage));
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

}
