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
    internal class FileMenu
    {
        public static void Dispatch()
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Exit", "Ctrl+Q"))
                {
                    Exit();
                }

                ImGui.EndMenu();
            }
        }

        private static void Exit(int exitCode = 0)
        {
            Environment.Exit(exitCode);
        }
    }
}
