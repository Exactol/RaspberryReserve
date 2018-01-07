using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RoomReservation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Room room;

        public MainPage()
        {
            room = new Room(532, 25, "Marketing");
            room.reservations = new List<Reservation>();
            room.reservations.Add(new Reservation(new DateTime(2017, 9, 10, 5, 52, 0), "Kieran Huang", 23));
            room.reservations.Add(new Reservation(new DateTime(2017, 9, 10, 15, 32, 0), "Mokshat Sood", 23));
            room.reservations.Add(new Reservation(new DateTime(2017, 9, 10, 8, 10, 0), "Amy Eng", 23));
            room.reservations = room.reservations.OrderByDescending(x => x.Time.Hour).Reverse().ToList();
            InitializeComponent();
        }

        private void Loaded(object sender, RoutedEventArgs e)
        {

            RoomNumberBlock.Text += $"\n{room.RoomNumber}";
            if (!String.IsNullOrEmpty(room.RoomName))
                RoomNumberBlock.Text += $"\n{room.RoomName}";
            
            CapacityBlock.Text += $"\n{room.Capacity}";

            //Add reservations to list
            foreach (Reservation reservation in room.reservations)
            {
                InfoBox.Items.Add(reservation);
            }

            
        }

        //public SolidColorBrush GetSolidColorBrush(string hex)
        //{
        //    hex = hex.Replace("#", string.Empty);
        //    byte r = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
        //    byte g = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
        //    byte b = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
        //    SolidColorBrush myBrush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
        //    return myBrush;
        //}
        private void ReserveClicked(object sender, RoutedEventArgs e)
        {
            ReservePopup.IsOpen = true;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            
        }

        private void RegisterClicked(object sender, RoutedEventArgs e)
        {
            
        }
    }

    public class Room
    {
        public List<Reservation> reservations;
        public string RoomNumber;
        public int Capacity;
        public string RoomName;

        public Room(int _roomNumber, int _capacity, string _roomName = "")
        {
            RoomNumber = _roomNumber.ToString();
            Capacity = _capacity;
            RoomName = _roomName;
        }

        public Room(string _roomNumber, int _capacity, string _roomName = "")
        {
            RoomNumber = _roomNumber;
            Capacity = _capacity;
            RoomName = _roomName;
        }
    }

    public class Reservation
    {
        public DateTime Time { get; private set; }
        public string TimeFormatted { get; private set; }
        public string Name { get; private set; }
        public int RoomNumber { get; private set; }

        public Reservation(DateTime _time, string _name, int _roomNum)
        {
            Time = _time;
            Name = _name;
            RoomNumber = _roomNum;
            TimeFormatted = string.Format("{0:HH:mm tt}", Time);

        }
    }
}
