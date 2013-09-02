// -----------------------------------------------------------------------
// <copyright file="SegmentRender.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using BlockWorldPrototype.Core.Debug;
using BlockWorldPrototype.Core.Graphics;
using BlockWorldPrototype.World.Block;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockWorldPrototype.World.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SegmentRender
    {
        public Segment ParentRenderSegment;
        public BlockMask[, ,] Blocks;
        private CachedGraphicsBuffer<VertexPositionNormalTexture> _blockDrawModule;
        private GraphicsDevice Device;
        public bool Dirty;
        private byte _heightOffset;
        private Vector3 _locationOffset;

        public SegmentRender(GraphicsDevice device, Segment container, int heightOffset)
        {
            ParentRenderSegment = container;
            _heightOffset = (byte)heightOffset;
            Dirty = true;
            Device = device;
            Blocks =
                new BlockMask[WorldGlobal.RenderSegmentSize.X, WorldGlobal.RenderSegmentSize.Y, WorldGlobal.RenderSegmentSize.Z
                    ];

            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        Blocks[x, y, z] = BlockHelper.BlockMasks.Air;
                    }
                }
            }


            _blockDrawModule = new CachedGraphicsBuffer<VertexPositionNormalTexture>(Device,
                VertexPositionNormalTexture.VertexDeclaration);

            _locationOffset = new Vector3(ParentRenderSegment.Position.X * WorldGlobal.SegmentSize.X,
                _heightOffset * WorldGlobal.RenderSegmentSize.Y, ParentRenderSegment.Position.Y
                                                            * WorldGlobal.SegmentSize.Z);
        }

        public void Update(GameTime gameTime)
        {
            if (Dirty)
                UpdateDrawModule();
        }

        public void Fill(BlockMask mask)
        {
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        Blocks[x, y, z] = mask;
                    }
                }
            }

            Dirty = true;
        }

        public void UpdateDrawModule()
        {
            DebugLog.Log("Updating RenderChunk: (" + ParentRenderSegment.Position.X + ", " + _heightOffset + ", " + ParentRenderSegment.Position.Y + ");");
            for (int x = 0; x < Blocks.GetLength(0); x++)
            {
                for (int y = 0; y < Blocks.GetLength(1); y++)
                {
                    for (int z = 0; z < Blocks.GetLength(2); z++)
                    {
                        if (Blocks[x, y, z] != BlockHelper.BlockMasks.Air && Blocks[x, y, z] != BlockHelper.BlockMasks.AirBlocked)
                        {
                            BlockVertexData currentVertextData = new BlockVertexData();
                            Vector3 blockLocation = new Vector3(x, y, z) + _locationOffset;
                            if (BlockHelper.IsBlock(Blocks[x, y, z]))
                            {
                                currentVertextData = BlockVertexData.GetCopyOfBlockMasBlockVertexData(Blocks[x, y, z]);
                            }
                            else if (BlockHelper.IsRampBlock(Blocks[x, y, z]))
                            {
                                currentVertextData = RampBlockVertexData.GetBlockMaskVertexData(Blocks[x, y, z]);
                            }

                            currentVertextData.TranslateToWorldLocation(blockLocation);

                            // Following checks all the face blocking cubes around the current location to see if we'll add the vertices to the buffer or not.
                            if (!BlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(blockLocation + WorldDirection.Up)), Blocks[x, y, z], WorldDirection.Up))
                            {
                                _blockDrawModule.AddData(currentVertextData.UpVertices.ToList(),
                                    currentVertextData.UpIndices.ToList());
                            }
                            if (!BlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(blockLocation + WorldDirection.Down)), Blocks[x, y, z], WorldDirection.Down))
                            {
                                _blockDrawModule.AddData(currentVertextData.DownVertices.ToList(),
                                    currentVertextData.DownIndices.ToList());
                            }
                            if (!BlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(blockLocation + WorldDirection.North)), Blocks[x, y, z], WorldDirection.North))
                            {
                                _blockDrawModule.AddData(currentVertextData.NorthVertices.ToList(),
                                    currentVertextData.NorthIndices.ToList());
                            }
                            if (!BlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(blockLocation + WorldDirection.East)), Blocks[x, y, z], WorldDirection.East))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(currentVertextData.EastVertices.ToList(),
                                    currentVertextData.EastIndices.ToList());
                            }
                            if (!BlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(blockLocation + WorldDirection.South)), Blocks[x, y, z], WorldDirection.South))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(currentVertextData.SouthVertices.ToList(),
                                    currentVertextData.SouthIndices.ToList());
                            }
                            if (!BlockHelper.DoesBlockMaskAObscureMaskBFace(ParentRenderSegment.ParentSegmentManager.GetBlockMaskAt(new SegmentLocation(blockLocation + WorldDirection.West)), Blocks[x, y, z], WorldDirection.West))
                            {
                                // add east vertices/indices, but don't forget to translate to world location...
                                _blockDrawModule.AddData(currentVertextData.WestVertices.ToList(),
                                    currentVertextData.WestIndices.ToList());
                            }
                        }
                    }
                }
            }
            _blockDrawModule.PrepareToDraw();

            Dirty = false;
        }

        public void DrawBlocks()
        {
            _blockDrawModule.Draw();
        }
    }
}
