using UnityEngine;
using UnityEngine.EventSystems;

namespace Codebase.Core.Gameplay
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Square : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private bool _canMove;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private RectTransform _dragArea;
        private BaseCell _currentParentCell;
        private Canvas _levelCanvas;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _currentParentCell = GetComponentInParent<BaseCell>();
            _dragArea = _currentParentCell.DragArea;
            _levelCanvas = GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_canMove) return;
            _rectTransform.anchoredPosition += eventData.delta / _levelCanvas.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_canMove) return;
            _canvasGroup.blocksRaycasts = false;
            transform.SetParent(_dragArea);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_canMove) return;
            _canvasGroup.blocksRaycasts = true;
            Drop();
        }

        public void SetCurrentParent(BaseCell newParentCell)
        {
            if (!_canMove) return;
            if (newParentCell is LevelCell) DisableSquare();
            _currentParentCell = newParentCell;
            Drop();
        }

        private void DisableSquare()
        {
            _canMove = false;
        }

        private void Drop()
        {
            transform.SetParent(_currentParentCell.RectTransform);
            transform.localPosition = Vector3.zero;
        }
    }
}
