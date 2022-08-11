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

namespace OrkEngine3D.Common.Utils;

public abstract class AssetDatabase
{
    public T LoadAsset<T>(AssetID assetID) => LoadAsset<T>(assetID, true);
    public abstract T LoadAsset<T>(AssetID assetID, bool cache);

    public T LoadAsset<T>(AssetRef<T> assetRef) => LoadAsset<T>(assetRef, true);
    public abstract T LoadAsset<T>(AssetRef<T> assetRef, bool cache);

    public object LoadAsset(AssetID assetID) => LoadAsset(assetID, true);
    public abstract object LoadAsset(AssetID assetID, bool cache);

    public bool TryLoadAsset<T>(AssetID assetID, out T asset) => TryLoadAsset(assetID, true, out asset);
    public abstract bool TryLoadAsset<T>(AssetID assetID, bool cache, out T asset);

    public abstract Stream OpenAssetStream(AssetID assetID);

    public abstract bool TryOpenAssetStream(AssetID assetID, out Stream stream);

    public abstract AssetID[] GetAssetsOfType(Type t);
}

/*
public abstract class EditableAssetDatabase : AssetDatabase
{
    public abstract string GetAssetPath(AssetID assetID);
    public abstract DirectoryNode GetRootDirectoryGraph();
    public abstract Type GetAssetType(AssetID assetID);
    public abstract void CloneAsset(string path);
    public abstract void DeleteAsset(string path);
}
*/
