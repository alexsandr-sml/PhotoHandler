using ImageMagick;
using System;
using System.IO;

namespace PhotoHandler
{
    class Program
    {
        private const string path = @"C:\Users\alexsandr\Google Диск\Картинки\Украшение";
        static void Main(string[] args)
        {
            var output = Path.Combine(path, "Output");
            if (Directory.Exists(output))
            {
                Directory.Delete(output, true);
            }

            Directory.CreateDirectory(output);

            var files = Directory.GetFiles(path, "*.jpg", SearchOption.TopDirectoryOnly);
            var counter = 1;
            foreach (var file in files)
            {
                Handle(file, output, $"photo_{counter++:D2}.jpg");
                //File.Move(file, Path.Combine(path, $"photo_{counter++:D2}.jpg"));
            }
        }

        private static void Handle(string filename, string output, string outputFilename)
        {
            // Read image that needs a watermark
            using (var image = new MagickImage(filename))
            {
                new Drawables()
                    .FontPointSize(72)
                    .Font("Comic Sans")
                    .StrokeColor(new MagickColor("yellow"))
                    .FillColor(MagickColors.Orange)
                    .TextAlignment(TextAlignment.Center)
                    .Text(256, image.Height - 50, "alexsandria.by")
                    .Draw(image);

                // Save the result
                image.Write(Path.Combine(output, outputFilename));
            }
        }
    }
}
