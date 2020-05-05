using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem4 : MonoBehaviour {

    public float triggerDuration = 2;
    private bool _isTriggered;
    public bool goToTitle;
    public bool goToInteraction3;
    public GameObject blackScreen;
    private Image _blackScreenImage;

    public Vector3 titleDirection = new Vector3(-0.8f, -0.2f, -0.6f);
    public GameObject titleCanvas;
    public Image titleTriggerIcon;
    public Image titleTriggerTopIcon;
    private float _titleTriggerTimer;

    public Vector3 interaction3Direction = new Vector3(-0.2f, -0.2f, 1.0f);
    public GameObject interaction3Canvas;
    public Image interaction3TriggerIcon;
    public Image interaction3TriggerTopIcon;
    private float _interaction3TriggerTimer;

    private void Update() {

        Vector3 cameraDirection = transform.forward;
        Vector3 angles;

        if (!_isTriggered && Vector3.Dot(cameraDirection, titleDirection) > 0.97) {
            _titleTriggerTimer += Time.deltaTime;
            titleTriggerIcon.fillAmount = _titleTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _titleTriggerTimer / triggerDuration * 360);
            titleTriggerTopIcon.transform.localEulerAngles = angles;
            if (_titleTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToTitle"));
            }
        }
        else {
            _titleTriggerTimer = Mathf.Max(_titleTriggerTimer - Time.deltaTime, 0);
            titleTriggerIcon.fillAmount = _titleTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _titleTriggerTimer / triggerDuration * 360);
            titleTriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, interaction3Direction) > 0.97) {
            _interaction3TriggerTimer += Time.deltaTime;
            interaction3TriggerIcon.fillAmount = _interaction3TriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _interaction3TriggerTimer / triggerDuration * 360);
            interaction3TriggerTopIcon.transform.localEulerAngles = angles;
            if (_interaction3TriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToInteraction3"));
            }
        }
        else {
            _interaction3TriggerTimer = Mathf.Max(_interaction3TriggerTimer - Time.deltaTime, 0);
            interaction3TriggerIcon.fillAmount = _interaction3TriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _interaction3TriggerTimer / triggerDuration * 360);
            interaction3TriggerTopIcon.transform.localEulerAngles = angles;
        }

    }

    private IEnumerator FadeIn() {
        _isTriggered = true;
        for (var alpha = 1f; alpha >= -0.01f; alpha -= 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        titleCanvas.SetActive(true);
        interaction3Canvas.SetActive(true);
        _isTriggered = false;
    }

    private IEnumerator FadeOut(string goToX) {
        _isTriggered = true;
        if (titleCanvas) titleCanvas.SetActive(false);
        if (interaction3Canvas) interaction3Canvas.SetActive(false);
        for (var alpha = 0f; alpha <= 1.01f; alpha += 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        if (goToX == "goToTitle") goToTitle = true;
        else if (goToX == "goToInteraction3") goToInteraction3 = true;
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
        _titleTriggerTimer = 0;
        _interaction3TriggerTimer = 0;
        _isTriggered = false;
        goToTitle = false;
        goToInteraction3 = false;
    }

}
