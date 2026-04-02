using ContactManager.ViewModels;
using ContactManager.Views;
using System.Windows;

namespace ContactManager
{
    /// <summary>
    /// Gắn kết View (MainWindow) với ViewModel (ContactViewModel)
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // ===== SETUP DATACONTEXT =====
            // Gán ViewModel làm DataContext để tất cả Binding trong XAML hoạt động
            DataContext = new ContactViewModel();
        }

        // ===== Mở cửa sổ lịch sử cuộc gọi =====
        private void OpenCallHistory_Click(object sender, RoutedEventArgs e)
        {
            var window = new CallHistoryWindow();
            window.Owner = this;
            window.ShowDialog();
        }
    }
}