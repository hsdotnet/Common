using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

namespace Framework.Common.Scheduler
{
    public class JobContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["JobDB"].ConnectionString;

        /// <summary>
        /// 持久化Job
        /// </summary>
        /// <param name="jobInfo">Job对象</param>
        public void AddJob(JobInfo jobInfo)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute("INSERT INTO [Sys_Job] ([JobName], [JobSeconds], [Description], [Status], [JobUrl], [IsValid]) VALUES (@JobName, @JobSeconds, @Description, @Status, @JobUrl, @IsValid)", jobInfo);
            }
        }

        /// <summary>
        /// 获取Job对象
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>Job对象</returns>
        public JobInfo Get(int id)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                return connection.Query<JobInfo>("SELECT JobId, JobName, JobSeconds, Description, Status, JobUrl, IsValid FROM Sys_Job WHERE JobId = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="jobInfo">Job对象</param>
        public void UpdateStatus(JobInfo jobInfo)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute("UPDATE [Sys_Job] SET [Status] = @Status WHERE JobId = @Id", jobInfo);
            }
        }

        /// <summary>
        /// 删除Job
        /// </summary>
        /// <param name="id">主键Id</param>
        public void DeleteJob(int id)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute("UPDATE [Sys_Job] SET [IsValid] = 0 WHERE JobId = @Id", new { Id = id });
            }
        }
    }
}