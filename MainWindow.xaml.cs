using Microsoft.VisualBasic;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Icarus_Service_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Drone> finishedList = new List<Drone>();
        Queue<Drone> expressQueue = new Queue<Drone>();
        Queue<Drone> regularQueue = new Queue<Drone>();
        
        
        
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
        if(!string.IsNullOrEmpty(TextBoxName.Text) && !string.IsNullOrEmpty(TextBoxModel.Text)
                &&!string.IsNullOrEmpty(TextBoxProblem.Text) &&!string.IsNullOrEmpty(TextBoxCost.Text) && !string.IsNullOrEmpty(GetServicePriority()))
            {
            try
             {
            Drone addDrone = new Drone();
            addDrone.SetName(TextBoxName.Text);
            addDrone.SetProblem(TextBoxProblem.Text);
            addDrone.SetModel(TextBoxModel.Text);
            addDrone.SetCost(Convert.ToDouble(TextBoxCost.Text));
            addDrone.SetTag(Convert.ToInt32(upDown.Value)+ TagIncrement());
            upDown.Value = addDrone.GetTag()+TagIncrement();
            if(GetServicePriority().Equals("Express"))
            {
                addDrone.SetCost(addDrone.GetCost()+(addDrone.GetCost()*15)/100);
                expressQueue.Enqueue(addDrone);
                DisplayExpressQueue();
            }
            else if(GetServicePriority().Equals("Regular")) 
            {
                regularQueue.Enqueue(addDrone);
                DisplayRegularQueue();
            }
            else
            {
                    MessageBar.Text = "Please Select the Service Type";
            }
            ClearTextBox();
            MessageBar.Text = "Drone Has been added Successfully";
        }
        catch(Exception)
                {
                    MessageBar.Text = "Something Went Wrong Please Try Again";
                }
            }
            else
            {
                MessageBar.Text = "All Fields are Mandotry to fill";
            }
         }

        #region Display
        public void DisplayExpressQueue()
        {

           ListViewExpress.Items.Clear();
            try
            {
                foreach (Drone information in expressQueue)
                {
                    var row = new
                    {
                        client_Name = information.GetName(),
                        drone_Model = information.GetModel(),
                        service_Problem = information.GetProblem(),
                        service_Cost = information.GetCost(),
                        service_Tag = information.GetTag()
                    };
                    ListViewExpress.Items.Add(row);
                }
            }
            catch(Exception)
            {
                MessageBar.Text = "Something Went Wrong Please Try Again";
            }
          
        }
        public void DisplayRegularQueue()
        {
            ListViewRegular.Items.Clear();
            try
            {

            foreach (Drone drone in regularQueue)
            {
                var row = new
                {
                    client_Name = drone.GetName(),
                    drone_Model = drone.GetModel(),
                    service_Problem = drone.GetProblem(),
                    service_Cost = drone.GetCost(),
                    service_Tag = drone.GetTag()
                };
                ListViewRegular.Items.Add(row);
            }
            }
            catch(Exception)
            {
                MessageBar.Text = "Something Went Wrong Please Try Again";
            }
        }
        public void DisplayList()
        {
            FinishedListBox.Items.Clear();
            foreach(var serviceComlpeted in finishedList)
            {
                FinishedListBox.Items.Add("Client Name: "+serviceComlpeted.GetName() + "\t\t Amount Due: " 
                    + serviceComlpeted.GetCost());  
            }
        }
           
        #endregion Display

        #region Service Priority
        private string GetServicePriority()
        {
            string serviceType = "";
            foreach(RadioButton rb in ServiceOption.Children.OfType<RadioButton>())
            {
                if(rb.IsChecked == true)
                {
                serviceType = rb.Name.ToString();
                }
            }
            return serviceType;
            
        }
        #endregion Service Priority

        private void ButtonExpress1_Click(object sender, RoutedEventArgs e)
        {
            if(expressQueue.Count != 0)
            {
                try
                {

                ClearTextBox();
                finishedList.Add(expressQueue.Dequeue());
                ListViewExpress.Items.RemoveAt(0);
                DisplayList();   
                }
                catch(Exception)
                {
                    MessageBar.Text = "Something Went Wrong Please Try Again";
                }
            }
            DisplayExpressQueue();
        }

        private void ButtonRegular1_Click(object sender, RoutedEventArgs e)
        {
            if(regularQueue.Count != 0)
            {
                try
                {
                    ClearTextBox();
                    finishedList.Add(regularQueue.Dequeue());
                    ListViewRegular.Items.RemoveAt(0);
                    DisplayList();
                }
                catch(Exception)
                {
                    MessageBar.Text = "Something Went Wrong Please Try Again";
                }
               
            }
            DisplayRegularQueue();
        }
        public void ClearTextBox()
        {
            TextBoxName.Clear();
            TextBoxCost.Clear();
            TextBoxProblem.Clear();
            TextBoxModel.Clear();
        }

       private void FinishedListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(FinishedListBox.SelectedItems.Count != 0)
            {
                int currentItem = FinishedListBox.SelectedIndex;
                finishedList.RemoveAt(currentItem);
                DisplayList();
            }
            else
            {
                MessageBar.Text = "Please Select From the List to remove from Service List";
            }
            
        }

        private void ListViewExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ButtonExpress1.IsFocused ==true)
            {
                ClearTextBox();
            }
            else
            {
                if(ListViewExpress.SelectedItems.Count != 0)
                {

                try
                {
                int select = ListViewExpress.SelectedIndex;
                TextBoxName.Text = expressQueue.ElementAt(select).GetName();
                TextBoxProblem.Text = expressQueue.ElementAt(select).GetProblem();
                }
                catch(Exception)
                {
                    MessageBar.Text = "Something went wrong please try again";
                }
                }
                else
                {
                    MessageBar.Text = "Please Select from queue to Get Details";
                }
            }
            
        }

        private void ListViewRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ButtonRegular1.IsFocused==true)
            {
                ClearTextBox();
            }
            else
            {
                if(ListViewRegular.SelectedItems.Count != 0)
                {

                int selected = ListViewRegular.SelectedIndex;
                TextBoxName.Text = regularQueue.ElementAt(selected).GetName();
                TextBoxProblem.Text = regularQueue.ElementAt(selected).GetProblem();
                }
                else
                {
                    MessageBar.Text = "Please Select from the queue to get deatils";
                }
            }
        }
        public int TagIncrement()
        {
        return 10;
        }


        private void TextBoxCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            bool approvedDecimalPoint = false;
            
            if (e.Text == ".")
            {
               
                if (!((TextBox)sender).Text.Contains("."))
                    
                approvedDecimalPoint = true;
            }
            if (!(char.IsDigit(e.Text, e.Text.Length - 1) || approvedDecimalPoint))
            {
                e.Handled = true;
                string inText = TextBoxCost.Text; //My Input TextBox from XAML
                int decPointIndex = inText.IndexOf('.');
                if (decPointIndex > 0 && ((inText.Length - (decPointIndex + 1)) > 2))
                    TextBoxCost.Text = inText.Substring(0, decPointIndex + 3);
            }
                

            /*
            Regex regex = new Regex("^[0-9]");
            if(regex.IsMatch(TextBoxCost.Text))
            {
                string inText = TextBoxCost.Text; //My Input TextBox from XAML
                int decPointIndex = inText.IndexOf('.');
                if (decPointIndex > 0 && ((inText.Length - (decPointIndex + 1)) > 2))
                    TextBoxCost.Text = inText.Substring(0, decPointIndex + 3);
            }
            
            */
        }
    }

}
