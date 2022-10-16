using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;
using Codebase.Infrastructure.Services.Factories;
using Codebase.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Audio
{
    public class AudioService : IAudioService
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly AudioSource _audioSource;
        private AudioPopup _audioPopup;
        
        private const string VolumeSaveKey = "AudioVolume";

        public AudioService(IAssetProvider assetProvider, ISaveLoadService saveLoadService, IUiFactory uiFactory)
        {
            _saveLoadService = saveLoadService;
            
            _audioSource = assetProvider.Instantiate<AudioSource>(AssetPath.AudioSourcePath);
            Object.DontDestroyOnLoad(_audioSource);
            
            _audioPopup = uiFactory.GetAudioPopup();
            LoadVolume();
            _audioPopup.VolumeChangedEvent += AudioPopup_OnVolumeChangedEvent;
        }

        private void LoadVolume()
        {
            float volume = _saveLoadService.LoadFloat(VolumeSaveKey, 0.5f);
            _audioSource.volume = volume;
            _audioPopup.SetVolumeViewValue(volume);
        }

        private void AudioPopup_OnVolumeChangedEvent(float newVolumeValue)
        {
            _audioSource.volume = newVolumeValue;
            _saveLoadService.SaveFloat(VolumeSaveKey, newVolumeValue);
        }
    }
}