﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectItem2 : MonoBehaviour {

    public float triggerDuration = 2;
    private bool _isTriggered;
    public bool goToScene4A;
    public bool goToScene4B;
    public bool goToScene4C;
    public GameObject blackScreen;
    private Image _blackScreenImage;

    public Vector3 scene4ADirection = new Vector3(-0.8f, 0.2f, 0.6f);
    public GameObject scene4ACanvas;
    public Image scene4ATriggerIcon;
    private float _scene4ATriggerTimer;

    public Vector3 scene4BDirection = new Vector3(0.4f, -0.2f, -0.9f);
    public GameObject scene4BCanvas;
    public Image scene4BTriggerIcon;
    private float _scene4BTriggerTimer;

    public Vector3 scene4CDirection = new Vector3(0.7f, 0.1f, 0.8f);
    public GameObject scene4CCanvas;
    public Image scene4CTriggerIcon;
    private float _scene4CTriggerTimer;

    private void Update() {

        Vector3 cameraDirection = transform.forward;

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene4ADirection) > 0.97) {
            _scene4ATriggerTimer += Time.deltaTime;
            scene4ATriggerIcon.fillAmount = _scene4ATriggerTimer / triggerDuration;
            if (_scene4ATriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene4A"));
            }
        }
        else {
            _scene4ATriggerTimer = 0;
            scene4ATriggerIcon.fillAmount = 0;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene4BDirection) > 0.97) {
            _scene4BTriggerTimer += Time.deltaTime;
            scene4BTriggerIcon.fillAmount = _scene4BTriggerTimer / triggerDuration;
            if (_scene4BTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene4B"));
            }
        }
        else {
            _scene4BTriggerTimer = 0;
            scene4BTriggerIcon.fillAmount = 0;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, scene4CDirection) > 0.97) {
            _scene4CTriggerTimer += Time.deltaTime;
            scene4CTriggerIcon.fillAmount = _scene4CTriggerTimer / triggerDuration;
            if (_scene4CTriggerTimer > triggerDuration) {
                StartCoroutine(FadeOut("goToScene4C"));
            }
        }
        else {
            _scene4CTriggerTimer = 0;
            scene4CTriggerIcon.fillAmount = 0;
        }

    }

    private IEnumerator FadeIn() {
        _isTriggered = true;
        for (var alpha = 1f; alpha >= 0; alpha -= 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
        scene4ACanvas.SetActive(true);
        scene4BCanvas.SetActive(true);
        scene4CCanvas.SetActive(true);
        _isTriggered = false;
    }

    private IEnumerator FadeOut(string goToX) {
        _isTriggered = true;
        if (scene4ACanvas) scene4ACanvas.SetActive(false);
        if (scene4BCanvas) scene4BCanvas.SetActive(false);
        if (scene4CCanvas) scene4CCanvas.SetActive(false);
        for (var alpha = 0f; alpha <= 1; alpha += 0.025f) {
            var newColor = _blackScreenImage.color;
            newColor.a = alpha;
            _blackScreenImage.color = newColor;
            yield return new WaitForSeconds(0.1f);
        }
        if (goToX == "goToScene4A") goToScene4A = true;
        else if (goToX == "goToScene4B") goToScene4B = true;
        else if (goToX == "goToScene4C") goToScene4C = true;
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
        _isTriggered = false;
        goToScene4A = false;
        goToScene4B = false;
        goToScene4C = false;
    }

}
