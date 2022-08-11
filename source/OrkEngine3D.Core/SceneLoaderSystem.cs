using OrkEngine3D.Core.Assets;
using OrkEngine3D.Core.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Core;

public class SceneLoaderSystem : GameSystem
{
    protected readonly GameObjectQuerySystem _goqs;
    private readonly string/*Game*/ _game;

    private SceneAsset _loadedScene;

    public event Action BeforeSceneLoaded;
    public event Action AfterSceneLoaded;

    public SceneAsset LoadedScene => _loadedScene;

    public SceneLoaderSystem(string/*Game*/ game, GameObjectQuerySystem goqs)
    {
        _game = game;
        _goqs = goqs;
    }

    public void LoadScene(SceneAsset asset, bool delayTilEndOfFrame = true)
    {
        if (delayTilEndOfFrame)
        {
            //  _game.QueueEndOfFrameAction(() =>
            //  {
            //      CoreLoadScene(asset);
            //  });
        }
        else
        {
            CoreLoadScene(asset);
        }
    }

    private void CoreLoadScene(SceneAsset asset)
    {
        BeforeSceneLoaded?.Invoke();
        ClearCurrentSceneGameObjects();
        //_game.FlushDeletedObjects();
        asset.GenerateGameObjects();
        _loadedScene = asset;
        AfterSceneLoaded?.Invoke();
        //_game.NewSceneLoaded();
    }

    protected virtual void ClearCurrentSceneGameObjects()
    {
        foreach (var go in _goqs.GetAllGameObjects())
        {
            go.Destroy();
        }
    }

    protected override void UpdateCore(float deltaSeconds)
    {
    }
}