using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;

namespace StorybrewScripts
{
    public class Bars : StoryboardObjectGenerator
    {
        [Configurable]
        public int StartTime = 0;

        [Configurable]
        public int EndTime = 0;

        [Configurable]
        public int BeatDivisor = 8;

        [Configurable]
        public int StartFadeDuration = 200;

        [Configurable]
        public int EndFadeDuration = 200;

        [Configurable]
        public string SpritePath = "";

        [Configurable]
        public double SpriteScale = 1;

        public override void Generate()
        {
            var hitobjectLayer = GetLayer("");
            foreach (var hitobject in Beatmap.HitObjects)
            {
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
//Appear first 
//Change color when hit + fade
                var hSprite = hitobjectLayer.CreateSprite(SpritePath, OsbOrigin.Centre, hitobject.Position);
                hSprite.Scale(OsbEasing.In, hitobject.StartTime, hitobject.EndTime + EndFadeDuration, 1, 1);
                hSprite.Fade(OsbEasing.In, hitobject.StartTime - StartFadeDuration, hitobject.EndTime + EndFadeDuration, 0.8, 0);
                hSprite.Additive(hitobject.StartTime, hitobject.EndTime + EndFadeDuration);
                //Make it Green!
                hSprite.Color(OsbEasing.In, hitobject.StartTime - StartFadeDuration, hitobject.StartTime, 252, 46, 32, 252, 46, 32);
                //Make it White!
                hSprite.Color(OsbEasing.In, hitobject.EndTime, hitobject.EndTime + EndFadeDuration, 252, 46, 32, 252, 46, 32);
                
 
                if (hitobject is OsuSlider)
                {
                    var timestep = Beatmap.GetTimingPointAt((int)hitobject.StartTime).BeatDuration / BeatDivisor;
                    var startTime = hitobject.StartTime;
                    while (true)
                    {
                        var endTime = startTime + timestep;

                        var complete = hitobject.EndTime - endTime < 5;
                        if (complete) endTime = hitobject.EndTime;

                        var startPosition = hSprite.PositionAt(startTime);
                        hSprite.Move(startTime, endTime, startPosition, hitobject.PositionAtTime(endTime));

                        if (complete) break;
                        startTime += timestep;
                    }
                }
            }
        }
    }
}
