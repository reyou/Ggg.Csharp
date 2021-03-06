using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Newtonsoft.Json;

namespace GggCloudinary
{
    public class TestUtilities
    {
        public static Cloudinary GetCloudinary()
        {
            string cloud = GetCloudName();
            string apiKey = GetApiKey();
            string apiSecret = GetApiSecret();
            Account account = new Account(cloud, apiKey, apiSecret);
            Cloudinary cloudinary = new Cloudinary(account);
            return cloudinary;
        }

        private static string GetApiSecret()
        {
            return File.ReadAllText(@"D:\apikeys\cloudinary\apiSecret.txt");
        }

        private static string GetApiKey()
        {
            return File.ReadAllText(@"D:\apikeys\cloudinary\apiKey.txt");
        }

        private static string GetCloudName()
        {
            return File.ReadAllText(@"D:\apikeys\cloudinary\cloud.txt");
        }

        public static void LogAndWrite(object objectItem, string fileName)
        {
            Trace.WriteLine(objectItem);
            string serializeObject = JsonConvert.SerializeObject(objectItem, Formatting.Indented);
            string path = @"D:\Git\Ggg.Github\Ggg.Csharp\apps\app-cloudinary\GggCloudinary\TestOutputs\" + fileName;
            File.WriteAllText(path, serializeObject);
        }

        public static string GetImagesFolder()
        {
            return @"D:\Git\Ggg.Github\Ggg.Csharp\apps\app-cloudinary\images";
        }
        public static string GetVideosFolder()
        {
            return @"D:\Git\Ggg.Github\Ggg.Csharp\apps\app-cloudinary\videos";
        }

        public static void UploadImageWithName(string name = "zombie", string url = "https://c.pxhere.com/photos/90/4b/carnival_venice_eyes_mask_woman-947285.jpg!d")
        {
            Cloudinary cloudinary = GetCloudinary();
            ImageUploadParams uploadParams = new ImageUploadParams
            {
                File = new FileDescription(url),
                PublicId = name,
                Invalidate = true
            };
            ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
            LogAndWrite(uploadResult, "UploadImageWithName.txt");
        }

        public static VideoUploadResult UploadVideo()
        {
            Cloudinary cloudinary = GetCloudinary();
            VideoUploadParams uploadParams = new VideoUploadParams()
            {
                File = new FileDescription(GetVideosFolder() + @"\dog.mp4"),
                PublicId = "dog",
                EagerTransforms = new List<Transformation>()
                {
                    new Transformation().Width(300).Height(300).Crop("pad").AudioCodec("none"),
                    new Transformation().Width(160).Height(100).Crop("crop").Gravity("south").AudioCodec("none"),
                },
                EagerAsync = true,
                EagerNotificationUrl = "https://requestbin.fullcontact.com/1964k7d1"
            };
            VideoUploadResult uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult;
        }
    }
}