// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using sus.Game.Beatmaps;
using sus.Game.Rulesets.Edit;
using sus.Game.Rulesets.Edit.Checks;
using sus.Game.Rulesets.Mania.Objects;
using sus.Game.Rulesets.Objects;
using sus.Game.Rulesets.Objects.Types;
using sus.Game.Rulesets.Osu.Objects;
using sus.Game.Tests.Beatmaps;

namespace sus.Game.Tests.Editing.Checks
{
    [TestFixture]
    public class CheckConcurrentObjectsTest
    {
        private CheckConcurrentObjects check = null!;

        [SetUp]
        public void Setup()
        {
            check = new CheckConcurrentObjects();
        }

        [Test]
        public void TestCirclesSeparate()
        {
            assertOk(new List<HitObject>
            {
                new HitCircle { StartTime = 100 },
                new HitCircle { StartTime = 150 }
            });
        }

        [Test]
        public void TestCirclesConcurrent()
        {
            assertConcurrentSame(new List<HitObject>
            {
                new HitCircle { StartTime = 100 },
                new HitCircle { StartTime = 100 }
            });
        }

        [Test]
        public void TestCirclesAlmostConcurrent()
        {
            assertConcurrentSame(new List<HitObject>
            {
                new HitCircle { StartTime = 100 },
                new HitCircle { StartTime = 101 }
            });
        }

        [Test]
        public void TestSlidersSeparate()
        {
            assertOk(new List<HitObject>
            {
                getSliderMock(startTime: 100, endTime: 400.75d).Object,
                getSliderMock(startTime: 500, endTime: 900.75d).Object
            });
        }

        [Test]
        public void TestSlidersConcurrent()
        {
            assertConcurrentSame(new List<HitObject>
            {
                getSliderMock(startTime: 100, endTime: 400.75d).Object,
                getSliderMock(startTime: 300, endTime: 700.75d).Object
            });
        }

        [Test]
        public void TestSlidersAlmostConcurrent()
        {
            assertConcurrentSame(new List<HitObject>
            {
                getSliderMock(startTime: 100, endTime: 400.75d).Object,
                getSliderMock(startTime: 402, endTime: 902.75d).Object
            });
        }

        [Test]
        public void TestSliderAndCircleConcurrent()
        {
            assertConcurrentDifferent(new List<HitObject>
            {
                getSliderMock(startTime: 100, endTime: 400.75d).Object,
                new HitCircle { StartTime = 300 }
            });
        }

        [Test]
        public void TestManyObjectsConcurrent()
        {
            var hitobjects = new List<HitObject>
            {
                getSliderMock(startTime: 100, endTime: 400.75d).Object,
                getSliderMock(startTime: 200, endTime: 500.75d).Object,
                new HitCircle { StartTime = 300 }
            };

            var issues = check.Run(getContext(hitobjects)).ToList();

            Assert.That(issues, Has.Count.EqualTo(3));
            Assert.That(issues.Where(issue => issue.Template is CheckConcurrentObjects.IssueTemplateConcurrentDifferent).ToList(), Has.Count.EqualTo(2));
            Assert.That(issues.Any(issue => issue.Template is CheckConcurrentObjects.IssueTemplateConcurrentSame));
        }

        private Mock<Slider> getSliderMock(double startTime, double endTime, int repeats = 0)
        {
            var mock = new Mock<Slider>();
            mock.SetupGet(s => s.StartTime).Returns(startTime);
            mock.As<IHasRepeats>().Setup(r => r.RepeatCount).Returns(repeats);
            mock.As<IHasDuration>().Setup(d => d.EndTime).Returns(endTime);

            return mock;
        }

        private Mock<HoldNote> getHoldNoteMock(double startTime, double endTime, int column)
        {
            var mock = new Mock<HoldNote>();
            mock.SetupGet(s => s.StartTime).Returns(startTime);
            mock.As<IHasDuration>().Setup(d => d.EndTime).Returns(endTime);
            mock.As<IHasColumn>().Setup(c => c.Column).Returns(column);

            return mock;
        }

        private void assertOk(List<HitObject> hitobjects)
        {
            Assert.That(check.Run(getContext(hitobjects)), Is.Empty);
        }

        private void assertConcurrentSame(List<HitObject> hitobjects, int count = 1)
        {
            var issues = check.Run(getContext(hitobjects)).ToList();

            Assert.That(issues, Has.Count.EqualTo(count));
            Assert.That(issues.All(issue => issue.Template is CheckConcurrentObjects.IssueTemplateConcurrentSame));
        }

        private void assertConcurrentDifferent(List<HitObject> hitobjects, int count = 1)
        {
            var issues = check.Run(getContext(hitobjects)).ToList();

            Assert.That(issues, Has.Count.EqualTo(count));
            Assert.That(issues.All(issue => issue.Template is CheckConcurrentObjects.IssueTemplateConcurrentDifferent));
        }

        private BeatmapVerifierContext getContext(List<HitObject> hitobjects)
        {
            var beatmap = new Beatmap<HitObject> { HitObjects = hitobjects };
            return new BeatmapVerifierContext(beatmap, new TestWorkingBeatmap(beatmap));
        }
    }
}
