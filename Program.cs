
//DIZAD 11/25/16-START
//DIZAD 12/19/16-END
//System namespaces

using System; // Default: Used for Arrays, environments, random, datetime
using System.Collections.Generic; //Default
using System.Linq; //Default

using System.Speech.Synthesis; //Added for text-to-speech
using System.Threading; //Added for multi-tasking 
using System.Media;//Added for system sounds
using System.Diagnostics; //Added for pulling system diagnostic classes
using System.Windows.Forms; //Added for mouse and keyboard manipulation
using STYLE_LIBRARY; //Custom library created for the styles
//this program is also referencing the camera.cs class

namespace CONSOLE_APPLICATION
{
    class Program
    {
        //Global objects and variables
        #region
            public static Random randomNumber = new Random();
            private static SpeechSynthesizer synth = new SpeechSynthesizer();
            static DateTime time = DateTime.Now; //Used for time variables
            public static string programEntry = "0"; //This is a universal variable used to select console options
            public static int programNoInMem = 0; //This global variable is used to remember the app to restart
        #endregion

        //Main method        
            static void Main(string[] args)
            {
            //Threads               
            Thread mainMenuObject = new Thread(new ThreadStart(mainMenu)); //Main menu thread
                mainMenuObject.Start();
            }

        //Instrumental functions
            //Entry control        
            static void controlEntry(int intMin, int intMax)
                {
                bool entryPass = false;
                        while (entryPass == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            entryPrompt();
                            programEntry = Console.ReadLine();

                            if (programEntry.Length == 0)
                            {
                                style.GrayLine("ERROR! The entry is "); style.WhiteLine("EMPTY");
                                Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine(", Enter #s between [{0}] to [{1}] ONLY: ", intMin, intMax);
                                newLine();
                            }
                            else
                            {
                                try
                                {
                                    int programEntryInt = Convert.ToInt32(programEntry);
                                    if (programEntryInt > intMax || programEntryInt < intMin)
                                    {

                                        style.GrayLine("ERROR! The entry is outside of "); style.WhiteLine("RANGE");
                                        Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine(", Enter #s between [{0}] to [{1}] ONLY: ", intMin, intMax);
                                        newLine();
                                        entryPass = false;
                                    }
                                    else { entryPass = true; }
                                }
                                catch (FormatException)
                                {

                                    style.GrayLine("ERROR! The entry has "); style.WhiteLine("STRINGS");
                                    Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine(", Enter #s between [{0}] to [{1}] ONLY: ", intMin, intMax);
                                    newLine();
                                }
                                catch (Exception)
                                {
                                    style.Gray("ERROR!");
                                }
                            }
                        }
                    }
            static void controlLengthOnly(int stringMin, int stringMax)
                {
                    bool entryPass = false;
                    while (entryPass == false)
                        {
                            entryPrompt();
                            programEntry = Console.ReadLine();
                            if (programEntry.Length < stringMin || programEntry.Length > stringMax)
                            { style.GrayLine("ERROR! The entry is outside of "); style.WhiteLine("RANGE");
                        if (stringMin == stringMax) { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine(", Enter [{0}] characters ONLY: ", stringMin); }
                        else { Console.ForegroundColor = ConsoleColor.DarkGray; Console.WriteLine(", Enter [{0}] to [{1}] characters ONLY: ", stringMin, stringMax); }
                    newLine(); }
                        else { entryPass = true; }
                        }                    
                }

            //consoleControl        
            public static void consoleControl()         
                    {
                        style.RedLine("[1]"); style.Yellow(" Main Menu");
                        style.RedLine("[2]"); style.Yellow(" Restart");
                        style.RedLine("[3]"); style.Yellow(" Exit");

                        Console.ForegroundColor = ConsoleColor.Red;
                        newLine();
                        controlEntry(1,3); 
                        switch (programEntry)
                            {
                                case "1": Console.Clear(); mainMenu(); break;
                                case "2":
                                    switch (programNoInMem)
                                        {
                                            case 1: PROGRAM1(); break;
                                            case 2: PROGRAM2(); break;
                                            case 3: PROGRAM3(); break;
                                            case 4: PROGRAM4(); break;
                                            case 5: PROGRAM5(); break;
                                            default: mainMenu(); break;
                                        } break;
                                case "3": Console.ForegroundColor = ConsoleColor.White; Environment.Exit(0); break;
                                default: break;
                            }
                        }
                   
            //Speak function       
            static void speak(string Message)
            {
                synth.Speak(Message);
                    //synth.Rate = rate;
                }

            //Prompt functions

            static void entryPrompt()
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("ENTRY: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                }
            static void responsePrompt()
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write("RESPONSE: ");
                }

            //newLine
                static void newLine()
                { Console.WriteLine(""); }       
            //Program initation
                static void selectProgram()
                            {
                                controlEntry(1,6);          
                                switch (programEntry)
                                    {
                                        case "1": programNoInMem = 1; PROGRAM1();  break;
                                        case "2": programNoInMem = 2; PROGRAM2();  break;
                                        case "3": programNoInMem = 3; PROGRAM3();  break;
                                        case "4": programNoInMem = 4; PROGRAM4();  break;
                                        case "5": programNoInMem = 5; PROGRAM5();  break;
                                        case "6": programNoInMem = 6; consoleControl(); break;
                                        default: style.Red("enter numbers 1 to 6 only"); mainMenu(); break;
                                    }
                            }

