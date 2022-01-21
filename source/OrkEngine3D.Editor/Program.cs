using System;
using System.Drawing;
using Silk.NET.Windowing;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.OpenGL.Extensions.ImGui;
using ImGuiNET;
namespace OrkEngine3D.Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Silk.NET window as usual
            using var window = Window.Create(WindowOptions.Default);

            // Declare some variables
            ImGuiController controller = null;
            GL gl = null;
            IInputContext inputContext = null;
            ImGuiIOPtr io = ImGui.GetIO();
            // Our loading function
            window.Load += () =>
            {
                controller = new ImGuiController(
                    gl = window.CreateOpenGL(), // load OpenGL
                    window, // pass in our window
                    inputContext = window.CreateInput() // create an input context
                );

                ImGui.GetIO().ConfigFlags |= ImGuiConfigFlags.DockingEnable;
            };

            // Handle resizes
            window.FramebufferResize += s =>
            {
                // Adjust the viewport to the new window size
                gl.Viewport(s);
            };
            // The render function
            window.Render += delta =>
            {
                // Make sure ImGui is up-to-date
                controller.Update((float)delta);

                // These epic lines makes the dock
                gl.ClearColor(Color.FromArgb(255, (int)(.45f * 255), (int)(.55f * 255), (int)(.60f * 255)));
                gl.Clear((uint)ClearBufferMask.ColorBufferBit);        
                // This is where you'll do all of your ImGUi rendering
                // Here, we're just showing the ImGui built-in demo window.

                RenderFullScreenDock();
                ImGui.ShowDemoWindow();


                // Make sure ImGui renders too!
                controller.Render();
            };

            // The closing function
            window.Closing += () =>
            {
                // Dispose our controller first
                controller?.Dispose();

                // Dispose the input context
                inputContext?.Dispose();

                // Unload OpenGL
                gl?.Dispose();
            };

            // Now that everything's defined, let's run this bad boy!
            window.Run();
        }

        public static void RenderFullScreenDock(){
                ImGuiViewportPtr viewport = ImGui.GetMainViewport();
                ImGui.SetNextWindowPos(viewport.WorkPos);
                ImGui.SetNextWindowSize(viewport.WorkSize);
                ImGui.SetNextWindowViewport(viewport.ID);
                ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0.0f);
                ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0.0f);
                if(ImGui.Begin("Main dock", ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBringToFrontOnFocus)){
                    ImGui.SetWindowSize(new System.Numerics.Vector2(700, 700));
                    ImGui.DockSpace(ImGui.GetID("Main dock"), new System.Numerics.Vector2(0, 0), ImGuiDockNodeFlags.NoResize);
                    ImGui.End();
                }
                    
        }
    }
}
