using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjPOE
{
    public partial class Form1 : Form
    {
        //List (Generic Collection) to store expenses
        List<double> lstExpenses = new List<double>();
        double income, price, deposit, interest, months;
        //Variables to get user input from textBoxes
        double carPrice, carDeposit, carInterest, insurance;
        string make, model;


        public Form1()
        {
            InitializeComponent();
        }

       //Radio Button method checks what radio button is selected and
       //makes the required changes to the form, (Only makes
       //relevant text boxes visible)
        private void rbtBuy_CheckedChanged(object sender, EventArgs e)
        {
            lblPriceRent.Text = "Purchase Price:";
            lblPriceRent.Visible = true;
            txtPriceRent.Visible = true;

            lblDeposit.Visible = true;
            txtDeposit.Visible = true;
            lblInterest.Visible = true;
            txtInterest.Visible = true;
            lblMonths.Visible = true;
            txtMonths.Visible = true;
        }

        private void rbtYes_CheckedChanged(object sender, EventArgs e)
        {
            lblMake.Visible = true;
            txtMake.Visible = true;
            lblModel.Visible = true;
            txtModel.Visible = true;
            lblCarPrice.Visible = true;
            txtCarPrice.Visible = true;
            lblCarDeposit.Visible = true;
            txtCarDeposit.Visible = true;
            lblCarInterest.Visible = true;
            txtCarInterest.Visible = true;
            lblCarInsurance.Visible = true;
            txtCarInsurance.Visible = true;
        }

        private void rbtNo_CheckedChanged(object sender, EventArgs e)
        {
            lblMake.Visible = false;
            txtMake.Visible = false;
            lblModel.Visible = false;
            txtModel.Visible = false;
            lblCarPrice.Visible = false;
            txtCarPrice.Visible = false;
            lblCarDeposit.Visible = false;
            txtCarDeposit.Visible = false;
            lblCarInterest.Visible = false;
            txtCarInterest.Visible = false;
            lblCarInsurance.Visible = false;
            txtCarInsurance.Visible = false;
        }

        private void rbtRent_CheckedChanged(object sender, EventArgs e)
        {
            lblPriceRent.Text = "Monthly Rent:";
            lblPriceRent.Visible = true;
            txtPriceRent.Visible = true;
            

            lblDeposit.Visible = false;
            txtDeposit.Visible = false;
            lblInterest.Visible = false;
            txtInterest.Visible = false;
            lblMonths.Visible = false;
            txtMonths.Visible = false;           
        }

        //Runs when calculate button is clicked
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var error = addValues();

            //If checks which radio button option is selected
            //and sets values to the applied class
            if (rbtBuy.Checked == true && error == false)
            {
                Expense buy = new HomeLoan();
                buy.income = income;
                buy.LstExpenses = lstExpenses;
                buy.deposit = deposit;
                buy.interest = interest;
                buy.months = months;

                //Checks if user wants to buy a car
                if (rbtYes.Checked == true)
                {
                    buy.LstExpenses = Car(buy.LstExpenses);
                    buy.LstExpensesNames.Add("Monthly car payments:");
                }
                buy.calculate(price);

                //Displays the output
                MessageBox.Show(buy.ToString());               
            }
            else if (rbtRent.Checked == true &&  error == false)
            {
                Expense rent = new Rent();
                rent.income = income;
                rent.LstExpenses = lstExpenses;

                //Checks if user wants to buy a car
                if (rbtYes.Checked == true)
                {
                    rent.LstExpenses = Car(rent.LstExpenses);
                    rent.LstExpensesNames.Add("Monthly Car payments:");
                }
                rent.calculate(price);

                //Display the output
                MessageBox.Show(rent.ToString());
            }
            //Only runs if the user has not selected to buy or rent
            else if (rbtBuy.Checked == false && rbtRent.Checked == false)
            {
                MessageBox.Show("Please select if you are renting or buying a property.");
            }

            //Clears the list after calculation so that new expenses
            //can be saved anew instead of being added to existing expenses
            lstExpenses.Clear();
        }

        //Method adds values to the BuyCar class
        //and alters the expenses list accordingly
        private List<double> Car(List<double> expenses)
        {
            BuyCar car = new BuyCar();
            car.LstExpenses = expenses;
            car.deposit = carDeposit;
            car.interest = carInterest;
            car.months = 60;
            car.Insurance = insurance;
            car.Make = make;
            car.Model = model;
            car.calculate(carPrice);
            expenses = car.LstExpenses;
            return expenses;
        }

        //Method stores user input
        private bool addValues()
        {
            var error = checkInput();

            //Try Catch to store input and throw exceptions
            //depending on error type
            try
            {
                if (error == true)
                {
                    throw new ArgumentNullException();
                }
                price = Convert.ToDouble(txtPriceRent.Text);
                income = Convert.ToDouble(txtIncome.Text);
                lstExpenses.Add(Convert.ToDouble(txtGroceries.Text));
                lstExpenses.Add(Convert.ToDouble(txtWaterLights.Text));
                lstExpenses.Add(Convert.ToDouble(txtTravel.Text));
                lstExpenses.Add(Convert.ToDouble(txtPhone.Text));
                lstExpenses.Add(Convert.ToDouble(txtOther.Text));


                //If statement stores buy values only if buy 
                //option is selected
                if (rbtBuy.Checked == true)
                {
                    deposit = Convert.ToDouble(txtDeposit.Text);
                    interest = Convert.ToDouble(txtInterest.Text) / 100;
                    months = Convert.ToDouble(txtMonths.Text);
                }

                //If statement stores car values only if yes 
                //option is selected
                if (rbtYes.Checked == true)
                {
                    make = txtMake.Text;
                    model = txtModel.Text;
                    carPrice = Convert.ToDouble(txtCarPrice.Text);
                    carDeposit = Convert.ToDouble(txtCarDeposit.Text);
                    carInterest = Convert.ToDouble(txtCarInterest.Text)/100;
                    insurance = Convert.ToDouble(txtCarInsurance.Text);
                }
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Please enter all fields");
                error = true;
            }
            catch(FormatException)
            {
                MessageBox.Show("Please only enter numeric values");
                error = true;
            }
            return error;            
        }

        //Method checks user input for errors and changes
        //color of text boxes with missing input
        private bool checkInput()
        {
            var error = false;
            foreach(TextBox t in Controls.OfType<TextBox>())
            {
                if (t.TextLength == 0 && t.Visible == true)
                {
                    error = true;
                    t.BackColor = Color.Tomato;
                }
                else
                {
                    t.BackColor = Color.White;
                }
            }
            return error;           
        }
    }
}
