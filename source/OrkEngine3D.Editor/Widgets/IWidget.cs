/*
 MIT License

 Copyright (c) 2021 Open Ability Software
 
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityEngine.Editor.Widgets
{
    public abstract class IWidget
    {
        public bool isActive;
        private List<IWidget> children = new List<IWidget>();
        public IWidget? parent { get; private set; }
        public Action preRender = () => {};

        public IWidget(IWidget? root, bool active = true)
        {
            if(root != null)
            {
                root.AddChild(this);
            }
            isActive = active;
        }

        public void AddChild(IWidget child)
        {
            children.Add(child);
            child.parent = this;
        }

        public void RemoveChild(IWidget child)
        {
            children.Remove(child);
            child.parent = null;
        }

        public void RemoveChildren()
        {
            List<IWidget> childrenToClear = new List<IWidget>();
            childrenToClear.AddRange(children);
            foreach(IWidget child in childrenToClear)
            {
                RemoveChild(child);
            }
        }
        public virtual void RenderFunc(){
            IWidget[] widgets = children.ToArray();
            foreach (IWidget c in widgets)
            {
                c.RenderFunc();
            }
        }
        public void Render()
        {
            preRender();
            RenderFunc();
        }
    }
}
