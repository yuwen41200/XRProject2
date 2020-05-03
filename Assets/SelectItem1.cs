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

    public Vector3 headsetDirection = new Vector3(-0.6f, -0.7f, 0.5f);
    public GameObject headsetCanvas;
    public Image headsetTriggerIcon;
    private float _headsetTriggerTimer;

    public Vector3 photoDirection = new Vector3(0.8f, -0.6f, 0.3f);
    public GameObject photoCanvas;
    public Image photoTriggerIcon;
    private float _photoTriggerTimer;
    public GameObject photoTriggerImage;

    public Vector3 phoneDirection = new Vector3(-0.6f, -0.7f, 0.5f);
    public GameObject phoneCanvas;
    public Image phoneTriggerIcon;
    private float _phoneTriggerTimer;
    public GameObject phoneTriggerImage;

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
                StartCoroutine(TriggerItem(photoTriggerImage));
            }
        }
        else {
            _photoTriggerTimer = 0;
            photoTriggerIcon.fillAmount = 0;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, phoneDirection) > 0.97) {
            _phoneTriggerTimer += Time.deltaTime;
            phoneTriggerIcon.fillAmount = _phoneTriggerTimer / triggerDuration;
            if (_phoneTriggerTimer > triggerDuration) {
                StartCoroutine(TriggerItem(phoneTriggerImage));
            }
        }
        else {
            _phoneTriggerTimer = 0;
            phoneTriggerIcon.fillAmount = 0;
        }

    }

    private IEnumerator TriggerItem(GameObject triggerImage) {

        var tImage = triggerImage.GetComponent<Image>();
        var tAudio = triggerImage.GetComponent<AudioSource>();
        Color newColor;

        _isTriggered = true;
        triggerImage.SetActive(true);
        for (var alpha = 0f; alpha <= 1; alpha += 0.05f) {
            newColor = tImage.color;
            newColor.a = alpha;
            tImage.color = newColor;
            newColor = _blackScreenImage.color;
            newColor.a = Mathf.Min(alpha, 0.5f);
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }

        tAudio.Play();
        yield return new WaitForSeconds(tAudio.clip.length + additionalActiveDuration);

        for (var alpha = 1f; alpha >= 0; alpha -= 0.05f) {
            newColor = tImage.color;
            newColor.a = alpha;
            tImage.color = newColor;
            newColor = _blackScreenImage.color;
            newColor.a = Mathf.Min(alpha, 0.5f);
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
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
        _headsetTriggerTimer = 0;
        _photoTriggerTimer = 0;
        _phoneTriggerTimer = 0;
        _isTriggered = false;
        goToScene2 = false;
    }

}
