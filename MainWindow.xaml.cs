using Microsoft.VisualBasic;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Drone addDrone = new Drone();
            addDrone.setName(TextBoxName.Text);
            addDrone.setProblem(TextBoxProblem.Text);
            addDrone.setModel(TextBoxModel.Text);
            addDrone.setCost(Convert.ToDouble(TextBoxCost.Text));
            addDrone.setTag(Convert.ToInt32(upDown.Value));
            upDown.Value = addDrone.getTag()+10;
            if(GetServicePriority().Equals("Express"))
            {
                addDrone.setCost(addDrone.getCost()+(addDrone.getCost()*15)/100);
                expressQueue.Enqueue(addDrone);
                
                DisplayExpressQueue();
            }
            else 
            {
                regularQueue.Enqueue(addDrone);
                DisplayRegularQueue();
            }
            ClearTextBox();
         }

        #region Display
        public void DisplayExpressQueue()
        {

           ListViewExpress.Items.Clear();
            foreach (Drone information in expressQueue)
            {
                var row = new
                {
                    client_Name = information.getName(),
                    drone_Model = information.getModel(),
                    service_Problem = information.getProblem(),
                    service_Cost = information.getCost(),
                    service_Tag = information.getTag()
                };
                ListViewExpress.Items.Add(row);
            }
        }
        public void DisplayRegularQueue()
        {
            ListViewRegular.Items.Clear();
            foreach (Drone drone in regularQueue)
            {
                var row = new
                {
                    client_Name = drone.getName(),
                    drone_Model = drone.getModel(),
                    service_Problem = drone.getProblem(),
                    service_Cost = drone.getCost(),
                    service_Tag = drone.getTag()
                };
                ListViewRegular.Items.Add(row);

            }
        }
        public void DisplayList()
        {
            FinishedListBox.Items.Clear();
            foreach(var serviceComlpeted in finishedList)
            {
                FinishedListBox.Items.Add("Client Name: "+serviceComlpeted.getName() + "\t\t Amount Due: " 
                    + serviceComlpeted.getCost());  
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
                    serviceType = rb.Content.ToString();
                }
                else
                {
                    serviceType = "";
                }

            }
            return serviceType;
        }
        #endregion Service Priority

        private void ButtonExpress1_Click(object sender, RoutedEventArgs e)
        {
            if(expressQueue.Count != 0)
            {
                finishedList.Add(expressQueue.Dequeue());
                ListViewExpress.Items.RemoveAt(0);
                DisplayList();   
            }
            DisplayExpressQueue();
        }

        private void ButtonRegular1_Click(object sender, RoutedEventArgs e)
        {
            if(regularQueue.Count != 0)
            {
               finishedList.Add(regularQueue.Dequeue());
               ListViewRegular.Items.RemoveAt(0);
               DisplayList();
            }
            DisplayRegularQueue();
        }
        public void ClearTextBox()
        {
            TextBoxName.Text.Equals("");
            TextBoxCost.Text.Equals("");
            TextBoxProblem.Text.Equals("");
            TextBoxModel.Text.Equals("");
        }

        private void ListViewExpress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Drone selected = (Drone)ListViewExpress.SelectedItem;
                       
            
        }
    }

}
