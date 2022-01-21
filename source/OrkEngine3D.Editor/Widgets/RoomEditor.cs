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

//using AbilityEngine.Engine.Entities;
//using AbilityEngine.Engine.Resources.Game;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbilityEngine.Editor.Widgets
{
    /*
    public class RoomEditor : IWidget
    {
        
        private Room room;
        private EditorController controller;
        private Entity selectedEntity = new Entity();
        private string newEntityName = "";
        public RoomEditor(IWidget? root, Room room, bool active = true) : base(root, active)
        {
            this.room = room;
            controller = EditorController.Last;
        }
        public override void RenderFunc()
        {
            if (isActive)
            {
                if (ImGui.BeginCombo("Edit Entity", selectedEntity.name))
                {
                    for (int i = 0; i < room.entities.Length; i++)
                    {
                        bool selected = (selectedEntity == room.entities[i]);

                        if (ImGui.Selectable(room.entities[i].name, selected))
                            selectedEntity = room.entities[i];
                        if (selected)
                            ImGui.SetItemDefaultFocus();
                    }
                    ImGui.EndCombo();
                }
                ImGui.SameLine();
                if (ImGui.Button("Open"))
                {
                    GUIWindow window = new GUIWindow("Entity Editor");
                    controller.rootWidget.AddChild(window);

                    EntityEditor e = new EntityEditor(window, selectedEntity);
                }
                ImGui.InputText("New Name", ref newEntityName, 32);
                ImGui.SameLine();
                if (ImGui.Button("Create Entity"))
                {
                    var l = room.entities.ToList();
                    Entity e = new Entity();
                    e.name = newEntityName;
                    l.Add(e);
                    room.entities = l.ToArray();
                }
            }
        }
    
}
    */
}
