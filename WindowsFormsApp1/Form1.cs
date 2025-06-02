using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private IWebDriver driver;
        public Form1()
        {
            InitializeComponent();
            StartBrowser();
        }
        private void StartBrowser()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.ebay.com");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = textBox1.Text;

                WebDriverWait wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(500));

                // Pagaida līdz meklēšanas lauks ir pieejams
                var searchBox = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("gh-ac")));
                searchBox.Clear();
                searchBox.SendKeys(query);

                // Pagaida līdz meklēšanas poga ir pieejama
                var searchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("gh-btn")));
                searchButton.Click();

                // Pagaida kamēr ielādējas rezultātu lapa 
                System.Threading.Thread.Sleep(2000);

                string resultUrl = driver.Url;
                textBox2.Text = resultUrl;
                richTextBox1.AppendText(resultUrl + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda meklēšanā: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                driver.Navigate().Back();
                textBox1.Clear();
                textBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda atgriežoties: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                driver.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kļūda aizverot pārlūku: " + ex.Message);
            }
        }
    }

}
