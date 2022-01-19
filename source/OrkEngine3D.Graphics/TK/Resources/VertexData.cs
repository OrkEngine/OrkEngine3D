using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;


namespace OrkEngine3D.Graphics.TK.Resources{

    [Obsolete("Why is this still here? Just to suffer?", true)]
    public class VertexData<T> : IEnumerable where T : struct{
        List<T> data = new List<T>();
        public int Length => data.Count;

        public int GetSize(){
            return Marshal.SizeOf(typeof(T));
        }

        public void SetData(IEnumerable<T> data){
            this.data.Clear();
            this.data.AddRange(data);
        }

        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public VertexData<T> Add(T value){
            data.Add(value);
            return this;
        }

        public VertexData<T> AddRange(IEnumerable<T> values){
            data.AddRange(values);
            return this;
        }

        internal T[] ToArray()
        {
            return data.ToArray();
        }
    }
}