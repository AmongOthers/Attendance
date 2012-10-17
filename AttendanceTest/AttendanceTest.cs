using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Attendance;
using System.IO;

namespace AttendanceTest
{
    [TestClass]
    public class AttendanceTest
    {
        [TestMethod]
        public void testReadRecord()
        {
            string str = "总公司	罗洪鹏	2	2012-04-05 12:33:41	1		FP	";
            var record = new AttendanceRecord(str);
            Assert.AreEqual("总公司", record.Department);
            Assert.AreEqual("罗洪鹏", record.Name);
            Assert.AreEqual("2", record.Id);
            DateTime date = DateTime.Parse("2012-04-05 12:33:41");
            Assert.AreEqual(date, record.Date);
        }

        [TestMethod]
        public void testProduceTestData()
        {
            var morningDate = DateTime.Parse("2012-01-01 09:00:00");
            var afternoonDate = DateTime.Parse("2012-01-01 18:00:00");
            var list = new List<AttendanceRecord>();
            for (int i = 0; i < 31; i++)
            {
                var morning = new AttendanceRecord
                {
                    Department = "开发",
                    Name = "郑文伟",
                    Id = "10",
                    Date = morningDate.AddDays(i)
                };
                var afternoon = new AttendanceRecord
                {
                    Department = "开发",
                    Name = "郑文伟",
                    Id = "10",
                    Date = afternoonDate.AddDays(i)
                };
                list.Add(morning);
                list.Add(afternoon);
            }
            String path = "I:\\GitHub\\Attendance\\AttendanceTest\\bin\\Debug\\testdata.txt";
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var item in list)
                {
                    writer.WriteLine(item.ToString());
                }
            }
            var validate = AttendanceRecord.LoadFile(path);
            Assert.AreEqual(list.Count, validate.Count);
        }

        [TestMethod]
        public void testReadRecordFile()
        {
            String path = "I:\\GitHub\\Attendance\\AttendanceTest\\bin\\Debug\\recordsOfOnePerson.txt";
            var records = AttendanceRecord.LoadFile(path);
            var test = records[2];
            Assert.AreEqual(DateTime.Parse("2012-04-09 20:55:38"), test.Date);
            Assert.AreEqual(13, records.Count);
        }

        //全勤, VIVA
        [TestMethod]
        public void testFull()
        {
            String path = "I:\\GitHub\\Attendance\\AttendanceTest\\bin\\Debug\\testdata.txt";
            var monthRecord = new MonthRecord();
            var records = AttendanceRecord.LoadFile(path);
            foreach(var record in records)
            {
                monthRecord.Add(record);
            }
            Assert.AreEqual(2012, monthRecord.Year);
            Assert.AreEqual(1, monthRecord.Month);
            Assert.AreEqual(22, monthRecord.Workdays);
            Assert.AreEqual(0, monthRecord.LateRecords.Count);
            Assert.AreEqual(0, monthRecord.EarlyLeaveRecords.Count);
            Assert.AreEqual(0, monthRecord.AbsenteeismRecords.Count);
            Assert.AreEqual(0, monthRecord.OvertimeRecords.Count);
        }

        [TestMethod]
        public void testRecordOfMonth()
        {
            String path = "";
            var monthRecord = new MonthRecord();
            var records = AttendanceRecord.LoadFile(path);
            foreach(var record in records)
            {
                monthRecord.Add(record);
            }
            Assert.AreEqual(2, monthRecord.LateRecords.Count);
            Assert.AreEqual(2, monthRecord.EarlyLeaveRecords.Count);
            Assert.AreEqual(2, monthRecord.AbsenteeismRecords.Count);
            Assert.AreEqual(2, monthRecord.OvertimeRecords.Count);
        }
    }
}
