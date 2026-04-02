using ContactManager.Models;
using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ContactManager.ViewModels
{
    /// <summary>
    /// ViewModel cho cửa sổ lịch sử cuộc gọi
    /// </summary>
    public class CallHistoryViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // Dữ liệu hiển thị trên DataGrid
        private ObservableCollection<CallHistoryRow> _callLogs = new();
        public ObservableCollection<CallHistoryRow> CallLogs
        {
            get => _callLogs;
            set { _callLogs = value; OnPropertyChanged(); }
        }

        // Lọc theo 1 ContactId cụ thể (-1 = hiển thị tất cả)
        public CallHistoryViewModel(int contactId = -1)
        {
            LoadData(contactId);
        }

        private void LoadData(int contactId)
        {
            using var db = new ContactManagementDbContext();

            // Join CallHistory với Contact để lấy tên + số điện thoại
            var query = db.CallHistories
                .Join(db.Contacts,
                      ch => ch.ContactId,
                      c  => c.Id,
                      (ch, c) => new CallHistoryRow
                      {
                          Name     = c.FullName,
                          Phone    = c.PhoneNumber,
                          CallDate = ch.CallDate,
                          CallTime = ch.CallTime,
                          Duration = ch.Duration,
                          CallType = ch.CallType,
                      });

            if (contactId > 0)
                query = query.Where(r => r.Name != null); // Placeholder – có thể filter thêm

            CallLogs = new ObservableCollection<CallHistoryRow>(query.ToList());
        }
    }

    // ===== Row model để hiển thị trên DataGrid =====
    public class CallHistoryRow
    {
        public string Name     { get; set; } = string.Empty;
        public string Phone    { get; set; } = string.Empty;
        public DateOnly CallDate { get; set; }
        public TimeOnly CallTime { get; set; }
        public int Duration    { get; set; }
        public string CallType { get; set; } = string.Empty;

        // Định dạng cho cột Time display
        public string CallTimeDisplay => $"{CallDate:dd/MM/yyyy} {CallTime:HH:mm}";
        public string DurationDisplay => Duration > 0 ? $"{Duration}s" : "Missed";
    }
}
