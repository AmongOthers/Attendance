using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attendance
{
    //TODO 处理加班和节假日调整
    public class MonthRecord
    {
        private List<AttendanceRecord> mRecords = new List<AttendanceRecord>();
        private List<LateRecord> mLateRecords = new List<LateRecord>();
        private List<AbsenteeismRecord> mAbsenteeismRecords = new List<AbsenteeismRecord>();
        private List<EarlyLeaveRecord> mEarlyLeaveRecords = new List<EarlyLeaveRecord>();
        private List<OvertimeRecord> mOvertimeRecords = new List<OvertimeRecord>();

        public int Year { get; private set; }

        public int Month { get; private set; }

        public int Workdays { get; private set; }

        public void Add(AttendanceRecord record)
        {
            if (mRecords.Count == 0)
            {
                Year = record.Date.Year;
                Month = record.Date.Month;
                Workdays = getWorkdays();
            }
            mRecords.Add(record);
        }

        public int getWorkdays()
        {
            String dateStr = String.Format("{0}-{1}-01 00:00:00", Year, Month);
            DateTime date = DateTime.Parse(dateStr);
            int count = 0;
            int i = 0;
            while (true)
            {
                var newDate = date.AddDays(i);
                if (newDate.Month != Month)
                {
                    break;
                }
                else if (newDate.DayOfWeek != DayOfWeek.Saturday && newDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    count++;
                }
                i++;
            }
            return count;
        }

        public List<LateRecord> LateRecords
        {
            get
            {
                return mLateRecords;
            }
        }

        public List<AbsenteeismRecord> AbsenteeismRecords
        {
            get
            {
                return mAbsenteeismRecords;
            }
        }

        public List<EarlyLeaveRecord> EarlyLeaveRecords
        {
            get
            {
                return mEarlyLeaveRecords;
            }
        }

        public List<OvertimeRecord> OvertimeRecords
        {
            get
            {
                return mOvertimeRecords;
            }
        }
    }
}
