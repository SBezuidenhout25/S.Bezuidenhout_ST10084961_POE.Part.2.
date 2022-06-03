using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjPOE
{
    class BuyCar : HomeLoan
    {
        //Additional variables for needed for calculations
        double totalLoan;
        double monthlyPayment = 0;
        double insurance;
        string make;
        string model;

        //Getter and setter methods for variables
        public double Insurance { get => insurance; set => insurance = value; }
        public string Make { get => make; set => make = value; }
        public string Model { get => model; set => model = value; }

        //Calculates monthly car loan repayments
        public override void calculate(double price)
        {
            totalLoan = (price - deposit) * (1 + interest * (months / 12));
            monthlyPayment = ((totalLoan / months) + Insurance);
            availableMoney -= monthlyPayment;

            LstExpenses.Add(Math.Round(monthlyPayment, 2));
        }
    }
}
