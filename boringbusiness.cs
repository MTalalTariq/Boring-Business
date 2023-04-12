using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BoringBusiness
{
    class Program 
    {
        //Boring Business: Talal, Suryanshu, Disha
        
        static void Main(string[] args)
        {
            // create one big array 
            int[,] ground = new int[205, 401];
            for (int y = 0; y < 10; y++)
            { 
                for (int x = 0; x < 10; x++) 
                {
                    ground[y, x] = 0;
                }
            }

            // premade tunnel
            ground[0, 201] = 1;
            ground[1, 201] = 1;
            ground[2, 201] = 1;
            ground[2, 202] = 1;
            ground[2, 203] = 1;
            ground[2, 204] = 1;
            ground[3, 204] = 1;
            ground[4, 204] = 1;
            ground[4, 205] = 1;
            ground[4, 206] = 1;
            ground[3, 206] = 1;
            ground[2, 206] = 1;
            ground[2, 207] = 1;
            ground[2, 208] = 1;
            ground[3, 208] = 1;
            ground[4, 208] = 1;
            ground[5, 208] = 1;
            ground[6, 208] = 1;
            ground[6, 207] = 1;
            ground[6, 206] = 1;
            ground[6, 205] = 1;
            ground[6, 204] = 1;
            ground[6, 203] = 1;
            ground[6, 202] = 1;
            ground[6, 201] = 1;
            ground[6, 200] = 1;
            ground[5, 200] = 1;
            ground[4, 200] = 1; // drill start location



            // interpret the input file
            // these two lists hold the info from the input file
            // the orientation of digging and distance to dig
            List<char> orientation = new List<char>();
            List<int> distance = new List<int>();
            // using streamreader to access the file
            // your 4 input files are already in the project folder
            // change the number of the file from 1-4 to access them
            //"input1.txt" "input2.txt" "input3.txt" "input4.txt"
            using (StreamReader rawfile = new StreamReader("input1.txt"))
            {
                bool leave = false;
                while (leave == false)
                {
                    try
                    { 
                        // reads the file and cuts it at the point where there is a space
                        string[] input = rawfile.ReadLine().Split(' ');
                        // the orientation and distance is stored for later
                        char orien = Convert.ToChar(input[0]);                                              
                        orientation.Add(orien);
                        int dist = Convert.ToInt32(input[1]);
                        distance.Add(dist);  
                        if (orien == 'q')
                        {
                            leave = true; 
                        } 
                    }
                    catch
                    {
                        // if anything goes wrong, it will leave
                        leave = true;
                    }
                }
            }

            /*
            //outputs the list to ensure it reports the input correctly
            foreach(char d in orientation)
            {
                Console.Write(d + " ");
                
            }
            Console.WriteLine();
            foreach (int d in distance)
            {
                Console.Write(d + " ");
            }
            Console.WriteLine();
            */

            // position in ground
            int gpositionx = -1;
            int gpositiony = -5;
            // position in array index
            int arrpositionx = 200;
            int arrpositiony = 4;
            // danger or safe
            bool dang = false;

            // repeats for however many times the drill digs
            for (int i = 0; i < orientation.Count; i++)
            {
                // executes the certain variation of the code for the specific direction of drilling
                switch (orientation[i])
                {
                    case 'u':
                        //up
                        //updates and outputs new position on graph
                        gpositiony = gpositiony + distance[i];
                        Console.Write(gpositionx + " ");
                        Console.Write(gpositiony);

                        // checks for tunnels, if none, makes it a tunnel location
                        for (int y = arrpositiony - 1; y >= arrpositiony - distance[i]; y--)
                        {
                                if (ground[y, arrpositionx] == 1) 
                                {
                                    dang = true;
                                    
                                }
                                else if (ground[y, arrpositionx] == 0)
                                {
                                    ground[y, arrpositionx] = 1;
                                }
                        }
                        //updates array index
                        arrpositiony = arrpositiony - distance[i];                     
                        break;
                    case 'd':
                        //down
                        gpositiony = gpositiony - distance[i];
                        Console.Write(gpositionx + " ");
                        Console.Write(gpositiony);
                        
                        for (int y = arrpositiony + 1; y <= arrpositiony + distance[i]; y++)
                        {
                            if (ground[y, arrpositionx] == 1)
                            {
                               dang = true;

                            }
                            else if (ground[y, arrpositionx] == 0)
                            {
                                ground[y, arrpositionx] = 1;
                            }
                        }
                        arrpositiony = arrpositiony + distance[i];                     
                        break;
                    case 'r':
                        //right
                        gpositionx = gpositionx + distance[i];
                        Console.Write(gpositionx + " ");
                        Console.Write(gpositiony);

                        for (int x = arrpositionx + 1; x <= arrpositionx + distance[i]; x++)
                        {
                            if (ground[arrpositiony, x] == 1)
                            {
                                dang = true;

                            }
                            else if (ground[arrpositiony, x] == 0)
                            {
                                ground[arrpositiony, x] = 1;
                            }
                        }
                        arrpositionx = arrpositionx + distance[i];
                        break;
                    case 'l':
                        //left
                        gpositionx = gpositionx - distance[i];
                        Console.Write(gpositionx + " ");
                        Console.Write(gpositiony);

                        for (int x = arrpositionx - 1; x >= arrpositionx - distance[i]; x--)
                        {
                            if (ground[arrpositiony, x] == 1)
                            {
                                dang = true;

                            }
                            else if (ground[arrpositiony, x] == 0)
                            {
                                ground[arrpositiony, x] = 1;
                            }
                        }
                        arrpositionx = arrpositionx - distance[i];
                        break;
                    case 'q':
                        //quit
                        return;
                        
                    default:
                        //quits 

                        return;
                }
                // the code will stop when the danger is detected
                if (dang == true)
                {
                    Console.WriteLine(" DANGER");
                    return;
                }
                else if (dang == false)
                {
                    Console.WriteLine(" safe");
                }
            }

        }
    }
}
