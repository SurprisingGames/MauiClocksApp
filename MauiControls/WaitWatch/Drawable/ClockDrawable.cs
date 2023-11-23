namespace MauiControls.WaitWatch.Drawable
{
    public class ClockDrawable : IDrawable
    {
        private const float Alpha = 0.2f;
        readonly DateTime _startTime = DateTime.Now;

        private readonly List<List<PathF>> _listOfPath = new()
        {
            Capacity = 0
        };
        private static PathF? _outerTicPath;

        static Color _fillColor = Color.FromArgb("#FF00FF00");
        public void SetNewColor(Color color)
        {
            _fillColor = color;
        }

        public ClockDrawable()
        {
            for (var i = 0; i < 2; i++)
            {
                var list = new List<PathF>();
                for (var j = 0; j < 7; j++)
                {
                    var path = new PathBuilder().BuildPath(_listOfSegments[i * 7 + j]);
                    list.Add(path);
                }

                _listOfPath.Add(list);
            }
            var tickPath = "M140.28,41.54 l -3.21,-27.11 l 4.78,-1.89 l 5.47,1.22 l 0.65,27.39 l -3.7,2.38 z";
            _outerTicPath = new PathBuilder().BuildPath(tickPath);
        }

        readonly List<string> _listOfSegments = new()
        {
            {"M85,197.88 l 8.15,-8.77 l 30.75,-0.2 l 7.59,8.42 l -3.89,3.69 l -40.11,0.15 z"},
            {"M80,191.12 l 4.39,-30.79 l 7.36,-7.85 l 5.38,6.09 l -3.67,27.19 l -9.68,10.47 z"},
            {"M86.77,143.9 l 4.68,-33.68 l 4.93,-4.71 l 7.41,8.96 l -4.67,27.12 l -7.49,7.73 z"},
            {"M97.9,104.06 l 3.94,-4.06 l 40.1,0 l 1.84,2.29 l -8.82,9.61 l -29.7,0.06 z"},
            {"M132.03,143.97 l 4.02,-30.3 l 9.19,-9.8 l 4.46,5.38 l -4.77,34.84 l -7.36,5.66 z"},
            {"M125.71,187.89 l 4.04,-29.55 l 7.33,-5.84 l 5.65,6.07 l -4.16,31.84 l -5.38,5.21 z"},
            {"M93.15,150.97 l 6.39,-6.82 l 29.94,0 l 6.39,6.88 l -7.55,5.78 l -29.69,0 z"},
            {"M155,197.88 l 8.15,-8.77 l 30.75,-0.2 l 7.59,8.42 l -3.89,3.69 l -40.11,0.15 z"},
            {"M150,191.12 l 4.39,-30.79 l 7.36,-7.85 l 5.38,6.09 l -3.67,27.19 l -9.68,10.47 z"},
            {"M156.77,143.9 l 4.68,-33.68 l 4.93,-4.71 l 7.41,8.96 l -4.67,27.12 l -7.49,7.73 z"},
            {"M167.9,104.06 l 3.94,-4.06 l 40.1,0 l 1.84,2.29 l -8.82,9.61 l -29.7,0.06 z"},
            {"M202.03,143.97 l 4.02,-30.3 l 9.19,-9.8 l 4.46,5.38 l -4.77,34.84 l -7.36,5.66 z"},
            {"M195.71,187.89 l 4.04,-29.55 l 7.33,-5.84 l 5.65,6.07 l -4.16,31.84 l -5.38,5.21 z"},
            {"M163.15,150.97 l 6.39,-6.82 l 29.94,0 l 6.39,6.88 l -7.55,5.78 l -29.69,0 z"}
        };

        readonly List<List<float>> _listOfOpacity = new()
        {
            new List<float> { 1, 1, 1, 1, 1, 1, Alpha  },// 0
            new List<float> { Alpha, Alpha, Alpha, Alpha, 1, 1, Alpha  },// 1
            new List<float> { 1, 1, Alpha, 1, 1, Alpha,1  },//2
            new List<float> { 1, Alpha, Alpha, 1, 1, 1, 1  },// 3
            new List<float> { Alpha, Alpha, 1, Alpha, 1, 1, 1  }, // 4
            new List<float> { 1, Alpha, 1,1, Alpha, 1, 1  }, // 5
            new List<float> { 1,1,1,1,Alpha,1,1 }, // 6
            new List<float> { Alpha, Alpha, Alpha, 1, 1, 1, Alpha  },// 7
            new List<float> {1,1,1,1,1,1,1},// 8
            new List<float> { 1,Alpha,1,1,1,1,1 }, // 9
        };

        void DrawNumber(ICanvas canvas, int index, int digitIndex)
        {
            List<float> opacity = _listOfOpacity[index];
            List<PathF> listPath = _listOfPath[digitIndex];
            for (var i = 0; i < 7; i++)
            {
                canvas.Alpha = opacity[i];
                canvas.FillPath(listPath[i]);
            }
            canvas.Alpha = 1;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            var dt = DateTime.Now;
            var ts = dt - _startTime;

            var seconds = ts.Seconds;
            var lowSecond = seconds % 10;
            var highSecond = seconds / 10;

            var center = dirtyRect.Center;
            canvas.FillColor = Colors.Black;

            float diameter = 300;
            canvas.FillEllipse(center.X - (float)(diameter / 2), center.Y - (float)(diameter / 2), diameter, diameter);

            canvas.FillColor = _fillColor;

            for (var i = 0; i < 60; i++)
            {
                var tick = seconds == 0 ? 60 : seconds;
                canvas.Alpha = tick > i ? 1 : Alpha;
                canvas.FillPath(_outerTicPath?.Rotate(6 * i, center));
            }

            DrawNumber(canvas, highSecond, 0);
            DrawNumber(canvas, lowSecond, 1);
            canvas.Alpha = 1;
        }
    }
}
