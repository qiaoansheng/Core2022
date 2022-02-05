using Core2022.Framework.Entity;
using Core2022.Framework.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Core2022.Framework.UnitOfWork
{
    public class AppUnitOfWork : DbContext, IUnitOfWork
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(Global.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Global.OrmModelInit.ForEach(t =>
            {
                modelBuilder.Model.AddEntityType(t);
            });
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<OrmEntity> CreateSet<OrmEntity>()
          where OrmEntity : class
        {
            return base.Set<OrmEntity>();
        }


        public new int SaveChanges()
        {
            EFLog();
            return base.SaveChanges();
        }

        #region EF Log
        private void EFLog()
        {
            IEnumerable<EntityEntry> list = this.ChangeTracker.Entries();
            foreach (var item in list)
            {
                //对应的表名
                string tableName = "";

                #region 获取表名
                Type type = item.Entity.GetType();
                Type patientMngAttrType = typeof(TableAttribute);
                TableAttribute attribute = null;
                if (type.IsDefined(patientMngAttrType, true))
                {
                    attribute = type.GetCustomAttributes(patientMngAttrType, true).FirstOrDefault() as TableAttribute;
                    if (attribute != null)
                    {
                        tableName = attribute.Name;
                    }
                }

                if (string.IsNullOrEmpty(tableName))
                {
                    tableName = type.Name;
                }
                #endregion

                BaseOrmModel model = item.Entity as BaseOrmModel;
                #region EntityState
                switch (item.State)
                {
                    //case EntityState.Detached:
                    //case EntityState.Unchanged:
                    //case EntityState.Deleted:
                    case EntityState.Modified:
                        model.UpdateTime = DateTime.Now;
                        model.Version = ++model.Version;
                        WriteEFUpdateLog(item, tableName);
                        break;
                    case EntityState.Added:
                        model.UpdateTime = DateTime.Now;
                        model.CreateTime = DateTime.Now;
                        model.IsDelete = false;
                        model.Version = 0;
                        WriteEFCreateLog(item, tableName);
                        break;
                }
                #endregion

            }
        }
        private void WriteEFCreateLog(EntityEntry entry, string tableName)
        {
            var propertyList = entry.CurrentValues.Properties;
            string userName = "系统用户";
            PropertyEntry keyEntity = entry.Property("KeyId");
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var prop in propertyList)
            {
                PropertyEntry entity = entry.Property(prop.Name);
                dic.Add(prop.Name, entity.CurrentValue);
            }
            DBLogCreateModel createLog = new DBLogCreateModel()
            {
                CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                TableName = tableName,
                OperatorUserName = userName,
                PrimaryKeyId = Guid.Parse(keyEntity.CurrentValue.ToString()),
                CreateValue = JsonConvert.SerializeObject(dic)
            };
            WriteLog(createLog);
        }

        /// <summary>
        /// 记录EF修改操作日志
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="tableName"></param>
        private void WriteEFUpdateLog(EntityEntry entry, string tableName)
        {
            var propertyList = entry.CurrentValues.Properties.Where(i => entry.Property(i.Name).IsModified);
            string userName = "系统用户";

            PropertyEntry keyEntity = entry.Property("KeyId");
            foreach (var prop in propertyList)
            {
                PropertyEntry entity = entry.Property(prop.Name);

                DBLogUpdateModel updateLog = new DBLogUpdateModel()
                {
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    CulumnName = prop.Name,
                    TableName = tableName,
                    OperatorUserName = userName,
                    CurrentValue = entity.CurrentValue,
                    OriginalValue = entity.OriginalValue,
                    PrimaryKeyId = Guid.Parse(keyEntity.CurrentValue.ToString()),

                };

                if (entity.OriginalValue == null || entity.CurrentValue == null)
                {
                    if (entity.OriginalValue != entity.CurrentValue)
                    {
                        WriteLog(updateLog);
                    }
                    continue;
                }
                if (!entity.OriginalValue.Equals(entity.CurrentValue))
                {
                    WriteLog(updateLog);
                }
            }
        }

        private void WriteLog(DBLogBaseModel updateLog)
        {
            updateLog.OperatorKeyId = "";
            DBLog.WriteLog(updateLog);
        }
        #endregion

    }
}
