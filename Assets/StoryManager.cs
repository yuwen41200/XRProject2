using UnityEngine;
using UnityEngine.Video;

public enum StoryState {
    Title, Scene1, Interaction1, Scene2, Scene3,
    Interaction2, Scene4A, Scene4B, Scene4C, Scene5,
    Interaction3, Scene6A, Scene6B, Credits
}

public class StoryManager : MonoBehaviour {

    public GameObject vrCamera;
    public GameObject debugCamera;
    private StoryState _storyState;
    private VideoPlayer _videoPlayer;
    private bool _videoEnded;
    public VideoClip[] storyVideos;
    private SelectItem1 _selectItem1;
    private SelectItem2 _selectItem2;
    private SelectItem3 _selectItem3;
    private SelectItem4 _selectItem4;
    private byte _scene4A4B4CHaveBeenPlayed;

    private void Awake() {
        if (!vrCamera.activeInHierarchy) vrCamera = debugCamera;
        _videoPlayer = GetComponent<VideoPlayer>();
        _selectItem1 = vrCamera.GetComponent<SelectItem1>();
        _selectItem2 = vrCamera.GetComponent<SelectItem2>();
        _selectItem3 = vrCamera.GetComponent<SelectItem3>();
        _selectItem4 = vrCamera.GetComponent<SelectItem4>();
        _storyState = StoryState.Title;
        _videoEnded = false;
        _videoPlayer.clip = storyVideos[0];
        _videoPlayer.isLooping = false;
        _videoPlayer.loopPointReached += EndReached;
        _videoPlayer.Play();
    }

    private void LateUpdate() {
        switch (_storyState) {

            case StoryState.Title:
                if (_videoEnded) {
                    _storyState = StoryState.Scene1;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[1];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                }
                break;

            case StoryState.Scene1:
                if (_videoEnded) {
                    _storyState = StoryState.Interaction1;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[2];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem1.enabled = true;
                }
                break;

            case StoryState.Interaction1:
                if (_selectItem1.goToScene2) {
                    _selectItem1.enabled = false;
                    _storyState = StoryState.Scene2;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[3];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                }
                break;

            case StoryState.Scene2:
                if (_videoEnded) {
                    _storyState = StoryState.Scene3;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[4];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                }
                break;

            case StoryState.Scene3:
                if (_videoEnded) {
                    _storyState = StoryState.Interaction2;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[5];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem2.enabled = true;
                }
                break;

            case StoryState.Interaction2:
                if (_selectItem2.goToScene4A) {
                    _selectItem2.enabled = false;
                    _storyState = StoryState.Scene4A;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[6];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _scene4A4B4CHaveBeenPlayed |= 0b001;
                }
                else if (_selectItem2.goToScene4B) {
                    _selectItem2.enabled = false;
                    _storyState = StoryState.Scene4B;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[7];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _scene4A4B4CHaveBeenPlayed |= 0b010;
                }
                else if (_selectItem2.goToScene4C) {
                    _selectItem2.enabled = false;
                    _storyState = StoryState.Scene4C;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[8];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _scene4A4B4CHaveBeenPlayed |= 0b100;
                }
                else if (_scene4A4B4CHaveBeenPlayed == 0b111) {
                    _selectItem2.enabled = false;
                    _storyState = StoryState.Scene5;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[9];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _scene4A4B4CHaveBeenPlayed = 0;
                }
                break;

            case StoryState.Scene4A:
                if (_videoEnded) {
                    _storyState = StoryState.Interaction2;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[5];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem2.enabled = true;
                }
                break;

            case StoryState.Scene4B:
                if (_videoEnded) {
                    _storyState = StoryState.Interaction2;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[5];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem2.enabled = true;
                }
                break;

            case StoryState.Scene4C:
                if (_videoEnded) {
                    _storyState = StoryState.Interaction2;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[5];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem2.enabled = true;
                }
                break;

            case StoryState.Scene5:
                if (_videoEnded) {
                    _storyState = StoryState.Interaction3;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[10];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem3.enabled = true;
                }
                break;

            case StoryState.Interaction3:
                if (_selectItem3.goToScene6A) {
                    _selectItem3.enabled = false;
                    _storyState = StoryState.Scene6A;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[11];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                }
                else if (_selectItem3.goToScene6B) {
                    _selectItem3.enabled = false;
                    _storyState = StoryState.Scene6B;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[12];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                }
                break;

            case StoryState.Scene6A:
                if (_videoEnded) {
                    _storyState = StoryState.Credits;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[13];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem4.enabled = true;
                }
                break;

            case StoryState.Scene6B:
                if (_videoEnded) {
                    _storyState = StoryState.Credits;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[13];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem4.enabled = true;
                }
                break;

            case StoryState.Credits:
                if (_selectItem4.goToTitle) {
                    _selectItem4.enabled = false;
                    _storyState = StoryState.Title;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[0];
                    _videoPlayer.isLooping = false;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                }
                else if (_selectItem4.goToInteraction3) {
                    _selectItem4.enabled = false;
                    _storyState = StoryState.Interaction3;
                    _videoEnded = false;
                    _videoPlayer.clip = storyVideos[10];
                    _videoPlayer.isLooping = true;
                    _videoPlayer.loopPointReached += EndReached;
                    _videoPlayer.Play();
                    _selectItem3.enabled = true;
                }
                break;

            default:
                Debug.LogError("Unknown story state: " + _storyState);
                break;

        }
    }

    private void EndReached(VideoPlayer vp) {
        _videoEnded = true;
    }

}
