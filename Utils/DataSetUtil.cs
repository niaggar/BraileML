﻿using BraileML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BraileML.Utils;

public class DataSetUtil
{
    public static (DataPoint[] train, DataPoint[] validate) SplitData(DataPoint[] allData, float trainingSplit = 0.75f, bool shuffle = true)
    {
        if (shuffle)
        {
            ShuffleArray(allData, new System.Random());
        }

        int trainCount = (int)(allData.Length * trainingSplit);
        int validationCount = allData.Length - trainCount;

        DataPoint[] trainData = new DataPoint[trainCount];
        DataPoint[] validationData = new DataPoint[validationCount];

        for (int i = 0; i < trainData.Length; i++)
        {
            trainData[i] = allData[i];
        }
        for (int i = 0; i < validationData.Length; i++)
        {
            validationData[i] = allData[trainData.Length + i];
        }

        return (trainData, validationData);
    }

    public static Batch[] CreateMiniBatches(DataPoint[] allData, int size, bool shuffle = true)
    {
        if (shuffle)
        {
            ShuffleArray(allData, new System.Random());
        }

        int numBatches = allData.Length / size;
        Batch[] batches = new Batch[numBatches];
        for (int i = 0; i < batches.Length; i++)
        {
            DataPoint[] batchData = new DataPoint[size];
            System.Array.Copy(allData, i * size, batchData, 0, size);
            batches[i] = new Batch(batchData);
        }
        return batches;
    }

    public static void ShuffleBatches(Batch[] batches)
    {
        ShuffleArray(batches, new System.Random());
    }

    static void ShuffleArray<T>(T[] array, System.Random prng)
    {

        int elementsRemainingToShuffle = array.Length;
        int randomIndex = 0;

        while (elementsRemainingToShuffle > 1)
        {
            // Choose a random element from array
            randomIndex = prng.Next(0, elementsRemainingToShuffle);
            T chosenElement = array[randomIndex];

            // Swap the randomly chosen element with the last unshuffled element in the array
            elementsRemainingToShuffle--;
            array[randomIndex] = array[elementsRemainingToShuffle];
            array[elementsRemainingToShuffle] = chosenElement;
        }
    }

    static int Clamp01(float value)
    {
        return (int)Math.Clamp(value, 0, 1);
    }
}