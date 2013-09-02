// -----------------------------------------------------------------------
// <copyright file="Camera3d.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockWorldPrototype.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Camera3D
    {
        private Vector3 _cameraPosition = new Vector3(0, 30, 0);
        private float _leftrightRot = MathHelper.PiOver2;
        private float _updownRot = -MathHelper.Pi / 10.0f;
        private float _rotationSpeed = 0.15f;
        private float _moveSpeed = 30.0f;
        private float _zoomSpeed = 2f;
        private float _zoom = 10f;

        private bool _isOrthographic;
        private bool _fixedMouse;

        public GraphicsDevice Device;
        public Matrix ViewMatrix;
        public Matrix ProjectionMatrix;

        public Camera3D(GraphicsDevice device)
        {
            _isOrthographic = false;
            _fixedMouse = true;
            Device = device;
            ResetCamera();
        }

        public void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
        }

        private void HandleInput(GameTime gameTime)
        {
            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;


            // Get mouse X/Y difference and rotate camera based on it.
            // Should maybe cap this as you can full flips at the moment... fucks with the movement vector.
            if (InputHelper.MouseState != InputHelper.PreviousMouseState)
            {
                float xDifference = InputHelper.MouseState.X - InputHelper.PreviousMouseState.X;
                float yDifference = InputHelper.MouseState.Y - InputHelper.PreviousMouseState.Y;
                _leftrightRot -= _rotationSpeed * xDifference * timeDifference;
                _updownRot -= _rotationSpeed * yDifference * timeDifference;
                if (_fixedMouse)
                    Mouse.SetPosition(Device.Viewport.Width / 2, Device.Viewport.Height / 2);
                UpdateViewMatrix();
            }


            if (InputHelper.IsNewKeyPress(Keys.Tab))
                _fixedMouse = !_fixedMouse;

            Vector3 moveVector = new Vector3(0, 0, 0);

            // Big list of keys that moves camera and stuff.
            if (InputHelper.IsKeyDown(Keys.W))
            {
                if (_isOrthographic)
                {
                    if (InputHelper.IsKeyDown(Keys.LeftShift))
                    {
                        _zoom -= (_zoomSpeed * 5) * timeDifference;
                    }
                    else
                    {
                        _zoom -= _zoomSpeed * timeDifference;
                    }
                }
                else
                {
                    moveVector += new Vector3(0, 0, -1);
                }
            }

            if (InputHelper.IsKeyDown(Keys.A))
            {
                moveVector += new Vector3(-1, 0, 0);
            }

            if (InputHelper.IsKeyDown(Keys.S))
            {
                // In orthographic mode, moving forwards/backwards does nothign so zoom instead.
                if (_isOrthographic)
                {
                    if (InputHelper.IsKeyDown(Keys.LeftShift))
                    {
                        _zoom += (_zoomSpeed * 5) * timeDifference;
                    }
                    else
                    {
                        _zoom += _zoomSpeed * timeDifference;
                    }
                }
                else
                {
                    moveVector += new Vector3(0, 0, 1);
                }
            }

            if (InputHelper.IsKeyDown(Keys.D))
            {
                moveVector += new Vector3(1, 0, 0);
            }

            if (InputHelper.IsKeyDown(Keys.Q))
            {
                moveVector += new Vector3(0, 1, 0);
            }

            if (InputHelper.IsKeyDown(Keys.Z))
            {
                moveVector += new Vector3(0, -1, 0);
            }

            if (InputHelper.IsKeyDown(Keys.R))
            {
                ResetCamera();
            }

            AddToCameraPosition(moveVector * timeDifference);

            if (_isOrthographic)
            {
                // Only apply zoom in orthographic
                UpdateZoom();
            }

            // Keys to toggle camera mode.
            if (InputHelper.IsNewKeyPress(Keys.F1))
            {
                _isOrthographic = false;
                ResetCamera();
            }

            if (InputHelper.IsNewKeyPress(Keys.F2))
            {
                _isOrthographic = true;
                ResetCamera();
            }

            if (InputHelper.IsNewKeyPress(Keys.F3))
            {
                _leftrightRot = 0.785398163f;
                _updownRot = -0.523598776f;
            }

            if (InputHelper.IsNewKeyPress(Keys.D1))
            {
                _leftrightRot = 0.785398163f;
                _updownRot = -0.523598776f;
            }
            if (InputHelper.IsNewKeyPress(Keys.D2))
            {
                _leftrightRot = -0.785398163f;
                _updownRot = -0.523598776f;
            }
            if (InputHelper.IsNewKeyPress(Keys.D3))
            {
                _leftrightRot = 2.35619449f;
                _updownRot = -0.523598776f;
            }
            if (InputHelper.IsNewKeyPress(Keys.D4))
            {
                _leftrightRot = -2.35619449f;
                _updownRot = -0.523598776f;
            }
        }

        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            float movespeed = _moveSpeed;
            if (InputHelper.IsKeyDown(Keys.LeftControl))
            {
                movespeed *= 0.1f;
            }
            Matrix cameraRotation = Matrix.CreateRotationX(_updownRot) * Matrix.CreateRotationY(_leftrightRot);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            _cameraPosition += movespeed * rotatedVector;
            UpdateViewMatrix();
        }

        public void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(_updownRot) * Matrix.CreateRotationY(_leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);

            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = _cameraPosition + cameraRotatedTarget;

            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

            ViewMatrix = Matrix.CreateLookAt(_cameraPosition, cameraFinalTarget, cameraRotatedUpVector);
        }

        public void UpdateZoom()
        {
            ProjectionMatrix = Matrix.CreateOrthographic(_zoom, _zoom * Device.Viewport.AspectRatio, -5000f, 5000f);
        }

        public void ResetCamera()
        {
            _cameraPosition = new Vector3(10, 10, 10);
            _leftrightRot = MathHelper.PiOver2;
            _updownRot = -MathHelper.Pi / 10.0f;
            _zoom = 10f;

            ViewMatrix = Matrix.Identity;
            if (_isOrthographic)
            {
                ProjectionMatrix = Matrix.CreateOrthographic(1f, 1f * Device.Viewport.AspectRatio, -5000f, 5000f);
            }
            else
            {
                ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                           Device.Viewport.AspectRatio, .2f, 5000f);
            }

        }
    }
}
