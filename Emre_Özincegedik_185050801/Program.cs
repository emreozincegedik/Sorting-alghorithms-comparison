using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Emre_Özincegedik_185050801
{
    class Program
    {
        static int[] original_array;
        static int[] sort_result;

        const int length = 100000;

        public static void Main(string[] args)
        {
            int stackSize = 1024 * 1024 * 1024;
            Thread th = new Thread(() =>
            {
                doJobs();
            },
                stackSize);

            th.Start();
            th.Join();
        }

        private static void doJobs()
        {
            Random rnd = new Random();
            string original_seed = "seed"; //if someone wants to change the original_seed name

            Console.Write($"\nCreating {original_seed} array. ");

            int[] original_seed_list = new int[length]; //create int array with 100,000 index
            string[] copy_string_array = new string[length]; //create string array, because File.WriteAllLines uses string array.

            Console.Write($"Adding {length} random integers between 1 to 2.1 billion to {original_seed} array and storing to {original_seed}.txt file. ");

            for (int i = 0; i < original_seed_list.Length; i++)
            {
                original_seed_list[i] = rnd.Next(1, 2100000000);//add random item between 1 to 2.1 billion to original_seed_list[i]
                copy_string_array[i] = original_seed_list[i].ToString(); //copy the same number to string array for File.WriteAllLines function.
            }
            File.WriteAllLines($"{original_seed}.txt", copy_string_array); //create seed.txt and write all random numbers inside.

            original_array = original_seed_list.ToArray(); //copy original_seed_list so we can use it later without changing original_seed_list

            Console.WriteLine("Done.\n");

            Multiple_sorts("Heap_Sort");

            Multiple_sorts("Quick_Sort_1");

            Multiple_sorts("Quick_Sort_Algo_2");

            Multiple_sorts("Qucik_Sort_Median_3");

            Multiple_sorts("Insertion_Sort");

            Multiple_sorts("Merge_Sort");

            

            //Project 1

            //Sort(5, "Merge_Sort", original_seed_list, "m-seed", "merge");
            //Sort(5, "Insertion_Sort", original_seed_list, "i-seed", "insertion"); 

            Console.ReadLine();
        }

        static void Multiple_sorts(string which_sort)
        {
            Console.WriteLine("\nUsing the original array. Starting " + which_sort + "\n");

            Sort(1, which_sort, original_array, $"{which_sort}_temp_as_seed", $"{which_sort}_temp"); //Before sorting write array to (sort_name)_temp_as_seed, sort it with given sort algorithm, and write it to (sorts_name)_temp

            Console.WriteLine("\nUsing the result array from previous sort");

            Sort(1, which_sort, sort_result, "", $"{which_sort}_temp2"); //This time don't write to seed file, Sort result of previous sort, write it to (sort_name)_temp2

            Array.Reverse(sort_result); //reverse the result of the previous sort's array

            Console.WriteLine("\nResult array is reversed");

            Sort(1, which_sort, sort_result, $"{which_sort}_temp3", $"{which_sort}_temp4"); //Write seed as (sort_name)_temp3 (which is reversed array), sort it with same alghorithm, write result to (sort_name)_temp4

            Random rnd=new Random();
            int prev = sort_result[sort_result.Length - 1]; //store last element of result array for future use
            sort_result[sort_result.Length - 1] = rnd.Next(1, 2100000000); //change last element of array to random number

            Console.WriteLine($"\nChanged the last element of result array from {prev} to {sort_result[sort_result.Length - 1]}");

            Sort(1,which_sort, sort_result,$"{which_sort}_temp4", $"{which_sort}_array_seed_2.txt");

            Console.WriteLine("\n\n");


        }

        static void Sort(int repetition_time, string sortname, int[] original, string sortseed, string sortoutput)
        {
            int totaltimespent = 0; //use it for adding individual times for each sort

            for (int i = 0; i < repetition_time; i++) //How many times we want to run program for getting average time.
            {
                string[] copy_original = new string[length]; //Create string array for File.WriteAllLines use
                List<int> lst2 = original.OfType<int>().ToList(); //create another list so original stays same. (merge sort function works with lists. Thats why I changed to list)
                int[] lst = original.ToArray(); //lst=original causes pointer bug and changes the original at sorting
                if (sortseed != "") { //If no name is given for seed file, it won't be stored
                    for (int j = 0; j < lst2.Count; j++)
                        copy_original[j] = lst[j].ToString(); //copy int array to string array for File.WriteAllLines use
                    if (repetition_time>1) //Seed file's name becomes different depending on the repetition time
                    {
                        Console.Write($"Storing {i + 1}. {sortname}'s temporay array in {sortseed}-{i + 1}.txt.  ");
                        File.WriteAllLines($"{sortseed}-{i + 1}.txt", copy_original); //Store seed file with number depending on which repetition time the loop is in
                    }
                    else
                    {
                        Console.Write($"Storing {sortname}'s temporary array in {sortseed}.txt.  ");
                        File.WriteAllLines($"{sortseed}.txt", copy_original); //Store seed file
                    }
                    Console.WriteLine("Done.");
                }

                if (repetition_time>1)
                    Console.Write($"Sorting {i + 1}. time. ");
                else
                    Console.Write($"Sorting. ");

                Stopwatch stp = new Stopwatch(); //Get the stopwatch feature so we can use timer.
                if (sortname == "Insertion_Sort")
                {
                    stp.Start(); //Start timer
                    lst=InsertionSort(lst); //call insertionsort function
                }
                else if (sortname == "Merge_Sort")
                {
                    stp.Start(); //Start timer
                    lst2 =MergeSort(lst2); //call merge sort function
                }
                else if (sortname== "Quick_Sort_1")
                {
                    stp.Start();//Start timer
                    Quick_Sort.Quick_Sort_Algo_1(lst, 0, lst.Length - 1); //call quick_sort_algo_1
                }
                else if (sortname == "Quick_Sort_Algo_2")
                {
                    stp.Start();//Start timer
                    Quick_Sort.Quick_Sort_Algo_2(lst, 0, lst.Length - 1); //call Quick_Sort_Algo_2
                }
                else if (sortname == "Qucik_Sort_Median_3")
                {
                    stp.Start();//Start timer
                    Quick_Sort.Qucik_Sort_Median_3(lst, 0, lst.Length - 1);//call Qucik_Sort_Median_3
                }
                else if (sortname=="Heap_Sort")
                {
                    stp.Start(); //Start timer
                    Heap_Sort(lst); //Call Heap_sort
                }
                else
                {
                    Console.WriteLine("Invalid sort algorithm. Terminating this sort.");
                    return;
                }
                stp.Stop(); //Stop timer
                
                if (sortname=="Merge_Sort")
                    lst = lst2.ToArray(); //Merge sort uses list, others use int[] array. So lst and lst2 are different after sorts. This makes them same.
                sort_result = lst.ToArray(); //Save the result for future use.

                Console.Write("Time Elapsed: "+stp.ElapsedMilliseconds.ToString() + " miliseconds. ");
                totaltimespent +=(int) stp.ElapsedMilliseconds; //add elapsed time for calculating average
                stp.Reset(); //Reset timer so we can start timer from 0 next time.

                if (sortoutput != "") { //if no name is given for output file, it won't be stored.
                    for (int j = 0; j < lst2.Count; j++) //copy int array to string array so we can store it using File.WriteAllLines.
                        copy_original[j] = lst[j].ToString();
                    if(repetition_time>1) //if repetition time is more than one, the name of output shows which number we are in.
                    {
                        Console.Write($"Storing result array in {sortoutput}-{i + 1}.txt.  ");
                        File.WriteAllLines($"{sortoutput}-{i + 1}.txt", copy_original); //Store seed file with number depending on which repetition time the loop is in
                    }
                    else
                    {
                        Console.Write($"Storing result array in {sortoutput}.txt.  ");
                        File.WriteAllLines($"{sortoutput}.txt", copy_original);//Store seed file
                    }
                    Console.WriteLine("Done.");
                }
            }
            if (repetition_time>1)
                Console.WriteLine($"\nAverage miliseconds for {sortname} after {repetition_time} times is: " + (totaltimespent / repetition_time) + " miliseconds\n");
        }

        public static List<int> MergeSort(List<int> unsorted) //Copied from internet
        {
            if (unsorted.Count <= 1)
                return unsorted;

            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int middle = unsorted.Count / 2;
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
            {
                left.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Count; i++)
            {
                right.Add(unsorted[i]);
            }

            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }

        public static List<int> Merge(List<int> left, List<int> right) //Copied from internet
        {
            List<int> result = new List<int>();

            while (left.Count > 0 || right.Count > 0)
            {
                if (left.Count > 0 && right.Count > 0)
                {
                    if (left.First() <= right.First())  //Comparing First two elements to see which is smaller
                    {
                        result.Add(left.First());
                        left.Remove(left.First());      //Rest of the list minus the first element
                    }
                    else
                    {
                        result.Add(right.First());
                        right.Remove(right.First());
                    }
                }
                else if (left.Count > 0)
                {
                    result.Add(left.First());
                    left.Remove(left.First());
                }
                else if (right.Count > 0)
                {
                    result.Add(right.First());

                    right.Remove(right.First());
                }
            }
            return result;
        }
        
         public static int[] InsertionSort(int[] intArray) //Copied from internet
        {
        
            int temp, j;

            for (int i = 1; i < intArray.Length; i++)
            {
                temp = intArray[i];
                j = i - 1;
                while (j >= 0 && intArray[j] > temp)
                {
                    intArray[j + 1] = intArray[j];

                    j--;
                }
                intArray[j + 1] = temp;
            }
            return intArray;
        }


        static void Heap_Sort(int[] data)
        {
            int heapSize = data.Length;
            for (int p = (heapSize - 1) / 2; p >= 0; p--)
            {
                MakeHeap(data, heapSize, p);
            }

            for (int i = data.Length - 1; i > 0; i--)
            {
                int temp = data[i];
                data[i] = data[0];
                data[0] = temp;

                heapSize--;
                MakeHeap(data, heapSize, 0);
            }
        }
        static void MakeHeap(int[] input, int heapSize, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest = 0;

            // finds the index of the largest
            if (left < heapSize && input[left] > input[index])
            {
                largest = left;
            }
            else
            {
                largest = index;
            }
            if (right < heapSize && input[right] > input[largest])
            {
                largest = right;
            }
            if (largest != index)
            {
                // process of reheaping / swapping
                int temp = input[index];
                input[index] = input[largest];
                input[largest] = temp;

                MakeHeap(input, heapSize, largest);
            }
        }
        


    }
}
