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

using System.Collections.Generic;
using System.Linq;

namespace OrkEngine3D.Core.Components;

public class GameObjectQuerySystem : GameSystem
{
    private readonly IReadOnlyList<GameObject> _gameObjects;

    public GameObjectQuerySystem(IReadOnlyList<GameObject> gameObjectList)
    {
        _gameObjects = gameObjectList;
    }

    protected override void UpdateCore(float deltaSeconds)
    {
    }

    public GameObject FindByName(string name)
    {
        return _gameObjects.FirstOrDefault(go => go.Name == name);
    }

    public IEnumerable<GameObject> GetUnparentedGameObjects()
    {
        return _gameObjects.Where(go => go.Transform.Parent == null);
    }

    public IEnumerable<GameObject> GetAllGameObjects()
    {
        return _gameObjects;
    }

    public string GetCloneName(string name)
    {
        while (FindByName(name) != null)
        {
            name += " (Clone)";
        }

        return name;
    }
}
