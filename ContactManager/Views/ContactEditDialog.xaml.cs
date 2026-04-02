using ContactManager.Models;
using System.Windows;

namespace ContactManager
{
    /// <summary>
    /// Dialog để thêm / sửa liên hệ
    /// </summary>
    public partial class ContactEditDialog : Window
    {
        // Kết quả trả về cho ViewModel sau khi user bấm Save
        public Contact Result { get; private set; }

        public ContactEditDialog(Contact contact, bool isNew)
        {
            InitializeComponent();

            Result = contact;

            // Tiêu đề tuỳ theo Add hay Edit
            lblTitle.Text = isNew ? "➕ Thêm liên hệ mới" : "✏ Chỉnh sửa liên hệ";

            // Điền sẵn dữ liệu vào form
            txtFullName.Text  = contact.FullName;
            txtPhone.Text     = contact.PhoneNumber;
            txtEmail.Text     = contact.Email ?? string.Empty;
            txtAddress.Text   = contact.Address ?? string.Empty;
            txtCategory.Text  = contact.Category ?? string.Empty;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Ghi dữ liệu từ form vào Result
            Result.FullName    = txtFullName.Text.Trim();
            Result.PhoneNumber = txtPhone.Text.Trim();
            Result.Email       = txtEmail.Text.Trim();
            Result.Address     = txtAddress.Text.Trim();
            Result.Category    = txtCategory.Text.Trim();

            DialogResult = true; // Báo cho ViewModel biết user đã bấm Save
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
