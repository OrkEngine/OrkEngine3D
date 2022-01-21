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

using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AbilityEngine.Editor.Widgets
{
    public class FileMenu : IWidget
    {
        public string value;
        public string title;
        public string tip;
        public string buttonText;
        public Action<string> callback = (string value) => { };
        public FileMenu(string title, string tip, string value, string buttonText = "Open") : base(null, false)
        {
            this.title = title;
            this.tip = tip;
            this.value = value;
            this.buttonText = buttonText;
        }

        public void Open(string value)
        {
            this.value = value;
            isActive = true;
            //TODO: impl logger
            //Window.LOGGER.Log("Opening file menu...");
        }

        public override void RenderFunc()
        {
            if (isActive)
            {
                ImGui.SetNextWindowBgAlpha(0.5f);
                ImGui.SetNextWindowCollapsed(false, ImGuiCond.Always);
                ImGui.SetNextWindowPos(new Vector2(0, ImGui.GetFrameHeight()));
                ImGui.SetNextWindowSizeConstraints(new Vector2(400, 100), new Vector2(400, 100));
                if (ImGui.Begin(title))
                {
                    ImGui.InputText("File", ref value, 100);
                    ImGui.Text(tip);
                    if (ImGui.Button(buttonText))
                    {
                        isActive = false;
                        callback(value);
                    }
                    ImGui.End();
                }
            }
        }
    }
}
