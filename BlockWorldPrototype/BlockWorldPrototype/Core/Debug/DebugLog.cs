// -----------------------------------------------------------------------
// <copyright file="DebugLog.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockWorldPrototype.Core.Debug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum DebugMessageType
    {
        Info,
        Warning,
        Error
    }

    public class DebugMessage
    {
        public string Message;
        public DebugMessageType Type;
        public Color Color;

        public DebugMessage(string message, DebugMessageType type)
        {
            Message = message;
            Type = type;

            if (Type == DebugMessageType.Error)
                Color = Color.Red;

            if (Type == DebugMessageType.Info)
                Color = Color.White;

            if (Type == DebugMessageType.Warning)
                Color = Color.Yellow;
        }
    }

    public static class DebugLog
    {
        private static List<string> _messages;
        private static List<DebugMessage> _debugMessages;
        private static int _noToShow;
        private static int _startIndex;
        private static SpriteFont _font;
        private static bool _show;
        private static bool _newMessages;

        public static void Init(SpriteFont font)
        {
            _messages = new List<string>();
            _debugMessages = new List<DebugMessage>();
            _noToShow = 25;
            _startIndex = 0;
            _font = font;
            _show = true;
            _newMessages = false;
        }

        public static void Log(string message, DebugMessageType type = DebugMessageType.Info)
        {
            _messages.Add(message);

            _debugMessages.Add(new DebugMessage(message, type));

            if (message.Length > _noToShow)
            {
                _startIndex = _messages.Count - _noToShow;
            }

            if (!_show)
            {
                _newMessages = true;
            }
        }

        public static void Update()
        {
            // only modify controls when visible.
            if (_show)
            {
                if (InputHelper.IsKeyDown(Keys.PageUp))
                {
                    if (_startIndex > 0)
                    {
                        _startIndex -= 1;
                    }
                }
                if (InputHelper.IsKeyDown(Keys.PageDown))
                {
                    if (_startIndex < _messages.Count - _noToShow)
                    {
                        _startIndex += 1;
                    }
                }
            }

            if (InputHelper.IsNewKeyPress(Keys.F12))
            {
                _show = !_show;
                _newMessages = false;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            float lineHeight = _font.MeasureString("|").Y;
            int linesDrawn = 2;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            if (_show)
            {
                spriteBatch.DrawString(_font,
                    "Debug Log (" + _startIndex + " - " + (_startIndex + _noToShow) + " of " + _messages.Count +
                    ") Pg dn/Pg Up to Scroll - F12 to Hide", new Vector2(32, 500), Color.White);
                spriteBatch.DrawString(_font, "-----------------------------------------------",
                    new Vector2(32, 500 + lineHeight), Color.Yellow);

                for (int i = _startIndex; i < _noToShow + _startIndex; i++)
                {
                    if (i >= _debugMessages.Count)
                    {
                        break;
                    }

                    spriteBatch.DrawString(_font, _debugMessages[i].Message, new Vector2(32, 500 + (linesDrawn * lineHeight)),
                       _debugMessages[i].Color);
                    linesDrawn++;
                }
            }
            else
            {
                string extraInfo = "";
                if (_newMessages)
                {
                    extraInfo = " (New Messages)";
                }
                spriteBatch.DrawString(_font, "Debug Log Hidden - F12 to Show" + extraInfo, new Vector2(32, 500), Color.Yellow);
            }

            spriteBatch.End();
        }

    }
}
