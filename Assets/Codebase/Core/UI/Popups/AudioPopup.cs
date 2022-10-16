using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.Core.UI.Popups
{
    public class AudioPopup : Popup
    {
        [SerializeField] private Slider _audioVolume;
        [SerializeField] private TMP_Text _audioVolumeLabel;
        [SerializeField] private string _labelText;

        public event Action<float> VolumeChangedEvent;

        protected override void OnOpenPopup()
        {
            base.OnOpenPopup();
            _audioVolume.onValueChanged.AddListener(OnSliderValueChanged);
        }

        protected override void OnClosePopup()
        {
            base.OnClosePopup();
            _audioVolume.onValueChanged.RemoveListener(OnSliderValueChanged);
        }

        private void OnSliderValueChanged(float newValue)
        {
            SetVolumeViewValue(newValue);
            VolumeChangedEvent?.Invoke(newValue);
        }

        public void SetVolumeViewValue(float volumeValue)
        {
            _audioVolumeLabel.text = $"{_labelText} {volumeValue*100:0}%";
            _audioVolume.value = volumeValue;
        }
    }
}