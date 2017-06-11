using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Find_Password
{
    public class CrackPassword
    {
        private long password;
        private int? taskNumber;
        private bool passwordFound = false;
        private string encodedPassword;
        private List<Task> tasks = new List<Task>();

        public CrackPassword(List<KeyValuePair<long, long>> intervals, string encodedPassword)
        {
            this.encodedPassword = encodedPassword;

            foreach (KeyValuePair<long, long> interval in intervals)
            {
                tasks.Add(new Task(() => ComputeHash(interval)));
            }
        }

        public void FindPassword()
        {
            foreach(Task task in tasks)
            {
                task.Start();
            }
        }

        private void ComputeHash(KeyValuePair<long, long> interval)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                for (long number = interval.Key; number <= interval.Value; ++number)
                {
                    string hashNumber = EncodeNumber(sha256, number.ToString());

                    if ( hashNumber == encodedPassword )
                    {
                        password = number;
                        taskNumber = Task.CurrentId;
                        passwordFound = true;
                    }
                }
            }
        }

        private string EncodeNumber(SHA256 sha256, string numberToEncode)
        {
            return string.Join("", sha256
                        .ComputeHash(Encoding.UTF8.GetBytes(numberToEncode))
                        .Select(item => item.ToString("x2")));
        }

        public long Password
        {
            get { return password; }
        }

        public KeyValuePair<long, int?> GetResult()
        {
            while (!passwordFound){}
            return new KeyValuePair<long, int?>(password, taskNumber);
        }

        public int? TaskNumber
        {
            get { return taskNumber; }
        }

        public Task[] Tasks
        {
            get { return tasks.ToArray(); }
        }

        public bool PasswordFound
        {
            get { return passwordFound; }
        }
    }
}
