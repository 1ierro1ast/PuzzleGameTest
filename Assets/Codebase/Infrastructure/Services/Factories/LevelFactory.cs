using System;
using Codebase.Core.Level;
using Codebase.Infrastructure.Services.AssetManagement;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Codebase.Infrastructure.Services.Factories
{
    public class LevelFactory : ILevelFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly int _levelsAmount;
        private Level _cachedLevel;

        public LevelFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _levelsAmount = _assetProvider.GetAssetAmount(AssetPath.LevelsPath);
        }

        public void CreateLevel(int levelNumber, Action levelOnReady = null)
        {
            var levels = _assetProvider.GetAllObjects<Level>(AssetPath.LevelsPath);
            string levelPath = AssetPath.LevelsPath + "/";
            if (levelNumber > levels.Length - 1)
            {
                levelPath = levelPath + "Level" + Random.Range(1, _levelsAmount+1);
            }
            else
            {
                levelPath = levelPath + "Level" + levelNumber;
            }
            _cachedLevel = _assetProvider.Instantiate<Level>(levelPath);
            levelOnReady?.Invoke();
        }

        public void ClearLevel()
        {
            if(_cachedLevel != null) Object.Destroy(_cachedLevel.gameObject);
        }
    }
}