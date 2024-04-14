using Domain.API;
using Domain.DataLayer.Contexts.Base;
using Domain.DataLayer.Contexts.FileDbContext;
using Domain.DataLayer.UnitOfWorks;
using Domain.Entities;
using DomainShared.Dtos.FileServer;
using Mapster;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.File
{
    public interface IFileServerService
    {
        TblFileServer GetActiveFileServer();
        ServiceResult<Guid> Add(CreateUpdateFileServerDto dto);
        ServiceResult SetAsActive(TblFileServer tblFileServer);
        ServiceResult Update(CreateUpdateFileServerDto dto, TblFileServer tblFileServer);
        ServiceResult Delete(TblFileServer tblFileServer);
    }

    public class FileServerService : IFileServerService
    {
        private readonly Core _core;

        public FileServerService(Core core)
        {
            _core = core;
        }

        #region Helpers

        private ServiceResult CreateTable(string connectionString)
        {
            try
            {
                bool isExist = false;
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    string extistCommand = "IF OBJECT_ID (N'TBlFile',N'U') Is not null select 'True' as res else Select 'False' as res";
                    using (SqlCommand sqlCommand = new SqlCommand(extistCommand, sqlConnection))
                    {
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader is not null)
                                while (reader.Read())
                                    isExist = Convert.ToBoolean(reader["res"]);
                        }
                    }

                    string createTableCommand = "CRAETE TABLE TblFile (Id uniqueidentifier NOT NULL,Data VARBINARY(MAX), PRIMARY KEY(Id))";
                    if (!isExist)
                        using (SqlCommand command = new SqlCommand(createTableCommand, sqlConnection))
                        {
                            command.ExecuteNonQuery();
                        }

                    sqlConnection.Close();
                }

                return new ServiceResult();
            }
            catch (Exception ex)
            {
                ElmahCore.ElmahExtensions.RaiseError(ex);
                return new ServiceResult("An Error occured: " + ex.Message);
            }
        }

        private ServiceResult SetAllDeActived()
        {
            var result = _core.ExecuteNonQueryCommand("UPDATE TblFile SET IsActive = 0 WHERE  IsActive = 1");
            if (result.Failure)
                ElmahCore.ElmahExtensions.RaiseError(result.Result);

            return new ServiceResult();
        }

        #endregion

        #region CUD

        public ServiceResult<Guid> Add(CreateUpdateFileServerDto dto)
        {
            try
            {
                var createResult = CreateTable(dto.ConnectionString);
                if (createResult.Failure)
                    return new ServiceResult<Guid>(createResult.Messages);

                var deactiveResult = SetAllDeActived();
                if (deactiveResult.Failure)
                    return new ServiceResult<Guid>(deactiveResult.Messages);

                var fileServer = dto.Adapt<TblFileServer>();
                _core.TblFileServer.Add(fileServer);
                _core.Save();

                return new ServiceResult<Guid>(fileServer.Id);
            }
            catch (Exception ex)
            {
                ElmahCore.ElmahExtensions.RaiseError(ex);
                return new ServiceResult<Guid>("An Error occured: " + ex.Message);
            }
        }

        public ServiceResult Delete(TblFileServer tblFileServer)
        {
            _core.TblFileServer.Reomve(tblFileServer);
            _core.Save();
            return new ServiceResult();
        }

        public ServiceResult Update(CreateUpdateFileServerDto dto, TblFileServer tblFileServer)
        {
            tblFileServer.Title = dto.Title;
            tblFileServer.ConnectionString = dto.ConnectionString;

            _core.TblFileServer.Update(tblFileServer);
            _core.Save();
            return new ServiceResult();
        }

        public ServiceResult SetAsActive(TblFileServer tblFileServer)
        {
            tblFileServer.IsActive = true;

            var deactiveResult = SetAllDeActived();
            if (deactiveResult.Failure)
                return new ServiceResult(deactiveResult.Messages);

            _core.TblFileServer.Update(tblFileServer);
            _core.Save();

            return new ServiceResult();
        }
        #endregion

        #region Get
        public TblFileServer GetActiveFileServer() =>
            _core.TblFileServer.Get(x => x.IsActive && !x.IsDeleted).LastOrDefault();
        #endregion
    }
}
