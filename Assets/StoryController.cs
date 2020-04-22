using System;
using UnityEngine;
using UnityEngine.Video;

public class StoryController : MonoBehaviour {

    private VideoPlayer _videoPlayer;

    private void Awake() {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update() {
        // Debug.Log("Video playback time: " + Math.Floor(_videoPlayer.time));
        if (_videoPlayer.time > 45) {
            _videoPlayer.Pause();
        }
    }

}
