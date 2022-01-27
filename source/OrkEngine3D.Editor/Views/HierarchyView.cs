using System;
using System.Drawing;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using ImGuiNET;
using System.Numerics;
using AbilityEngine.Editor.Widgets;
namespace OrkEngine3D.Editor.Views
{
    internal class HierarchyView
    {

        public static IntPtr TreeNode = IntPtr.Zero;
        public static void Show()
        {
            //ImGuiViewportPtr viewport = ImGui.GetMainViewport();
            //ImGui.SetNextWindowPos(new Vector2(50, 50));
            //ImGui.SetNextWindowSize(new Vector2(200, 200));
            //ImGui.SetNextWindowViewport(viewport.ID);
            //ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
            //ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
            //ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
            
            if (ImGui.Begin("Hierarchy"))
            {

                ImGuiStylePtr style = ImGui.GetStyle();
                //style.Colors[(int)ImGuiCol.Text] = Imgui
                ImGui.SetWindowSize(new Vector2(500, 500));

                ImGui.TextColored(new Vector4(255, 0, 0, 255), "ColoredText");
                if (ImGui.TreeNode(TreeNode, "Items"))
                {
                    ImGui.TreePush("Example");

                    ImGui.TextColored(new Vector4(255, 0, 0, 255), "Example");

                    

                    ImGui.End();
                    
                    ImGui.End();
                }
                

                //ImGui.
                //ImGui.Button("Hello", new Vector2(10, 10));
                
                ImGui.End();
            }
        }
    }
}
