using UnityEngine;

namespace Codebase.Core.Gameplay
{
    public class CellsPreparer : MonoBehaviour
    {
        private void Awake()
        {
            InitializeCells();
        }

        private void InitializeCells()
        {
            int currentId = 0;
            foreach (Transform child in transform)
            {
                child.GetComponent<LevelCell>().SetId(currentId);
                currentId++;
            }
        }
    }
}