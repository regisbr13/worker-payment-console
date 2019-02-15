using System;
using System.Globalization;
using WorkerPayment.Entities;
using WorkerPayment.Entities.Enums;

namespace WorkerPayment
{
    class Program
    {
        static void Main(string[] args)
        {
            Exception exception = null;
            do
            {
                try
                {
                    Console.Write("Enter department's name: ");
                    string departmentName = Console.ReadLine();
                    Console.WriteLine("Enter worker data: ");
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Level(Junior / MidLevel / Senior): ");
                    WorkerLevel level = Enum.Parse<WorkerLevel>(Console.ReadLine());
                    Console.Write("Base salary: ");
                    double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

                    Department department = new Department(departmentName);
                    Worker worker = new Worker(name, level, salary, department);

                    Console.Write("How many contracts to this worker ? ");
                    int n = int.Parse(Console.ReadLine());
                    for (int i = 1; i <= n; i++)
                    {
                        Console.WriteLine("Enter #" + i + " contract data:");
                        Console.Write("Date(DD/MM/YYYY): ");
                        DateTime dateContract = DateTime.Parse(Console.ReadLine());
                        Console.Write("Value per hour: ");
                        double value = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                        Console.Write("Duration(hours): ");
                        int hours = int.Parse(Console.ReadLine());
                        HourContract contract = new HourContract(dateContract, value, hours);
                        worker.AddContract(contract);
                    }

                    Console.Write("Enter month and year to calculate income(MM/YYYY): ");
                    string date = Console.ReadLine();
                    int month = int.Parse(date.Substring(0, 2));
                    int year = int.Parse(date.Substring(3));
                    Console.WriteLine(worker);
                    Console.Write("Income for " + date + ": R$ " + worker.Income(year, month).ToString("F2", CultureInfo.InvariantCulture) + "\n");
                }
                catch(ArgumentException e)
                {
                    exception = e;
                    Console.WriteLine("Invalid option! " + e.Message);
                    Console.WriteLine();
                    Console.WriteLine("Let's start again:");
                }
                catch (FormatException e)
                {
                    exception = e;
                    Console.WriteLine("Format error! " + e.Message);
                    Console.WriteLine();
                    Console.WriteLine("Let's start again:");
                }
            } while (exception != null);

            Console.ReadKey();
        }
    }
}
