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

using System.Numerics;

namespace AbilityEngine.Editor.Widgets{

    /*
    public class CodeEditor : IWidget
    {

        //public Action OnEdit = () =>{};

        public string value { get => preview.text; set { preview.text = value; } }

        private CodePreview preview;

        public CodeEditor(IWidget root, string file, bool active = true) : base(root, active)
        {
            
            Text title = new Text(this, file);

            Button deleteButton = new Button(this, "Delete");
            deleteButton.preRender = () => { ImGuiNET.ImGui.SameLine(); };
            deleteButton.callback = () =>{
                File.Delete(EditorController.Last.project.GetScriptLocation(file, EditorController.Last.projectFile));
                EditorController.Last.project.scripts.Remove(file);
                EditorController.Last.ProjectHasBeenEdited = true;
            };

            FileMenu renameFileMenu = new FileMenu("Rename script", "Make it memorable", file, "Rename");
            EditorController.Last.rootWidget.AddChild(renameFileMenu);

            renameFileMenu.callback = (string value) =>
            {
                EditorController.LOGGER.Log("Renaming file " + file + "to " + value);
                File.Move(EditorController.Last.project.GetScriptLocation(file, EditorController.Last.projectFile), EditorController.Last.project.GetScriptLocation(value, EditorController.Last.projectFile));
                EditorController.Last.project.scripts.Remove(file);
                EditorController.Last.project.scripts.Add(value);
                EditorController.Last.ProjectHasBeenEdited = true;
                title.content = value;
            };

            Button renameButton = new Button(this, "Rename");

            renameButton.preRender = () => { ImGuiNET.ImGui.SameLine(); };
            renameButton.callback = () =>{
                renameFileMenu.Open(file);
            };

            Button editButton = new Button(this, "Edit");
            editButton.preRender = () => { ImGuiNET.ImGui.SameLine(); };
            editButton.callback = () => {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C {Settings.defaultEditorCommand} \"{EditorController.Last.project.GetScriptLocation(file, EditorController.Last.projectFile)}\"";
                process.StartInfo = startInfo;
                process.Start();
            };

            preview = new CodePreview(this, file);
            preview.preRender = () => { ImGuiNET.ImGui.Spacing(); };

            //textInput = new CodeEditorText(this, file);

        }

        public override void RenderFunc()
        {
            base.RenderFunc();
        }
    }


    public class CodePreview : IWidget
    {
        public string text;
        public CodePreview(IWidget root, string text, bool active = true) : base(root, active)
        {
            this.text = text;
        }

        public override void RenderFunc()
        {
            ImGuiNET.ImGui.TextWrapped(text);
        }
    }

    internal class CodeEditorText : IInputWidget
    {
        public string value;
        public CodeEditorText(IWidget root, string file, bool active = true) : base(root, file, active)
        {
        }

        protected override bool GetInput()
        {
            Vector2 size = ImGuiNET.ImGui.GetWindowSize();
            return ImGuiNET.ImGui.InputTextMultiline("", ref value, 10000, size);
        }
    }
    */
}