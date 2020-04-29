using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem1 : MonoBehaviour {

    public float triggerDuration = 2;
    public float activeDuration = 10;
    private bool _isTriggered;
    public bool goToScene2;
    public GameObject blackScreen;
    private Image _blackScreenImage;

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
                StartCoroutine(FadeOut());
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

    private IEnumerator FadeIn() {
        _isTriggered = true;
        for (var alpha = 1f; alpha >= 0; alpha -= 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
        photoCanvas.SetActive(true);
        headsetCanvas.SetActive(true);
        _isTriggered = false;
    }

    private IEnumerator FadeOut() {
        _isTriggered = true;
        if (photoCanvas) photoCanvas.SetActive(false);
        if (headsetCanvas) headsetCanvas.SetActive(false);
        for (var alpha = 0f; alpha <= 1; alpha += 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
        goToScene2 = true;
    }

    private void OnEnable() {
        _blackScreenImage = blackScreen.GetComponent<Image>();
        StartCoroutine(FadeIn());
    }

    private void OnDisable() {
        StopAllCoroutines();
        if (_blackScreenImage) {
            var resetColor = _blackScreenImage.color;
            resetColor.a = 0;
            _blackScreenImage.color = resetColor;
        }
        _photoTriggerTimer = 0;
        _headsetTriggerTimer = 0;
        _isTriggered = false;
        goToScene2 = false;
    }

}
