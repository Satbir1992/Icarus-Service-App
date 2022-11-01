using Microsoft.VisualBasic;
using Syncfusion.Windows.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.PortableExecutable;
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
//Satbir Singh
// Student ID 30048567
//Date 01/11/2022
//6.18	All code is required to be adequately commented.
//Map the programming criteria and features to your code/methods by adding comments above the method signatures. 

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
        //6.2	Create a global List<T> of type Drone called “FinishedList”. 
        //6.3	Create a global Queue<T> of type Drone called “RegularService”.
        //6.4	Create a global Queue<T> of type Drone called “ExpressService”.
        List<Drone> finishedList = new List<Drone>();
        Queue<Drone> expressQueue = new Queue<Drone>();
        Queue<Drone> regularQueue = new Queue<Drone>();
        List<int> tagsaver = new();

        //6.5	Create a button method called “AddNewItem” that will add a new service item to a Queue<> based on the priority.
        //Use TextBoxes for the Client Name, Drone Model, Service Problem and Service Cost.
        //Use a numeric up/down control for the Service Tag. The new service item will be added to the
        //appropriate Queue based on the Priority radio button.
        #region Add Drone 
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
        if(!string.IsNullOrEmpty(TextBoxName.Text) && !string.IsNullOrEmpty(TextBoxModel.Text)
                &&!string.IsNullOrEmpty(TextBoxProblem.Text) &&!string.IsNullOrEmpty(TextBoxCost.Text) && 
                !string.IsNullOrEmpty(GetServicePriority()))//Checking For textboxes empty or null values
            {
             try
                     {
                      Drone addDrone = new Drone();
                      addDrone.SetName(TextBoxName.Text);
                      addDrone.SetProblem(TextBoxProblem.Text);
                      addDrone.SetModel(TextBoxModel.Text);
                      addDrone.SetCost(Convert.ToDouble(TextBoxCost.Text));
                      addDrone.SetTag(TagIncrement());// Using Tag Increment
                    tagsaver.Add(addDrone.GetTag());
                      upDown.Value = addDrone.GetTag();
                      if(GetServicePriority().Equals("Express"))
                        {
                      //6.6	Before a new service item is added to the Express Queue the service cost must be increased by 15%.
                        addDrone.SetCost(addDrone.GetCost()+(addDrone.GetCost()*15)/100);//if express increasing cost by 15%
                        expressQueue.Enqueue(addDrone);
                        DisplayExpressQueue();
                        }
                      if(GetServicePriority().Equals("Regular")) 
                        {
                        regularQueue.Enqueue(addDrone);
                        DisplayRegularQueue();
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
        #endregion Add Drone

        //6.7	Create a custom method called “GetServicePriority” which returns the value of the priority radio group.
        //This method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        #region Service Priority
        private string GetServicePriority()// Checking service option through selected radio buttons
        {
            string serviceType = "";
            foreach (RadioButton rb in ServiceOption.Children.OfType<RadioButton>())
            {
                if (rb.IsChecked == true)
                {
                    serviceType = rb.Name.ToString();
                }
            }
            return serviceType;

        }
        #endregion Service Priority

        //6.8	Create a custom method that will display all the elements in the RegularService queue.
        //The display must use a List View and with appropriate column headers.
        //6.9	Create a custom method that will display all the elements in the ExpressService queue.
        //The display must use a List View and with appropriate column headers.

        #region Display
        public void DisplayExpressQueue()
        {

           ListViewExpress.Items.Clear();
            try
            {
                foreach (Drone information in expressQueue)
                {
                    var row = new// Displaying in queue and display binding defined in Xaml file
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
                var row = new  //Displaying in queue and display binding defined in Xaml file
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
        public void DisplayList()// Display of finished List
        {
            FinishedListBox.Items.Clear();
            foreach(var serviceComlpeted in finishedList)
            {
                FinishedListBox.Items.Add("Client Name:  "+serviceComlpeted.GetName() + " \tAmount Due:  " 
                    + serviceComlpeted.GetCost());  
            }
        }

        #endregion Display

        //6.10	Create a custom keypress method to ensure the Service Cost textbox can only accept a double value with one decimal point.
        #region Service Cost one Decimal 
        private void TextBoxCost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
            {
                e.Handled = false;
                int decPointIndex = TextBoxCost.Text.IndexOf('.');
                if (decPointIndex > 0 && ((TextBoxCost.Text.Length - (decPointIndex + 1)) >= 1))
                {
                    TextBoxCost.Text = TextBoxCost.Text.Substring(0, decPointIndex + 2);
                    upDown.Focus();
                }
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion Service Cost Two Decimal

        //6.11	Create a custom method to increment the service tag control,
        //this method must be called inside the “AddNewItem” method before the new service item is added to a queue.
        #region Tag Increment and duplicate values
        public int TagIncrement()
        {
            
            int tagCheck = Convert.ToInt32(upDown.Value);
            if(tagCheck>=100 && tagCheck<=900 && tagsaver.Exists(x=>x.Equals(tagCheck)))//checking for duplicate tags and assiging the unique value 
            {
                do
                {
                    tagCheck += 10;
                }
                while (tagsaver.Exists(x => x.Equals(tagCheck)));
            }
            else
            {
                MessageBar.Text = "Invalid Tag Value please check again";
            }
            return tagCheck;
        }
        #endregion Tag Increment and duplicate values

        //6.12	Create a mouse click method for the regular service ListView that will display the Client Name and Service Problem in the related textboxes.
        //6.13	Create a mouse click method for the express service ListView that will display the Client Name and Service Problem in the related textboxes
        #region Selected listview Item Display
        private void ListViewExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if (ListViewExpress.SelectedItems.Count != 0)
                {
                    try
                    {
                        int select = ListViewExpress.SelectedIndex;
                        TextBoxName.Text = expressQueue.ElementAt(select).GetName();
                        TextBoxProblem.Text = expressQueue.ElementAt(select).GetProblem();
                        ListViewExpress.SelectedIndex = -1;
                   }
                    catch (Exception)
                    {
                        MessageBar.Text = "Something went wrong please try again";
                    }
                }
        }
        
        private void ListViewRegular_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (ListViewRegular.SelectedItems.Count != 0)
                {
                try
                {
                    int selected = ListViewRegular.SelectedIndex;
                    TextBoxName.Text = regularQueue.ElementAt(selected).GetName();
                    TextBoxProblem.Text = regularQueue.ElementAt(selected).GetProblem();
                    ListViewRegular.SelectedIndex = -1; 
                }
                catch(Exception)
                {
                    MessageBar.Text = "Something went wrong please try again";
                }
                }
        }
        #endregion Selected listview item display

        //6.14	Create a button click method that will remove a service item from the regular ListView and dequeue the regular service
        //Queue<T> data structure. The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.
        //6.15	Create a button click method that will remove a service item from the express ListView and dequeue the express service
        //Queue<T> data structure.The dequeued item must be added to the List<T> and displayed in the ListBox for finished service items.

        #region Service Completed
        private void ButtonExpress1_Click(object sender, RoutedEventArgs e)
        {
            if(expressQueue.Count != 0)
            {
                try
                {
                ClearTextBox();
                upDown.Value = expressQueue.ElementAt(0).GetTag();
                tagsaver.Remove(Convert.ToInt32(upDown.Value));
                finishedList.Add(expressQueue.Dequeue());//dequeueing the service from queue and sending it to finished list 
                ListViewExpress.Items.RemoveAt(0);
                DisplayList();   
                }
                catch(Exception)
                {
                    MessageBar.Text = "Something Went Wrong Please Try Again";
                }
            }
            else
            {
                MessageBar.Text = "nothing to remove";
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
                    upDown.Value = regularQueue.ElementAt(0).GetTag();
                    tagsaver.Remove(Convert.ToInt32(upDown.Value));
                    finishedList.Add(regularQueue.Dequeue());//dequeueing the service from queue and sending it to finished list
                    ListViewRegular.Items.RemoveAt(0);
                    DisplayList();
                }
                catch(Exception)
                {
                    MessageBar.Text = "Something Went Wrong Please Try Again";
                }
            }
            else
            {
                MessageBar.Text = "nothing to remove";
            }
            DisplayRegularQueue();
        }
        #endregion Service Completed

        //6.16	Create a double mouse click method that will delete a service item from the finished listbox and remove the same item from the List<T>.
        #region Finished List
        private void FinishedListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)//double click to remove the drone from the service list
        {
            if (FinishedListBox.SelectedItems.Count != 0)
            {
                try
                {
                    int currentItem = FinishedListBox.SelectedIndex;
                    finishedList.RemoveAt(currentItem);
                    DisplayList();
                }
                catch (Exception)
                {
                    MessageBar.Text = "Something Went Wrong Please Try Again";
                }
            }
            else
            {
                MessageBar.Text = "Please Select From the List to remove from Service List";
            }

        }

        #endregion Finished List

        //6.17	Create a custom method that will clear all the textboxes after each service item has been added.
        #region Clear TextBoxes zone
        public void ClearTextBox()
        {
            TextBoxName.Clear();
            TextBoxCost.Clear();
            TextBoxProblem.Clear();
            TextBoxModel.Clear();
        }
        #endregion Clear TextBoxes zone
    }
}
