using System;
using System.Drawing;
using System.Windows.Forms;

namespace GestaoOS.WinForms
{
    internal static class UiStyle
    {
        public static readonly Color AppBackground = Color.FromArgb(245, 246, 248);
        public static readonly Color PanelBackground = Color.White;
        public static readonly Color GridHeaderBackground = Color.FromArgb(238, 241, 245);
        public static readonly Color GridSelection = Color.FromArgb(0, 120, 215);

        public static void ApplyForm(Form form, Size size, Size minimumSize)
        {
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            form.BackColor = AppBackground;
            form.Size = size;
            form.MinimumSize = minimumSize;
            form.Padding = new Padding(10);
            ApplyApplicationIcon(form);
        }

        public static Button Button(string text, Action action)
        {
            return Button(text, action, 96);
        }

        public static Button Button(string text, Action action, int width)
        {
            var button = new Button
            {
                Text = text,
                Width = width,
                Height = 30,
                Margin = new Padding(4),
                FlatStyle = FlatStyle.System,
                UseVisualStyleBackColor = true
            };
            button.Click += (s, e) => action();
            return button;
        }

        public static Button PagerButton(string text, Action action)
        {
            var button = Button(text, action, 96);
            button.Margin = new Padding(4, 2, 4, 8);
            return button;
        }

        public static GroupBox GroupBox(string text)
        {
            return new GroupBox
            {
                Text = text,
                Dock = DockStyle.Fill,
                BackColor = PanelBackground,
                Padding = new Padding(10, 8, 10, 10)
            };
        }

        public static Label Label(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(3, 3, 6, 3)
            };
        }

        public static T Fill<T>(T control) where T : Control
        {
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(3, 3, 8, 3);
            return control;
        }

        public static void ConfigureDigitsOnly(TextBox textBox, int maxLength)
        {
            textBox.MaxLength = maxLength;
            textBox.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }

        public static void ConfigureNoSpaces(TextBox textBox)
        {
            textBox.KeyPress += (s, e) =>
            {
                if (!char.IsControl(e.KeyChar) && char.IsWhiteSpace(e.KeyChar))
                {
                    e.Handled = true;
                }
            };
        }

        public static string DigitsOnly(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var buffer = new char[value.Length];
            var index = 0;
            foreach (var ch in value)
            {
                if (char.IsDigit(ch))
                {
                    buffer[index++] = ch;
                }
            }

            return new string(buffer, 0, index);
        }

        public static void ConfigureGrid(DataGridView grid)
        {
            grid.Dock = DockStyle.Fill;
            grid.AutoGenerateColumns = false;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = PanelBackground;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBackground;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
            grid.ColumnHeadersHeight = 32;
            grid.RowTemplate.Height = 28;
            grid.DefaultCellStyle.SelectionBackColor = GridSelection;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
            grid.DataError += GridDataError;
        }

        public static DataGridViewTextBoxColumn TextColumn(string propertyName, string headerText, int fillWeight)
        {
            return TextColumn(propertyName, headerText, fillWeight, null, DataGridViewContentAlignment.MiddleLeft);
        }

        public static DataGridViewTextBoxColumn TextColumn(
            string propertyName,
            string headerText,
            int fillWeight,
            string format,
            DataGridViewContentAlignment alignment)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = propertyName,
                HeaderText = headerText,
                FillWeight = fillWeight,
                MinimumWidth = Math.Min(70, Math.Max(45, fillWeight))
            };
            column.DefaultCellStyle.Alignment = alignment;
            column.DefaultCellStyle.Format = format;
            column.DefaultCellStyle.NullValue = string.Empty;
            return column;
        }

        public static void AddField(TableLayoutPanel panel, string label, Control control, int col, int row)
        {
            panel.Controls.Add(Label(label), col, row);
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(3, 3, 8, 3);
            panel.Controls.Add(control, col + 1, row);
        }

        public static void AddField(TableLayoutPanel panel, string label, Control control, int row)
        {
            panel.Controls.Add(Label(label), 0, row);
            control.Dock = DockStyle.Fill;
            control.Margin = new Padding(3, 3, 8, 3);
            panel.Controls.Add(control, 1, row);
        }

        private static void GridDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = true;
        }

        private static void ApplyApplicationIcon(Form form)
        {
            try
            {
                var icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
                if (icon != null)
                {
                    form.Icon = icon;
                }
            }
            catch
            {
                // The default Windows Forms icon remains available if extraction fails.
            }
        }
    }
}
