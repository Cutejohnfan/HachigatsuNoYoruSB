using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System.Linq;

namespace StorybrewScripts
{
    public class BackgroundMovingBlur : StoryboardObjectGenerator
    {
        [Configurable]
        public string BackgroundPath = "";

        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public double Opacity = 0.2;

        [Configurable]
        public int startX = 0;

        [Configurable]
        public int startY = 0;

        [Configurable]
        public int endX = 0;

        [Configurable]
        public int endY = 0;

        public override void Generate()
        {
            if (BackgroundPath == "") BackgroundPath = Beatmap.BackgroundPath ?? string.Empty;
            if (StartTime == EndTime) EndTime = (int)(Beatmap.HitObjects.LastOrDefault()?.EndTime ?? AudioDuration);

            var bitmap = GetMapsetBitmap(BackgroundPath);
            var bg = GetLayer("").CreateSprite(BackgroundPath, OsbOrigin.Centre);
            bg.Scale(StartTime, 680.0f / bitmap.Height);
            bg.Fade(StartTime, StartTime, 0, 0);
            bg.Move(StartTime, EndTime, startX, startY, endX, endY);
            bg.Fade(21414, 59712, 1, 1);
            bg.Fade(59712, 59712, 0, 0);
            bg.Fade(62584, 62584, 0, 0);
            bg.Fade(82691, 82691, 1, 1);
            bg.Fade(EndTime, EndTime + 500, Opacity, 0);
        }
    }
}
