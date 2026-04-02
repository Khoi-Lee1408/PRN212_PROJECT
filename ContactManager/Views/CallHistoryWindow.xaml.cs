using ContactManager.ViewModels;
using System.Windows;

namespace ContactManager.Views
{
    /// <summary>
    /// Gắn kết CallHistoryWindow với CallHistoryViewModel
    /// </summary>
    public partial class CallHistoryWindow : Window
    {
        public CallHistoryWindow(int contactId = -1)
        {
            InitializeComponent();
            // ===== SETUP DATACONTEXT =====
            DataContext = new CallHistoryViewModel(contactId);
        }
    }
}
