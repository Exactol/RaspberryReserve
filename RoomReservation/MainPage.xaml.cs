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
            room.reservations.Add(new Reservation(new DateTime(2017, 9, 10, 5, 52, 0), "Kieran Huang", 23, 15));
            room.reservations.Add(new Reservation(new DateTime(2017, 9, 10, 15, 32, 0), "Mokshat Sood", 23, 30));
            room.reservations.Add(new Reservation(new DateTime(2017, 9, 10, 8, 10, 0), "Amy Eng", 23, 50));
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

        private void ReserveClicked(object sender, RoutedEventArgs e)
        {
            ReservePopup.IsOpen = true;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            ReserveText.Text="";
            ReserveCalender.Date = DateTimeOffset.Now;
            ReserveTime.Time = TimeSpan.Zero;
            ReserveLength.Text = "";
            ReservePopup.IsOpen = false;
        }

        private void RegisterClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ReserveText.Text))
                return;
            if (!ReserveCalender.Date.HasValue)
                return;
            if (string.IsNullOrEmpty(ReserveLength.Text))
                return;
            ReservePopup.IsOpen = false;

            DateTimeOffset calenderVal = ReserveCalender.Date.Value;

            DateTime date = new DateTime(calenderVal.Year, calenderVal.Month, calenderVal.Day, ReserveTime.Time.Hours, ReserveTime.Time.Minutes, ReserveTime.Time.Seconds);

            int roomNumber;
            Int32.TryParse(room.RoomNumber, out roomNumber);
            int reserveLength;
            Int32.TryParse(ReserveLength.Text, out reserveLength);

            room.reservations.Add(new Reservation(date, ReserveText.Text, roomNumber, reserveLength));
            room.reservations = room.reservations.OrderByDescending(x => x.Time.Hour).Reverse().ToList();

            InfoBox.Items.Clear();

            ReserveText.Text = "";
            ReserveCalender.Date = DateTimeOffset.Now;
            ReserveTime.Time = TimeSpan.Zero;
            ReserveLength.Text = "";
            ReservePopup.IsOpen = false;

            foreach (var roomReservation in room.reservations)
            {
                InfoBox.Items.Add(roomReservation);
            }
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
        public int Duration { get; private set; }

        public Reservation(DateTime _time, string _name, int _roomNum, int _duration)
        {
            Time = _time;
            Name = _name;
            RoomNumber = _roomNum;
            TimeFormatted = string.Format("{0:hh:mm tt}", Time);
            Duration = _duration;
        }
    }
}
