using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.UI;

public class RChannel : MonoBehaviour
{
    public Texture2D sourceTex;

    private void Start()
    {
        GenerateJobs(new Rect(0, 0, sourceTex.width / 2, sourceTex.height / 2));
        GenerateJobs(new Rect(sourceTex.width / 2, 0, sourceTex.width / 2, sourceTex.height / 2));
        GenerateJobs(new Rect(0, sourceTex.height / 2, sourceTex.width / 2, sourceTex.height / 2));
        GenerateJobs(new Rect(sourceTex.width / 2,sourceTex.height/2, sourceTex.width / 2, sourceTex.height / 2));
    }

    public void GenerateJobs(Rect textureRectArea)
    {
        float[] RChannelValues = GetPixelValuesFromAreas(textureRectArea);

        NativeArray<float> NativeRChannel = new NativeArray<float>(RChannelValues, Allocator.TempJob);
        TextureJob textJob = new TextureJob()
        {
            MyFloats = NativeRChannel,
            sum = 0
        };
        JobHandle jHandle = textJob.Schedule();
        jHandle.Complete();
        NativeRChannel.Dispose();
    }

    public float[] GetPixelValuesFromAreas(Rect sourceRect){
        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.y);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);

        Color[] pixels = sourceTex.GetPixels(x, y, width, height);
        float[] RChannelColor = new float[pixels.Length];
        for (int i = 0; i< pixels.Length; i++)
        {
            RChannelColor[i] = pixels[i].r;
        }
        return RChannelColor;
    }


    public struct TextureJob : IJob
    {
        public NativeArray<float> MyFloats;
        public float sum;
        public void Execute()
        {
            for (int i = 0; i < MyFloats.Length; i++) {
                sum += MyFloats[i];
            }
            Debug.Log(sum);
        }
    }
}
