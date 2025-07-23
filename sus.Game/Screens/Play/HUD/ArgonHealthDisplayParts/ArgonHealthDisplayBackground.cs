// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Runtime.InteropServices;
using sus.Framework.Allocation;
using sus.Framework.Graphics;
using sus.Framework.Graphics.Rendering;
using sus.Framework.Graphics.Shaders;
using sus.Framework.Graphics.Shaders.Types;
using sus.Framework.Graphics.Shapes;
using sus.Framework.Graphics.Sprites;
using susTK;

namespace sus.Game.Screens.Play.HUD.ArgonHealthDisplayParts
{
    public partial class ArgonHealthDisplayBackground : Box
    {
        [BackgroundDependencyLoader]
        private void load(ShaderManager shaders)
        {
            TextureShader = shaders.Load(VertexShaderDescriptor.TEXTURE_2, "ArgonBarPathBackground");
        }

        protected override DrawNode CreateDrawNode() => new ArgonBarPathDrawNode(this);

        private class ArgonBarPathDrawNode : SpriteDrawNode
        {
            protected new ArgonHealthDisplayBackground Source => (ArgonHealthDisplayBackground)base.Source;

            private IUniformBuffer<ArgonBarPathBackgroundParameters>? parametersBuffer;

            public ArgonBarPathDrawNode(ArgonHealthDisplayBackground source)
                : base(source)
            {
            }

            private Vector2 size;

            public override void ApplyState()
            {
                base.ApplyState();
                size = Source.DrawSize;
            }

            protected override void BindUniformResources(IShader shader, IRenderer renderer)
            {
                base.BindUniformResources(shader, renderer);

                parametersBuffer ??= renderer.CreateUniformBuffer<ArgonBarPathBackgroundParameters>();
                parametersBuffer.Data = new ArgonBarPathBackgroundParameters { Size = size };

                shader.BindUniformBlock("m_ArgonBarPathBackgroundParameters", parametersBuffer);
            }

            protected override bool CanDrawOpaqueInterior => false;

            protected override void Dispose(bool isDisposing)
            {
                base.Dispose(isDisposing);
                parametersBuffer?.Dispose();
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private record struct ArgonBarPathBackgroundParameters
            {
                public UniformVector2 Size;
                private readonly UniformPadding8 pad;
            }
        }
    }
}
