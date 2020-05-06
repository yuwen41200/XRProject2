using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem3 : MonoBehaviour {

    public float triggerDuration = 2;
    private bool _isTriggered;
    public bool goToScene6A;
    public bool goToScene6B;
    public GameObject blackScreen;
    private Image _blackScreenImage;

    public Vector3 scene6ADirection = new Vector3(0.7f, 0.1f, -0.7f);
    public GameObject scene6ACanvas;
    public Image scene6ATriggerIcon;
    public Image scene6ATriggerTopIcon;
    private float _scene6ATriggerTimer;

    public Vector3 scene6BDirection = new Vector3(-1.0f, 0.1f, 0.0f);
    public GameObject scene6BCanvas;
    public Image scene6BTriggerIcon;
    public Image scene6BTriggerTopIcon;
    private float _scene6BTriggerTimer;

    private void Update() {

        Vector3 cameraDirection = transform.forward;
        Vector3 angles;

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene6ADirection) > 0.97) {
            _scene6ATriggerTimer += Time.deltaTime;
            scene6ATriggerIcon.fillAmount = _scene6ATriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene6ATriggerTimer / triggerDuration * 360);
            scene6ATriggerTopIcon.transform.localEulerAngles = angles;
            if (_scene6ATriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene6A"));
            }
        }
        else {
            _scene6ATriggerTimer = Mathf.Max(_scene6ATriggerTimer - Time.deltaTime, 0);
            scene6ATriggerIcon.fillAmount = _scene6ATriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene6ATriggerTimer / triggerDuration * 360);
            scene6ATriggerTopIcon.transform.localEulerAngles = angles;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene6BDirection) > 0.97) {
            _scene6BTriggerTimer += Time.deltaTime;
            scene6BTriggerIcon.fillAmount = _scene6BTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene6BTriggerTimer / triggerDuration * 360);
            scene6BTriggerTopIcon.transform.localEulerAngles = angles;
            if (_scene6BTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene6B"));
            }
        }
        else {
            _scene6BTriggerTimer = Mathf.Max(_scene6BTriggerTimer - Time.deltaTime, 0);
            scene6BTriggerIcon.fillAmount = _scene6BTriggerTimer / triggerDuration;
            angles = new Vector3(0, 0, 90 - _scene6BTriggerTimer / triggerDuration * 360);
            scene6BTriggerTopIcon.transform.localEulerAngles = angles;
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
        scene6ACanvas.SetActive(true);
        scene6BCanvas.SetActive(true);
        _isTriggered = false;
    }

    private IEnumerator FadeOut(string goToX) {
        _isTriggered = true;
        if (scene6ACanvas) scene6ACanvas.SetActive(false);
        if (scene6BCanvas) scene6BCanvas.SetActive(false);
        for (var alpha = 0f; alpha <= 1.01f; alpha += 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.05f);
        }
        if (goToX == "goToScene6A") goToScene6A = true;
        else if (goToX == "goToScene6B") goToScene6B = true;
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
        _scene6ATriggerTimer = 0;
        _scene6BTriggerTimer = 0;
        _isTriggered = false;
        goToScene6A = false;
        goToScene6B = false;
    }

}
