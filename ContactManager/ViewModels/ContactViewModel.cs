using ContactManager.Models;
using Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ContactManager.ViewModels
{
    // ===================================================
    //  Base helpers
    // ===================================================
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool>? _canExecute;
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        { _execute = execute; _canExecute = canExecute; }
        public bool CanExecute(object? p) => _canExecute?.Invoke(p) ?? true;
        public void Execute(object? p) => _execute(p);
        public event EventHandler? CanExecuteChanged
        {
            add    => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    // ===================================================
    //  Main ViewModel
    // ===================================================
    public class ContactViewModel : BaseViewModel
    {
        // true = đang nhập Contact mới (chưa có trong DB)
        private bool _isNewContact = false;

        // Danh sách gốc (tất cả từ DB)
        private List<Contact> _allContacts = new();

        // ── Danh sách hiển thị (đã lọc) ──
        private ObservableCollection<Contact> _contactList = new();
        public ObservableCollection<Contact> ContactList
        {
            get => _contactList;
            set { _contactList = value; OnPropertyChanged(); }
        }

        // ── Contact đang chọn / đang chỉnh sửa ──
        private Contact? _selectedContact;
        public Contact? SelectedContact
        {
            get => _selectedContact;
            set { _selectedContact = value; OnPropertyChanged(); }
        }

        // ── Ô tìm kiếm ──
        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(); ApplySearch(); }
        }

        // ── Status bar ──
        private string _statusText = "✅ Sẵn sàng";
        public string StatusText
        {
            get => _statusText;
            set { _statusText = value; OnPropertyChanged(); }
        }

        // ── Tổng số liên hệ ──
        public int TotalContacts => _allContacts.Count;

        // ── Commands ──
        public ICommand AddCommand    { get; }
        public ICommand SaveCommand   { get; }
        public ICommand DeleteCommand { get; }
        public ICommand CancelCommand { get; }

        // ===================================================
        public ContactViewModel()
        {
            AddCommand    = new RelayCommand(_ => PrepareAdd());
            SaveCommand   = new RelayCommand(_ => SaveContact(),   _ => SelectedContact != null);
            DeleteCommand = new RelayCommand(_ => DeleteContact(), _ => SelectedContact != null && !_isNewContact);
            CancelCommand = new RelayCommand(_ => CancelEdit(),    _ => _isNewContact);

            LoadContacts();
        }

        // ── Load danh sách từ DB ──
        private void LoadContacts()
        {
            using var db = new ContactManagementDbContext();
            _allContacts = db.Contacts.ToList();
            ApplySearch();
            OnPropertyChanged(nameof(TotalContacts));
        }

        // ── Lọc theo SearchText (theo Tên hoặc SĐT) ──
        private void ApplySearch()
        {
            if (string.IsNullOrWhiteSpace(_searchText))
            {
                ContactList = new ObservableCollection<Contact>(_allContacts);
                StatusText  = $"✅ Hiển thị tất cả {_allContacts.Count} liên hệ";
            }
            else
            {
                var kw = _searchText.Trim().ToLower();
                var filtered = _allContacts.Where(c =>
                    c.FullName.ToLower().Contains(kw) ||
                    (c.PhoneNumber?.Contains(kw) == true)).ToList();
                ContactList = new ObservableCollection<Contact>(filtered);
                StatusText  = $"🔍 Tìm thấy {filtered.Count} kết quả cho \"{_searchText.Trim()}\"";
            }
        }

        // ── Validate dữ liệu đầu vào ──
        private string? Validate(Contact c)
        {
            if (string.IsNullOrWhiteSpace(c.FullName))
                return "❌ Họ và Tên không được để trống!";

            if (string.IsNullOrWhiteSpace(c.PhoneNumber))
                return "❌ Số điện thoại không được để trống!";

            // Chỉ cho phép chữ số, +, -, khoảng trắng
            if (!Regex.IsMatch(c.PhoneNumber.Trim(), @"^[\d\+\-\s]+$"))
                return "❌ Số điện thoại không hợp lệ!\n   Chỉ được nhập chữ số, dấu '+', '-' và khoảng trắng.";

            // Đếm số ký tự số thực sự
            var digits = Regex.Replace(c.PhoneNumber, @"[^\d]", "");
            if (digits.Length < 9)
                return "❌ Số điện thoại quá ngắn! (Tối thiểu 9 chữ số)";

            if (!string.IsNullOrWhiteSpace(c.Email) && !c.Email.Contains('@'))
                return "❌ Địa chỉ Email không hợp lệ!\n   Email phải chứa dấu '@'.";

            return null; // OK
        }

        // ── "Thêm mới": tạo contact rỗng, user điền vào right panel ──
        private void PrepareAdd()
        {
            _isNewContact  = true;
            SelectedContact = new Contact
            {
                FullName    = string.Empty,
                PhoneNumber = string.Empty,
                Email       = string.Empty,
                Address     = string.Empty,
                Category    = string.Empty,
            };
            StatusText = "📝 Đang nhập liên hệ mới — Điền thông tin rồi bấm 💾 Lưu thay đổi";
        }

        // ── "Hủy": thoát chế độ thêm mới ──
        private void CancelEdit()
        {
            _isNewContact   = false;
            SelectedContact = null;
            StatusText      = "⛔ Đã hủy thao tác thêm mới";
        }

        // ── "Lưu thay đổi": Validate rồi INSERT hoặc UPDATE ──
        private void SaveContact()
        {
            if (SelectedContact == null) return;

            // Trim các field trước khi validate
            SelectedContact.FullName    = SelectedContact.FullName?.Trim() ?? string.Empty;
            SelectedContact.PhoneNumber = SelectedContact.PhoneNumber?.Trim() ?? string.Empty;
            SelectedContact.Email       = SelectedContact.Email?.Trim() ?? string.Empty;
            SelectedContact.Address     = SelectedContact.Address?.Trim();
            SelectedContact.Category    = SelectedContact.Category?.Trim();

            var error = Validate(SelectedContact);
            if (error != null)
            {
                MessageBox.Show(error, "⚠ Dữ liệu không hợp lệ",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using var db = new ContactManagementDbContext();

            if (_isNewContact)
            {
                // ── INSERT ──
                db.Contacts.Add(SelectedContact);
                db.SaveChanges();
                // Lưu Id TRƯỚC khi LoadContacts() thay List mới
                var newId = SelectedContact.Id;
                _isNewContact = false;
                LoadContacts();

                // Giữ selection về contact vừa thêm
                SelectedContact = _allContacts.FirstOrDefault(c => c.Id == newId);
                StatusText = $"✅ Đã thêm liên hệ \"{SelectedContact?.FullName}\" thành công!";

                MessageBox.Show($"✅ Thêm liên hệ thành công!\n\nTên: {SelectedContact?.FullName}",
                    "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // ── UPDATE ──
                var tracked = db.Contacts.Find(SelectedContact.Id);
                if (tracked == null)
                {
                    MessageBox.Show("❌ Không tìm thấy liên hệ này trong CSDL!", "Lỗi",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                tracked.FullName    = SelectedContact.FullName;
                tracked.PhoneNumber = SelectedContact.PhoneNumber;
                tracked.Email       = SelectedContact.Email ?? string.Empty;
                tracked.Address     = SelectedContact.Address;
                tracked.Category    = SelectedContact.Category;
                db.SaveChanges();

                var savedName = tracked.FullName;
                LoadContacts();
                SelectedContact = _allContacts.FirstOrDefault(c => c.Id == tracked.Id);
                StatusText = $"✅ Đã cập nhật liên hệ \"{savedName}\" thành công!";

                MessageBox.Show($"✅ Cập nhật thành công!\n\nTên: {savedName}",
                    "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // ── "Xóa": xác nhận rồi DELETE ──
        private void DeleteContact()
        {
            if (SelectedContact == null) return;

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa liên hệ sau?\n\n" +
                $"   👤 Tên:  {SelectedContact.FullName}\n" +
                $"   📞 SĐT: {SelectedContact.PhoneNumber}\n\n" +
                $"⚠ Hành động này KHÔNG thể hoàn tác!",
                "Xác nhận xóa liên hệ",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm != MessageBoxResult.Yes) return;

            using var db = new ContactManagementDbContext();
            var entity = db.Contacts.Find(SelectedContact.Id);
            if (entity == null) return;

            var deletedName = entity.FullName;
            db.Contacts.Remove(entity);
            db.SaveChanges();

            SelectedContact = null;
            LoadContacts();
            StatusText = $"🗑 Đã xóa liên hệ \"{deletedName}\" thành công!";

            MessageBox.Show($"🗑 Đã xóa liên hệ \"{deletedName}\" thành công!",
                "Hoàn tất", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
