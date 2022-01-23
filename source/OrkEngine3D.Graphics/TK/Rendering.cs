﻿using OrkEngine3D.Components.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrkEngine3D.Graphics.TK
{
    public static class Rendering
    {
        public static Camera currentCamera { get; private set; }
        public static GraphicsContext currentContext { get; private set; }
        public static Transform currentTransform { get; private set; }
        public static IRenderTarget renderTarget { get; private set; } = WindowTarget.Global;
        public static LightScene currentLightning { get; private set; }
        public static Material[] currentMaterials { get; private set; } = new Material[] { new Material() };

        public static void BindCamera(Camera camera)
        {
            currentCamera = camera;
        }

        public static void BindContext(GraphicsContext ctx)
        {
            currentContext = ctx;
        }

        public static void BindLightning(LightScene light)
        {
            currentLightning = light;
        }

        public static void BindMaterial(Material material)
        {
            currentMaterials = new[] { material };
        }

        public static void BindMaterials(params Material[] materials)
        {
            currentMaterials = materials;
        }

        public static void BindTransform(Transform t)
        {
            currentTransform = t;
        }

        public static void BindTarget(IRenderTarget t)
        {
            renderTarget = t;
        }

        public static void ResetTarget()
        {
            currentContext.ResetFrameBuffer();
        }

        public static void ClearTarget()
        {
            renderTarget.Clear();
        }

        public static void SwapBuffers()
        {
            currentContext.SwapBuffers();
        }
    }
}
