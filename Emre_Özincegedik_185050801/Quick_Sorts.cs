using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emre_Özincegedik_185050801
{
    class Quick_Sort
    {
      


        public static void Quick_Sort_Algo_1(int[] A, int left, int right)
        {
            if (left > right || left < 0 || right < 0) return;

            int index = partition(A, left, right);

            if (index != -1)
            {
                Quick_Sort_Algo_1(A, left, index - 1);
                Quick_Sort_Algo_1(A, index + 1, right);
            }
        }

        public static int partition(int[] A, int left, int right)
        {
            if (left > right) return -1;

            int end = left;

            int pivot = A[right];    // choose last one to pivot, easy to code
            for (int i = left; i < right; i++)
            {
          
                if (A[i] < pivot)
                {
                    swap(A, i, end);
                    end++;
                }
            }

            swap(A, end, right);

            return end;
        }

        public static void swap(int[] A, int left, int right)
        {
          
            int tmp = A[left];
            A[left] = A[right];
            A[right] = tmp;
        }


        public static void Quick_Sort_Algo_2(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition2(arr, left, right);

                if (pivot > 1)
                {
                    Quick_Sort_Algo_2(arr, left, pivot - 1);
                }
                if (pivot + 1 < right)
                {
                    Quick_Sort_Algo_2(arr, pivot + 1, right);
                }
            }

        }

         static int Partition2(int[] arr, int left, int right)
        {
            int pivot = arr[left];
            while (true)
            {

                while (arr[left] < pivot)
                {
                    left++;
                }

                while (arr[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left] == arr[right]) return right;
                    
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;


                }
                else
                {
                    return right;
                }
            }
        }


        public static void Qucik_Sort_Median_3(int[] intArray, int left, int right)
        {
            int size = right - left + 1;
            if (size <= 3)
                manualSort(intArray, left, right);
            else
            {
                double median = medianOf3(intArray, left, right);
                int partition = partitionIt(intArray, left, right, median);
                Qucik_Sort_Median_3(intArray, left, partition - 1);
                Qucik_Sort_Median_3(intArray, partition + 1, right);
            }
        }

        public static int medianOf3(int[] intArray, int left, int right)
        {
            int center = (left + right) / 2;

            if (intArray[left] > intArray[center])
                swap(intArray, left, center);

            if (intArray[left] > intArray[right])
                swap(intArray, left, right);

            if (intArray[center] > intArray[right])
                swap(intArray, center, right);

            swap(intArray, center, right - 1);
            return intArray[right - 1];
        }

        public static int partitionIt(int[] intArray, int left, int right, double pivot)
        {
            int leftPtr = left;
            int rightPtr = right - 1;

            while (true)
            {
                while (intArray[++leftPtr] < pivot)
                    ;
                while (intArray[--rightPtr] > pivot)
                    ;
                if (leftPtr >= rightPtr)
                    break;
                else
                    swap(intArray, leftPtr, rightPtr);
            }
            
            swap(intArray, leftPtr, right - 1);
            return leftPtr;
        }

        public static void manualSort(int[] intArray, int left, int right)
        {
            int size = right - left + 1;
            if (size <= 1)
                return;
            if (size == 2)
            {
                if (intArray[left] > intArray[right])
                    swap(intArray, left, right);
                return;
            }
            else
            {
                if (intArray[left] > intArray[right - 1])
                    swap(intArray, left, right - 1);
                if (intArray[left] > intArray[right])
                    swap(intArray, left, right);
                if (intArray[right - 1] > intArray[right])
                    swap(intArray, right - 1, right);
            }
        }
    }
}
