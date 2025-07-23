// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

#nullable disable

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using sus.Game.Graphics;
using sus.Game.Graphics.Sprites;

namespace sus.Game.Tests.Visual.UserInterface
{
    public partial class TestSceneOsuFont : OsuTestScene
    {
        private OsuSpriteText spriteText;

        private readonly BindableBool useAlternates = new BindableBool();
        private readonly Bindable<FontWeight> weight = new Bindable<FontWeight>(FontWeight.Regular);

        [BackgroundDependencyLoader]
        private void load()
        {
            Child = spriteText = new OsuSpriteText
            {
                Origin = Anchor.Centre,
                Anchor = Anchor.Centre,
                RelativeSizeAxes = Axes.X,
                AllowMultiline = true,
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            useAlternates.BindValueChanged(_ => updateFont());
            weight.BindValueChanged(_ => updateFont(), true);
        }

        private void updateFont()
        {
            FontUsage usage = useAlternates.Value ? OsuFont.TorusAlternate : OsuFont.Torus;
            spriteText.Font = usage.With(size: 40, weight: weight.Value);
        }

        [Test]
        public void TestTorusAlternates()
        {
            AddStep("set all ASCII letters", () => spriteText.Text = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ
abcdefghijklmnopqrstuvwxyz");
            AddStep("set all alternates", () => spriteText.Text = @"A Á Ă Â Ä À Ā Ą Å Ã
Æ B D Ð Ď Đ E É Ě Ê
Ë Ė È Ē Ę F G Ğ Ģ Ġ
H I Í Î Ï İ Ì Ī Į K
Ķ O Œ P Þ Q R Ŕ Ř Ŗ
T Ŧ Ť Ţ Ț V W Ẃ Ŵ Ẅ
Ẁ X Y Ý Ŷ Ÿ Ỳ a á ă
â ä à ā ą å ã æ b d
ď đ e é ě ê ë ė è ē
ę f g ğ ģ ġ k ķ m n
ń ň ņ ŋ ñ o œ p þ q
t ŧ ť ţ ț u ú û ü ù
ű ū ų ů w ẃ ŵ ẅ ẁ x
y ý ŷ ÿ ỳ");

            AddToggleStep("toggle alternates", alternates => useAlternates.Value = alternates);

            addSetWeightStep(FontWeight.Light);
            addSetWeightStep(FontWeight.Regular);
            addSetWeightStep(FontWeight.SemiBold);
            addSetWeightStep(FontWeight.Bold);

            void addSetWeightStep(FontWeight newWeight) => AddStep($"set weight {newWeight}", () => weight.Value = newWeight);
        }
    }
}
