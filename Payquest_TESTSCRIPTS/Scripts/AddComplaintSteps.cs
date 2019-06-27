using Dapper;
using OpenQA.Selenium;
using Payquest_Testing;
using System;
using System.Configuration;
using System.Data.SqlClient;
using TechTalk.SpecFlow;

namespace Payquest_TESTSCRIPTS
{
    [Binding]


    public class AddComplaintSteps
    {

        public class SpecFlowFeature1Steps_Transaction : Class1
        {
            private static Class1 accessor = new Class1();


            #region Queries -------------------------------------------------------

            private const string RANDOM_DEBTOR_QUERY = @"SELECT TOP(1) ddd.DebtorEntityID FROM Debt.DebtDebtorDetail ddd JOIN Debt.Debt  d ON ddd.DebtID = d.DebtID WHERE d.DebtStatusID =8 and TrancheID=852";

            #endregion Queries ----------------------------------------------------

            #region Queries -------------------------------------------------------

            private const string RANDOM_DEBT_QUERY = @"SELECT TOP(1) ddd.debtID FROM Debt.DebtDebtorDetail ddd JOIN Debt.Debt d ON ddd.DebtID = d.DebtID WHERE d.DebtStatusID =8 and TrancheID=852 ";

            #endregion Queries ----------------------------------------------------




            private static long debtorID = -1;

            private static long GetDebtorID()
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GenericConnection"].ConnectionString))
                {
                    conn.Open();

                    return conn.QuerySingle(RANDOM_DEBTOR_QUERY).DebtorEntityID;
                }


            }

            private static long debtID = -1;

            private static long GetDebtID()
            {

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GenericConnection"].ConnectionString))
                {
                    conn.Open();

                    return conn.QuerySingle(RANDOM_DEBT_QUERY).debtID;
                }

            }
            [BeforeTestRun]

            public void Start()
            {

                debtID = GetDebtID();
                accessor.Open(string.Format(@"{0}/DebtDebtorDetails/Debt/{1}", accessor.BaseURL, debtID));

            }

            [AfterTestRun]

            public void Teardown()
            {

                accessor.Close();
            }

            [Given(@"I have logged into the application")]
        public void GivenIHaveLoggedIntoTheApplication()
        {
                debtorID = GetDebtorID();
                accessor.ClickTab(string.Format("#debtor{0}BankDetails", debtorID));

                IWebElement addcreditcard = accessor.GetElementByXPath(string.Format("//button[@ng-click='accountCtrl.model.addCreditCard($event)']"));
                addcreditcard.Click();

                System.Threading.Thread.Sleep(2000);
            }
        
        [When(@"I raise complaint")]
        public void WhenIRaiseComplaint(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the debt status is complaint")]
        public void ThenTheDebtStatusIsComplaint()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
