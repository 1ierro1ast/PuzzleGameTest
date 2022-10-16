using Codebase.Core.UI.Popups;
using Codebase.Infrastructure.Services.AssetManagement;
using UnityEngine;

namespace Codebase.Infrastructure.Services.Factories
{
    public class UiFactory : IUiFactory
    {
        private IAssetProvider _assetProvider;

        private Canvas _mainCanvas;
        private StartPopup _startPopup;
        private OverlayPopup _overlayPopup;
        private WinPopup _winPopup;
        private LosePopup _losePopup;
        private AudioPopup _audioPopup;

        public UiFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            InitializePopups();
        }

        private void InitializePopups()
        {
            CreateMainCanvas();
            GetAudioPopup();
            GetOverlayPopup();
            GetStartPopup();
            GetWinPopup();
            GetLosePopup();
        }

        private void CreateMainCanvas()
        {
            _mainCanvas = _assetProvider.Instantiate<Canvas>(AssetPath.MainCanvasPath);
            Object.DontDestroyOnLoad(_mainCanvas);
        }

        public StartPopup GetStartPopup()
        {
            if (_startPopup == null)
            {
                _startPopup = _assetProvider.Instantiate<StartPopup>(AssetPath.StartPopupPath);
                _startPopup.transform.SetParent(_mainCanvas.transform);
                _startPopup.GetComponent<Canvas>().overrideSorting = true;

                _startPopup.Construct(_audioPopup);
            }
            return _startPopup;
        }

        public OverlayPopup GetOverlayPopup()
        {
            if (_overlayPopup == null)
            {
                _overlayPopup = _assetProvider.Instantiate<OverlayPopup>(AssetPath.OverlayPopupPath);
                _overlayPopup.transform.SetParent(_mainCanvas.transform);
                _overlayPopup.GetComponent<Canvas>().overrideSorting = true;

                _overlayPopup.Construct(_audioPopup);
            }
            return _overlayPopup;
        }

        public WinPopup GetWinPopup()
        {
            if (_winPopup == null)
            {
                _winPopup = _assetProvider.Instantiate<WinPopup>(AssetPath.WinPopupPath);
                _winPopup.transform.SetParent(_mainCanvas.transform);
                _winPopup.GetComponent<Canvas>().overrideSorting = true;

            }
            return _winPopup;
        }

        public LosePopup GetLosePopup()
        {
            if (_losePopup == null)
            {
                _losePopup = _assetProvider.Instantiate<LosePopup>(AssetPath.LosePopupPath);
                _losePopup.transform.SetParent(_mainCanvas.transform);
                _losePopup.GetComponent<Canvas>().overrideSorting = true;

            }
            return _losePopup;
        }

        public AudioPopup GetAudioPopup()
        {
            if (_audioPopup == null)
            {
                _audioPopup = _assetProvider.Instantiate<AudioPopup>(AssetPath.AudioPopupPath);
                _audioPopup.transform.SetParent(_mainCanvas.transform);
                _audioPopup.GetComponent<Canvas>().overrideSorting = true;
            }
            return _audioPopup;
        }
    }
}