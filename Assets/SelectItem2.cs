using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem2 : MonoBehaviour {

    public float triggerDuration = 2;
    private bool _isTriggered;
    public bool goToScene4A;
    public bool goToScene4B;
    public bool goToScene4C;
    public bool goToScene5;
    public GameObject blackScreen;
    private Image _blackScreenImage;

    public Vector3 scene4ADirection = new Vector3(-0.8f, 0.1f, 0.6f);
    public GameObject scene4ACanvas;
    public Image scene4ATriggerIcon;
    public Image scene4ATriggerTopIcon;
    private float _scene4ATriggerTimer;

    public Vector3 scene4BDirection = new Vector3(0.7f, 0.0f, 0.7f);
    public GameObject scene4BCanvas;
    public Image scene4BTriggerIcon;
    public Image scene4BTriggerTopIcon;
    private float _scene4BTriggerTimer;

    public Vector3 scene4CDirection = new Vector3(0.1f, 0.2f, -1.0f);
    public GameObject scene4CCanvas;
    public Image scene4CTriggerIcon;
    public Image scene4CTriggerTopIcon;
    private float _scene4CTriggerTimer;

    public Vector3 scene5Direction = new Vector3(0.0f, -1.0f, 0.0f);
    public GameObject scene5Canvas;
    public Image scene5TriggerIcon;
    public Image scene5TriggerTopIcon;
    private float _scene5TriggerTimer;

    private void Update() {

        Vector3 cameraDirection = transform.forward;
        Vector3 angles;

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene4ADirection) > 0.97) {
            _scene4ATriggerTimer += Time.deltaTime;
            scene4ATriggerIcon.fillAmount = _scene4ATriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene4ATriggerTimer / triggerDuration * 360);
            scene4ATriggerTopIcon.transform.localEulerAngles = angles;
            if (_scene4ATriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene4A"));
            }
        }
        else {
            _scene4ATriggerTimer = Mathf.Max(_scene4ATriggerTimer - Time.deltaTime, 0);
            scene4ATriggerIcon.fillAmount = _scene4ATriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene4ATriggerTimer / triggerDuration * 360);
            scene4ATriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene4BDirection) > 0.97) {
            _scene4BTriggerTimer += Time.deltaTime;
            scene4BTriggerIcon.fillAmount = _scene4BTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene4BTriggerTimer / triggerDuration * 360);
            scene4BTriggerTopIcon.transform.localEulerAngles = angles;
            if (_scene4BTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene4B"));
            }
        }
        else {
            _scene4BTriggerTimer = Mathf.Max(_scene4BTriggerTimer - Time.deltaTime, 0);
            scene4BTriggerIcon.fillAmount = _scene4BTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene4BTriggerTimer / triggerDuration * 360);
            scene4BTriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene4CDirection) > 0.97) {
            _scene4CTriggerTimer += Time.deltaTime;
            scene4CTriggerIcon.fillAmount = _scene4CTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene4CTriggerTimer / triggerDuration * 360);
            scene4CTriggerTopIcon.transform.localEulerAngles = angles;
            if (_scene4CTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene4C"));
            }
        }
        else {
            _scene4CTriggerTimer = Mathf.Max(_scene4CTriggerTimer - Time.deltaTime, 0);
            scene4CTriggerIcon.fillAmount = _scene4CTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene4CTriggerTimer / triggerDuration * 360);
            scene4CTriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene5Direction) > 0.97) {
            _scene5TriggerTimer += Time.deltaTime;
            scene5TriggerIcon.fillAmount = _scene5TriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene5TriggerTimer / triggerDuration * 360);
            scene5TriggerTopIcon.transform.localEulerAngles = angles;
            if (_scene5TriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene5"));
            }
        }
        else {
            _scene5TriggerTimer = Mathf.Max(_scene5TriggerTimer - Time.deltaTime, 0);
            scene5TriggerIcon.fillAmount = _scene5TriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene5TriggerTimer / triggerDuration * 360);
            scene5TriggerTopIcon.transform.localEulerAngles = angles;
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
        scene4ACanvas.SetActive(true);
        scene4BCanvas.SetActive(true);
        scene4CCanvas.SetActive(true);
        scene5Canvas.SetActive(true);
        _isTriggered = false;
    }

    private IEnumerator FadeOut(string goToX) {
        _isTriggered = true;
        if (scene4ACanvas) scene4ACanvas.SetActive(false);
        if (scene4BCanvas) scene4BCanvas.SetActive(false);
        if (scene4CCanvas) scene4CCanvas.SetActive(false);
        if (scene5Canvas) scene5Canvas.SetActive(false);
        for (var alpha = 0f; alpha <= 1.01f; alpha += 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        if (goToX == "goToScene4A") goToScene4A = true;
        else if (goToX == "goToScene4B") goToScene4B = true;
        else if (goToX == "goToScene4C") goToScene4C = true;
        else if (goToX == "goToScene5") goToScene5 = true;
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
        _scene4ATriggerTimer = 0;
        _scene4BTriggerTimer = 0;
        _scene4CTriggerTimer = 0;
        _scene5TriggerTimer = 0;
        _isTriggered = false;
        goToScene4A = false;
        goToScene4B = false;
        goToScene4C = false;
        goToScene5 = false;
    }

}
