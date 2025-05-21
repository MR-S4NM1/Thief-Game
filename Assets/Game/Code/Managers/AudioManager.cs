using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region References

    public static AudioManager instance;

    [Header("Audio Source")]
    [SerializeField] protected AudioSource _audioSource;

    [Header("Audio Clips")]
    [SerializeField] protected AudioClip _camouflageAudioClip;
    [SerializeField] protected AudioClip _hologramAudioClip;
    [SerializeField] protected AudioClip _doorAudioClip;
    [SerializeField] protected AudioClip _pickUpAudioClip;
    [SerializeField] protected AudioClip _shotAudioClip;
    [SerializeField] protected AudioClip _alertAudioClip;
    #endregion

    #region Knobs

    [Header("Audio Volume Scale")]
    [SerializeField] protected float _camouflageAudioVolumeScale;
    [SerializeField] protected float _hologramAudioVolumeScale;
    [SerializeField] protected float _doorAudioVolumeScale;
    [SerializeField] protected float _pickUpAudioVolumeScale;
    [SerializeField] protected float _shotAudioVolumeScale;
    [SerializeField] protected float _alertAudioVolumeScale;

    #endregion

    #region UnityMethods
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnDrawGizmos()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    #endregion

    #region PublicMethods

    public void PlayAudio(string p_audioName)
    {
        switch (p_audioName)
        {
            case "Door":
                _audioSource?.PlayOneShot(_doorAudioClip, _doorAudioVolumeScale);
                break;

            case "Camouflage":
                _audioSource?.PlayOneShot(_camouflageAudioClip, _camouflageAudioVolumeScale);
                break;

            case "Hologram":
                _audioSource?.PlayOneShot(_hologramAudioClip, _hologramAudioVolumeScale);
                break;

            case "PickUp":
                _audioSource?.PlayOneShot(_pickUpAudioClip, _pickUpAudioVolumeScale);
                break;

            case "Shoot":
                _audioSource?.PlayOneShot(_shotAudioClip, _shotAudioVolumeScale);
                break;

            case "Alert":
                _audioSource?.PlayOneShot(_alertAudioClip, _alertAudioVolumeScale);
                break;
        }
    }

    #endregion
}
