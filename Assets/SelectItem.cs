using UnityEngine;
using UnityEngine.UI;

public class SelectItem : MonoBehaviour {

    public float triggerDuration = 2;

    public Vector3 photoDirection = new Vector3(0.8f, -0.6f, 0.3f);
    public Image photoTriggerIcon;
    private float _photoTriggerTimer;

    public Vector3 headsetDirection = new Vector3(0.9f, -0.4f, -0.1f);
    public Image headsetTriggerIcon;
    private float _headsetTriggerTimer;

    void Update() {
        Vector3 cameraDirection = transform.forward;
        if (Vector3.Dot(cameraDirection, headsetDirection) > 0.95) {
            _headsetTriggerTimer += Time.deltaTime;
            headsetTriggerIcon.fillAmount = _headsetTriggerTimer / triggerDuration;
            if (_headsetTriggerTimer > triggerDuration) {
                // triggered
            }
        }
        else {
            _headsetTriggerTimer = 0;
            headsetTriggerIcon.fillAmount = _headsetTriggerTimer / triggerDuration;
        }
    }

}