            //Auxiliary functions       
            public static void OpenProgram(string programName)
        {
            try
            {
                Process OpenProgram = new Process();
                OpenProgram.StartInfo.FileName = programName;
                if (programName == "chrome.exe")
                { OpenProgram.StartInfo.Arguments = "https://en.wikipedia.org/wiki/Dennis_Ritchie"; }
                OpenProgram.Start();
            }
            catch (Exception) { style.Gray("You must not have chrome installed..."); }
        }

        //Main Menu        
        public static void mainMenu()    
        {
                Console.Clear();
                style.Green("CONSOLE APPLICATION (DIZAD 12/10/16)"); 
                style.Green("Current Time:" + time.ToString());newLine();  
                style.Yellow("Make sure your sound is ON and choose an option:"); newLine();
                speak("Please choose an option");

                style.RedLine("   [1]"); style.printText(" Reverse Characters:", ConsoleColor.Cyan, false);
                style.WhiteLine("    Concepts:"); style.Gray("  Variables, Enumerations, Foreach statements"); newLine();

                style.RedLine("   [2]"); style.printText(" Guessing Game:", ConsoleColor.Cyan, false);
                style.WhiteLine("    Concepts:"); style.Gray("  While loops, Randomizing objects, If statements"); newLine();

                style.RedLine("   [3]"); style.printText(" Magic 8 Ball:", ConsoleColor.Cyan, false);
                style.WhiteLine("    Concepts:"); style.Gray("  Switch statements, Timer objects, Text-to-speech"); newLine();

                style.RedLine("   [4]"); style.printText(" System Control", ConsoleColor.Cyan, false);
                style.WhiteLine("    Concepts:"); style.Gray("  System libraries/references, ASCII, Threads, Arrays"); newLine();

                style.RedLine("   [5]"); style.printText(" Object List Manipulation:", ConsoleColor.Cyan, false);
                style.WhiteLine("    Concepts:"); style.Gray("  Collections, LINQ, Custom libraries, Handling exceptions "); newLine();

                style.RedLine("   [6]"); style.Green(" Console Control");
                newLine();                           
                selectProgram();
            }

        //Project functions
            //Program#1-Reverse Characters        
            static void PROGRAM1()
                    {
                        Console.Clear();
                        style.Green("PROGRAM#1 - Reverse Characters:");
                        newLine();
                        style.Yellow("Type a phrase or a numbered sequence that you would like reversed:");
                        newLine();
                        speak("Type a phrase or a numbered sequence that you would like reversed");

                        Console.ForegroundColor = ConsoleColor.Red;
                        controlLengthOnly(1,150);
            
                        //string entry = Console.ReadLine();                   
                        char[] reverseArray = programEntry.ToCharArray();
                        Array.Reverse(reverseArray);
                        responsePrompt();
                        Console.ForegroundColor = ConsoleColor.White;
                        foreach (var reverseArray1 in reverseArray)
                            {                       
                                Console.Write(Convert.ToString(reverseArray1));
                            }
                            newLine();
                            newLine(); Thread.Sleep(1000); consoleControl();
                    }

            //Program#2-Guessing Game 
            static void PROGRAM2()
            {
                Console.Clear();
                style.Green("PROGRAM#2 - GUESSING GAME:");
                newLine();
                style.Yellow("Guess a number between 1 and 5:");
                newLine();
                speak("Guess a number between 1 and 5");                

                Random compNumber = new Random();
                int compNumber1 = compNumber.Next(1,5);
                int guesses = 0;
                bool fail = true;
                do
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        controlEntry(1,5);
                        guesses++;
                    if (programEntry == compNumber1.ToString())
                        { fail = false; }
                    else
                        {
                            responsePrompt();
                            style.White("Fail! Try again!");
                            newLine();
                        }
                    } while (fail); //Continue cycle if fail is still true otherwise carry on

                    responsePrompt();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("congratulations! You guessed correctly after {0} guess(es)!", guesses);
                    speak("congratulations, you guessed correctly.");
                    newLine(); Thread.Sleep(1000); consoleControl();
            }

