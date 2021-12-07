using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
namespace Sorting_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a randomizer:
            Random randomizer = new Random();
            Stopwatch swInit = new Stopwatch();
            Stopwatch swBubble = new Stopwatch();
            Stopwatch swSelection = new Stopwatch();
            Stopwatch swInsert = new Stopwatch();
            Stopwatch swHeap = new Stopwatch();
            Stopwatch swQuick = new Stopwatch();


            using (StreamWriter writer = new(@"test.html"))
            {

                GenHTML.Head(writer, "Generated diagrams");
                GenHTML.Write(writer, "<h1>Generated diagram 2</h1>");
                GenHTML.SVGhead(writer);

                for (int i = 0; i < 10; i++)
                {
                    GenHTML.SVGrotTextAt(writer, $"{i * 10000}", 30 * i + 27, 363);
                }

                GenHTML.SVGrotTextAt(writer, "100000", 327, 361);
                GenHTML.Scale scale = new GenHTML.Scale { X = 0.003, Y = 3, xoff = 30, yoff = 40 };

                for (int intMax = 10000; intMax <= 100000; intMax += 10000)
                {
                    // Generate the array and measure the time:
                    Console.WriteLine($"\nStarting timer for initiating an array with length: {intMax}");

                    swInit.Start();
                    int[] testArray = new int[intMax];
                    FillRandomly(randomizer, testArray);
                    swInit.Stop();
                    Console.WriteLine($"  Elapsed time for populating the array: {swInit.Elapsed.TotalMilliseconds}");
                    Console.WriteLine($"  This array consist of {testArray.Length} random integer numbers.");
                    Console.WriteLine();
                    // Test the sorting algorithm:


                    swBubble.Start();
                    BubbleSort(testArray);
                    swBubble.Stop();
                    Console.WriteLine($" Bubble Sort --->  Elapsed time for sorting the array: {swBubble.Elapsed.TotalMilliseconds}");

                    GenHTML.PointAt(writer, scale, "red", intMax, swBubble.Elapsed.TotalSeconds);
                    if (!TestSortedAscending(testArray))
                    {
                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
                        break; // the for loop
                    }

                    // Insert new sorting algorithms here:
                    FillRandomly(randomizer, testArray);
                    swSelection.Start();
                    SelectionSort(testArray);
                    swSelection.Stop();
                    Console.WriteLine($" Selection Sort --->  Elapsed time for sorting the array: {swSelection.Elapsed.TotalMilliseconds}");
                    GenHTML.PointAt(writer, scale, "green", intMax, swSelection.Elapsed.TotalSeconds);
                    if (!TestSortedAscending(testArray))
                    {
                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
                        break; // the for loop
                    }

                    FillRandomly(randomizer, testArray);
                    swInsert.Start();
                    InsertionSort(testArray);
                    swInsert.Stop();
                    Console.WriteLine($" Insertion Sort --->  Elapsed time for sorting the array: {swInsert.Elapsed.TotalMilliseconds}");
                    GenHTML.PointAt(writer, scale, "blue", intMax, swInsert.Elapsed.TotalSeconds);
                    if (!TestSortedAscending(testArray))
                    {
                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
                        break; // the for loop
                    }

                    FillRandomly(randomizer, testArray);
                    swHeap.Start();
                    heapSort(testArray);
                    swHeap.Stop();
                    Console.WriteLine($" Heap Sort --->  Elapsed time for sorting the array: {swHeap.Elapsed.TotalMilliseconds}");
                    GenHTML.PointAt(writer, scale, "yellow", intMax, swHeap.Elapsed.TotalSeconds);
                    if (!TestSortedAscending(testArray))
                    {
                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
                        break; // the for loop
                    }

                    FillRandomly(randomizer, testArray);
                    swQuick.Start();
                    heapSort(testArray);
                    swQuick.Stop();
                    Console.WriteLine($" Quick Sort --->  Elapsed time for sorting the array: {swQuick.Elapsed.TotalMilliseconds}");
                    GenHTML.PointAt(writer, scale, "purple", intMax, swQuick.Elapsed.TotalSeconds);
                    // Test that the algorithm actually sorts correctly:

                    if (!TestSortedAscending(testArray))
                    {
                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
                        break; // the for loop
                    }
                    Console.WriteLine($"\n============================Time calculation for {intMax} numbers finished ==================================\n\n");
                    //Console.ReadKey();
                }


                GenHTML.SVGfoot(writer);
                //GenHTML.SVGhead(writer);
            }
            Console.WriteLine("\nPress any key to exit program...");
            Console.ReadKey();
        }

        // Populate the array with random integer values. Negative values are allowed:
        private static void FillRandomly(Random randomizer, int[] testArray)
        {
            for (int i = 0; i < testArray.Length; i++)
                testArray[i] = randomizer.Next();
        }

        // Bubble sort:
        public static void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] > arr[j]) Swap(ref arr[i], ref arr[j]);
                }
            }
        }

        // Selection Sort
        public static void SelectionSort(int[] arr)
        {
            int smallest;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                smallest = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[smallest])
                    {
                        smallest = j;
                    }
                }
                Swap(ref arr[smallest], ref arr[i]);
            }
        }

        // Insertion Sort
        public static void InsertionSort(int[] arr)
        {
            int i;
            int j;
            for (i = 1; i < arr.Length; i++)
            {
                for (j = i; j > 0; j--)
                {
                    if (arr[j] < arr[j - 1])
                    {
                        Swap(ref arr[j - 1], ref arr[j]);
                    }
                    else
                        break;
                }
            }
        }

        // Heap Sort
        static void heapSort(int[] arr)
        {
            for (int i = arr.Length / 2 - 1; i >= 0; i--)
                heapStruct(arr, arr.Length, i);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                Swap(ref arr[0], ref arr[i]);
                heapStruct(arr, i, 0);
            }
            // Just for test and print out the result
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    Console.Write($" {arr[i]}");
            //}
        }
        static void heapStruct(int[] arr, int len, int i)
        {
            int max = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            if (left < len && arr[left] > arr[max])
                max = left;
            if (right < len && arr[right] > arr[max])
                max = right;
            if (max != i)
            {
                Swap(ref arr[i], ref arr[max]);
                heapStruct(arr, len, max);
            }
        }



        // Quick Sort
        static public void QuickSort(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(arr, left, right);
                if (pivot > 1)
                    QuickSort(arr, left, pivot - 1);
                if (pivot + 1 < right)
                    QuickSort(arr, pivot + 1, right);
            }
        }
        static public int Partition(int[] numbers, int left, int right)
        {
            int pivot = numbers[left];
            while (true)
            {
                while (numbers[left] < pivot)
                    left++;
                while (numbers[right] > pivot)
                    right--;
                if (left < right)
                {
                    Swap(ref numbers[right], ref numbers[left]);
                    int temp = numbers[right];
                    numbers[right] = numbers[left];
                    numbers[left] = temp;
                }
                else
                {
                    return right;
                }
            }
        }
        private static void Swap(ref int v1, ref int v2)
        {
            int temp = v1; v1 = v2; v2 = temp;
        }

        public static bool TestSortedAscending(int[] prArray)
        {
            for (int i = 1; i < prArray.Length; i++)
            {
                if (prArray[i - 1] > prArray[i]) return false;
            }
            return true;
        }

    }
}





































