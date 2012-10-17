using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Attendance
{
    public class AttendanceRecord
    {
        private static char[] SEPS = new char[]{' ', '\t'};
        public String Department { get; set;}
        public String Name { get; set; }
        public String Id { get; set; }
        public DateTime Date { get; set; }

        public AttendanceRecord()
        {
        }

        public AttendanceRecord(String str)
        {
            String[] fields = str.Split(SEPS);
            Department = fields[0];
            Name = fields[1];
            Id = fields[2];
            String dateStr = fields[3] + " " + fields[4];
            Date = DateTime.Parse(dateStr);
        }

        public override string ToString()
        {
            return Department + " " + Name + " " + Id + " " + Date.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static List<AttendanceRecord> LoadFile(String path)
        {
            var records = new List<AttendanceRecord>();
            using (StreamReader reader = new StreamReader(path))
            {
                while (true)
                {
                    String line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    var record = new AttendanceRecord(line);
                    records.Add(record);
                }
            }
            return records;
        }
    }
}
