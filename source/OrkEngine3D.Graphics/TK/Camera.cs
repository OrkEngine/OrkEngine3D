using OpenTK.Mathematics;
using OrkEngine3D.Core.Components;
using System;
//using OrkEngine3D.Mathematics;

namespace OrkEngine3D.Graphics.TK;

/// <summary>
/// The graphics end camera
/// </summary>
public class Camera
{
    private Vector3 _front = -Vector3.UnitZ;
    private Vector3 _up = Vector3.UnitY;
    private Vector3 _right = Vector3.UnitX;

    // Rotation around the X axis (radians)
    private float _pitch;

    private float _yaw = -MathHelper.PiOver2;
    private float _fov = MathHelper.PiOver2;

    public Camera(Vector3 position, float aspectRatio)
    {
        Position = position;
        AspectRatio = aspectRatio;
    }

    // The position of the camera
    public Vector3 Position { get; set; }

    // This is simply the aspect ratio of the viewport, used for the projection matrix
    public float AspectRatio { private get; set; }

    public Vector3 Front => _front;

    public Vector3 Up => _up;

    public Vector3 Right => _right;

    public float Pitch
    {
        get => MathHelper.RadiansToDegrees(_pitch);
        set
        {
            // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
            // of weird "bugs" when you are using euler angles for rotation.
            // If you want to read more about this you can try researching a topic called gimbal lock
            var angle = MathHelper.Clamp(value, -89f, 89f);
            _pitch = MathHelper.DegreesToRadians(angle);
            UpdateVectors();
        }
    }

    // We convert from degrees to radians as soon as the property is set to improve performance.
    public float Yaw
    {
        get => MathHelper.RadiansToDegrees(_yaw);
        set
        {
            _yaw = MathHelper.DegreesToRadians(value);
            UpdateVectors();
        }
    }

    // The field of view (FOV) is the vertical angle of the camera view.
    // This has been discussed more in depth in a previous tutorial,
    // but in this tutorial, you have also learned how we can use this to simulate a zoom feature.
    // We convert from degrees to radians as soon as the property is set to improve performance.
    public float Fov
    {
        get => MathHelper.RadiansToDegrees(_fov);
        set
        {
            var angle = MathHelper.Clamp(value, 1f, 90f);
            _fov = MathHelper.DegreesToRadians(angle);
        }
    }

    /// <summary>
    /// Is the camera perspective or not?
    /// </summary>
    public bool perspective = true;

    /// <summary>
    /// Camera FOV
    /// </summary>
    public float fov = 90;

    /// <summary>
    /// The near plane in units
    /// Things closer than this wont render
    /// </summary>
    public float nearPlane = 0.1f;
    /// <summary>
    /// The far plane in units
    /// Things further away than this wont render
    /// </summary>
    public float farPlane = 1000f;

    //come back to this
    // <summary>
    // The cameras transform, should be updated every frame
    // </summary>
    //public Transform transform = new Transform();

    // Get the view matrix using the amazing LookAt function described more in depth on the web tutorials
    public Matrix4 GetViewMatrix()
    {
        return Matrix4.LookAt(Position, Position + _front, _up);
    }

    // Get the projection matrix using the same method we have used up until this point
    public Matrix4 GetProjectionMatrix()
    {
        return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
    }

    // This function is going to update the direction vertices using some of the math learned in the web tutorials.
    private void UpdateVectors()
    {
        // First, the front matrix is calculated using some basic trigonometry.
        _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
        _front.Y = MathF.Sin(_pitch);
        _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

        // We need to make sure the vectors are all normalized, as otherwise we would get some funky results.
        _front = Vector3.Normalize(_front);

        // Calculate both the right and the up vector using cross product.
        // Note that we are calculating the right from the global up; this behaviour might
        // not be what you need for all cameras so keep this in mind if you do not want a FPS camera.
        _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
        _up = Vector3.Normalize(Vector3.Cross(_right, _front));
    }

    /*
    /// <summary>
    /// Is the camera perspective or not?
    /// </summary>
    public bool perspective = true;

    /// <summary>
    /// Camera FOV
    /// </summary>
    public float fov = 90;

    /// <summary>
    /// The near plane in units
    /// Things closer than this wont render
    /// </summary>
    public float nearPlane = 0.1f;
    /// <summary>
    /// The far plane in units
    /// Things further away than this wont render
    /// </summary>
    public float farPlane = 1000f;

    /// <summary>
    /// The cameras transform, should be updated every frame
    /// </summary>
    public Transform transform = new Transform();

    /// <summary>
    /// Get the camera matrix, projection * view.
    /// </summary>
    /// <param name="ctx">The current graphics context</param>
    /// <returns>The camera matrix</returns>
    public Matrix GetMatrix()
    {
        GraphicsContext ctx = Rendering.currentContext;
        if (perspective)
            return Matrix.PerspectiveFovRH(fov, (float)ctx.window.Size.X / (float)ctx.window.Size.Y, nearPlane, farPlane); //* Matrix.LookAtRH(transform.position, transform.forward, transform.up);
        else
            return Matrix.OrthoRH((float)ctx.window.Size.X / (float)ctx.window.Size.Y, 1, nearPlane, farPlane) * Matrix.LookAtRH(transform.position, transform.forward, transform.up);
    }
    */
}