//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Numerics;
//using System.Text;

//namespace Sorting_1
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Create a randomizer:
//            Random randomizer = new Random();
//            Stopwatch swInit = new Stopwatch();
//            Stopwatch swBubble = new Stopwatch();
//            Stopwatch swSelection = new Stopwatch();
//            Stopwatch swInsert = new Stopwatch();
//            Stopwatch swHeap = new Stopwatch();
//            Stopwatch swQuick = new Stopwatch();

//            using (StreamWriter writer = new(@"test.html"))
//            {
//                GenHTML.Head(writer, "Generated diagrams");
//                GenHTML.Write(writer, "<h1>Generated diagram 1</h1>");
//                GenHTML.SVGhead(writer);
//                int j = 0;
//                for (int i = 0; i < 6; i++)
//                {
//                    GenHTML.SVGtextAt(writer, $"{j}s", 19, 363 - 60 * i);
//                    j += 10;
//                }
//                for (int i = 0; i < 10; i++)
//                {
//                    GenHTML.SVGrotTextAt(writer, $"{i * 10000}", 30 * i + 27, 363);
//                }
//                GenHTML.SVGrotTextAt(writer, "100000", 327, 361);

//                GenHTML.Scale scale = new GenHTML.Scale { X = 0.003, Y = 6, xoff = 30, yoff = 40 };

//                for (int intMax = 10000; intMax <= 100000; intMax += 10000)  
//                {
//                    // Generate the array and measure the time:
//                    Console.WriteLine();
//                    int[] testArray = new int[intMax];

//                    FillRandomly(randomizer, testArray);

//                    Console.WriteLine($"  This array consist of {testArray.Length} random integer numbers.");

