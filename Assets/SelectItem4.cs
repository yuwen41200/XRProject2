using UnityEngine;
using UnityEngine.UI;

public class SelectItem4 : MonoBehaviour {

    public float triggerDuration = 2;
    private bool _isTriggered;
    public bool goToTitle;
    public bool goToInteraction3;

    public Vector3 titleDirection = new Vector3(0.8f, -0.6f, 0.3f);
    public GameObject titleCanvas;
    public Image titleTriggerIcon;
    private float _titleTriggerTimer;

    public Vector3 interaction3Direction = new Vector3(0.9f, -0.4f, -0.1f);
    public GameObject interaction3Canvas;
    public Image interaction3TriggerIcon;
    private float _interaction3TriggerTimer;

    private void Update() {

        Vector3 cameraDirection = transform.forward;

        if (!_isTriggered && Vector3.Dot(cameraDirection, titleDirection) > 0.97) {
            _titleTriggerTimer += Time.deltaTime;
            titleTriggerIcon.fillAmount = _titleTriggerTimer / triggerDuration;
            if (_titleTriggerTimer > triggerDuration) {
                _isTriggered = true;
                goToTitle = true;
            }
        }
        else {
            _titleTriggerTimer = 0;
            titleTriggerIcon.fillAmount = 0;
        }

        if (!_isTriggered && Vector3.Dot(cameraDirection, interaction3Direction) > 0.97) {
            _interaction3TriggerTimer += Time.deltaTime;
            interaction3TriggerIcon.fillAmount = _interaction3TriggerTimer / triggerDuration;
            if (_interaction3TriggerTimer > triggerDuration) {
                _isTriggered = true;
                goToInteraction3 = true;
            }
        }
        else {
            _interaction3TriggerTimer = 0;
            interaction3TriggerIcon.fillAmount = 0;
        }

    }

    private void OnEnable() {
        titleCanvas.SetActive(true);
        interaction3Canvas.SetActive(true);
    }

    private void OnDisable() {
        titleCanvas.SetActive(false);
        interaction3Canvas.SetActive(false);
        _titleTriggerTimer = 0;
        _interaction3TriggerTimer = 0;
        _isTriggered = false;
        goToTitle = false;
        goToInteraction3 = false;
    }

}
