using UnityEngine;
using UnityEngine.EventSystems;

namespace Codebase.Core.Gameplay
{
    public abstract class BaseCell : MonoBehaviour, IDropHandler
    {
        [SerializeField] private RectTransform _dragArea;
        private RectTransform _rectTransform;

        private bool _hasItem => transform.childCount > 0;
        
        public RectTransform DragArea => _dragArea;

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {
            if(_hasItem) return;
            
            Square square; 
            eventData.pointerDrag.TryGetComponent(out square);
            square?.SetCurrentParent(this);
            
            OnCellFilled();
        }

        protected virtual void OnCellFilled(){}
    }
}