//                    // Test the sorting algorithm:
//                    swBubble.Start();
//                    BubbleSort(testArray);
//                    swBubble.Stop();
//                    Console.WriteLine($"  Elapsed time for BUBBLE SORT the array: {swBubble.Elapsed.TotalMilliseconds}");
//                    GenHTML.PointAt(writer, scale, "red", intMax, swBubble.Elapsed.TotalSeconds);
//                    // Test that the algorithm actually sorts correctly:
//                    if (!TestSortedAscending(testArray))
//                    {
//                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
//                        break; // the for loop
//                    }

//                    //Insert new sorting algorithms here:
//                    FillRandomly(randomizer, testArray);
//                    swSelection.Start();
//                    SelectionSort(testArray);
//                    swSelection.Stop();
//                    Console.WriteLine($"  Elapsed time for SELECTION SORT the array: {swSelection.Elapsed.TotalMilliseconds}");
//                    GenHTML.PointAt(writer, scale, "blue", intMax, swSelection.Elapsed.TotalSeconds);
//                    // Test that the algorithm actually sorts correctly:
//                    if (!TestSortedAscending(testArray))
//                    {
//                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
//                        break; // the for loop
//                    }

//                    FillRandomly(randomizer, testArray);
//                    swInsert.Start();
//                    InsertSort(testArray);
//                    swInsert.Stop();
//                    Console.WriteLine($"  Elapsed time for INSERT SORT the array: {swInsert.Elapsed.TotalMilliseconds}");
//                    GenHTML.PointAt(writer, scale, "green", intMax, swInsert.Elapsed.TotalSeconds);
//                    // Test that the algorithm actually sorts correctly:
//                    if (!TestSortedAscending(testArray))
//                    {
//                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
//                        break; // the for loop
//                    }

//                }
//                GenHTML.SVGfoot(writer);

//                GenHTML.SVGhead(writer);
//                //for (int i = 0; i < 6; i++)
//                //{
//                //    GenHTML.SVGtextAt(writer, $"{k}s", 19, 363 - 60 * i);
//                //    k += 0.5;
//                //}
//                //for (int i = 0; i < 10; i++)
//                //{
//                //    GenHTML.SVGrotTextAt(writer, $"{i * 10000}", 30 * i + 27, 363);
//                //}
//                //GenHTML.SVGrotTextAt(writer, "100000", 327, 361);

//                //GenHTML.Scale scale2 = new GenHTML.Scale { X = 0.003, Y = 120, xoff = 30, yoff = 40 };

//                GenHTML.Scale scale2 = new GenHTML.Scale { X = 0.003, Y = 0.6, xoff = 30, yoff = 40 };
//                for (int i = 0; i <= 500; i+=100)
//                {
//                    GenHTML.SVGtextAt(writer, $"{i}ms", 15, 363 - 0.6 * i);

//                }
//                for (int i = 0; i < 10; i++)
//                {
//                    GenHTML.SVGrotTextAt(writer, $"{i * 10000}", 30 * i + 27, 363);
//                }
//                GenHTML.SVGrotTextAt(writer, "100000", 327, 361);

//                for (int intMax = 10000; intMax <= 100000; intMax += 10000)
//                {
//                    // Generate the array and measure the time:
//                    Console.WriteLine();
//                    int[] testArray = new int[intMax];
//                    FillRandomly(randomizer, testArray);
//                    swHeap.Start();
//                    HeapSort(testArray);
//                    swHeap.Stop();
//                    Console.WriteLine($"  Elapsed time for HEAP SORT the array: {swHeap.Elapsed.TotalMilliseconds}");
//                    GenHTML.PointAt(writer, scale2, "purple", intMax, swHeap.Elapsed.TotalMilliseconds);

//                    // Test that the algorithm actually sorts correctly:
//                    if (!TestSortedAscending(testArray))
//                    {
//                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
//                        break; // the for loop
//                    }

//                    FillRandomly(randomizer, testArray);
//                    swQuick.Start();
//                    QuickSort(testArray, 0, testArray.Length - 1);
//                    swQuick.Stop();
//                    Console.WriteLine($"  Elapsed time for QUICK SORT the array: {swQuick.Elapsed.TotalMilliseconds}");
//                    GenHTML.PointAt(writer, scale2, "orange", intMax, swQuick.Elapsed.TotalMilliseconds);

//                    // Test that the algorithm actually sorts correctly:
//                    if (!TestSortedAscending(testArray))
//                    {
//                        Console.WriteLine("ERROR: The implementation actually doesn't sort the array!!");
//                        break; // the for loop
//                    }
//                }
//                GenHTML.Foot(writer);
//            }
//            Console.WriteLine("\nPress any key to exit program...");
//            Console.ReadKey();
//        }

