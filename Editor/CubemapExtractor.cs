using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class CubemapExtractor : Editor
{
    private static readonly CubemapFace[] CubemapFaces = new CubemapFace[]
    {
        CubemapFace.PositiveX, // Right
        CubemapFace.NegativeX, // Left
        CubemapFace.PositiveY, // Up
        CubemapFace.NegativeY, // Down
        CubemapFace.NegativeZ, // Back
        CubemapFace.PositiveZ  // Front
    };

    private static readonly string[] FaceNames = new string[]
    {
        "Right", "Left", "Up", "Down", "Back", "Front"
    };

    [MenuItem("Assets/Extract Cubemap Images", true)]
    private static bool ValidateExtractCubemapImages()
    {
        return Selection.objects.All(obj => obj is Cubemap);
    }

    [MenuItem("Assets/Extract Cubemap Images")]
    private static void ExtractCubemapImages()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj is Cubemap cubemap)
            {
                string assetPath = AssetDatabase.GetAssetPath(cubemap);
                string folderPath = Path.Combine(Path.GetDirectoryName(assetPath), cubemap.name);
                string fileName = cubemap.name;

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                for (int i = 0; i < 6; i++)
                {
                    CubemapFace face = CubemapFaces[i];
                    Texture2D faceTexture = new Texture2D(cubemap.width, cubemap.height, TextureFormat.RGBA32, false);
                    faceTexture.SetPixels(MirrorTexture(FlipTexture(cubemap.GetPixels(face))));
                    faceTexture.Apply();

                    byte[] bytes = faceTexture.EncodeToPNG();
                    string saveFilePath = Path.Combine(folderPath, $"{FaceNames[i]}.png");

                    File.WriteAllBytes(saveFilePath, bytes);
                    Debug.Log($"Saved {FaceNames[i]}.png at {saveFilePath}");

                    AssetDatabase.Refresh();
                }
            }
        }
    }

    private static Color[] FlipTexture(Color[] pixels)
    {
        int width = Mathf.FloorToInt(Mathf.Sqrt(pixels.Length));
        int height = width;

        Color[] flippedPixels = new Color[pixels.Length];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int flippedY = height - 1 - y;
                flippedPixels[y * width + x] = pixels[flippedY * width + x];
            }
        }

        return flippedPixels;
    }

    private static Color[] MirrorTexture(Color[] pixels)
    {
        int width = Mathf.FloorToInt(Mathf.Sqrt(pixels.Length));
        int height = width;

        Color[] mirroredPixels = new Color[pixels.Length];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int mirroredX = width - 1 - x;
                mirroredPixels[y * width + x] = pixels[y * width + mirroredX];
            }
        }

        return mirroredPixels;
    }
}
