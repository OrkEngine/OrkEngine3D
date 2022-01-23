using System;
using System.Drawing;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using ImGuiNET;
using System.Numerics;
using OrkEngine3D.Editor.Views;
namespace OrkEngine3D.Editor.Menu
{
    internal class ViewMenu
    {
        public static void Dispatch()
        {
            if (ImGui.BeginMenu("View"))
            {
                if (ImGui.MenuItem("Hierarchy"))
                {
                    HierarchyView.Show();
                }

                ImGui.EndMenu();
            }
        }
    }
}
