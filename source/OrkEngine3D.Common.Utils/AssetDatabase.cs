using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Common.Utils
{
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
}
