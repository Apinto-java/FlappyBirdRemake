using Godot;

namespace FlappyBirdRemake
{
    public partial class Globals : Node
    {
        public float ScrollSpeed { get; set; } = 1f;
        public float InitialDistanceBetweenPipes { get; } = 70.0f;
        public float CurrentDistanceBetweenPipes { get; set; }

        public float InitialGapBetweenPipes { get; } = 24.0f;
        public float CurrentGapBetweenPipesInPixels { get; set; }

        public Globals()
        {
            CurrentGapBetweenPipesInPixels = InitialGapBetweenPipes;
            CurrentDistanceBetweenPipes = InitialDistanceBetweenPipes;
        }

    }
}
