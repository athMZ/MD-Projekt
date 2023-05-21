using System.Diagnostics;
using System.Drawing;

#pragma warning disable CA1416

namespace GraphMatrix
{
    public class DrawingModule
    {
        private const string GraphFileName = @"graph.png";

        public static void OpenWithDefaultProgram(string path)
        {
            using var fileOpener = new Process();

            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = "\"" + path + "\"";
            fileOpener.Start();
        }

        public static void DrawGraphOnCircle(Graph graph)
        {
            const int width = 600;
            const int height = 600;

            var background = new Rectangle(0, 0, width, height);

            using var bmp = new Bitmap(width, height);
            using var gfx = System.Drawing.Graphics.FromImage(bmp);

            gfx.FillRectangle(Brushes.White, background);

            const int radius = 250;
            const int circleRadius = 30;
            var center = new Point(width / 2 - circleRadius / 3, height / 2 - circleRadius / 3);
            var angle = 0.0;
            var angleStep = 2 * Math.PI / graph.Rows;

            List<Tuple<int, int, int, int>> created = new();

            for (var i = 1; i <= graph.Rows; i++)
            {
                var n = graph.GetNeighboursOfVertex(i);

                var circleX = (int)(center.X + radius * Math.Cos(angle));
                var circleY = (int)(center.Y + radius * Math.Sin(angle));

                gfx.FillEllipse(Brushes.Black, circleX, circleY, circleRadius, circleRadius);
                gfx.FillEllipse(Brushes.LightBlue, circleX + 2, circleY + 2, circleRadius - 4, circleRadius - 4);

                const int spacer = 25;
                var textX = (int)(center.X + (radius + spacer) * Math.Cos(angle));
                var textY = (int)(center.Y + (radius + spacer) * Math.Sin(angle));

                gfx.DrawString((i + 1).ToString(), new Font("Arial", 10), Brushes.Black, textX + circleRadius / 3, textY + circleRadius / 3);

                angle += angleStep;

                foreach (var neighbour in n)
                {
                    var x1 = (int)(center.X + radius * Math.Cos(angleStep * neighbour));
                    var y1 = (int)(center.Y + radius * Math.Sin(angleStep * neighbour));

                    var linePoints = new Tuple<int, int, int, int>(circleX, circleY, x1, y1);

                    if (created.Contains(linePoints)) continue;

                    gfx.DrawLine(new Pen(Brushes.Black, 2), circleX + circleRadius / 2, circleY + circleRadius / 2, x1 + circleRadius / 2, y1 + circleRadius / 2);

                    created.Add(linePoints);
                }
            }

            bmp.Save(GraphFileName);
            OpenWithDefaultProgram(GraphFileName);
        }
    }
}