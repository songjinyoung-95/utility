using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util.RandomUtil
{
    public static class RandomUtils
    {
        /// <summary>
        /// 들어온 값 중 가중치에 따라 랜덤으로 반환
        /// </summary>
        public static float GetWeightedRandom(float[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
                total += elem;

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }

            return probs.Length - 1;
        }

        /// <summary>
        /// 들어온 값 중 가중치에 따라 랜덤으로 반환
        /// </summary>
        public static int GetWeightedRandom(int[] probs)
        {
            float total = 0;

            foreach (float elem in probs)
                total += elem;

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }

            return probs.Length - 1;
        }

        /// <summary>
        /// 최소값과 최대값에서 랜덤한 값을 하나 출력
        /// </summary>
        public static int GetRandomValue(int minValue, int maxValue)
        {
            return Random.Range(minValue, maxValue + 1);
        }

        /// <summary>
        /// 최소값과 최대값에서 랜덤한 값을 하나 출력
        /// </summary>
        public static float GetRandomValue(float minValue, float maxValue)
        {
            return Random.Range(minValue, maxValue + 1);
        }

        /// <summary>
        /// 최소값과 최대값에서 랜덤한 값을 원하는 크기 만큼 출력
        /// </summary>
        public static int[] GetRandomValues(int returnCount, int minValue, int maxValue, bool enableDuplicates)
        {
            int[] randomValues = new int[returnCount];

            if (enableDuplicates)
            {
                for (int i = 0; i < returnCount; i++)
                    randomValues[i] = Random.Range(minValue, maxValue + 1);

                return randomValues;
            }
            else
            {
                HashSet<int> non_duplicateValues = new HashSet<int>();

                while(non_duplicateValues.Count < returnCount)
                {
                    non_duplicateValues.Add(Random.Range(minValue, maxValue + 1));
                }

                return non_duplicateValues.ToArray();
            }

        }

        public static void RandomShuffle<T>(T[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);

                T temp = array[i];
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }
    }
}