using UnityEngine;
using UnityEngine.Video;

public enum StoryState {
    Title, Scene1, Interaction1, Scene2
}

public class StoryController : MonoBehaviour {

    public GameObject vrCamera;
    private StoryState _storyState;
    private VideoPlayer _videoPlayer;
    private SelectItem1 _selectItem1;

    private void Awake() {
        _storyState = StoryState.Title;
        _videoPlayer = GetComponent<VideoPlayer>();
        _selectItem1 = vrCamera.GetComponent<SelectItem1>();
    }

    private void Update() {
        switch (_storyState) {
            case StoryState.Title:
                _storyState = StoryState.Scene1;
                break;
            case StoryState.Scene1:
                if (_videoPlayer.time > 45) {
                    _videoPlayer.Pause();
                    _selectItem1.enabled = true;
                    _storyState = StoryState.Interaction1;
                }
                break;
            case StoryState.Interaction1:
                if (_selectItem1.enabled && _selectItem1.needToBeDisabled) {
                    _selectItem1.enabled = false;
                    _storyState = StoryState.Scene2;
                }
                break;
            case StoryState.Scene2:
                break;
            default:
                Debug.LogError("Unknown story state: " + _storyState);
                break;
        }
    }

}
