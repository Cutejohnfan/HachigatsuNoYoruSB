using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class Flash : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        public override void Generate()
        {
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = GetLayer("").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 980.0f / bitmap.Height);
            bg.Fade(0, 0, 0, 0);
            bg.Fade(988, 988 + 200, 0.8, 0);
            bg.Fade(11201, 11201 + 200, 0.8, 0);
            bg.Fade(21414, 21414 + 200, 0.8, 0);
            bg.Fade(31627, 31627 + 200, 0.8, 0);
            bg.Fade(41840, 41840 + 200, 0.8, 0);
            bg.Fade(52052, 52052 + 200, 0.8, 0);
            bg.Fade(59712, 59712 + 200, 0.8, 0);
            bg.Fade(62584, 62584 + 200, 0.8, 0);
            bg.Fade(72478, 72478 + 200, 0.8, 0);
            bg.Fade(82691, 82691 + 200, 0.8, 0);

        }
    }
}