            //Program#3-Magic 8 Ball        
            static void PROGRAM3()
            {
                Console.Clear();
                style.Green("PROGRAM#3 - MAGIC 8 BALL:");
                newLine();
                style.Yellow("Ask a YES or NO question about anything:");
                newLine();
                speak("Ask a YES or NO question about anything");
                    
                while(true)
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    controlLengthOnly(1,150);
                    //string response = Console.ReadLine();
                        
                    int randomNumber1 = randomNumber.Next(5);

                    if (programEntry.Length == 0)
                        {
                            style.Red("You did not enter a question, please type again.");
                            continue;
                        }
                    if (randomNumber.Next(2) < 1){ style.Gray("Processing a complex algorithm..."); } else { style.Gray("Reviewing the orbs..."); };
                    Random randomSeconds = new Random();
                    int randomSeconds1 = randomSeconds.Next(5);
                    Thread.Sleep(randomSeconds1*800 + 500); //add lag time on response, adjust multiplier for duration max
                    switch(randomNumber1)
                        {
                            case 0: responsePrompt(); style.White("Yes!"); speak("The answer is Yes"); break;
                            case 1: responsePrompt(); style.White("No!"); speak("The answer is No"); break;
                            case 2: responsePrompt(); style.White("Absolutely Yes!"); speak("The answer is Absolutely Yes!"); break;
                            case 3: responsePrompt(); style.White("Absolutely Not!"); speak("The answer is Absolutely Not!"); break;
                            case 4: responsePrompt(); style.White("Hummm, I'm not sure about this one."); speak("I'm not sure about this one."); break;
                        }        
                    newLine(); Thread.Sleep(1000); consoleControl();
                        }
            }

            //Program#4-System Control
            static void PROGRAM4()
                {
                    Console.Clear();
                    style.Green("PROGRAM#4 - SYSTEM CONTROL");
                    newLine();
                    style.Yellow("Please choose an option:");
                    speak("Please choose an option.");
                    newLine();
                    style.RedLine("   [1]"); style.Cyan(" Mouse Control"); newLine();//Changes the motion of the mouse
                    style.RedLine("   [2]"); style.Cyan(" Keyboard Control"); newLine();//Types a random message
                    style.RedLine("   [3]"); style.Cyan(" System Sound Control"); newLine();//Plays the system sounds
                    style.RedLine("   [4]"); style.Cyan(" Message Box Control"); newLine();//Pops up random microsoft windows
                    style.RedLine("   [5]"); style.Cyan(" Application Control"); newLine();//Pops up random microsoft windows
                    style.RedLine("   [6]"); style.Cyan(" Show current CPU/MEM Usage"); newLine();//Pops up random microsoft windows
                       
                    Console.ForegroundColor = ConsoleColor.Red;
                    controlEntry(1,6);
                    // int responseProgram4 = Convert.ToInt32(Console.ReadLine());
                    switch (programEntry)
                        {
                            case "1": mouseControl(); break;
                            case "2": keyboardControl(); break;
                            case "3": systemSound(); break;
                            case "4": messageControl(); break;
                            case "5": applicationControl(); break;
                            case "6": systemMonitor();  break;
                            default: speak("enter numbers 1 to 6 only"); break;
                        }
                    newLine(); consoleControl();
                }

                //Option#1-Mouse Control
                public static void mouseControl()
                {
                    newLine();
                    style.Yellow("Please ENTER the level of inconvenience (1=Low, 2=Intermediate, 3=High):");
                    speak("Please ENTER the level of inconvenience between one and three:");
                    controlEntry(1,3);

                    int inconvenience = Convert.ToInt32(programEntry);
                    if (inconvenience < 1 || inconvenience > 3)
                    { speak("Please enter one, two, or three only");
                    Console.ReadLine();}
                    Console.ForegroundColor = ConsoleColor.White;                          
                        int increment = 0;
                        int incrementInconvenience = 0;

                    while (incrementInconvenience < 2)
                        {
                            while (increment < (5 * Convert.ToInt32(Math.Pow(inconvenience, 3))))
                                {
                                    int randMoveX = randomNumber.Next(-50 * Convert.ToInt32(Math.Pow(inconvenience, 3)), 50 * Convert.ToInt32(Math.Pow(inconvenience, 3)));
                                    int randMoveY = randomNumber.Next(-50 * Convert.ToInt32(Math.Pow(inconvenience, 3)), 50 * Convert.ToInt32(Math.Pow(inconvenience, 3)));
                                    Console.WriteLine(Cursor.Position.ToString()); //display the mouse coordinates
                                    Cursor.Position = new System.Drawing.Point(Cursor.Position.X + randMoveX, Cursor.Position.Y + randMoveY);
                                    Thread.Sleep(1000 / Convert.ToInt32(Math.Pow(inconvenience, 3)));
                                    increment++;
                                }
                            if (inconvenience == 3 && incrementInconvenience < 1)
                                {
                                    speak("Let's go one more time since you asked for high inconvenience.");
                                    incrementInconvenience++;
                                    increment = 0;
                                }
                            else { incrementInconvenience += 2; };
                        }
                        newLine(); Thread.Sleep(1000); consoleControl();
                }

                //Option#2-Keyboard Control
                public static void keyboardControl()
                {
                    style.Yellow("Keys will be entered on the console:");
                    speak("Keys will be entered on the console.");
                    newLine();
                    Thread.Sleep(500);
                    //Type random text sporadically
                        style.CyanLine("[1]  ");
                        int increment = 0;
                        while (increment < 28)
                            {
                                char key = (char)0;
                                if (randomNumber.Next(100) < 80)
                                {  key = (char)(randomNumber.Next(25) + 97); }
                                else { key = (char)32; } //Put a random space after 20% of the keys
                                style.WhiteLine(Convert.ToString(key));
                                Thread.Sleep(randomNumber.Next(200));
                                increment++;
                            }
                        newLine();
                        Thread.Sleep(500);                          

                    //Type a real sentence sporadically
                        style.CyanLine("[2]  ");
                        int[] messageArray = new int[] 
                        { 072, 101, 108, 108, 111, 032, 097, 110, 100, 032, 104, 101, 114, 101, 032,
                            105, 115, 032, 097, 032, 109, 101, 115, 115, 097, 103, 101, 033 }; //An alternative could have been using .ToArray

                        for (int arrayIndex = 0; arrayIndex < messageArray.Length; arrayIndex++)
                            {
                                char key = (char)messageArray[arrayIndex];
                                style.WhiteLine(Convert.ToString(key));
                                // SendKeys.SendWait(key.ToString());
                                Thread.Sleep(randomNumber.Next(200));
                            }
                        newLine();
                        newLine(); Thread.Sleep(2000); consoleControl();
                }

                //Option#3-Sound
                public static void systemSound()
                {
                    style.Yellow("Make sure that your sound is ON.");
                    speak("Make sure that your sound is on.");
                    Thread.Sleep(2000);
                    SystemSounds.Beep.Play(); style.Gray("Playing the \"Beep\" system sound...");
                    Thread.Sleep(1000);
                    SystemSounds.Asterisk.Play(); style.Gray("Playing the \"Asterisk\" system sound...");
                    Thread.Sleep(1000);
                    SystemSounds.Exclamation.Play(); style.Gray("Playing the \"Exclamation\" system sound...");
                    Thread.Sleep(1000);
                    SystemSounds.Hand.Play(); style.Gray("Playing the \"Hand\" system sound...");
                    Thread.Sleep(1000);
                    newLine();
                    newLine();
                    Thread.Sleep(1000); consoleControl();
                }

                //Option#4-Message Control
                public static void messageControl()
                {
                    string response = "Default";
                    switch (randomNumber.Next(3))
                        {
                            case 0: response = "Have a nice day!"; break;
                            case 1: response = "Here is a message box!"; break;
                            case 2: response = "Hello there!"; break;
                        }
                        MessageBox.Show(response, "Message Control Window", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        newLine(); Thread.Sleep(1000); consoleControl();
                }

                //Option#5-Application Control
                public static void applicationControl()
                    {                            
                        //run windows program#1
                        style.Gray("The Calculator application will now open...");
                        speak("The Calculator application will now open.");
                        OpenProgram("calc.exe");
                        Thread.Sleep(1000);

                        //run windows program#2
                        style.Gray("The Paint application will now open...");
                        speak("The paint application will now open.");
                        OpenProgram("mspaint.exe");
                        Thread.Sleep(1000);

                        //run windows program#3
                        style.Gray("The Notepad application will now open...");
                        speak("The notepad application will now open.");
                        OpenProgram("notepad.exe");
                            Thread.Sleep(1000);
                            int[] messageArrayNotepad = new int[] { 070, 111, 108, 108, 111, 119, 032, 116, 104, 101,
                                032, 119, 104, 105, 116, 101, 032, 114, 097, 098, 098, 105, 116, 046, 046, 046 }; //An alternative could have been using .ToArray
                            for (int arrayIndex = 0; arrayIndex < messageArrayNotepad.Length; arrayIndex++)
                            {
                                char key = (char)messageArrayNotepad[arrayIndex];
                                SendKeys.SendWait(key.ToString());
                                Thread.Sleep(randomNumber.Next(200));
                            }
                            Thread.Sleep(1000);

                        //run windows program#4
                        style.Gray("The Chrome application will now open...");
                        speak("If you have chrome installed, that will open too.");
                        OpenProgram("chrome.exe");
                        Thread.Sleep(500);

                        Process runApp = new Process();
                        newLine(); Thread.Sleep(1000); consoleControl();
                    }

                //Option#6-CPU/Memory
                public static void systemMonitor()
                {
                    style.Gray("Processing...");
                    PerformanceCounter valueCPU = new PerformanceCounter("Processor Information", "% Processor Time", "_Total"); 
                    PerformanceCounter valueMEM = new PerformanceCounter("Memory", "Available MBytes");                       
                    style.CyanLine("Current CPU / Memory Usage: ");                     
                    newLine();
                    for (int i = 0; i < 11; i++)
                        {
                            int currentValueCPU = (int)valueCPU.NextValue();
                            int currentValueMEM = (int)valueMEM.NextValue();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("\r{0}% / {1}MB", currentValueCPU, currentValueMEM);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("Counter: {0}", i);
                            Thread.Sleep(200);        
                        }
                    newLine();
                    Console.WriteLine("Done...");
                 }

            //Program#5 - Object List Manipulation
            static void PROGRAM5()
                {
                    Console.Clear();
                    style.Green("PROGRAM#5 - Object List Manipulation (using LINQ)");
                    //Default collection
                        List<camera> cameraCollection = new List<camera>()
                        {
                            new camera() { itemNo = 1, make = "COMP1", year = 2015, price = 210},
                            new camera() { itemNo = 2, make = "COMP1", year = 2015, price = 185},
                            new camera() { itemNo = 3, make = "COMP2", year = 2016, price = 100},
                            new camera() { itemNo = 4, make = "COMP2", year = 2014, price = 135},
                            new camera() { itemNo = 5, make = "COMP3", year = 2016, price = 175}
                        };

                    //Display table:
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.Cyan("|                     DATABASE CONTENT:                    |");
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    ItemNo:  |");
                        foreach (camera tableDisplay in cameraCollection)
                            { style.WhiteLine("    " + tableDisplay.itemNo); style.CyanLine("   |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    Make:    |");
                        foreach (camera tableDisplay in cameraCollection)
                            { style.WhiteLine("  " + tableDisplay.make); style.CyanLine(" |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    Year:    |");
                        foreach (camera tableDisplay in cameraCollection)
                            { style.WhiteLine("  " + tableDisplay.year); style.CyanLine("  |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    Price:   |");
                        foreach (camera tableDisplay in cameraCollection)
                            { style.WhiteLine("  " + "$" + tableDisplay.price); style.CyanLine("  |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        newLine();
                
                    //Choose an option
                        style.Yellow("Please choose an option:");
                        speak("Please choose an option.");
                        newLine();

                        style.RedLine("  [1]"); style.Cyan(" Add ITEM to database.");
                        style.RedLine("  [2]"); style.Cyan(" Remove ITEM from database.");
                        style.RedLine("  [3]"); style.Cyan(" Modify ITEM on database.");
                        style.RedLine("  [4]"); style.Cyan(" Query options for database.");
                        newLine();
                    
                        Console.ForegroundColor = ConsoleColor.Red;

                        controlEntry(1,4);
                        newLine();
                        switch (programEntry)
                        {
                            case "1": addItem(); break;
                            case "2": deleteItem(); break;
                            case "3": modifyItem(); break;
                            case "4": queryItem(); break;
                            default: style.White("Error!"); break;
                        }
                    }

                //Add item to database
                static void addItem()
                {
                    //pseudocode: 1)Enter values, 2)Display results, 3)Console line:
                    //Item value
                        int itemResponse = 6;

                    //Make value
                        style.YellowLine("Select the MAKER of the new item: "); style.RedLine("[1]"); style.YellowLine(" COMP1 ");
                                                                               style.RedLine("[2]"); style.YellowLine(" COMP2 ");
                                                                               style.RedLine("[3]"); style.Yellow(" COMP3 ");
                        Console.ForegroundColor = ConsoleColor.Red;

                        string makeResponse = "";
                        controlEntry(1,3);
                        switch (Convert.ToInt32(programEntry))
                        {
                            case 1: makeResponse = "COMP1"; break;
                            case 2: makeResponse = "COMP2"; break;
                            case 3: makeResponse = "COMP3"; break;
                            default: Console.Write("Select 1, 2, or 3 only."); break;
                        }newLine();

                    //Year value
                        style.YellowLine("Select the YEAR of the new item: "); style.RedLine("[1]"); style.YellowLine(" 2014 ");
                                                                               style.RedLine("[2]"); style.YellowLine(" 2015 ");
                                                                               style.RedLine("[3]"); style.Yellow(" 2016 ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        int yearResponse = 2014;
                        controlEntry(1,3);
                        switch (Convert.ToInt32(programEntry))
                        {
                            case 1: yearResponse = 2014; break;
                            case 2: yearResponse = 2015; break;
                            case 3: yearResponse = 2016; break;
                            default: Console.Write("Select 1, 2, or 3 only."); break;
                        }newLine();

                    //Price value
                        style.YellowLine("Enter PRICE of the new item in"); style.RedLine(" [XXX] "); style.Yellow("format:");
                        Console.ForegroundColor = ConsoleColor.Red;

                        controlEntry(100,999);
                        int priceResponse =  Convert.ToInt32(programEntry);
                        newLine();
                        //confirmation
                            style.Gray("Adding to table...");
                            Thread.Sleep(1000);

                    //Combine the entered values into a camera instance
                        camera camera1 = new camera();
                        camera1.itemNo = itemResponse;
                        camera1.make = makeResponse;
                        camera1.year = yearResponse;
                        camera1.price = priceResponse;

                        //Default collection
                            List<camera> cameraCollection = new List<camera>()
                                {
                                    new camera() { itemNo = 1, make = "COMP1", year = 2015, price = 210},
                                    new camera() { itemNo = 2, make = "COMP1", year = 2015, price = 185},
                                    new camera() { itemNo = 3, make = "COMP2", year = 2016, price = 100},
                                    new camera() { itemNo = 4, make = "COMP2", year = 2014, price = 135},
                                    new camera() { itemNo = 5, make = "COMP3", year = 2016, price = 175}
                                };
                    //Add the values to the table
                    cameraCollection.Add(camera1);

                    //Display table:
                        newLine();
                            style.Cyan("---------------------------------------------------------------------");
                            style.Cyan("|                         DATABASE CONTENT:                         |");
                            style.Cyan("---------------------------------------------------------------------");
                            style.CyanLine("|    ItemNo:  |");
                            foreach (camera tableDisplay in cameraCollection)
                                { style.WhiteLine("    " + tableDisplay.itemNo); style.CyanLine("   |"); }
                            newLine();
                            style.Cyan("---------------------------------------------------------------------");
                            style.CyanLine("|    Make:    |");
                            foreach (camera tableDisplay in cameraCollection)
                                { style.WhiteLine("  " + tableDisplay.make); style.CyanLine(" |"); }
                            newLine();
                            style.Cyan("---------------------------------------------------------------------");
                            style.CyanLine("|    Year:    |");
                            foreach (camera tableDisplay in cameraCollection)
                                { style.WhiteLine("  " + tableDisplay.year); style.CyanLine("  |"); }
                            newLine();
                            style.Cyan("---------------------------------------------------------------------");
                            style.CyanLine("|    Price:   |");
                            foreach (camera tableDisplay in cameraCollection)
                                { style.WhiteLine("  " + "$" + tableDisplay.price); style.CyanLine("  |"); }
                            newLine();
                            style.Cyan("---------------------------------------------------------------------");
                        newLine();

                    //Console control
                        newLine(); Thread.Sleep(2000); consoleControl();
                }//end of AddItem()

                //Remove item from database
                static void deleteItem()
                {
                    List<camera> cameraCollection = new List<camera>()
                        {
                            new camera() { itemNo = 1, make = "COMP1", year = 2015, price = 210},
                            new camera() { itemNo = 2, make = "COMP1", year = 2015, price = 185},
                            new camera() { itemNo = 3, make = "COMP2", year = 2016, price = 100},
                            new camera() { itemNo = 4, make = "COMP2", year = 2014, price = 135},
                            new camera() { itemNo = 5, make = "COMP3", year = 2016, price = 175}
                        };

                    //select line item to remove[1][2][3]
                            newLine();
                            style.Yellow("Select the line item to delete: ");
                            newLine();

                    //speak("Select the line item to remove.");
                            style.RedLine("  [1]"); style.Cyan(" Item#1 ");
                            style.RedLine("  [2]"); style.Cyan(" Item#2 ");
                            style.RedLine("  [3]"); style.Cyan(" Item#3 ");
                            style.RedLine("  [4]"); style.Cyan(" Item#4 ");
                            style.RedLine("  [5]"); style.Cyan(" Item#5 ");
                            newLine();
                    
                            controlEntry(1,5);
                 
                    //confirmation
                        style.Gray("Deleting item...");
                        Thread.Sleep(500);           

                                switch (Convert.ToInt32(programEntry))
                                 {          
                                     case 1: cameraCollection.RemoveAt(0); break;
                                     case 2: cameraCollection.RemoveAt(1); break;
                                     case 3: cameraCollection.RemoveAt(2); break;
                                     case 4: cameraCollection.RemoveAt(3); break;
                                     case 5: cameraCollection.RemoveAt(4); break;
                                     default: speak("Select 1 to 5 only."); break;               
                                 };

                    //Display table:
                        newLine();
                        style.Cyan("---------------------------------------------------");
                        style.Cyan("|                DATABASE CONTENT:                |");
                        style.Cyan("---------------------------------------------------");
                        style.CyanLine("|    ItemNo:  |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("    " + tableDisplay.itemNo); style.CyanLine("   |"); }
                        newLine();
                        style.Cyan("---------------------------------------------------");
                        style.CyanLine("|    Make:    |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("  " + tableDisplay.make); style.CyanLine(" |"); }
                        newLine();
                        style.Cyan("---------------------------------------------------");
                        style.CyanLine("|    Year:    |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("  " + tableDisplay.year); style.CyanLine("  |"); }
                        newLine();
                        style.Cyan("---------------------------------------------------");
                        style.CyanLine("|    Price:   |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("  " + "$" + tableDisplay.price); style.CyanLine("  |"); }
                        newLine();
                        style.Cyan("---------------------------------------------------");
                        newLine();

                    //Console control
                        newLine(); Thread.Sleep(2000); consoleControl();
                }//End of deleteItem()

                //Modify item from database
                static void modifyItem()
                {   //Pseudocode: ask for item number to modify, ask for parameter to modify, enter new value, show results, console
                        string make1 = "COMP1"; string make2 = "COMP2"; string make3 = "COMP3"; string make4 = "COMP4"; string make5 = "COMP5";
                        int year1 = 2015; int year2 = 2015; int year3 = 2016; int year4 = 2014; int year5 = 2016;
                        int price1 = 210; int price2 = 185; int price3 = 100; int price4 = 135; int price5 = 175;

                    //Ask for item number:
                        newLine();
                        style.Yellow("Select the line item to modify: ");
                        newLine();
                        style.RedLine("  [1]"); style.Cyan(" Item#1 ");
                        style.RedLine("  [2]"); style.Cyan(" Item#2 ");
                        style.RedLine("  [3]"); style.Cyan(" Item#3 ");
                        style.RedLine("  [4]"); style.Cyan(" Item#4 ");
                        style.RedLine("  [5]"); style.Cyan(" Item#5 ");
                        newLine();

                        controlEntry(1,5);
                        int responseItem = Convert.ToInt32(programEntry);

                    //Ask for parameter to modify [1]year [2]make [3]price;
                        newLine();
                        style.Yellow("Select the parameter in that item to modify: ");
                        newLine();
                        style.RedLine("  [1]"); style.Cyan(" make ");
                        style.RedLine("  [2]"); style.Cyan(" year ");
                        style.RedLine("  [3]"); style.Cyan(" price ");
                        newLine();                

                        controlEntry(1,3);
                        int response = Convert.ToInt32(programEntry);

                        string responseParameter = "default";
                        switch (response)
                            {
                                case 1: responseParameter = "make"; break;
                                case 2: responseParameter = "year"; break;
                                case 3: responseParameter = "price"; break;
                                default: style.Red("Select 1 to 3 only."); break;
                            };

                    //Enter the new value:
                        newLine();
                        style.Yellow("Enter the new value: ");
                        style.WhiteLine("["); style.RedLine("[XXX]"); style.WhiteLine(" for price, "); style.RedLine("[XXXX]"); style.WhiteLine(" for year, "); style.RedLine("[COMPX]"); style.WhiteLine(" for make]");
                        newLine();

                                //entryPrompt();
                        if (responseParameter == "make") { controlLengthOnly(5,5); }
                        else if (responseParameter == "year") { controlEntry(1000,9999); }
                        else if (responseParameter == "price") { controlEntry(100, 999); }
                        else {style.White("Error!");}
                
                    //string newValueString = "default";
                        //string newValueString = Console.ReadLine();
                        string newValueString = programEntry;

                    //confirmation
                        style.Gray("Updating table...");
                        Thread.Sleep(1000);

                    //change the table:
                        if (responseItem == 1 && responseParameter == "make") { make1 = newValueString; }
                        else if (responseItem == 1 && responseParameter == "year") { year1 = Convert.ToInt32(newValueString); }
                        else if (responseItem == 1 && responseParameter == "price") { price1 = Convert.ToInt32(newValueString); }

                        else if (responseItem == 2 && responseParameter == "make") { make2 = newValueString; }
                        else if (responseItem == 2 && responseParameter == "year") { year2 = Convert.ToInt32(newValueString); }
                        else if (responseItem == 2 && responseParameter == "price") { price2 = Convert.ToInt32(newValueString); }

                        else if (responseItem == 3 && responseParameter == "make") { make3 = newValueString; }
                        else if (responseItem == 3 && responseParameter == "year") { year3 = Convert.ToInt32(newValueString); }
                        else if (responseItem == 3 && responseParameter == "price") { price3 = Convert.ToInt32(newValueString); }

                        else if (responseItem == 4 && responseParameter == "make") { make4 = newValueString; }
                        else if (responseItem == 4 && responseParameter == "year") { year4 = Convert.ToInt32(newValueString); }
                        else if (responseItem == 4 && responseParameter == "price") { price4 = Convert.ToInt32(newValueString); }

                        else if (responseItem == 5 && responseParameter == "make") { make5 = newValueString; }
                        else if (responseItem == 5 && responseParameter == "year") { year5 = Convert.ToInt32(newValueString); }
                        else if (responseItem == 5 && responseParameter == "price") { price5 = Convert.ToInt32(newValueString); }

                        List<camera> cameraCollection = new List<camera>()
                            {
                                new camera() { itemNo = 1, make = make1, year = year1, price = price1},
                                new camera() { itemNo = 2, make = make2, year = year2, price = price2},
                                new camera() { itemNo = 3, make = make3, year = year3, price = price3},
                                new camera() { itemNo = 4, make = make4, year = year4, price = price4},
                                new camera() { itemNo = 5, make = make5, year = year5, price = price5}
                            };

                    //Display table:
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.Cyan("|                     DATABASE CONTENT:                    |");
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    ItemNo:  |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("    " + tableDisplay.itemNo); style.CyanLine("   |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    Make:    |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("  " + tableDisplay.make); style.CyanLine(" |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    Year:    |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("  " + tableDisplay.year); style.CyanLine("  |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        style.CyanLine("|    Price:   |");
                        foreach (camera tableDisplay in cameraCollection)
                        { style.WhiteLine("  " + "$" + tableDisplay.price); style.CyanLine("  |"); }
                        newLine();
                        style.Cyan("------------------------------------------------------------");
                        newLine();

                    //Console control
                        newLine(); Thread.Sleep(2000); consoleControl();
                }

                //Query database
                static void queryItem()
            { 
                //Pseudocode = select sort/filter, proceed accordingly, display results, console
                //Default collection
                List<camera> cameraCollection = new List<camera>()
                            {
                                new camera() { itemNo = 1, make = "COMP1", year = 2015, price = 210},
                                new camera() { itemNo = 2, make = "COMP1", year = 2015, price = 185},
                                new camera() { itemNo = 3, make = "COMP2", year = 2016, price = 100},
                                new camera() { itemNo = 4, make = "COMP2", year = 2014, price = 135},
                                new camera() { itemNo = 5, make = "COMP3", year = 2016, price = 175}
                            };

                //Select sort or filter
                    newLine();
                    style.Yellow("Select the query option: ");
                    newLine();
                    style.RedLine("  [1]"); style.Cyan(" Sort ");
                    style.RedLine("  [2]"); style.Cyan(" Filter ");
                    newLine();

                    controlEntry(1,2);
                    int responseQuery = Convert.ToInt32(programEntry);

                if (responseQuery == 1)
                    {//start of responseQuery if statement
                        newLine();
                        style.Yellow("Select the type of sort:");
                        newLine();
                        style.RedLine("  [1]"); style.Cyan(" Ascending ");
                        style.RedLine("  [2]"); style.Cyan(" Descending ");
                        newLine();
                    
                        controlEntry(1,2);
                        int responseSort = Convert.ToInt32(programEntry);

                        newLine();
                        style.Yellow("Select the parameter to sort:");
                        newLine();
                        style.RedLine("  [1]"); style.Cyan(" Make ");
                        style.RedLine("  [2]"); style.Cyan(" Year ");
                        style.RedLine("  [3]"); style.Cyan(" Price ");
                        newLine();
                    
                        controlEntry(1,3);
                        int responseSortParameter = Convert.ToInt32(programEntry);
                    
                        //confirmation
                        style.Gray("Sorting table...");
                        Thread.Sleep(1000);
               
                        //Update and show results
                        if (responseSort == 1)
                            {//end of responseSort if#1
                                switch (responseSortParameter)
                                {
                                    case 1: var sort1 = from camera in cameraCollection orderby camera.make ascending select camera;
                                        //Display table:
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.Cyan("|                     DATABASE CONTENT:                    |");
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    ItemNo:  |");
                                            foreach (var camera in sort1)
                                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Make:    |");
                                            foreach (var camera in sort1)
                                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Year:    |");
                                            foreach (var camera in sort1)
                                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Price:   |");
                                            foreach (var camera in sort1)
                                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            newLine();break;
                            
                                    case 2: var sort2 = from camera in cameraCollection orderby camera.year ascending select camera;
                                        //Display table:
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.Cyan("|                     DATABASE CONTENT:                    |");
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    ItemNo:  |");
                                            foreach (var camera in sort2)
                                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Make:    |");
                                            foreach (var camera in sort2)
                                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Year:    |");
                                            foreach (var camera in sort2)
                                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Price:   |");
                                            foreach (var camera in sort2)
                                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            newLine();break;
                            
                                    case 3: var sort3 = from camera in cameraCollection orderby camera.price ascending select camera; 
                                        //Display table:
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.Cyan("|                     DATABASE CONTENT:                    |");
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    ItemNo:  |");
                                            foreach (var camera in sort3)
                                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Make:    |");
                                            foreach (var camera in sort3)
                                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Year:    |");
                                            foreach (var camera in sort3)
                                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Price:   |");
                                            foreach (var camera in sort3)
                                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            newLine();break;
                                }
                            }//end of responseSort if#1
                        else if (responseSort == 2)
                            {//start of responseSort if#2
                                switch (responseSortParameter)
                                {
                                    case 1: var sort4 = from camera in cameraCollection orderby camera.make descending select camera;
                                        //Display table:
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.Cyan("|                     DATABASE CONTENT:                    |");
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    ItemNo:  |");
                                            foreach (var camera in sort4)
                                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Make:    |");
                                            foreach (var camera in sort4)
                                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Year:    |");
                                            foreach (var camera in sort4)
                                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Price:   |");
                                            foreach (var camera in sort4)
                                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            newLine(); break;
                                    case 2: var sort5 = from camera in cameraCollection orderby camera.year descending select camera;
                                        //Display table:
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.Cyan("|                     DATABASE CONTENT:                    |");
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    ItemNo:  |");
                                            foreach (var camera in sort5)
                                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Make:    |");
                                            foreach (var camera in sort5)
                                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Year:    |");
                                            foreach (var camera in sort5)
                                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Price:   |");
                                            foreach (var camera in sort5)
                                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            newLine(); break;
                                    case 3: var sort6 = from camera in cameraCollection orderby camera.price descending select camera; 
                                        //Display table:
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.Cyan("|                     DATABASE CONTENT:                    |");
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    ItemNo:  |");
                                            foreach (var camera in sort6)
                                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Make:    |");
                                            foreach (var camera in sort6)
                                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Year:    |");
                                            foreach (var camera in sort6)
                                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            style.CyanLine("|    Price:   |");
                                            foreach (var camera in sort6)
                                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                                            newLine();
                                            style.Cyan("------------------------------------------------------------");
                                            newLine();break;
                                }
                            }//end of responseSort if#2

                }//end of responseQuery if statement

                else if (responseQuery == 2)
                {
                    newLine();
                    style.Yellow("Select the MINIMUM year:");
                    newLine();
                    style.RedLine("  [1]"); style.Cyan(" 2014 ");
                    style.RedLine("  [2]"); style.Cyan(" 2015 ");
                    style.RedLine("  [3]"); style.Cyan(" No year limit required. ");
                    newLine();

                    controlEntry(1, 3);
                    int responseYearLim = Convert.ToInt32(programEntry);
                    int yearLim = 0;
                    if (responseYearLim == 1) { yearLim = 2014; }
                    else if (responseYearLim == 2) { yearLim = 2015; }
                    else { yearLim = 0; }
                    newLine();
                    style.Yellow("Select the MAXIMUM price:");
                    newLine();
                    style.RedLine("  [1]"); style.Cyan(" $150 ");
                    style.RedLine("  [2]"); style.Cyan(" $200 ");
                    style.RedLine("  [3]"); style.Cyan(" No $ limit required. ");
                    newLine();

                    controlEntry(1, 3);
                    int responsePriceLim = Convert.ToInt32(programEntry);
                    int priceLim = 0;
                    if (responsePriceLim == 1) { priceLim = 150; }
                    else if (responsePriceLim == 2) { priceLim = 200; }
                    else { priceLim = 1000; }
                    newLine();

                    style.Yellow("Select the make:");
                    newLine();
                    style.RedLine("  [1]"); style.Cyan(" COMP1 ");
                    style.RedLine("  [2]"); style.Cyan(" COMP2 ");
                    style.RedLine("  [3]"); style.Cyan(" COMP3 ");
                    style.RedLine("  [4]"); style.Cyan(" No make requirement. ");
                    newLine();

                    controlEntry(1,4);
                    int responseCompLim = Convert.ToInt32(programEntry);
                    string compLim = "default";
                    if (responseCompLim == 1) { compLim = "COMP1"; }
                    else if (responseCompLim == 2) { compLim = "COMP2"; }
                    else if (responseCompLim == 3) { compLim = "COMP3"; }             
                    if (responseCompLim == 1 || responseCompLim == 2 || responseCompLim == 3)
                    {
                        var sort1 = from camera in cameraCollection
                                    where camera.make == compLim && camera.year > yearLim && camera.price < priceLim
                                    select camera;

                        //confirmation
                            style.Gray("Filtering table...");
                            Thread.Sleep(1000);

                        //Update and display table:
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.Cyan("|                     DATABASE CONTENT:                    |");
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    ItemNo:  |");
                            foreach (var camera in sort1)
                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    Make:    |");
                            foreach (var camera in sort1)
                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    Year:    |");
                            foreach (var camera in sort1)
                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    Price:   |");
                            foreach (var camera in sort1)
                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            newLine();
                        }

                    else if(responseCompLim == 4)
                    { 
                        var sort2 = from camera in cameraCollection
                                        where camera.make == "COMP1" && camera.year > yearLim && camera.price < priceLim || 
                                        camera.make == "COMP2" && camera.year > yearLim && camera.price < priceLim || 
                                        camera.make == "COMP3" && camera.year > yearLim && camera.price < priceLim
                                        select camera;

                        //Update and display table:
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.Cyan("|                     DATABASE CONTENT:                    |");
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    ItemNo:  |");
                            foreach (var camera in sort2)
                            { style.WhiteLine("    " + camera.itemNo); style.CyanLine("   |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    Make:    |");
                            foreach (var camera in sort2)
                            { style.WhiteLine("  " + camera.make); style.CyanLine(" |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    Year:    |");
                            foreach (var camera in sort2)
                            { style.WhiteLine("  " + camera.year); style.CyanLine("  |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            style.CyanLine("|    Price:   |");
                            foreach (var camera in sort2)
                            { style.WhiteLine("  " + "$" + camera.price); style.CyanLine("  |"); }
                            newLine();
                            style.Cyan("------------------------------------------------------------");
                            newLine();
                        }
                }
                else { style.Red("Enter 1 or 2 ONLY!"); }

                //Console control
                    newLine(); Thread.Sleep(2000); consoleControl();      
            }
    }  

}