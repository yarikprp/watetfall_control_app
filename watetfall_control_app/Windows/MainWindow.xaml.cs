using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Threading.Tasks;
using watetfall_control_app.Classes;
using System.Windows.Media;

namespace watetfall_control_app
{
    public partial class MainWindow : Window
    {
        private readonly WaterfallDbContext _dbContext;

        public MainWindow()
        {
            InitializeComponent();
            _dbContext = new WaterfallDbContext();
        }

        private void ValidateTicket_Click(object sender, RoutedEventArgs e)
        {
            string ticketNumber = TicketNumberTextBox.Text.Trim();
            ValidateAndDisplayTicket(ticketNumber);
        }

        private async void ScanTicket_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBlock.Text = "Сканирование...";
            await Task.Delay(2000);

            string scannedTicketNumber = "12345678";
            ResultTextBlock.Text = $"Сканированный билет: {scannedTicketNumber}";

            ValidateAndDisplayTicket(scannedTicketNumber);
        }

        private void ValidateAndDisplayTicket(string ticketNumber)
        {
            if (string.IsNullOrWhiteSpace(ticketNumber))
            {
                MessageBox.Show("Заполните поле с номером билета");
                return;
            }

            var ticket = _dbContext.Tickets
                .Include(t => t.IdSheduleNavigation)
                .FirstOrDefault(t => t.NumberTicket == ticketNumber);

            if (ticket == null)
            {
                ResultTextBlock.Text = "Проход запрещен";
                ResultTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                return;
            }

            DateTime entranceTime = ticket.IdSheduleNavigation.EntranceTime;

            if (entranceTime < DateTime.Now.AddMinutes(-30))
            {
                ResultTextBlock.Text = "Проход запрещен";
                ResultTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (entranceTime > DateTime.Now.AddMinutes(10))
            {
                ResultTextBlock.Text = "Проход запрещен";
                ResultTextBlock.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                ResultTextBlock.Text = "Проход разрешен";
                ResultTextBlock.Foreground = new SolidColorBrush(Colors.Green); 
            }
        }
    }
}
