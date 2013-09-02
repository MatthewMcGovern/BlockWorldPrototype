// -----------------------------------------------------------------------
// <copyright file="WorldEditor.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core;
using BlockWorldPrototype.Core.Debug;
using BlockWorldPrototype.Core.Graphics;
using BlockWorldPrototype.World.Block;
using BlockWorldPrototype.World.Segments;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BlockWorldPrototype.World.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    /// 

    public enum EditorType
    {
        Blocks,
        TopRamps,
        BottomRamps,
        Objects
    }

    public class WorldEditor
    {
        public SegmentManager ParentSegmentManager;

        private SegmentLocation _keyboardCursor;

        private List<BlockMask> _blockMasks;
        private List<BlockMask> _topRampMasks;
        private List<BlockMask> _bottomRampMasks;

        private BlockMask _RampBlockDirection;
        private BlockMask _activeBlockMask;

        private RenderBasic _blockHighlight;
        private RenderBasic _blockHighlightBlocked;
        private EditorType _selectedType;

        private BlockVertexData _previewData;

        private int _index;

        public WorldEditor(SegmentManager parent)
        {
            ParentSegmentManager = parent;

            _blockMasks = new List<BlockMask>();
            _blockMasks.Add(BlockHelper.BlockMasks.Debug);
            _blockMasks.Add(BlockHelper.BlockMasks.Soil);

            _topRampMasks = new List<BlockMask>();
            _topRampMasks.Add(BlockHelper.RampBlockMasks.Top.Debug);
            _topRampMasks.Add(BlockHelper.RampBlockMasks.Top.Soil);

            _bottomRampMasks = new List<BlockMask>();
            _bottomRampMasks.Add(BlockHelper.RampBlockMasks.Bottom.Debug);
            _bottomRampMasks.Add(BlockHelper.RampBlockMasks.Bottom.Soil);

            _index = 0;

            _selectedType = EditorType.Blocks;

            _keyboardCursor = new SegmentLocation(Vector3.Zero);

            _RampBlockDirection = RampBlockDirection.North;

            _activeBlockMask = BlockHelper.BlockMasks.Soil;

            _previewData = new BlockVertexData();
        }

        public void LoadContent(ContentManager content, Effect effect)
        {
            _blockHighlight = new RenderBasic(content.Load<Model>("Models/HighLight"), effect);
            _blockHighlightBlocked = new RenderBasic(content.Load<Model>("Models/HighLight_Blocked"), effect);
        }

        public void Update()
        {
            bool changed = false;

            // SHIFT 
            if (InputHelper.IsKeyDown(Keys.LeftShift) || InputHelper.IsKeyDown(Keys.RightShift))
            {
                if (InputHelper.IsNewKeyPress(Keys.Right))
                {
                    if (_RampBlockDirection == RampBlockDirection.North)
                    {
                        _RampBlockDirection = RampBlockDirection.East;
                    }
                    else if (_RampBlockDirection == RampBlockDirection.East)
                    {
                        _RampBlockDirection = RampBlockDirection.South;
                    }
                    else if (_RampBlockDirection == RampBlockDirection.South)
                    {
                        _RampBlockDirection = RampBlockDirection.West;
                    }
                    else if (_RampBlockDirection == RampBlockDirection.West)
                    {
                        _RampBlockDirection = RampBlockDirection.North;
                    }

                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Left))
                {

                    if (_RampBlockDirection == RampBlockDirection.North)
                    {
                        _RampBlockDirection = RampBlockDirection.West;
                    }
                    else if (_RampBlockDirection == RampBlockDirection.East)
                    {
                        _RampBlockDirection = RampBlockDirection.North;
                    }
                    else if (_RampBlockDirection == RampBlockDirection.South)
                    {
                        _RampBlockDirection = RampBlockDirection.East;
                    }
                    else if (_RampBlockDirection == RampBlockDirection.West)
                    {
                        _RampBlockDirection = RampBlockDirection.South;
                    }
                    changed = true;
                }
            }
            else
            {
                if (InputHelper.IsNewKeyPress(Keys.Up))
                {
                    switch (_selectedType)
                    {
                        case EditorType.Blocks:
                            _selectedType = EditorType.BottomRamps;
                            break;
                        case EditorType.BottomRamps:
                            _selectedType = EditorType.TopRamps;
                            break;
                        case EditorType.TopRamps:
                            _selectedType = EditorType.Blocks;
                            break;
                        default:
                            _previewData = new BlockVertexData();
                            break;
                    }
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Down))
                {
                    switch (_selectedType)
                    {
                        case EditorType.Blocks:
                            _selectedType = EditorType.TopRamps;
                            break;
                        case EditorType.BottomRamps:
                            _selectedType = EditorType.Blocks;
                            break;
                        case EditorType.TopRamps:
                            _selectedType = EditorType.BottomRamps;
                            break;
                        default:
                            _previewData = new BlockVertexData();
                            break;
                    }
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Right))
                {
                    _index++;

                    if (_index >= CountCurrentList())
                    {
                        _index = 0;
                    }
                    changed = true;
                }

                if (InputHelper.IsNewKeyPress(Keys.Left))
                {
                    _index--;

                    if (_index < 0)
                    {
                        _index = CountCurrentList() - 1;
                    }
                    changed = true;
                }
            }

            if (changed)
            {
                switch (_selectedType)
                {
                    case EditorType.Blocks:
                        _activeBlockMask = _blockMasks[_index];
                        _previewData = BlockVertexData.GetBlockMaskVertexData(_blockMasks[_index]).Copy();
                        break;
                    case EditorType.BottomRamps:
                        _activeBlockMask = _bottomRampMasks[_index] | _RampBlockDirection;
                        _previewData = RampBlockVertexData.GetBlockMaskVertexData(_bottomRampMasks[_index] | _RampBlockDirection).Copy();
                        break;
                    case EditorType.TopRamps:
                        _activeBlockMask = _topRampMasks[_index] | _RampBlockDirection;
                        _previewData = RampBlockVertexData.GetBlockMaskVertexData(_topRampMasks[_index] | _RampBlockDirection).Copy();
                        break;
                    default:
                        _previewData = new BlockVertexData();
                        break;
                }
            }

            HandleHighlight();
        }

        public void HandleHighlight()
        {
            Vector3 newPosition = _keyboardCursor.WorldLocation;
            if (InputHelper.IsNewKeyPress(Keys.NumPad3))
            {
                newPosition += WorldDirection.East;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad1))
            {
                newPosition += WorldDirection.North;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad7))
            {
                newPosition += WorldDirection.West;
            }
            if (InputHelper.IsNewKeyPress(Keys.NumPad9))
            {
                newPosition += WorldDirection.South;
            }
            if (InputHelper.IsNewKeyPress(Keys.Add))
            {
                newPosition += WorldDirection.Up;
            }
            if (InputHelper.IsNewKeyPress(Keys.Subtract))
            {
                newPosition += WorldDirection.Down;
            }

            // Following is for valid block ranges only.
            if (ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(newPosition)) != BlockHelper.BlockMasks.Null)
            {
                if (newPosition != _keyboardCursor.WorldLocation)
                {
                    // check for an object?
                    int segmentX = (int)Math.Floor((double)newPosition.X / WorldGlobal.SegmentSize.X);
                    int segmentZ = (int)Math.Floor((double)newPosition.Z / WorldGlobal.SegmentSize.Z);

                    //_ItemsUnderCursor = ParentSegmentManager.Segments[segmentX, segmentZ].Items.FindItemsAt(newPosition);
                }

                _keyboardCursor = new SegmentLocation(newPosition);
                if (InputHelper.IsNewKeyPress(Keys.Delete))
                {
                    ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, BlockHelper.BlockMasks.Air);
                }

                if (InputHelper.IsNewKeyPress(Keys.Insert))
                {
                    BlockMask maskAtCursor = ParentSegmentManager.GetBlockMaskAt(_keyboardCursor);
                    switch (_selectedType)
                    {
                        case EditorType.Blocks:
                            ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, _activeBlockMask);
                            break;
                        case EditorType.BottomRamps:
                            if (BlockHelper.HasTopRamp(maskAtCursor) &&
                                BlockHelper.GetRampBlockDirection(maskAtCursor) ==
                                _RampBlockDirection)
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, maskAtCursor | _activeBlockMask);
                            }
                            else
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, _activeBlockMask);
                            }
                            break;
                        case EditorType.TopRamps:
                            if (BlockHelper.HasBottomRamp(maskAtCursor) &&
                                BlockHelper.GetRampBlockDirection(maskAtCursor) ==
                                _RampBlockDirection)
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, maskAtCursor | _activeBlockMask);
                            }
                            else
                            {
                                ParentSegmentManager.SetBlockMaskAt(_keyboardCursor, _activeBlockMask);
                            }
                            break;
                        default:
                            _previewData = new BlockVertexData();
                            break;
                    }
                }
            }
            else
            {
                DebugLog.Log("Can't move World Cursor to (" + newPosition.X + ", " + newPosition.Y + ", " + newPosition.Z + ") - Out of bounds", DebugMessageType.Warning);
            }
        }

        public int CountCurrentList()
        {
            switch (_selectedType)
            {
                case EditorType.Blocks:
                    return _blockMasks.Count;
                case EditorType.BottomRamps:
                    return _bottomRampMasks.Count;
                case EditorType.TopRamps:
                    return _topRampMasks.Count;
                default:
                    return 0;
            }
        }

        public void DrawBlocks()
        {
            VertexPositionNormalTexture[] previewVerts =
                _previewData.GetAllVertsTranslated(_keyboardCursor.WorldLocation);
            short[] previewIndices = _previewData.GetAllIndices();


            if (previewVerts.Count() > 0)
            {
                ParentSegmentManager.Device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    previewVerts, 0,
                    previewVerts.Count(), previewIndices, 0,
                    previewIndices.Count() / 3);
            }
        }

        public void DrawModels(Camera3D camera)
        {
            BlockMask maskAtCursor = ParentSegmentManager.GetBlockMaskAt(_keyboardCursor);

            switch (_selectedType)
            {
                case EditorType.Blocks:
                    if (ParentSegmentManager.IsLocationObstructed(_keyboardCursor))
                    {
                        _blockHighlightBlocked.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    else
                    {
                        _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    break;
                case EditorType.BottomRamps:
                    if (BlockHelper.HasTopRamp(maskAtCursor) &&
                        BlockHelper.GetRampBlockDirection(maskAtCursor) ==
                        _RampBlockDirection)
                    {
                        _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    else
                    {
                        if (ParentSegmentManager.IsLocationObstructed(_keyboardCursor))
                        {
                            _blockHighlightBlocked.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                        else
                        {
                            _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                    }
                    break;
                case EditorType.TopRamps:
                    if (BlockHelper.HasBottomRamp(maskAtCursor) &&
                        BlockHelper.GetRampBlockDirection(maskAtCursor) ==
                        _RampBlockDirection)
                    {
                        _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                    }
                    else
                    {
                        if (ParentSegmentManager.IsLocationObstructed(_keyboardCursor))
                        {
                            _blockHighlightBlocked.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                        else
                        {
                            _blockHighlight.Draw(ParentSegmentManager.Device, camera, _keyboardCursor.WorldLocation, Vector3.Zero, 0f, new Vector3(1, 1, 1));
                        }
                    }
                    break;
                default:
                    _previewData = new BlockVertexData();
                    break;
            }
        }

        public void Draw(GraphicsDevice device, Camera3D camera)
        {
            if (_previewData.GetAllVertsTranslated(Vector3.Zero).Count() > 0)
            {
                device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    _previewData.GetAllVertsTranslated(_keyboardCursor.WorldLocation), 0,
                    _previewData.GetAllVertsTranslated(Vector3.Zero).Count(), _previewData.GetAllIndices(), 0,
                    _previewData.GetAllIndices().Count() / 3);
            }
        }
    }
}
