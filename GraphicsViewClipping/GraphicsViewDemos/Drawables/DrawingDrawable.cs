using System.Reflection;
using Font = Microsoft.Maui.Graphics.Font;
using IImage = Microsoft.Maui.Graphics.IImage;
using Microsoft.Maui.Graphics.Platform;

namespace GraphicsViewDemos.Drawables
{
    internal class ClippingDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            IImage image;
            var assembly = GetType().GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("GraphicsViewDemos.Resources.Images.dotnet_bot.png"))
            {
                image = PlatformImage.FromStream(stream);
            }

            if (image != null)
            {
                PathF path = new PathF();
                path.AppendCircle(100, 90, 80);
                canvas.ClipPath(path);  // Must be called before DrawImage
                canvas.DrawImage(image, 10, 10, image.Width, image.Height);
            }
        }
    }

    internal class SubtractClippingDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            IImage image;
            var assembly = GetType().GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("GraphicsViewDemos.Resources.Images.dotnet_bot.png"))
            {
                image = PlatformImage.FromStream(stream);
            }

            if (image != null)
            {
                canvas.SubtractFromClip(60, 60, 90, 90);
                canvas.DrawImage(image, 10, 10, image.Width, image.Height);
            }
        }
    }
}
