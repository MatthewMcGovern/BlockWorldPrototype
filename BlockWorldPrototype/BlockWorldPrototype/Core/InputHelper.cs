// -----------------------------------------------------------------------
// <copyright file="InputHelper.cs" company="Microsoft">
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
    /// <summary>
    ///   an enum of all available mouse buttons.
    /// </summary>
    public enum MouseButtons
    {
        LeftButton,
        MiddleButton,
        RightButton,
        ExtraButton1,
        ExtraButton2
    }

    public static class InputHelper
    {
        private static MouseState _currentMouseState;

        private static KeyboardState _currentKeyboardState;

        private static MouseState _lastMouseState;
        private static KeyboardState _lastKeyboardState;

        private static Vector2 _cursor;
        private static bool _cursorIsValid;
        private static bool _cursorIsVisible;
        private static bool _cursorMoved;

        private static Viewport _viewport;

        /// <summary>
        ///   Constructs a new input state.
        /// </summary>
        public static void Init()
        {

            _currentMouseState = new MouseState();
            _currentKeyboardState = new KeyboardState();

            _lastMouseState = new MouseState();
            _lastKeyboardState = new KeyboardState();

            _cursorIsVisible = false;
            _cursorMoved = false;

            _cursorIsValid = true;
            _cursor = Vector2.Zero;
        }

        public static MouseState MouseState
        {
            get { return _currentMouseState; }
        }

        public static MouseState PreviousMouseState
        {
            get { return _lastMouseState; }
        }

        public static KeyboardState KeyboardState
        {
            get { return _currentKeyboardState; }
        }

        public static KeyboardState PreviousKeyboardState
        {
            get { return _lastKeyboardState; }
        }

        public static bool ShowCursor
        {
            get { return _cursorIsVisible && _cursorIsValid; }
            set { _cursorIsVisible = value; }
        }

        public static Vector2 Cursor
        {
            get { return _cursor; }
        }

        /*public static Vector2 CursorRelative
        {
            get { return new Vector2(_cursor.X + mainCamera.GetTopLeftPixelPosition().X, _cursor.Y + mainCamera.GetTopLeftPixelPosition().Y); }
        }*/

        public static bool IsCursorMoved
        {
            get { return _cursorMoved; }
        }

        public static bool IsCursorValid
        {
            get { return _cursorIsValid; }
        }

        public static void LoadContent(GraphicsDevice graphicsDevice)
        {
            _viewport = graphicsDevice.Viewport;
        }

        /// <summary>
        ///   Reads the latest state of the keyboard and gamepad and mouse/touchpad.
        /// </summary>
        public static void Update(GameTime gameTime)
        {

            _lastMouseState = _currentMouseState;
            _lastKeyboardState = _currentKeyboardState;

            _currentMouseState = Mouse.GetState();
            _currentKeyboardState = Keyboard.GetState();

            // Update cursor
            Vector2 oldCursor = _cursor;

            _cursor.X = _currentMouseState.X;
            _cursor.Y = _currentMouseState.Y;
            _cursor.X = MathHelper.Clamp(_cursor.X, 0f, _viewport.Width);
            _cursor.Y = MathHelper.Clamp(_cursor.Y, 0f, _viewport.Height);

            if (_cursorIsValid && oldCursor != _cursor)
            {
                _cursorMoved = true;
            }
            else
            {
                _cursorMoved = false;
            }

            if (_viewport.Bounds.Contains(_currentMouseState.X, _currentMouseState.Y))
            {
                _cursorIsValid = true;
            }
            else
            {
                _cursorIsValid = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (_cursorIsVisible && _cursorIsValid)
            {
                //spriteBatch.Draw(_cursorSprite.Texture, _cursor, Color.White);
            }
        }

        public static bool IsNewMouseButtonPress(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (_currentMouseState.LeftButton == ButtonState.Pressed &&
                            _lastMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.RightButton:
                    return (_currentMouseState.RightButton == ButtonState.Pressed &&
                            _lastMouseState.RightButton == ButtonState.Released);
                case MouseButtons.MiddleButton:
                    return (_currentMouseState.MiddleButton == ButtonState.Pressed &&
                            _lastMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.ExtraButton1:
                    return (_currentMouseState.XButton1 == ButtonState.Pressed &&
                            _lastMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.ExtraButton2:
                    return (_currentMouseState.XButton2 == ButtonState.Pressed &&
                            _lastMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Checks if the requested mouse button is released.
        /// </summary>
        /// <param name="button">The button.</param>
        public static bool IsNewMouseButtonRelease(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return (_lastMouseState.LeftButton == ButtonState.Pressed &&
                            _currentMouseState.LeftButton == ButtonState.Released);
                case MouseButtons.RightButton:
                    return (_lastMouseState.RightButton == ButtonState.Pressed &&
                            _currentMouseState.RightButton == ButtonState.Released);
                case MouseButtons.MiddleButton:
                    return (_lastMouseState.MiddleButton == ButtonState.Pressed &&
                            _currentMouseState.MiddleButton == ButtonState.Released);
                case MouseButtons.ExtraButton1:
                    return (_lastMouseState.XButton1 == ButtonState.Pressed &&
                            _currentMouseState.XButton1 == ButtonState.Released);
                case MouseButtons.ExtraButton2:
                    return (_lastMouseState.XButton2 == ButtonState.Pressed &&
                            _currentMouseState.XButton2 == ButtonState.Released);
                default:
                    return false;
            }
        }

        public static bool IsMouseButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return _currentMouseState.LeftButton == ButtonState.Pressed;
                case MouseButtons.RightButton:
                    return _currentMouseState.RightButton == ButtonState.Pressed;
                case MouseButtons.MiddleButton:
                    return _currentMouseState.MiddleButton == ButtonState.Pressed;
                case MouseButtons.ExtraButton1:
                    return _currentMouseState.XButton1 == ButtonState.Pressed;
                case MouseButtons.ExtraButton2:
                    return _currentMouseState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        public static bool IsNewKeyPress(Keys key)
        {
            return (_currentKeyboardState.IsKeyDown(key) &&
                    _lastKeyboardState.IsKeyUp(key));
        }

        public static bool IsNewKeyRelease(Keys key)
        {
            return (_lastKeyboardState.IsKeyDown(key) &&
                    _currentKeyboardState.IsKeyUp(key));
        }

        public static bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }
    }
}