//        // Populate the array with random integer values. Negative values are allowed:
//        private static void FillRandomly(Random randomizer, int[] testArray)
//        {
//            for (int i = 0; i < testArray.Length; i++)
//                testArray[i] = randomizer.Next();
//        }

//        // Bubble sort:
//        public static void BubbleSort(int[] arr)
//        {
//            for (int i = 0; i < arr.Length - 1; i++)
//            {
//                for (int j = i + 1; j < arr.Length; j++)
//                {
//                    if (arr[i] > arr[j]) Swap(ref arr[i], ref arr[j]);
//                }
//            }
//        }

//        /********************************/
//        /********************************/
//        /**** ADD ALGORITHMS BELOW!! ****/
//        /********************************/
//        /********************************/

//        // Selection Sort
//        public static void SelectionSort(int[] array)
//        {
//            int smallest;
//            int arrLength = array.Length;

//            for (int i = 0; i < arrLength - 1; i++)
//            {
//                smallest = i;
//                for (int j = i + 1; j < arrLength; j++)
//                {
//                    if (array[j] < array[smallest])
//                    {
//                        smallest = j;
//                    }
//                }
//                Swap(ref array[i], ref array[smallest]);
//            }
//        }
//        // Insertion Sort
//        public static void InsertSort(int[] array)
//        {
//            int arrLength = array.Length;
//            for (int i = 1; i <= arrLength - 1; i++)
//            {
//                int currentValue = array[i];
//                int j = i - 1;
//                while (j >= 0 && array[j] > currentValue)
//                {
//                    array[j + 1] = array[j];
//                    j--;
//                }
//                array[j + 1] = currentValue;
//            }

//        }
//        // Heap Sort
//        public static void HeapSort(int[] array)
//        {
//            var arrayLength = array.Length;
//            for (int i = arrayLength / 2 - 1; i >= 0; i--)
//            {
//                MaxHeap(array, arrayLength, i);
//            }
//            for (int i = arrayLength - 1; i >= 0; i--)
//            {
//                Swap(ref array[i], ref array[0]);
//                MaxHeap(array, i, 0);
//            }
//        }
//        static void MaxHeap(int[] array, int arrayLength, int i)
//        {
//            int largest = i;
//            int leftChild = 2 * i + 1;
//            int rightChild = 2 * i + 2;

//            if (leftChild < arrayLength && array[leftChild] > array[largest])
//            {
//                largest = leftChild;
//            }
//            if (rightChild < arrayLength && array[rightChild] > array[largest])
//            {
//                largest = rightChild;
//            }
//            if (largest != i)
//            {
//                Swap(ref array[i], ref array[largest]);
//                MaxHeap(array, arrayLength, largest);
//            }

//        }

//        // Quick Sort
//        // taget från: https://csharpskolan.se/article/quick-sort/
//        public static void QuickSort(int[] data, int left, int right)
//        {
//            //Välj det tal som avgör indelningen i "högre" och "lägre"
//            int pivot = data[(left + right) / 2];
//            //Välj det område som skall bearbetas
//            int leftHold = left;
//            int rightHold = right;

//            //Så länge vi har ett område kvar
//            while (leftHold < rightHold)
//            {
//                //Hitta ett tal på vänster sida som skall ligga i den "högre" delen
//                while ((data[leftHold] < pivot) && (leftHold <= rightHold)) leftHold++;
//                //Hitta ett tal på höger sida som skall ligga i den "lägre" delen
//                while ((data[rightHold] > pivot) && (rightHold >= leftHold)) rightHold--;

//                //Om vi nu har ett område kvar så skall talen på 
//                //vänster kant och höger kant byta plats
//                if (leftHold < rightHold)
//                {
//                    //Byta plats
//                    int tmp = data[leftHold];
//                    data[leftHold] = data[rightHold];
//                    data[rightHold] = tmp;
//                    //Minska området om vi flyttat två pivot-tal
//                    if (data[leftHold] == pivot && data[rightHold] == pivot)
//                        leftHold++;
//                }
//            }
//            //Nu när området är bearbetat så skall "lägre" delen bearbetas
//            //om sådan finns därefter detsamma med en eventuell "högre" del
//            if (left < leftHold - 1) QuickSort(data, left, leftHold - 1);
//            if (right > rightHold + 1) QuickSort(data, rightHold + 1, right);

//        }

//        private static void Swap(ref int v1, ref int v2)
//        {
//            int temp = v1; v1 = v2; v2 = temp;
//        }

//        public static bool TestSortedAscending(int[] prArray)
//        {
//            for (int i = 1; i < prArray.Length; i++)
//            {
//                if (prArray[i - 1] > prArray[i]) return false;
//            }
//            return true;
//        }

//    }
//}
