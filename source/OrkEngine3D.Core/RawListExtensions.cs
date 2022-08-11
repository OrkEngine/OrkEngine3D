using BEPUutilities.DataStructures;
using System;

namespace OrkEngine3D.Core
{
    public static class RawListExtensions
    {
        public static ArraySegment<T> GetArraySegment<T>(this RawList<T> rawList)
        {
            return new ArraySegment<T>(rawList.Elements, 0, rawList.Count);
        }
    }
}
