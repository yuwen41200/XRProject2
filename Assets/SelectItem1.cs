using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem1 : MonoBehaviour {

    public float triggerDuration = 2;
    public float additionalActiveDuration = 3.5f;
    private bool _isTriggered;
    public bool goToScene2;
    public GameObject blackScreen;
    private Image _blackScreenImage;

    public Vector3 headsetDirection = new Vector3(-0.7f, -0.6f, 0.5f);
    public GameObject headsetCanvas;
    public Image headsetTriggerIcon;
    public Image headsetTriggerTopIcon;
    private float _headsetTriggerTimer;

    public Vector3 photoDirection = new Vector3(-0.5f, -0.9f, 0.1f);
    public GameObject photoCanvas;
    public Image photoTriggerIcon;
    public Image photoTriggerTopIcon;
    private float _photoTriggerTimer;
    public GameObject photoTriggerImage;

    public Vector3 phoneDirection = new Vector3(-0.4f, -0.8f, -0.5f);
    public GameObject phoneCanvas;
    public Image phoneTriggerIcon;
    public Image phoneTriggerTopIcon;
    private float _phoneTriggerTimer;
    public GameObject phoneTriggerImage;

    private void Update() {

        Vector3 cameraDirection = transform.forward;
        Vector3 angles;

        if (!_isTriggered && Vector3.Dot(cameraDirection, headsetDirection) > 0.97) {
            _headsetTriggerTimer += Time.deltaTime;
            headsetTriggerIcon.fillAmount = _headsetTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _headsetTriggerTimer / triggerDuration * 360);
            headsetTriggerTopIcon.transform.localEulerAngles = angles;
            if (_headsetTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut());
            }
        }
        else {
            _headsetTriggerTimer = Mathf.Max(_headsetTriggerTimer - Time.deltaTime, 0);
            headsetTriggerIcon.fillAmount = _headsetTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _headsetTriggerTimer / triggerDuration * 360);
            headsetTriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, photoDirection) > 0.97) {
            _photoTriggerTimer += Time.deltaTime;
            photoTriggerIcon.fillAmount = _photoTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _photoTriggerTimer / triggerDuration * 360);
            photoTriggerTopIcon.transform.localEulerAngles = angles;
            if (_photoTriggerTimer > triggerDuration) {
                StartCoroutine(TriggerItem(photoTriggerImage));
            }
        }
        else {
            _photoTriggerTimer = Mathf.Max(_photoTriggerTimer - Time.deltaTime, 0);
            photoTriggerIcon.fillAmount = _photoTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _photoTriggerTimer / triggerDuration * 360);
            photoTriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, phoneDirection) > 0.97) {
            _phoneTriggerTimer += Time.deltaTime;
            phoneTriggerIcon.fillAmount = _phoneTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _phoneTriggerTimer / triggerDuration * 360);
            phoneTriggerTopIcon.transform.localEulerAngles = angles;
            if (_phoneTriggerTimer > triggerDuration) {
                StartCoroutine(TriggerItem(phoneTriggerImage));
            }
        }
        else {
            _phoneTriggerTimer = Mathf.Max(_phoneTriggerTimer - Time.deltaTime, 0);
            phoneTriggerIcon.fillAmount = _phoneTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _phoneTriggerTimer / triggerDuration * 360);
            phoneTriggerTopIcon.transform.localEulerAngles = angles;
        }

    }

    private IEnumerator TriggerItem(GameObject triggerImage) {

        var tImage = triggerImage.GetComponent<Image>();
        var tAudio = triggerImage.GetComponent<AudioSource>();
        Color newColor;

        _isTriggered = true;
        triggerImage.SetActive(true);
        for (var alpha = 0f; alpha <= 1.01f; alpha += 0.05f) {
            newColor = tImage.color;
            newColor.a = alpha;
            tImage.color = newColor;
            newColor = _blackScreenImage.color;
            newColor.a = Mathf.Min(alpha, 0.95f);
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }

        tAudio.Play();
        yield return new WaitForSeconds(tAudio.clip.length + additionalActiveDuration);

        for (var alpha = 1f; alpha >= -0.01f; alpha -= 0.05f) {
            newColor = tImage.color;
            newColor.a = alpha;
            tImage.color = newColor;
            newColor = _blackScreenImage.color;
            newColor.a = Mathf.Min(alpha, 0.95f);
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        triggerImage.SetActive(false);
        _isTriggered = false;

    }

    private IEnumerator FadeIn() {
        _isTriggered = true;
        for (var alpha = 1f; alpha >= -0.01f; alpha -= 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        headsetCanvas.SetActive(true);
        photoCanvas.SetActive(true);
        phoneCanvas.SetActive(true);
        _isTriggered = false;
    }

    private IEnumerator FadeOut() {
        _isTriggered = true;
        if (headsetCanvas) headsetCanvas.SetActive(false);
        if (photoCanvas) photoCanvas.SetActive(false);
        if (phoneCanvas) phoneCanvas.SetActive(false);
        for (var alpha = 0f; alpha <= 1.01f; alpha += 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
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
        _headsetTriggerTimer = 0;
        _photoTriggerTimer = 0;
        _phoneTriggerTimer = 0;
        _isTriggered = false;
        goToScene2 = false;
    }

}
