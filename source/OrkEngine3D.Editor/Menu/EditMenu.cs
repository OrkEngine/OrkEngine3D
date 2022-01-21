using System;
using System.Drawing;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using ImGuiNET;
using System.Numerics;

namespace OrkEngine3D.Editor.Menu
{
    internal class EditMenu
    {
        public static void Dispatch()
        {
            if (ImGui.BeginMenu("Edit"))
            {
                if (ImGui.MenuItem("Test", "Ctrl+T"))
                {
                    //TODO: debugger logger test
                }

                ImGui.EndMenu();
            }
        }
    }
}
