using System; 
using System.IO; 
 
namespace lab1_mpp 
{ 
    class Program 
    { 
        static void Main(string[] args) 
        { 
            String inputString = File.ReadAllText("sample.txt"); 
            char[] input = new char[inputString.Length]; 
            int copyIndex = 0; 
 
            start7: 
            input[copyIndex] = inputString[copyIndex]; 
            copyIndex++; 
 
            if (copyIndex < inputString.Length) goto start7; 
            else goto end7; 
            end7: 
 
            const int mapSize = 10000; 
            Node[] hashMap = new Node[mapSize]; 
            int index = 0; 
            int wordStart = -1; 
            String bufString; 
            int difWordsCount = 0; 
            bool isReading = false; 
 
            start: 
 
            if ((input[index] >= 65 && input[index] <= 90) || (input[index] >= 97 && input[index] <= 122)) 
            { 
                if (input[index] >= 65 && input[index] <= 90) input[index] = (char)(input[index] + 32); 
                if (!isReading) 
                { 
                    isReading = true; 
                    wordStart = index; 
                } 
                if (index == input.Length - 1 && isReading) 
                { 
                    bufString = new String(input, wordStart, index - wordStart + 1); 
                    goto addString; 
 
                } 
            } else 
            { 
                if (isReading) 
                { 
                    isReading = false; 
                    bufString = new String(input, wordStart, index - wordStart); 
                    goto addString; 
                } 
            } 
            returnFlag: 
 
            index++; 
            if (index >= input.Length) goto end; 
            else goto start; 
            end: 
 
            goto escape; 
 
            //add string to map 
            addString: 
            if (bufString.Length <= 3) goto end1; 
            int hashCode0 = 0; 
            int multiplier0 = 31; 
            int index0 = 0; 
            start0: 
            hashCode0 += bufString[index0] * multiplier0; 
            multiplier0 *= 31; 
            index0++; 
            if (index0 >= bufString.Length) goto end0; 
            else goto start0; 
            end0: 
            if (hashCode0 < 0) hashCode0 = -hashCode0; 
 
            Node node = hashMap[hashCode0 % mapSize]; 
 
            if (node == null) 
            { 
                hashMap[hashCode0 % mapSize] = new Node(bufString, 1); 
                difWordsCount++; 
                goto end1; 
            } else 
            { 
                start1: 
                Node newNode; 
                if (bufString == node.value) 
                { 
                    node.count++; 
                    goto end1; 
                } 
                newNode = node.nextNode; 
                if (newNode == null) 
                { 
                    node.nextNode = new Node(bufString, 1); 
                    difWordsCount++; 
                    goto end1; 
                } else 
                { 
                    node = newNode; 
                    goto start1; 
                } 
            } 
            end1: 
 
            goto returnFlag; 
 
            escape: 
 
            Node[] wordsToCount = new Node[difWordsCount]; 
            Node searchNode = null; 
            index = 0; 
            int hashmapIndex = 0; 
            start2: 
 
            if (searchNode == null) 
            { 
                searchNode = hashMap[hashmapIndex]; 
                hashmapIndex++; 
            } else 
            { 
                wordsToCount[index] = searchNode; 
                index++; 
                searchNode = searchNode.nextNode; 
            } 
 
            if (index < difWordsCount) goto start2; 
            else goto end2; 
 
            end2: 
 
            int index4 = 0; 
            int index5 = 0; 
            Node bufNode; 
 
            start4: 
            start5: 
 
            if (wordsToCount[index5].count > wordsToCount[index5 + 1].count)
{ 
                bufNode = wordsToCount[index5 + 1]; 
                wordsToCount[index5 + 1] = wordsToCount[index5]; 
                wordsToCount[index5] = bufNode; 
            } 
 
            index5++; 
            if (index5 < wordsToCount.Length - 1) goto start5; 
            else goto end5; 
 
            end5: 
            index4++; 
            index5 = index4; 
            if (index4 < wordsToCount.Length - 1) goto start4; 
            else goto end4; 
 
            end4: 
 
            Node[] result = new Node[wordsToCount.Length]; 
            int index6 = 0; 
            start6: 
 
            result[index6] = wordsToCount[result.Length - index6 - 1]; 
 
            index6++; 
            if (index6 < wordsToCount.Length) goto start6; 
            else goto end6; 
 
            end6: 
 
            int printIndex = 0; 
            start3: 
            Console.WriteLine(result[printIndex].value + " - " + result[printIndex].count); 
            printIndex++; 
            if (printIndex == 25) goto end3; 
            if (printIndex < result.Length) goto start3; 
            else goto end3; 
 
            end3: 
            Console.ReadKey(); 
        } 
    } 
 
    class Node 
    { 
        public Node(String value, int count) 
        { 
            this.value = value; 
            this.count = count; 
        } 
 
        public String value; 
        public int count; 
        public Node nextNode; 
    } 
}