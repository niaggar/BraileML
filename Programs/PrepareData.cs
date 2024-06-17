using BraileML.Models;
using BraileML.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Programs;

public class PrepareData
{
    public void Run1()
    {
        // Prepare the data
        var path = @"C:\Users\nicho\Desktop\TrainImages\Convinations\";
        var images = FileManager.ReadImagesPointsAlphabet(path);

        // Resize the images
        for (var i = 0; i < images.Length; i++)
        {
            ImagesManager.Resize(28, 28, ref images[i]);
        }

        // Save the images
        var savePath = @"C:\Users\nicho\Desktop\TrainImages\Convinations\Processed\";
        var groups = images.GroupBy(x => x.Character);
        foreach (var group in groups)
        {
            for (var i = 0; i < group.Count(); i++)
            {
                var image = group.ElementAt(i);
                var imageName = $"{image.Character}-{i}.jpg";
                FileManager.SaveImage(Path.Combine(savePath, imageName), image);
            }
        }
    }

    public void Run2()
    {
        // Prepare the data
        var path = @"C:\Users\nicho\Desktop\TrainImages\Processed\";
        var images = FileManager.ReadImagesPoints(path);
        
        //// Resize the images
        //for (var i = 0; i < images.Length; i++)
        //{
        //    ImagesManager.Resize(28, 28, ref images[i]);
        //}

        ////// Treshold the images
        ////for (var i = 0; i < images.Length; i++)
        ////{
        ////    ImagesManager.Treshold(0.3, ref images[i]);
        ////}

        //// Rotate the images to have the counterpart
        //var newImages = new List<ImageModel>();
        //for (var i = 0; i < images.Length; i++)
        //{
        //    var imageCopy = images[i].Copy();
        //    imageCopy.Character = imageCopy.Character switch
        //    {
        //        '1' => '6',
        //        '3' => '4',
        //        '5' => '2',
        //        _ => throw new NotImplementedException(),
        //    };
        //    ImagesManager.Rotate(180, ref imageCopy);

        //    newImages.Add(imageCopy);
        //}
        //newImages.AddRange(images);
        //images = newImages.ToArray();
        //newImages = new List<ImageModel>();

        //// Create new images with random rotations, zooms and translations
        //var random = new Random();
        //var numberOfNewImages = 4;
        //for (var i = 0; i < images.Length; i++)
        //{
        //    for (var j = 0; j < numberOfNewImages; j++)
        //    {
        //        var imageCopy = images[i].Copy();

        //        var rotation = RandomUtil.GenerateRandomDouble(-10, 10);
        //        var zoom = RandomUtil.GenerateRandomDouble(0.9, 1.1);
        //        var xMove = RandomUtil.GenerateRandomDouble(-2, 2);
        //        var yMove = RandomUtil.GenerateRandomDouble(-2, 2);

        //        ImagesManager.Rotate(rotation, ref imageCopy);
        //        ImagesManager.Zoom(zoom, ref imageCopy);
        //        ImagesManager.Translate((int)xMove, (int)yMove, ref imageCopy);

        //        newImages.Add(imageCopy);
        //    }
        //}
        //newImages.AddRange(images);
        //images = newImages.ToArray();

        //// Random noise
        //for (var i = 0; i < images.Length; i++)
        //{
        //    ImagesManager.RandomNoise(0.08, ref images[i]);
        //}

        // Save the images
        var savePath = @"C:\Users\nicho\Desktop\TrainImages\testPhotos\";
        var groups = images.GroupBy(x => x.Character);
        foreach (var group in groups)
        {
            for (var i = 0; i < group.Count(); i++)
            {
                var image = group.ElementAt(i);
                var imageName = $"{image.Character}-{i}.jpg";
                FileManager.SaveImage(Path.Combine(savePath, imageName), image);
            }
        }
    }
}
