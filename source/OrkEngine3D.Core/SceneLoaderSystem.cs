/*
    MIT License

Copyright (c) 2022 OrkEngine

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using OrkEngine3D.Core.Assets;
using OrkEngine3D.Core.Components;
using System;

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