using System;
using System.ComponentModel;
using System.Net.Mail;
using BE;
using BL;
using static BL.bl_imp;

namespace PL
{
    public class Program
    {
         IBL bl = bl_imp.getBl();
      
        //private IBL bl = new bl_imp();
       
        static void Main(string[] args)
        {
            Person human = new Person("314784539", "ehud", "gershony", DateTime.Parse("30/07/1996"), Gender.male,"0530010100",new Adress("kolombia",6,"jerusalem"),Vehicle.motorcycle );
            Tester checkForTester = new Tester(human.ID, human.FirstName, human.LastName, human.Birthday, human.Gender, human.Phone, human.Adress, human.VehicleType,11,25,100);
            Trainee checkForTrainee = new Trainee(human.ID, human.FirstName, human.LastName, human.Birthday, human.Gender, human.Phone, human.Adress, human.VehicleType,Gear.manual,"or-yarok",checkForTester.FirstName+"\n"+ checkForTester.LastName,21);
            Console.WriteLine("some person");
            Console.WriteLine(human);
            Console.WriteLine("\ndetails for tester");
            Console.WriteLine(checkForTester);
            Console.WriteLine("\ndetails for trainee");
            Console.WriteLine(checkForTrainee);
            Console.WriteLine("welcome to computer data bank of transport ministery");
            do
            {
                switch ((User) Convert.ToInt32(Console.ReadLine()))
                {
                    case User.administrator:
                        Console.WriteLine("print:\n   1:all tests list" +
                                          "\n   2:all testers list" +
                                          "\n   3:all trainees list" +
                                          "\n   4:list for tests in specific day" +
                                          "\n   5:list for passed trainees" +
                                          "\n   6:list for tests in specific vehicle" +
                                          "\n   7:list for tests in specific tester" +
                                          "\n   8:list for tests in specific trainee\n");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                            case 7:
                                break;
                            case 8:
                                break;
                            default:
                                break;
                        }

                        break;
                    case User.tester:
                        Console.WriteLine("1: new tester\n" +
                                          "2: exist tester\n");
                        switch (Convert.ToInt32(Console.ReadLine()))
                        {
                            case 1:
                                Console.WriteLine("put the next fields:\n" +
                                                  "   ID,first name, last name, birthday(with the form: DD/MM/YYYY, gender, phone number, adress");


                                break;
                            case 2:
                                Console.WriteLine("1:update test\n" +
                                                  "2:quit from this job\n");
                                switch (Convert.ToInt32(Console.ReadLine()))
                                {
                                    case 1:
                                        Console.WriteLine("put the test ID\n");
                                        string testId = Console.ReadLine();
                                        //find the excepted test
                                        //fill the fields for test
                                        //send to update
                                        break;
                                    case 2:
                                        Console.WriteLine("sory for your leaving\n");
                                        //send to remove test
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default:
                                break;
                        }

                        break;
                    case User.trainee:
                        break;
                    default:
                        break;
                }

            } while (true);

        }
    }

}
