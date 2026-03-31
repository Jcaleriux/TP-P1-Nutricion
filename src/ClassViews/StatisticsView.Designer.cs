namespace ClassViews
{
    partial class StatisticsView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblDailySectionTitle = new Label();
            lblSelectedDateTitle = new Label();
            dtpSelectedDate = new DateTimePicker();
            btnLoadDaily = new Button();
            lblDailyConsumedTitle = new Label();
            lblDailyConsumedValue = new Label();
            lblDailyGoalsTitle = new Label();
            lblDailyGoalsValue = new Label();
            lblDailyDifferenceTitle = new Label();
            lblDailyDifferenceValue = new Label();
            lblDailyStatusTitle = new Label();
            lblDailyStatusValue = new Label();
            lblTodayProgressTitle = new Label();
            lblTodayProgressValue = new Label();
            lblRangeSectionTitle = new Label();
            lblStartDateTitle = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDateTitle = new Label();
            dtpEndDate = new DateTimePicker();
            btnLoadRange = new Button();
            btnCurrentMonth = new Button();
            lblDaysWithMenusTitle = new Label();
            lblDaysWithMenusValue = new Label();
            lblDaysGoalMetTitle = new Label();
            lblDaysGoalMetValue = new Label();
            lblDaysGoalNotMetTitle = new Label();
            lblDaysGoalNotMetValue = new Label();
            lblAverageCaloriesTitle = new Label();
            lblAverageCaloriesValue = new Label();
            lblAverageMacrosTitle = new Label();
            lblAverageMacrosValue = new Label();
            dgvRangeStatistics = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvRangeStatistics).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(334, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(341, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Nutrition Statistics Overview";
            // 
            // lblDailySectionTitle
            // 
            lblDailySectionTitle.AutoSize = true;
            lblDailySectionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDailySectionTitle.Location = new Point(33, 63);
            lblDailySectionTitle.Name = "lblDailySectionTitle";
            lblDailySectionTitle.Size = new Size(122, 21);
            lblDailySectionTitle.TabIndex = 1;
            lblDailySectionTitle.Text = "Daily Statistics";
            // 
            // lblSelectedDateTitle
            // 
            lblSelectedDateTitle.AutoSize = true;
            lblSelectedDateTitle.Location = new Point(33, 98);
            lblSelectedDateTitle.Name = "lblSelectedDateTitle";
            lblSelectedDateTitle.Size = new Size(78, 15);
            lblSelectedDateTitle.TabIndex = 2;
            lblSelectedDateTitle.Text = "Selected Date";
            // 
            // dtpSelectedDate
            // 
            dtpSelectedDate.Location = new Point(147, 94);
            dtpSelectedDate.Margin = new Padding(3, 2, 3, 2);
            dtpSelectedDate.Name = "dtpSelectedDate";
            dtpSelectedDate.Size = new Size(241, 23);
            dtpSelectedDate.TabIndex = 3;
            // 
            // btnLoadDaily
            // 
            btnLoadDaily.Location = new Point(405, 94);
            btnLoadDaily.Margin = new Padding(3, 2, 3, 2);
            btnLoadDaily.Name = "btnLoadDaily";
            btnLoadDaily.Size = new Size(121, 22);
            btnLoadDaily.TabIndex = 4;
            btnLoadDaily.Text = "Load Daily Stats";
            btnLoadDaily.UseVisualStyleBackColor = true;
            btnLoadDaily.Click += btnLoadDaily_Click;
            // 
            // lblDailyConsumedTitle
            // 
            lblDailyConsumedTitle.AutoSize = true;
            lblDailyConsumedTitle.Location = new Point(33, 134);
            lblDailyConsumedTitle.Name = "lblDailyConsumedTitle";
            lblDailyConsumedTitle.Size = new Size(109, 15);
            lblDailyConsumedTitle.TabIndex = 5;
            lblDailyConsumedTitle.Text = "Daily Consumption";
            // 
            // lblDailyConsumedValue
            // 
            lblDailyConsumedValue.AutoSize = true;
            lblDailyConsumedValue.Location = new Point(182, 134);
            lblDailyConsumedValue.MaximumSize = new Size(788, 0);
            lblDailyConsumedValue.Name = "lblDailyConsumedValue";
            lblDailyConsumedValue.Size = new Size(12, 15);
            lblDailyConsumedValue.TabIndex = 6;
            lblDailyConsumedValue.Text = "-";
            // 
            // lblDailyGoalsTitle
            // 
            lblDailyGoalsTitle.AutoSize = true;
            lblDailyGoalsTitle.Location = new Point(33, 161);
            lblDailyGoalsTitle.Name = "lblDailyGoalsTitle";
            lblDailyGoalsTitle.Size = new Size(65, 15);
            lblDailyGoalsTitle.TabIndex = 7;
            lblDailyGoalsTitle.Text = "Daily Goals";
            // 
            // lblDailyGoalsValue
            // 
            lblDailyGoalsValue.AutoSize = true;
            lblDailyGoalsValue.Location = new Point(182, 161);
            lblDailyGoalsValue.MaximumSize = new Size(788, 0);
            lblDailyGoalsValue.Name = "lblDailyGoalsValue";
            lblDailyGoalsValue.Size = new Size(12, 15);
            lblDailyGoalsValue.TabIndex = 8;
            lblDailyGoalsValue.Text = "-";
            // 
            // lblDailyDifferenceTitle
            // 
            lblDailyDifferenceTitle.AutoSize = true;
            lblDailyDifferenceTitle.Location = new Point(33, 187);
            lblDailyDifferenceTitle.Name = "lblDailyDifferenceTitle";
            lblDailyDifferenceTitle.Size = new Size(110, 15);
            lblDailyDifferenceTitle.TabIndex = 9;
            lblDailyDifferenceTitle.Text = "Difference vs. Goals";
            // 
            // lblDailyDifferenceValue
            // 
            lblDailyDifferenceValue.AutoSize = true;
            lblDailyDifferenceValue.Location = new Point(182, 187);
            lblDailyDifferenceValue.MaximumSize = new Size(788, 0);
            lblDailyDifferenceValue.Name = "lblDailyDifferenceValue";
            lblDailyDifferenceValue.Size = new Size(12, 15);
            lblDailyDifferenceValue.TabIndex = 10;
            lblDailyDifferenceValue.Text = "-";
            // 
            // lblDailyStatusTitle
            // 
            lblDailyStatusTitle.AutoSize = true;
            lblDailyStatusTitle.Location = new Point(33, 216);
            lblDailyStatusTitle.Name = "lblDailyStatusTitle";
            lblDailyStatusTitle.Size = new Size(68, 15);
            lblDailyStatusTitle.TabIndex = 11;
            lblDailyStatusTitle.Text = "Daily Status";
            // 
            // lblDailyStatusValue
            // 
            lblDailyStatusValue.AutoSize = true;
            lblDailyStatusValue.Location = new Point(182, 216);
            lblDailyStatusValue.Name = "lblDailyStatusValue";
            lblDailyStatusValue.Size = new Size(12, 15);
            lblDailyStatusValue.TabIndex = 12;
            lblDailyStatusValue.Text = "-";
            // 
            // lblTodayProgressTitle
            // 
            lblTodayProgressTitle.AutoSize = true;
            lblTodayProgressTitle.Location = new Point(33, 242);
            lblTodayProgressTitle.Name = "lblTodayProgressTitle";
            lblTodayProgressTitle.Size = new Size(87, 15);
            lblTodayProgressTitle.TabIndex = 13;
            lblTodayProgressTitle.Text = "Today Progress";
            // 
            // lblTodayProgressValue
            // 
            lblTodayProgressValue.AutoSize = true;
            lblTodayProgressValue.Location = new Point(182, 242);
            lblTodayProgressValue.MaximumSize = new Size(788, 0);
            lblTodayProgressValue.Name = "lblTodayProgressValue";
            lblTodayProgressValue.Size = new Size(12, 15);
            lblTodayProgressValue.TabIndex = 14;
            lblTodayProgressValue.Text = "-";
            // 
            // lblRangeSectionTitle
            // 
            lblRangeSectionTitle.AutoSize = true;
            lblRangeSectionTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRangeSectionTitle.Location = new Point(33, 290);
            lblRangeSectionTitle.Name = "lblRangeSectionTitle";
            lblRangeSectionTitle.Size = new Size(179, 21);
            lblRangeSectionTitle.TabIndex = 15;
            lblRangeSectionTitle.Text = "Range / Monthly Stats";
            // 
            // lblStartDateTitle
            // 
            lblStartDateTitle.AutoSize = true;
            lblStartDateTitle.Location = new Point(33, 327);
            lblStartDateTitle.Name = "lblStartDateTitle";
            lblStartDateTitle.Size = new Size(58, 15);
            lblStartDateTitle.TabIndex = 16;
            lblStartDateTitle.Text = "Start Date";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Location = new Point(147, 323);
            dtpStartDate.Margin = new Padding(3, 2, 3, 2);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(241, 23);
            dtpStartDate.TabIndex = 17;
            // 
            // lblEndDateTitle
            // 
            lblEndDateTitle.AutoSize = true;
            lblEndDateTitle.Location = new Point(33, 355);
            lblEndDateTitle.Name = "lblEndDateTitle";
            lblEndDateTitle.Size = new Size(54, 15);
            lblEndDateTitle.TabIndex = 18;
            lblEndDateTitle.Text = "End Date";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Location = new Point(147, 351);
            dtpEndDate.Margin = new Padding(3, 2, 3, 2);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(241, 23);
            dtpEndDate.TabIndex = 19;
            // 
            // btnLoadRange
            // 
            btnLoadRange.Location = new Point(405, 322);
            btnLoadRange.Margin = new Padding(3, 2, 3, 2);
            btnLoadRange.Name = "btnLoadRange";
            btnLoadRange.Size = new Size(129, 22);
            btnLoadRange.TabIndex = 20;
            btnLoadRange.Text = "Load Range Stats";
            btnLoadRange.UseVisualStyleBackColor = true;
            btnLoadRange.Click += btnLoadRange_Click;
            // 
            // btnCurrentMonth
            // 
            btnCurrentMonth.Location = new Point(405, 350);
            btnCurrentMonth.Margin = new Padding(3, 2, 3, 2);
            btnCurrentMonth.Name = "btnCurrentMonth";
            btnCurrentMonth.Size = new Size(129, 22);
            btnCurrentMonth.TabIndex = 21;
            btnCurrentMonth.Text = "Current Month";
            btnCurrentMonth.UseVisualStyleBackColor = true;
            btnCurrentMonth.Click += btnCurrentMonth_Click;
            // 
            // lblDaysWithMenusTitle
            // 
            lblDaysWithMenusTitle.AutoSize = true;
            lblDaysWithMenusTitle.Location = new Point(33, 395);
            lblDaysWithMenusTitle.Name = "lblDaysWithMenusTitle";
            lblDaysWithMenusTitle.Size = new Size(99, 15);
            lblDaysWithMenusTitle.TabIndex = 22;
            lblDaysWithMenusTitle.Text = "Days With Menus";
            // 
            // lblDaysWithMenusValue
            // 
            lblDaysWithMenusValue.AutoSize = true;
            lblDaysWithMenusValue.Location = new Point(182, 395);
            lblDaysWithMenusValue.Name = "lblDaysWithMenusValue";
            lblDaysWithMenusValue.Size = new Size(12, 15);
            lblDaysWithMenusValue.TabIndex = 23;
            lblDaysWithMenusValue.Text = "-";
            // 
            // lblDaysGoalMetTitle
            // 
            lblDaysGoalMetTitle.AutoSize = true;
            lblDaysGoalMetTitle.Location = new Point(33, 420);
            lblDaysGoalMetTitle.Name = "lblDaysGoalMetTitle";
            lblDaysGoalMetTitle.Size = new Size(83, 15);
            lblDaysGoalMetTitle.TabIndex = 24;
            lblDaysGoalMetTitle.Text = "Days Goal Met";
            // 
            // lblDaysGoalMetValue
            // 
            lblDaysGoalMetValue.AutoSize = true;
            lblDaysGoalMetValue.Location = new Point(182, 420);
            lblDaysGoalMetValue.Name = "lblDaysGoalMetValue";
            lblDaysGoalMetValue.Size = new Size(12, 15);
            lblDaysGoalMetValue.TabIndex = 25;
            lblDaysGoalMetValue.Text = "-";
            // 
            // lblDaysGoalNotMetTitle
            // 
            lblDaysGoalNotMetTitle.AutoSize = true;
            lblDaysGoalNotMetTitle.Location = new Point(33, 446);
            lblDaysGoalNotMetTitle.Name = "lblDaysGoalNotMetTitle";
            lblDaysGoalNotMetTitle.Size = new Size(106, 15);
            lblDaysGoalNotMetTitle.TabIndex = 26;
            lblDaysGoalNotMetTitle.Text = "Days Goal Not Met";
            // 
            // lblDaysGoalNotMetValue
            // 
            lblDaysGoalNotMetValue.AutoSize = true;
            lblDaysGoalNotMetValue.Location = new Point(182, 446);
            lblDaysGoalNotMetValue.Name = "lblDaysGoalNotMetValue";
            lblDaysGoalNotMetValue.Size = new Size(12, 15);
            lblDaysGoalNotMetValue.TabIndex = 27;
            lblDaysGoalNotMetValue.Text = "-";
            // 
            // lblAverageCaloriesTitle
            // 
            lblAverageCaloriesTitle.AutoSize = true;
            lblAverageCaloriesTitle.Location = new Point(33, 472);
            lblAverageCaloriesTitle.Name = "lblAverageCaloriesTitle";
            lblAverageCaloriesTitle.Size = new Size(95, 15);
            lblAverageCaloriesTitle.TabIndex = 28;
            lblAverageCaloriesTitle.Text = "Average Calories";
            // 
            // lblAverageCaloriesValue
            // 
            lblAverageCaloriesValue.AutoSize = true;
            lblAverageCaloriesValue.Location = new Point(182, 472);
            lblAverageCaloriesValue.Name = "lblAverageCaloriesValue";
            lblAverageCaloriesValue.Size = new Size(12, 15);
            lblAverageCaloriesValue.TabIndex = 29;
            lblAverageCaloriesValue.Text = "-";
            // 
            // lblAverageMacrosTitle
            // 
            lblAverageMacrosTitle.AutoSize = true;
            lblAverageMacrosTitle.Location = new Point(33, 500);
            lblAverageMacrosTitle.Name = "lblAverageMacrosTitle";
            lblAverageMacrosTitle.Size = new Size(92, 15);
            lblAverageMacrosTitle.TabIndex = 30;
            lblAverageMacrosTitle.Text = "Average Macros";
            // 
            // lblAverageMacrosValue
            // 
            lblAverageMacrosValue.AutoSize = true;
            lblAverageMacrosValue.Location = new Point(182, 500);
            lblAverageMacrosValue.MaximumSize = new Size(788, 0);
            lblAverageMacrosValue.Name = "lblAverageMacrosValue";
            lblAverageMacrosValue.Size = new Size(12, 15);
            lblAverageMacrosValue.TabIndex = 31;
            lblAverageMacrosValue.Text = "-";
            // 
            // dgvRangeStatistics
            // 
            dgvRangeStatistics.AllowUserToAddRows = false;
            dgvRangeStatistics.AllowUserToDeleteRows = false;
            dgvRangeStatistics.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRangeStatistics.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRangeStatistics.Location = new Point(675, 94);
            dgvRangeStatistics.Margin = new Padding(3, 2, 3, 2);
            dgvRangeStatistics.MultiSelect = false;
            dgvRangeStatistics.Name = "dgvRangeStatistics";
            dgvRangeStatistics.ReadOnly = true;
            dgvRangeStatistics.RowHeadersWidth = 51;
            dgvRangeStatistics.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRangeStatistics.Size = new Size(516, 421);
            dgvRangeStatistics.TabIndex = 32;
            // 
            // StatisticsView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1235, 543);
            Controls.Add(dgvRangeStatistics);
            Controls.Add(lblAverageMacrosValue);
            Controls.Add(lblAverageMacrosTitle);
            Controls.Add(lblAverageCaloriesValue);
            Controls.Add(lblAverageCaloriesTitle);
            Controls.Add(lblDaysGoalNotMetValue);
            Controls.Add(lblDaysGoalNotMetTitle);
            Controls.Add(lblDaysGoalMetValue);
            Controls.Add(lblDaysGoalMetTitle);
            Controls.Add(lblDaysWithMenusValue);
            Controls.Add(lblDaysWithMenusTitle);
            Controls.Add(btnCurrentMonth);
            Controls.Add(btnLoadRange);
            Controls.Add(dtpEndDate);
            Controls.Add(lblEndDateTitle);
            Controls.Add(dtpStartDate);
            Controls.Add(lblStartDateTitle);
            Controls.Add(lblRangeSectionTitle);
            Controls.Add(lblTodayProgressValue);
            Controls.Add(lblTodayProgressTitle);
            Controls.Add(lblDailyStatusValue);
            Controls.Add(lblDailyStatusTitle);
            Controls.Add(lblDailyDifferenceValue);
            Controls.Add(lblDailyDifferenceTitle);
            Controls.Add(lblDailyGoalsValue);
            Controls.Add(lblDailyGoalsTitle);
            Controls.Add(lblDailyConsumedValue);
            Controls.Add(lblDailyConsumedTitle);
            Controls.Add(btnLoadDaily);
            Controls.Add(dtpSelectedDate);
            Controls.Add(lblSelectedDateTitle);
            Controls.Add(lblDailySectionTitle);
            Controls.Add(lblTitle);
            Margin = new Padding(3, 2, 3, 2);
            Name = "StatisticsView";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Nutrition Statistics";
            Load += StatisticsView_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRangeStatistics).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblDailySectionTitle;
        private Label lblSelectedDateTitle;
        private DateTimePicker dtpSelectedDate;
        private Button btnLoadDaily;
        private Label lblDailyConsumedTitle;
        private Label lblDailyConsumedValue;
        private Label lblDailyGoalsTitle;
        private Label lblDailyGoalsValue;
        private Label lblDailyDifferenceTitle;
        private Label lblDailyDifferenceValue;
        private Label lblDailyStatusTitle;
        private Label lblDailyStatusValue;
        private Label lblTodayProgressTitle;
        private Label lblTodayProgressValue;
        private Label lblRangeSectionTitle;
        private Label lblStartDateTitle;
        private DateTimePicker dtpStartDate;
        private Label lblEndDateTitle;
        private DateTimePicker dtpEndDate;
        private Button btnLoadRange;
        private Button btnCurrentMonth;
        private Label lblDaysWithMenusTitle;
        private Label lblDaysWithMenusValue;
        private Label lblDaysGoalMetTitle;
        private Label lblDaysGoalMetValue;
        private Label lblDaysGoalNotMetTitle;
        private Label lblDaysGoalNotMetValue;
        private Label lblAverageCaloriesTitle;
        private Label lblAverageCaloriesValue;
        private Label lblAverageMacrosTitle;
        private Label lblAverageMacrosValue;
        private DataGridView dgvRangeStatistics;
    }
}
