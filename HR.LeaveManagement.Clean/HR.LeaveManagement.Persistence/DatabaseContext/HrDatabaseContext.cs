using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using HR.LeaveManagement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
    public class HrDatabaseContext : DbContext
    {
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=YourDatabaseName;user=username;password=password");
            }
        }
        /// <summary>
        /// 데이터 베이스 모델이 만들어 질때 오버라이드
        /// Config의 어셈블리에 있을 수 있는 구성을 가져오기
        /// 초기 LeaveType에대한 모델 생성
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);

            // LeaveTypeConfig class 로 이동
            //modelBuilder.Entity<LeaveType>().HasData(
            //        new LeaveType
            //        {
            //            Id = 1,
            //            Name = "Vacation",
            //            DefaltDays = 10,
            //            DateCreated = DateTime.Now,
            //            DateModified = DateTime.Now
            //        }
            //    );
            //modelBuilder.ApplyConfiguration(new LeaveTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// DbContext에 저장이 발생 시 오버라이드 함수 
        /// 현재 수정 혹은 추가시 현재 시각 입력하도록 만듬
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {            
            foreach (var entity in base.ChangeTracker.Entries<BaseEntity>()
                                    .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))

            {
                entity.Entity.DateModified = DateTime.Now;
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.DateCreated = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
